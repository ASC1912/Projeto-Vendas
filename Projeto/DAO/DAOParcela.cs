using MySql.Data.MySqlClient;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.DAO
{
    internal class DAOParcela
    {
        private string connectionString = "Server=localhost;Database=sistema;Uid=root;Pwd=12345678;";

        public void SalvarParcelas(List<Parcelamento> parcelas, int condicaoId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    foreach (var parcela in parcelas)
                    {
                        string query = "INSERT INTO parcelamento (numero_parcela, condicao_pagamento_id, prazo_dias, porcentagem, forma_pagamento_id) " +
                                       "VALUES (@numero_parcela, @condicao_pagamento_id, @prazo_dias, @porcentagem, @forma_pagamento_id)";

                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@numero_parcela", parcela.NumParcela);
                            cmd.Parameters.AddWithValue("@condicao_pagamento_id", condicaoId);
                            cmd.Parameters.AddWithValue("@prazo_dias", parcela.PrazoDias);
                            cmd.Parameters.AddWithValue("@porcentagem", parcela.Porcentagem);
                            cmd.Parameters.AddWithValue("@forma_pagamento_id", parcela.FormaPagamentoId);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar parcelas: " + ex.Message);
            }
        }


        public void EditarParcelas(Parcelamento parcela, int condicaoId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            string query = "UPDATE parcelamento " +
                                           "SET prazo_dias = @prazo_dias, porcentagem = @porcentagem, forma_pagamento_id = @forma_pagamento_id " +
                                           "WHERE numero_parcela = @numero_parcela AND condicao_pagamento_id = @condicao_pagamento_id";

                            using (MySqlCommand cmd = new MySqlCommand(query, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@numero_parcela", parcela.NumParcela);
                                cmd.Parameters.AddWithValue("@condicao_pagamento_id", condicaoId);
                                cmd.Parameters.AddWithValue("@prazo_dias", parcela.PrazoDias);
                                cmd.Parameters.AddWithValue("@porcentagem", parcela.Porcentagem);
                                cmd.Parameters.AddWithValue("@forma_pagamento_id", parcela.FormaPagamentoId);

                                cmd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw new Exception("Erro ao editar parcela: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao conectar ao banco de dados: " + ex.Message);
            }
        }

        public List<Parcelamento> ListarParcelas(int condicaoId)
        {
            List<Parcelamento> parcelas = new List<Parcelamento>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT numero_parcela, prazo_dias, porcentagem, condicao_pagamento_id, forma_pagamento_id " +
                                   "FROM parcelamento WHERE condicao_pagamento_id = @condicao_pagamento_id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@condicao_pagamento_id", condicaoId);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Parcelamento parcela = new Parcelamento
                                {
                                    NumParcela = reader.GetInt32("numero_parcela"),
                                    PrazoDias = reader.GetInt32("prazo_dias"),
                                    Porcentagem = reader.GetDecimal("porcentagem"),
                                    CondicaoId = reader.GetInt32("condicao_pagamento_id"),
                                    FormaPagamentoId = reader.GetInt32("forma_pagamento_id")
                                };

                                parcelas.Add(parcela);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar parcelas: " + ex.Message);
            }

            return parcelas;
        }
    }
}
