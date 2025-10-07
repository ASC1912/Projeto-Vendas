using MySql.Data.MySqlClient;
using Projeto.Models;
using System;
using System.Collections.Generic;

namespace Projeto.DAO
{
    internal class DAOProduto
    {
        private string connectionString = "Server=localhost;Database=sistema;Uid=root;Pwd=12345678;";

        public void Salvar(Produto produto)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query;

                    if (produto.Id > 0)
                    {
                        query = @"
                        UPDATE Produtos SET 
                            Produto = @Produto,
                            Descricao = @Descricao,
                            PrecoCusto = @PrecoCusto,
                            PrecoVenda = @PrecoVenda,
                            PorcentagemLucro = @PorcentagemLucro,
                            Estoque = @Estoque,
                            IdMarca = @IdMarca,
                            GrupoId = @GrupoId,
                            UnidadeMedidaId = @UnidadeMedidaId,
                            Ativo = @Ativo,
                            DataAlteracao = @DataAlteracao
                        WHERE Id = @Id";
                    }
                    else
                    {
                        query = @"
                        INSERT INTO Produtos (
                            Produto, Descricao, PrecoCusto, PrecoVenda, PorcentagemLucro, Estoque, IdMarca, GrupoId, UnidadeMedidaId, Ativo, DataCadastro, DataAlteracao
                        ) VALUES (
                            @Produto, @Descricao, @PrecoCusto, @PrecoVenda, @PorcentagemLucro, @Estoque, @IdMarca, @GrupoId, @UnidadeMedidaId, @Ativo, @DataCadastro, @DataAlteracao
                        )";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Produto", produto.NomeProduto);
                        cmd.Parameters.AddWithValue("@Descricao", string.IsNullOrEmpty(produto.Descricao) ? (object)DBNull.Value : produto.Descricao);
                        cmd.Parameters.AddWithValue("@PrecoCusto", produto.PrecoCusto);
                        cmd.Parameters.AddWithValue("@PrecoVenda", produto.PrecoVenda);
                        cmd.Parameters.AddWithValue("@PorcentagemLucro", produto.PorcentagemLucro);
                        cmd.Parameters.AddWithValue("@Estoque", produto.Estoque);
                        cmd.Parameters.AddWithValue("@IdMarca", produto.IdMarca ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@GrupoId", produto.GrupoId);
                        cmd.Parameters.AddWithValue("@UnidadeMedidaId", produto.UnidadeMedidaId);
                        cmd.Parameters.AddWithValue("@Ativo", produto.Ativo);
                        cmd.Parameters.AddWithValue("@DataAlteracao", DateTime.Now);

                        if (produto.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@Id", produto.Id);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@DataCadastro", DateTime.Now);
                        }

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar produto: " + ex.Message);
            }
        }

        public void Excluir(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Produtos WHERE Id = @Id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Produto BuscarPorId(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT p.*, m.Marca AS NomeMarca, g.Grupo AS NomeGrupo, um.Nome AS NomeUnidadeMedida
                    FROM Produtos p
                    LEFT JOIN Marcas m ON p.IdMarca = m.Id
                    LEFT JOIN Grupos g ON p.GrupoId = g.Id
                    LEFT JOIN UnidadesMedida um ON p.UnidadeMedidaId = um.Id
                    WHERE p.Id = @Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MontarObjetoProduto(reader);
                        }
                    }
                }
            }
            return null;
        }

        public List<Produto> ListarProdutos()
        {
            List<Produto> lista = new List<Produto>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT p.*, m.Marca AS NomeMarca, g.Grupo AS NomeGrupo, um.Nome AS NomeUnidadeMedida
                    FROM Produtos p
                    LEFT JOIN Marcas m ON p.IdMarca = m.Id
                    LEFT JOIN Grupos g ON p.GrupoId = g.Id
                    LEFT JOIN UnidadesMedida um ON p.UnidadeMedidaId = um.Id
                    ORDER BY p.Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(MontarObjetoProduto(reader));
                        }
                    }
                }
            }
            return lista;
        }

        private Produto MontarObjetoProduto(MySqlDataReader reader)
        {
            return new Produto
            {
                Id = reader.GetInt32("Id"),
                NomeProduto = reader.GetString("Produto"),
                Descricao = reader.IsDBNull(reader.GetOrdinal("Descricao")) ? null : reader.GetString("Descricao"),
                PrecoCusto = reader.GetDecimal("PrecoCusto"),
                PrecoVenda = reader.IsDBNull(reader.GetOrdinal("PrecoVenda")) ? 0 : reader.GetDecimal("PrecoVenda"),
                PorcentagemLucro = reader.IsDBNull(reader.GetOrdinal("PorcentagemLucro")) ? 0 : reader.GetDecimal("PorcentagemLucro"),
                Estoque = reader.GetInt32("Estoque"),
                IdMarca = reader.IsDBNull(reader.GetOrdinal("IdMarca")) ? (int?)null : reader.GetInt32("IdMarca"),
                NomeMarca = reader.IsDBNull(reader.GetOrdinal("NomeMarca")) ? null : reader.GetString("NomeMarca"),
                GrupoId = reader.GetInt32("GrupoId"),
                NomeGrupo = reader.IsDBNull(reader.GetOrdinal("NomeGrupo")) ? null : reader.GetString("NomeGrupo"),
                UnidadeMedidaId = reader.IsDBNull(reader.GetOrdinal("UnidadeMedidaId")) ? (int?)null : reader.GetInt32("UnidadeMedidaId"),
                NomeUnidadeMedida = reader.IsDBNull(reader.GetOrdinal("NomeUnidadeMedida")) ? null : reader.GetString("NomeUnidadeMedida"),
                Ativo = reader.GetBoolean("Ativo"),
                DataCadastro = reader.IsDBNull(reader.GetOrdinal("DataCadastro")) ? (DateTime?)null : reader.GetDateTime("DataCadastro"),
                DataAlteracao = reader.IsDBNull(reader.GetOrdinal("DataAlteracao")) ? (DateTime?)null : reader.GetDateTime("DataAlteracao")
            };
        }
    }
}