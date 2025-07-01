using MySql.Data.MySqlClient;
using Projeto.Models;
using System;
using System.Collections.Generic;

namespace Projeto.DAO
{
    internal class DAOVeiculo
    {
        private string connectionString = "Server=localhost;Database=sistema;Uid=root;Pwd=12345678;";

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
                        UPDATE veiculos SET 
                            transportadora_id = @transportadora_id, id_marca = @id_marca, placa = @placa, 
                            modelo = @modelo, ano_fabricacao = @ano_fabricacao, 
                            capacidade_carga_kg = @capacidade_carga_kg, ativo = @ativo,
                            data_alteracao = @data_alteracao
                        WHERE id = @id";
                    }
                    else
                    {
                        query = @"
                        INSERT INTO veiculos (
                            transportadora_id, id_marca, placa, modelo, ano_fabricacao, 
                            capacidade_carga_kg, ativo, data_cadastro, data_alteracao
                        ) VALUES (
                            @transportadora_id, @id_marca, @placa, @modelo, @ano_fabricacao, 
                            @capacidade_carga_kg, @ativo, @data_cadastro, @data_alteracao
                        )";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@transportadora_id", veiculo.TransportadoraId);
                        cmd.Parameters.AddWithValue("@id_marca", veiculo.IdMarca); // ALTERADO
                        cmd.Parameters.AddWithValue("@placa", veiculo.Placa);
                        cmd.Parameters.AddWithValue("@modelo", veiculo.Modelo);
                        cmd.Parameters.AddWithValue("@ano_fabricacao", veiculo.AnoFabricacao);
                        cmd.Parameters.AddWithValue("@capacidade_carga_kg", veiculo.CapacidadeCargaKg);
                        cmd.Parameters.AddWithValue("@ativo", veiculo.Ativo);
                        cmd.Parameters.AddWithValue("@data_alteracao", veiculo.DataAlteracao ?? DateTime.Now);

                        if (veiculo.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@id", veiculo.Id);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@data_cadastro", veiculo.DataCadastro ?? DateTime.Now);
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

        // ... (o método Excluir(int id) continua o mesmo)
        public void Excluir(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM veiculos WHERE id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public List<Veiculo> ListarVeiculos()
        {
            List<Veiculo> lista = new List<Veiculo>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT v.id, v.placa, v.modelo, v.ano_fabricacao, v.capacidade_carga_kg, v.ativo, 
                           v.transportadora_id, t.transportadora AS nome_transportadora,
                           v.id_marca, m.marca AS nome_marca
                    FROM veiculos v
                    JOIN transportadoras t ON v.transportadora_id = t.id
                    LEFT JOIN marcas m ON v.id_marca = m.id
                    ORDER BY v.id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Veiculo
                            {
                                Id = reader.GetInt32("id"),
                                Placa = reader.GetString("placa"),
                                Modelo = reader.IsDBNull(reader.GetOrdinal("modelo")) ? null : reader.GetString("modelo"),
                                AnoFabricacao = reader.IsDBNull(reader.GetOrdinal("ano_fabricacao")) ? (int?)null : reader.GetInt32("ano_fabricacao"),
                                CapacidadeCargaKg = reader.IsDBNull(reader.GetOrdinal("capacidade_carga_kg")) ? (decimal?)null : reader.GetDecimal("capacidade_carga_kg"),
                                Ativo = reader.GetBoolean("ativo"),
                                TransportadoraId = reader.GetInt32("transportadora_id"),
                                NomeTransportadora = reader.GetString("nome_transportadora"),
                                IdMarca = reader.IsDBNull(reader.GetOrdinal("id_marca")) ? (int?)null : reader.GetInt32("id_marca"),
                                NomeMarca = reader.IsDBNull(reader.GetOrdinal("nome_marca")) ? null : reader.GetString("nome_marca")
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public Veiculo BuscarPorId(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT v.id, v.placa, v.modelo, v.ano_fabricacao, v.capacidade_carga_kg, v.ativo, 
                           v.transportadora_id, t.transportadora AS nome_transportadora,
                           v.id_marca, m.marca AS nome_marca, v.data_cadastro, v.data_alteracao
                    FROM veiculos v
                    JOIN transportadoras t ON v.transportadora_id = t.id
                    LEFT JOIN marcas m ON v.id_marca = m.id
                    WHERE v.id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Veiculo
                            {
                                Id = reader.GetInt32("id"),
                                Placa = reader.GetString("placa"),
                                Modelo = reader.IsDBNull(reader.GetOrdinal("modelo")) ? null : reader.GetString("modelo"),
                                AnoFabricacao = reader.IsDBNull(reader.GetOrdinal("ano_fabricacao")) ? (int?)null : reader.GetInt32("ano_fabricacao"),
                                CapacidadeCargaKg = reader.IsDBNull(reader.GetOrdinal("capacidade_carga_kg")) ? (decimal?)null : reader.GetDecimal("capacidade_carga_kg"),
                                Ativo = reader.GetBoolean("ativo"),
                                TransportadoraId = reader.GetInt32("transportadora_id"),
                                NomeTransportadora = reader.GetString("nome_transportadora"),
                                IdMarca = reader.IsDBNull(reader.GetOrdinal("id_marca")) ? (int?)null : reader.GetInt32("id_marca"),
                                NomeMarca = reader.IsDBNull(reader.GetOrdinal("nome_marca")) ? null : reader.GetString("nome_marca"),
                                DataCadastro = reader.IsDBNull(reader.GetOrdinal("data_cadastro")) ? (DateTime?)null : reader.GetDateTime("data_cadastro"),
                                DataAlteracao = reader.IsDBNull(reader.GetOrdinal("data_alteracao")) ? (DateTime?)null : reader.GetDateTime("data_alteracao")
                            };
                        }
                    }
                }
            }
            return null;
        }
    }
}