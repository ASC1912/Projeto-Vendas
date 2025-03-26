﻿using MySql.Data.MySqlClient;
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
                        query = "UPDATE condicoes_pagamento SET descricao = @descricao, qtd_parcelas = @qtd_parcelas WHERE id = @id";
                    }
                    else
                    {
                        query = "INSERT INTO condicoes_pagamento (descricao, qtd_parcelas) VALUES (@descricao, @qtd_parcelas); SELECT LAST_INSERT_ID();";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (condicao.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@id", condicao.Id);
                        }

                        cmd.Parameters.AddWithValue("@descricao", condicao.Descricao);
                        cmd.Parameters.AddWithValue("@qtd_parcelas", condicao.QtdParcelas);

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

        /*
        public void Salvar(CondicaoPagamento condPagamento)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query;

                    if (condPagamento.Id > 0)
                    {
                        query = "UPDATE condicoes_pagamento SET descricao = @descricao, qtd_parcelas = @qtd_parcelas WHERE id = @id";
                    }
                    else
                    {
                        query = "INSERT INTO condicoes_pagamento (descricao, qtd_parcelas) VALUES (@descricao, @qtd_parcelas)";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (condPagamento.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@id", condPagamento.Id);
                        }

                        cmd.Parameters.AddWithValue("@descricao", condPagamento.Descricao);
                        cmd.Parameters.AddWithValue("@qtd_parcelas", condPagamento.QtdParcelas);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar condição de pagamento: " + ex.Message);
            }
        }
        */
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

        public List<CondicaoPagamento> ListarCondicaoPagamento()
        {
            List<CondicaoPagamento> lista = new List<CondicaoPagamento>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT id, descricao, qtd_parcelas FROM condicoes_pagamento ORDER BY id";

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
                                QtdParcelas = reader.GetInt32("qtd_parcelas")
                            });
                        }
                    }
                }
            }
            return lista;
        }

        


    }
}
