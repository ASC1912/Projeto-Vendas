using Projeto.Controller;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Projeto.Views
{
    public partial class frmConsultaFornecedor : Projeto.frmBaseConsulta
    {
        private FornecedorController controller = new FornecedorController();

        public frmConsultaFornecedor()
        {
            InitializeComponent();
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            frmCadastroFornecedor formCadastroFornecedor = new frmCadastroFornecedor();
            formCadastroFornecedor.ShowDialog();
        }

        private void CarregarFornecedores()
        {
            try
            {
                listView1.Items.Clear();
                List<Fornecedor> fornecedores = controller.ListarFornecedor();

                foreach (var fornecedor in fornecedores)
                {
                    ListViewItem item = new ListViewItem(fornecedor.Id.ToString());
                    item.SubItems.Add(fornecedor.Nome);
                    item.SubItems.Add(fornecedor.CPF_CNPJ);
                    item.SubItems.Add(fornecedor.Telefone);
                    item.SubItems.Add(fornecedor.Email);
                    item.SubItems.Add(fornecedor.Endereco);
                    item.SubItems.Add(fornecedor.NumeroEndereco.ToString());
                    item.SubItems.Add(fornecedor.Bairro);
                    item.SubItems.Add(fornecedor.Complemento);
                    item.SubItems.Add(fornecedor.CEP);
                    item.SubItems.Add(fornecedor.Tipo);
                    item.SubItems.Add(fornecedor.InscricaoEstadual);
                    item.SubItems.Add(fornecedor.InscricaoEstadualSubTrib);
                    item.SubItems.Add(fornecedor.NomeCidade);

                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar fornecedor: " + ex.Message);
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            CarregarFornecedores();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);
                string nome = itemSelecionado.SubItems[1].Text;
                string cpf_cnpj = itemSelecionado.SubItems[2].Text;
                string telefone = itemSelecionado.SubItems[3].Text;
                string email = itemSelecionado.SubItems[4].Text;
                string endereco = itemSelecionado.SubItems[5].Text;
                string numEnderecoStr = itemSelecionado.SubItems[6].Text;
                int numEndereco = string.IsNullOrWhiteSpace(numEnderecoStr) ? 0 : int.Parse(numEnderecoStr);
                string bairro = itemSelecionado.SubItems[7].Text;
                string complemento = itemSelecionado.SubItems[8].Text;
                string cep = itemSelecionado.SubItems[9].Text;
                string tipo = itemSelecionado.SubItems[10].Text;
                string insEst = itemSelecionado.SubItems[11].Text;
                string insEstSubTrib = itemSelecionado.SubItems[12].Text;
                string nomeCidade = itemSelecionado.SubItems[13].Text;

                var formCadastro = new frmCadastroFornecedor();
                formCadastro.CarregarFornecedor(id, nome, cpf_cnpj, telefone, email, endereco, numEndereco, bairro, complemento, cep, insEst, insEstSubTrib, tipo, nomeCidade);

                formCadastro.FormClosed += (s, args) => CarregarFornecedores();
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

                var confirmacao = MessageBox.Show("Tem certeza que deseja excluir esse Fornecedor?", "Confirmação", MessageBoxButtons.YesNo);
                if (confirmacao == DialogResult.Yes)
                {
                    try
                    {
                        controller.Excluir(id);
                        listView1.Items.Remove(itemSelecionado);
                        MessageBox.Show("Fornecedor excluído com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao excluir: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um fornecedor para excluir.");
            }
        }
    }
}
