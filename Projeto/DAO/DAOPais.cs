using MySql.Data.MySqlClient;
using Projeto.Models;
using System;
using System.Collections.Generic;

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
                                  SET pais = @pais, 
                                      ativo = @ativo,
                                      data_alteracao = @data_alteracao 
                                  WHERE id = @id";
                    }
                    else
                    {
                        query = @"INSERT INTO paises 
                                  (pais, ativo, data_cadastro, data_alteracao) 
                                  VALUES (@pais, @ativo, @data_cadastro, @data_alteracao)";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@pais", pais.NomePais);
                        cmd.Parameters.AddWithValue("@ativo", pais.Ativo);
                        cmd.Parameters.AddWithValue("@data_alteracao", pais.DataAlteracao);

                        if (pais.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@id", pais.Id);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@data_cadastro", pais.DataCadastro);
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
                string query = "SELECT id, pais, ativo, data_cadastro, data_alteracao FROM paises WHERE id = @id";

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
                                NomePais = reader.GetString("pais"),
                                Ativo = reader.GetBoolean("ativo"),
                                DataCadastro = reader.IsDBNull(reader.GetOrdinal("data_cadastro")) ? (DateTime?)null : reader.GetDateTime("data_cadastro"),
                                DataAlteracao = reader.IsDBNull(reader.GetOrdinal("data_alteracao")) ? (DateTime?)null : reader.GetDateTime("data_alteracao"),
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
                string query = "SELECT id, pais, ativo, data_cadastro, data_alteracao FROM paises ORDER BY id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Pais
                            {
                                Id = reader.GetInt32("id"),
                                NomePais = reader.GetString("pais"),
                                Ativo = reader.GetBoolean("ativo"),
                                DataCadastro = reader.IsDBNull(reader.GetOrdinal("data_cadastro")) ? (DateTime?)null : reader.GetDateTime("data_cadastro"),
                                DataAlteracao = reader.IsDBNull(reader.GetOrdinal("data_alteracao")) ? (DateTime?)null : reader.GetDateTime("data_alteracao"),
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}
