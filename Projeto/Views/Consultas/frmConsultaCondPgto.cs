using MySql.Data.MySqlClient;
using Projeto.Controller;
using Projeto.Models;
using Projeto.Views.Cadastros;
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
        frmCadastroCondPgto oFrmCadastroCondPgto;
        private CondicaoPagamentoController controller = new CondicaoPagamentoController();
        internal CondicaoPagamento CondicaoSelecionado { get; private set; }

        public frmConsultaCondPgto() : base()
        {
            InitializeComponent();
        }

        public override void setFrmCadastro(object obj)
        {
            if (obj != null)
            {
                oFrmCadastroCondPgto = (frmCadastroCondPgto)obj;
            }
        }

        public override void ConhecaObj(object obj, object ctrl)
        {
            if (ctrl != null)
            {
                controller = (CondicaoPagamentoController)ctrl;
            }
        }

        private async void frmConsultaCondPgto_Load(object sender, EventArgs e)
        {
            await CarregarCondicoesPagamento();
            btnSelecionar.Visible = ModoSelecao;

            foreach (ColumnHeader column in listView1.Columns)
            {
                switch (column.Text)
                {
                    case "ID":
                        column.Width = 50;
                        column.TextAlign = HorizontalAlignment.Right;
                        break;
                    case "Descrição":
                        column.Width = 200;
                        column.TextAlign = HorizontalAlignment.Center;
                        break;
                    case "Qtd_Parcelas":
                        column.Width = 80;
                        column.TextAlign = HorizontalAlignment.Center;
                        break;
                    case "Juros":
                        column.Width = 60;
                        column.TextAlign = HorizontalAlignment.Center;
                        break;
                    case "Multa":
                        column.Width = 60;
                        column.TextAlign = HorizontalAlignment.Center;
                        break;
                    case "Desconto":
                        column.Width = 60;
                        column.TextAlign = HorizontalAlignment.Center;
                        break;
                    case "Ativo":
                        column.Width = 50;
                        column.TextAlign = HorizontalAlignment.Center;
                        break;
                    default:
                        column.Width = 100;
                        break;
                }
            }
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

        private async void btnIncluir_Click(object sender, EventArgs e)
        {
            oFrmCadastroCondPgto.modoEdicao = false;
            oFrmCadastroCondPgto.modoExclusao = false;
            oFrmCadastroCondPgto.LimparTxt();
            oFrmCadastroCondPgto.ShowDialog();
            await CarregarCondicoesPagamento();
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
                    oFrmCadastroCondPgto.modoEdicao = true;
                    oFrmCadastroCondPgto.modoExclusao = false;
                    oFrmCadastroCondPgto.ConhecaObj(condicao, controller);
                    oFrmCadastroCondPgto.LimparTxt();
                    oFrmCadastroCondPgto.CarregaTxt();
                    oFrmCadastroCondPgto.ShowDialog();
                    await CarregarCondicoesPagamento();
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
                    oFrmCadastroCondPgto.modoExclusao = true;
                    oFrmCadastroCondPgto.modoEdicao = false;
                    oFrmCadastroCondPgto.ConhecaObj(condicao, controller);
                    oFrmCadastroCondPgto.LimparTxt();
                    oFrmCadastroCondPgto.CarregaTxt();
                    oFrmCadastroCondPgto.BloquearTxt();
                    oFrmCadastroCondPgto.ShowDialog();
                    oFrmCadastroCondPgto.DesbloquearTxt();
                    await CarregarCondicoesPagamento();
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