using MySql.Data.MySqlClient;
using Projeto.Models;
using System;
using System.Collections.Generic;

namespace Projeto.DAO
{
    internal class DAOGrupo
    {
        private string connectionString = "Server=localhost;Database=sistema;Uid=root;Pwd=12345678;";

        public void Salvar(Grupo grupo)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query;

                    if (grupo.Id > 0)
                    {
                        query = @"
                        UPDATE grupos SET 
                            nome = @nome,
                            descricao = @descricao,
                            status = @status,
                            data_modificacao = @data_modificacao
                        WHERE id = @id";
                    }
                    else
                    {
                        query = @"
                        INSERT INTO grupos (
                            nome, descricao, status, data_criacao, data_modificacao
                        ) VALUES (
                            @nome, @descricao, @status, @data_criacao, @data_modificacao
                        )";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nome", grupo.Nome);
                        cmd.Parameters.AddWithValue("@descricao", string.IsNullOrEmpty(grupo.Descricao) ? (object)DBNull.Value : grupo.Descricao);
                        cmd.Parameters.AddWithValue("@status", grupo.Status);
                        cmd.Parameters.AddWithValue("@data_modificacao", grupo.DataModificacao ?? DateTime.Now);

                        if (grupo.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@id", grupo.Id);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@data_criacao", grupo.DataCriacao ?? DateTime.Now);
                        }

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar grupo: " + ex.Message);
            }
        }

        public void Excluir(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM grupos WHERE id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Grupo BuscarPorId(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT id, nome, descricao, status, data_criacao, data_modificacao
                    FROM grupos 
                    WHERE id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Grupo
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Nome = reader.GetString(reader.GetOrdinal("nome")),
                                Descricao = reader.IsDBNull(reader.GetOrdinal("descricao")) ? null : reader.GetString(reader.GetOrdinal("descricao")),
                                Status = reader.GetBoolean(reader.GetOrdinal("status")),
                                DataCriacao = reader.IsDBNull(reader.GetOrdinal("data_criacao")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("data_criacao")),
                                DataModificacao = reader.IsDBNull(reader.GetOrdinal("data_modificacao")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("data_modificacao"))
                            };
                        }
                    }
                }
            }

            return null;
        }

        public List<Grupo> ListarGrupos()
        {
            List<Grupo> lista = new List<Grupo>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT id, nome, descricao, status, data_criacao, data_modificacao
                    FROM grupos 
                    ORDER BY nome";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Grupo
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Nome = reader.GetString(reader.GetOrdinal("nome")),
                                Descricao = reader.IsDBNull(reader.GetOrdinal("descricao")) ? null : reader.GetString(reader.GetOrdinal("descricao")),
                                Status = reader.GetBoolean(reader.GetOrdinal("status")),
                                DataCriacao = reader.IsDBNull(reader.GetOrdinal("data_criacao")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("data_criacao")),
                                DataModificacao = reader.IsDBNull(reader.GetOrdinal("data_modificacao")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("data_modificacao"))
                            });
                        }
                    }
                }
            }

            return lista;
        }
    }
}
