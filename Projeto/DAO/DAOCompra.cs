using MySql.Data.MySqlClient;
using Projeto.Models;
using System;
using System.Collections.Generic;

namespace Projeto.DAO
{
    internal class DAOCompra
    {
        private string connectionString = "Server=localhost;Database=sistema;Uid=root;Pwd=12345678;";

        public void Salvar(Compra compra)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlTransaction tran = conn.BeginTransaction();

                try
                {
                    string checkQuery = "SELECT COUNT(1) FROM Compras WHERE Modelo = @Modelo AND Serie = @Serie AND NumeroNota = @NumeroNota AND FornecedorId = @FornecedorId";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn, tran))
                    {
                        checkCmd.Parameters.AddWithValue("@Modelo", compra.Modelo);
                        checkCmd.Parameters.AddWithValue("@Serie", compra.Serie);
                        checkCmd.Parameters.AddWithValue("@NumeroNota", compra.NumeroNota);
                        checkCmd.Parameters.AddWithValue("@FornecedorId", compra.FornecedorId);
                        if (Convert.ToInt32(checkCmd.ExecuteScalar()) > 0)
                        {
                            throw new InvalidOperationException("Esta nota de compra já foi cadastrada.");
                        }
                    }

                    string insertCompraQuery = @"
                        INSERT INTO Compras (
                            Modelo, Serie, NumeroNota, FornecedorId, CondicaoPagamentoId, DataEmissao, DataChegada,
                            ValorFrete, ValorSeguro, OutrasDespesas, ValorTotal, Observacao, Ativo, DataCadastro, DataAlteracao
                        ) VALUES (
                            @Modelo, @Serie, @NumeroNota, @FornecedorId, @CondicaoPagamentoId, @DataEmissao, @DataChegada,
                            @ValorFrete, @ValorSeguro, @OutrasDespesas, @ValorTotal, @Observacao, @Ativo, @DataCadastro, @DataAlteracao
                        )";
                    using (MySqlCommand cmdCompra = new MySqlCommand(insertCompraQuery, conn, tran))
                    {
                        cmdCompra.Parameters.AddWithValue("@Modelo", compra.Modelo);
                        cmdCompra.Parameters.AddWithValue("@Serie", compra.Serie);
                        cmdCompra.Parameters.AddWithValue("@NumeroNota", compra.NumeroNota);
                        cmdCompra.Parameters.AddWithValue("@FornecedorId", compra.FornecedorId);
                        cmdCompra.Parameters.AddWithValue("@CondicaoPagamentoId", compra.CondicaoPagamentoId);
                        cmdCompra.Parameters.AddWithValue("@DataEmissao", compra.DataEmissao);
                        cmdCompra.Parameters.AddWithValue("@DataChegada", compra.DataChegada);
                        cmdCompra.Parameters.AddWithValue("@ValorFrete", compra.ValorFrete);
                        cmdCompra.Parameters.AddWithValue("@ValorSeguro", compra.Seguro);
                        cmdCompra.Parameters.AddWithValue("@OutrasDespesas", compra.Despesas);
                        cmdCompra.Parameters.AddWithValue("@ValorTotal", compra.ValorTotal);
                        cmdCompra.Parameters.AddWithValue("@Observacao", compra.Observacao);
                        cmdCompra.Parameters.AddWithValue("@Ativo", true);
                        cmdCompra.Parameters.AddWithValue("@DataCadastro", DateTime.Now);
                        cmdCompra.Parameters.AddWithValue("@DataAlteracao", DateTime.Now);
                        cmdCompra.ExecuteNonQuery();
                    }

                    if (compra.Itens != null && compra.Itens.Count > 0)
                    {
                        string insertItemQuery = @"INSERT INTO ItensCompra (CompraModelo, CompraSerie, CompraNumeroNota, CompraFornecedorId, ProdutoId, Quantidade, ValorUnitario, ValorTotalItem) VALUES (@CompraModelo, @CompraSerie, @CompraNumeroNota, @CompraFornecedorId, @ProdutoId, @Quantidade, @ValorUnitario, @ValorTotalItem)";
                        string updateEstoqueQuery = "UPDATE Produtos SET Estoque = Estoque + @Quantidade WHERE Id = @ProdutoId";

                        foreach (var item in compra.Itens)
                        {
                            using (MySqlCommand cmdItem = new MySqlCommand(insertItemQuery, conn, tran))
                            {
                                cmdItem.Parameters.AddWithValue("@CompraModelo", compra.Modelo);
                                cmdItem.Parameters.AddWithValue("@CompraSerie", compra.Serie);
                                cmdItem.Parameters.AddWithValue("@CompraNumeroNota", compra.NumeroNota);
                                cmdItem.Parameters.AddWithValue("@CompraFornecedorId", compra.FornecedorId);
                                cmdItem.Parameters.AddWithValue("@ProdutoId", item.ProdutoId);
                                cmdItem.Parameters.AddWithValue("@Quantidade", item.Quantidade);
                                cmdItem.Parameters.AddWithValue("@ValorUnitario", item.ValorUnitario);
                                cmdItem.Parameters.AddWithValue("@ValorTotalItem", item.ValorTotalItem);
                                cmdItem.ExecuteNonQuery();
                            }
                            using (MySqlCommand cmdEstoque = new MySqlCommand(updateEstoqueQuery, conn, tran))
                            {
                                cmdEstoque.Parameters.AddWithValue("@Quantidade", item.Quantidade);
                                cmdEstoque.Parameters.AddWithValue("@ProdutoId", item.ProdutoId);
                                cmdEstoque.ExecuteNonQuery();
                            }
                        }
                    }

                    if (compra.ParcelasCompra != null && compra.ParcelasCompra.Count > 0)
                    {
                        string insertParcelaQuery = @"
                            INSERT INTO ParcelasCompra (
                                CompraModelo, CompraSerie, CompraNumeroNota, CompraFornecedorId, NumeroParcela, 
                                ValorParcela, DataVencimento
                            ) VALUES (
                                @CompraModelo, @CompraSerie, @CompraNumeroNota, @CompraFornecedorId, @NumeroParcela, 
                                @ValorParcela, @DataVencimento
                            )";

                        foreach (var parcela in compra.ParcelasCompra)
                        {
                            using (MySqlCommand cmdParcela = new MySqlCommand(insertParcelaQuery, conn, tran))
                            {
                                cmdParcela.Parameters.AddWithValue("@CompraModelo", parcela.CompraModelo);
                                cmdParcela.Parameters.AddWithValue("@CompraSerie", parcela.CompraSerie);
                                cmdParcela.Parameters.AddWithValue("@CompraNumeroNota", parcela.CompraNumeroNota);
                                cmdParcela.Parameters.AddWithValue("@CompraFornecedorId", parcela.CompraFornecedorId);
                                cmdParcela.Parameters.AddWithValue("@NumeroParcela", parcela.NumeroParcela);
                                cmdParcela.Parameters.AddWithValue("@ValorParcela", parcela.ValorParcela);
                                cmdParcela.Parameters.AddWithValue("@DataVencimento", parcela.DataVencimento);
                                cmdParcela.ExecuteNonQuery();
                            }
                        }
                    }

                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        public void Cancelar(Compra compra)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlTransaction tran = conn.BeginTransaction();
                try
                {
                    string selectItensQuery = "SELECT ProdutoId, Quantidade FROM ItensCompra WHERE CompraModelo = @Modelo AND CompraSerie = @Serie AND CompraNumeroNota = @NumeroNota AND CompraFornecedorId = @FornecedorId";
                    var itensParaEstornar = new List<ItemCompra>();
                    using (MySqlCommand cmdSelect = new MySqlCommand(selectItensQuery, conn, tran))
                    {
                        cmdSelect.Parameters.AddWithValue("@Modelo", compra.Modelo);
                        cmdSelect.Parameters.AddWithValue("@Serie", compra.Serie);
                        cmdSelect.Parameters.AddWithValue("@NumeroNota", compra.NumeroNota);
                        cmdSelect.Parameters.AddWithValue("@FornecedorId", compra.FornecedorId);
                        using (MySqlDataReader reader = cmdSelect.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                itensParaEstornar.Add(new ItemCompra
                                {
                                    ProdutoId = reader.GetInt32("ProdutoId"),
                                    Quantidade = reader.GetDecimal("Quantidade")
                                });
                            }
                        }
                    }

                    string updateEstoqueQuery = "UPDATE Produtos SET Estoque = Estoque - @Quantidade WHERE Id = @ProdutoId";
                    foreach (var item in itensParaEstornar)
                    {
                        using (MySqlCommand cmdEstoque = new MySqlCommand(updateEstoqueQuery, conn, tran))
                        {
                            cmdEstoque.Parameters.AddWithValue("@Quantidade", item.Quantidade);
                            cmdEstoque.Parameters.AddWithValue("@ProdutoId", item.ProdutoId);
                            cmdEstoque.ExecuteNonQuery();
                        }
                    }

                    string updateCompraQuery = @"UPDATE Compras SET Ativo = 0, DataAlteracao = @DataAlteracao WHERE Modelo = @Modelo AND Serie = @Serie AND NumeroNota = @NumeroNota AND FornecedorId = @FornecedorId";
                    using (MySqlCommand cmd = new MySqlCommand(updateCompraQuery, conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@DataAlteracao", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Modelo", compra.Modelo);
                        cmd.Parameters.AddWithValue("@Serie", compra.Serie);
                        cmd.Parameters.AddWithValue("@NumeroNota", compra.NumeroNota);
                        cmd.Parameters.AddWithValue("@FornecedorId", compra.FornecedorId);
                        cmd.ExecuteNonQuery();
                    }

                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        public List<Compra> Listar()
        {
            List<Compra> lista = new List<Compra>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT 
                        c.*,
                        f.Fornecedor AS NomeFornecedor,
                        cp.Descricao AS NomeCondPgto
                    FROM Compras c
                    LEFT JOIN Fornecedores f ON c.FornecedorId = f.Id
                    LEFT JOIN CondicoesPagamento cp ON c.CondicaoPagamentoId = cp.Id
                    ORDER BY c.DataEmissao DESC";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(MontarObjetoCompra(reader));
                        }
                    }
                }
            }
            return lista;
        }

        public Compra BuscarPorChave(string modelo, string serie, int numeroNota, int fornecedorId)
        {
            Compra compra = null;
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string queryCompra = @"
                    SELECT 
                        c.*,
                        f.Fornecedor AS NomeFornecedor,
                        cp.Descricao AS NomeCondPgto
                    FROM Compras c
                    LEFT JOIN Fornecedores f ON c.FornecedorId = f.Id
                    LEFT JOIN CondicoesPagamento cp ON c.CondicaoPagamentoId = cp.Id
                    WHERE c.Modelo = @Modelo AND c.Serie = @Serie AND c.NumeroNota = @NumeroNota AND c.FornecedorId = @FornecedorId";

                using (MySqlCommand cmd = new MySqlCommand(queryCompra, conn))
                {
                    cmd.Parameters.AddWithValue("@Modelo", modelo);
                    cmd.Parameters.AddWithValue("@Serie", serie);
                    cmd.Parameters.AddWithValue("@NumeroNota", numeroNota);
                    cmd.Parameters.AddWithValue("@FornecedorId", fornecedorId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            compra = MontarObjetoCompra(reader);
                        }
                    }
                }

                if (compra != null)
                {
                    string queryItens = @"
                        SELECT 
                            i.*,
                            p.Produto AS NomeProduto
                        FROM ItensCompra i
                        JOIN Produtos p ON i.ProdutoId = p.Id
                        WHERE i.CompraModelo = @Modelo AND i.CompraSerie = @Serie AND i.CompraNumeroNota = @NumeroNota AND i.CompraFornecedorId = @FornecedorId";

                    using (MySqlCommand cmdItens = new MySqlCommand(queryItens, conn))
                    {
                        cmdItens.Parameters.AddWithValue("@Modelo", modelo);
                        cmdItens.Parameters.AddWithValue("@Serie", serie);
                        cmdItens.Parameters.AddWithValue("@NumeroNota", numeroNota);
                        cmdItens.Parameters.AddWithValue("@FornecedorId", fornecedorId);

                        using (MySqlDataReader readerItens = cmdItens.ExecuteReader())
                        {
                            while (readerItens.Read())
                            {
                                compra.Itens.Add(MontarObjetoItem(readerItens));
                            }
                        }
                    }
                }
            }
            return compra;
        }

        private Compra MontarObjetoCompra(MySqlDataReader reader)
        {
            return new Compra
            {
                Modelo = reader.GetString("Modelo"),
                Serie = reader.GetString("Serie"),
                NumeroNota = reader.GetInt32("NumeroNota"),
                FornecedorId = reader.GetInt32("FornecedorId"),
                CondicaoPagamentoId = reader.IsDBNull(reader.GetOrdinal("CondicaoPagamentoId")) ? (int?)null : reader.GetInt32("CondicaoPagamentoId"),
                DataEmissao = reader.IsDBNull(reader.GetOrdinal("DataEmissao")) ? (DateTime?)null : reader.GetDateTime("DataEmissao"),
                DataChegada = reader.IsDBNull(reader.GetOrdinal("DataChegada")) ? (DateTime?)null : reader.GetDateTime("DataChegada"),
                ValorFrete = reader.GetDecimal("ValorFrete"),
                Seguro = reader.GetDecimal("ValorSeguro"),
                Despesas = reader.GetDecimal("OutrasDespesas"),
                ValorTotal = reader.GetDecimal("ValorTotal"),
                Observacao = reader.IsDBNull(reader.GetOrdinal("Observacao")) ? null : reader.GetString("Observacao"),
                Ativo = reader.GetBoolean("Ativo"),
                DataCadastro = reader.GetDateTime("DataCadastro"),
                DataAlteracao = reader.GetDateTime("DataAlteracao"),
                oFornecedor = new Fornecedor
                {
                    Id = reader.GetInt32("FornecedorId"),
                    Nome = reader.IsDBNull(reader.GetOrdinal("NomeFornecedor")) ? "" : reader.GetString("NomeFornecedor")
                },
                oCondicaoPagamento = new CondicaoPagamento
                {
                    Id = reader.IsDBNull(reader.GetOrdinal("CondicaoPagamentoId")) ? 0 : reader.GetInt32("CondicaoPagamentoId"),
                    Descricao = reader.IsDBNull(reader.GetOrdinal("NomeCondPgto")) ? "" : reader.GetString("NomeCondPgto")
                }
            };
        }

        private ItemCompra MontarObjetoItem(MySqlDataReader reader)
        {
            return new ItemCompra
            {
                CompraModelo = reader.GetString("CompraModelo"),
                CompraSerie = reader.GetString("CompraSerie"),
                CompraNumeroNota = reader.GetInt32("CompraNumeroNota"),
                CompraFornecedorId = reader.GetInt32("CompraFornecedorId"),
                ProdutoId = reader.GetInt32("ProdutoId"),
                Quantidade = reader.GetDecimal("Quantidade"),
                ValorUnitario = reader.GetDecimal("ValorUnitario"),
                ValorTotalItem = reader.GetDecimal("ValorTotalItem"),
                NomeProduto = reader.IsDBNull(reader.GetOrdinal("NomeProduto")) ? "" : reader.GetString("NomeProduto")
            };
        }
    }
}