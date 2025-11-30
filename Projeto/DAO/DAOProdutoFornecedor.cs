using MySql.Data.MySqlClient;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Projeto.DAO
{
    internal class DAOProdutoFornecedor
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString;

        public void Salvar(ProdutoFornecedor relacao)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT IGNORE INTO ProdutoFornecedores (ProdutoId, FornecedorId) VALUES (@ProdutoId, @FornecedorId)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProdutoId", relacao.ProdutoId);
                        cmd.Parameters.AddWithValue("@FornecedorId", relacao.FornecedorId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar a relação produto-fornecedor: " + ex.Message);
            }
        }

        public void Excluir(ProdutoFornecedor relacao)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM ProdutoFornecedores WHERE ProdutoId = @ProdutoId AND FornecedorId = @FornecedorId";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProdutoId", relacao.ProdutoId);
                    cmd.Parameters.AddWithValue("@FornecedorId", relacao.FornecedorId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<ProdutoFornecedor> ListarPorProduto(int produtoId)
        {
            List<ProdutoFornecedor> lista = new List<ProdutoFornecedor>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT ProdutoId, FornecedorId FROM ProdutoFornecedores WHERE ProdutoId = @ProdutoId";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ProdutoId", produtoId);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new ProdutoFornecedor
                            {
                                ProdutoId = reader.GetInt32("ProdutoId"),
                                FornecedorId = reader.GetInt32("FornecedorId")
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public void SincronizarFornecedores(List<ProdutoFornecedor> fornecedores, int produtoId)
        {
            using (var conexao = new MySqlConnection(connectionString))
            {
                conexao.Open();
                MySqlTransaction transaction = conexao.BeginTransaction();

                try
                {
                    string deleteQuery = "DELETE FROM ProdutoFornecedores WHERE ProdutoId = @ProdutoId";
                    using (var cmdDelete = new MySqlCommand(deleteQuery, conexao, transaction))
                    {
                        cmdDelete.Parameters.AddWithValue("@ProdutoId", produtoId);
                        cmdDelete.ExecuteNonQuery();
                    }

                    if (fornecedores != null && fornecedores.Count > 0)
                    {
                        string insertQuery = "INSERT INTO ProdutoFornecedores (ProdutoId, FornecedorId) VALUES (@ProdutoId, @FornecedorId)";
                        using (var cmdInsert = new MySqlCommand(insertQuery, conexao, transaction))
                        {
                            foreach (var relacao in fornecedores)
                            {
                                cmdInsert.Parameters.Clear();
                                cmdInsert.Parameters.AddWithValue("@ProdutoId", produtoId);
                                cmdInsert.Parameters.AddWithValue("@FornecedorId", relacao.FornecedorId);
                                cmdInsert.ExecuteNonQuery();
                            }
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw; 
                }
            }
        }
    }
}