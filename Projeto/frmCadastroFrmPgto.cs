using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Projeto
{
    public partial class frmCadastroFrmPgto : Projeto.frmBase
    {
        private string connectionString = "Server=localhost;Database=sistema;Uid=root;Pwd=;";

        public frmCadastroFrmPgto()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
        }

        public void CarregarFormaPagamento(int id, string descricao)
        {
            txtCodigo.Text = id.ToString();
            txtDescricao.Text = descricao;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query;

                    if (!string.IsNullOrEmpty(txtCodigo.Text)) 
                    {
                        query = "UPDATE formas_pagamento SET descricao = @descricao WHERE id = @id";
                    }
                    else 
                    {
                        query = "INSERT INTO formas_pagamento (descricao) VALUES (@descricao)";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (!string.IsNullOrEmpty(txtCodigo.Text))
                            cmd.Parameters.AddWithValue("@id", int.Parse(txtCodigo.Text));

                        cmd.Parameters.AddWithValue("@descricao", txtDescricao.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Dados salvos com sucesso!");

                        this.Close(); 
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar dados: {ex.Message}");
            }
        }
    }
}
