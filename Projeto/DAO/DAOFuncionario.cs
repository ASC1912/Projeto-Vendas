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
                        UPDATE funcionarios SET 
                            funcionario = @funcionario, apelido = @apelido, cpf_cnpj = @cpf_cnpj, rg = @rg, telefone = @telefone, email = @email,
                            endereco = @endereco, numero_endereco = @numero_endereco, complemento = @complemento,
                            bairro = @bairro, cep = @cep, id_cidade = @id_cidade, tipo = @tipo, cargo = @cargo, salario = @salario,
                            matricula = @matricula, genero = @genero, data_admissao = @data_admissao, data_demissao = @data_demissao,
                            ativo = @ativo, data_alteracao = @data_alteracao
                        WHERE id = @id";
                    }
                    else
                    {
                        query = @"
                        INSERT INTO funcionarios (
                            funcionario, apelido, cpf_cnpj, rg, telefone, email, endereco, numero_endereco, complemento,
                            bairro, cep, id_cidade, tipo, cargo, salario, matricula, genero, data_admissao, data_demissao,
                            ativo, data_cadastro, data_alteracao
                        )
                        VALUES (
                            @funcionario, @apelido, @cpf_cnpj, @rg, @telefone, @email, @endereco, @numero_endereco, @complemento,
                            @bairro, @cep, @id_cidade, @tipo, @cargo, @salario, @matricula, @genero, @data_admissao, @data_demissao,
                            @ativo, @data_cadastro, @data_alteracao
                        )";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@funcionario", funcionario.Nome);
                        cmd.Parameters.AddWithValue("@apelido", funcionario.Apelido ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@cpf_cnpj", funcionario.CPF_CNPJ);
                        cmd.Parameters.AddWithValue("@rg", funcionario.Rg ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@telefone", funcionario.Telefone ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@email", funcionario.Email ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@endereco", funcionario.Endereco ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@numero_endereco", funcionario.NumeroEndereco ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@complemento", funcionario.Complemento ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@bairro", funcionario.Bairro ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@cep", funcionario.CEP ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@id_cidade", funcionario.IdCidade ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@tipo", funcionario.Tipo ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@cargo", funcionario.Cargo ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@salario", funcionario.Salario);
                        cmd.Parameters.AddWithValue("@matricula", funcionario.Matricula ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@genero", funcionario.Genero ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@data_admissao", funcionario.DataAdmissao ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@data_demissao", funcionario.DataDemissao ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@ativo", funcionario.Ativo);
                        cmd.Parameters.AddWithValue("@data_alteracao", funcionario.DataAlteracao ?? DateTime.Now);

                        if (funcionario.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@id", funcionario.Id);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@data_cadastro", funcionario.DataCadastro ?? DateTime.Now);
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
                string query = "DELETE FROM funcionarios WHERE id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
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
                SELECT f.id, f.funcionario, f.apelido, f.cpf_cnpj, f.rg, f.telefone, f.email,
                       f.endereco, f.numero_endereco, f.complemento, f.bairro,
                       f.cep, f.tipo, f.cargo, f.salario, f.matricula, f.genero, f.data_admissao, f.data_demissao,
                       f.ativo, f.id_cidade, f.data_cadastro, f.data_alteracao,
                       c.cidade AS cidade_nome
                FROM funcionarios f
                LEFT JOIN cidades c ON f.id_cidade = c.id
                WHERE f.id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Funcionario
                            {
                                Id = reader.GetInt32("id"),
                                Nome = reader.GetString("funcionario"),
                                Apelido = reader.IsDBNull(reader.GetOrdinal("apelido")) ? null : reader.GetString("apelido"),
                                CPF_CNPJ = reader.GetString("cpf_cnpj"),
                                Rg = reader.IsDBNull(reader.GetOrdinal("rg")) ? null : reader.GetString("rg"),
                                Telefone = reader.IsDBNull(reader.GetOrdinal("telefone")) ? null : reader.GetString("telefone"),
                                Email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString("email"),
                                Endereco = reader.IsDBNull(reader.GetOrdinal("endereco")) ? null : reader.GetString("endereco"),
                                NumeroEndereco = reader.IsDBNull(reader.GetOrdinal("numero_endereco")) ? (int?)null : reader.GetInt32("numero_endereco"),
                                Complemento = reader.IsDBNull(reader.GetOrdinal("complemento")) ? null : reader.GetString("complemento"),
                                Bairro = reader.IsDBNull(reader.GetOrdinal("bairro")) ? null : reader.GetString("bairro"),
                                CEP = reader.IsDBNull(reader.GetOrdinal("cep")) ? null : reader.GetString("cep"),
                                Tipo = reader.IsDBNull(reader.GetOrdinal("tipo")) ? null : reader.GetString("tipo"),
                                Cargo = reader.IsDBNull(reader.GetOrdinal("cargo")) ? null : reader.GetString("cargo"),
                                Salario = reader.GetDecimal("salario"),
                                Matricula = reader.IsDBNull(reader.GetOrdinal("matricula")) ? null : reader.GetString("matricula"),
                                Genero = reader.IsDBNull(reader.GetOrdinal("genero")) ? null : reader.GetString("genero"),
                                DataAdmissao = reader.IsDBNull(reader.GetOrdinal("data_admissao")) ? (DateTime?)null : reader.GetDateTime("data_admissao"),
                                DataDemissao = reader.IsDBNull(reader.GetOrdinal("data_demissao")) ? (DateTime?)null : reader.GetDateTime("data_demissao"),
                                Ativo = reader.GetBoolean("ativo"),
                                IdCidade = reader.IsDBNull(reader.GetOrdinal("id_cidade")) ? (int?)null : reader.GetInt32("id_cidade"),
                                NomeCidade = reader.IsDBNull(reader.GetOrdinal("cidade_nome")) ? null : reader.GetString("cidade_nome"),
                                DataCadastro = reader.IsDBNull(reader.GetOrdinal("data_cadastro")) ? (DateTime?)null : reader.GetDateTime("data_cadastro"),
                                DataAlteracao = reader.IsDBNull(reader.GetOrdinal("data_alteracao")) ? (DateTime?)null : reader.GetDateTime("data_alteracao")
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
                SELECT f.id, f.funcionario, f.apelido, f.cpf_cnpj, f.rg, f.telefone, f.email,
                       f.endereco, f.numero_endereco, f.complemento, f.bairro,
                       f.cep, f.tipo, f.cargo, f.salario, f.matricula, f.genero, f.data_admissao, f.data_demissao,
                       f.ativo, f.id_cidade, f.data_cadastro, f.data_alteracao,
                       c.cidade AS cidade_nome
                FROM funcionarios f
                LEFT JOIN cidades c ON f.id_cidade = c.id
                ORDER BY f.id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Funcionario
                            {
                                Id = reader.GetInt32("id"),
                                Nome = reader.GetString("funcionario"),
                                Apelido = reader.IsDBNull(reader.GetOrdinal("apelido")) ? null : reader.GetString("apelido"),
                                CPF_CNPJ = reader.GetString("cpf_cnpj"),
                                Rg = reader.IsDBNull(reader.GetOrdinal("rg")) ? null : reader.GetString("rg"),
                                Telefone = reader.IsDBNull(reader.GetOrdinal("telefone")) ? null : reader.GetString("telefone"),
                                Email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString("email"),
                                Endereco = reader.IsDBNull(reader.GetOrdinal("endereco")) ? null : reader.GetString("endereco"),
                                NumeroEndereco = reader.IsDBNull(reader.GetOrdinal("numero_endereco")) ? (int?)null : reader.GetInt32("numero_endereco"),
                                Complemento = reader.IsDBNull(reader.GetOrdinal("complemento")) ? null : reader.GetString("complemento"),
                                Bairro = reader.IsDBNull(reader.GetOrdinal("bairro")) ? null : reader.GetString("bairro"),
                                CEP = reader.IsDBNull(reader.GetOrdinal("cep")) ? null : reader.GetString("cep"),
                                Tipo = reader.IsDBNull(reader.GetOrdinal("tipo")) ? null : reader.GetString("tipo"),
                                Cargo = reader.IsDBNull(reader.GetOrdinal("cargo")) ? null : reader.GetString("cargo"),
                                Salario = reader.GetDecimal("salario"),
                                Matricula = reader.IsDBNull(reader.GetOrdinal("matricula")) ? null : reader.GetString("matricula"),
                                Genero = reader.IsDBNull(reader.GetOrdinal("genero")) ? null : reader.GetString("genero"),
                                DataAdmissao = reader.IsDBNull(reader.GetOrdinal("data_admissao")) ? (DateTime?)null : reader.GetDateTime("data_admissao"),
                                DataDemissao = reader.IsDBNull(reader.GetOrdinal("data_demissao")) ? (DateTime?)null : reader.GetDateTime("data_demissao"),
                                Ativo = reader.GetBoolean("ativo"),
                                IdCidade = reader.IsDBNull(reader.GetOrdinal("id_cidade")) ? (int?)null : reader.GetInt32("id_cidade"),
                                NomeCidade = reader.IsDBNull(reader.GetOrdinal("cidade_nome")) ? null : reader.GetString("cidade_nome"),
                                DataCadastro = reader.IsDBNull(reader.GetOrdinal("data_cadastro")) ? (DateTime?)null : reader.GetDateTime("data_cadastro"),
                                DataAlteracao = reader.IsDBNull(reader.GetOrdinal("data_alteracao")) ? (DateTime?)null : reader.GetDateTime("data_alteracao")
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}
