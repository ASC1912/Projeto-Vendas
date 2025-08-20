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
                        query = @"UPDATE Estados 
                                  SET Estado = @Estado, 
                                      UF = @UF,
                                      PaisId = @PaisId, 
                                      Ativo = @Ativo, 
                                      DataAlteracao = @DataAlteracao 
                                  WHERE Id = @Id";
                    }
                    else
                    {
                        query = @"INSERT INTO Estados 
                                  (Estado, UF, PaisId, Ativo, DataCadastro, DataAlteracao) 
                                  VALUES (@Estado, @UF, @PaisId, @Ativo, @DataCadastro, @DataAlteracao)";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Estado", estado.NomeEstado);
                        cmd.Parameters.AddWithValue("@UF", estado.UF);
                        cmd.Parameters.AddWithValue("@PaisId", estado.PaisId);
                        cmd.Parameters.AddWithValue("@Ativo", estado.Ativo);
                        cmd.Parameters.AddWithValue("@DataAlteracao", estado.DataAlteracao);

                        if (estado.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@Id", estado.Id);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@DataCadastro", estado.DataCadastro);
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
                string query = "DELETE FROM Estados WHERE Id = @Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Estado> ListarEstado()
        {
            List<Estado> lista = new List<Estado>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT 
                                     e.Id, e.Estado AS NomeEstado, e.UF, e.PaisId, e.Ativo, e.DataCadastro, e.DataAlteracao,
                                     p.Id AS PaisObjId, p.Pais AS PaisObjNome 
                                 FROM Estados e
                                 JOIN Paises p ON e.PaisId = p.Id
                                 ORDER BY e.Id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var pais = new Pais
                            {
                                Id = reader.GetInt32("PaisObjId"),
                                NomePais = reader.GetString("PaisObjNome")
                            };

                            var estado = new Estado
                            {
                                Id = reader.GetInt32("Id"),
                                NomeEstado = reader.GetString("NomeEstado"),
                                UF = reader.GetString("UF"),
                                PaisId = reader.GetInt32("PaisId"),
                                Ativo = reader.GetBoolean("Ativo"),
                                DataCadastro = reader.IsDBNull(reader.GetOrdinal("DataCadastro")) ? (DateTime?)null : reader.GetDateTime("DataCadastro"),
                                DataAlteracao = reader.IsDBNull(reader.GetOrdinal("DataAlteracao")) ? (DateTime?)null : reader.GetDateTime("DataAlteracao"),
                                oPais = pais
                            };
                            lista.Add(estado);
                        }
                    }
                }
            }
            return lista;
        }

        public Estado BuscarPorId(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT 
                                     e.Id, e.Estado AS NomeEstado, e.UF, e.PaisId, e.Ativo, e.DataCadastro, e.DataAlteracao,
                                     p.Id AS PaisObjId, p.Pais AS PaisObjNome 
                                 FROM Estados e
                                 JOIN Paises p ON e.PaisId = p.Id
                                 WHERE e.Id = @Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var pais = new Pais
                            {
                                Id = reader.GetInt32("PaisObjId"),
                                NomePais = reader.GetString("PaisObjNome")
                            };

                            return new Estado
                            {
                                Id = reader.GetInt32("Id"),
                                NomeEstado = reader.GetString("NomeEstado"),
                                UF = reader.GetString("UF"),
                                PaisId = reader.GetInt32("PaisId"),
                                Ativo = reader.GetBoolean("Ativo"),
                                DataCadastro = reader.IsDBNull(reader.GetOrdinal("DataCadastro")) ? (DateTime?)null : reader.GetDateTime("DataCadastro"),
                                DataAlteracao = reader.IsDBNull(reader.GetOrdinal("DataAlteracao")) ? (DateTime?)null : reader.GetDateTime("DataAlteracao"),
                                oPais = pais
                            };
                        }
                    }
                }
            }
            return null;
        }
    }
}