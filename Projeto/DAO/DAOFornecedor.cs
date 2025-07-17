using MySql.Data.MySqlClient;
using Projeto.Models;
using System;
using System.Collections.Generic;

namespace Projeto.DAO
{
    internal class DAOFornecedor
    {
        private string connectionString = "Server=localhost;Database=sistema;Uid=root;Pwd=12345678;";

        public void Salvar(Fornecedor fornecedor)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query;

                    if (fornecedor.Id > 0)
                    {
                        query = @"
                        UPDATE Fornecedores SET 
                            Fornecedor = @Fornecedor, CpfCnpj = @CpfCnpj, Telefone = @Telefone, Email = @Email,
                            Endereco = @Endereco, NumeroEndereco = @NumeroEndereco, Complemento = @Complemento,
                            Bairro = @Bairro, Cep = @Cep, IdCidade = @IdCidade, Tipo = @Tipo,
                            InscricaoEstadual = @InscricaoEstadual, InscricaoEstadualSubtrib = @InscricaoEstadualSubtrib,
                            IdCondicaoPagamento = @IdCondicaoPagamento,
                            Ativo = @Ativo, DataAlteracao = @DataAlteracao
                        WHERE Id = @Id";
                    }
                    else
                    {
                        query = @"
                        INSERT INTO Fornecedores (
                            Fornecedor, CpfCnpj, Telefone, Email, Endereco, NumeroEndereco, Complemento,
                            Bairro, Cep, IdCidade, Tipo, InscricaoEstadual, InscricaoEstadualSubtrib, IdCondicaoPagamento,
                            Ativo, DataCadastro, DataAlteracao
                        ) VALUES (
                            @Fornecedor, @CpfCnpj, @Telefone, @Email, @Endereco, @NumeroEndereco, @Complemento,
                            @Bairro, @Cep, @IdCidade, @Tipo, @InscricaoEstadual, @InscricaoEstadualSubtrib, @IdCondicaoPagamento,
                            @Ativo, @DataCadastro, @DataAlteracao
                        )";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Fornecedor", fornecedor.Nome);
                        cmd.Parameters.AddWithValue("@CpfCnpj", fornecedor.CPF_CNPJ);
                        cmd.Parameters.AddWithValue("@Telefone", fornecedor.Telefone ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Email", fornecedor.Email ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Endereco", fornecedor.Endereco ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@NumeroEndereco", fornecedor.NumeroEndereco ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Complemento", fornecedor.Complemento ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Bairro", fornecedor.Bairro ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Cep", fornecedor.CEP ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@IdCidade", fornecedor.IdCidade ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Tipo", fornecedor.Tipo ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@InscricaoEstadual", fornecedor.InscricaoEstadual ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@InscricaoEstadualSubtrib", fornecedor.InscricaoEstadualSubTrib ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@IdCondicaoPagamento", fornecedor.IdCondicao ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Ativo", fornecedor.Ativo);
                        cmd.Parameters.AddWithValue("@DataAlteracao", fornecedor.DataAlteracao ?? DateTime.Now);

                        if (fornecedor.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@Id", fornecedor.Id);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@DataCadastro", fornecedor.DataCadastro ?? DateTime.Now);
                        }

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar fornecedor: " + ex.Message);
            }
        }

        public void Excluir(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Fornecedores WHERE Id = @Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Fornecedor BuscarPorId(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                SELECT f.Id, f.Fornecedor, f.CpfCnpj, f.Telefone, f.Email, f.Endereco, f.NumeroEndereco,
                       f.Complemento, f.Bairro, f.Cep, f.IdCidade, ci.Cidade AS NomeCidade,
                       f.Tipo, f.InscricaoEstadual, f.InscricaoEstadualSubtrib, f.IdCondicaoPagamento,
                       cp.Descricao AS DescricaoCondicao,
                       f.Ativo, f.DataCadastro, f.DataAlteracao
                FROM Fornecedores f
                LEFT JOIN Cidades ci ON f.IdCidade = ci.Id
                LEFT JOIN CondicoesPagamento cp ON f.IdCondicaoPagamento = cp.Id
                WHERE f.Id = @Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Fornecedor
                            {
                                Id = reader.GetInt32("Id"),
                                Nome = reader.GetString("Fornecedor"),
                                CPF_CNPJ = reader.GetString("CpfCnpj"),
                                Telefone = reader.IsDBNull(reader.GetOrdinal("Telefone")) ? null : reader.GetString("Telefone"),
                                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString("Email"),
                                Endereco = reader.IsDBNull(reader.GetOrdinal("Endereco")) ? null : reader.GetString("Endereco"),
                                NumeroEndereco = reader.IsDBNull(reader.GetOrdinal("NumeroEndereco")) ? (int?)null : reader.GetInt32("NumeroEndereco"),
                                Complemento = reader.IsDBNull(reader.GetOrdinal("Complemento")) ? null : reader.GetString("Complemento"),
                                Bairro = reader.IsDBNull(reader.GetOrdinal("Bairro")) ? null : reader.GetString("Bairro"),
                                CEP = reader.IsDBNull(reader.GetOrdinal("Cep")) ? null : reader.GetString("Cep"),
                                IdCidade = reader.IsDBNull(reader.GetOrdinal("IdCidade")) ? (int?)null : reader.GetInt32("IdCidade"),
                                NomeCidade = reader.IsDBNull(reader.GetOrdinal("NomeCidade")) ? null : reader.GetString("NomeCidade"),
                                Tipo = reader.GetString("Tipo"),
                                InscricaoEstadual = reader.IsDBNull(reader.GetOrdinal("InscricaoEstadual")) ? null : reader.GetString("InscricaoEstadual"),
                                InscricaoEstadualSubTrib = reader.IsDBNull(reader.GetOrdinal("InscricaoEstadualSubtrib")) ? null : reader.GetString("InscricaoEstadualSubtrib"),
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

        public List<Fornecedor> ListarFornecedores()
        {
            List<Fornecedor> lista = new List<Fornecedor>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                SELECT f.Id, f.Fornecedor, f.CpfCnpj, f.Telefone, f.Email, f.Endereco, f.NumeroEndereco,
                       f.Complemento, f.Bairro, f.Cep, f.IdCidade, ci.Cidade AS NomeCidade,
                       f.Tipo, f.InscricaoEstadual, f.InscricaoEstadualSubtrib, f.IdCondicaoPagamento,
                       cp.Descricao AS DescricaoCondicao,
                       f.Ativo, f.DataCadastro, f.DataAlteracao
                FROM Fornecedores f
                LEFT JOIN Cidades ci ON f.IdCidade = ci.Id
                LEFT JOIN CondicoesPagamento cp ON f.IdCondicaoPagamento = cp.Id
                ORDER BY f.Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Fornecedor
                            {
                                Id = reader.GetInt32("Id"),
                                Nome = reader.GetString("Fornecedor"),
                                CPF_CNPJ = reader.GetString("CpfCnpj"),
                                Telefone = reader.IsDBNull(reader.GetOrdinal("Telefone")) ? null : reader.GetString("Telefone"),
                                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString("Email"),
                                Endereco = reader.IsDBNull(reader.GetOrdinal("Endereco")) ? null : reader.GetString("Endereco"),
                                NumeroEndereco = reader.IsDBNull(reader.GetOrdinal("NumeroEndereco")) ? (int?)null : reader.GetInt32("NumeroEndereco"),
                                Complemento = reader.IsDBNull(reader.GetOrdinal("Complemento")) ? null : reader.GetString("Complemento"),
                                Bairro = reader.IsDBNull(reader.GetOrdinal("Bairro")) ? null : reader.GetString("Bairro"),
                                CEP = reader.IsDBNull(reader.GetOrdinal("Cep")) ? null : reader.GetString("Cep"),
                                IdCidade = reader.IsDBNull(reader.GetOrdinal("IdCidade")) ? (int?)null : reader.GetInt32("IdCidade"),
                                NomeCidade = reader.IsDBNull(reader.GetOrdinal("NomeCidade")) ? null : reader.GetString("NomeCidade"),
                                Tipo = reader.GetString("Tipo"),
                                InscricaoEstadual = reader.IsDBNull(reader.GetOrdinal("InscricaoEstadual")) ? null : reader.GetString("InscricaoEstadual"),
                                InscricaoEstadualSubTrib = reader.IsDBNull(reader.GetOrdinal("InscricaoEstadualSubtrib")) ? null : reader.GetString("InscricaoEstadualSubtrib"),
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