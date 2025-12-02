using MySql.Data.MySqlClient;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Projeto.DAO
{
    internal class DAOPais
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString;
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
                                      Sigla = @Sigla,
                                      DDI = @DDI,
                                      Moeda = @Moeda,
                                      Ativo = @Ativo,
                                      DataAlteracao = @DataAlteracao 
                                  WHERE Id = @Id";
                    }
                    else
                    {
                        query = @"INSERT INTO Paises 
                                  (Pais, Sigla, DDI, Moeda, Ativo, DataCadastro, DataAlteracao) 
                                  VALUES (@Pais, @Sigla, @DDI, @Moeda, @Ativo, @DataCadastro, @DataAlteracao)";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Pais", pais.NomePais);
                        cmd.Parameters.AddWithValue("@Sigla", pais.Sigla);
                        cmd.Parameters.AddWithValue("@DDI", pais.DDI);
                        cmd.Parameters.AddWithValue("@Moeda", pais.Moeda);
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
                string query = "SELECT Id, Pais, Sigla, DDI, Moeda, Ativo, DataCadastro, DataAlteracao FROM Paises WHERE Id = @Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Pais
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                NomePais = reader.GetString(reader.GetOrdinal("Pais")),
                                Sigla = reader.IsDBNull(reader.GetOrdinal("Sigla")) ? null : reader.GetString(reader.GetOrdinal("Sigla")),
                                DDI = reader.IsDBNull(reader.GetOrdinal("DDI")) ? null : reader.GetString(reader.GetOrdinal("DDI")),
                                Moeda = reader.IsDBNull(reader.GetOrdinal("Moeda")) ? null : reader.GetString(reader.GetOrdinal("Moeda")),
                                Ativo = reader.GetBoolean(reader.GetOrdinal("Ativo")),
                                DataCadastro = reader.IsDBNull(reader.GetOrdinal("DataCadastro")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("DataCadastro")),
                                DataAlteracao = reader.IsDBNull(reader.GetOrdinal("DataAlteracao")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("DataAlteracao")),
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
                string query = "SELECT Id, Pais, Sigla, DDI, Moeda, Ativo, DataCadastro, DataAlteracao FROM Paises ORDER BY Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Pais
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                NomePais = reader.GetString(reader.GetOrdinal("Pais")),
                                Sigla = reader.IsDBNull(reader.GetOrdinal("Sigla")) ? null : reader.GetString(reader.GetOrdinal("Sigla")),
                                DDI = reader.IsDBNull(reader.GetOrdinal("DDI")) ? null : reader.GetString(reader.GetOrdinal("DDI")),
                                Moeda = reader.IsDBNull(reader.GetOrdinal("Moeda")) ? null : reader.GetString(reader.GetOrdinal("Moeda")),
                                Ativo = reader.GetBoolean(reader.GetOrdinal("Ativo")),
                                DataCadastro = reader.IsDBNull(reader.GetOrdinal("DataCadastro")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("DataCadastro")),
                                DataAlteracao = reader.IsDBNull(reader.GetOrdinal("DataAlteracao")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("DataAlteracao")),
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}