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
                        UPDATE fornecedores SET 
                            fornecedor = @fornecedor, cpf_cnpj = @cpf_cnpj, telefone = @telefone, email = @email,
                            endereco = @endereco, numero_endereco = @numero_endereco, complemento = @complemento,
                            bairro = @bairro, cep = @cep, id_cidade = @id_cidade, tipo = @tipo,
                            inscricao_estadual = @inscricao_estadual, inscricao_estadual_subtrib = @inscricao_estadual_subtrib,
                            id_condicao_pagamento = @id_condicao_pagamento,
                            ativo = @ativo, data_alteracao = @data_alteracao
                        WHERE id = @id";
                    }
                    else
                    {
                        query = @"
                        INSERT INTO fornecedores (
                            fornecedor, cpf_cnpj, telefone, email, endereco, numero_endereco, complemento,
                            bairro, cep, id_cidade, tipo, inscricao_estadual, inscricao_estadual_subtrib, id_condicao_pagamento,
                            ativo, data_cadastro, data_alteracao
                        ) VALUES (
                            @fornecedor, @cpf_cnpj, @telefone, @email, @endereco, @numero_endereco, @complemento,
                            @bairro, @cep, @id_cidade, @tipo, @inscricao_estadual, @inscricao_estadual_subtrib, @id_condicao_pagamento,
                            @ativo, @data_cadastro, @data_alteracao
                        )";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@fornecedor", fornecedor.Nome);
                        cmd.Parameters.AddWithValue("@cpf_cnpj", fornecedor.CPF_CNPJ);
                        cmd.Parameters.AddWithValue("@telefone", fornecedor.Telefone ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@email", fornecedor.Email ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@endereco", fornecedor.Endereco ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@numero_endereco", fornecedor.NumeroEndereco ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@complemento", fornecedor.Complemento ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@bairro", fornecedor.Bairro ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@cep", fornecedor.CEP ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@id_cidade", fornecedor.IdCidade ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@tipo", fornecedor.Tipo ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@inscricao_estadual", fornecedor.InscricaoEstadual ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@inscricao_estadual_subtrib", fornecedor.InscricaoEstadualSubTrib ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@id_condicao_pagamento", fornecedor.IdCondicao ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@ativo", fornecedor.Ativo);
                        cmd.Parameters.AddWithValue("@data_alteracao", fornecedor.DataAlteracao ?? DateTime.Now);

                        if (fornecedor.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@id", fornecedor.Id);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@data_cadastro", fornecedor.DataCadastro ?? DateTime.Now);
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
                string query = "DELETE FROM fornecedores WHERE id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
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
                SELECT f.id, f.fornecedor, f.cpf_cnpj, f.telefone, f.email, f.endereco, f.numero_endereco,
                       f.complemento, f.bairro, f.cep, f.id_cidade, ci.cidade AS cidade_nome,
                       f.tipo, f.inscricao_estadual, f.inscricao_estadual_subtrib, f.id_condicao_pagamento,
                       cp.descricao AS condicao_descricao,
                       f.ativo, f.data_cadastro, f.data_alteracao
                FROM fornecedores f
                LEFT JOIN cidades ci ON f.id_cidade = ci.id
                LEFT JOIN condicoes_pagamento cp ON f.id_condicao_pagamento = cp.id
                WHERE f.id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Fornecedor
                            {
                                Id = reader.GetInt32("id"),
                                Nome = reader.GetString("fornecedor"),
                                CPF_CNPJ = reader.GetString("cpf_cnpj"),
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
                                InscricaoEstadual = reader.IsDBNull(reader.GetOrdinal("inscricao_estadual")) ? null : reader.GetString("inscricao_estadual"),
                                InscricaoEstadualSubTrib = reader.IsDBNull(reader.GetOrdinal("inscricao_estadual_subtrib")) ? null : reader.GetString("inscricao_estadual_subtrib"),
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

        public List<Fornecedor> ListarFornecedores()
        {
            List<Fornecedor> lista = new List<Fornecedor>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                SELECT f.id, f.fornecedor, f.cpf_cnpj, f.telefone, f.email, f.endereco, f.numero_endereco,
                       f.complemento, f.bairro, f.cep, f.id_cidade, ci.cidade AS cidade_nome,
                       f.tipo, f.inscricao_estadual, f.inscricao_estadual_subtrib, f.id_condicao_pagamento,
                       cp.descricao AS condicao_descricao,
                       f.ativo, f.data_cadastro, f.data_alteracao
                FROM fornecedores f
                LEFT JOIN cidades ci ON f.id_cidade = ci.id
                LEFT JOIN condicoes_pagamento cp ON f.id_condicao_pagamento = cp.id
                ORDER BY f.id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Fornecedor
                            {
                                Id = reader.GetInt32("id"),
                                Nome = reader.GetString("fornecedor"),
                                CPF_CNPJ = reader.GetString("cpf_cnpj"),
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
                                InscricaoEstadual = reader.IsDBNull(reader.GetOrdinal("inscricao_estadual")) ? null : reader.GetString("inscricao_estadual"),
                                InscricaoEstadualSubTrib = reader.IsDBNull(reader.GetOrdinal("inscricao_estadual_subtrib")) ? null : reader.GetString("inscricao_estadual_subtrib"),
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
