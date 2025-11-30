using MySql.Data.MySqlClient;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Configuration;

namespace Projeto.DAO
{
    internal class DAOFormaPagamento
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString;

        public void Salvar(FormaPagamento formaPagamento)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query;

                    if (formaPagamento.Id > 0)
                    {
                        query = @"UPDATE FormasPagamento 
                                  SET Descricao = @Descricao, 
                                      Ativo = @Ativo,
                                      DataAlteracao = @DataAlteracao 
                                  WHERE Id = @Id";
                    }
                    else
                    {
                        query = @"INSERT INTO FormasPagamento 
                                  (Descricao, Ativo, DataCadastro, DataAlteracao) 
                                  VALUES (@Descricao, @Ativo, @DataCadastro, @DataAlteracao)";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Descricao", formaPagamento.Descricao);
                        cmd.Parameters.AddWithValue("@Ativo", formaPagamento.Ativo);
                        cmd.Parameters.AddWithValue("@DataAlteracao", formaPagamento.DataAlteracao ?? DateTime.Now);

                        if (formaPagamento.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@Id", formaPagamento.Id);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@DataCadastro", formaPagamento.DataCadastro ?? DateTime.Now);
                        }

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar forma de pagamento: " + ex.Message);
            }
        }

        public void Excluir(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM FormasPagamento WHERE Id = @Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public FormaPagamento BuscarPorId(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT Id, Descricao, Ativo, DataCadastro, DataAlteracao 
                                 FROM FormasPagamento 
                                 WHERE Id = @Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new FormaPagamento
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Descricao = reader.GetString(reader.GetOrdinal("Descricao")),
                                Ativo = reader.GetBoolean(reader.GetOrdinal("Ativo")),
                                DataCadastro = reader.IsDBNull(reader.GetOrdinal("DataCadastro")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("DataCadastro")),
                                DataAlteracao = reader.IsDBNull(reader.GetOrdinal("DataAlteracao")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("DataAlteracao"))
                            };
                        }
                    }
                }
            }
            return null;
        }

        public List<FormaPagamento> ListarFormaPagamento()
        {
            List<FormaPagamento> lista = new List<FormaPagamento>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT Id, Descricao, Ativo, DataCadastro, DataAlteracao 
                                 FROM FormasPagamento 
                                 ORDER BY Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new FormaPagamento
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Descricao = reader.GetString(reader.GetOrdinal("Descricao")),
                                Ativo = reader.GetBoolean(reader.GetOrdinal("Ativo")),
                                DataCadastro = reader.IsDBNull(reader.GetOrdinal("DataCadastro")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("DataCadastro")),
                                DataAlteracao = reader.IsDBNull(reader.GetOrdinal("DataAlteracao")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("DataAlteracao"))
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public string ObterDescricaoFormaPagamento(int id)
        {
            string descricao = string.Empty;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Descricao FROM FormasPagamento WHERE Id = @Id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        descricao = cmd.ExecuteScalar()?.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao buscar descrição da forma de pagamento: " + ex.Message);
                }
            }
            return descricao;
        }

        public int ObterIdFormaPagamento(string descricao)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Id FROM FormasPagamento WHERE Descricao = @Descricao";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Descricao", descricao);
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
        }
    }
}