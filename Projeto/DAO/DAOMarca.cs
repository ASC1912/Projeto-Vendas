using MySql.Data.MySqlClient;
using Projeto.Models;
using System;
using System.Collections.Generic;

namespace Projeto.DAO
{
    internal class DAOMarca
    {
        private string connectionString = "Server=localhost;Database=sistema;Uid=root;Pwd=12345678;";

        public void Salvar(Marca marca)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query;

                    if (marca.Id > 0)
                    {
                        query = @"
                        UPDATE marcas SET 
                            nome = @nome, 
                            status = @status,
                            data_modificacao = @data_modificacao
                        WHERE id = @id";
                    }
                    else
                    {
                        query = @"
                        INSERT INTO marcas (
                            nome, status, data_criacao, data_modificacao
                        ) VALUES (
                            @nome, @status, @data_criacao, @data_modificacao
                        )";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nome", marca.Nome);
                        cmd.Parameters.AddWithValue("@status", marca.Status);
                        cmd.Parameters.AddWithValue("@data_modificacao", marca.DataModificacao ?? DateTime.Now);

                        if (marca.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@id", marca.Id);
                        }   
                        else
                        {
                            cmd.Parameters.AddWithValue("@data_criacao", marca.DataCriacao ?? DateTime.Now);
                        }

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar marca: " + ex.Message);
            }
        }

        public void Excluir(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM marcas WHERE id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Marca BuscarPorId(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT id, nome, status, data_criacao, data_modificacao
                    FROM marcas 
                    WHERE id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Marca
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Nome = reader.GetString(reader.GetOrdinal("nome")),
                                Status = reader.GetBoolean(reader.GetOrdinal("status")),
                                DataCriacao = reader.IsDBNull(reader.GetOrdinal("data_criacao")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("data_criacao")),
                                DataModificacao = reader.IsDBNull(reader.GetOrdinal("data_modificacao")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("data_modificacao")),
                            };
                        }
                    }
                }
            }

            return null;
        }

        public List<Marca> ListarMarcas()
        {
            List<Marca> lista = new List<Marca>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT id, nome, status, data_criacao, data_modificacao
                    FROM marcas 
                    ORDER BY nome";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Marca
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Nome = reader.GetString(reader.GetOrdinal("nome")),
                                Status = reader.GetBoolean(reader.GetOrdinal("status")),
                                DataCriacao = reader.IsDBNull(reader.GetOrdinal("data_criacao")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("data_criacao")),
                                DataModificacao = reader.IsDBNull(reader.GetOrdinal("data_modificacao")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("data_modificacao")),
                            });
                        }
                    }
                }
            }

            return lista;
        }
    }
}
