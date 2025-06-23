using MySql.Data.MySqlClient;
using Projeto.Models;
using System;
using System.Collections.Generic;

namespace Projeto.DAO
{
    internal class DAOEstado
    {
        private string connectionString = "Server=localhost;Database=sistema;Uid=root;Pwd=12345678;";

        public void Salvar(Estado estado)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query;

                    if (estado.Id > 0)
                    {
                        query = @"UPDATE estados 
                                  SET nome = @nome, 
                                      uf = @uf,
                                      id_pais = @id_pais, 
                                      status = @status, 
                                      data_modificacao = @data_modificacao 
                                  WHERE id = @id";
                    }
                    else
                    {
                        query = @"INSERT INTO estados 
                                  (nome, uf, id_pais, status, data_criacao, data_modificacao) 
                                  VALUES (@nome, @uf, @id_pais, @status, @data_criacao, @data_modificacao)";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nome", estado.Nome);
                        cmd.Parameters.AddWithValue("@uf", estado.UF);
                        cmd.Parameters.AddWithValue("@id_pais", estado.IdPais);
                        cmd.Parameters.AddWithValue("@status", estado.Status);
                        cmd.Parameters.AddWithValue("@data_modificacao", estado.DataModificacao);

                        if (estado.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@id", estado.Id);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@data_criacao", estado.DataCriacao);
                        }

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar estado: " + ex.Message);
            }
        }

        public void Excluir(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM estados WHERE id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Estado BuscarPorId(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT e.id, e.nome AS estado_nome, e.uf, e.id_pais, e.status, 
                                        e.data_criacao, e.data_modificacao, 
                                        p.nome AS pais_nome 
                                 FROM estados e
                                 JOIN paises p ON e.id_pais = p.id
                                 WHERE e.id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Estado
                            {
                                Id = reader.GetInt32("id"),
                                Nome = reader.GetString("estado_nome"),
                                UF = reader.GetString("uf"),
                                IdPais = reader.GetInt32("id_pais"),
                                PaisNome = reader.GetString("pais_nome"),
                                Status = reader.GetBoolean("status"),
                                DataCriacao = reader.IsDBNull(reader.GetOrdinal("data_criacao")) ? (DateTime?)null : reader.GetDateTime("data_criacao"),
                                DataModificacao = reader.IsDBNull(reader.GetOrdinal("data_modificacao")) ? (DateTime?)null : reader.GetDateTime("data_modificacao")
                            };
                        }
                    }
                }
            }
            return null;
        }

        public List<Estado> ListarEstado()
        {
            List<Estado> lista = new List<Estado>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT e.id, e.nome AS estado_nome, e.uf, e.id_pais, e.status, p.nome AS pais_nome 
                    FROM estados e
                    JOIN paises p ON e.id_pais = p.id
                    ORDER BY e.nome";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Estado
                            {
                                Id = reader.GetInt32("id"),
                                Nome = reader.GetString("estado_nome"),
                                UF = reader.GetString("uf"),
                                IdPais = reader.GetInt32("id_pais"),
                                PaisNome = reader.GetString("pais_nome"),
                                Status = reader.GetBoolean("status"),
                            });
                        }
                    }
                }
            }

            return lista;
        }
    }
}
