using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Projeto
{
    public partial class frmCadastroCondPgto : Projeto.frmBase
    {
        private string connectionString = "Server=localhost;Database=sistema;Uid=root;Pwd=;";

        public frmCadastroCondPgto()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
        }

        public void CarregarCondicaoPagamento(int id, int prazo_dias, string descricao, int qtdParcelas)
        {
            txtCodigo.Text = id.ToString();
            txtPrazoDias.Text = prazo_dias.ToString();
            txtDescricao.Text = descricao.ToString();
            txtQtdParcelas.Text = qtdParcelas.ToString();
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
                        query = "UPDATE condicoes_pagamento SET prazo_dias = @prazo_dias, descricao = @descricao, qtd_parcelas = @qtd_parcelas  WHERE id = @id";
                    }
                    else
                    {
                        query = "INSERT INTO condicoes_pagamento (prazo_dias, descricao, qtd_parcelas) VALUES (@prazo_dias, @descricao, @qtd_parcelas)";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (!string.IsNullOrEmpty(txtCodigo.Text))
                            cmd.Parameters.AddWithValue("@id", int.Parse(txtCodigo.Text));

                        cmd.Parameters.AddWithValue("@prazo_dias", int.Parse(txtPrazoDias.Text));
                        cmd.Parameters.AddWithValue("@descricao", txtDescricao.Text);
                        cmd.Parameters.AddWithValue("@qtd_parcelas", int.Parse(txtQtdParcelas.Text));

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Dados salvos com sucesso!");

                        this.Close();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
        }
    }
}
