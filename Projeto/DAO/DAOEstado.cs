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
                                  SET estado = @estado, 
                                      uf = @uf,
                                      pais_id = @pais_id, 
                                      ativo = @ativo, 
                                      data_alteracao = @data_alteracao 
                                  WHERE id = @id";
                    }
                    else
                    {
                        query = @"INSERT INTO estados 
                                  (estado, uf, pais_id, ativo, data_cadastro, data_alteracao) 
                                  VALUES (@estado, @uf, @pais_id, @ativo, @data_cadastro, @data_alteracao)";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@estado", estado.NomeEstado);
                        cmd.Parameters.AddWithValue("@uf", estado.UF);
                        cmd.Parameters.AddWithValue("@pais_id", estado.PaisId);
                        cmd.Parameters.AddWithValue("@ativo", estado.Ativo);
                        cmd.Parameters.AddWithValue("@data_alteracao", estado.DataAlteracao);

                        if (estado.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@id", estado.Id);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@data_cadastro", estado.DataCadastro);
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
                string query = @"SELECT e.id, e.estado, e.uf, e.pais_id, e.ativo, 
                                        e.data_cadastro, e.data_alteracao, 
                                        p.pais AS pais_nome 
                                 FROM estados e
                                 JOIN paises p ON e.pais_id = p.id
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
                                NomeEstado = reader.GetString("estado"),
                                UF = reader.GetString("uf"),
                                PaisId = reader.GetInt32("pais_id"),
                                PaisNome = reader.GetString("pais_nome"),
                                Ativo = reader.GetBoolean("ativo"),
                                DataCadastro = reader.IsDBNull(reader.GetOrdinal("data_cadastro")) ? (DateTime?)null : reader.GetDateTime("data_cadastro"),
                                DataAlteracao = reader.IsDBNull(reader.GetOrdinal("data_alteracao")) ? (DateTime?)null : reader.GetDateTime("data_alteracao")
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
                    SELECT e.id, e.estado, e.uf, e.pais_id, e.ativo, p.pais AS pais_nome 
                    FROM estados e
                    JOIN paises p ON e.pais_id = p.id
                    ORDER BY e.estado";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Estado
                            {
                                Id = reader.GetInt32("id"),
                                NomeEstado = reader.GetString("estado"),
                                UF = reader.GetString("uf"),
                                PaisId = reader.GetInt32("pais_id"),
                                PaisNome = reader.GetString("pais_nome"),
                                Ativo = reader.GetBoolean("ativo"),
                            });
                        }
                    }
                }
            }

            return lista;
        }
    }
}
