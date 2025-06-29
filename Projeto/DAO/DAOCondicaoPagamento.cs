using MySql.Data.MySqlClient;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Projeto.DAO
{
    internal class DAOCondicaoPagamento
    {
        private string connectionString = "Server=localhost;Database=sistema;Uid=root;Pwd=12345678;";

        public int SalvarCondicaoPagamento(CondicaoPagamento condicao)
        {
            int condicaoId = 0;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query;

                    if (condicao.Id > 0)
                    {
                        query = @"
                        UPDATE condicoes_pagamento SET 
                            descricao = @descricao, qtd_parcelas = @qtd_parcelas, juros = @juros, 
                            multa = @multa, desconto = @desconto, ativo = @ativo, 
                            data_alteracao = @data_alteracao
                        WHERE id = @id";
                    }
                    else
                    {
                        query = @"
                        INSERT INTO condicoes_pagamento (
                            descricao, qtd_parcelas, juros, multa, desconto, ativo, 
                            data_cadastro, data_alteracao
                        )
                        VALUES (
                            @descricao, @qtd_parcelas, @juros, @multa, @desconto, @ativo, 
                            @data_cadastro, @data_alteracao
                        )";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@descricao", condicao.Descricao);
                        cmd.Parameters.AddWithValue("@qtd_parcelas", condicao.QtdParcelas);
                        cmd.Parameters.AddWithValue("@juros", condicao.Juros);
                        cmd.Parameters.AddWithValue("@multa", condicao.Multa);
                        cmd.Parameters.AddWithValue("@desconto", condicao.Desconto);
                        cmd.Parameters.AddWithValue("@ativo", condicao.Ativo);
                        cmd.Parameters.AddWithValue("@data_alteracao", condicao.DataAlteracao ?? DateTime.Now);

                        if (condicao.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@id", condicao.Id);
                            cmd.ExecuteNonQuery();
                            condicaoId = condicao.Id;
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@data_cadastro", condicao.DataCadastro ?? DateTime.Now);
                            cmd.ExecuteNonQuery();
                            condicaoId = (int)cmd.LastInsertedId;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar condição de pagamento: " + ex.Message);
            }

            return condicaoId;
        }

        public void Excluir(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM condicoes_pagamento WHERE id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public CondicaoPagamento BuscarPorId(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                SELECT id, descricao, qtd_parcelas, juros, multa, desconto, ativo,
                       data_cadastro, data_alteracao
                FROM condicoes_pagamento
                WHERE id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new CondicaoPagamento
                            {
                                Id = reader.GetInt32("id"),
                                Descricao = reader.GetString("descricao"),
                                QtdParcelas = reader.GetInt32("qtd_parcelas"),
                                Juros = reader.GetDecimal("juros"),
                                Multa = reader.GetDecimal("multa"),
                                Desconto = reader.GetDecimal("desconto"),
                                Ativo = reader.GetBoolean("ativo"),
                                DataCadastro = reader.IsDBNull(reader.GetOrdinal("data_cadastro")) ? (DateTime?)null : reader.GetDateTime("data_cadastro"),
                                DataAlteracao = reader.IsDBNull(reader.GetOrdinal("data_alteracao")) ? (DateTime?)null : reader.GetDateTime("data_alteracao"),
                            };
                        }
                    }
                }
            }
            return null;
        }

        public List<CondicaoPagamento> ListarCondicaoPagamento()
        {
            List<CondicaoPagamento> lista = new List<CondicaoPagamento>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                SELECT id, descricao, qtd_parcelas, juros, multa, desconto, ativo,
                       data_cadastro, data_alteracao
                FROM condicoes_pagamento
                ORDER BY id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new CondicaoPagamento
                            {
                                Id = reader.GetInt32("id"),
                                Descricao = reader.GetString("descricao"),
                                QtdParcelas = reader.GetInt32("qtd_parcelas"),
                                Juros = reader.GetDecimal("juros"),
                                Multa = reader.GetDecimal("multa"),
                                Desconto = reader.GetDecimal("desconto"),
                                Ativo = reader.GetBoolean("ativo"),
                                DataCadastro = reader.IsDBNull(reader.GetOrdinal("data_cadastro")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("data_cadastro")),
                                DataAlteracao = reader.IsDBNull(reader.GetOrdinal("data_alteracao")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("data_alteracao"))
                            });
                        }
                    }
                }
            }

            return lista;
        }
    }
}
