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
                            marca = @marca, 
                            ativo = @ativo,
                            data_alteracao = @data_alteracao
                        WHERE id = @id";
                    }
                    else
                    {
                        query = @"
                        INSERT INTO marcas (
                            marca, ativo, data_cadastro, data_alteracao
                        ) VALUES (
                            @marca, @ativo, @data_cadastro, @data_alteracao
                        )";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@marca", marca.NomeMarca);
                        cmd.Parameters.AddWithValue("@ativo", marca.Ativo);
                        cmd.Parameters.AddWithValue("@data_alteracao", marca.DataAlteracao ?? DateTime.Now);

                        if (marca.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@id", marca.Id);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@data_cadastro", marca.DataCadastro ?? DateTime.Now);
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
                    SELECT id, marca, ativo, data_cadastro, data_alteracao
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
                                Id = reader.GetInt32("id"),
                                NomeMarca = reader.GetString("marca"),
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

        public List<Marca> ListarMarcas()
        {
            List<Marca> lista = new List<Marca>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT id, marca, ativo, data_cadastro, data_alteracao
                    FROM marcas 
                    ORDER BY id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Marca
                            {
                                Id = reader.GetInt32("id"),
                                NomeMarca = reader.GetString("marca"),
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
