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
                        query = @"UPDATE Paises 
                                  SET Pais = @Pais, 
                                      Ativo = @Ativo,
                                      DataAlteracao = @DataAlteracao 
                                  WHERE Id = @Id";
                    }
                    else
                    {
                        query = @"INSERT INTO Paises 
                                  (Pais, Ativo, DataCadastro, DataAlteracao) 
                                  VALUES (@Pais, @Ativo, @DataCadastro, @DataAlteracao)";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Pais", pais.NomePais);
                        cmd.Parameters.AddWithValue("@Ativo", pais.Ativo);
                        cmd.Parameters.AddWithValue("@DataAlteracao", pais.DataAlteracao);

                        if (pais.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@Id", pais.Id);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@DataCadastro", pais.DataCadastro);
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
                string query = "DELETE FROM Paises WHERE Id = @Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Pais BuscarPorId(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Id, Pais, Ativo, DataCadastro, DataAlteracao FROM Paises WHERE Id = @Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Pais
                            {
                                Id = reader.GetInt32("Id"),
                                NomePais = reader.GetString("Pais"),
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

        public List<Pais> ListarPais()
        {
            List<Pais> lista = new List<Pais>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Id, Pais, Ativo, DataCadastro, DataAlteracao FROM Paises ORDER BY Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Pais
                            {
                                Id = reader.GetInt32("Id"),
                                NomePais = reader.GetString("Pais"),
                                Ativo = reader.GetBoolean("Ativo"),
                                DataCadastro = reader.IsDBNull(reader.GetOrdinal("DataCadastro")) ? (DateTime?)null : reader.GetDateTime("DataCadastro"),
                                DataAlteracao = reader.IsDBNull(reader.GetOrdinal("DataAlteracao")) ? (DateTime?)null : reader.GetDateTime("DataAlteracao"),
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}