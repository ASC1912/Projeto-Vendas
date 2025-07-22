using MySql.Data.MySqlClient;
using Projeto.Models;
using System;
using System.Collections.Generic;

namespace Projeto.DAO
{
    internal class DAOFuncionario
    {
        private string connectionString = "Server=localhost;Database=sistema;Uid=root;Pwd=12345678;";

        public void Salvar(Funcionario funcionario)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query;

                    if (funcionario.Id > 0)
                    {
                        query = @"
                        UPDATE Funcionarios SET
                            Funcionario = @Funcionario, Apelido = @Apelido, CpfCnpj = @CpfCnpj, Rg = @Rg, Telefone = @Telefone, Email = @Email,
                            Endereco = @Endereco, NumeroEndereco = @NumeroEndereco, Complemento = @Complemento,
                            Bairro = @Bairro, Cep = @Cep, CidadeId = @CidadeId, Tipo = @Tipo, Cargo = @Cargo, Salario = @Salario,
                            Matricula = @Matricula, Genero = @Genero, DataAdmissao = @DataAdmissao, DataDemissao = @DataDemissao,
                            DataNascimento = @DataNascimento, Ativo = @Ativo, DataAlteracao = @DataAlteracao
                        WHERE Id = @Id";
                    }
                    else
                    {
                        query = @"
                        INSERT INTO Funcionarios (
                            Funcionario, Apelido, CpfCnpj, Rg, Telefone, Email, Endereco, NumeroEndereco, Complemento,
                            Bairro, Cep, CidadeId, Tipo, Cargo, Salario, Matricula, Genero, DataAdmissao, DataDemissao,
                            DataNascimento, Ativo, DataCadastro, DataAlteracao
                        )
                        VALUES (
                            @Funcionario, @Apelido, @CpfCnpj, @Rg, @Telefone, @Email, @Endereco, @NumeroEndereco, @Complemento,
                            @Bairro, @Cep, @CidadeId, @Tipo, @Cargo, @Salario, @Matricula, @Genero, @DataAdmissao, @DataDemissao,
                            @DataNascimento, @Ativo, @DataCadastro, @DataAlteracao
                        )";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Funcionario", funcionario.Nome);
                        cmd.Parameters.AddWithValue("@Apelido", funcionario.Apelido ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CpfCnpj", funcionario.CPF_CNPJ);
                        cmd.Parameters.AddWithValue("@Rg", funcionario.Rg ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Telefone", funcionario.Telefone ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Email", funcionario.Email ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Endereco", funcionario.Endereco ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@NumeroEndereco", funcionario.NumeroEndereco ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Complemento", funcionario.Complemento ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Bairro", funcionario.Bairro ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Cep", funcionario.CEP ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CidadeId", funcionario.CidadeId ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Tipo", funcionario.Tipo ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Cargo", funcionario.Cargo ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Salario", funcionario.Salario);
                        cmd.Parameters.AddWithValue("@Matricula", funcionario.Matricula ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Genero", funcionario.Genero ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@DataAdmissao", funcionario.DataAdmissao ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@DataDemissao", funcionario.DataDemissao ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@DataNascimento", funcionario.DataNascimento ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Ativo", funcionario.Ativo);
                        cmd.Parameters.AddWithValue("@DataAlteracao", funcionario.DataAlteracao ?? DateTime.Now);

                        if (funcionario.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@Id", funcionario.Id);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@DataCadastro", funcionario.DataCadastro ?? DateTime.Now);
                        }

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar funcionário: " + ex.Message);
            }
        }

        public void Excluir(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Funcionarios WHERE Id = @Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Funcionario BuscarPorId(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                SELECT f.Id, f.Funcionario, f.Apelido, f.CpfCnpj, f.Rg, f.Telefone, f.Email,
                       f.Endereco, f.NumeroEndereco, f.Complemento, f.Bairro,
                       f.Cep, f.Tipo, f.Cargo, f.Salario, f.Matricula, f.Genero, f.DataAdmissao, f.DataDemissao,
                       f.DataNascimento, f.Ativo, f.CidadeId, f.DataCadastro, f.DataAlteracao,
                       c.Cidade AS NomeCidade
                FROM Funcionarios f
                LEFT JOIN Cidades c ON f.CidadeId = c.Id
                WHERE f.Id = @Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Funcionario
                            {
                                Id = reader.GetInt32("Id"),
                                Nome = reader.GetString("Funcionario"),
                                Apelido = reader.IsDBNull(reader.GetOrdinal("Apelido")) ? null : reader.GetString("Apelido"),
                                CPF_CNPJ = reader.GetString("CpfCnpj"),
                                Rg = reader.IsDBNull(reader.GetOrdinal("Rg")) ? null : reader.GetString("Rg"),
                                Telefone = reader.IsDBNull(reader.GetOrdinal("Telefone")) ? null : reader.GetString("Telefone"),
                                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString("Email"),
                                Endereco = reader.IsDBNull(reader.GetOrdinal("Endereco")) ? null : reader.GetString("Endereco"),
                                NumeroEndereco = reader.IsDBNull(reader.GetOrdinal("NumeroEndereco")) ? (int?)null : reader.GetInt32("NumeroEndereco"),
                                Complemento = reader.IsDBNull(reader.GetOrdinal("Complemento")) ? null : reader.GetString("Complemento"),
                                Bairro = reader.IsDBNull(reader.GetOrdinal("Bairro")) ? null : reader.GetString("Bairro"),
                                CEP = reader.IsDBNull(reader.GetOrdinal("Cep")) ? null : reader.GetString("Cep"),
                                Tipo = reader.IsDBNull(reader.GetOrdinal("Tipo")) ? null : reader.GetString("Tipo"),
                                Cargo = reader.IsDBNull(reader.GetOrdinal("Cargo")) ? null : reader.GetString("Cargo"),
                                Salario = reader.GetDecimal("Salario"),
                                Matricula = reader.IsDBNull(reader.GetOrdinal("Matricula")) ? null : reader.GetString("Matricula"),
                                Genero = reader.IsDBNull(reader.GetOrdinal("Genero")) ? null : reader.GetString("Genero"),
                                DataAdmissao = reader.IsDBNull(reader.GetOrdinal("DataAdmissao")) ? (DateTime?)null : reader.GetDateTime("DataAdmissao"),
                                DataDemissao = reader.IsDBNull(reader.GetOrdinal("DataDemissao")) ? (DateTime?)null : reader.GetDateTime("DataDemissao"),
                                DataNascimento = reader.IsDBNull(reader.GetOrdinal("DataNascimento")) ? (DateTime?)null : reader.GetDateTime("DataNascimento"),
                                Ativo = reader.GetBoolean("Ativo"),
                                CidadeId = reader.IsDBNull(reader.GetOrdinal("CidadeId")) ? (int?)null : reader.GetInt32("CidadeId"),
                                NomeCidade = reader.IsDBNull(reader.GetOrdinal("NomeCidade")) ? null : reader.GetString("NomeCidade"),
                                DataCadastro = reader.IsDBNull(reader.GetOrdinal("DataCadastro")) ? (DateTime?)null : reader.GetDateTime("DataCadastro"),
                                DataAlteracao = reader.IsDBNull(reader.GetOrdinal("DataAlteracao")) ? (DateTime?)null : reader.GetDateTime("DataAlteracao")
                            };
                        }
                    }
                }
            }
            return null;
        }

        public List<Funcionario> ListarFuncionarios()
        {
            List<Funcionario> lista = new List<Funcionario>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                SELECT f.Id, f.Funcionario, f.Apelido, f.CpfCnpj, f.Rg, f.Telefone, f.Email,
                       f.Endereco, f.NumeroEndereco, f.Complemento, f.Bairro,
                       f.Cep, f.Tipo, f.Cargo, f.Salario, f.Matricula, f.Genero, f.DataAdmissao, f.DataDemissao,
                       f.DataNascimento, f.Ativo, f.CidadeId, f.DataCadastro, f.DataAlteracao,
                       c.Cidade AS NomeCidade
                FROM Funcionarios f
                LEFT JOIN Cidades c ON f.CidadeId = c.Id
                ORDER BY f.Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Funcionario
                            {
                                Id = reader.GetInt32("Id"),
                                Nome = reader.GetString("Funcionario"),
                                Apelido = reader.IsDBNull(reader.GetOrdinal("Apelido")) ? null : reader.GetString("Apelido"),
                                CPF_CNPJ = reader.GetString("CpfCnpj"),
                                Rg = reader.IsDBNull(reader.GetOrdinal("Rg")) ? null : reader.GetString("Rg"),
                                Telefone = reader.IsDBNull(reader.GetOrdinal("Telefone")) ? null : reader.GetString("Telefone"),
                                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString("Email"),
                                Endereco = reader.IsDBNull(reader.GetOrdinal("Endereco")) ? null : reader.GetString("Endereco"),
                                NumeroEndereco = reader.IsDBNull(reader.GetOrdinal("NumeroEndereco")) ? (int?)null : reader.GetInt32("NumeroEndereco"),
                                Complemento = reader.IsDBNull(reader.GetOrdinal("Complemento")) ? null : reader.GetString("Complemento"),
                                Bairro = reader.IsDBNull(reader.GetOrdinal("Bairro")) ? null : reader.GetString("Bairro"),
                                CEP = reader.IsDBNull(reader.GetOrdinal("Cep")) ? null : reader.GetString("Cep"),
                                Tipo = reader.IsDBNull(reader.GetOrdinal("Tipo")) ? null : reader.GetString("Tipo"),
                                Cargo = reader.IsDBNull(reader.GetOrdinal("Cargo")) ? null : reader.GetString("Cargo"),
                                Salario = reader.GetDecimal("Salario"),
                                Matricula = reader.IsDBNull(reader.GetOrdinal("Matricula")) ? null : reader.GetString("Matricula"),
                                Genero = reader.IsDBNull(reader.GetOrdinal("Genero")) ? null : reader.GetString("Genero"),
                                DataAdmissao = reader.IsDBNull(reader.GetOrdinal("DataAdmissao")) ? (DateTime?)null : reader.GetDateTime("DataAdmissao"),
                                DataDemissao = reader.IsDBNull(reader.GetOrdinal("DataDemissao")) ? (DateTime?)null : reader.GetDateTime("DataDemissao"),
                                DataNascimento = reader.IsDBNull(reader.GetOrdinal("DataNascimento")) ? (DateTime?)null : reader.GetDateTime("DataNascimento"),
                                Ativo = reader.GetBoolean("Ativo"),
                                CidadeId = reader.IsDBNull(reader.GetOrdinal("CidadeId")) ? (int?)null : reader.GetInt32("CidadeId"),
                                NomeCidade = reader.IsDBNull(reader.GetOrdinal("NomeCidade")) ? null : reader.GetString("NomeCidade"),
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