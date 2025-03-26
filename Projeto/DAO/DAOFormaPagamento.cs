using MySql.Data.MySqlClient;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                        query = "UPDATE formas_pagamento SET descricao = @descricao WHERE id = @id";
                    }
                    else 
                    {
                        query = "INSERT INTO formas_pagamento (descricao) VALUES (@descricao)";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (formaPagamento.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@id", formaPagamento.Id);
                        }

                        cmd.Parameters.AddWithValue("@descricao", formaPagamento.Descricao);
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
                string query = "SELECT id, descricao FROM formas_pagamento WHERE id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new FormaPagamento
                            {
                                Id = reader.GetInt32("id"),
                                Descricao = reader.GetString("descricao")
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
                string query = "SELECT id, descricao FROM formas_pagamento ORDER BY id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new FormaPagamento
                            {
                                Id = reader.GetInt32("id"),
                                Descricao = reader.GetString("descricao")
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
                    string query = "SELECT Descricao FROM formas_pagamento WHERE Id = @id";
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
