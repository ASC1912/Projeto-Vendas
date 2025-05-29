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

        public frmConsultaFuncionario() : base()
        {
            InitializeComponent();
            this.Shown += frmConsultaFuncionario_Shown;
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
                    item.SubItems.Add(funcionario.NumeroEndereco.ToString());
                    item.SubItems.Add(funcionario.Bairro);
                    item.SubItems.Add(funcionario.Complemento);
                    item.SubItems.Add(funcionario.CEP);
                    item.SubItems.Add(funcionario.Tipo);
                    item.SubItems.Add(funcionario.Cargo);
                    item.SubItems.Add(funcionario.Salario.ToString());
                    item.SubItems.Add(funcionario.NomeCidade);
                    item.SubItems.Add(funcionario.DataAdmissao?.ToShortDateString() ?? "");
                    item.SubItems.Add(funcionario.DataDemissao?.ToShortDateString() ?? "");
                    item.SubItems.Add(funcionario.Status ? "Ativo" : "Inativo");
                    item.SubItems.Add(funcionario.Rg);

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
            try
            {
                listView1.Items.Clear();
                string texto = txtPesquisar.Text.Trim();

                if (string.IsNullOrEmpty(texto))
                {
                    CarregarFuncionarios();
                }
                else if (int.TryParse(texto, out int id))
                {
                    Funcionario funcionario = controller.BuscarPorId(id);

                    if (funcionario != null)
                    {
                        ListViewItem item = new ListViewItem(funcionario.Id.ToString());
                        item.SubItems.Add(funcionario.Nome);
                        item.SubItems.Add(funcionario.CPF_CNPJ);
                        item.SubItems.Add(funcionario.Telefone);
                        item.SubItems.Add(funcionario.Email);
                        item.SubItems.Add(funcionario.Endereco);
                        item.SubItems.Add(funcionario.NumeroEndereco?.ToString() ?? "0");
                        item.SubItems.Add(funcionario.Bairro);
                        item.SubItems.Add(funcionario.Complemento);
                        item.SubItems.Add(funcionario.CEP);
                        item.SubItems.Add(funcionario.Tipo);
                        item.SubItems.Add(funcionario.Cargo);
                        item.SubItems.Add(funcionario.Salario.ToString("F2"));
                        item.SubItems.Add(funcionario.NomeCidade ?? "-");
                        item.SubItems.Add(funcionario.DataAdmissao?.ToShortDateString() ?? "");
                        item.SubItems.Add(funcionario.DataDemissao?.ToShortDateString() ?? "");
                        item.SubItems.Add(funcionario.Status ? "Ativo" : "Inativo");
                        item.SubItems.Add(funcionario.Rg ?? "");

                        listView1.Items.Add(item);
                    }
                    else
                    {
                        MessageBox.Show("Funcionário não encontrado.");
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


        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);

                Funcionario funcionario = controller.BuscarPorId(id);

                if (funcionario != null)
                {
                    Cidade cidade = funcionario.IdCidade.HasValue
                        ? new CidadeController().BuscarPorId(funcionario.IdCidade.Value)
                        : null;

                    var formCadastro = new frmCadastroFuncionario();
                    formCadastro.modoEdicao = true;
                    formCadastro.CarregarFuncionario(
                        funcionario.Id,
                        funcionario.Nome,
                        funcionario.CPF_CNPJ,
                        funcionario.Telefone,
                        funcionario.Email,
                        funcionario.Endereco,
                        funcionario.NumeroEndereco ?? 0,
                        funcionario.Bairro,
                        funcionario.Complemento,
                        funcionario.CEP,
                        funcionario.Cargo,
                        funcionario.Salario,
                        funcionario.Tipo,
                        cidade?.Nome ?? "Não encontrado",
                        funcionario.IdCidade ?? 0,
                        funcionario.Status,
                        funcionario.DataAdmissao,
                        funcionario.DataDemissao,
                        funcionario.Rg,
                        funcionario.DataCriacao,
                        funcionario.DataModificacao
                    );

                    formCadastro.FormClosed += (s, args) => CarregarFuncionarios();
                    formCadastro.ShowDialog();
                }

                else
                {
                    MessageBox.Show("Funcionário não encontrado.");
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


        private void frmConsultaFuncionario_Shown(object sender, EventArgs e)
        {
            /*
            StringBuilder sb = new StringBuilder();
            int i = 0;

            foreach (ColumnHeader column in listView1.Columns)
            {
                sb.AppendLine($"Coluna {i++}: \"{column.Text}\" - Largura: {column.Width}");
            }

            MessageBox.Show(sb.ToString(), "Tamanhos das colunas");
            */
        }


    }
}
