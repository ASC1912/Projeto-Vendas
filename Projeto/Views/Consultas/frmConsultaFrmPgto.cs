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
    public partial class frmConsultaFrmPgto : Projeto.frmBaseConsulta
    {
        private FormaPagamentoController controller = new FormaPagamentoController();
        public bool ModoSelecao { get; set; } = false;
        internal FormaPagamento FormaSelecionada { get; private set; }

        public frmConsultaFrmPgto() : base()
        {
            InitializeComponent();
            this.Text = "Consulta Forma de Pagamento";
        }

        private void frmConsultaFrmPgto_Load(object sender, EventArgs e)
        {
            btnSelecionar.Visible = ModoSelecao;

            foreach (ColumnHeader column in listView1.Columns)
            {
                switch (column.Text)
                {
                    case "ID":
                        column.Width = 50;
                        break;
                    case "Descricao":
                        column.Width = 100;
                        break;
                    case "Status":
                        column.Width = 50;
                        break;
                    default:
                        column.Width = 100;
                        break;
                }
            }
        }

        private void CarregarFormasPagamento()
        {
            try
            {
                listView1.Items.Clear();
                List<FormaPagamento> formasPagamento = controller.ListarFormaPagamento();

                foreach (var forma in formasPagamento)
                {
                    ListViewItem item = new ListViewItem(forma.Id.ToString());
                    item.SubItems.Add(forma.Descricao);
                    item.SubItems.Add(forma.Ativo ? "Ativo" : "Inativo");
                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar as formas de pagamento: " + ex.Message);
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
                    CarregarFormasPagamento();
                }
                else if (int.TryParse(texto, out int id))
                {
                    FormaPagamento forma = controller.BuscarPorId(id);

                    if (forma != null)
                    {
                        ListViewItem item = new ListViewItem(forma.Id.ToString());
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

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            frmCadastroFrmPgto formCadastroPagamento = new frmCadastroFrmPgto();
            formCadastroPagamento.ShowDialog();
            CarregarFormasPagamento();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);

                FormaPagamento forma = controller.BuscarPorId(id);

                if (forma != null)
                {
                    var formCadastro = new frmCadastroFrmPgto();
                    formCadastro.modoEdicao = true;

                    formCadastro.CarregarFormaPagamento(
                        forma.Id,
                        forma.Descricao,
                        forma.Ativo,
                        forma.DataCadastro,
                        forma.DataAlteracao
                    );


                    formCadastro.FormClosed += (s, args) => CarregarFormasPagamento();
                    formCadastro.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Forma de pagamento não encontrada.");
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

                var confirmacao = MessageBox.Show("Tem certeza que deseja excluir essa forma de pagamento?", "Confirmação", MessageBoxButtons.YesNo);
                if (confirmacao == DialogResult.Yes)
                {
                    try
                    {
                        controller.Excluir(id);
                        listView1.Items.Remove(itemSelecionado);
                        MessageBox.Show("Forma de pagamento excluída com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("Cannot delete or update a parent row"))
                        {
                            MessageBox.Show(
                                "Não é possível excluir este item, pois existem registros vinculados a ele.",
                                "Erro ao excluir",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                        }
                        else
                        {
                            MessageBox.Show(
                                $"Erro ao excluir: {ex.Message}",
                                "Erro ao excluir",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma forma de pagamento para excluir.");
            }
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);

                FormaSelecionada = controller.BuscarPorId(id);

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
