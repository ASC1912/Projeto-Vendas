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
                        UPDATE Grupos SET 
                            Grupo = @Grupo,
                            Descricao = @Descricao,
                            Ativo = @Ativo,
                            DataAlteracao = @DataAlteracao
                        WHERE Id = @Id";
                    }
                    else
                    {
                        query = @"
                        INSERT INTO Grupos (
                            Grupo, Descricao, Ativo, DataCadastro, DataAlteracao
                        ) VALUES (
                            @Grupo, @Descricao, @Ativo, @DataCadastro, @DataAlteracao
                        )";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Grupo", grupo.NomeGrupo);
                        cmd.Parameters.AddWithValue("@Descricao", string.IsNullOrEmpty(grupo.Descricao) ? (object)DBNull.Value : grupo.Descricao);
                        cmd.Parameters.AddWithValue("@Ativo", grupo.Ativo);
                        cmd.Parameters.AddWithValue("@DataAlteracao", grupo.DataAlteracao ?? DateTime.Now);

                        if (grupo.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@Id", grupo.Id);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@DataCadastro", grupo.DataCadastro ?? DateTime.Now);
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
                string query = "DELETE FROM Grupos WHERE Id = @Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
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
                    SELECT Id, Grupo, Descricao, Ativo, DataCadastro, DataAlteracao
                    FROM Grupos 
                    WHERE Id = @Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Grupo
                            {
                                Id = reader.GetInt32("Id"),
                                NomeGrupo = reader.GetString("Grupo"),
                                Descricao = reader.IsDBNull(reader.GetOrdinal("Descricao")) ? null : reader.GetString("Descricao"),
                                Ativo = reader.GetBoolean("Ativo"),
                                DataCadastro = reader.IsDBNull(reader.GetOrdinal("DataCadastro")) ? (DateTime?)null : reader.GetDateTime("DataCadastro"),
                                DataAlteracao = reader.IsDBNull(reader.GetOrdinal("DataAlteracao")) ? (DateTime?)null : reader.GetDateTime("DataAlteracao")
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
                    SELECT Id, Grupo, Descricao, Ativo, DataCadastro, DataAlteracao
                    FROM Grupos 
                    ORDER BY Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Grupo
                            {
                                Id = reader.GetInt32("Id"),
                                NomeGrupo = reader.GetString("Grupo"),
                                Descricao = reader.IsDBNull(reader.GetOrdinal("Descricao")) ? null : reader.GetString("Descricao"),
                                Ativo = reader.GetBoolean("Ativo"),
                                DataCadastro = reader.IsDBNull(reader.GetOrdinal("DataCadastro")) ? (DateTime?)null : reader.GetDateTime("DataCadastro"),
                                DataAlteracao = reader.IsDBNull(reader.GetOrdinal("DataAlteracao")) ? (DateTime?)null : reader.GetDateTime("DataAlteracao")
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}