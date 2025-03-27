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

        public void SalvarParcelas(List<Parcelamento> parcelas, int condicaoPagamentoId)
        {
            using (var conexao = new MySqlConnection(connectionString))
            {
                conexao.Open();

                RemoverParcelasExtras(condicaoPagamentoId, parcelas.Count);

                string query = @"
                    INSERT INTO parcelamento (numero_parcela, condicao_pagamento_id, forma_pagamento_id, prazo_dias, porcentagem) 
                    VALUES (@numeroParcela, @condicaoPagamentoId, @formaPagamentoId, @prazoDias, @porcentagem)
                    ON DUPLICATE KEY UPDATE
                    forma_pagamento_id = VALUES(forma_pagamento_id),
                    prazo_dias = VALUES(prazo_dias),
                    porcentagem = VALUES(porcentagem)";

                using (var cmd = new MySqlCommand(query, conexao))
                {
                    foreach (var parcela in parcelas)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@numeroParcela", parcela.NumParcela);
                        cmd.Parameters.AddWithValue("@condicaoPagamentoId", condicaoPagamentoId);
                        cmd.Parameters.AddWithValue("@formaPagamentoId", parcela.FormaPagamentoId);
                        cmd.Parameters.AddWithValue("@prazoDias", parcela.PrazoDias);
                        cmd.Parameters.AddWithValue("@porcentagem", parcela.Porcentagem);
                        cmd.ExecuteNonQuery();
                    }
                }
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


        public void RemoverParcelasExtras(int condicaoPagamentoId, int qtdParcelasAtuais)
        {
            using (var conexao = new MySqlConnection(connectionString))
            {
                conexao.Open();
                string query = @"DELETE FROM parcelamento WHERE condicao_pagamento_id = @condicaoPagamentoId AND numero_parcela > @qtdParcelasAtuais";

                using (var cmd = new MySqlCommand(query, conexao))
                {
                    cmd.Parameters.AddWithValue("@condicaoPagamentoId", condicaoPagamentoId);
                    cmd.Parameters.AddWithValue("@qtdParcelasAtuais", qtdParcelasAtuais);
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
