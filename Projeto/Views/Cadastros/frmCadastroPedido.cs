using Projeto.Controller;
using Projeto.Models;
using Projeto.Utils;
using Projeto.Views.Consultas;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto.Views.Cadastros
{
    public partial class frmCadastroPedido : frmBase
    {
        // Controllers para acessar outros módulos
        private PedidoController controller = new PedidoController();
        private FuncionarioController funcionarioController = new FuncionarioController();
        private ProdutoController produtoController = new ProdutoController();

        // Formulários de consulta que serão abertos
        private frmConsultaMesa oFrmConsultaMesa;
        private frmConsultaFuncionario oFrmConsultaFuncionario;
        private frmConsultaProduto oFrmConsultaProduto;

        // Objeto principal e objetos selecionados
        private Pedido aPedido;
        private Mesa mesaSelecionada;
        private Funcionario funcionarioSelecionado;
        private Produto produtoSelecionado;

        public bool modoEdicao = false;
        public bool modoExclusao = false;

        public frmCadastroPedido()
        {
            InitializeComponent();
            // Associa os eventos aos métodos
            // Certifique-se de que os nomes dos botões aqui correspondem aos do seu designer
            btnPesquisarMesa.Click += new EventHandler(btnPesquisarMesa_Click);
            btnPesquisarFuncionario.Click += new EventHandler(btnPesquisarFuncionario_Click);
            btnPesquisarProduto.Click += new EventHandler(btnPesquisarProduto_Click);
            btnAdicionarProduto.Click += new EventHandler(btnAdicionar_Click);
            btnEditarProduto.Click += new EventHandler(btnEditar_Click);
            btnRemoverProduto.Click += new EventHandler(btnRemover_Click);
            btnLimparProduto.Click += new EventHandler(btnLimpar_Click);
            listViewProdutos.SelectedIndexChanged += new EventHandler(listViewProdutos_SelectedIndexChanged);
            txtQuantidade.TextChanged += new EventHandler(CalcularTotalItem);
            txtValorUnitario.TextChanged += new EventHandler(CalcularTotalItem);
        }

        // Métodos para receber os formulários de consulta da classe Interfaces
        public void setFrmConsultaMesa(object obj)
        {
            if (obj != null) oFrmConsultaMesa = (frmConsultaMesa)obj;
        }
        public void setFrmConsultaFuncionario(object obj)
        {
            if (obj != null) oFrmConsultaFuncionario = (frmConsultaFuncionario)obj;
        }
        public void setFrmConsultaProduto(object obj)
        {
            if (obj != null) oFrmConsultaProduto = (frmConsultaProduto)obj;
        }

        public override void ConhecaObj(object obj, object ctrl)
        {
            if (obj != null) aPedido = (Pedido)obj;
            if (ctrl != null) controller = (PedidoController)ctrl;
        }


        private void btnPesquisarMesa_Click(object sender, EventArgs e)
        {
            oFrmConsultaMesa.ModoSelecao = true;
            if (oFrmConsultaMesa.ShowDialog() == DialogResult.OK && oFrmConsultaMesa.MesaSelecionada != null)
            {
                mesaSelecionada = oFrmConsultaMesa.MesaSelecionada;
                txtMesa.Text = mesaSelecionada.NumeroMesa.ToString();
            }
        }

        private void btnPesquisarFuncionario_Click(object sender, EventArgs e)
        {
            oFrmConsultaFuncionario.ModoSelecao = true;
            if (oFrmConsultaFuncionario.ShowDialog() == DialogResult.OK && oFrmConsultaFuncionario.FuncionarioSelecionado != null)
            {
                funcionarioSelecionado = oFrmConsultaFuncionario.FuncionarioSelecionado;
                txtFuncionario.Text = funcionarioSelecionado.Nome;
            }
        }

        private void btnPesquisarProduto_Click(object sender, EventArgs e)
        {
            oFrmConsultaProduto.ModoSelecao = true;
            if (oFrmConsultaProduto.ShowDialog() == DialogResult.OK && oFrmConsultaProduto.ProdutoSelecionado != null)
            {
                produtoSelecionado = oFrmConsultaProduto.ProdutoSelecionado;
                txtProduto.Text = produtoSelecionado.NomeProduto;
                txtValorUnitario.Text = produtoSelecionado.Preco.ToString("F2");
                txtQuantidade.Focus();
            }
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (produtoSelecionado == null)
            {
                MessageBox.Show("Por favor, pesquise e selecione um produto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!Validador.CampoObrigatorio(txtQuantidade, "A quantidade é obrigatória.") || !int.TryParse(txtQuantidade.Text, out _))
            {
                MessageBox.Show("A quantidade deve ser um número válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ListViewItem item = new ListViewItem(produtoSelecionado.Id.ToString());
            item.SubItems.Add(produtoSelecionado.NomeProduto);
            item.SubItems.Add("UN");
            item.SubItems.Add(txtQuantidade.Text);
            item.SubItems.Add(txtValorUnitario.Text);
            item.SubItems.Add(txtTotal.Text);
            item.Tag = produtoSelecionado;

            listViewProdutos.Items.Add(item);
            LimparCamposItem();
            CalcularTotalPedido();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (listViewProdutos.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecione um item na lista para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var itemDoListView = listViewProdutos.SelectedItems[0];
            itemDoListView.SubItems[3].Text = txtQuantidade.Text;
            itemDoListView.SubItems[4].Text = txtValorUnitario.Text;
            itemDoListView.SubItems[5].Text = txtTotal.Text;

            LimparCamposItem();
            CalcularTotalPedido();
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (listViewProdutos.SelectedItems.Count > 0)
            {
                listViewProdutos.Items.Remove(listViewProdutos.SelectedItems[0]);
                CalcularTotalPedido();
            }
            else
            {
                MessageBox.Show("Selecione um item para remover.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCamposItem();
        }

        private void listViewProdutos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewProdutos.SelectedItems.Count > 0)
            {
                var item = listViewProdutos.SelectedItems[0];
                produtoSelecionado = (Produto)item.Tag;

                txtProduto.Text = produtoSelecionado.NomeProduto;
                txtQuantidade.Text = item.SubItems[3].Text;
                txtValorUnitario.Text = item.SubItems[4].Text;
                txtTotal.Text = item.SubItems[5].Text;
            }
        }

        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            if (mesaSelecionada == null || funcionarioSelecionado == null)
            {
                MessageBox.Show("É obrigatório selecionar uma mesa e um funcionário.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (listViewProdutos.Items.Count == 0)
            {
                MessageBox.Show("É necessário adicionar pelo menos um item ao pedido.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Pedido pedidoParaSalvar = new Pedido
                {
                    Id = modoEdicao ? aPedido.Id : 0,
                    MesaNumero = mesaSelecionada.NumeroMesa,
                    FuncionarioId = funcionarioSelecionado.Id,
                    Observacao = txtObservacao.Text,
                    Status = "Aberto",
                    Ativo = !chkInativo.Checked,
                    DataPedido = DateTime.Now
                };

                foreach (ListViewItem item in listViewProdutos.Items)
                {
                    Produto prod = (Produto)item.Tag;
                    pedidoParaSalvar.Itens.Add(new ItemPedido
                    {
                        ProdutoId = prod.Id,
                        Quantidade = int.Parse(item.SubItems[3].Text),
                        PrecoUnitario = decimal.Parse(item.SubItems[4].Text),
                        Status = "Pendente"
                    });
                }

                await controller.Salvar(pedidoParaSalvar);
                MessageBox.Show("Pedido salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar pedido: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public override void LimparTxt()
        {
            base.LimparTxt();
            txtMesa.Clear();
            txtFuncionario.Clear();
            txtObservacao.Clear();
            chkInativo.Checked = false;
            listViewProdutos.Items.Clear();
            LimparCamposItem();
            CalcularTotalPedido();
        }

        private void LimparCamposItem()
        {
            produtoSelecionado = null;
            txtProduto.Clear();
            txtQuantidade.Text = "1";
            txtValorUnitario.Clear();
            txtTotal.Clear();
            btnPesquisarProduto.Focus();
        }

        private void CalcularTotalItem(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtQuantidade.Text, out decimal qtd) && decimal.TryParse(txtValorUnitario.Text, out decimal vlr))
            {
                txtTotal.Text = (qtd * vlr).ToString("F2");
            }
        }

        private void CalcularTotalPedido()
        {
            decimal total = 0;
            foreach (ListViewItem item in listViewProdutos.Items)
            {
                total += decimal.Parse(item.SubItems[5].Text);
            }
            lblTotalPedido.Text = $"Total (R$): {total:F2}";
        }

        public override async void CarregaTxt()
        {
            txtCodigo.Text = aPedido.Id.ToString();

            // Await é correto aqui porque estes controllers retornam Task
            mesaSelecionada = await new MesaController().BuscarPorNumero(aPedido.MesaNumero);
            txtMesa.Text = mesaSelecionada?.NumeroMesa.ToString();

            funcionarioSelecionado = await funcionarioController.BuscarPorId(aPedido.FuncionarioId);
            txtFuncionario.Text = funcionarioSelecionado?.Nome;

            txtObservacao.Text = aPedido.Observacao;
            chkInativo.Checked = !aPedido.Ativo;

            listViewProdutos.Items.Clear();
            foreach (var item in aPedido.Itens)
            {
                ListViewItem listItem = new ListViewItem(item.ProdutoId.ToString());
                listItem.SubItems.Add(item.NomeProduto);
                listItem.SubItems.Add("UN");
                listItem.SubItems.Add(item.Quantidade.ToString());
                listItem.SubItems.Add(item.PrecoUnitario.ToString("F2"));
                listItem.SubItems.Add((item.Quantidade * item.PrecoUnitario).ToString("F2"));

                listItem.Tag = produtoController.BuscarPorId(item.ProdutoId);

                listViewProdutos.Items.Add(listItem);
            }
            CalcularTotalPedido();
        }

    }
}