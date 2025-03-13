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
    public partial class frmConsultaFrmPgto : Projeto.frmBaseConsulta
    {
        public frmConsultaFrmPgto()
        {
            InitializeComponent();
            this.Text = "Consulta Forma de Pagamento";
        }
        private void CarregarFormasPagamento()
        {
            string conexao = "Server=localhost;Database=sistema;Uid=root;Pwd=;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(conexao))
                {
                    conn.Open();

                    string query = "SELECT id, descricao FROM formas_pagamento";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            listView1.Items.Clear();

                            while (reader.Read())
                            {
                                ListViewItem item = new ListViewItem(reader["id"].ToString()); 
                                item.SubItems.Add(reader["descricao"].ToString()); 

                                listView1.Items.Add(item);
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

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            CarregarFormasPagamento();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);
                string descricao = itemSelecionado.SubItems[1].Text;

                var formCadastro = new frmCadastroFrmPgto();
                formCadastro.CarregarFormaPagamento(id, descricao);

                formCadastro.FormClosed += (s, args) => CarregarFormasPagamento();
                formCadastro.ShowDialog();
            }
            else
            {
                MessageBox.Show("Por favor, selecione um item para editar.");
            }
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);

                var confirmacao = MessageBox.Show("Tem certeza que deseja excluir essa forma de pagamento?", "Confirmação", MessageBoxButtons.YesNo);
                if (confirmacao == DialogResult.Yes)
                {
                    string conexao = "Server=localhost;Database=sistema;Uid=root;Pwd=;";

                    try
                    {
                        using (MySqlConnection conn = new MySqlConnection(conexao))
                        {
                            conn.Open();
                            string query = "DELETE FROM formas_pagamento WHERE id = @id";

                            using (MySqlCommand cmd = new MySqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@id", id);
                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    listView1.Items.Remove(itemSelecionado);
                                    MessageBox.Show("Forma de pagamento excluída com sucesso!");
                                }
                                else
                                {
                                    MessageBox.Show("Erro ao excluir a forma de pagamento.");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma forma de pagamento para excluir.");
            }
        }
    }
}
