using Projeto.Controller;
using Projeto.Models;
using Projeto.Views.Cadastros;
using System;
using System.Collections.Generic;
using System.Threading.Tasks; 
using System.Windows.Forms;

namespace Projeto.Views.Consultas
{
    public partial class frmConsultaProduto : Projeto.frmBaseConsulta
    {
        private ProdutoController controller = new ProdutoController();
        private frmCadastroProduto oFrmCadastroProduto;
        internal Produto ProdutoSelecionado { get; private set; }

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

        private async Task CarregarProdutos()
        {
            try
            {
                listView1.Items.Clear();
                List<Produto> produtos = await controller.ListarProdutos();

                foreach (var produto in produtos)
                {
                    ListViewItem item = new ListViewItem(produto.Id.ToString());
                    item.SubItems.Add(produto.NomeProduto);
                    item.SubItems.Add(produto.Descricao);
                    item.SubItems.Add(produto.PrecoCusto.ToString("F2"));
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

        private async void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                listView1.Items.Clear();
                string texto = txtPesquisar.Text.Trim();

                if (string.IsNullOrEmpty(texto))
                {
                    await CarregarProdutos();
                }
                else if (int.TryParse(texto, out int id))
                {
                    Produto produto = await controller.BuscarPorId(id);

                    if (produto != null)
                    {
                        ListViewItem item = new ListViewItem(produto.Id.ToString());
                        item.SubItems.Add(produto.NomeProduto);
                        item.SubItems.Add(produto.Descricao);
                        item.SubItems.Add(produto.PrecoCusto.ToString("F2"));
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

        private async void btnIncluir_Click(object sender, EventArgs e)
        {
            oFrmCadastroProduto.modoEdicao = false;
            oFrmCadastroProduto.modoExclusao = false;
            oFrmCadastroProduto.LimparTxt();
            oFrmCadastroProduto.ShowDialog();
            await CarregarProdutos();
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);

                Produto produto = await controller.BuscarPorId(id);

                if (produto != null)
                {
                    oFrmCadastroProduto.modoEdicao = true;
                    oFrmCadastroProduto.modoExclusao = false;
                    oFrmCadastroProduto.ConhecaObj(produto, controller);
                    oFrmCadastroProduto.LimparTxt();
                    oFrmCadastroProduto.CarregaTxt();
                    oFrmCadastroProduto.ShowDialog();
                    await CarregarProdutos();
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

        private async void btnDeletar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);

                Produto produto = await controller.BuscarPorId(id);

                if (produto != null)
                {
                    oFrmCadastroProduto.modoExclusao = true;
                    oFrmCadastroProduto.modoEdicao = false;
                    oFrmCadastroProduto.ConhecaObj(produto, controller);
                    oFrmCadastroProduto.LimparTxt();
                    oFrmCadastroProduto.CarregaTxt();
                    oFrmCadastroProduto.BloquearTxt();
                    oFrmCadastroProduto.btnSalvar.Text = "Excluir";
                    oFrmCadastroProduto.ShowDialog();
                    oFrmCadastroProduto.btnSalvar.Text = "Salvar";
                    oFrmCadastroProduto.DesbloquearTxt();
                    await CarregarProdutos();
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

        private async void btnSelecionar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                try
                {
                    int id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
                    ProdutoSelecionado = await controller.BuscarPorId(id);

                    if (ProdutoSelecionado != null)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao selecionar o produto: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um produto.");
            }
        }

        private async void frmConsultaProduto_Load(object sender, EventArgs e)
        {
            btnSelecionar.Visible = ModoSelecao;
            await CarregarProdutos();

            foreach (ColumnHeader column in listView1.Columns)
            {
                switch (column.Text)
                {
                    case "ID":
                        column.Width = 50;
                        column.TextAlign = HorizontalAlignment.Right;
                        break;
                    case "Preço":
                        column.Width = 80;
                        column.TextAlign = HorizontalAlignment.Right;
                        break;
                    case "Estoque":
                        column.Width = 70;
                        column.TextAlign = HorizontalAlignment.Right;
                        break;
                    case "Produto":
                        column.Width = 200;
                        column.TextAlign = HorizontalAlignment.Center;
                        break;
                    case "Descrição":
                        column.Width = 200;
                        column.TextAlign = HorizontalAlignment.Center;
                        break;
                    case "Marca":
                        column.Width = 100;
                        column.TextAlign = HorizontalAlignment.Center;
                        break;
                    case "Grupo":
                        column.Width = 100;
                        column.TextAlign = HorizontalAlignment.Center;
                        break;
                    case "Ativo":
                        column.Width = 60;
                        column.TextAlign = HorizontalAlignment.Center;
                        break;
                    default:
                        column.Width = 100;
                        break;
                }
            }
        }
    }
}