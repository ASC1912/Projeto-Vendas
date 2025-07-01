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

        public frmConsultaFornecedor() : base()
        {
            InitializeComponent();
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            frmCadastroFornecedor formCadastroFornecedor = new frmCadastroFornecedor();
            formCadastroFornecedor.FormClosed += (s, args) => CarregarFornecedores();

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
                    item.SubItems.Add(fornecedor.Tipo);
                    item.SubItems.Add(fornecedor.Nome);
                    item.SubItems.Add(fornecedor.Endereco);
                    item.SubItems.Add(fornecedor.NumeroEndereco.ToString());
                    item.SubItems.Add(fornecedor.Bairro);
                    item.SubItems.Add(fornecedor.Complemento);
                    item.SubItems.Add(fornecedor.CEP);
                    item.SubItems.Add(fornecedor.NomeCidade);
                    item.SubItems.Add(fornecedor.DescricaoCondicao);
                    item.SubItems.Add(fornecedor.Telefone);
                    item.SubItems.Add(fornecedor.Email);
                    item.SubItems.Add(fornecedor.CPF_CNPJ);
                    item.SubItems.Add(fornecedor.InscricaoEstadual);
                    item.SubItems.Add(fornecedor.InscricaoEstadualSubTrib);
                    item.SubItems.Add(fornecedor.Ativo ? "Ativo" : "Inativo");
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
            try
            {
                listView1.Items.Clear();
                string texto = txtPesquisar.Text.Trim();

                if (string.IsNullOrEmpty(texto))
                {
                    CarregarFornecedores();
                }
                else if (int.TryParse(texto, out int id))
                {
                    Fornecedor fornecedor = controller.BuscarPorId(id);

                    if (fornecedor != null)
                    {
                        ListViewItem item = new ListViewItem(fornecedor.Id.ToString());
                        item.SubItems.Add(fornecedor.Tipo);
                        item.SubItems.Add(fornecedor.Nome);
                        item.SubItems.Add(fornecedor.Endereco);
                        item.SubItems.Add(fornecedor.NumeroEndereco.ToString());
                        item.SubItems.Add(fornecedor.Bairro);
                        item.SubItems.Add(fornecedor.Complemento);
                        item.SubItems.Add(fornecedor.CEP);
                        item.SubItems.Add(fornecedor.NomeCidade);
                        item.SubItems.Add(fornecedor.DescricaoCondicao);
                        item.SubItems.Add(fornecedor.Telefone);
                        item.SubItems.Add(fornecedor.Email);
                        item.SubItems.Add(fornecedor.CPF_CNPJ);
                        item.SubItems.Add(fornecedor.InscricaoEstadual);
                        item.SubItems.Add(fornecedor.InscricaoEstadualSubTrib);
                        item.SubItems.Add(fornecedor.Ativo ? "Ativo" : "Inativo");
                        listView1.Items.Add(item);
                    }
                    else
                    {
                        MessageBox.Show("Fornecedor não encontrado.");
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

                Fornecedor fornecedor = controller.BuscarPorId(id);

                if (fornecedor != null)
                {
                    Cidade cidade = fornecedor.IdCidade.HasValue
                        ? new CidadeController().BuscarPorId(fornecedor.IdCidade.Value)
                        : null;

                    var formCadastro = new frmCadastroFornecedor();
                    formCadastro.modoEdicao = true;
                    formCadastro.CarregarFornecedor(
                        fornecedor.Id,
                        fornecedor.Nome,
                        fornecedor.CPF_CNPJ,
                        fornecedor.Telefone,
                        fornecedor.Email,
                        fornecedor.Endereco,
                        fornecedor.NumeroEndereco ?? 0,
                        fornecedor.Bairro,
                        fornecedor.Complemento,
                        fornecedor.CEP,
                        fornecedor.InscricaoEstadual,
                        fornecedor.InscricaoEstadualSubTrib,
                        fornecedor.Tipo,
                        cidade?.NomeCidade ?? "Não encontrado",
                        fornecedor.IdCidade ?? 0,
                        fornecedor.DescricaoCondicao ?? "Não encontrada",
                        fornecedor.IdCondicao ?? 0,
                        fornecedor.Ativo,
                        fornecedor.DataCadastro,
                        fornecedor.DataAlteracao
                    );

                    formCadastro.FormClosed += (s, args) => CarregarFornecedores();
                    formCadastro.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Fornecedor não encontrado.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um fornecedor para editar.");
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

        private void frmConsultaFornecedor_Load(object sender, EventArgs e)
        {
            CarregarFornecedores();

            foreach (ColumnHeader column in listView1.Columns)
            {
                switch (column.Text)
                {
                    case "ID":
                        column.Width = 40;
                        break;
                    case "Tipo":
                        column.Width = 60;
                        break;
                    case "Nome":
                        column.Width = 200;
                        break;
                    case "Endereço":
                        column.Width = 200;
                        break;
                    case "Número":
                        column.Width = 60;
                        break;
                    case "Bairro":
                        column.Width = 150;
                        break;
                    case "Complemento":
                        column.Width = 130;
                        break;
                    case "CEP":
                        column.Width = 80;
                        break;
                    case "Cidade":
                        column.Width = 150;
                        break;
                    case "Telefone":
                        column.Width = 120;
                        break;
                    case "Email":
                        column.Width = 200;
                        break;
                    case "Cond. Pgto":
                        column.Width = 150;
                        break;
                    case "Status":
                        column.Width = 60;
                        break;
                    default:
                        column.Width = 100;
                        break;
                }
            }
        }


    }
}
