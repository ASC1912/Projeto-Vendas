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
                        UPDATE transportadoras 
                        SET nome = @nome, cpf_cnpj = @cpf_cnpj, telefone = @telefone, email = @email, 
                            endereco = @endereco, numero_endereco = @numero_endereco, complemento = @complemento, 
                            bairro = @bairro, cep = @cep, inscricao_estadual = @inscricao_estadual, 
                            inscricao_estadual_subtrib = @inscricao_estadual_subtrib, id_cidade = @id_cidade, 
                            tipo = @tipo, id_condicao_pagamento = @id_condicao_pagamento, status = @status,
                            data_modificacao = @data_modificacao
                        WHERE id = @id";
                    }
                    else
                    {
                        query = @"
                        INSERT INTO transportadoras (
                            nome, cpf_cnpj, telefone, email, endereco, numero_endereco, complemento, 
                            bairro, cep, inscricao_estadual, inscricao_estadual_subtrib, id_cidade, tipo, 
                            id_condicao_pagamento, status, data_criacao, data_modificacao
                        ) 
                        VALUES (
                            @nome, @cpf_cnpj, @telefone, @email, @endereco, @numero_endereco, @complemento, 
                            @bairro, @cep, @inscricao_estadual, @inscricao_estadual_subtrib, @id_cidade, @tipo, 
                            @id_condicao_pagamento, @status, @data_criacao, @data_modificacao
                        )";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nome", transportadora.Nome);
                        cmd.Parameters.AddWithValue("@cpf_cnpj", transportadora.CPF_CNPJ);
                        cmd.Parameters.AddWithValue("@telefone", transportadora.Telefone);
                        cmd.Parameters.AddWithValue("@email", transportadora.Email);
                        cmd.Parameters.AddWithValue("@endereco", transportadora.Endereco);
                        cmd.Parameters.AddWithValue("@numero_endereco", transportadora.NumeroEndereco);
                        cmd.Parameters.AddWithValue("@complemento", transportadora.Complemento);
                        cmd.Parameters.AddWithValue("@bairro", transportadora.Bairro);
                        cmd.Parameters.AddWithValue("@cep", transportadora.CEP);
                        cmd.Parameters.AddWithValue("@inscricao_estadual", transportadora.InscricaoEstadual);
                        cmd.Parameters.AddWithValue("@inscricao_estadual_subtrib", transportadora.InscricaoEstadualSubTrib);
                        cmd.Parameters.AddWithValue("@id_cidade", transportadora.IdCidade);
                        cmd.Parameters.AddWithValue("@tipo", transportadora.Tipo);
                        cmd.Parameters.AddWithValue("@id_condicao_pagamento", transportadora.IdCondicao > 0 ? (object)transportadora.IdCondicao : DBNull.Value);
                        cmd.Parameters.AddWithValue("@status", transportadora.Status);
                        cmd.Parameters.AddWithValue("@data_modificacao", transportadora.DataModificacao);

                        if (transportadora.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@id", transportadora.Id);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@data_criacao", transportadora.DataCriacao);
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
                string query = "DELETE FROM transportadoras WHERE id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
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
                    SELECT t.id, t.nome, t.cpf_cnpj, t.telefone, t.email, t.endereco, t.numero_endereco, 
                           t.complemento, t.bairro, t.cep, t.inscricao_estadual, t.inscricao_estadual_subtrib, 
                           t.id_cidade, ci.nome AS cidade_nome, t.tipo, t.id_condicao_pagamento, 
                           t.status, t.data_criacao, t.data_modificacao
                    FROM transportadoras t
                    LEFT JOIN cidades ci ON t.id_cidade = ci.id
                    WHERE t.id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Transportadora
                            {
                                Id = reader.GetInt32("id"),
                                Nome = reader.GetString("nome"),
                                CPF_CNPJ = reader.GetString("cpf_cnpj"),
                                Telefone = reader.IsDBNull(reader.GetOrdinal("telefone")) ? null : reader.GetString("telefone"),
                                Email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString("email"),
                                Endereco = reader.IsDBNull(reader.GetOrdinal("endereco")) ? null : reader.GetString("endereco"),
                                NumeroEndereco = reader.IsDBNull(reader.GetOrdinal("numero_endereco")) ? 0 : reader.GetInt32("numero_endereco"),
                                Complemento = reader.IsDBNull(reader.GetOrdinal("complemento")) ? null : reader.GetString("complemento"),
                                Bairro = reader.IsDBNull(reader.GetOrdinal("bairro")) ? null : reader.GetString("bairro"),
                                CEP = reader.IsDBNull(reader.GetOrdinal("cep")) ? null : reader.GetString("cep"),
                                InscricaoEstadual = reader.IsDBNull(reader.GetOrdinal("inscricao_estadual")) ? null : reader.GetString("inscricao_estadual"),
                                InscricaoEstadualSubTrib = reader.IsDBNull(reader.GetOrdinal("inscricao_estadual_subtrib")) ? null : reader.GetString("inscricao_estadual_subtrib"),
                                IdCidade = reader.IsDBNull(reader.GetOrdinal("id_cidade")) ? (int?)null : reader.GetInt32("id_cidade"),
                                NomeCidade = reader.IsDBNull(reader.GetOrdinal("cidade_nome")) ? null : reader.GetString("cidade_nome"),
                                Tipo = reader.GetString("tipo"),
                                IdCondicao = reader.IsDBNull(reader.GetOrdinal("id_condicao_pagamento")) ? (int?)null : reader.GetInt32("id_condicao_pagamento"),
                                Status = reader.GetBoolean("status"),
                                DataCriacao = reader.IsDBNull(reader.GetOrdinal("data_criacao")) ? (DateTime?)null : reader.GetDateTime("data_criacao"),
                                DataModificacao = reader.IsDBNull(reader.GetOrdinal("data_modificacao")) ? (DateTime?)null : reader.GetDateTime("data_modificacao")
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
                    SELECT t.id, t.nome, t.cpf_cnpj, t.telefone, t.email, t.endereco, t.numero_endereco, 
                           t.complemento, t.bairro, t.cep, t.inscricao_estadual, t.inscricao_estadual_subtrib, 
                           t.id_cidade, ci.nome AS cidade_nome, t.tipo, t.id_condicao_pagamento, 
                           t.status, t.data_criacao, t.data_modificacao
                    FROM transportadoras t
                    LEFT JOIN cidades ci ON t.id_cidade = ci.id
                    ORDER BY t.nome";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Transportadora
                            {
                                Id = reader.GetInt32("id"),
                                Nome = reader.GetString("nome"),
                                CPF_CNPJ = reader.GetString("cpf_cnpj"),
                                Telefone = reader.IsDBNull(reader.GetOrdinal("telefone")) ? null : reader.GetString("telefone"),
                                Email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString("email"),
                                Endereco = reader.IsDBNull(reader.GetOrdinal("endereco")) ? null : reader.GetString("endereco"),
                                NumeroEndereco = reader.IsDBNull(reader.GetOrdinal("numero_endereco")) ? 0 : reader.GetInt32("numero_endereco"),
                                Complemento = reader.IsDBNull(reader.GetOrdinal("complemento")) ? null : reader.GetString("complemento"),
                                Bairro = reader.IsDBNull(reader.GetOrdinal("bairro")) ? null : reader.GetString("bairro"),
                                CEP = reader.IsDBNull(reader.GetOrdinal("cep")) ? null : reader.GetString("cep"),
                                InscricaoEstadual = reader.IsDBNull(reader.GetOrdinal("inscricao_estadual")) ? null : reader.GetString("inscricao_estadual"),
                                InscricaoEstadualSubTrib = reader.IsDBNull(reader.GetOrdinal("inscricao_estadual_subtrib")) ? null : reader.GetString("inscricao_estadual_subtrib"),
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
