using MySql.Data.MySqlClient;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.DAO
{
    internal class DAOCliente
    {
        private string connectionString = "Server=localhost;Database=sistema;Uid=root;Pwd=12345678;";

        public void Salvar(Cliente cliente)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query;

                    if (cliente.Id > 0)
                    {
                        query = "UPDATE clientes SET nome = @nome, cpf_cnpj = @cpf_cnpj, telefone = @telefone, email = @email, endereco = @endereco, numero_endereco = @numero_endereco, complemento = @complemento, bairro = @bairro, cep = @cep, id_cidade = @id_cidade, tipo = @tipo WHERE id = @id";
                    }
                    else
                    {
                        query = "INSERT INTO clientes (nome, cpf_cnpj, telefone, email, endereco, numero, complemento, bairro, cep, id_cidade, tipo) VALUES (@nome, @cpf_cnpj, @telefone, @email, @endereco, @numero, @complemento, @bairro, @cep, @id_cidade, @tipo)";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (cliente.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@id", cliente.Id);
                        }

                        cmd.Parameters.AddWithValue("@nome", cliente.Nome);
                        cmd.Parameters.AddWithValue("@cpf_cnpj", cliente.CPF_CNPJ);
                        cmd.Parameters.AddWithValue("@telefone", cliente.Telefone);
                        cmd.Parameters.AddWithValue("@email", cliente.Email);
                        cmd.Parameters.AddWithValue("@endereco", cliente.Endereco);
                        cmd.Parameters.AddWithValue("@numero_endereco", cliente.NumeroEndereco);
                        cmd.Parameters.AddWithValue("@complemento", cliente.Complemento);
                        cmd.Parameters.AddWithValue("@bairro", cliente.Bairro);
                        cmd.Parameters.AddWithValue("@cep", cliente.CEP);
                        cmd.Parameters.AddWithValue("@id_cidade", cliente.IdCidade);
                        cmd.Parameters.AddWithValue("@tipo", cliente.Tipo);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar cliente: " + ex.Message);
            }
        }


        public void Excluir(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM clientes WHERE id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Cliente> ListarClientes()
        {
            List<Cliente> lista = new List<Cliente>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                SELECT c.id, c.nome, c.cpf_cnpj, c.telefone, c.email, c.endereco, c.numero_endereco, c.complemento, c.bairro, c.cep, c.id_cidade, ci.nome AS cidade_nome, c.tipo
                FROM clientes c
                LEFT JOIN cidades ci ON c.id_cidade = ci.id
                ORDER BY c.nome";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Cliente
                            {
                                Id = reader.GetInt32("id"),
                                Nome = reader.GetString("nome"),
                                CPF_CNPJ = reader.GetString("cpf_cnpj"),
                                Telefone = reader.IsDBNull(reader.GetOrdinal("telefone")) ? null : reader.GetString("telefone"),
                                Email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString("email"),
                                Endereco = reader.IsDBNull(reader.GetOrdinal("endereco")) ? null : reader.GetString("endereco"),
                                NumeroEndereco = reader.IsDBNull(reader.GetOrdinal("numero_endereco")) ? (int?)null : reader.GetInt32("numero_endereco"),
                                Complemento = reader.IsDBNull(reader.GetOrdinal("complemento")) ? null : reader.GetString("complemento"),
                                Bairro = reader.IsDBNull(reader.GetOrdinal("bairro")) ? null : reader.GetString("bairro"),
                                CEP = reader.IsDBNull(reader.GetOrdinal("cep")) ? null : reader.GetString("cep"),
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
