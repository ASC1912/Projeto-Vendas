using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.DAO
{
    internal class DAOEstado
    {
        private string connectionString = "Server=localhost;Database=sistema;Uid=root;Pwd=12345678;";

        public void Salvar(Estado estado)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query;

                    if (estado.Id > 0)
                    {
                        query = "UPDATE estados SET nome = @nome, id_pais = @id_pais, status = @status WHERE id = @id";
                    }
                    else
                    {
                        query = "INSERT INTO estados (nome, id_pais, status) VALUES (@nome, @id_pais, @status)";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (estado.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@id", estado.Id);
                        }

                        cmd.Parameters.AddWithValue("@nome", estado.Nome);
                        cmd.Parameters.AddWithValue("@id_pais", estado.IdPais);
                        cmd.Parameters.AddWithValue("@status", estado.Status);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar estado: " + ex.Message);
            }
        }

        public void Excluir(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM estados WHERE id = @id";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }



        public List<Estado> ListarEstado()
        {
            List<Estado> lista = new List<Estado>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
            SELECT e.id, e.nome AS estado_nome, e.id_pais, e.status, p.nome AS pais_nome 
            FROM estados e
            JOIN paises p ON e.id_pais = p.id
            ORDER BY e.nome";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Estado
                            {
                                Id = reader.GetInt32("id"),
                                Nome = reader.GetString("estado_nome"),
                                IdPais = reader.GetInt32("id_pais"),
                                PaisNome = reader.GetString("pais_nome"),
                                Status = reader.GetBoolean("status"),
                            });
                        }
                    }
                }
            }
            return lista;
        }


    }
}
