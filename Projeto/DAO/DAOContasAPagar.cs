using MySql.Data.MySqlClient;
using Projeto.Models;
using System;
using System.Collections.Generic; 
using System.Windows.Forms; 

namespace Projeto.DAO
{
    internal class DAOContasAPagar
    {
        private string connectionString = "Server=localhost;Database=sistema;Uid=root;Pwd=12345678;";


        public void Salvar(ContasAPagar conta, MySqlConnection conn, MySqlTransaction trans)
        {
            string query = @"
                INSERT INTO ContasAPagar (
                    CompraModelo, CompraSerie, CompraNumeroNota, CompraFornecedorId, NumeroParcela,
                    FornecedorId, Descricao, DataEmissao, DataVencimento, ValorVencimento,
                    Status, Ativo, DataCadastro, DataAlteracao
                ) VALUES (
                    @CompraModelo, @CompraSerie, @CompraNumeroNota, @CompraFornecedorId, @NumeroParcela,
                    @FornecedorId, @Descricao, @DataEmissao, @DataVencimento, @ValorVencimento,
                    @Status, @Ativo, @DataCadastro, @DataAlteracao
                )";

            using (MySqlCommand cmd = new MySqlCommand(query, conn, trans))
            {
                cmd.Parameters.AddWithValue("@CompraModelo", conta.CompraModelo);
                cmd.Parameters.AddWithValue("@CompraSerie", conta.CompraSerie);
                cmd.Parameters.AddWithValue("@CompraNumeroNota", conta.CompraNumeroNota);
                cmd.Parameters.AddWithValue("@CompraFornecedorId", conta.CompraFornecedorId);
                cmd.Parameters.AddWithValue("@NumeroParcela", conta.NumeroParcela);
                cmd.Parameters.AddWithValue("@FornecedorId", conta.FornecedorId);
                cmd.Parameters.AddWithValue("@Descricao", conta.Descricao);
                cmd.Parameters.AddWithValue("@DataEmissao", conta.DataEmissao);
                cmd.Parameters.AddWithValue("@DataVencimento", conta.DataVencimento);
                cmd.Parameters.AddWithValue("@ValorVencimento", conta.ValorVencimento);
                cmd.Parameters.AddWithValue("@Status", "Aberta");
                cmd.Parameters.AddWithValue("@Ativo", true);
                cmd.Parameters.AddWithValue("@DataCadastro", DateTime.Now);
                cmd.Parameters.AddWithValue("@DataAlteracao", DateTime.Now);

                cmd.ExecuteNonQuery();
            }
        }

        public void SalvarManual(ContasAPagar conta)
        {
            conta.CompraModelo = "MANUAL";
            conta.CompraSerie = "MAN";
            // Gera um número único negativo para a chave primária
            conta.CompraNumeroNota = (int)(DateTime.Now.Ticks % int.MaxValue) * -1;
            conta.CompraFornecedorId = conta.FornecedorId;
            conta.NumeroParcela = 1; 
            conta.Ativo = true;
            if (conta.DataEmissao == DateTime.MinValue || conta.DataEmissao == default(DateTime))
                conta.DataEmissao = DateTime.Now.Date;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                Salvar(conta, conn, null);
            }
        }


        public List<ContasAPagar> Listar(string status, string busca)
        {
            List<ContasAPagar> lista = new List<ContasAPagar>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT cap.*, f.Fornecedor AS NomeFornecedor, fp.Descricao AS NomeFormaPagamento
                    FROM ContasAPagar cap
                    JOIN Fornecedores f ON cap.FornecedorId = f.Id
                    LEFT JOIN FormasPagamento fp ON cap.IdFormaPagamento = fp.Id
                    WHERE (@Status = 'Todos' OR cap.Status = @Status)
                      AND (f.Fornecedor LIKE @Busca OR cap.Descricao LIKE @Busca)
                    ORDER BY cap.DataVencimento";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@Busca", $"%{busca}%");

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


        public void Pagar(ContasAPagar conta)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    UPDATE ContasAPagar SET
                        IdFormaPagamento = @IdFormaPagamento,
                        DataPagamento = @DataPagamento,
                        ValorPago = @ValorPago,
                        Juros = @Juros,
                        Multa = @Multa,
                        Desconto = @Desconto,
                        Status = 'Paga',
                        DataAlteracao = @DataAlteracao
                    WHERE CompraModelo = @CompraModelo
                      AND CompraSerie = @CompraSerie
                      AND CompraNumeroNota = @CompraNumeroNota
                      AND CompraFornecedorId = @CompraFornecedorId
                      AND NumeroParcela = @NumeroParcela";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdFormaPagamento", conta.IdFormaPagamento);
                    cmd.Parameters.AddWithValue("@DataPagamento", conta.DataPagamento);
                    cmd.Parameters.AddWithValue("@ValorPago", conta.ValorPago);
                    cmd.Parameters.AddWithValue("@Juros", conta.Juros);
                    cmd.Parameters.AddWithValue("@Multa", conta.Multa);
                    cmd.Parameters.AddWithValue("@Desconto", conta.Desconto);
                    cmd.Parameters.AddWithValue("@DataAlteracao", DateTime.Now);

                    // Chave Composta
                    cmd.Parameters.AddWithValue("@CompraModelo", conta.CompraModelo);
                    cmd.Parameters.AddWithValue("@CompraSerie", conta.CompraSerie);
                    cmd.Parameters.AddWithValue("@CompraNumeroNota", conta.CompraNumeroNota);
                    cmd.Parameters.AddWithValue("@CompraFornecedorId", conta.CompraFornecedorId);
                    cmd.Parameters.AddWithValue("@NumeroParcela", conta.NumeroParcela);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Estornar(ContasAPagar conta)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    UPDATE ContasAPagar SET
                        IdFormaPagamento = NULL,
                        DataPagamento = NULL,
                        ValorPago = NULL,
                        Juros = 0,
                        Multa = 0,
                        Desconto = 0,
                        Status = 'Aberta',
                        DataAlteracao = @DataAlteracao
                    WHERE CompraModelo = @CompraModelo
                      AND CompraSerie = @CompraSerie
                      AND CompraNumeroNota = @CompraNumeroNota
                      AND CompraFornecedorId = @CompraFornecedorId
                      AND NumeroParcela = @NumeroParcela";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DataAlteracao", DateTime.Now);

                    // Chave Composta
                    cmd.Parameters.AddWithValue("@CompraModelo", conta.CompraModelo);
                    cmd.Parameters.AddWithValue("@CompraSerie", conta.CompraSerie);
                    cmd.Parameters.AddWithValue("@CompraNumeroNota", conta.CompraNumeroNota);
                    cmd.Parameters.AddWithValue("@CompraFornecedorId", conta.CompraFornecedorId);
                    cmd.Parameters.AddWithValue("@NumeroParcela", conta.NumeroParcela);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private ContasAPagar MontarObjeto(MySqlDataReader reader)
        {
            return new ContasAPagar
            {
                // Chave Composta
                CompraModelo = reader.GetString("CompraModelo"),
                CompraSerie = reader.GetString("CompraSerie"),
                CompraNumeroNota = reader.GetInt32("CompraNumeroNota"),
                CompraFornecedorId = reader.GetInt32("CompraFornecedorId"),
                NumeroParcela = reader.GetInt32("NumeroParcela"),

                // Dados
                FornecedorId = reader.GetInt32("FornecedorId"),
                NomeFornecedor = reader.GetString("NomeFornecedor"),
                Descricao = reader.GetString("Descricao"),
                DataEmissao = reader.GetDateTime("DataEmissao"),
                DataVencimento = reader.GetDateTime("DataVencimento"),
                ValorVencimento = reader.GetDecimal("ValorVencimento"),
                Status = reader.GetString("Status"),
                Ativo = reader.GetBoolean("Ativo"),

                // Dados Pagamento (podem ser NULOS)
                IdFormaPagamento = reader.IsDBNull(reader.GetOrdinal("IdFormaPagamento")) ? (int?)null : reader.GetInt32("IdFormaPagamento"),
                NomeFormaPagamento = reader.IsDBNull(reader.GetOrdinal("NomeFormaPagamento")) ? null : reader.GetString("NomeFormaPagamento"),
                DataPagamento = reader.IsDBNull(reader.GetOrdinal("DataPagamento")) ? (DateTime?)null : reader.GetDateTime("DataPagamento"),
                ValorPago = reader.IsDBNull(reader.GetOrdinal("ValorPago")) ? (decimal?)null : reader.GetDecimal("ValorPago"),
                Juros = reader.IsDBNull(reader.GetOrdinal("Juros")) ? (decimal?)null : reader.GetDecimal("Juros"),
                Multa = reader.IsDBNull(reader.GetOrdinal("Multa")) ? (decimal?)null : reader.GetDecimal("Multa"),
                Desconto = reader.IsDBNull(reader.GetOrdinal("Desconto")) ? (decimal?)null : reader.GetDecimal("Desconto")
            };
        }

        public void CancelarContasPorCompra(Compra compra, MySqlConnection conn, MySqlTransaction trans)
        {
            string query = @"
                    UPDATE ContasAPagar SET
                        Ativo = 0,
                        Status = 'Cancelada',
                        DataAlteracao = @DataAlteracao
                    WHERE CompraModelo = @CompraModelo
                      AND CompraSerie = @CompraSerie
                      AND CompraNumeroNota = @CompraNumeroNota
                      AND CompraFornecedorId = @CompraFornecedorId";

            using (MySqlCommand cmd = new MySqlCommand(query, conn, trans))
            {
                cmd.Parameters.AddWithValue("@DataAlteracao", DateTime.Now);
                cmd.Parameters.AddWithValue("@CompraModelo", compra.Modelo);
                cmd.Parameters.AddWithValue("@CompraSerie", compra.Serie);
                cmd.Parameters.AddWithValue("@CompraNumeroNota", compra.NumeroNota);
                cmd.Parameters.AddWithValue("@CompraFornecedorId", compra.FornecedorId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}