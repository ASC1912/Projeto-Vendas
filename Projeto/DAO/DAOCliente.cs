using MySql.Data.MySqlClient;
using Projeto.Models;
using System;
using System.Collections.Generic;

namespace Projeto.DAO
{
    internal class DAOCliente
    {
        private string connectionString = "Server=localhost;Database=sistema;Uid=root;Pwd=12345678;";

        public void Salvar(Cliente cliente)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query;

                    if (cliente.Id > 0)
                    {
                        query = @"
                        UPDATE Clientes SET
                            Cliente = @Cliente, CpfCnpj = @CpfCnpj, Rg = @Rg, Telefone = @Telefone, Email = @Email,
                            Endereco = @Endereco, NumeroEndereco = @NumeroEndereco, Complemento = @Complemento,
                            Bairro = @Bairro, Cep = @Cep, CidadeId = @CidadeId, Tipo = @Tipo, Genero = @Genero,
                            IdCondicaoPagamento = @IdCondicaoPagamento, Ativo = @Ativo,
                            DataNascimento = @DataNascimento, DataAlteracao = @DataAlteracao
                        WHERE Id = @Id";
                    }
                    else
                    {
                        query = @"
                        INSERT INTO Clientes (
                            Cliente, CpfCnpj, Rg, Telefone, Email, Endereco, NumeroEndereco, Complemento,
                            Bairro, Cep, CidadeId, Tipo, Genero, IdCondicaoPagamento, Ativo,
                            DataNascimento, DataCadastro, DataAlteracao
                        ) VALUES (
                            @Cliente, @CpfCnpj, @Rg, @Telefone, @Email, @Endereco, @NumeroEndereco, @Complemento,
                            @Bairro, @Cep, @CidadeId, @Tipo, @Genero, @IdCondicaoPagamento, @Ativo,
                            @DataNascimento, @DataCadastro, @DataAlteracao
                        )";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Cliente", cliente.Nome);
                        cmd.Parameters.AddWithValue("@CpfCnpj", cliente.CPF_CNPJ);
                        cmd.Parameters.AddWithValue("@Rg", cliente.Rg ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Telefone", cliente.Telefone ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Email", cliente.Email ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Endereco", cliente.Endereco ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@NumeroEndereco", cliente.NumeroEndereco ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Complemento", cliente.Complemento ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Bairro", cliente.Bairro ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Cep", cliente.CEP ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CidadeId", cliente.CidadeId ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Tipo", cliente.Tipo ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Genero", string.IsNullOrEmpty(cliente.Genero) ? (object)DBNull.Value : cliente.Genero);
                        cmd.Parameters.AddWithValue("@IdCondicaoPagamento", cliente.IdCondicao > 0 ? (object)cliente.IdCondicao : DBNull.Value);
                        cmd.Parameters.AddWithValue("@Ativo", cliente.Ativo);
                        cmd.Parameters.AddWithValue("@DataNascimento", cliente.DataNascimento ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@DataAlteracao", cliente.DataAlteracao ?? DateTime.Now);

                        if (cliente.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@Id", cliente.Id);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@DataCadastro", cliente.DataCadastro ?? DateTime.Now);
                        }

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar cliente: " + ex.Message);
            }
        }

        public void Excluir(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Clientes WHERE Id = @Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Cliente BuscarPorId(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                SELECT c.Id, c.Cliente, c.CpfCnpj, c.Rg, c.Telefone, c.Email, c.Endereco, c.NumeroEndereco,
                       c.Complemento, c.Bairro, c.Cep, c.CidadeId, ci.Cidade AS NomeCidade,
                       c.Tipo, c.Genero, c.IdCondicaoPagamento, cp.Descricao AS DescricaoCondicao, c.Ativo,
                       c.DataNascimento, c.DataCadastro, c.DataAlteracao
                FROM Clientes c
                LEFT JOIN Cidades ci ON c.CidadeId = ci.Id
                LEFT JOIN CondicoesPagamento cp ON c.IdCondicaoPagamento = cp.Id
                WHERE c.Id = @Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Cliente
                            {
                                Id = reader.GetInt32("Id"),
                                Nome = reader.GetString("Cliente"),
                                CPF_CNPJ = reader.GetString("CpfCnpj"),
                                Rg = reader.IsDBNull(reader.GetOrdinal("Rg")) ? null : reader.GetString("Rg"),
                                Telefone = reader.IsDBNull(reader.GetOrdinal("Telefone")) ? null : reader.GetString("Telefone"),
                                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString("Email"),
                                Endereco = reader.IsDBNull(reader.GetOrdinal("Endereco")) ? null : reader.GetString("Endereco"),
                                NumeroEndereco = reader.IsDBNull(reader.GetOrdinal("NumeroEndereco")) ? (int?)null : reader.GetInt32("NumeroEndereco"),
                                Complemento = reader.IsDBNull(reader.GetOrdinal("Complemento")) ? null : reader.GetString("Complemento"),
                                Bairro = reader.IsDBNull(reader.GetOrdinal("Bairro")) ? null : reader.GetString("Bairro"),
                                CEP = reader.IsDBNull(reader.GetOrdinal("Cep")) ? null : reader.GetString("Cep"),
                                CidadeId = reader.IsDBNull(reader.GetOrdinal("CidadeId")) ? (int?)null : reader.GetInt32("CidadeId"),
                                NomeCidade = reader.IsDBNull(reader.GetOrdinal("NomeCidade")) ? null : reader.GetString("NomeCidade"),
                                Tipo = reader.GetString("Tipo"),
                                Genero = reader.IsDBNull(reader.GetOrdinal("Genero")) ? null : reader.GetString("Genero"),
                                IdCondicao = reader.IsDBNull(reader.GetOrdinal("IdCondicaoPagamento")) ? (int?)null : reader.GetInt32("IdCondicaoPagamento"),
                                DescricaoCondicao = reader.IsDBNull(reader.GetOrdinal("DescricaoCondicao")) ? null : reader.GetString("DescricaoCondicao"),
                                Ativo = reader.GetBoolean("Ativo"),
                                DataNascimento = reader.IsDBNull(reader.GetOrdinal("DataNascimento")) ? (DateTime?)null : reader.GetDateTime("DataNascimento"),
                                DataCadastro = reader.IsDBNull(reader.GetOrdinal("DataCadastro")) ? (DateTime?)null : reader.GetDateTime("DataCadastro"),
                                DataAlteracao = reader.IsDBNull(reader.GetOrdinal("DataAlteracao")) ? (DateTime?)null : reader.GetDateTime("DataAlteracao")
                            };
                        }
                    }
                }
            }
            return null;
        }

        public List<Cliente> ListarClientes()
        {
            List<Cliente> lista = new List<Cliente>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                SELECT c.Id, c.Cliente, c.CpfCnpj, c.Rg, c.Telefone, c.Email, c.Endereco, c.NumeroEndereco,
                       c.Complemento, c.Bairro, c.Cep, c.CidadeId, ci.Cidade AS NomeCidade,
                       c.Tipo, c.Genero, c.IdCondicaoPagamento, cp.Descricao AS DescricaoCondicao, c.Ativo,
                       c.DataNascimento, c.DataCadastro, c.DataAlteracao
                FROM Clientes c
                LEFT JOIN Cidades ci ON c.CidadeId = ci.Id
                LEFT JOIN CondicoesPagamento cp ON c.IdCondicaoPagamento = cp.Id
                ORDER BY c.Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Cliente
                            {
                                Id = reader.GetInt32("Id"),
                                Nome = reader.GetString("Cliente"),
                                CPF_CNPJ = reader.GetString("CpfCnpj"),
                                Rg = reader.IsDBNull(reader.GetOrdinal("Rg")) ? null : reader.GetString("Rg"),
                                Telefone = reader.IsDBNull(reader.GetOrdinal("Telefone")) ? null : reader.GetString("Telefone"),
                                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString("Email"),
                                Endereco = reader.IsDBNull(reader.GetOrdinal("Endereco")) ? null : reader.GetString("Endereco"),
                                NumeroEndereco = reader.IsDBNull(reader.GetOrdinal("NumeroEndereco")) ? (int?)null : reader.GetInt32("NumeroEndereco"),
                                Complemento = reader.IsDBNull(reader.GetOrdinal("Complemento")) ? null : reader.GetString("Complemento"),
                                Bairro = reader.IsDBNull(reader.GetOrdinal("Bairro")) ? null : reader.GetString("Bairro"),
                                CEP = reader.IsDBNull(reader.GetOrdinal("Cep")) ? null : reader.GetString("Cep"),
                                CidadeId = reader.IsDBNull(reader.GetOrdinal("CidadeId")) ? (int?)null : reader.GetInt32("CidadeId"),
                                NomeCidade = reader.IsDBNull(reader.GetOrdinal("NomeCidade")) ? null : reader.GetString("NomeCidade"),
                                Tipo = reader.GetString("Tipo"),
                                Genero = reader.IsDBNull(reader.GetOrdinal("Genero")) ? null : reader.GetString("Genero"),
                                IdCondicao = reader.IsDBNull(reader.GetOrdinal("IdCondicaoPagamento")) ? (int?)null : reader.GetInt32("IdCondicaoPagamento"),
                                DescricaoCondicao = reader.IsDBNull(reader.GetOrdinal("DescricaoCondicao")) ? null : reader.GetString("DescricaoCondicao"),
                                Ativo = reader.GetBoolean("Ativo"),
                                DataNascimento = reader.IsDBNull(reader.GetOrdinal("DataNascimento")) ? (DateTime?)null : reader.GetDateTime("DataNascimento"),
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