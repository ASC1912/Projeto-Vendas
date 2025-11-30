using MySql.Data.MySqlClient;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Projeto.DAO
{
    internal class DAOVeiculo
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString;

        public void Salvar(Veiculo veiculo)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query;

                    if (veiculo.Id > 0)
                    {
                        query = @"
                        UPDATE Veiculos SET 
                            TransportadoraId = @TransportadoraId, IdMarca = @IdMarca, Placa = @Placa, 
                            Modelo = @Modelo, AnoFabricacao = @AnoFabricacao, 
                            CapacidadeCargaKg = @CapacidadeCargaKg, Ativo = @Ativo,
                            DataAlteracao = @DataAlteracao
                        WHERE Id = @Id";
                    }
                    else
                    {
                        query = @"
                        INSERT INTO Veiculos (
                            TransportadoraId, IdMarca, Placa, Modelo, AnoFabricacao, 
                            CapacidadeCargaKg, Ativo, DataCadastro, DataAlteracao
                        ) VALUES (
                            @TransportadoraId, @IdMarca, @Placa, @Modelo, @AnoFabricacao, 
                            @CapacidadeCargaKg, @Ativo, @DataCadastro, @DataAlteracao
                        )";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TransportadoraId", veiculo.TransportadoraId);
                        cmd.Parameters.AddWithValue("@IdMarca", veiculo.IdMarca ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Placa", veiculo.Placa);
                        cmd.Parameters.AddWithValue("@Modelo", veiculo.Modelo);
                        cmd.Parameters.AddWithValue("@AnoFabricacao", veiculo.AnoFabricacao);
                        cmd.Parameters.AddWithValue("@CapacidadeCargaKg", veiculo.CapacidadeCargaKg);
                        cmd.Parameters.AddWithValue("@Ativo", veiculo.Ativo);
                        cmd.Parameters.AddWithValue("@DataAlteracao", veiculo.DataAlteracao ?? DateTime.Now);

                        if (veiculo.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@Id", veiculo.Id);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@DataCadastro", veiculo.DataCadastro ?? DateTime.Now);
                        }

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar veículo: " + ex.Message);
            }
        }

        public void Excluir(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Veiculos WHERE Id = @Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Veiculo BuscarPorId(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT v.Id, v.Placa, v.Modelo, v.AnoFabricacao, v.CapacidadeCargaKg, v.Ativo, 
                           v.TransportadoraId, t.Transportadora AS NomeTransportadora,
                           v.IdMarca, m.Marca AS NomeMarca, v.DataCadastro, v.DataAlteracao
                    FROM Veiculos v
                    JOIN Transportadoras t ON v.TransportadoraId = t.Id
                    LEFT JOIN Marcas m ON v.IdMarca = m.Id
                    WHERE v.Id = @Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Veiculo
                            {
                                Id = reader.GetInt32("Id"),
                                Placa = reader.GetString("Placa"),
                                Modelo = reader.IsDBNull(reader.GetOrdinal("Modelo")) ? null : reader.GetString("Modelo"),
                                AnoFabricacao = reader.IsDBNull(reader.GetOrdinal("AnoFabricacao")) ? (int?)null : reader.GetInt32("AnoFabricacao"),
                                CapacidadeCargaKg = reader.IsDBNull(reader.GetOrdinal("CapacidadeCargaKg")) ? (decimal?)null : reader.GetDecimal("CapacidadeCargaKg"),
                                Ativo = reader.GetBoolean("Ativo"),
                                TransportadoraId = reader.GetInt32("TransportadoraId"),
                                NomeTransportadora = reader.GetString("NomeTransportadora"),
                                IdMarca = reader.IsDBNull(reader.GetOrdinal("IdMarca")) ? (int?)null : reader.GetInt32("IdMarca"),
                                NomeMarca = reader.IsDBNull(reader.GetOrdinal("NomeMarca")) ? null : reader.GetString("NomeMarca"),
                                DataCadastro = reader.IsDBNull(reader.GetOrdinal("DataCadastro")) ? (DateTime?)null : reader.GetDateTime("DataCadastro"),
                                DataAlteracao = reader.IsDBNull(reader.GetOrdinal("DataAlteracao")) ? (DateTime?)null : reader.GetDateTime("DataAlteracao")
                            };
                        }
                    }
                }
            }
            return null;
        }

        public List<Veiculo> ListarVeiculos()
        {
            List<Veiculo> lista = new List<Veiculo>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT v.Id, v.Placa, v.Modelo, v.AnoFabricacao, v.CapacidadeCargaKg, v.Ativo, 
                           v.TransportadoraId, t.Transportadora AS NomeTransportadora,
                           v.IdMarca, m.Marca AS NomeMarca
                    FROM Veiculos v
                    JOIN Transportadoras t ON v.TransportadoraId = t.Id
                    LEFT JOIN Marcas m ON v.IdMarca = m.Id
                    ORDER BY v.Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Veiculo
                            {
                                Id = reader.GetInt32("Id"),
                                Placa = reader.GetString("Placa"),
                                Modelo = reader.IsDBNull(reader.GetOrdinal("Modelo")) ? null : reader.GetString("Modelo"),
                                AnoFabricacao = reader.IsDBNull(reader.GetOrdinal("AnoFabricacao")) ? (int?)null : reader.GetInt32("AnoFabricacao"),
                                CapacidadeCargaKg = reader.IsDBNull(reader.GetOrdinal("CapacidadeCargaKg")) ? (decimal?)null : reader.GetDecimal("CapacidadeCargaKg"),
                                Ativo = reader.GetBoolean("Ativo"),
                                TransportadoraId = reader.GetInt32("TransportadoraId"),
                                NomeTransportadora = reader.GetString("NomeTransportadora"),
                                IdMarca = reader.IsDBNull(reader.GetOrdinal("IdMarca")) ? (int?)null : reader.GetInt32("IdMarca"),
                                NomeMarca = reader.IsDBNull(reader.GetOrdinal("NomeMarca")) ? null : reader.GetString("NomeMarca")
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}