using MySql.Data.MySqlClient;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                          SET nome = @nome, id_estado = @id_estado, status = @status, 
                              data_modificacao = @data_modificacao 
                          WHERE id = @id";
                    }
                    else
                    {
                        query = @"INSERT INTO cidades 
                          (nome, id_estado, status, data_criacao, data_modificacao) 
                          VALUES (@nome, @id_estado, @status, @data_criacao, @data_modificacao)";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nome", cidade.Nome);
                        cmd.Parameters.AddWithValue("@id_estado", cidade.IdEstado);
                        cmd.Parameters.AddWithValue("@status", cidade.Status);
                        cmd.Parameters.AddWithValue("@data_modificacao", cidade.DataModificacao);

                        if (cidade.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@id", cidade.Id);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@data_criacao", cidade.DataCriacao);
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
                                SELECT c.id, c.nome AS cidade_nome, c.id_estado, c.status, 
                                       c.data_criacao, c.data_modificacao, 
                                       e.nome AS estado_nome 
                                FROM cidades c
                                JOIN estados e ON c.id_estado = e.id
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
                                Nome = reader.GetString("cidade_nome"),
                                IdEstado = reader.GetInt32("id_estado"),
                                EstadoNome = reader.GetString("estado_nome"),
                                Status = reader.GetBoolean("status"),
                                DataCriacao = reader.IsDBNull(reader.GetOrdinal("data_criacao")) ? (DateTime?)null : reader.GetDateTime("data_criacao"),
                                DataModificacao = reader.IsDBNull(reader.GetOrdinal("data_modificacao")) ? (DateTime?)null : reader.GetDateTime("data_modificacao")
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
            SELECT c.id, c.nome AS cidade_nome, c.id_estado, c.status, e.nome AS estado_nome 
            FROM cidades c
            JOIN estados e ON c.id_estado = e.id
            ORDER BY c.nome";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Cidade
                            {
                                Id = reader.GetInt32("id"),
                                Nome = reader.GetString("cidade_nome"),
                                IdEstado = reader.GetInt32("id_estado"),
                                EstadoNome = reader.GetString("estado_nome"),
                                Status = reader.GetBoolean("status"),
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}
