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
        private frmCadastroProduto oFrmCadastroProduto;


        public frmConsultaProduto() : base()
        {
            InitializeComponent();
        }

        public override void setFrmCadastro(object obj)
        {
            if (obj != null)
            {
                oFrmCadastroProduto = (frmCadastroProduto)obj;
            }
        }

        public override void ConhecaObj(object obj, object ctrl)
        {
            if (ctrl != null)
            {
                controller = (ProdutoController)ctrl;
            }
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
            oFrmCadastroProduto.modoEdicao = false;
            oFrmCadastroProduto.modoExclusao = false;
            oFrmCadastroProduto.ShowDialog();
            CarregarProdutos();
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

                    oFrmCadastroProduto.modoEdicao = true;
                    oFrmCadastroProduto.modoExclusao = false;
                    oFrmCadastroProduto.ConhecaObj(produto, controller);
                    oFrmCadastroProduto.CarregaTxt();
                    oFrmCadastroProduto.ShowDialog();
                    oFrmCadastroProduto.LimparTxt();
                    CarregarProdutos();
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

                Produto produto = controller.BuscarPorId(id);

                if (produto != null)
                {
                    oFrmCadastroProduto.modoExclusao = true;
                    oFrmCadastroProduto.modoEdicao = false;
                    oFrmCadastroProduto.ConhecaObj(produto, controller);
                    oFrmCadastroProduto.CarregaTxt();
                    oFrmCadastroProduto.BloquearTxt();
                    oFrmCadastroProduto.ShowDialog();
                    oFrmCadastroProduto.DesbloquearTxt();
                    oFrmCadastroProduto.LimparTxt();
                    CarregarProdutos();
                }
                else
                {
                    MessageBox.Show("Produto não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um produto para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
