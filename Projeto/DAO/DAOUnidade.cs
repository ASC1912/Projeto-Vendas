using MySql.Data.MySqlClient;
using Projeto.Models;
using System;
using System.Collections.Generic;

namespace Projeto.DAO
{
    internal class DAOUnidade
    {
        private string connectionString = "Server=localhost;Database=sistema;Uid=root;Pwd=12345678;";

        public void Salvar(UnidadeMedida unidade)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query;

                    if (unidade.Id > 0)
                    {
                        query = @"UPDATE UnidadesMedida SET 
                                  Nome = @Nome, Sigla = @Sigla, Ativo = @Ativo, DataAlteracao = @DataAlteracao 
                                  WHERE Id = @Id";
                    }
                    else
                    {
                        query = @"INSERT INTO UnidadesMedida (Nome, Sigla, Ativo, DataCadastro, DataAlteracao) 
                                  VALUES (@Nome, @Sigla, @Ativo, @DataCadastro, @DataAlteracao)";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", unidade.Id);
                        cmd.Parameters.AddWithValue("@Nome", unidade.NomeUnidade);
                        cmd.Parameters.AddWithValue("@Sigla", unidade.Sigla);
                        cmd.Parameters.AddWithValue("@Ativo", unidade.Ativo);
                        cmd.Parameters.AddWithValue("@DataAlteracao", DateTime.Now);
                        if (unidade.Id == 0)
                        {
                            cmd.Parameters.AddWithValue("@DataCadastro", DateTime.Now);
                        }
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar a unidade de medida: " + ex.Message);
            }
        }

        public void Excluir(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM UnidadesMedida WHERE Id = @Id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public UnidadeMedida BuscarPorId(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Id, Nome, Sigla, Ativo, DataCadastro, DataAlteracao FROM UnidadesMedida WHERE Id = @Id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MontarObjeto(reader);
                        }
                    }
                }
            }
            return null;
        }

        public List<UnidadeMedida> ListarUnidades()
        {
            List<UnidadeMedida> lista = new List<UnidadeMedida>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Id, Nome, Sigla, Ativo, DataCadastro, DataAlteracao FROM UnidadesMedida ORDER BY Nome";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(MontarObjeto(reader));
                        }
                    }
                }
            }
            return lista;
        }

        private UnidadeMedida MontarObjeto(MySqlDataReader reader)
        {
            return new UnidadeMedida
            {
                Id = reader.GetInt32("Id"),
                NomeUnidade = reader.GetString("Nome"),
                Sigla = reader.GetString("Sigla"),
                Ativo = reader.GetBoolean("Ativo"),
                DataCadastro = reader.GetDateTime("DataCadastro"),
                DataAlteracao = reader.GetDateTime("DataAlteracao")
            };
        }
    }
}