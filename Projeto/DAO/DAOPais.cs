using MySql.Data.MySqlClient;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.DAO
{
    internal class DAOPais
    {
        private string connectionString = "Server=localhost;Database=sistema;Uid=root;Pwd=12345678;";

        public void Salvar(Pais pais)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query;

                    if (pais.Id > 0)
                    {
                        query = "UPDATE paises SET nome = @nome WHERE id = @id";
                    }
                    else
                    {
                        query = "INSERT INTO paises (nome) VALUES (@nome)";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (pais.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@id", pais.Id);
                        }

                        cmd.Parameters.AddWithValue("@nome", pais.Nome);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar país: " + ex.Message);
            }
        }

        public void Excluir(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM paises WHERE id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        

        public List<Pais> ListarPais()
        {
            List<Pais> lista = new List<Pais>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT id, nome FROM paises ORDER BY id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Pais
                            {
                                Id = reader.GetInt32("id"),
                                Nome = reader.GetString("nome")
                            });
                        }
                    }
                }
            }
            return lista;
        }

    }
}
