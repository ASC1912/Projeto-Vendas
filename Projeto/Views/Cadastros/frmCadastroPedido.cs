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
        private PedidoController controller = new PedidoController();
        private FuncionarioController funcionarioController = new FuncionarioController();
        private ProdutoController produtoController = new ProdutoController();
        private MesaController mesaController = new MesaController();

        private frmConsultaMesa oFrmConsultaMesa;
        private frmConsultaFuncionario oFrmConsultaFuncionario;
        private frmConsultaProduto oFrmConsultaProduto;

        private Pedido aPedido;
        private Mesa mesaSelecionada;
        private Funcionario funcionarioSelecionado;
        private Produto produtoSelecionado;

        public bool modoEdicao = false;
        public bool modoExclusao = false;

        public frmCadastroPedido()
        {
            InitializeComponent();
            VerificarMaxLength(this);

            txtCodigo.Enabled = false;
            txtTotal.ReadOnly = true;
            txtMesa.ReadOnly = false; 

            listViewProdutos.SelectedIndexChanged += new EventHandler(listViewProdutos_SelectedIndexChanged);
            txtQuantidade.TextChanged += new EventHandler(CalcularTotalItem);
            txtValorUnitario.TextChanged += new EventHandler(CalcularTotalItem);
            this.txtValorUnitario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress_ApenasNumerosEVirgula);
        }

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

        public override void BloquearTxt()
        {
            txtMesa.Enabled = false;
            btnPesquisarMesa.Enabled = false;
            
            txtIdFuncionario.Enabled = false;
            txtFuncionario.Enabled = false;
            btnPesquisarFuncionario.Enabled = false;
            
            txtObservacao.Enabled = false;
            chkInativo.Enabled = false;
            txtQuantidadeClientes.Enabled = false;
            chkFinalizado.Enabled = false;

            txtIdProduto.Enabled = false;
            txtProduto.Enabled = false;
            btnPesquisarProduto.Enabled = false;
            txtQuantidade.Enabled = false;
            txtValorUnitario.Enabled = false;
            btnAdicionarProduto.Enabled = false;
            btnEditarProduto.Enabled = false;
            btnRemoverProduto.Enabled = false;
            btnLimparProduto.Enabled = false;
            listViewProdutos.Enabled = false;
        }

        public override void DesbloquearTxt()
        {
            txtMesa.Enabled = true;
            btnPesquisarMesa.Enabled = true;

            txtIdFuncionario.Enabled = true;
            txtFuncionario.Enabled = true;
            btnPesquisarFuncionario.Enabled = true;

            txtObservacao.Enabled = true;
            chkInativo.Enabled = true;
            txtQuantidadeClientes.Enabled = true;
            chkFinalizado.Enabled = true;

            txtIdProduto.Enabled = true;
            txtProduto.Enabled = true;
            btnPesquisarProduto.Enabled = true;
            txtQuantidade.Enabled = true;
            txtValorUnitario.Enabled = true;
            btnAdicionarProduto.Enabled = true;
            btnEditarProduto.Enabled = true;
            btnRemoverProduto.Enabled = true;
            btnLimparProduto.Enabled = true;
            listViewProdutos.Enabled = true;
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
                txtIdFuncionario.Text = funcionarioSelecionado.Id.ToString();
                txtFuncionario.Text = funcionarioSelecionado.Nome;
            }
        }

        private void btnPesquisarProduto_Click(object sender, EventArgs e)
        {
            oFrmConsultaProduto.ModoSelecao = true;
            if (oFrmConsultaProduto.ShowDialog() == DialogResult.OK && oFrmConsultaProduto.ProdutoSelecionado != null)
            {
                produtoSelecionado = oFrmConsultaProduto.ProdutoSelecionado;
                txtIdProduto.Text = produtoSelecionado.Id.ToString();
                txtProduto.Text = produtoSelecionado.NomeProduto;
                txtValorUnitario.Text = produtoSelecionado.PrecoVenda.ToString("F2"); 
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
            item.SubItems.Add(produtoSelecionado.NomeUnidadeMedida ?? "UN");
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

                txtIdProduto.Text = produtoSelecionado.Id.ToString();
                txtProduto.Text = produtoSelecionado.NomeProduto;
                txtQuantidade.Text = item.SubItems[3].Text;
                txtValorUnitario.Text = item.SubItems[4].Text;
                txtTotal.Text = item.SubItems[5].Text;
            }
        }

        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            if (modoExclusao)
            {
                var confirmacao = MessageBox.Show("Tem certeza que deseja excluir este pedido?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmacao == DialogResult.Yes)
                {
                    try
                    {
                        int id = int.Parse(txtCodigo.Text);
                        await controller.Excluir(id);
                        MessageBox.Show("Pedido excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao excluir o pedido: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                return;
            }

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
                    Status = chkFinalizado.Checked ? "Finalizado" : "Aberto",
                    Ativo = !chkInativo.Checked,

                    QuantidadeClientes = int.TryParse(txtQuantidadeClientes.Text, out int qtd) ? qtd : 0,
                    Finalizado = chkFinalizado.Checked,
                    DataAberturaPedido = modoEdicao ? aPedido.DataAberturaPedido : DateTime.Now,
                    DataConclusaoPedido = chkFinalizado.Checked ? (DateTime?)DateTime.Now : null
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
            txtIdFuncionario.Clear();
            txtFuncionario.Clear();
            txtObservacao.Clear();
            chkInativo.Checked = false;
            listViewProdutos.Items.Clear();
            LimparCamposItem();
            CalcularTotalPedido();

            txtQuantidadeClientes.Clear();
            chkFinalizado.Checked = false;
        }

        private void LimparCamposItem()
        {
            produtoSelecionado = null;
            txtIdProduto.Clear();
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

            mesaSelecionada = await new MesaController().BuscarPorNumero(aPedido.MesaNumero);
            txtMesa.Text = mesaSelecionada?.NumeroMesa.ToString();

            funcionarioSelecionado = await funcionarioController.BuscarPorId(aPedido.FuncionarioId);
            txtIdFuncionario.Text = funcionarioSelecionado?.Id.ToString();
            txtFuncionario.Text = funcionarioSelecionado?.Nome;

            txtObservacao.Text = aPedido.Observacao;
            chkInativo.Checked = !aPedido.Ativo;

            txtQuantidadeClientes.Text = aPedido.QuantidadeClientes.ToString();
            chkFinalizado.Checked = aPedido.Finalizado;

            listViewProdutos.Items.Clear();
            if (aPedido.Itens != null)
            {
                foreach (var item in aPedido.Itens)
                {
                    ListViewItem listItem = new ListViewItem(item.ProdutoId.ToString());
                    listItem.SubItems.Add(item.NomeProduto);
                    listItem.SubItems.Add("UN");
                    listItem.SubItems.Add(item.Quantidade.ToString());
                    listItem.SubItems.Add(item.PrecoUnitario.ToString("F2"));
                    listItem.SubItems.Add((item.Quantidade * item.PrecoUnitario).ToString("F2"));

                    var produtoDoItem = await produtoController.BuscarPorId(item.ProdutoId);
                    if (produtoDoItem != null)
                    {
                        listItem.Tag = produtoDoItem;
                    }

                    listViewProdutos.Items.Add(listItem);
                }
            }
            CalcularTotalPedido();
        }
        
        private async void txtIdFuncionario_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdFuncionario.Text))
            {
                txtFuncionario.Clear();
                funcionarioSelecionado = null;
                return;
            }
            if (int.TryParse(txtIdFuncionario.Text, out int id))
            {
                var func = await funcionarioController.BuscarPorId(id);
                if (func != null)
                {
                    txtFuncionario.Text = func.Nome;
                    funcionarioSelecionado = func;
                }
                else
                {
                    MessageBox.Show("Funcionário não encontrado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtFuncionario.Clear();
                    funcionarioSelecionado = null;
                }
            }
            else
            {
                MessageBox.Show("ID do Funcionário inválido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFuncionario.Clear();
                funcionarioSelecionado = null;
            }
        }

        private async void txtIdProduto_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdProduto.Text))
            {
                txtProduto.Clear();
                produtoSelecionado = null;
                return;
            }
            if (int.TryParse(txtIdProduto.Text, out int id))
            {
                var prod = await produtoController.BuscarPorId(id);
                if (prod != null)
                {
                    txtProduto.Text = prod.NomeProduto;
                    txtValorUnitario.Text = prod.PrecoVenda.ToString("F2");
                    produtoSelecionado = prod;
                }
                else
                {
                    MessageBox.Show("Produto não encontrado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtProduto.Clear();
                    produtoSelecionado = null;
                }
            }
            else
            {
                MessageBox.Show("ID do Produto inválido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProduto.Clear();
                produtoSelecionado = null;
            }
        }
    }
}