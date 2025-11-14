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
                    Status, Ativo, DataCadastro, DataAlteracao,
                    Juros, Multa, Desconto  
                ) VALUES (
                    @CompraModelo, @CompraSerie, @CompraNumeroNota, @CompraFornecedorId, @NumeroParcela,
                    @FornecedorId, @Descricao, @DataEmissao, @DataVencimento, @ValorVencimento,
                    @Status, @Ativo, @DataCadastro, @DataAlteracao,
                    @Juros, @Multa, @Desconto  
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
                cmd.Parameters.AddWithValue("@Juros", conta.Juros);
                cmd.Parameters.AddWithValue("@Multa", conta.Multa);
                cmd.Parameters.AddWithValue("@Desconto", conta.Desconto);

                cmd.ExecuteNonQuery();
            }
        }

        public void SalvarManual(ContasAPagar conta)
        {
         
            conta.Ativo = true;
            if (conta.DataEmissao == DateTime.MinValue || conta.DataEmissao == default(DateTime))
                conta.DataEmissao = DateTime.Now.Date;
            conta.NumeroParcela = 1;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                Salvar(conta, conn, null); 
            }
        }


        public List<ContasAPagar> Listar(List<string> statuses, string busca)
        {
            List<ContasAPagar> lista = new List<ContasAPagar>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = @"
                    SELECT cap.*, f.Fornecedor AS NomeFornecedor, fp.Descricao AS NomeFormaPagamento
                    FROM ContasAPagar cap
                    JOIN Fornecedores f ON cap.FornecedorId = f.Id
                    LEFT JOIN FormasPagamento fp ON cap.IdFormaPagamento = fp.Id";

                var whereClauses = new List<string>();

                whereClauses.Add("(f.Fornecedor LIKE @Busca OR cap.Descricao LIKE @Busca)");

                if (statuses != null && statuses.Count > 0)
                {
                    var statusParams = new List<string>();
                    for (int i = 0; i < statuses.Count; i++)
                    {
                        statusParams.Add($"@Status{i}");
                    }
                    whereClauses.Add($"cap.Status IN ({string.Join(", ", statusParams)})");
                }
                else
                {
                    whereClauses.Add("1 = 0"); 
                }

                query += $" WHERE {string.Join(" AND ", whereClauses)} ORDER BY cap.DataVencimento";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
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
                CompraModelo = reader.GetString("CompraModelo"),
                CompraSerie = reader.GetString("CompraSerie"),
                CompraNumeroNota = reader.GetInt32("CompraNumeroNota"),
                CompraFornecedorId = reader.GetInt32("CompraFornecedorId"),
                NumeroParcela = reader.GetInt32("NumeroParcela"),

                FornecedorId = reader.GetInt32("FornecedorId"),
                NomeFornecedor = reader.GetString("NomeFornecedor"),
                Descricao = reader.GetString("Descricao"),
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

        public bool VerificarExistencia(string modelo, string serie, int numeroNota, int fornecedorId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(1) FROM Compras WHERE Modelo = @Modelo AND Serie = @Serie AND NumeroNota = @NumeroNota AND FornecedorId = @FornecedorId";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Modelo", modelo);
                    cmd.Parameters.AddWithValue("@Serie", serie);
                    cmd.Parameters.AddWithValue("@NumeroNota", numeroNota);
                    cmd.Parameters.AddWithValue("@FornecedorId", fornecedorId);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        public bool VerificarParcelaAnteriorPaga(ContasAPagar conta)
        {
            if (conta.NumeroParcela <= 1)
            {
                return true;
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT Status 
                    FROM ContasAPagar
                    WHERE CompraModelo = @CompraModelo
                      AND CompraSerie = @CompraSerie
                      AND CompraNumeroNota = @CompraNumeroNota
                      AND CompraFornecedorId = @CompraFornecedorId
                      AND NumeroParcela = @NumeroParcelaAnterior";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CompraModelo", conta.CompraModelo);
                    cmd.Parameters.AddWithValue("@CompraSerie", conta.CompraSerie);
                    cmd.Parameters.AddWithValue("@CompraNumeroNota", conta.CompraNumeroNota);
                    cmd.Parameters.AddWithValue("@CompraFornecedorId", conta.CompraFornecedorId);
                    cmd.Parameters.AddWithValue("@NumeroParcelaAnterior", conta.NumeroParcela - 1);

                    var resultado = cmd.ExecuteScalar();

                    return (resultado != null && resultado.ToString() == "Paga");
                }
            }
        }

        public void CancelarContaManual(ContasAPagar conta, string motivo)
        {
            if (conta.Status != "Aberta")
            {
                throw new InvalidOperationException("Apenas contas com status 'Aberta' podem ser canceladas.");
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    UPDATE ContasAPagar SET
                        Status = 'Cancelada',
                        Ativo = 0,                     
                        MotivoCancelamento = @Motivo,  
                        DataAlteracao = @DataAlteracao
                    WHERE CompraModelo = @CompraModelo      
                      AND CompraSerie = @CompraSerie
                      AND CompraNumeroNota = @CompraNumeroNota
                      AND CompraFornecedorId = @CompraFornecedorId
                      AND NumeroParcela = @NumeroParcela
                      AND Status = 'Aberta'"; 

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Motivo", motivo);
                    cmd.Parameters.AddWithValue("@DataAlteracao", DateTime.Now);

                    cmd.Parameters.AddWithValue("@CompraModelo", conta.CompraModelo);
                    cmd.Parameters.AddWithValue("@CompraSerie", conta.CompraSerie);
                    cmd.Parameters.AddWithValue("@CompraNumeroNota", conta.CompraNumeroNota);
                    cmd.Parameters.AddWithValue("@CompraFornecedorId", conta.CompraFornecedorId);
                    cmd.Parameters.AddWithValue("@NumeroParcela", conta.NumeroParcela);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new Exception("Conta não encontrada ou o status mudou. Nenhuma linha foi atualizada.");
                    }
                }
            }
        }
    }
}