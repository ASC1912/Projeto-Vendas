using MySql.Data.MySqlClient;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                            multa = @multa, desconto = @desconto, status = @status, 
                            data_modificacao = @data_modificacao
                        WHERE id = @id";
                    }
                    else
                    {
                        query = @"
                        INSERT INTO condicoes_pagamento (
                            descricao, qtd_parcelas, juros, multa, desconto, status, 
                            data_criacao, data_modificacao
                        )
                        VALUES (
                            @descricao, @qtd_parcelas, @juros, @multa, @desconto, @status, 
                            @data_criacao, @data_modificacao
                        )";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@descricao", condicao.Descricao);
                        cmd.Parameters.AddWithValue("@qtd_parcelas", condicao.QtdParcelas);
                        cmd.Parameters.AddWithValue("@juros", condicao.Juros);
                        cmd.Parameters.AddWithValue("@multa", condicao.Multa);
                        cmd.Parameters.AddWithValue("@desconto", condicao.Desconto);
                        cmd.Parameters.AddWithValue("@status", condicao.Status);
                        cmd.Parameters.AddWithValue("@data_modificacao", condicao.DataModificacao);

                        if (condicao.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@id", condicao.Id);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@data_criacao", condicao.DataCriacao);
                        }

                        if (condicao.Id > 0)
                        {
                            cmd.ExecuteNonQuery();
                            condicaoId = condicao.Id;
                        }
                        else
                        {
                            condicaoId = Convert.ToInt32(cmd.ExecuteScalar());
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
            SELECT id, descricao, qtd_parcelas, juros, multa, desconto, status,
                   data_criacao, data_modificacao
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
                                Status = reader.GetBoolean("status"),
                                DataCriacao = reader.GetDateTime("data_criacao"),
                                DataModificacao = reader.GetDateTime("data_modificacao"),
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
                SELECT id, descricao, qtd_parcelas, juros, multa, desconto, status,
                       data_criacao, data_modificacao
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
