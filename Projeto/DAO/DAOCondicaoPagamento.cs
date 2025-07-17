using MySql.Data.MySqlClient;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Projeto.DAO
{
    internal class DAOCondicaoPagamento
    {
        private string connectionString = "Server=localhost;Database=sistema;Uid=root;Pwd=12345678;";

        public int SalvarCondicaoPagamento(CondicaoPagamento condicao)
        {
            int condicaoId = 0;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query;

                    if (condicao.Id > 0)
                    {
                        query = @"
                        UPDATE CondicoesPagamento SET 
                            Descricao = @Descricao, QtdParcelas = @QtdParcelas, Juros = @Juros, 
                            Multa = @Multa, Desconto = @Desconto, Ativo = @Ativo, 
                            DataAlteracao = @DataAlteracao
                        WHERE Id = @Id";
                    }
                    else
                    {
                        query = @"
                        INSERT INTO CondicoesPagamento (
                            Descricao, QtdParcelas, Juros, Multa, Desconto, Ativo, 
                            DataCadastro, DataAlteracao
                        )
                        VALUES (
                            @Descricao, @QtdParcelas, @Juros, @Multa, @Desconto, @Ativo, 
                            @DataCadastro, @DataAlteracao
                        )";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Descricao", condicao.Descricao);
                        cmd.Parameters.AddWithValue("@QtdParcelas", condicao.QtdParcelas);
                        cmd.Parameters.AddWithValue("@Juros", condicao.Juros);
                        cmd.Parameters.AddWithValue("@Multa", condicao.Multa);
                        cmd.Parameters.AddWithValue("@Desconto", condicao.Desconto);
                        cmd.Parameters.AddWithValue("@Ativo", condicao.Ativo);
                        cmd.Parameters.AddWithValue("@DataAlteracao", condicao.DataAlteracao ?? DateTime.Now);

                        if (condicao.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@Id", condicao.Id);
                            cmd.ExecuteNonQuery();
                            condicaoId = condicao.Id;
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@DataCadastro", condicao.DataCadastro ?? DateTime.Now);
                            cmd.ExecuteNonQuery();
                            condicaoId = (int)cmd.LastInsertedId;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar condição de pagamento: " + ex.Message);
            }

            return condicaoId;
        }

        public void Excluir(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM CondicoesPagamento WHERE Id = @Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public CondicaoPagamento BuscarPorId(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                SELECT Id, Descricao, QtdParcelas, Juros, Multa, Desconto, Ativo,
                       DataCadastro, DataAlteracao
                FROM CondicoesPagamento
                WHERE Id = @Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new CondicaoPagamento
                            {
                                Id = reader.GetInt32("Id"),
                                Descricao = reader.GetString("Descricao"),
                                QtdParcelas = reader.GetInt32("QtdParcelas"),
                                Juros = reader.GetDecimal("Juros"),
                                Multa = reader.GetDecimal("Multa"),
                                Desconto = reader.GetDecimal("Desconto"),
                                Ativo = reader.GetBoolean("Ativo"),
                                DataCadastro = reader.IsDBNull(reader.GetOrdinal("DataCadastro")) ? (DateTime?)null : reader.GetDateTime("DataCadastro"),
                                DataAlteracao = reader.IsDBNull(reader.GetOrdinal("DataAlteracao")) ? (DateTime?)null : reader.GetDateTime("DataAlteracao"),
                            };
                        }
                    }
                }
            }
            return null;
        }

        public List<CondicaoPagamento> ListarCondicaoPagamento()
        {
            List<CondicaoPagamento> lista = new List<CondicaoPagamento>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                SELECT Id, Descricao, QtdParcelas, Juros, Multa, Desconto, Ativo,
                       DataCadastro, DataAlteracao
                FROM CondicoesPagamento
                ORDER BY Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new CondicaoPagamento
                            {
                                Id = reader.GetInt32("Id"),
                                Descricao = reader.GetString("Descricao"),
                                QtdParcelas = reader.GetInt32("QtdParcelas"),
                                Juros = reader.GetDecimal("Juros"),
                                Multa = reader.GetDecimal("Multa"),
                                Desconto = reader.GetDecimal("Desconto"),
                                Ativo = reader.GetBoolean("Ativo"),
                                DataCadastro = reader.IsDBNull(reader.GetOrdinal("DataCadastro")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("DataCadastro")),
                                DataAlteracao = reader.IsDBNull(reader.GetOrdinal("DataAlteracao")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("DataAlteracao"))
                            });
                        }
                    }
                }
            }

            return lista;
        }
    }
}