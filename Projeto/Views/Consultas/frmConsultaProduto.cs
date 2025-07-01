using Projeto.Controller;
using Projeto.Models;
using Projeto.Views.Cadastros;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Projeto.Views.Consultas
{
    public partial class frmConsultaProduto : Projeto.frmBaseConsulta
    {
        private ProdutoController controller = new ProdutoController();
        public bool ModoSelecao { get; set; } = false;

        public frmConsultaProduto() : base()
        {
            InitializeComponent();
        }

        private void CarregarProdutos()
        {
            try
            {
                listView1.Items.Clear();
                List<Produto> produtos = controller.ListarProdutos();

                foreach (var produto in produtos)
                {
                    ListViewItem item = new ListViewItem(produto.Id.ToString());
                    item.SubItems.Add(produto.NomeProduto);
                    item.SubItems.Add(produto.Descricao);
                    item.SubItems.Add(produto.Preco.ToString("F2"));
                    item.SubItems.Add(produto.Estoque.ToString());
                    item.SubItems.Add(produto.NomeMarca ?? "");
                    item.SubItems.Add(produto.NomeGrupo ?? "");
                    item.SubItems.Add(produto.Ativo ? "Ativo" : "Inativo");

                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar produtos: " + ex.Message);
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
                    CarregarProdutos();
                }
                else if (int.TryParse(texto, out int id))
                {
                    Produto produto = controller.BuscarPorId(id);

                    if (produto != null)
                    {
                        ListViewItem item = new ListViewItem(produto.Id.ToString());
                        item.SubItems.Add(produto.NomeProduto);
                        item.SubItems.Add(produto.Descricao);
                        item.SubItems.Add(produto.Preco.ToString("F2"));
                        item.SubItems.Add(produto.Estoque.ToString());
                        item.SubItems.Add(produto.NomeMarca ?? "");
                        item.SubItems.Add(produto.NomeGrupo ?? "");
                        item.SubItems.Add(produto.Ativo ? "Ativo" : "Inativo");

                        listView1.Items.Add(item);
                    }
                    else
                    {
                        MessageBox.Show("Produto não encontrado.");
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
            frmCadastroProduto formCadastroProduto = new frmCadastroProduto();
            formCadastroProduto.FormClosed += (s, args) => CarregarProdutos();
            formCadastroProduto.ShowDialog();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);

                Produto produto = controller.BuscarPorId(id);

                if (produto != null)
                {
                    var formCadastro = new frmCadastroProduto
                    {
                        modoEdicao = true
                    };

                    formCadastro.CarregarProduto(
                        produto.Id,
                        produto.NomeProduto,
                        produto.Descricao,
                        produto.Preco,
                        produto.Estoque,
                        produto.IdMarca ?? 0,
                        produto.NomeMarca,
                        produto.GrupoId ?? 0,
                        produto.NomeGrupo,
                        produto.Ativo,
                        produto.DataCadastro,
                        produto.DataAlteracao
                    );

                    formCadastro.FormClosed += (s, args) => CarregarProdutos();
                    formCadastro.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Produto não encontrado.");
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

                var confirmacao = MessageBox.Show("Tem certeza que deseja excluir esse produto?", "Confirmação", MessageBoxButtons.YesNo);
                if (confirmacao == DialogResult.Yes)
                {
                    try
                    {
                        controller.Excluir(id);
                        listView1.Items.Remove(itemSelecionado);
                        MessageBox.Show("Produto excluído com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao excluir: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um produto para excluir.");
            }
        }

        private void frmConsultaProduto_Load(object sender, EventArgs e)
        {
            CarregarProdutos();

            foreach (ColumnHeader column in listView1.Columns)
            {
                switch (column.Text)
                {
                    case "ID":
                        column.Width = 50;
                        break;
                    case "Produto":
                        column.Width = 200;
                        break;
                    case "Descrição":
                        column.Width = 200;
                        break;
                    case "Preço":
                        column.Width = 80;
                        break;
                    case "Estoque":
                        column.Width = 70;
                        break;
                    case "Marca":
                        column.Width = 100;
                        break;
                    case "Grupo":
                        column.Width = 100;
                        break;
                    case "Ativo":
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
