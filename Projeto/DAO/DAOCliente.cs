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
                                Cliente = @Nome, CpfCnpj = @CPF_CNPJ, Rg = @Rg, Telefone = @Telefone, Email = @Email,
                                Endereco = @Endereco, NumeroEndereco = @NumeroEndereco, Complemento = @Complemento,
                                Bairro = @Bairro, Cep = @CEP, CidadeId = @CidadeId, Tipo = @Tipo, Genero = @Genero,
                                IdCondicaoPagamento = @IdCondicao, Ativo = @Ativo,
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
                                @Nome, @CPF_CNPJ, @Rg, @Telefone, @Email, @Endereco, @NumeroEndereco, @Complemento,
                                @Bairro, @CEP, @CidadeId, @Tipo, @Genero, @IdCondicao, @Ativo,
                                @DataNascimento, @DataCadastro, @DataAlteracao
                            )";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", cliente.Id);
                        cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                        cmd.Parameters.AddWithValue("@CPF_CNPJ", cliente.CPF_CNPJ);
                        cmd.Parameters.AddWithValue("@Rg", cliente.Rg);
                        cmd.Parameters.AddWithValue("@Telefone", cliente.Telefone);
                        cmd.Parameters.AddWithValue("@Email", cliente.Email);
                        cmd.Parameters.AddWithValue("@Endereco", cliente.Endereco);
                        cmd.Parameters.AddWithValue("@NumeroEndereco", cliente.NumeroEndereco);
                        cmd.Parameters.AddWithValue("@Complemento", cliente.Complemento);
                        cmd.Parameters.AddWithValue("@Bairro", cliente.Bairro);
                        cmd.Parameters.AddWithValue("@CEP", cliente.CEP);
                        cmd.Parameters.AddWithValue("@CidadeId", cliente.CidadeId);
                        cmd.Parameters.AddWithValue("@Tipo", cliente.Tipo);
                        cmd.Parameters.AddWithValue("@Genero", cliente.Genero);
                        cmd.Parameters.AddWithValue("@IdCondicao", cliente.IdCondicao);
                        cmd.Parameters.AddWithValue("@Ativo", cliente.Ativo);
                        cmd.Parameters.AddWithValue("@DataNascimento", cliente.DataNascimento);
                        cmd.Parameters.AddWithValue("@DataCadastro", cliente.DataCadastro);
                        cmd.Parameters.AddWithValue("@DataAlteracao", cliente.DataAlteracao);
                        
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
                    SELECT 
                        cl.*,
                        c.Id AS CidadeObjId, c.Cidade AS CidadeObjNome,
                        e.Id AS EstadoObjId, e.Estado AS EstadoObjNome, e.UF,
                        p.Id AS PaisObjId, p.Pais AS PaisObjNome,
                        cp.Id as CondicaoObjId, cp.Descricao as CondicaoObjDescricao
                    FROM Clientes cl
                    LEFT JOIN Cidades c ON cl.CidadeId = c.Id
                    LEFT JOIN Estados e ON c.EstadoId = e.Id
                    LEFT JOIN Paises p ON e.PaisId = p.Id
                    LEFT JOIN CondicoesPagamento cp ON cl.IdCondicaoPagamento = cp.Id
                    WHERE cl.Id = @Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MontarObjetoCliente(reader);
                        }
                    }
                }
            }
            return null;
        }

        public List<Cliente> ListarClientes() // Corrigido nome do método
        {
            List<Cliente> lista = new List<Cliente>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT 
                        cl.*,
                        c.Id AS CidadeObjId, c.Cidade AS CidadeObjNome,
                        e.Id AS EstadoObjId, e.Estado AS EstadoObjNome, e.UF,
                        p.Id AS PaisObjId, p.Pais AS PaisObjNome,
                        cp.Id as CondicaoObjId, cp.Descricao as CondicaoObjDescricao
                    FROM Clientes cl
                    LEFT JOIN Cidades c ON cl.CidadeId = c.Id
                    LEFT JOIN Estados e ON c.EstadoId = e.Id
                    LEFT JOIN Paises p ON e.PaisId = p.Id
                    LEFT JOIN CondicoesPagamento cp ON cl.IdCondicaoPagamento = cp.Id
                    ORDER BY cl.Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(MontarObjetoCliente(reader));
                        }
                    }
                }
            }
            return lista;
        }

        private Cliente MontarObjetoCliente(MySqlDataReader reader)
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

            CondicaoPagamento condicao = null;
            if (reader["CondicaoObjId"] != DBNull.Value)
            {
                condicao = new CondicaoPagamento
                {
                    Id = Convert.ToInt32(reader["CondicaoObjId"]),
                    Descricao = Convert.ToString(reader["CondicaoObjDescricao"])
                };
            }

            var cliente = new Cliente
            {
                Id = Convert.ToInt32(reader["Id"]),
                Nome = Convert.ToString(reader["Cliente"]),
                CPF_CNPJ = Convert.ToString(reader["CpfCnpj"]),
                Rg = reader["Rg"] == DBNull.Value ? null : Convert.ToString(reader["Rg"]),
                Telefone = reader["Telefone"] == DBNull.Value ? null : Convert.ToString(reader["Telefone"]),
                Email = reader["Email"] == DBNull.Value ? null : Convert.ToString(reader["Email"]),
                Endereco = reader["Endereco"] == DBNull.Value ? null : Convert.ToString(reader["Endereco"]),
                NumeroEndereco = reader["NumeroEndereco"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["NumeroEndereco"]),
                Complemento = reader["Complemento"] == DBNull.Value ? null : Convert.ToString(reader["Complemento"]),
                Bairro = reader["Bairro"] == DBNull.Value ? null : Convert.ToString(reader["Bairro"]),
                CEP = reader["Cep"] == DBNull.Value ? null : Convert.ToString(reader["Cep"]),
                Tipo = Convert.ToString(reader["Tipo"]),
                Genero = reader["Genero"] == DBNull.Value ? null : Convert.ToString(reader["Genero"]),
                IdCondicao = reader["IdCondicaoPagamento"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["IdCondicaoPagamento"]),
                Ativo = Convert.ToBoolean(reader["Ativo"]),
                DataNascimento = reader["DataNascimento"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["DataNascimento"]),
                DataCadastro = reader["DataCadastro"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["DataCadastro"]),
                DataAlteracao = reader["DataAlteracao"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["DataAlteracao"]),
                CidadeId = reader["CidadeId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["CidadeId"]),
                oCidade = cidade,
                //oCondicaoPagamento = condicao
            };

            return cliente;
        }
    }
}