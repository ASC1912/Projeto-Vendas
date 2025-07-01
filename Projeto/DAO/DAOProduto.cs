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
                        UPDATE produtos SET 
                            produto = @produto,
                            descricao = @descricao,
                            preco = @preco,
                            estoque = @estoque,
                            id_marca = @id_marca,
                            grupo_id = @grupo_id,
                            ativo = @ativo,
                            data_alteracao = @data_alteracao
                        WHERE id = @id";
                    }
                    else
                    {
                        query = @"
                        INSERT INTO produtos (
                            produto, descricao, preco, estoque, id_marca, grupo_id, ativo, data_cadastro, data_alteracao
                        ) VALUES (
                            @produto, @descricao, @preco, @estoque, @id_marca, @grupo_id, @ativo, @data_cadastro, @data_alteracao
                        )";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@produto", produto.NomeProduto);
                        cmd.Parameters.AddWithValue("@descricao", string.IsNullOrEmpty(produto.Descricao) ? (object)DBNull.Value : produto.Descricao);
                        cmd.Parameters.AddWithValue("@preco", produto.Preco);
                        cmd.Parameters.AddWithValue("@estoque", produto.Estoque);
                        cmd.Parameters.AddWithValue("@id_marca", produto.IdMarca ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@grupo_id", produto.GrupoId ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@ativo", produto.Ativo);
                        cmd.Parameters.AddWithValue("@data_alteracao", produto.DataAlteracao ?? DateTime.Now);

                        if (produto.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@id", produto.Id);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@data_cadastro", produto.DataCadastro ?? DateTime.Now);
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
                string query = "DELETE FROM produtos WHERE id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
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
                    SELECT p.id, p.produto, p.descricao, p.preco, p.estoque, p.id_marca, m.marca AS nome_marca,
                           p.grupo_id, g.grupo AS nome_grupo, p.ativo, p.data_cadastro, p.data_alteracao
                    FROM produtos p
                    LEFT JOIN marcas m ON p.id_marca = m.id
                    LEFT JOIN grupos g ON p.grupo_id = g.id
                    WHERE p.id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Produto
                            {
                                Id = reader.GetInt32("id"),
                                NomeProduto = reader.GetString("produto"),
                                Descricao = reader.IsDBNull(reader.GetOrdinal("descricao")) ? null : reader.GetString("descricao"),
                                Preco = reader.GetDecimal("preco"),
                                Estoque = reader.GetInt32("estoque"),
                                IdMarca = reader.IsDBNull(reader.GetOrdinal("id_marca")) ? (int?)null : reader.GetInt32("id_marca"),
                                NomeMarca = reader.IsDBNull(reader.GetOrdinal("nome_marca")) ? null : reader.GetString("nome_marca"),
                                GrupoId = reader.IsDBNull(reader.GetOrdinal("grupo_id")) ? (int?)null : reader.GetInt32("grupo_id"),
                                NomeGrupo = reader.IsDBNull(reader.GetOrdinal("nome_grupo")) ? null : reader.GetString("nome_grupo"),
                                Ativo = reader.GetBoolean("ativo"),
                                DataCadastro = reader.IsDBNull(reader.GetOrdinal("data_cadastro")) ? (DateTime?)null : reader.GetDateTime("data_cadastro"),
                                DataAlteracao = reader.IsDBNull(reader.GetOrdinal("data_alteracao")) ? (DateTime?)null : reader.GetDateTime("data_alteracao")
                            };
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
                    SELECT p.id, p.produto, p.descricao, p.preco, p.estoque, p.id_marca, m.marca AS nome_marca,
                           p.grupo_id, g.grupo AS nome_grupo, p.ativo, p.data_cadastro, p.data_alteracao
                    FROM produtos p
                    LEFT JOIN marcas m ON p.id_marca = m.id
                    LEFT JOIN grupos g ON p.grupo_id = g.id
                    ORDER BY p.id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Produto
                            {
                                Id = reader.GetInt32("id"),
                                NomeProduto = reader.GetString("produto"),
                                Descricao = reader.IsDBNull(reader.GetOrdinal("descricao")) ? null : reader.GetString("descricao"),
                                Preco = reader.GetDecimal("preco"),
                                Estoque = reader.GetInt32("estoque"),
                                IdMarca = reader.IsDBNull(reader.GetOrdinal("id_marca")) ? (int?)null : reader.GetInt32("id_marca"),
                                NomeMarca = reader.IsDBNull(reader.GetOrdinal("nome_marca")) ? null : reader.GetString("nome_marca"),
                                GrupoId = reader.IsDBNull(reader.GetOrdinal("grupo_id")) ? (int?)null : reader.GetInt32("grupo_id"),
                                NomeGrupo = reader.IsDBNull(reader.GetOrdinal("nome_grupo")) ? null : reader.GetString("nome_grupo"),
                                Ativo = reader.GetBoolean("ativo"),
                                DataCadastro = reader.IsDBNull(reader.GetOrdinal("data_cadastro")) ? (DateTime?)null : reader.GetDateTime("data_cadastro"),
                                DataAlteracao = reader.IsDBNull(reader.GetOrdinal("data_alteracao")) ? (DateTime?)null : reader.GetDateTime("data_alteracao")
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}
