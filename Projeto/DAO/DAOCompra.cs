using MySql.Data.MySqlClient;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;

namespace Projeto.DAO
{
    internal class DAOCompra
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString;

        private void AtualizarProduto(ItemCompra item, decimal custoUnitarioReal, MySqlConnection conn, MySqlTransaction trans)
        {
            string sqlSelect = "SELECT Estoque, PrecoCusto, PrecoVenda FROM Produtos WHERE Id = @ProdutoId";
            MySqlCommand cmdSelect = new MySqlCommand(sqlSelect, conn, trans);
            cmdSelect.Parameters.AddWithValue("@ProdutoId", item.ProdutoId);

            using (var reader = cmdSelect.ExecuteReader())
            {
                if (reader.Read())
                {
                    int estoqueAtual = reader.GetInt32("Estoque");
                    decimal custoAtual = reader.GetDecimal("PrecoCusto");
                    decimal valorVenda = reader.GetDecimal("PrecoVenda");
                    reader.Close();

                    decimal novoCustoMedio = ((custoAtual * estoqueAtual) + (custoUnitarioReal * item.Quantidade)) / (estoqueAtual + item.Quantidade);

                    decimal novoPercentualLucro = 0;
                    if (novoCustoMedio > 0)
                    {
                        novoPercentualLucro = ((valorVenda / novoCustoMedio) - 1) * 100;
                    }

                    string sqlUpdate = "UPDATE Produtos SET Estoque = @NovoEstoque, PrecoCusto = @NovoCustoMedio, PrecoCustoAnterior = @CustoAnterior, PorcentagemLucro = @NovoPercentualLucro WHERE Id = @ProdutoId";
                    MySqlCommand cmdUpdate = new MySqlCommand(sqlUpdate, conn, trans);
                    cmdUpdate.Parameters.AddWithValue("@NovoEstoque", estoqueAtual + item.Quantidade);
                    cmdUpdate.Parameters.AddWithValue("@NovoCustoMedio", novoCustoMedio);
                    cmdUpdate.Parameters.AddWithValue("@CustoAnterior", custoAtual);
                    cmdUpdate.Parameters.AddWithValue("@NovoPercentualLucro", novoPercentualLucro);
                    cmdUpdate.Parameters.AddWithValue("@ProdutoId", item.ProdutoId);
                    cmdUpdate.ExecuteNonQuery();
                }
                else
                {
                    reader.Close();
                    throw new Exception($"Produto com ID {item.ProdutoId} não encontrado para atualização de estoque.");
                }
            }
        }
        private void ReverterAtualizacaoProduto(ItemCompra item, MySqlConnection conn, MySqlTransaction trans)
        {
            string sqlSelect = "SELECT Estoque, PrecoCusto, PrecoCustoAnterior, PrecoVenda FROM Produtos WHERE Id = @ProdutoId";
            MySqlCommand cmdSelect = new MySqlCommand(sqlSelect, conn, trans);
            cmdSelect.Parameters.AddWithValue("@ProdutoId", item.ProdutoId);

            using (var reader = cmdSelect.ExecuteReader())
            {
                if (reader.Read())
                {
                    int estoqueAtual = reader.GetInt32("Estoque");
                    decimal custoAtual = reader.GetDecimal("PrecoCusto");
                    decimal custoParaRestaurar = reader.GetDecimal("PrecoCustoAnterior"); 
                    decimal valorVenda = reader.GetDecimal("PrecoVenda");
                    reader.Close(); 

                    int novoEstoque = estoqueAtual - (int)item.Quantidade;
                    if (novoEstoque < 0) novoEstoque = 0; 

                    decimal precoCustoRestaurado = custoParaRestaurar;

                    decimal novoPercentualLucro = 0;
                    if (precoCustoRestaurado > 0)
                    {
                        novoPercentualLucro = ((valorVenda / precoCustoRestaurado) - 1) * 100;
                    }

                    decimal novoPrecoCustoAnterior = custoParaRestaurar;

                    string sqlUpdate = @"UPDATE Produtos SET 
                                    Estoque = @NovoEstoque, 
                                    PrecoCusto = @PrecoCustoRestaurado, 
                                    PrecoCustoAnterior = @NovoPrecoCustoAnterior, 
                                    PorcentagemLucro = @NovoPercentualLucro 
                                 WHERE Id = @ProdutoId";

                    MySqlCommand cmdUpdate = new MySqlCommand(sqlUpdate, conn, trans);
                    cmdUpdate.Parameters.AddWithValue("@NovoEstoque", novoEstoque);
                    cmdUpdate.Parameters.AddWithValue("@PrecoCustoRestaurado", precoCustoRestaurado);
                    cmdUpdate.Parameters.AddWithValue("@NovoPrecoCustoAnterior", novoPrecoCustoAnterior);
                    cmdUpdate.Parameters.AddWithValue("@NovoPercentualLucro", novoPercentualLucro);
                    cmdUpdate.Parameters.AddWithValue("@ProdutoId", item.ProdutoId);
                    cmdUpdate.ExecuteNonQuery();
                }
                else
                {
                    reader.Close();
                    throw new Exception($"Produto com ID {item.ProdutoId} não encontrado durante a reversão de estoque.");
                }
            }
        }

        public void Salvar(Compra compra)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlTransaction tran = conn.BeginTransaction();

                DAOContasAPagar daoContas = new DAOContasAPagar();

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

                        decimal totalCustosAdicionais = compra.ValorFrete + compra.Seguro + compra.Despesas;
                        decimal valorTotalCompra = compra.Itens.Sum(i => i.ValorTotalItem);

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

                            // Calcula o custo real do item, distribuindo as despesas
                            decimal custoUnitarioReal = item.ValorUnitario;
                            if (valorTotalCompra > 0 && item.Quantidade > 0)
                            {
                                custoUnitarioReal += (totalCustosAdicionais * (item.ValorTotalItem / valorTotalCompra)) / item.Quantidade;
                            }

                            AtualizarProduto(item, custoUnitarioReal, conn, tran);
                        }
                    }



                    if (compra.Parcelas != null && compra.Parcelas.Count > 0)
                    {

                        foreach (var conta in compra.Parcelas)
                        {
                            conta.FornecedorId = compra.FornecedorId;
                            conta.DataEmissao = (DateTime)compra.DataEmissao;

                            daoContas.Salvar(conta, conn, tran);
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


                string checkPagamentosQuery = @"
                    SELECT COUNT(*) FROM ContasAPagar 
                    WHERE CompraModelo = @Modelo 
                      AND CompraSerie = @Serie 
                      AND CompraNumeroNota = @NumeroNota 
                      AND CompraFornecedorId = @FornecedorId 
                      AND Status = 'Paga'";

                using (MySqlCommand checkCmd = new MySqlCommand(checkPagamentosQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@Modelo", compra.Modelo);
                    checkCmd.Parameters.AddWithValue("@Serie", compra.Serie);
                    checkCmd.Parameters.AddWithValue("@NumeroNota", compra.NumeroNota);
                    checkCmd.Parameters.AddWithValue("@FornecedorId", compra.FornecedorId);

                    if (Convert.ToInt32(checkCmd.ExecuteScalar()) > 0)
                    {
                        throw new InvalidOperationException("Esta compra não pode ser cancelada, pois já possui uma ou mais parcelas pagas.");
                    }
                }

                MySqlTransaction tran = conn.BeginTransaction();

                DAOContasAPagar daoContas = new DAOContasAPagar();

                try
                {
                    string selectItensQuery = "SELECT ProdutoId, Quantidade, ValorUnitario FROM ItensCompra WHERE CompraModelo = @Modelo AND CompraSerie = @Serie AND CompraNumeroNota = @NumeroNota AND CompraFornecedorId = @FornecedorId";
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
                                    Quantidade = reader.GetDecimal("Quantidade"),
                                    ValorUnitario = reader.GetDecimal("ValorUnitario")
                                });
                            }
                        }
                    }

                    foreach (var item in itensParaEstornar)
                    {
                        ReverterAtualizacaoProduto(item, conn, tran);
                    }

                    string updateCompraQuery = @"UPDATE Compras 
                                                 SET Ativo = 0, 
                                                     Observacao = @Observacao, 
                                                     DataAlteracao = @DataAlteracao 
                                                 WHERE Modelo = @Modelo 
                                                   AND Serie = @Serie 
                                                   AND NumeroNota = @NumeroNota 
                                                   AND FornecedorId = @FornecedorId";
                    using (MySqlCommand cmd = new MySqlCommand(updateCompraQuery, conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@Observacao", compra.Observacao);
                        cmd.Parameters.AddWithValue("@DataAlteracao", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Modelo", compra.Modelo);
                        cmd.Parameters.AddWithValue("@Serie", compra.Serie);
                        cmd.Parameters.AddWithValue("@NumeroNota", compra.NumeroNota);
                        cmd.Parameters.AddWithValue("@FornecedorId", compra.FornecedorId);
                        cmd.ExecuteNonQuery();
                    }

                    daoContas.CancelarContasPorCompra(compra, conn, tran);

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
                    ORDER BY c.Modelo, c.Serie, c.NumeroNota, c.FornecedorId";

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
                            p.Produto AS NomeProduto,
                            um.Nome AS NomeUnidadeMedida
                        FROM ItensCompra i
                        JOIN Produtos p ON i.ProdutoId = p.Id
                        LEFT JOIN UnidadesMedida um ON p.UnidadeMedidaId = um.Id
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

        public bool VerificarDuplicidade(string modelo, string serie, int numeroNota, int fornecedorId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT COUNT(1) FROM Compras WHERE Modelo = @Modelo AND Serie = @Serie AND NumeroNota = @NumeroNota AND FornecedorId = @FornecedorId";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Modelo", modelo);
                    cmd.Parameters.AddWithValue("@Serie", serie);
                    cmd.Parameters.AddWithValue("@NumeroNota", numeroNota);
                    cmd.Parameters.AddWithValue("@FornecedorId", fornecedorId);

                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
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
                NomeProduto = reader.IsDBNull(reader.GetOrdinal("NomeProduto")) ? "" : reader.GetString("NomeProduto"),
                NomeUnidadeMedida = reader.IsDBNull(reader.GetOrdinal("NomeUnidadeMedida")) ? "UN" : reader.GetString("NomeUnidadeMedida")
            };
        }

        public List<Compra> Pesquisar(string busca)
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
                    WHERE 1=1";

                if (!string.IsNullOrWhiteSpace(busca))
                {
                    query += " AND (f.Fornecedor LIKE @Busca OR c.NumeroNota LIKE @Busca)";
                }

                query += " ORDER BY c.DataEmissao DESC";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    if (!string.IsNullOrWhiteSpace(busca))
                    {
                        cmd.Parameters.AddWithValue("@Busca", $"%{busca}%");
                    }

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

    }
}