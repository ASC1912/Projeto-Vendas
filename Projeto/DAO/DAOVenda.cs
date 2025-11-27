using MySql.Data.MySqlClient;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.DAO
{
    internal class DAOVenda
    {
        private string connectionString = "Server=localhost;Database=sistema;Uid=root;Pwd=12345678;";

        private void AtualizarProduto(ItemVenda item, MySqlConnection conn, MySqlTransaction trans)
        {
            string sqlSelect = "SELECT Estoque FROM Produtos WHERE Id = @ProdutoId";
            MySqlCommand cmdSelect = new MySqlCommand(sqlSelect, conn, trans);
            cmdSelect.Parameters.AddWithValue("@ProdutoId", item.ProdutoId);

            using (var reader = cmdSelect.ExecuteReader())
            {
                if (reader.Read())
                {
                    int estoqueAtual = reader.GetInt32("Estoque");
                    reader.Close();

                    int novoEstoque = estoqueAtual - (int)item.Quantidade;

                    if (novoEstoque < 0)
                    {
                        throw new InvalidOperationException($"Estoque insuficiente para o produto ID {item.ProdutoId}.");
                    }

                    string sqlUpdate = "UPDATE Produtos SET Estoque = @NovoEstoque WHERE Id = @ProdutoId";
                    MySqlCommand cmdUpdate = new MySqlCommand(sqlUpdate, conn, trans);
                    cmdUpdate.Parameters.AddWithValue("@NovoEstoque", novoEstoque);
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

        private void ReverterAtualizacaoProduto(ItemVenda item, MySqlConnection conn, MySqlTransaction trans)
        {
            string sqlUpdate = "UPDATE Produtos SET Estoque = Estoque + @Quantidade WHERE Id = @ProdutoId";
            MySqlCommand cmdUpdate = new MySqlCommand(sqlUpdate, conn, trans);
            cmdUpdate.Parameters.AddWithValue("@Quantidade", item.Quantidade);
            cmdUpdate.Parameters.AddWithValue("@ProdutoId", item.ProdutoId);
            cmdUpdate.ExecuteNonQuery();
        }

        public void Salvar(Venda venda)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string checkPagamentosQuery = @"
                    SELECT COUNT(*) FROM ContasAReceber 
                    WHERE VendaModelo = @Modelo 
                      AND VendaSerie = @Serie 
                      AND VendaNumeroNota = @NumeroNota 
                      AND ClienteId = @ClienteId 
                      AND Status = 'Paga'";

                using (MySqlCommand checkCmd = new MySqlCommand(checkPagamentosQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@Modelo", venda.Modelo);
                    checkCmd.Parameters.AddWithValue("@Serie", venda.Serie);
                    checkCmd.Parameters.AddWithValue("@NumeroNota", venda.NumeroNota);
                    checkCmd.Parameters.AddWithValue("@ClienteId", venda.ClienteId); 

                    if (Convert.ToInt32(checkCmd.ExecuteScalar()) > 0)
                    {
                        throw new InvalidOperationException("Esta venda não pode ser cancelada, pois já possui uma ou mais parcelas pagas/recebidas.");
                    }
                }

                MySqlTransaction tran = conn.BeginTransaction();

                DAOContasAReceber daoContas = new DAOContasAReceber();

                try
                {
                    string checkQuery = "SELECT COUNT(1) FROM Vendas WHERE Modelo = @Modelo AND Serie = @Serie AND NumeroNota = @NumeroNota AND ClienteId = @ClienteId";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn, tran))
                    {
                        checkCmd.Parameters.AddWithValue("@Modelo", venda.Modelo);
                        checkCmd.Parameters.AddWithValue("@Serie", venda.Serie);
                        checkCmd.Parameters.AddWithValue("@NumeroNota", venda.NumeroNota);
                        checkCmd.Parameters.AddWithValue("@ClienteId", venda.ClienteId);
                        if (Convert.ToInt32(checkCmd.ExecuteScalar()) > 0)
                        {
                            throw new InvalidOperationException("Esta nota de venda já foi cadastrada.");
                        }
                    }

                    string insertVendaQuery = @"
                        INSERT INTO Vendas (
                            Modelo, Serie, NumeroNota, ClienteId, FuncionarioId, CondicaoPagamentoId, DataEmissao, DataSaida,
                            ValorTotal, Observacao, Ativo, DataCadastro, DataAlteracao
                        ) VALUES (
                            @Modelo, @Serie, @NumeroNota, @ClienteId, @FuncionarioId, @CondicaoPagamentoId, @DataEmissao, @DataSaida,
                            @ValorTotal, @Observacao, @Ativo, @DataCadastro, @DataAlteracao
                        )";

                    using (MySqlCommand cmdVenda = new MySqlCommand(insertVendaQuery, conn, tran))
                    {
                        cmdVenda.Parameters.AddWithValue("@Modelo", venda.Modelo);
                        cmdVenda.Parameters.AddWithValue("@Serie", venda.Serie);
                        cmdVenda.Parameters.AddWithValue("@NumeroNota", venda.NumeroNota);
                        cmdVenda.Parameters.AddWithValue("@ClienteId", venda.ClienteId);
                        cmdVenda.Parameters.AddWithValue("@FuncionarioId", venda.FuncionarioId);
                        cmdVenda.Parameters.AddWithValue("@CondicaoPagamentoId", venda.CondicaoPagamentoId);
                        cmdVenda.Parameters.AddWithValue("@DataEmissao", venda.DataEmissao);
                        cmdVenda.Parameters.AddWithValue("@DataSaida", venda.DataSaida);
                        cmdVenda.Parameters.AddWithValue("@ValorTotal", venda.ValorTotal);
                        cmdVenda.Parameters.AddWithValue("@Observacao", venda.Observacao);
                        cmdVenda.Parameters.AddWithValue("@Ativo", true);
                        cmdVenda.Parameters.AddWithValue("@DataCadastro", DateTime.Now);
                        cmdVenda.Parameters.AddWithValue("@DataAlteracao", DateTime.Now);
                        cmdVenda.ExecuteNonQuery();
                    }

                    if (venda.Itens != null && venda.Itens.Count > 0)
                    {
                        string insertItemQuery = @"INSERT INTO ItensVenda (VendaModelo, VendaSerie, VendaNumeroNota, VendaClienteId, ProdutoId, Quantidade, ValorUnitario, ValorTotalItem) 
                                                   VALUES (@VendaModelo, @VendaSerie, @VendaNumeroNota, @VendaClienteId, @ProdutoId, @Quantidade, @ValorUnitario, @ValorTotalItem)";

                        foreach (var item in venda.Itens)
                        {
                            using (MySqlCommand cmdItem = new MySqlCommand(insertItemQuery, conn, tran))
                            {
                                cmdItem.Parameters.AddWithValue("@VendaModelo", venda.Modelo);
                                cmdItem.Parameters.AddWithValue("@VendaSerie", venda.Serie);
                                cmdItem.Parameters.AddWithValue("@VendaNumeroNota", venda.NumeroNota);
                                cmdItem.Parameters.AddWithValue("@VendaClienteId", venda.ClienteId);
                                cmdItem.Parameters.AddWithValue("@ProdutoId", item.ProdutoId);
                                cmdItem.Parameters.AddWithValue("@Quantidade", item.Quantidade);
                                cmdItem.Parameters.AddWithValue("@ValorUnitario", item.ValorUnitario);
                                cmdItem.Parameters.AddWithValue("@ValorTotalItem", item.ValorTotalItem);
                                cmdItem.ExecuteNonQuery();
                            }

                            AtualizarProduto(item, conn, tran);
                        }
                    }

                    if (venda.Parcelas != null && venda.Parcelas.Count > 0)
                    {
                        foreach (var conta in venda.Parcelas)
                        {
                            conta.VendaModelo = venda.Modelo;
                            conta.VendaSerie = venda.Serie;
                            conta.VendaNumeroNota = venda.NumeroNota;
                            conta.ClienteId = venda.ClienteId;

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

        public void Cancelar(Venda venda)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlTransaction tran = conn.BeginTransaction();

                DAOContasAReceber daoContas = new DAOContasAReceber();

                try
                {
                    string selectItensQuery = "SELECT ProdutoId, Quantidade FROM ItensVenda WHERE VendaModelo = @Modelo AND VendaSerie = @Serie AND VendaNumeroNota = @NumeroNota AND ClienteId = @ClienteId";
                    var itensParaEstornar = new List<ItemVenda>();
                    using (MySqlCommand cmdSelect = new MySqlCommand(selectItensQuery, conn, tran))
                    {
                        cmdSelect.Parameters.AddWithValue("@Modelo", venda.Modelo);
                        cmdSelect.Parameters.AddWithValue("@Serie", venda.Serie);
                        cmdSelect.Parameters.AddWithValue("@NumeroNota", venda.NumeroNota);
                        cmdSelect.Parameters.AddWithValue("@ClienteId", venda.ClienteId);
                        using (MySqlDataReader reader = cmdSelect.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                itensParaEstornar.Add(new ItemVenda
                                {
                                    ProdutoId = reader.GetInt32("ProdutoId"),
                                    Quantidade = reader.GetDecimal("Quantidade")
                                });
                            }
                        }
                    }

                    foreach (var item in itensParaEstornar)
                    {
                        ReverterAtualizacaoProduto(item, conn, tran);
                    }

                    string updateVendaQuery = @"UPDATE Vendas 
                                                SET Ativo = 0, 
                                                    Observacao = @Observacao, 
                                                    DataAlteracao = @DataAlteracao 
                                                WHERE Modelo = @Modelo 
                                                  AND Serie = @Serie 
                                                  AND NumeroNota = @NumeroNota 
                                                  AND ClienteId = @ClienteId";
                    using (MySqlCommand cmd = new MySqlCommand(updateVendaQuery, conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@Observacao", venda.Observacao);
                        cmd.Parameters.AddWithValue("@DataAlteracao", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Modelo", venda.Modelo);
                        cmd.Parameters.AddWithValue("@Serie", venda.Serie);
                        cmd.Parameters.AddWithValue("@NumeroNota", venda.NumeroNota);
                        cmd.Parameters.AddWithValue("@ClienteId", venda.ClienteId);
                        cmd.ExecuteNonQuery();
                    }

                    daoContas.CancelarContasPorVenda(venda, conn, tran);

                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        public List<Venda> Listar()
        {
            List<Venda> lista = new List<Venda>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT 
                        v.*,
                        c.Cliente AS NomeCliente,
                        f.Funcionario AS NomeFuncionario,  
                        cp.Descricao AS NomeCondPgto
                    FROM Vendas v
                    LEFT JOIN Clientes c ON v.ClienteId = c.Id
                    LEFT JOIN Funcionarios f ON v.FuncionarioId = f.Id 
                    LEFT JOIN CondicoesPagamento cp ON v.CondicaoPagamentoId = cp.Id
                    ORDER BY v.Modelo, CAST(v.Serie AS UNSIGNED), v.NumeroNota, v.ClienteId"; 

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(MontarObjetoVenda(reader));
                        }
                    }
                }
            }
            return lista;
        }

        public Venda BuscarPorChave(string modelo, string serie, int numeroNota, int clienteId)
        {
            Venda venda = null;
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string queryVenda = @"
                    SELECT 
                        v.*,
                        c.Cliente AS NomeCliente,
                        f.Funcionario AS NomeFuncionario,
                        cp.Descricao AS NomeCondPgto
                    FROM Vendas v
                    LEFT JOIN Clientes c ON v.ClienteId = c.Id
                    LEFT JOIN Funcionarios f ON v.FuncionarioId = f.Id 
                    LEFT JOIN CondicoesPagamento cp ON v.CondicaoPagamentoId = cp.Id
                    WHERE v.Modelo = @Modelo AND v.Serie = @Serie AND v.NumeroNota = @NumeroNota AND v.ClienteId = @ClienteId";

                using (MySqlCommand cmd = new MySqlCommand(queryVenda, conn))
                {
                    cmd.Parameters.AddWithValue("@Modelo", modelo);
                    cmd.Parameters.AddWithValue("@Serie", serie);
                    cmd.Parameters.AddWithValue("@NumeroNota", numeroNota);
                    cmd.Parameters.AddWithValue("@ClienteId", clienteId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            venda = MontarObjetoVenda(reader);
                        }
                    }
                }

                if (venda != null)
                {
                    string queryItens = @"
                        SELECT 
                            i.*,
                            p.Produto AS NomeProduto,
                            um.Nome AS NomeUnidadeMedida
                        FROM ItensVenda i
                        JOIN Produtos p ON i.ProdutoId = p.Id
                        LEFT JOIN UnidadesMedida um ON p.UnidadeMedidaId = um.Id
                        WHERE i.VendaModelo = @Modelo AND i.VendaSerie = @Serie AND i.VendaNumeroNota = @NumeroNota AND i.VendaClienteId = @ClienteId";

                    using (MySqlCommand cmdItens = new MySqlCommand(queryItens, conn))
                    {
                        cmdItens.Parameters.AddWithValue("@Modelo", modelo);
                        cmdItens.Parameters.AddWithValue("@Serie", serie);
                        cmdItens.Parameters.AddWithValue("@NumeroNota", numeroNota);
                        cmdItens.Parameters.AddWithValue("@ClienteId", clienteId);

                        using (MySqlDataReader readerItens = cmdItens.ExecuteReader())
                        {
                            while (readerItens.Read())
                            {
                                venda.Itens.Add(MontarObjetoItem(readerItens));
                            }
                        }
                    }
                }
            }
            return venda;
        }

        private Venda MontarObjetoVenda(MySqlDataReader reader)
        {
            return new Venda
            {
                Modelo = reader.GetString("Modelo"),
                Serie = reader.GetString("Serie"),
                NumeroNota = reader.GetInt32("NumeroNota"),
                ClienteId = reader.GetInt32("ClienteId"),
                FuncionarioId = reader.GetInt32("FuncionarioId"),
                CondicaoPagamentoId = reader.IsDBNull(reader.GetOrdinal("CondicaoPagamentoId")) ? (int?)null : reader.GetInt32("CondicaoPagamentoId"),
                DataEmissao = reader.IsDBNull(reader.GetOrdinal("DataEmissao")) ? (DateTime?)null : reader.GetDateTime("DataEmissao"),
                DataSaida = reader.IsDBNull(reader.GetOrdinal("DataSaida")) ? (DateTime?)null : reader.GetDateTime("DataSaida"),
                ValorTotal = reader.GetDecimal("ValorTotal"),
                Observacao = reader.IsDBNull(reader.GetOrdinal("Observacao")) ? null : reader.GetString("Observacao"),
                Ativo = reader.GetBoolean("Ativo"),
                DataCadastro = reader.GetDateTime("DataCadastro"),
                DataAlteracao = reader.GetDateTime("DataAlteracao"),
                oCliente = new Cliente
                {
                    Id = reader.GetInt32("ClienteId"),
                    Nome = reader.IsDBNull(reader.GetOrdinal("NomeCliente")) ? "" : reader.GetString("NomeCliente")
                },
                oFuncionario = new Funcionario
                {
                    Id = reader.GetInt32("FuncionarioId"),
                    Nome = reader.IsDBNull(reader.GetOrdinal("NomeFuncionario")) ? "" : reader.GetString("NomeFuncionario")
                },
                oCondicaoPagamento = new CondicaoPagamento
                {
                    Id = reader.IsDBNull(reader.GetOrdinal("CondicaoPagamentoId")) ? 0 : reader.GetInt32("CondicaoPagamentoId"),
                    Descricao = reader.IsDBNull(reader.GetOrdinal("NomeCondPgto")) ? "" : reader.GetString("NomeCondPgto")
                }
            };
        }

        private ItemVenda MontarObjetoItem(MySqlDataReader reader)
        {
            return new ItemVenda
            {
                VendaModelo = reader.GetString("VendaModelo"),
                VendaSerie = reader.GetString("VendaSerie"),
                VendaNumeroNota = reader.GetInt32("VendaNumeroNota"),
                VendaClienteId = reader.GetInt32("VendaClienteId"),
                ProdutoId = reader.GetInt32("ProdutoId"),
                Quantidade = reader.GetDecimal("Quantidade"),
                ValorUnitario = reader.GetDecimal("ValorUnitario"),
                ValorTotalItem = reader.GetDecimal("ValorTotalItem"),
                NomeProduto = reader.IsDBNull(reader.GetOrdinal("NomeProduto")) ? "" : reader.GetString("NomeProduto"),
                NomeUnidadeMedida = reader.IsDBNull(reader.GetOrdinal("NomeUnidadeMedida")) ? "UN" : reader.GetString("NomeUnidadeMedida")
            };
        }
    }
}