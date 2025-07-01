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
        public bool ModoSelecao { get; set; } = false;
        internal CondicaoPagamento CondicaoSelecionado { get; private set; }

        public frmConsultaCondPgto() : base()
        {
            InitializeComponent();
        }
        private void frmConsultaCondPgto_Load(object sender, EventArgs e)
        {
            CarregarCondicoesPagamento();

            btnSelecionar.Visible = ModoSelecao;

            foreach (ColumnHeader column in listView1.Columns)
            {
                switch (column.Text)
                {
                    case "ID":
                        column.Width = 50;
                        break;
                    case "Descrição":
                        column.Width = 200;
                        break;
                    case "Qtd_parcelas":
                        column.Width = 50;
                        break;
                    case "Juros":
                        column.Width = 60;
                        break;
                    case "Multa":
                        column.Width = 60;
                        break;
                    case "Desconto":
                        column.Width = 60;
                        break;
                    case "Ativo":
                        column.Width = 50;
                        break;
                    default:
                        column.Width = 100;
                        break;
                }
            }
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

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                listView1.Items.Clear();
                string texto = txtPesquisar.Text.Trim();

                if (string.IsNullOrEmpty(texto))
                {
                    CarregarCondicoesPagamento();
                }
                else if (int.TryParse(texto, out int id))
                {
                    CondicaoPagamento cond = controller.BuscarPorId(id);

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
            formCadastroPagamento.FormClosed += (s, args) => CarregarCondicoesPagamento();
            formCadastroPagamento.ShowDialog();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);

                CondicaoPagamento condicao = new CondicaoPagamentoController().BuscarPorId(id);

                if (condicao != null)
                {
                    var formCadastro = new frmCadastroCondPgto();
                    formCadastro.modoEdicao = true;

                    formCadastro.CarregarCondicaoPagamento(
                        condicao.Id,
                        condicao.Descricao,
                        condicao.QtdParcelas,
                        condicao.Juros,
                        condicao.Multa,
                        condicao.Desconto,
                        condicao.Ativo,
                        condicao.DataCadastro,
                        condicao.DataAlteracao
                    );

                    formCadastro.FormClosed += (s, args) => CarregarCondicoesPagamento();
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

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);
                string descricao = itemSelecionado.SubItems[1].Text;

                CondicaoSelecionado = new CondicaoPagamento
                {
                    Id = id,
                    Descricao = descricao,
                };

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
