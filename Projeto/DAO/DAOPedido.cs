using MySql.Data.MySqlClient;
using Projeto.Models;
using System;
using System.Collections.Generic;

namespace Projeto.DAO
{
    internal class DAOPedido
    {
        private string connectionString = "Server=localhost;Database=sistema;Uid=root;Pwd=12345678;";

        public void Salvar(Pedido pedido)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    string queryPedido;
                    if (pedido.Id > 0)
                    {
                        // Atualiza o pedido principal
                        queryPedido = @"UPDATE Pedidos SET 
                                            MesaNumero = @MesaNumero, 
                                            FuncionarioId = @FuncionarioId,
                                            VendaId = @VendaId,
                                            Observacao = @Observacao,
                                            Status = @Status,
                                            Ativo = @Ativo,
                                            DataAlteracao = @DataAlteracao
                                        WHERE Id = @Id";
                    }
                    else
                    {
                        // Insere um novo pedido
                        queryPedido = @"INSERT INTO Pedidos 
                                      (MesaNumero, FuncionarioId, VendaId, Observacao, Status, Ativo, DataCadastro, DataAlteracao) 
                                      VALUES (@MesaNumero, @FuncionarioId, @VendaId, @Observacao, @Status, @Ativo, @DataCadastro, @DataAlteracao);
                                      SELECT LAST_INSERT_ID();";
                    }

                    using (MySqlCommand cmdPedido = new MySqlCommand(queryPedido, conn, transaction))
                    {
                        cmdPedido.Parameters.AddWithValue("@Id", pedido.Id);
                        cmdPedido.Parameters.AddWithValue("@MesaNumero", pedido.MesaNumero);
                        cmdPedido.Parameters.AddWithValue("@FuncionarioId", pedido.FuncionarioId);
                        cmdPedido.Parameters.AddWithValue("@VendaId", (object)pedido.VendaId ?? DBNull.Value);
                        cmdPedido.Parameters.AddWithValue("@Observacao", pedido.Observacao);
                        cmdPedido.Parameters.AddWithValue("@Status", pedido.Status);
                        cmdPedido.Parameters.AddWithValue("@Ativo", pedido.Ativo);
                        cmdPedido.Parameters.AddWithValue("@DataAlteracao", DateTime.Now);

                        if (pedido.Id == 0)
                        {
                            cmdPedido.Parameters.AddWithValue("@DataCadastro", DateTime.Now);
                            pedido.Id = Convert.ToInt32(cmdPedido.ExecuteScalar());
                        }
                        else
                        {
                            cmdPedido.ExecuteNonQuery();
                        }
                    }

                    // Deleta os itens existentes para reinseri-los (abordagem simples para atualização)
                    if (pedido.Id > 0)
                    {
                        string deleteItensQuery = "DELETE FROM ItensPedidos WHERE pedidos_id = @PedidoId";
                        using (MySqlCommand cmdDelete = new MySqlCommand(deleteItensQuery, conn, transaction))
                        {
                            cmdDelete.Parameters.AddWithValue("@PedidoId", pedido.Id);
                            cmdDelete.ExecuteNonQuery();
                        }
                    }

                    // Insere os novos itens do pedido
                    string queryItens = @"INSERT INTO ItensPedidos 
                                        (pedidos_id, produto_id, numero_mesa, quantidade, precoUnitario, Status) 
                                        VALUES (@PedidoId, @ProdutoId, @NumeroMesa, @Quantidade, @PrecoUnitario, @Status)";
                    foreach (var item in pedido.Itens)
                    {
                        using (MySqlCommand cmdItem = new MySqlCommand(queryItens, conn, transaction))
                        {
                            cmdItem.Parameters.AddWithValue("@PedidoId", pedido.Id);
                            cmdItem.Parameters.AddWithValue("@ProdutoId", item.ProdutoId);
                            cmdItem.Parameters.AddWithValue("@NumeroMesa", pedido.MesaNumero);
                            cmdItem.Parameters.AddWithValue("@Quantidade", item.Quantidade);
                            cmdItem.Parameters.AddWithValue("@PrecoUnitario", item.PrecoUnitario);
                            cmdItem.Parameters.AddWithValue("@Status", item.Status);
                            cmdItem.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao salvar o pedido: " + ex.Message);
                }
            }
        }

        public void Excluir(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    // Exclui os itens do pedido primeiro
                    string queryItens = "DELETE FROM ItensPedidos WHERE pedidos_id = @Id";
                    using (MySqlCommand cmdItens = new MySqlCommand(queryItens, conn, transaction))
                    {
                        cmdItens.Parameters.AddWithValue("@Id", id);
                        cmdItens.ExecuteNonQuery();
                    }

                    // Exclui o pedido principal
                    string queryPedido = "DELETE FROM Pedidos WHERE Id = @Id";
                    using (MySqlCommand cmdPedido = new MySqlCommand(queryPedido, conn, transaction))
                    {
                        cmdPedido.Parameters.AddWithValue("@Id", id);
                        cmdPedido.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao excluir o pedido: " + ex.Message);
                }
            }
        }

        public Pedido BuscarPorId(int id)
        {
            Pedido pedido = null;
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT p.*, f.Funcionario AS NomeFuncionario FROM Pedidos p
                                 LEFT JOIN Funcionarios f ON p.FuncionarioId = f.Id
                                 WHERE p.Id = @Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            pedido = new Pedido
                            {
                                Id = reader.GetInt32("Id"),
                                MesaNumero = reader.GetInt32("MesaNumero"),
                                FuncionarioId = reader.GetInt32("FuncionarioId"),
                                VendaId = reader.IsDBNull(reader.GetOrdinal("VendaId")) ? (int?)null : reader.GetInt32("VendaId"),
                                Observacao = reader.GetString("Observacao"),
                                Status = reader.GetString("Status"),
                                DataPedido = reader.GetDateTime("DataPedido"),
                                Ativo = reader.GetBoolean("Ativo"),
                                NomeFuncionario = reader.GetString("NomeFuncionario")
                            };
                        }
                    }
                }

                if (pedido != null)
                {
                    string queryItens = @"SELECT ip.*, pr.Produto AS NomeProduto FROM ItensPedidos ip
                                          JOIN Produtos pr ON ip.produto_id = pr.Id
                                          WHERE ip.pedidos_id = @Id";
                    using (MySqlCommand cmdItens = new MySqlCommand(queryItens, conn))
                    {
                        cmdItens.Parameters.AddWithValue("@Id", id);
                        using (MySqlDataReader readerItens = cmdItens.ExecuteReader())
                        {
                            while (readerItens.Read())
                            {
                                pedido.Itens.Add(new ItemPedido
                                {
                                    PedidoId = readerItens.GetInt32("pedidos_id"),
                                    ProdutoId = readerItens.GetInt32("produto_id"),
                                    NumeroMesa = readerItens.GetInt32("numero_mesa"),
                                    Quantidade = readerItens.GetInt32("quantidade"),
                                    PrecoUnitario = readerItens.GetDecimal("precoUnitario"),
                                    Status = readerItens.GetString("Status"),
                                    NomeProduto = readerItens.GetString("NomeProduto")
                                });
                            }
                        }
                    }
                }
            }
            return pedido;
        }

        public List<Pedido> ListarPedidos()
        {
            List<Pedido> lista = new List<Pedido>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT p.*, f.Funcionario AS NomeFuncionario FROM Pedidos p
                                 LEFT JOIN Funcionarios f ON p.FuncionarioId = f.Id
                                 ORDER BY p.DataPedido DESC";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Pedido
                            {
                                Id = reader.GetInt32("Id"),
                                MesaNumero = reader.GetInt32("MesaNumero"),
                                FuncionarioId = reader.GetInt32("FuncionarioId"),
                                VendaId = reader.IsDBNull(reader.GetOrdinal("VendaId")) ? (int?)null : reader.GetInt32("VendaId"),
                                Observacao = reader.GetString("Observacao"),
                                Status = reader.GetString("Status"),
                                DataPedido = reader.GetDateTime("DataPedido"),
                                Ativo = reader.GetBoolean("Ativo"),
                                NomeFuncionario = reader.GetString("NomeFuncionario")
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}