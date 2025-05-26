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
                                      status = @status,
                                      data_modificacao = @data_modificacao 
                                  WHERE id = @id";
                    }
                    else 
                    {
                        query = @"INSERT INTO formas_pagamento 
                                  (descricao, status, data_criacao, data_modificacao) 
                                  VALUES (@descricao, @status, @data_criacao, @data_modificacao)";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@descricao", formaPagamento.Descricao);
                        cmd.Parameters.AddWithValue("@status", formaPagamento.Status);
                        cmd.Parameters.AddWithValue("@data_modificacao", formaPagamento.DataModificacao);

                        if (formaPagamento.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@id", formaPagamento.Id);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@data_criacao", formaPagamento.DataCriacao);
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
                string query = @"SELECT id, descricao, status, data_criacao, data_modificacao 
                                 FROM formas_pagamento 
                                 WHERE id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int colId = reader.GetOrdinal("id");
                            int colDescricao = reader.GetOrdinal("descricao");
                            int colStatus = reader.GetOrdinal("status");
                            int colDataCriacao = reader.GetOrdinal("data_criacao");
                            int colDataModificacao = reader.GetOrdinal("data_modificacao");

                            return new FormaPagamento
                            {
                                Id = reader.GetInt32(colId),
                                Descricao = reader.GetString(colDescricao),
                                Status = reader.IsDBNull(colStatus) ? true : reader.GetBoolean(colStatus),
                                DataCriacao = reader.IsDBNull(colDataCriacao) ? DateTime.MinValue : reader.GetDateTime(colDataCriacao),
                                DataModificacao = reader.IsDBNull(colDataModificacao) ? DateTime.MinValue : reader.GetDateTime(colDataModificacao)
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
                string query = @"SELECT id, descricao, status, data_criacao, data_modificacao 
                                 FROM formas_pagamento 
                                 ORDER BY id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int colId = reader.GetOrdinal("id");
                        int colDescricao = reader.GetOrdinal("descricao");
                        int colStatus = reader.GetOrdinal("status");
                        int colDataCriacao = reader.GetOrdinal("data_criacao");
                        int colDataModificacao = reader.GetOrdinal("data_modificacao");

                        while (reader.Read())
                        {
                            lista.Add(new FormaPagamento
                            {
                                Id = reader.GetInt32(colId),
                                Descricao = reader.GetString(colDescricao),
                                Status = reader.IsDBNull(colStatus) ? true : reader.GetBoolean(colStatus),
                                DataCriacao = reader.IsDBNull(colDataCriacao) ? DateTime.MinValue : reader.GetDateTime(colDataCriacao),
                                DataModificacao = reader.IsDBNull(colDataModificacao) ? DateTime.MinValue : reader.GetDateTime(colDataModificacao)
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
