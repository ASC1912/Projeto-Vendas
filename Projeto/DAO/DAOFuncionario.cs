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
                                Funcionario = @Nome, Apelido = @Apelido, CpfCnpj = @CPF_CNPJ, Rg = @Rg, Telefone = @Telefone, Email = @Email,
                                Endereco = @Endereco, NumeroEndereco = @NumeroEndereco, Complemento = @Complemento,
                                Bairro = @Bairro, Cep = @CEP, CidadeId = @CidadeId, Tipo = @Tipo, Cargo = @Cargo, Salario = @Salario,
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
                            ) VALUES (
                                @Nome, @Apelido, @CPF_CNPJ, @Rg, @Telefone, @Email, @Endereco, @NumeroEndereco, @Complemento,
                                @Bairro, @CEP, @CidadeId, @Tipo, @Cargo, @Salario, @Matricula, @Genero, @DataAdmissao, @DataDemissao,
                                @DataNascimento, @Ativo, @DataCadastro, @DataAlteracao
                            )";
                    }
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", funcionario.Id);
                        cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
                        cmd.Parameters.AddWithValue("@Apelido", funcionario.Apelido);
                        cmd.Parameters.AddWithValue("@CPF_CNPJ", funcionario.CPF_CNPJ);
                        cmd.Parameters.AddWithValue("@Rg", funcionario.Rg);
                        cmd.Parameters.AddWithValue("@Telefone", funcionario.Telefone);
                        cmd.Parameters.AddWithValue("@Email", funcionario.Email);
                        cmd.Parameters.AddWithValue("@Endereco", funcionario.Endereco);
                        cmd.Parameters.AddWithValue("@NumeroEndereco", funcionario.NumeroEndereco);
                        cmd.Parameters.AddWithValue("@Complemento", funcionario.Complemento);
                        cmd.Parameters.AddWithValue("@Bairro", funcionario.Bairro);
                        cmd.Parameters.AddWithValue("@CEP", funcionario.CEP);
                        cmd.Parameters.AddWithValue("@CidadeId", funcionario.CidadeId);
                        cmd.Parameters.AddWithValue("@Tipo", funcionario.Tipo);
                        cmd.Parameters.AddWithValue("@Cargo", funcionario.Cargo);
                        cmd.Parameters.AddWithValue("@Salario", funcionario.Salario);
                        cmd.Parameters.AddWithValue("@Matricula", funcionario.Matricula);
                        cmd.Parameters.AddWithValue("@Genero", funcionario.Genero);
                        cmd.Parameters.AddWithValue("@DataAdmissao", funcionario.DataAdmissao);
                        cmd.Parameters.AddWithValue("@DataDemissao", funcionario.DataDemissao);
                        cmd.Parameters.AddWithValue("@DataNascimento", funcionario.DataNascimento);
                        cmd.Parameters.AddWithValue("@Ativo", funcionario.Ativo);
                        cmd.Parameters.AddWithValue("@DataCadastro", funcionario.DataCadastro);
                        cmd.Parameters.AddWithValue("@DataAlteracao", funcionario.DataAlteracao);

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
                    SELECT 
                        f.*, 
                        c.Id AS CidadeObjId, c.Cidade AS CidadeObjNome,
                        e.Id AS EstadoObjId, e.Estado AS EstadoObjNome, e.UF,
                        p.Id AS PaisObjId, p.Pais AS PaisObjNome
                    FROM Funcionarios f
                    LEFT JOIN Cidades c ON f.CidadeId = c.Id
                    LEFT JOIN Estados e ON c.EstadoId = e.Id
                    LEFT JOIN Paises p ON e.PaisId = p.Id
                    WHERE f.Id = @Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MontarObjetoFuncionario(reader);
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
                    SELECT 
                        f.*, 
                        c.Id AS CidadeObjId, c.Cidade AS CidadeObjNome,
                        e.Id AS EstadoObjId, e.Estado AS EstadoObjNome, e.UF,
                        p.Id AS PaisObjId, p.Pais AS PaisObjNome
                    FROM Funcionarios f
                    LEFT JOIN Cidades c ON f.CidadeId = c.Id
                    LEFT JOIN Estados e ON c.EstadoId = e.Id
                    LEFT JOIN Paises p ON e.PaisId = p.Id
                    ORDER BY f.Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(MontarObjetoFuncionario(reader));
                        }
                    }
                }
            }
            return lista;
        }

        private Funcionario MontarObjetoFuncionario(MySqlDataReader reader)
        {
            Pais pais = null;
            if (reader["PaisObjId"] != DBNull.Value)
            {
                pais = new Pais
                {
                    Id = Convert.ToInt32(reader["PaisObjId"]),
                    NomePais = Convert.ToString(reader["PaisObjNome"])
                };
            }

            Estado estado = null;
            if (reader["EstadoObjId"] != DBNull.Value)
            {
                estado = new Estado
                {
                    Id = Convert.ToInt32(reader["EstadoObjId"]),
                    NomeEstado = Convert.ToString(reader["EstadoObjNome"]),
                    UF = Convert.ToString(reader["UF"]),
                    oPais = pais
                };
            }

            Cidade cidade = null;
            if (reader["CidadeObjId"] != DBNull.Value)
            {
                cidade = new Cidade
                {
                    Id = Convert.ToInt32(reader["CidadeObjId"]),
                    NomeCidade = Convert.ToString(reader["CidadeObjNome"]),
                    oEstado = estado
                };
            }

            var funcionario = new Funcionario
            {
                Id = Convert.ToInt32(reader["Id"]),
                Nome = Convert.ToString(reader["Funcionario"]),
                Apelido = reader["Apelido"] == DBNull.Value ? null : Convert.ToString(reader["Apelido"]),
                CPF_CNPJ = Convert.ToString(reader["CpfCnpj"]),
                Rg = reader["Rg"] == DBNull.Value ? null : Convert.ToString(reader["Rg"]),
                Telefone = reader["Telefone"] == DBNull.Value ? null : Convert.ToString(reader["Telefone"]),
                Email = reader["Email"] == DBNull.Value ? null : Convert.ToString(reader["Email"]),
                Endereco = reader["Endereco"] == DBNull.Value ? null : Convert.ToString(reader["Endereco"]),
                NumeroEndereco = reader["NumeroEndereco"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["NumeroEndereco"]),
                Complemento = reader["Complemento"] == DBNull.Value ? null : Convert.ToString(reader["Complemento"]),
                Bairro = reader["Bairro"] == DBNull.Value ? null : Convert.ToString(reader["Bairro"]),
                CEP = reader["Cep"] == DBNull.Value ? null : Convert.ToString(reader["Cep"]),
                Tipo = reader["Tipo"] == DBNull.Value ? null : Convert.ToString(reader["Tipo"]),
                Cargo = reader["Cargo"] == DBNull.Value ? null : Convert.ToString(reader["Cargo"]),
                Salario = Convert.ToDecimal(reader["Salario"]),
                Matricula = reader["Matricula"] == DBNull.Value ? null : Convert.ToString(reader["Matricula"]),
                Genero = reader["Genero"] == DBNull.Value ? null : Convert.ToString(reader["Genero"]),
                DataAdmissao = reader["DataAdmissao"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["DataAdmissao"]),
                DataDemissao = reader["DataDemissao"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["DataDemissao"]),
                DataNascimento = reader["DataNascimento"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["DataNascimento"]),
                Ativo = Convert.ToBoolean(reader["Ativo"]),
                CidadeId = reader["CidadeId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["CidadeId"]),
                DataCadastro = reader["DataCadastro"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["DataCadastro"]),
                DataAlteracao = reader["DataAlteracao"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["DataAlteracao"]),
                oCidade = cidade
            };

            return funcionario;
        }
    }
}