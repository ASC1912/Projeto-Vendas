using MySql.Data.MySqlClient;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Projeto.DAO
{
    internal class DAOParcela
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString;

        public void SalvarParcelas(List<Parcelamento> parcelas, int condicaoPagamentoId)
        {
            using (var conexao = new MySqlConnection(connectionString))
            {
                conexao.Open();

                RemoverParcelasExtras(condicaoPagamentoId, parcelas.Count);

                string query = @"
                    INSERT INTO Parcelamentos (NumeroParcela, CondicaoPagamentoId, FormaPagamentoId, PrazoDias, PorcentagemValor) 
                    VALUES (@NumeroParcela, @CondicaoPagamentoId, @FormaPagamentoId, @PrazoDias, @PorcentagemValor)
                    ON DUPLICATE KEY UPDATE
                    FormaPagamentoId = VALUES(FormaPagamentoId),
                    PrazoDias = VALUES(PrazoDias),
                    PorcentagemValor = VALUES(PorcentagemValor)";

                using (var cmd = new MySqlCommand(query, conexao))
                {
                    foreach (var parcela in parcelas)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@NumeroParcela", parcela.NumParcela);
                        cmd.Parameters.AddWithValue("@CondicaoPagamentoId", condicaoPagamentoId);
                        cmd.Parameters.AddWithValue("@FormaPagamentoId", parcela.FormaPagamentoId);
                        cmd.Parameters.AddWithValue("@PrazoDias", parcela.PrazoDias);
                        cmd.Parameters.AddWithValue("@PorcentagemValor", parcela.Porcentagem);

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

                    string query = @"
                        SELECT 
                            p.NumeroParcela, 
                            p.PrazoDias, 
                            p.PorcentagemValor, 
                            p.CondicaoPagamentoId, 
                            p.FormaPagamentoId,
                            fp.Descricao AS FormaPagamentoDescricao
                        FROM Parcelamentos p
                        JOIN FormasPagamento fp ON p.FormaPagamentoId = fp.Id
                        WHERE p.CondicaoPagamentoId = @CondicaoPagamentoId";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CondicaoPagamentoId", condicaoId);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Parcelamento parcela = new Parcelamento
                                {
                                    NumParcela = reader.GetInt32("NumeroParcela"),
                                    PrazoDias = reader.GetInt32("PrazoDias"),
                                    Porcentagem = reader.GetDecimal("PorcentagemValor"),
                                    CondicaoId = reader.GetInt32("CondicaoPagamentoId"),
                                    FormaPagamentoId = reader.GetInt32("FormaPagamentoId"),
                                    FormaPagamento = new FormaPagamento
                                    {
                                        Id = reader.GetInt32("FormaPagamentoId"),
                                        Descricao = reader.GetString("FormaPagamentoDescricao")
                                    }
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
                string query = @"DELETE FROM Parcelamentos WHERE CondicaoPagamentoId = @CondicaoPagamentoId AND NumeroParcela > @QtdParcelasAtuais";

                using (var cmd = new MySqlCommand(query, conexao))
                {
                    cmd.Parameters.AddWithValue("@CondicaoPagamentoId", condicaoPagamentoId);
                    cmd.Parameters.AddWithValue("@QtdParcelasAtuais", qtdParcelasAtuais);
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}