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
                        UPDATE clientes SET 
                            cliente = @cliente, cpf_cnpj = @cpf_cnpj, rg = @rg, telefone = @telefone, email = @email,
                            endereco = @endereco, numero_endereco = @numero_endereco, complemento = @complemento,
                            bairro = @bairro, cep = @cep, id_cidade = @id_cidade, tipo = @tipo, genero = @genero,
                            id_condicao_pagamento = @id_condicao_pagamento, ativo = @ativo,
                            data_alteracao = @data_alteracao
                        WHERE id = @id";
                    }
                    else
                    {
                        query = @"
                        INSERT INTO clientes (
                            cliente, cpf_cnpj, rg, telefone, email, endereco, numero_endereco, complemento,
                            bairro, cep, id_cidade, tipo, genero, id_condicao_pagamento, ativo,
                            data_cadastro, data_alteracao
                        ) VALUES (
                            @cliente, @cpf_cnpj, @rg, @telefone, @email, @endereco, @numero_endereco, @complemento,
                            @bairro, @cep, @id_cidade, @tipo, @genero, @id_condicao_pagamento, @ativo,
                            @data_cadastro, @data_alteracao
                        )";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@cliente", cliente.Nome);
                        cmd.Parameters.AddWithValue("@cpf_cnpj", cliente.CPF_CNPJ);
                        cmd.Parameters.AddWithValue("@rg", cliente.Rg ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@telefone", cliente.Telefone ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@email", cliente.Email ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@endereco", cliente.Endereco ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@numero_endereco", cliente.NumeroEndereco ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@complemento", cliente.Complemento ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@bairro", cliente.Bairro ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@cep", cliente.CEP ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@id_cidade", cliente.IdCidade ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@tipo", cliente.Tipo ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@genero", string.IsNullOrEmpty(cliente.Genero) ? (object)DBNull.Value : cliente.Genero);
                        cmd.Parameters.AddWithValue("@id_condicao_pagamento", cliente.IdCondicao > 0 ? (object)cliente.IdCondicao : DBNull.Value);
                        cmd.Parameters.AddWithValue("@ativo", cliente.Ativo);
                        cmd.Parameters.AddWithValue("@data_alteracao", cliente.DataAlteracao ?? DateTime.Now);

                        if (cliente.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@id", cliente.Id);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@data_cadastro", cliente.DataCadastro ?? DateTime.Now);
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
                string query = "DELETE FROM clientes WHERE id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
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
                SELECT c.id, c.cliente, c.cpf_cnpj, c.rg, c.telefone, c.email, c.endereco, c.numero_endereco,
                       c.complemento, c.bairro, c.cep, c.id_cidade, ci.cidade AS cidade_nome,
                       c.tipo, c.genero, c.id_condicao_pagamento, cp.descricao AS condicao_descricao, c.ativo,
                       c.data_cadastro, c.data_alteracao
                FROM clientes c
                LEFT JOIN cidades ci ON c.id_cidade = ci.id
                LEFT JOIN condicoes_pagamento cp ON c.id_condicao_pagamento = cp.id
                WHERE c.id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Cliente
                            {
                                Id = reader.GetInt32("id"),
                                Nome = reader.GetString("cliente"),
                                CPF_CNPJ = reader.GetString("cpf_cnpj"),
                                Rg = reader.IsDBNull(reader.GetOrdinal("rg")) ? null : reader.GetString("rg"),
                                Telefone = reader.IsDBNull(reader.GetOrdinal("telefone")) ? null : reader.GetString("telefone"),
                                Email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString("email"),
                                Endereco = reader.IsDBNull(reader.GetOrdinal("endereco")) ? null : reader.GetString("endereco"),
                                NumeroEndereco = reader.IsDBNull(reader.GetOrdinal("numero_endereco")) ? (int?)null : reader.GetInt32("numero_endereco"),
                                Complemento = reader.IsDBNull(reader.GetOrdinal("complemento")) ? null : reader.GetString("complemento"),
                                Bairro = reader.IsDBNull(reader.GetOrdinal("bairro")) ? null : reader.GetString("bairro"),
                                CEP = reader.IsDBNull(reader.GetOrdinal("cep")) ? null : reader.GetString("cep"),
                                IdCidade = reader.IsDBNull(reader.GetOrdinal("id_cidade")) ? (int?)null : reader.GetInt32("id_cidade"),
                                NomeCidade = reader.IsDBNull(reader.GetOrdinal("cidade_nome")) ? null : reader.GetString("cidade_nome"),
                                Tipo = reader.GetString("tipo"),
                                Genero = reader.IsDBNull(reader.GetOrdinal("genero")) ? null : reader.GetString("genero"),
                                IdCondicao = reader.IsDBNull(reader.GetOrdinal("id_condicao_pagamento")) ? (int?)null : reader.GetInt32("id_condicao_pagamento"),
                                DescricaoCondicao = reader.IsDBNull(reader.GetOrdinal("condicao_descricao")) ? null : reader.GetString("condicao_descricao"),
                                Ativo = reader.GetBoolean("ativo"),
                                DataCadastro = reader.IsDBNull(reader.GetOrdinal("data_cadastro")) ? (DateTime?)null : reader.GetDateTime("data_cadastro"),
                                DataAlteracao = reader.IsDBNull(reader.GetOrdinal("data_alteracao")) ? (DateTime?)null : reader.GetDateTime("data_alteracao")
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
                SELECT c.id, c.cliente, c.cpf_cnpj, c.rg, c.telefone, c.email, c.endereco, c.numero_endereco,
                       c.complemento, c.bairro, c.cep, c.id_cidade, ci.cidade AS cidade_nome,
                       c.tipo, c.genero, c.id_condicao_pagamento, cp.descricao AS condicao_descricao, c.ativo,
                       c.data_cadastro, c.data_alteracao
                FROM clientes c
                LEFT JOIN cidades ci ON c.id_cidade = ci.id
                LEFT JOIN condicoes_pagamento cp ON c.id_condicao_pagamento = cp.id
                ORDER BY c.id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Cliente
                            {
                                Id = reader.GetInt32("id"),
                                Nome = reader.GetString("cliente"),
                                CPF_CNPJ = reader.GetString("cpf_cnpj"),
                                Rg = reader.IsDBNull(reader.GetOrdinal("rg")) ? null : reader.GetString("rg"),
                                Telefone = reader.IsDBNull(reader.GetOrdinal("telefone")) ? null : reader.GetString("telefone"),
                                Email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString("email"),
                                Endereco = reader.IsDBNull(reader.GetOrdinal("endereco")) ? null : reader.GetString("endereco"),
                                NumeroEndereco = reader.IsDBNull(reader.GetOrdinal("numero_endereco")) ? (int?)null : reader.GetInt32("numero_endereco"),
                                Complemento = reader.IsDBNull(reader.GetOrdinal("complemento")) ? null : reader.GetString("complemento"),
                                Bairro = reader.IsDBNull(reader.GetOrdinal("bairro")) ? null : reader.GetString("bairro"),
                                CEP = reader.IsDBNull(reader.GetOrdinal("cep")) ? null : reader.GetString("cep"),
                                IdCidade = reader.IsDBNull(reader.GetOrdinal("id_cidade")) ? (int?)null : reader.GetInt32("id_cidade"),
                                NomeCidade = reader.IsDBNull(reader.GetOrdinal("cidade_nome")) ? null : reader.GetString("cidade_nome"),
                                Tipo = reader.GetString("tipo"),
                                Genero = reader.IsDBNull(reader.GetOrdinal("genero")) ? null : reader.GetString("genero"),
                                IdCondicao = reader.IsDBNull(reader.GetOrdinal("id_condicao_pagamento")) ? (int?)null : reader.GetInt32("id_condicao_pagamento"),
                                DescricaoCondicao = reader.IsDBNull(reader.GetOrdinal("condicao_descricao")) ? null : reader.GetString("condicao_descricao"),
                                Ativo = reader.GetBoolean("ativo"),
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
