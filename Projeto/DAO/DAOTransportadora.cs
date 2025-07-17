using MySql.Data.MySqlClient;
using Projeto.Models;
using System;
using System.Collections.Generic;

namespace Projeto.DAO
{
    internal class DAOTransportadora
    {
        private string connectionString = "Server=localhost;Database=sistema;Uid=root;Pwd=12345678;";

        public void Salvar(Transportadora transportadora)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query;

                    if (transportadora.Id > 0)
                    {
                        query = @"
                        UPDATE Transportadoras 
                        SET Transportadora = @Transportadora, CpfCnpj = @CpfCnpj, Telefone = @Telefone, Email = @Email, 
                            Endereco = @Endereco, NumeroEndereco = @NumeroEndereco, Complemento = @Complemento, 
                            Bairro = @Bairro, Cep = @Cep, InscricaoEstadual = @InscricaoEstadual, 
                            InscricaoEstadualSubtrib = @InscricaoEstadualSubtrib, IdCidade = @IdCidade, 
                            Tipo = @Tipo, IdCondicaoPagamento = @IdCondicaoPagamento, Ativo = @Ativo,
                            DataAlteracao = @DataAlteracao
                        WHERE Id = @Id";
                    }
                    else
                    {
                        query = @"
                        INSERT INTO Transportadoras (
                            Transportadora, CpfCnpj, Telefone, Email, Endereco, NumeroEndereco, Complemento, 
                            Bairro, Cep, InscricaoEstadual, InscricaoEstadualSubtrib, IdCidade, Tipo, IdCondicaoPagamento,
                            Ativo, DataCadastro, DataAlteracao
                        ) 
                        VALUES (
                            @Transportadora, @CpfCnpj, @Telefone, @Email, @Endereco, @NumeroEndereco, @Complemento, 
                            @Bairro, @Cep, @InscricaoEstadual, @InscricaoEstadualSubtrib, @IdCidade, @Tipo, @IdCondicaoPagamento,
                            @Ativo, @DataCadastro, @DataAlteracao
                        )";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Transportadora", transportadora.Nome);
                        cmd.Parameters.AddWithValue("@CpfCnpj", transportadora.CPF_CNPJ);
                        cmd.Parameters.AddWithValue("@Telefone", transportadora.Telefone ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Email", transportadora.Email ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Endereco", transportadora.Endereco ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@NumeroEndereco", transportadora.NumeroEndereco ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Complemento", transportadora.Complemento ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Bairro", transportadora.Bairro ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Cep", transportadora.CEP ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@InscricaoEstadual", transportadora.InscricaoEstadual ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@InscricaoEstadualSubtrib", transportadora.InscricaoEstadualSubTrib ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@IdCidade", transportadora.IdCidade ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Tipo", transportadora.Tipo ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@IdCondicaoPagamento", transportadora.IdCondicao ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Ativo", transportadora.Ativo);
                        cmd.Parameters.AddWithValue("@DataAlteracao", transportadora.DataAlteracao ?? DateTime.Now);

                        if (transportadora.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@Id", transportadora.Id);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@DataCadastro", transportadora.DataCadastro ?? DateTime.Now);
                        }

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar transportadora: " + ex.Message);
            }
        }

        public void Excluir(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Transportadoras WHERE Id = @Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Transportadora BuscarPorId(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT t.Id, t.Transportadora, t.CpfCnpj, t.Telefone, t.Email, t.Endereco, t.NumeroEndereco, 
                           t.Complemento, t.Bairro, t.Cep, t.InscricaoEstadual, t.InscricaoEstadualSubtrib, 
                           t.IdCidade, ci.Cidade AS NomeCidade, t.Tipo, t.IdCondicaoPagamento,
                           cp.Descricao AS DescricaoCondicao, t.Ativo, t.DataCadastro, t.DataAlteracao
                    FROM Transportadoras t
                    LEFT JOIN Cidades ci ON t.IdCidade = ci.Id
                    LEFT JOIN CondicoesPagamento cp ON t.IdCondicaoPagamento = cp.Id
                    WHERE t.Id = @Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Transportadora
                            {
                                Id = reader.GetInt32("Id"),
                                Nome = reader.GetString("Transportadora"),
                                CPF_CNPJ = reader.GetString("CpfCnpj"),
                                Telefone = reader.IsDBNull(reader.GetOrdinal("Telefone")) ? null : reader.GetString("Telefone"),
                                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString("Email"),
                                Endereco = reader.IsDBNull(reader.GetOrdinal("Endereco")) ? null : reader.GetString("Endereco"),
                                NumeroEndereco = reader.IsDBNull(reader.GetOrdinal("NumeroEndereco")) ? (int?)null : reader.GetInt32("NumeroEndereco"),
                                Complemento = reader.IsDBNull(reader.GetOrdinal("Complemento")) ? null : reader.GetString("Complemento"),
                                Bairro = reader.IsDBNull(reader.GetOrdinal("Bairro")) ? null : reader.GetString("Bairro"),
                                CEP = reader.IsDBNull(reader.GetOrdinal("Cep")) ? null : reader.GetString("Cep"),
                                InscricaoEstadual = reader.IsDBNull(reader.GetOrdinal("InscricaoEstadual")) ? null : reader.GetString("InscricaoEstadual"),
                                InscricaoEstadualSubTrib = reader.IsDBNull(reader.GetOrdinal("InscricaoEstadualSubtrib")) ? null : reader.GetString("InscricaoEstadualSubtrib"),
                                IdCidade = reader.IsDBNull(reader.GetOrdinal("IdCidade")) ? (int?)null : reader.GetInt32("IdCidade"),
                                NomeCidade = reader.IsDBNull(reader.GetOrdinal("NomeCidade")) ? null : reader.GetString("NomeCidade"),
                                Tipo = reader.GetString("Tipo"),
                                IdCondicao = reader.IsDBNull(reader.GetOrdinal("IdCondicaoPagamento")) ? (int?)null : reader.GetInt32("IdCondicaoPagamento"),
                                DescricaoCondicao = reader.IsDBNull(reader.GetOrdinal("DescricaoCondicao")) ? null : reader.GetString("DescricaoCondicao"),
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

        public List<Transportadora> ListarTransportadoras()
        {
            List<Transportadora> lista = new List<Transportadora>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT t.Id, t.Transportadora, t.CpfCnpj, t.Telefone, t.Email, t.Endereco, t.NumeroEndereco, 
                           t.Complemento, t.Bairro, t.Cep, t.InscricaoEstadual, t.InscricaoEstadualSubtrib, 
                           t.IdCidade, ci.Cidade AS NomeCidade, t.Tipo, t.IdCondicaoPagamento,
                           cp.Descricao AS DescricaoCondicao, t.Ativo, t.DataCadastro, t.DataAlteracao
                    FROM Transportadoras t
                    LEFT JOIN Cidades ci ON t.IdCidade = ci.Id
                    LEFT JOIN CondicoesPagamento cp ON t.IdCondicaoPagamento = cp.Id
                    ORDER BY t.Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Transportadora
                            {
                                Id = reader.GetInt32("Id"),
                                Nome = reader.GetString("Transportadora"),
                                CPF_CNPJ = reader.GetString("CpfCnpj"),
                                Telefone = reader.IsDBNull(reader.GetOrdinal("Telefone")) ? null : reader.GetString("Telefone"),
                                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString("Email"),
                                Endereco = reader.IsDBNull(reader.GetOrdinal("Endereco")) ? null : reader.GetString("Endereco"),
                                NumeroEndereco = reader.IsDBNull(reader.GetOrdinal("NumeroEndereco")) ? (int?)null : reader.GetInt32("NumeroEndereco"),
                                Complemento = reader.IsDBNull(reader.GetOrdinal("Complemento")) ? null : reader.GetString("Complemento"),
                                Bairro = reader.IsDBNull(reader.GetOrdinal("Bairro")) ? null : reader.GetString("Bairro"),
                                CEP = reader.IsDBNull(reader.GetOrdinal("Cep")) ? null : reader.GetString("Cep"),
                                InscricaoEstadual = reader.IsDBNull(reader.GetOrdinal("InscricaoEstadual")) ? null : reader.GetString("InscricaoEstadual"),
                                InscricaoEstadualSubTrib = reader.IsDBNull(reader.GetOrdinal("InscricaoEstadualSubtrib")) ? null : reader.GetString("InscricaoEstadualSubtrib"),
                                IdCidade = reader.IsDBNull(reader.GetOrdinal("IdCidade")) ? (int?)null : reader.GetInt32("IdCidade"),
                                NomeCidade = reader.IsDBNull(reader.GetOrdinal("NomeCidade")) ? null : reader.GetString("NomeCidade"),
                                Tipo = reader.GetString("Tipo"),
                                IdCondicao = reader.IsDBNull(reader.GetOrdinal("IdCondicaoPagamento")) ? (int?)null : reader.GetInt32("IdCondicaoPagamento"),
                                DescricaoCondicao = reader.IsDBNull(reader.GetOrdinal("DescricaoCondicao")) ? null : reader.GetString("DescricaoCondicao"),
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