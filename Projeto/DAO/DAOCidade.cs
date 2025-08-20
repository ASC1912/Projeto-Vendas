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
                        query = @"UPDATE Cidades 
                                  SET Cidade = @Cidade, EstadoId = @EstadoId, Ativo = @Ativo, 
                                      DataAlteracao = @DataAlteracao 
                                  WHERE Id = @Id";
                    }
                    else
                    {
                        query = @"INSERT INTO Cidades 
                                  (Cidade, EstadoId, Ativo, DataCadastro, DataAlteracao) 
                                  VALUES (@Cidade, @EstadoId, @Ativo, @DataCadastro, @DataAlteracao)";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Cidade", cidade.NomeCidade);
                        cmd.Parameters.AddWithValue("@EstadoId", cidade.EstadoId);
                        cmd.Parameters.AddWithValue("@Ativo", cidade.Ativo);
                        cmd.Parameters.AddWithValue("@DataAlteracao", cidade.DataAlteracao);

                        if (cidade.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@Id", cidade.Id);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@DataCadastro", cidade.DataCadastro);
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
                string query = "DELETE FROM Cidades WHERE Id = @Id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
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
                    SELECT c.Id, c.Cidade, c.EstadoId, c.Ativo, c.DataCadastro, c.DataAlteracao,
                           e.Id AS EstadoObjId, e.Estado AS EstadoObjNome, e.UF,
                           p.Id AS PaisObjId, p.Pais AS PaisObjNome
                    FROM Cidades c
                    JOIN Estados e ON c.EstadoId = e.Id
                    JOIN Paises p ON e.PaisId = p.Id
                    WHERE c.Id = @Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MontarObjetoCidade(reader);
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
                    SELECT c.Id, c.Cidade, c.EstadoId, c.Ativo, c.DataCadastro, c.DataAlteracao,
                           e.Id AS EstadoObjId, e.Estado AS EstadoObjNome, e.UF,
                           p.Id AS PaisObjId, p.Pais AS PaisObjNome
                    FROM Cidades c
                    JOIN Estados e ON c.EstadoId = e.Id
                    JOIN Paises p ON e.PaisId = p.Id
                    ORDER BY c.Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(MontarObjetoCidade(reader));
                        }
                    }
                }
            }
            return lista;
        }

        // Método auxiliar para não repetir código
        private Cidade MontarObjetoCidade(MySqlDataReader reader)
        {
            var pais = new Pais
            {
                Id = reader.GetInt32("PaisObjId"),
                NomePais = reader.GetString("PaisObjNome")
            };

            var estado = new Estado
            {
                Id = reader.GetInt32("EstadoObjId"),
                NomeEstado = reader.GetString("EstadoObjNome"),
                UF = reader.GetString("UF"),
                oPais = pais 
            };

            var cidade = new Cidade
            {
                Id = reader.GetInt32("Id"),
                NomeCidade = reader.GetString("Cidade"),
                EstadoId = reader.GetInt32("EstadoId"),
                Ativo = reader.GetBoolean("Ativo"),
                DataCadastro = reader.IsDBNull(reader.GetOrdinal("DataCadastro")) ? (DateTime?)null : reader.GetDateTime("DataCadastro"),
                DataAlteracao = reader.IsDBNull(reader.GetOrdinal("DataAlteracao")) ? (DateTime?)null : reader.GetDateTime("DataAlteracao"),
                oEstado = estado 
            };

            return cidade;
        }
    }
}