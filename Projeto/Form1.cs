using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CarregarFormasPagamento();
            CarregarCondiecoesPagamento();
        }

      
        private void CarregarFormasPagamento()
        {
            cbFormaPagamento.Items.Clear();

            string conexao = "Server=localhost;Database=sistema;Uid=root;Pwd=;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(conexao))
                {
                    conn.Open();

                    string query = "SELECT descricao FROM formas_pagamento";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                cbFormaPagamento.Items.Add(reader.GetString("descricao"));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar as formas de pagamento: " + ex.Message);
            }
        }

        private void CarregarCondiecoesPagamento()
        {
            string conexao = "Server=localhost;Database=sistema;Uid=root;Pwd=;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(conexao))
                {
                    conn.Open();

                    string query = "SELECT prazo_dias FROM condicoes_pagamento";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            cbCondicaoPagamento.Items.Clear();

                            while (reader.Read())
                            {
                                cbCondicaoPagamento.Items.Add(reader.GetInt32("prazo_dias"));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar as condições de pagamento: " + ex.Message);
            }
        }

        private void btnGerarParcelas_Click(object sender, EventArgs e)
        {
            
        }

        private void formaDePagamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCadastroFrmPgto formCadastroPagamento = new frmCadastroFrmPgto();
            formCadastroPagamento.ShowDialog();
        }


        private void condiçãoDePagamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCadastroCondPgto formCadastroCondPgto = new frmCadastroCondPgto();
            formCadastroCondPgto.ShowDialog();
        }

        private void formaDePagamentoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmConsultaFrmPgto formConsultaFrmPgto = new frmConsultaFrmPgto();
            formConsultaFrmPgto.ShowDialog();
        }

        private void condiçãoDePagamentoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmConsultaCondPgto formConsultaCondPgto = new frmConsultaCondPgto();
            formConsultaCondPgto.ShowDialog();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            CarregarFormasPagamento();
            CarregarCondiecoesPagamento();
        }
    }
}
