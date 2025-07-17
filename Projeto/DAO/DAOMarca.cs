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
                        UPDATE Marcas SET 
                            Marca = @Marca, 
                            Ativo = @Ativo,
                            DataAlteracao = @DataAlteracao
                        WHERE Id = @Id";
                    }
                    else
                    {
                        query = @"
                        INSERT INTO Marcas (
                            Marca, Ativo, DataCadastro, DataAlteracao
                        ) VALUES (
                            @Marca, @Ativo, @DataCadastro, @DataAlteracao
                        )";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Marca", marca.NomeMarca);
                        cmd.Parameters.AddWithValue("@Ativo", marca.Ativo);
                        cmd.Parameters.AddWithValue("@DataAlteracao", marca.DataAlteracao ?? DateTime.Now);

                        if (marca.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@Id", marca.Id);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@DataCadastro", marca.DataCadastro ?? DateTime.Now);
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
                string query = "DELETE FROM Marcas WHERE Id = @Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
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
                    SELECT Id, Marca, Ativo, DataCadastro, DataAlteracao
                    FROM Marcas 
                    WHERE Id = @Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Marca
                            {
                                Id = reader.GetInt32("Id"),
                                NomeMarca = reader.GetString("Marca"),
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

        public List<Marca> ListarMarcas()
        {
            List<Marca> lista = new List<Marca>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT Id, Marca, Ativo, DataCadastro, DataAlteracao
                    FROM Marcas 
                    ORDER BY Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Marca
                            {
                                Id = reader.GetInt32("Id"),
                                NomeMarca = reader.GetString("Marca"),
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