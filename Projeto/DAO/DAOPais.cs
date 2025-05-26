using MySql.Data.MySqlClient;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.DAO
{
    internal class DAOPais
    {
        private string connectionString = "Server=localhost;Database=sistema;Uid=root;Pwd=12345678;";

        public void Salvar(Pais pais)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query;

                    if (pais.Id > 0)
                    {
                        query = @"UPDATE paises 
                          SET nome = @nome, 
                              status = @status,
                              data_modificacao = @data_modificacao 
                          WHERE id = @id";
                    }
                    else
                    {
                        query = @"INSERT INTO paises 
                          (nome, status, data_criacao, data_modificacao) 
                          VALUES (@nome, @status, @data_criacao, @data_modificacao)";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nome", pais.Nome);
                        cmd.Parameters.AddWithValue("@status", pais.Status);
                        cmd.Parameters.AddWithValue("@data_modificacao", pais.DataModificacao);

                        if (pais.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@id", pais.Id);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@data_criacao", pais.DataCriacao);
                        }

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar país: " + ex.Message);
            }
        }


        public void Excluir(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM paises WHERE id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Pais BuscarPorId(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT id, nome, status, data_criacao, data_modificacao FROM paises WHERE id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Pais
                            {
                                Id = reader.GetInt32("id"),
                                Nome = reader.GetString("nome"),
                                Status = reader.GetBoolean("status"),
                                DataCriacao = reader.GetDateTime("data_criacao"),
                                DataModificacao = reader.GetDateTime("data_modificacao")
                            };
                        }
                    }
                }
            }
            return null;
        }


        public List<Pais> ListarPais()
        {
            List<Pais> lista = new List<Pais>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT id, nome, status, data_criacao, data_modificacao FROM paises ORDER BY id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Pais
                            {
                                Id = reader.GetInt32("id"),
                                Nome = reader.GetString("nome"),
                                Status = reader.GetBoolean("status"),
                                DataCriacao = reader.IsDBNull(reader.GetOrdinal("data_criacao")) ? (DateTime?)null : reader.GetDateTime("data_criacao"),
                                DataModificacao = reader.IsDBNull(reader.GetOrdinal("data_modificacao")) ? (DateTime?)null : reader.GetDateTime("data_modificacao"),

                            });
                        }
                    }
                }
            }
            return lista;
        }

    }
}
