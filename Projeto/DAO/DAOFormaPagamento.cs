using MySql.Data.MySqlClient;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Projeto.DAO
{
    internal class DAOFormaPagamento
    {
        private string connectionString = "Server=localhost;Database=sistema;Uid=root;Pwd=12345678;";

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
                        query = @"UPDATE formas_pagamento 
                                  SET descricao = @descricao, 
                                      ativo = @ativo,
                                      data_alteracao = @data_alteracao 
                                  WHERE id = @id";
                    }
                    else
                    {
                        query = @"INSERT INTO formas_pagamento 
                                  (descricao, ativo, data_cadastro, data_alteracao) 
                                  VALUES (@descricao, @ativo, @data_cadastro, @data_alteracao)";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@descricao", formaPagamento.Descricao);
                        cmd.Parameters.AddWithValue("@ativo", formaPagamento.Ativo);
                        cmd.Parameters.AddWithValue("@data_alteracao", formaPagamento.DataAlteracao ?? DateTime.Now);

                        if (formaPagamento.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@id", formaPagamento.Id);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@data_cadastro", formaPagamento.DataCadastro ?? DateTime.Now);
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
                string query = "DELETE FROM formas_pagamento WHERE id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public FormaPagamento BuscarPorId(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT id, descricao, ativo, data_cadastro, data_alteracao 
                                 FROM formas_pagamento 
                                 WHERE id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new FormaPagamento
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Descricao = reader.GetString(reader.GetOrdinal("descricao")),
                                Ativo = reader.GetBoolean(reader.GetOrdinal("ativo")),
                                DataCadastro = reader.IsDBNull(reader.GetOrdinal("data_cadastro")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("data_cadastro")),
                                DataAlteracao = reader.IsDBNull(reader.GetOrdinal("data_alteracao")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("data_alteracao"))
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
                string query = @"SELECT id, descricao, ativo, data_cadastro, data_alteracao 
                                 FROM formas_pagamento 
                                 ORDER BY id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new FormaPagamento
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Descricao = reader.GetString(reader.GetOrdinal("descricao")),
                                Ativo = reader.GetBoolean(reader.GetOrdinal("ativo")),
                                DataCadastro = reader.IsDBNull(reader.GetOrdinal("data_cadastro")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("data_cadastro")),
                                DataAlteracao = reader.IsDBNull(reader.GetOrdinal("data_alteracao")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("data_alteracao"))
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
                    string query = "SELECT descricao FROM formas_pagamento WHERE id = @id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
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
                string query = "SELECT id FROM formas_pagamento WHERE descricao = @descricao";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@descricao", descricao);
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
        }
    }
}
