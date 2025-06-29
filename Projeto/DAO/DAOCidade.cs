using MySql.Data.MySqlClient;
using Projeto.Models;
using System;
using System.Collections.Generic;

namespace Projeto.DAO
{
    internal class DAOCidade
    {
        private string connectionString = "Server=localhost;Database=sistema;Uid=root;Pwd=12345678;";

        public void Salvar(Cidade cidade)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query;

                    if (cidade.Id > 0)
                    {
                        query = @"UPDATE cidades 
                                  SET cidade = @cidade, estado_id = @estado_id, ativo = @ativo, 
                                      data_alteracao = @data_alteracao 
                                  WHERE id = @id";
                    }
                    else
                    {
                        query = @"INSERT INTO cidades 
                                  (cidade, estado_id, ativo, data_cadastro, data_alteracao) 
                                  VALUES (@cidade, @estado_id, @ativo, @data_cadastro, @data_alteracao)";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@cidade", cidade.NomeCidade);
                        cmd.Parameters.AddWithValue("@estado_id", cidade.EstadoId);
                        cmd.Parameters.AddWithValue("@ativo", cidade.Ativo);
                        cmd.Parameters.AddWithValue("@data_alteracao", cidade.DataAlteracao);

                        if (cidade.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@id", cidade.Id);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@data_cadastro", cidade.DataCadastro);
                        }

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar cidade: " + ex.Message);
            }
        }

        public void Excluir(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM cidades WHERE id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Cidade BuscarPorId(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT c.id, c.cidade, c.estado_id, c.ativo, 
                           c.data_cadastro, c.data_alteracao, 
                           e.estado AS estado_nome 
                    FROM cidades c
                    JOIN estados e ON c.estado_id = e.id
                    WHERE c.id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Cidade
                            {
                                Id = reader.GetInt32("id"),
                                NomeCidade = reader.GetString("cidade"),
                                EstadoId = reader.GetInt32("estado_id"),
                                EstadoNome = reader.GetString("estado_nome"),
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

        public List<Cidade> ListarCidade()
        {
            List<Cidade> lista = new List<Cidade>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT c.id, c.cidade, c.estado_id, c.ativo, e.estado AS estado_nome 
                    FROM cidades c
                    JOIN estados e ON c.estado_id = e.id
                    ORDER BY c.cidade";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Cidade
                            {
                                Id = reader.GetInt32("id"),
                                NomeCidade = reader.GetString("cidade"),
                                EstadoId = reader.GetInt32("estado_id"),
                                EstadoNome = reader.GetString("estado_nome"),
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
