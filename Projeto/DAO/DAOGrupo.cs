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
                            grupo = @grupo,
                            descricao = @descricao,
                            ativo = @ativo,
                            data_alteracao = @data_alteracao
                        WHERE id = @id";
                    }
                    else
                    {
                        query = @"
                        INSERT INTO grupos (
                            grupo, descricao, ativo, data_cadastro, data_alteracao
                        ) VALUES (
                            @grupo, @descricao, @ativo, @data_cadastro, @data_alteracao
                        )";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@grupo", grupo.NomeGrupo);
                        cmd.Parameters.AddWithValue("@descricao", string.IsNullOrEmpty(grupo.Descricao) ? (object)DBNull.Value : grupo.Descricao);
                        cmd.Parameters.AddWithValue("@ativo", grupo.Ativo);
                        cmd.Parameters.AddWithValue("@data_alteracao", grupo.DataAlteracao ?? DateTime.Now);

                        if (grupo.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@id", grupo.Id);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@data_cadastro", grupo.DataCadastro ?? DateTime.Now);
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
                    SELECT id, grupo, descricao, ativo, data_cadastro, data_alteracao
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
                                NomeGrupo = reader.GetString(reader.GetOrdinal("grupo")),
                                Descricao = reader.IsDBNull(reader.GetOrdinal("descricao")) ? null : reader.GetString(reader.GetOrdinal("descricao")),
                                Ativo = reader.GetBoolean(reader.GetOrdinal("ativo")),
                                DataCadastro = reader.IsDBNull(reader.GetOrdinal("data_cadastro")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("data_cadastro")),
                                DataAlteracao = reader.IsDBNull(reader.GetOrdinal("data_alteracao")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("data_alteracao"))
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
                    SELECT id, grupo, descricao, ativo, data_cadastro, data_alteracao
                    FROM grupos 
                    ORDER BY id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Grupo
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                NomeGrupo = reader.GetString(reader.GetOrdinal("grupo")),
                                Descricao = reader.IsDBNull(reader.GetOrdinal("descricao")) ? null : reader.GetString(reader.GetOrdinal("descricao")),
                                Ativo = reader.GetBoolean(reader.GetOrdinal("ativo")),
                                DataCadastro = reader.IsDBNull(reader.GetOrdinal("data_cadastro")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("data_cadastro")),
                                DataAlteracao = reader.IsDBNull(reader.GetOrdinal("data_alteracao")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("data_alteracao"))
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}
