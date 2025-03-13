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
    public partial class frmConsultaCondPgto : Projeto.frmBaseConsulta
    {
        public frmConsultaCondPgto()
        {
            InitializeComponent();
        }
        private void CarregarCondicoesPagamento()
        {
            string conexao = "Server=localhost;Database=sistema;Uid=root;Pwd=;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(conexao))
                {
                    conn.Open();

                    string query = "SELECT id, prazo_dias, descricao, qtd_parcelas FROM condicoes_pagamento";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            listView1.Items.Clear();

                            while (reader.Read())
                            {
                                ListViewItem item = new ListViewItem(reader["id"].ToString());
                                item.SubItems.Add(reader["prazo_dias"].ToString());
                                item.SubItems.Add(reader["descricao"].ToString());
                                item.SubItems.Add(reader["qtd_parcelas"].ToString());

                                listView1.Items.Add(item);
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

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            CarregarCondicoesPagamento();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);
                int prazo_dias = int.Parse(itemSelecionado.SubItems[1].Text);
                string descricao = itemSelecionado.SubItems[2].Text;
                int qtd_parcelas = int.Parse(itemSelecionado.SubItems[3].Text);

                var formCadastro = new frmCadastroCondPgto();
                formCadastro.CarregarCondicaoPagamento(id, prazo_dias, descricao, qtd_parcelas);

                formCadastro.FormClosed += (s, args) => CarregarCondicoesPagamento();
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
                            string query = "DELETE FROM condicoes_pagamento WHERE id = @id";

                            using (MySqlCommand cmd = new MySqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@id", id);
                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    listView1.Items.Remove(itemSelecionado);
                                    MessageBox.Show("Condição de pagamento excluída com sucesso!");
                                }
                                else
                                {
                                    MessageBox.Show("Erro ao excluir a condição de pagamento.");
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
                MessageBox.Show("Por favor, selecione um condição de pagamento para excluir.");
            }
        }
    }
}
