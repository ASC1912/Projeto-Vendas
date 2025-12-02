using Projeto.Controller;
using Projeto.Models;
using Projeto.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto
{
    public partial class frmConsultaFrmPgto : Projeto.frmBaseConsulta
    {
        frmCadastroFrmPgto oFrmCadastroFrmPgto;

        private FormaPagamentoController controller = new FormaPagamentoController();
        internal FormaPagamento FormaSelecionada { get; private set; }

        public frmConsultaFrmPgto() : base()
        {
            InitializeComponent();
            this.Text = "Consulta Forma de Pagamento";
        }

        public override void setFrmCadastro(object obj)
        {
            if (obj != null)
            {
                oFrmCadastroFrmPgto = (frmCadastroFrmPgto)obj;
            }
        }

        public override void ConhecaObj(object obj, object ctrl)
        {
            if (ctrl != null)
            {
                controller = (FormaPagamentoController)ctrl;
            }
        }
        private async void frmConsultaFrmPgto_Load(object sender, EventArgs e)
        {
            await CarregarFormasPagamento();
            btnSelecionar.Visible = ModoSelecao;
        }

        private async Task CarregarFormasPagamento()
        {
            try
            {
                listView1.Items.Clear();
                List<FormaPagamento> formasPagamento = await controller.ListarFormaPagamento();

                if (formasPagamento != null)
                {
                    foreach (var forma in formasPagamento)
                    {
                        ListViewItem item = new ListViewItem("");
                        item.SubItems.Add(forma.Id.ToString());
                        item.SubItems.Add(forma.Descricao);
                        item.SubItems.Add(forma.Ativo ? "Ativo" : "Inativo");
                        listView1.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar as formas de pagamento: " + ex.Message);
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
                    await CarregarFormasPagamento();
                }
                else if (int.TryParse(texto, out int id))
                {
                    FormaPagamento forma = await controller.BuscarPorId(id);

                    if (forma != null)
                    {
                        ListViewItem item = new ListViewItem("");
                        item.SubItems.Add(forma.Id.ToString());
                        item.SubItems.Add(forma.Descricao);
                        item.SubItems.Add(forma.Ativo ? "Ativo" : "Inativo");
                        listView1.Items.Add(item);
                    }
                    else
                    {
                        MessageBox.Show("Forma de pagamento não encontrada.");
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
            if (oFrmCadastroFrmPgto != null)
            {
                oFrmCadastroFrmPgto.modoEdicao = false;
                oFrmCadastroFrmPgto.modoExclusao = false;
                oFrmCadastroFrmPgto.LimparTxt();
                oFrmCadastroFrmPgto.ShowDialog();
                await CarregarFormasPagamento();
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                if (oFrmCadastroFrmPgto != null)
                {
                    var itemSelecionado = listView1.SelectedItems[0];
                    int id = int.Parse(itemSelecionado.SubItems[1].Text);
                    FormaPagamento forma = await controller.BuscarPorId(id);

                    if (forma != null)
                    {
                        oFrmCadastroFrmPgto.modoEdicao = true;
                        oFrmCadastroFrmPgto.modoExclusao = false;
                        oFrmCadastroFrmPgto.ConhecaObj(forma, controller);
                        oFrmCadastroFrmPgto.LimparTxt();
                        oFrmCadastroFrmPgto.CarregaTxt();
                        oFrmCadastroFrmPgto.ShowDialog();
                        await CarregarFormasPagamento();
                    }
                    else
                    {
                        MessageBox.Show("Forma de pagamento não encontrada.");
                    }
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
                if (oFrmCadastroFrmPgto != null)
                {
                    var itemSelecionado = listView1.SelectedItems[0];
                    int id = int.Parse(itemSelecionado.SubItems[1].Text);
                    FormaPagamento forma = await controller.BuscarPorId(id);

                    if (forma != null)
                    {
                        oFrmCadastroFrmPgto.modoExclusao = true;
                        oFrmCadastroFrmPgto.modoEdicao = false;
                        oFrmCadastroFrmPgto.ConhecaObj(forma, controller);
                        oFrmCadastroFrmPgto.LimparTxt();
                        oFrmCadastroFrmPgto.CarregaTxt();
                        oFrmCadastroFrmPgto.BloquearTxt();
                        oFrmCadastroFrmPgto.btnSalvar.Text = "Excluir";
                        oFrmCadastroFrmPgto.ShowDialog();
                        oFrmCadastroFrmPgto.btnSalvar.Text = "Salvar";
                        oFrmCadastroFrmPgto.DesbloquearTxt();
                        await CarregarFormasPagamento();
                    }
                    else
                    {
                        MessageBox.Show("Forma de pagamento não encontrada.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma forma de pagamento para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnSelecionar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[1].Text);
                FormaSelecionada = await controller.BuscarPorId(id);

                if (FormaSelecionada != null)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Erro ao carregar os dados da forma de pagamento.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma forma de pagamento.");
            }
        }
    }
}