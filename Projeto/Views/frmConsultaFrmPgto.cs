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


        public frmConsultaFrmPgto()
        {
            InitializeComponent();
            this.Text = "Consulta Forma de Pagamento";
        }

        private void frmConsultaFrmPgto_Load(object sender, EventArgs e)
        {
            btnSelecionar.Visible = ModoSelecao;
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
            CarregarFormasPagamento();
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            frmCadastroFrmPgto formCadastroPagamento = new frmCadastroFrmPgto();
            formCadastroPagamento.ShowDialog();
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
                    try
                    {
                        controller.Excluir(id);
                        listView1.Items.Remove(itemSelecionado);
                        MessageBox.Show("Forma de pagamento excluída com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao excluir: {ex.Message}");
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
                string descricao = itemSelecionado.SubItems[1].Text;

                FormaSelecionada = new FormaPagamento
                {
                    Id = id,
                    Descricao = descricao
                };

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma forma de pagamento.");
            }
        }
    }
}
