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
    public partial class frmConsultaFuncionario : Projeto.frmBaseConsulta
    {
        private FuncionarioController controller = new FuncionarioController();

        public frmConsultaFuncionario()
        {
            InitializeComponent();
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            frmCadastroFuncionario formCadastroFuncionario = new frmCadastroFuncionario();
            formCadastroFuncionario.ShowDialog();
        }

        private void CarregarFuncionarios()
        {
            try
            {
                listView1.Items.Clear();
                List<Funcionario> funcionarios = controller.ListarFuncionario();

                foreach (var funcionario in funcionarios)
                {
                    ListViewItem item = new ListViewItem(funcionario.Id.ToString());
                    item.SubItems.Add(funcionario.Nome);
                    item.SubItems.Add(funcionario.CPF_CNPJ);
                    item.SubItems.Add(funcionario.Telefone);
                    item.SubItems.Add(funcionario.Email);
                    item.SubItems.Add(funcionario.Endereco);
                    item.SubItems.Add(funcionario.CEP);
                    item.SubItems.Add(funcionario.Tipo);
                    item.SubItems.Add(funcionario.Cargo);
                    item.SubItems.Add(funcionario.Salario.ToString());
                    item.SubItems.Add(funcionario.NomeCidade);

                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar funcionarios: " + ex.Message);
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            CarregarFuncionarios();
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
                string cep = itemSelecionado.SubItems[6].Text;
                string tipo = itemSelecionado.SubItems[7].Text;
                string cargo = itemSelecionado.SubItems[8].Text;
                decimal salario = Convert.ToDecimal(itemSelecionado.SubItems[9].Text);
                string nomeCidade = itemSelecionado.SubItems[10].Text;

                var formCadastro = new frmCadastroFuncionario();
                formCadastro.CarregarFuncionario(id, nome, cpf_cnpj, telefone, email, endereco, cep, cargo, salario, tipo, nomeCidade);

                formCadastro.FormClosed += (s, args) => CarregarFuncionarios();
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

                var confirmacao = MessageBox.Show("Tem certeza que deseja excluir esse Funcionário?", "Confirmação", MessageBoxButtons.YesNo);
                if (confirmacao == DialogResult.Yes)
                {
                    try
                    {
                        controller.Excluir(id);
                        listView1.Items.Remove(itemSelecionado);
                        MessageBox.Show("Funcionário excluído com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao excluir: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um funcionário para excluir.");
            }
        }
    }
}
