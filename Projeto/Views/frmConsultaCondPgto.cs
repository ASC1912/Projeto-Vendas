using MySql.Data.MySqlClient;
using Projeto.Controller;
using Projeto.Models;
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
        private CondicaoPagamentoController controller = new CondicaoPagamentoController();
        public frmConsultaCondPgto()
        {
            InitializeComponent();
        }
        private void CarregarCondicoesPagamento()
        {
            try
            {
                listView1.Items.Clear();
                List<CondicaoPagamento> condicaoPagamento = controller.ListarCondicaoPagamento();

                foreach (var cond in condicaoPagamento)
                {
                    ListViewItem item = new ListViewItem(cond.Id.ToString());
                    item.SubItems.Add(cond.Descricao);
                    item.SubItems.Add(cond.QtdParcelas.ToString());
                    listView1.Items.Add(item);
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

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            frmCadastroCondPgto formCadastroPagamento = new frmCadastroCondPgto();
            formCadastroPagamento.ShowDialog();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);
                string descricao = itemSelecionado.SubItems[1].Text;
                int qtd_parcelas = int.Parse(itemSelecionado.SubItems[2].Text);

                var formCadastro = new frmCadastroCondPgto();
                formCadastro.CarregarCondicaoPagamento(id, descricao, qtd_parcelas);

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

                var confirmacao = MessageBox.Show("Tem certeza que deseja excluir essa condição de pagamento?", "Confirmação", MessageBoxButtons.YesNo);
                if (confirmacao == DialogResult.Yes)
                {
                    try
                    {
                        controller.Excluir(id);
                        listView1.Items.Remove(itemSelecionado);
                        MessageBox.Show("Condição de pagamento excluída com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao excluir: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma condição de pagamento para excluir.");
            }
        }

        
    }
}
