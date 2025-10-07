using MySql.Data.MySqlClient;
using Projeto.Models;
using System;
using System.Collections.Generic;

namespace Projeto.DAO
{
    internal class DAOTransportadora
    {
        private string connectionString = "Server=localhost;Database=sistema;Uid=root;Pwd=12345678;";

        public void Salvar(Transportadora transportadora)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query;
                    if (transportadora.Id > 0)
                    {
                        query = @"
                            UPDATE Transportadoras 
                            SET Transportadora = @Nome, CpfCnpj = @CPF_CNPJ, Telefone = @Telefone, Email = @Email, 
                                Endereco = @Endereco, NumeroEndereco = @NumeroEndereco, Complemento = @Complemento, 
                                Bairro = @Bairro, Cep = @CEP, InscricaoEstadual = @InscricaoEstadual, 
                                InscricaoEstadualSubtrib = @InscricaoEstadualSubTrib, CidadeId = @CidadeId, 
                                Tipo = @Tipo, IdCondicaoPagamento = @IdCondicao, Ativo = @Ativo,
                                DataAlteracao = @DataAlteracao
                            WHERE Id = @Id";
                    }
                    else
                    {
                        query = @"
                            INSERT INTO Transportadoras (
                                Transportadora, CpfCnpj, Telefone, Email, Endereco, NumeroEndereco, Complemento, 
                                Bairro, Cep, InscricaoEstadual, InscricaoEstadualSubtrib, CidadeId, Tipo, IdCondicaoPagamento,
                                Ativo, DataCadastro, DataAlteracao
                            ) VALUES (
                                @Nome, @CPF_CNPJ, @Telefone, @Email, @Endereco, @NumeroEndereco, @Complemento, 
                                @Bairro, @CEP, @InscricaoEstadual, @InscricaoEstadualSubTrib, @CidadeId, @Tipo, @IdCondicao,
                                @Ativo, @DataCadastro, @DataAlteracao
                            )";
                    }
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", transportadora.Id);
                        cmd.Parameters.AddWithValue("@Nome", transportadora.Nome);
                        cmd.Parameters.AddWithValue("@CPF_CNPJ", transportadora.CPF_CNPJ);
                        cmd.Parameters.AddWithValue("@Telefone", transportadora.Telefone);
                        cmd.Parameters.AddWithValue("@Email", transportadora.Email);
                        cmd.Parameters.AddWithValue("@Endereco", transportadora.Endereco);
                        cmd.Parameters.AddWithValue("@NumeroEndereco", transportadora.NumeroEndereco);
                        cmd.Parameters.AddWithValue("@Complemento", transportadora.Complemento);
                        cmd.Parameters.AddWithValue("@Bairro", transportadora.Bairro);
                        cmd.Parameters.AddWithValue("@CEP", transportadora.CEP);
                        cmd.Parameters.AddWithValue("@InscricaoEstadual", transportadora.InscricaoEstadual);
                        cmd.Parameters.AddWithValue("@InscricaoEstadualSubTrib", transportadora.InscricaoEstadualSubTrib);
                        cmd.Parameters.AddWithValue("@CidadeId", transportadora.CidadeId);
                        cmd.Parameters.AddWithValue("@Tipo", transportadora.Tipo);
                        cmd.Parameters.AddWithValue("@IdCondicao", transportadora.IdCondicao);
                        cmd.Parameters.AddWithValue("@Ativo", transportadora.Ativo);
                        cmd.Parameters.AddWithValue("@DataCadastro", transportadora.DataCadastro);
                        cmd.Parameters.AddWithValue("@DataAlteracao", transportadora.DataAlteracao);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar transportadora: " + ex.Message);
            }
        }

        public void Excluir(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Transportadoras WHERE Id = @Id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Transportadora BuscarPorId(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT 
                        t.*,
                        c.Id AS CidadeObjId, c.Cidade AS CidadeObjNome,
                        e.Id AS EstadoObjId, e.Estado AS EstadoObjNome, e.UF,
                        p.Id AS PaisObjId, p.Pais AS PaisObjNome,
                        cp.Id as CondicaoObjId, cp.Descricao as CondicaoObjDescricao
                    FROM Transportadoras t
                    LEFT JOIN Cidades c ON t.CidadeId = c.Id
                    LEFT JOIN Estados e ON c.EstadoId = e.Id
                    LEFT JOIN Paises p ON e.PaisId = p.Id
                    LEFT JOIN CondicoesPagamento cp ON t.IdCondicaoPagamento = cp.Id
                    WHERE t.Id = @Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MontarObjetoTransportadora(reader);
                        }
                    }
                }
            }
            return null;
        }

        public List<Transportadora> ListarTransportadoras() 
        {
            List<Transportadora> lista = new List<Transportadora>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT 
                        t.*,
                        c.Id AS CidadeObjId, c.Cidade AS CidadeObjNome,
                        e.Id AS EstadoObjId, e.Estado AS EstadoObjNome, e.UF,
                        p.Id AS PaisObjId, p.Pais AS PaisObjNome,
                        cp.Id as CondicaoObjId, cp.Descricao as CondicaoObjDescricao
                    FROM Transportadoras t
                    LEFT JOIN Cidades c ON t.CidadeId = c.Id
                    LEFT JOIN Estados e ON c.EstadoId = e.Id
                    LEFT JOIN Paises p ON e.PaisId = p.Id
                    LEFT JOIN CondicoesPagamento cp ON t.IdCondicaoPagamento = cp.Id
                    ORDER BY t.Id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(MontarObjetoTransportadora(reader));
                        }
                    }
                }
            }
            return lista;
        }

        private Transportadora MontarObjetoTransportadora(MySqlDataReader reader)
        {
            Pais pais = null;
            if (reader["PaisObjId"] != DBNull.Value)
            {
                pais = new Pais
                {
                    Id = Convert.ToInt32(reader["PaisObjId"]),
                    NomePais = Convert.ToString(reader["PaisObjNome"])
                };
            }

            Estado estado = null;
            if (reader["EstadoObjId"] != DBNull.Value)
            {
                estado = new Estado
                {
                    Id = Convert.ToInt32(reader["EstadoObjId"]),
                    NomeEstado = Convert.ToString(reader["EstadoObjNome"]),
                    UF = Convert.ToString(reader["UF"]),
                    oPais = pais
                };
            }

            Cidade cidade = null;
            if (reader["CidadeObjId"] != DBNull.Value)
            {
                cidade = new Cidade
                {
                    Id = Convert.ToInt32(reader["CidadeObjId"]),
                    NomeCidade = Convert.ToString(reader["CidadeObjNome"]),
                    oEstado = estado
                };
            }


            CondicaoPagamento condicao = null;
            if (reader["CondicaoObjId"] != DBNull.Value)
            {
                condicao = new CondicaoPagamento
                {
                    Id = Convert.ToInt32(reader["CondicaoObjId"]),
                    Descricao = Convert.ToString(reader["CondicaoObjDescricao"])
                };
            }

            var transportadora = new Transportadora
            {
                Id = Convert.ToInt32(reader["Id"]),
                Nome = Convert.ToString(reader["Transportadora"]),
                CPF_CNPJ = Convert.ToString(reader["CpfCnpj"]),
                Telefone = reader["Telefone"] == DBNull.Value ? null : Convert.ToString(reader["Telefone"]),
                Email = reader["Email"] == DBNull.Value ? null : Convert.ToString(reader["Email"]),
                Endereco = reader["Endereco"] == DBNull.Value ? null : Convert.ToString(reader["Endereco"]),
                NumeroEndereco = reader["NumeroEndereco"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["NumeroEndereco"]),
                Complemento = reader["Complemento"] == DBNull.Value ? null : Convert.ToString(reader["Complemento"]),
                Bairro = reader["Bairro"] == DBNull.Value ? null : Convert.ToString(reader["Bairro"]),
                CEP = reader["Cep"] == DBNull.Value ? null : Convert.ToString(reader["Cep"]),
                Tipo = Convert.ToString(reader["Tipo"]),
                InscricaoEstadual = reader["InscricaoEstadual"] == DBNull.Value ? null : Convert.ToString(reader["InscricaoEstadual"]),
                InscricaoEstadualSubTrib = reader["InscricaoEstadualSubtrib"] == DBNull.Value ? null : Convert.ToString(reader["InscricaoEstadualSubtrib"]),
                IdCondicao = reader["IdCondicaoPagamento"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["IdCondicaoPagamento"]),
                Ativo = Convert.ToBoolean(reader["Ativo"]),
                DataCadastro = reader["DataCadastro"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["DataCadastro"]),
                DataAlteracao = reader["DataAlteracao"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["DataAlteracao"]),
                CidadeId = reader["CidadeId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["CidadeId"]),
                oCidade = cidade,
                oCondicaoPagamento = condicao,
                DescricaoCondicao = condicao?.Descricao
            };

            return transportadora;
        }
    }
}