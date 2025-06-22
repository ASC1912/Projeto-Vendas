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
                            nome = @nome,
                            descricao = @descricao,
                            preco = @preco,
                            estoque = @estoque,
                            id_marca = @id_marca,
                            id_grupo = @id_grupo,
                            status = @status,
                            data_modificacao = @data_modificacao
                        WHERE id = @id";
                    }
                    else
                    {
                        query = @"
                        INSERT INTO produtos (
                            nome, descricao, preco, estoque, id_marca, id_grupo, status, data_criacao, data_modificacao
                        ) VALUES (
                            @nome, @descricao, @preco, @estoque, @id_marca, @id_grupo, @status, @data_criacao, @data_modificacao
                        )";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nome", produto.Nome);
                        cmd.Parameters.AddWithValue("@descricao", string.IsNullOrEmpty(produto.Descricao) ? (object)DBNull.Value : produto.Descricao);
                        cmd.Parameters.AddWithValue("@preco", produto.Preco);
                        cmd.Parameters.AddWithValue("@estoque", produto.Estoque);
                        cmd.Parameters.AddWithValue("@id_marca", produto.IdMarca ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@id_grupo", produto.IdGrupo ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@status", produto.Status);
                        cmd.Parameters.AddWithValue("@data_modificacao", produto.DataModificacao ?? DateTime.Now);

                        if (produto.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@id", produto.Id);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@data_criacao", produto.DataCriacao ?? DateTime.Now);
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
                    SELECT p.id, p.nome, p.descricao, p.preco, p.estoque, p.id_marca, m.nome AS nome_marca,
                           p.id_grupo, g.nome AS nome_grupo, p.status, p.data_criacao, p.data_modificacao
                    FROM produtos p
                    LEFT JOIN marcas m ON p.id_marca = m.id
                    LEFT JOIN grupos g ON p.id_grupo = g.id
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
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Nome = reader.GetString(reader.GetOrdinal("nome")),
                                Descricao = reader.IsDBNull(reader.GetOrdinal("descricao")) ? null : reader.GetString(reader.GetOrdinal("descricao")),
                                Preco = reader.GetDecimal(reader.GetOrdinal("preco")),
                                Estoque = reader.GetInt32(reader.GetOrdinal("estoque")),
                                IdMarca = reader.IsDBNull(reader.GetOrdinal("id_marca")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("id_marca")),
                                NomeMarca = reader.IsDBNull(reader.GetOrdinal("nome_marca")) ? null : reader.GetString(reader.GetOrdinal("nome_marca")),
                                IdGrupo = reader.IsDBNull(reader.GetOrdinal("id_grupo")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("id_grupo")),
                                NomeGrupo = reader.IsDBNull(reader.GetOrdinal("nome_grupo")) ? null : reader.GetString(reader.GetOrdinal("nome_grupo")),
                                Status = reader.GetBoolean(reader.GetOrdinal("status")),
                                DataCriacao = reader.IsDBNull(reader.GetOrdinal("data_criacao")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("data_criacao")),
                                DataModificacao = reader.IsDBNull(reader.GetOrdinal("data_modificacao")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("data_modificacao"))
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
                    SELECT p.id, p.nome, p.descricao, p.preco, p.estoque, p.id_marca, m.nome AS nome_marca,
                           p.id_grupo, g.nome AS nome_grupo, p.status, p.data_criacao, p.data_modificacao
                    FROM produtos p
                    LEFT JOIN marcas m ON p.id_marca = m.id
                    LEFT JOIN grupos g ON p.id_grupo = g.id
                    ORDER BY p.nome";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Produto
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Nome = reader.GetString(reader.GetOrdinal("nome")),
                                Descricao = reader.IsDBNull(reader.GetOrdinal("descricao")) ? null : reader.GetString(reader.GetOrdinal("descricao")),
                                Preco = reader.GetDecimal(reader.GetOrdinal("preco")),
                                Estoque = reader.GetInt32(reader.GetOrdinal("estoque")),
                                IdMarca = reader.IsDBNull(reader.GetOrdinal("id_marca")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("id_marca")),
                                NomeMarca = reader.IsDBNull(reader.GetOrdinal("nome_marca")) ? null : reader.GetString(reader.GetOrdinal("nome_marca")),
                                IdGrupo = reader.IsDBNull(reader.GetOrdinal("id_grupo")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("id_grupo")),
                                NomeGrupo = reader.IsDBNull(reader.GetOrdinal("nome_grupo")) ? null : reader.GetString(reader.GetOrdinal("nome_grupo")),
                                Status = reader.GetBoolean(reader.GetOrdinal("status")),
                                DataCriacao = reader.IsDBNull(reader.GetOrdinal("data_criacao")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("data_criacao")),
                                DataModificacao = reader.IsDBNull(reader.GetOrdinal("data_modificacao")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("data_modificacao"))
                            });
                        }
                    }
                }
            }

            return lista;
        }
    }
}
