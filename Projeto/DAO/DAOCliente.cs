using MySql.Data.MySqlClient;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                        nome = @nome, cpf_cnpj = @cpf_cnpj, rg = @rg, telefone = @telefone, email = @email,
                        endereco = @endereco, numero_endereco = @numero_endereco, complemento = @complemento,
                        bairro = @bairro, cep = @cep, id_cidade = @id_cidade, tipo = @tipo, 
                        id_condicao_pagamento = @id_condicao_pagamento, status = @status,
                        data_modificacao = @data_modificacao
                    WHERE id = @id";
                    }
                    else
                    {
                        query = @"
                    INSERT INTO clientes (
                        nome, cpf_cnpj, rg, telefone, email, endereco, numero_endereco, complemento,
                        bairro, cep, id_cidade, tipo, id_condicao_pagamento, status,
                        data_criacao, data_modificacao
                    ) VALUES (
                        @nome, @cpf_cnpj, @rg, @telefone, @email, @endereco, @numero_endereco, @complemento,
                        @bairro, @cep, @id_cidade, @tipo, @id_condicao_pagamento, @status,
                        @data_criacao, @data_modificacao
                    )";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nome", cliente.Nome);
                        cmd.Parameters.AddWithValue("@cpf_cnpj", cliente.CPF_CNPJ);
                        cmd.Parameters.AddWithValue("@rg", cliente.Rg);
                        cmd.Parameters.AddWithValue("@telefone", cliente.Telefone);
                        cmd.Parameters.AddWithValue("@email", cliente.Email);
                        cmd.Parameters.AddWithValue("@endereco", cliente.Endereco);
                        cmd.Parameters.AddWithValue("@numero_endereco", cliente.NumeroEndereco);
                        cmd.Parameters.AddWithValue("@complemento", cliente.Complemento);
                        cmd.Parameters.AddWithValue("@bairro", cliente.Bairro);
                        cmd.Parameters.AddWithValue("@cep", cliente.CEP);
                        cmd.Parameters.AddWithValue("@id_cidade", cliente.IdCidade);
                        cmd.Parameters.AddWithValue("@tipo", cliente.Tipo);
                        cmd.Parameters.AddWithValue("@id_condicao_pagamento", cliente.IdCondicao > 0 ? (object)cliente.IdCondicao : DBNull.Value);
                        cmd.Parameters.AddWithValue("@status", cliente.Status);
                        cmd.Parameters.AddWithValue("@data_modificacao", cliente.DataModificacao);

                        if (cliente.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@id", cliente.Id);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@data_criacao", cliente.DataCriacao);
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
                                SELECT c.id, c.nome, c.cpf_cnpj, c.rg, c.telefone, c.email, c.endereco, c.numero_endereco, 
                                       c.complemento, c.bairro, c.cep, c.id_cidade, ci.nome AS cidade_nome, 
                                       c.tipo, c.id_condicao_pagamento, c.status, c.data_criacao, c.data_modificacao
                                FROM clientes c
                                LEFT JOIN cidades ci ON c.id_cidade = ci.id
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
                                Nome = reader.GetString("nome"),
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
                                IdCondicao = reader.IsDBNull(reader.GetOrdinal("id_condicao_pagamento")) ? (int?)null : reader.GetInt32("id_condicao_pagamento"),
                                Status = reader.GetBoolean("status"),
                                DataCriacao = reader.GetDateTime("data_criacao"),
                                DataModificacao = reader.GetDateTime("data_modificacao")
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
                SELECT c.id, c.nome, c.cpf_cnpj, c.rg, c.telefone, c.email, c.endereco, c.numero_endereco, 
                       c.complemento, c.bairro, c.cep, c.id_cidade, ci.nome AS cidade_nome, 
                       c.tipo, c.id_condicao_pagamento, c.status,
                       c.data_criacao, c.data_modificacao
                FROM clientes c
                LEFT JOIN cidades ci ON c.id_cidade = ci.id
                ORDER BY c.nome";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Cliente
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
                                IdCidade = reader.IsDBNull(reader.GetOrdinal("id_cidade")) ? (int?)null : reader.GetInt32("id_cidade"),
                                NomeCidade = reader.IsDBNull(reader.GetOrdinal("cidade_nome")) ? null : reader.GetString("cidade_nome"),
                                Tipo = reader.GetString("tipo"),
                                IdCondicao = reader.IsDBNull(reader.GetOrdinal("id_condicao_pagamento")) ? (int?)null : reader.GetInt32("id_condicao_pagamento"),
                                Status = reader.GetBoolean("status"),
                                DataCriacao = reader.IsDBNull(reader.GetOrdinal("data_criacao")) ? (DateTime?)null : reader.GetDateTime("data_criacao"),
                                DataModificacao = reader.IsDBNull(reader.GetOrdinal("data_modificacao")) ? (DateTime?)null : reader.GetDateTime("data_modificacao")
                            });
                        }
                    }
                }
            }
            return lista;
        }



    }
}
