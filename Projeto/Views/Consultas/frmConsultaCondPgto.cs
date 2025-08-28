using MySql.Data.MySqlClient;
using Projeto.Controller;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks; 
using System.Windows.Forms;

namespace Projeto
{
    public partial class frmConsultaCondPgto : Projeto.frmBaseConsulta
    {
        private CondicaoPagamentoController controller = new CondicaoPagamentoController();
        public bool ModoSelecao { get; set; } = false;
        internal CondicaoPagamento CondicaoSelecionado { get; private set; }

        public frmConsultaCondPgto() : base()
        {
            InitializeComponent();
        }

        private async void frmConsultaCondPgto_Load(object sender, EventArgs e)
        {
            await CarregarCondicoesPagamento();
            btnSelecionar.Visible = ModoSelecao;

            // ... (seu código de ajuste de colunas não muda) ...
        }

        private async Task CarregarCondicoesPagamento()
        {
            try
            {
                listView1.Items.Clear();
                List<CondicaoPagamento> condicaoPagamento = await controller.ListarCondicaoPagamento();

                foreach (var cond in condicaoPagamento)
                {
                    ListViewItem item = new ListViewItem(cond.Id.ToString());
                    item.SubItems.Add(cond.Descricao);
                    item.SubItems.Add(cond.QtdParcelas.ToString());
                    item.SubItems.Add(cond.Juros.ToString("0.00"));
                    item.SubItems.Add(cond.Multa.ToString("0.00"));
                    item.SubItems.Add(cond.Desconto.ToString("0.00"));
                    item.SubItems.Add(cond.Ativo ? "Ativo" : "Inativo");
                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar as condições de pagamento: " + ex.Message);
            }
        }

        private async void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                listView1.Items.Clear();
                string texto = txtPesquisar.Text.Trim();

                if (string.IsNullOrEmpty(texto))
                {
                    await CarregarCondicoesPagamento();
                }
                else if (int.TryParse(texto, out int id))
                {
                    CondicaoPagamento cond = await controller.BuscarPorId(id);

                    if (cond != null)
                    {
                        ListViewItem item = new ListViewItem(cond.Id.ToString());
                        item.SubItems.Add(cond.Descricao);
                        item.SubItems.Add(cond.QtdParcelas.ToString());
                        item.SubItems.Add(cond.Juros.ToString("0.00"));
                        item.SubItems.Add(cond.Multa.ToString("0.00"));
                        item.SubItems.Add(cond.Desconto.ToString("0.00"));
                        item.SubItems.Add(cond.Ativo ? "Ativo" : "Inativo");
                        listView1.Items.Add(item);
                    }
                    else
                    {
                        MessageBox.Show("Condição de pagamento não encontrada.");
                    }
                }
                else
                {
                    MessageBox.Show("Digite um ID válido (número inteiro).");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao pesquisar: " + ex.Message);
            }
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            frmCadastroCondPgto formCadastroPagamento = new frmCadastroCondPgto();
            formCadastroPagamento.FormClosed += async (s, args) => await CarregarCondicoesPagamento();
            formCadastroPagamento.ShowDialog();
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);

                CondicaoPagamento condicao = await controller.BuscarPorId(id);

                if (condicao != null)
                {
                    var formCadastro = new frmCadastroCondPgto { modoEdicao = true };
                    formCadastro.CarregarCondicaoPagamento(condicao.Id, condicao.Descricao, condicao.QtdParcelas, condicao.Juros, condicao.Multa, condicao.Desconto, condicao.Ativo, condicao.DataCadastro, condicao.DataAlteracao);
                    formCadastro.FormClosed += async (s, args) => await CarregarCondicoesPagamento();
                    formCadastro.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Condição de pagamento não encontrada.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um item para editar.");
            }
        }

        private async void btnDeletar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);

                CondicaoPagamento condicao = await controller.BuscarPorId(id);

                if (condicao != null)
                {
                    var formCadastro = new frmCadastroCondPgto { modoExclusao = true };
                    formCadastro.CarregarCondicaoPagamento(condicao.Id, condicao.Descricao, condicao.QtdParcelas, condicao.Juros, condicao.Multa, condicao.Desconto, condicao.Ativo, condicao.DataCadastro, condicao.DataAlteracao);
                    formCadastro.FormClosed += async (s, args) => await CarregarCondicoesPagamento();
                    formCadastro.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Condição de pagamento não encontrada.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma condição de pagamento para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnSelecionar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);
                CondicaoSelecionado = await controller.BuscarPorId(id);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor, selecione um nome.");
            }
        }
    }
}