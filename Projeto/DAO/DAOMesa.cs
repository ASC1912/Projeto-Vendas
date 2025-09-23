using MySql.Data.MySqlClient;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Projeto.DAO
{
    internal class DAOMesa
    {
        private string connectionString = "Server=localhost;Database=sistema;Uid=root;Pwd=12345678;";

        public void Salvar(Mesa mesa)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query;
                    bool existe = false;

                    // Passo 1: Verifica se a mesa já existe no banco de dados
                    string checkQuery = "SELECT COUNT(*) FROM Mesas WHERE NumeroMesa = @NumeroMesa";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@NumeroMesa", mesa.NumeroMesa);
                        // ExecuteScalar retorna a primeira coluna da primeira linha do resultado.
                        existe = Convert.ToInt32(checkCmd.ExecuteScalar()) > 0;
                    }

                    // Passo 2: Decide se vai fazer UPDATE ou INSERT
                    if (existe)
                    {
                        // Se existe, monta o comando de UPDATE
                        query = @"UPDATE Mesas SET 
                              QuantidadeCadeiras = @QuantidadeCadeiras, 
                              Localizacao = @Localizacao,
                              StatusMesa = @StatusMesa,
                              Ativo = @Ativo, 
                              DataAlteracao = @DataAlteracao 
                          WHERE NumeroMesa = @NumeroMesa";
                    }
                    else
                    {
                        // Se não existe, monta o comando de INSERT
                        query = @"INSERT INTO Mesas 
                              (NumeroMesa, QuantidadeCadeiras, Localizacao, StatusMesa, Ativo, DataCadastro, DataAlteracao) 
                              VALUES (@NumeroMesa, @QuantidadeCadeiras, @Localizacao, @StatusMesa, @Ativo, @DataCadastro, @DataAlteracao)";
                    }

                    // Passo 3: Executa o comando escolhido
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Adiciona todos os parâmetros que podem ser usados por qualquer uma das queries
                        cmd.Parameters.AddWithValue("@NumeroMesa", mesa.NumeroMesa);
                        cmd.Parameters.AddWithValue("@QuantidadeCadeiras", mesa.QuantidadeCadeiras);
                        cmd.Parameters.AddWithValue("@Localizacao", mesa.Localizacao);
                        cmd.Parameters.AddWithValue("@StatusMesa", mesa.StatusMesa.ToString()); // Convertendo para string para garantir compatibilidade
                        cmd.Parameters.AddWithValue("@Ativo", mesa.Ativo);
                        cmd.Parameters.AddWithValue("@DataAlteracao", DateTime.Now);

                        // Adiciona o DataCadastro apenas se for um INSERT
                        if (!existe)
                        {
                            cmd.Parameters.AddWithValue("@DataCadastro", DateTime.Now);
                        }

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar a mesa: " + ex.Message);
            }
        }
        public void Excluir(int numeroMesa)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Mesas WHERE NumeroMesa = @NumeroMesa";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NumeroMesa", numeroMesa);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Mesa BuscarPorNumero(int numeroMesa)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Mesas WHERE NumeroMesa = @NumeroMesa";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NumeroMesa", numeroMesa);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Mesa
                            {
                                NumeroMesa = reader.GetInt32("NumeroMesa"),
                                QuantidadeCadeiras = reader.GetInt32("QuantidadeCadeiras"),
                                Localizacao = reader.GetString("Localizacao"),
                                StatusMesa = reader.GetString("StatusMesa")[0],
                                Ativo = reader.GetBoolean("Ativo"),
                                DataCadastro = reader.GetDateTime("DataCadastro"),
                                DataAlteracao = reader.IsDBNull(reader.GetOrdinal("DataAlteracao")) ? (DateTime?)null : reader.GetDateTime("DataAlteracao")
                            };
                        }
                    }
                }
            }
            return null;
        }

        public List<Mesa> ListarMesas()
        {
            List<Mesa> lista = new List<Mesa>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Mesas ORDER BY NumeroMesa";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Mesa
                            {
                                NumeroMesa = reader.GetInt32("NumeroMesa"),
                                QuantidadeCadeiras = reader.GetInt32("QuantidadeCadeiras"),
                                Localizacao = reader.GetString("Localizacao"),
                                StatusMesa = reader.GetString("StatusMesa")[0],
                                Ativo = reader.GetBoolean("Ativo"),
                                DataCadastro = reader.GetDateTime("DataCadastro"),
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