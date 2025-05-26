using MySql.Data.MySqlClient;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                        nome = @nome, cpf_cnpj = @cpf_cnpj, rg = @rg, telefone = @telefone, email = @email,
                        endereco = @endereco, numero_endereco = @numero_endereco, complemento = @complemento,
                        bairro = @bairro, cep = @cep, cargo = @cargo, salario = @salario, id_cidade = @id_cidade,
                        tipo = @tipo, status = @status, data_admissao = @data_admissao, data_demissao = @data_demissao,
                        data_modificacao = @data_modificacao
                    WHERE id = @id";
                    }
                    else
                    {
                        query = @"
                    INSERT INTO funcionarios (
                        nome, cpf_cnpj, rg, telefone, email, endereco, numero_endereco, complemento,
                        bairro, cep, cargo, salario, id_cidade, tipo, status, data_admissao, data_demissao,
                        data_criacao, data_modificacao
                    )
                    VALUES (
                        @nome, @cpf_cnpj, @rg, @telefone, @email, @endereco, @numero_endereco, @complemento,
                        @bairro, @cep, @cargo, @salario, @id_cidade, @tipo, @status, @data_admissao, @data_demissao,
                        @data_criacao, @data_modificacao
                    )";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nome", funcionario.Nome);
                        cmd.Parameters.AddWithValue("@cpf_cnpj", funcionario.CPF_CNPJ);
                        cmd.Parameters.AddWithValue("@rg", funcionario.Rg);
                        cmd.Parameters.AddWithValue("@telefone", funcionario.Telefone);
                        cmd.Parameters.AddWithValue("@email", funcionario.Email);
                        cmd.Parameters.AddWithValue("@endereco", funcionario.Endereco);
                        cmd.Parameters.AddWithValue("@numero_endereco", funcionario.NumeroEndereco);
                        cmd.Parameters.AddWithValue("@complemento", funcionario.Complemento);
                        cmd.Parameters.AddWithValue("@bairro", funcionario.Bairro);
                        cmd.Parameters.AddWithValue("@cep", funcionario.CEP);
                        cmd.Parameters.AddWithValue("@cargo", funcionario.Cargo);
                        cmd.Parameters.AddWithValue("@salario", funcionario.Salario);
                        cmd.Parameters.AddWithValue("@id_cidade", funcionario.IdCidade);
                        cmd.Parameters.AddWithValue("@tipo", funcionario.Tipo);
                        cmd.Parameters.AddWithValue("@status", funcionario.Status);
                        cmd.Parameters.AddWithValue("@data_admissao", funcionario.DataAdmissao?.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@data_demissao", funcionario.DataDemissao?.ToString("yyyy-MM-dd"));

                        cmd.Parameters.AddWithValue("@data_modificacao", funcionario.DataModificacao);

                        if (funcionario.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@id", funcionario.Id);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@data_criacao", funcionario.DataCriacao);
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
                                SELECT f.id, f.nome, f.cpf_cnpj, f.rg, f.telefone, f.email,
                                       f.endereco, f.numero_endereco, f.complemento, f.bairro,
                                       f.cep, f.cargo, f.salario, f.tipo, f.data_admissao,
                                       f.data_demissao, f.status, f.id_cidade,
                                       f.data_criacao, f.data_modificacao,
                                       c.nome AS cidade_nome
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
                                Nome = reader.GetString("nome"),
                                CPF_CNPJ = reader.GetString("cpf_cnpj"),
                                Rg = reader.IsDBNull(reader.GetOrdinal("rg")) ? null : reader.GetString("rg"),
                                Telefone = reader.IsDBNull(reader.GetOrdinal("telefone")) ? null : reader.GetString("telefone"),
                                Email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString("email"),
                                Endereco = reader.IsDBNull(reader.GetOrdinal("endereco")) ? null : reader.GetString("endereco"),
                                NumeroEndereco = reader.IsDBNull(reader.GetOrdinal("numero_endereco")) ? 0 : reader.GetInt32("numero_endereco"),
                                Complemento = reader.IsDBNull(reader.GetOrdinal("complemento")) ? null : reader.GetString("complemento"),
                                Bairro = reader.IsDBNull(reader.GetOrdinal("bairro")) ? null : reader.GetString("bairro"),
                                CEP = reader.IsDBNull(reader.GetOrdinal("cep")) ? null : reader.GetString("cep"),
                                Cargo = reader.IsDBNull(reader.GetOrdinal("cargo")) ? null : reader.GetString("cargo"),
                                Salario = reader.IsDBNull(reader.GetOrdinal("salario")) ? 0 : reader.GetDecimal("salario"),
                                Tipo = reader.IsDBNull(reader.GetOrdinal("tipo")) ? null : reader.GetString("tipo"),
                                DataAdmissao = reader.GetDateTime("data_admissao"),
                                DataDemissao = reader.IsDBNull(reader.GetOrdinal("data_demissao")) ? (DateTime?)null : reader.GetDateTime("data_demissao"),
                                Status = reader.GetBoolean("status"),
                                IdCidade = reader.IsDBNull(reader.GetOrdinal("id_cidade")) ? 0 : reader.GetInt32("id_cidade"),
                                NomeCidade = reader.IsDBNull(reader.GetOrdinal("cidade_nome")) ? null : reader.GetString("cidade_nome"),
                                DataCriacao = reader.GetDateTime("data_criacao"),
                                DataModificacao = reader.GetDateTime("data_modificacao"),

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
                SELECT f.id, f.nome, f.cpf_cnpj, f.rg, f.telefone, f.email, f.endereco, f.numero_endereco, f.complemento, 
                       f.bairro, f.cep, f.cargo, f.salario, f.id_cidade, ci.nome AS cidade_nome, f.tipo, 
                       f.status, f.data_admissao, f.data_demissao, f.data_criacao, f.data_modificacao
                FROM funcionarios f
                LEFT JOIN cidades ci ON f.id_cidade = ci.id
                ORDER BY f.nome";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Funcionario
                            {
                                Id = reader.GetInt32("id"),
                                Nome = reader.GetString("nome"),
                                CPF_CNPJ = reader.GetString("cpf_cnpj"),
                                Rg = reader.IsDBNull(reader.GetOrdinal("rg")) ? null : reader.GetString("rg"),
                                Telefone = reader.IsDBNull(reader.GetOrdinal("telefone")) ? null : reader.GetString("telefone"),
                                Email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString("email"),
                                Endereco = reader.IsDBNull(reader.GetOrdinal("endereco")) ? null : reader.GetString("endereco"),
                                NumeroEndereco = reader.IsDBNull(reader.GetOrdinal("numero_endereco")) ? 0 : reader.GetInt32("numero_endereco"),
                                Complemento = reader.IsDBNull(reader.GetOrdinal("complemento")) ? null : reader.GetString("complemento"),
                                Bairro = reader.IsDBNull(reader.GetOrdinal("bairro")) ? null : reader.GetString("bairro"),
                                CEP = reader.IsDBNull(reader.GetOrdinal("cep")) ? null : reader.GetString("cep"),
                                Cargo = reader.IsDBNull(reader.GetOrdinal("cargo")) ? null : reader.GetString("cargo"),
                                Salario = reader.IsDBNull(reader.GetOrdinal("salario")) ? 0m : reader.GetDecimal(reader.GetOrdinal("salario")),
                                IdCidade = reader.IsDBNull(reader.GetOrdinal("id_cidade")) ? (int?)null : reader.GetInt32("id_cidade"),
                                NomeCidade = reader.IsDBNull(reader.GetOrdinal("cidade_nome")) ? null : reader.GetString("cidade_nome"),
                                Tipo = reader.GetString("tipo"),
                                Status = reader.GetBoolean("status"),
                                DataAdmissao = reader.IsDBNull(reader.GetOrdinal("data_admissao")) ? (DateTime?)null : reader.GetDateTime("data_admissao"),
                                DataDemissao = reader.IsDBNull(reader.GetOrdinal("data_demissao")) ? (DateTime?)null : reader.GetDateTime("data_demissao"),
                                DataCriacao = reader.IsDBNull(reader.GetOrdinal("data_criacao")) ? (DateTime?)null : reader.GetDateTime("data_criacao"),
                                DataModificacao = reader.IsDBNull(reader.GetOrdinal("data_modificacao")) ? (DateTime?)null : reader.GetDateTime("data_modificacao"),

                            });
                        }
                    }
                }
            }
            return lista;
        }


    }
}
