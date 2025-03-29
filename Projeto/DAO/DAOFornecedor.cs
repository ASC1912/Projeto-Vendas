using MySql.Data.MySqlClient;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.DAO
{
    internal class DAOFornecedor
    {
        private string connectionString = "Server=localhost;Database=sistema;Uid=root;Pwd=12345678;";

        public void Salvar(Fornecedor fornecedor)
{
    try
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query;

            if (fornecedor.Id > 0)
            {
                query = @"
                UPDATE fornecedores 
                SET nome = @nome, cpf_cnpj = @cpf_cnpj, telefone = @telefone, email = @email, endereco = @endereco, numero_endereco = @numero_endereco, complemento = @complemento, bairro = @bairro, cep = @cep, inscricao_estadual = @inscricao_estadual, inscricao_estadual_subtrib = @inscricao_estadual_subtrib, id_cidade = @id_cidade, tipo = @tipo 
                WHERE id = @id";
            }
            else
            {
                query = @"
                INSERT INTO fornecedores (nome, cpf_cnpj, telefone, email, endereco, numero_endereco, complemento, bairro, cep, inscricao_estadual, inscricao_estadual_subtrib, id_cidade, tipo) 
                VALUES (@nome, @cpf_cnpj, @telefone, @email, @endereco, @numero_endereco, @complemento, @bairro, @cep, @inscricao_estadual, @inscricao_estadual_subtrib, @id_cidade, @tipo)";
            }

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                if (fornecedor.Id > 0)
                {
                    cmd.Parameters.AddWithValue("@id", fornecedor.Id);
                }

                cmd.Parameters.AddWithValue("@nome", fornecedor.Nome);
                cmd.Parameters.AddWithValue("@cpf_cnpj", fornecedor.CPF_CNPJ);
                cmd.Parameters.AddWithValue("@telefone", fornecedor.Telefone);
                cmd.Parameters.AddWithValue("@email", fornecedor.Email);
                cmd.Parameters.AddWithValue("@endereco", fornecedor.Endereco);
                cmd.Parameters.AddWithValue("@numero_endereco", fornecedor.NumeroEndereco);
                cmd.Parameters.AddWithValue("@complemento", fornecedor.Complemento);
                cmd.Parameters.AddWithValue("@bairro", fornecedor.Bairro);
                cmd.Parameters.AddWithValue("@cep", fornecedor.CEP);
                cmd.Parameters.AddWithValue("@inscricao_estadual", fornecedor.InscricaoEstadual);
                cmd.Parameters.AddWithValue("@inscricao_estadual_subtrib", fornecedor.InscricaoEstadualSubTrib);
                cmd.Parameters.AddWithValue("@id_cidade", fornecedor.IdCidade);
                cmd.Parameters.AddWithValue("@tipo", fornecedor.Tipo);
                cmd.ExecuteNonQuery();
            }
        }
    }
    catch (Exception ex)
    {
        throw new Exception("Erro ao salvar fornecedor: " + ex.Message);
    }
}



        public void Excluir(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM fornecedores WHERE id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Fornecedor> ListarFornecedores()
        {
            List<Fornecedor> lista = new List<Fornecedor>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                SELECT f.id, f.nome, f.cpf_cnpj, f.telefone, f.email, f.endereco, f.numero_endereco, f.complemento, f.bairro, f.cep, f.inscricao_estadual, f.inscricao_estadual_subtrib, f.id_cidade, ci.nome AS cidade_nome, f.tipo
                FROM fornecedores f
                LEFT JOIN cidades ci ON f.id_cidade = ci.id
                ORDER BY f.nome";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Fornecedor
                            {
                                Id = reader.GetInt32("id"),
                                Nome = reader.GetString("nome"),
                                CPF_CNPJ = reader.GetString("cpf_cnpj"),
                                Telefone = reader.IsDBNull(reader.GetOrdinal("telefone")) ? null : reader.GetString("telefone"),
                                Email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString("email"),
                                Endereco = reader.IsDBNull(reader.GetOrdinal("endereco")) ? null : reader.GetString("endereco"),
                                NumeroEndereco = reader.IsDBNull(reader.GetOrdinal("numero_endereco")) ? 0 : reader.GetInt32("numero_endereco"),
                                Complemento = reader.IsDBNull(reader.GetOrdinal("complemento")) ? null : reader.GetString("complemento"),
                                Bairro = reader.IsDBNull(reader.GetOrdinal("bairro")) ? null : reader.GetString("bairro"),
                                CEP = reader.IsDBNull(reader.GetOrdinal("cep")) ? null : reader.GetString("cep"),
                                InscricaoEstadual = reader.IsDBNull(reader.GetOrdinal("inscricao_estadual")) ? null : reader.GetString("inscricao_estadual"),
                                InscricaoEstadualSubTrib = reader.IsDBNull(reader.GetOrdinal("inscricao_estadual_subtrib")) ? null : reader.GetString("inscricao_estadual_subtrib"),
                                IdCidade = reader.IsDBNull(reader.GetOrdinal("id_cidade")) ? (int?)null : reader.GetInt32("id_cidade"),
                                NomeCidade = reader.IsDBNull(reader.GetOrdinal("cidade_nome")) ? null : reader.GetString("cidade_nome"),
                                Tipo = reader.GetString("tipo")
                            });
                        }
                    }
                }
            }
            return lista;
        }

    }
}

