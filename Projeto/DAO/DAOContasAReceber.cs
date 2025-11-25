using MySql.Data.MySqlClient;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Projeto.DAO
{
    internal class DAOContasAReceber
    {
        // Sua string de conexão padrão
        private string connectionString = "Server=localhost;Database=sistema;Uid=root;Pwd=12345678;";

        // 1. SALVAR (INSERT)
        // Chamado automaticamente pelo DAOVenda quando a venda é finalizada
        public void Salvar(ContasAReceber conta, MySqlConnection conn, MySqlTransaction trans)
        {
            string query = @"
                INSERT INTO ContasAReceber (
                    VendaModelo, VendaSerie, VendaNumeroNota, NumeroParcela, ClienteId,
                    Descricao, DataEmissao, DataVencimento, ValorVencimento,
                    IdFormaPagamento, Status, Ativo,
                    Juros, Multa, Desconto,
                    DataCadastro, DataAlteracao
                ) VALUES (
                    @VendaModelo, @VendaSerie, @VendaNumeroNota, @NumeroParcela, @ClienteId,
                    @Descricao, @DataEmissao, @DataVencimento, @ValorVencimento,
                    @IdFormaPagamento, @Status, @Ativo,
                    @Juros, @Multa, @Desconto,
                    @DataCadastro, @DataAlteracao
                )";

            // Lógica para usar conexão existente (transação) ou abrir uma nova
            bool abrirConexao = (conn.State != System.Data.ConnectionState.Open);
            if (abrirConexao) conn.Open();

            using (MySqlCommand cmd = new MySqlCommand(query, conn, trans))
            {
                // Chave Primária
                cmd.Parameters.AddWithValue("@VendaModelo", conta.VendaModelo);
                cmd.Parameters.AddWithValue("@VendaSerie", conta.VendaSerie);
                cmd.Parameters.AddWithValue("@VendaNumeroNota", conta.VendaNumeroNota);
                cmd.Parameters.AddWithValue("@NumeroParcela", conta.NumeroParcela);
                cmd.Parameters.AddWithValue("@ClienteId", conta.ClienteId);

                // Dados
                cmd.Parameters.AddWithValue("@Descricao", conta.Descricao ?? "");
                cmd.Parameters.AddWithValue("@DataEmissao", conta.DataEmissao);
                cmd.Parameters.AddWithValue("@DataVencimento", conta.DataVencimento);
                cmd.Parameters.AddWithValue("@ValorVencimento", conta.ValorVencimento);

                cmd.Parameters.AddWithValue("@IdFormaPagamento", (object)conta.IdFormaPagamento ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", "Aberta");
                cmd.Parameters.AddWithValue("@Ativo", true);

                // Taxas
                cmd.Parameters.AddWithValue("@Juros", conta.Juros ?? 0);
                cmd.Parameters.AddWithValue("@Multa", conta.Multa ?? 0);
                cmd.Parameters.AddWithValue("@Desconto", conta.Desconto ?? 0);

                cmd.Parameters.AddWithValue("@DataCadastro", DateTime.Now);
                cmd.Parameters.AddWithValue("@DataAlteracao", DateTime.Now);

                cmd.ExecuteNonQuery();
            }

            if (abrirConexao) conn.Close();
        }

        // 2. LISTAR (SELECT)
        // Usado na tela de Consulta de Contas a Receber
        public List<ContasAReceber> Listar(List<string> statuses, string busca)
        {
            List<ContasAReceber> lista = new List<ContasAReceber>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = @"
                    SELECT cr.*, c.Nome AS NomeCliente, fp.Descricao AS NomeFormaPagamento
                    FROM ContasAReceber cr
                    JOIN Clientes c ON cr.ClienteId = c.Id
                    LEFT JOIN FormasPagamento fp ON cr.IdFormaPagamento = fp.Id";

                var whereClauses = new List<string>();

                // Busca por Nome do Cliente OU Número da Nota
                if (!string.IsNullOrEmpty(busca))
                {
                    whereClauses.Add("(c.Nome LIKE @Busca OR cr.VendaNumeroNota LIKE @Busca)");
                }

                // Filtro de Status
                if (statuses != null && statuses.Count > 0)
                {
                    var statusParams = new List<string>();
                    for (int i = 0; i < statuses.Count; i++)
                    {
                        statusParams.Add($"@Status{i}");
                    }
                    whereClauses.Add($"cr.Status IN ({string.Join(", ", statusParams)})");
                }
                else
                {
                    whereClauses.Add("1 = 0"); // Trava se não selecionar status
                }

                // Ordena por Vencimento
                query += $" WHERE {string.Join(" AND ", whereClauses)} ORDER BY cr.DataVencimento ASC";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    if (!string.IsNullOrEmpty(busca))
                        cmd.Parameters.AddWithValue("@Busca", $"%{busca}%");

                    if (statuses != null)
                    {
                        for (int i = 0; i < statuses.Count; i++)
                        {
                            cmd.Parameters.AddWithValue($"@Status{i}", statuses[i]);
                        }
                    }

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(MontarObjeto(reader));
                        }
                    }
                }
            }
            return lista;
        }

        // 3. RECEBER (UPDATE - DAR BAIXA)
        public void Receber(ContasAReceber conta)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    UPDATE ContasAReceber SET
                        IdFormaPagamento = @IdFormaPagamento,
                        DataPagamento = @DataPagamento,
                        ValorPago = @ValorPago,
                        Juros = @Juros,
                        Multa = @Multa,
                        Desconto = @Desconto,
                        Status = 'Paga',
                        DataAlteracao = @DataAlteracao
                    WHERE VendaModelo = @VendaModelo
                      AND VendaSerie = @VendaSerie
                      AND VendaNumeroNota = @VendaNumeroNota
                      AND ClienteId = @ClienteId
                      AND NumeroParcela = @NumeroParcela";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    // Dados do Pagamento
                    cmd.Parameters.AddWithValue("@IdFormaPagamento", conta.IdFormaPagamento);
                    cmd.Parameters.AddWithValue("@DataPagamento", conta.DataPagamento);
                    cmd.Parameters.AddWithValue("@ValorPago", conta.ValorPago);
                    cmd.Parameters.AddWithValue("@Juros", conta.Juros);
                    cmd.Parameters.AddWithValue("@Multa", conta.Multa);
                    cmd.Parameters.AddWithValue("@Desconto", conta.Desconto);
                    cmd.Parameters.AddWithValue("@DataAlteracao", DateTime.Now);

                    // Chave Primária (WHERE)
                    cmd.Parameters.AddWithValue("@VendaModelo", conta.VendaModelo);
                    cmd.Parameters.AddWithValue("@VendaSerie", conta.VendaSerie);
                    cmd.Parameters.AddWithValue("@VendaNumeroNota", conta.VendaNumeroNota);
                    cmd.Parameters.AddWithValue("@ClienteId", conta.ClienteId);
                    cmd.Parameters.AddWithValue("@NumeroParcela", conta.NumeroParcela);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // 4. CANCELAR POR VENDA
        // Usado pelo DAOVenda para cancelar todas as contas quando a venda é cancelada
        public void CancelarContasPorVenda(Venda venda, MySqlConnection conn, MySqlTransaction trans)
        {
            string query = @"
                UPDATE ContasAReceber SET
                    Ativo = 0,
                    Status = 'Cancelada',
                    DataAlteracao = @DataAlteracao,
                    MotivoCancelamento = 'Cancelamento da Venda de Origem'
                WHERE VendaModelo = @Modelo
                  AND VendaSerie = @Serie
                  AND VendaNumeroNota = @NumeroNota
                  AND ClienteId = @ClienteId";

            using (MySqlCommand cmd = new MySqlCommand(query, conn, trans))
            {
                cmd.Parameters.AddWithValue("@DataAlteracao", DateTime.Now);
                cmd.Parameters.AddWithValue("@Modelo", venda.Modelo);
                cmd.Parameters.AddWithValue("@Serie", venda.Serie);
                cmd.Parameters.AddWithValue("@NumeroNota", venda.NumeroNota);
                cmd.Parameters.AddWithValue("@ClienteId", venda.ClienteId);
                cmd.ExecuteNonQuery();
            }
        }

        private ContasAReceber MontarObjeto(MySqlDataReader reader)
        {
            return new ContasAReceber
            {
                VendaModelo = reader.GetString("VendaModelo"),
                VendaSerie = reader.GetString("VendaSerie"),
                VendaNumeroNota = reader.GetInt32("VendaNumeroNota"),
                ClienteId = reader.GetInt32("ClienteId"),
                NumeroParcela = reader.GetInt32("NumeroParcela"),

                NomeCliente = reader.IsDBNull(reader.GetOrdinal("NomeCliente")) ? "" : reader.GetString("NomeCliente"),
                Descricao = reader.IsDBNull(reader.GetOrdinal("Descricao")) ? "" : reader.GetString("Descricao"),
                DataEmissao = reader.GetDateTime("DataEmissao"),
                DataVencimento = reader.GetDateTime("DataVencimento"),
                ValorVencimento = reader.GetDecimal("ValorVencimento"),

                Status = reader.GetString("Status"),
                Ativo = reader.GetBoolean("Ativo"),
                IdFormaPagamento = reader.IsDBNull(reader.GetOrdinal("IdFormaPagamento")) ? (int?)null : reader.GetInt32("IdFormaPagamento"),
                NomeFormaPagamento = reader.IsDBNull(reader.GetOrdinal("NomeFormaPagamento")) ? null : reader.GetString("NomeFormaPagamento"),

                DataPagamento = reader.IsDBNull(reader.GetOrdinal("DataPagamento")) ? (DateTime?)null : reader.GetDateTime("DataPagamento"),
                ValorPago = reader.IsDBNull(reader.GetOrdinal("ValorPago")) ? (decimal?)null : reader.GetDecimal("ValorPago"),

                Juros = reader.IsDBNull(reader.GetOrdinal("Juros")) ? (decimal?)null : reader.GetDecimal("Juros"),
                Multa = reader.IsDBNull(reader.GetOrdinal("Multa")) ? (decimal?)null : reader.GetDecimal("Multa"),
                Desconto = reader.IsDBNull(reader.GetOrdinal("Desconto")) ? (decimal?)null : reader.GetDecimal("Desconto")
            };
        }
    }
}