using Projeto.Controller;
using Projeto.Models;
using Projeto.Views.Consultas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Projeto.Views.Cadastros
{
    public partial class frmCadastroCompra : Projeto.frmBase
    {
        private Compra aCompra; 
        private CompraController controller = new CompraController();
        private FornecedorController fornecedorController = new FornecedorController(); 
        private ProdutoController produtoController = new ProdutoController();
        private CondicaoPagamentoController condicaoPagamentoController = new CondicaoPagamentoController();

        private frmConsultaFornecedor oFrmConsultaFornecedor;
        private frmConsultaProduto oFrmConsultaProduto;
        private frmConsultaCondPgto oFrmConsultaCondPgto;
        private Produto produtoSelecionado; 

        private List<Control> parte1Controles;
        private List<Control> parte2Controles;
        private List<Control> parte3Controles;
        public frmCadastroCompra()
        {
            InitializeComponent();
            AgruparControles();
            ConfigurarEstadoInicial();
        }

        public void setFrmConsultaFornecedor(object obj)
        {
            if (obj != null)
            {
                oFrmConsultaFornecedor = (frmConsultaFornecedor)obj;
            }
        }

        public void setFrmConsultaProduto(object obj)
        {
            if (obj != null)
            {
                oFrmConsultaProduto = (frmConsultaProduto)obj;
            }
        }

        public void setFrmConsultaCondPgto(object obj)
        {
            if (obj != null)
            {
                oFrmConsultaCondPgto = (frmConsultaCondPgto)obj;
            }
        }


        private void AgruparControles()
        {
            parte1Controles = new List<Control>
            {
                txtCodigo, txtSerie, txtNumero, txtIDFornecedor, 
                txtFornecedor, btnPesquisarFornecedor, dtpEmissao, dtpChegada
            };

            parte2Controles = new List<Control>
            {
                txtIdProduto, 
                txtProduto, btnPesquisarProduto, txtQuantidade,
                txtValorUnitario, txtTotal, btnAdicionarProduto,
                btnEditarProduto, btnRemoverProduto, btnLimparProduto,
                listViewProdutos
            };

            parte3Controles = new List<Control>
            {
                txtFrete,
                txtSeguro,
                txtDespesas,
                txtValorTotal,
                txtIdCondPgto, 
                txtCondPgto,
                btnAdicionarCondPgto,
                btnLimparCondPgto,
                listViewCondPgto
            };
        }

        private void ConfigurarEstadoInicial()
        {
            HabilitarControles(parte1Controles, true);
            HabilitarControles(parte2Controles, true);
            HabilitarControles(parte3Controles, false);
            btnSalvar.Enabled = false;
        }

        private void HabilitarControles(List<Control> controles, bool habilitar)
        {
            foreach (var control in controles)
            {
                control.Enabled = habilitar;
            }
        }

        public override void ConhecaObj(object obj, object ctrl)
        {
            if (obj != null)
            {
                aCompra = (Compra)obj; 
            }

            if (ctrl != null)
            {
                controller = (CompraController)ctrl; 
            }
        }

        public override void LimparTxt()
        {
            txtCodigo.Text = "0";
            txtSerie.Clear();
            txtNumero.Clear();
            txtIDFornecedor.Clear();
            txtFornecedor.Clear();
            dtpEmissao.Value = DateTime.Now;
            dtpChegada.Value = DateTime.Now;

            txtIdProduto.Clear();

            txtProduto.Clear();
            txtQuantidade.Clear();
            txtValorUnitario.Clear();
            txtTotal.Clear();
            listViewProdutos.Items.Clear();

            txtFrete.Clear();
            txtSeguro.Clear();
            txtDespesas.Clear();
            txtValorTotal.Clear();

            txtIdCondPgto.Clear();

            txtCondPgto.Clear();
            listViewCondPgto.Items.Clear();

            ConfigurarEstadoInicial();
        }

        public override void CarregaTxt()
        {
            if (aCompra == null)
            {
                MessageBox.Show("Não foi possível carregar os dados da compra.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtCodigo.Text = aCompra.Modelo;
            txtSerie.Text = aCompra.Serie;
            txtNumero.Text = aCompra.NumeroNota.ToString();
            txtIDFornecedor.Text = aCompra.FornecedorId.ToString();
            txtFornecedor.Text = aCompra.NomeFornecedor;
            dtpEmissao.Value = aCompra.DataEmissao ?? DateTime.Now;
            dtpChegada.Value = aCompra.DataChegada ?? DateTime.Now;

            listViewProdutos.Items.Clear();
            if (aCompra.Itens != null)
            {
                foreach (var itemCompra in aCompra.Itens)
                {
                    ListViewItem item = new ListViewItem(itemCompra.ProdutoId.ToString());
                    item.SubItems.Add(itemCompra.NomeProduto);
                    item.SubItems.Add("UN"); 
                    item.SubItems.Add(itemCompra.Quantidade.ToString());
                    item.SubItems.Add(itemCompra.ValorUnitario.ToString("F2"));
                    item.SubItems.Add(itemCompra.ValorTotalItem.ToString("F2"));
                    listViewProdutos.Items.Add(item);
                }
            }

            txtFrete.Text = aCompra.ValorFrete.ToString("F2");
            txtSeguro.Text = aCompra.Seguro.ToString("F2");
            txtDespesas.Text = aCompra.Despesas.ToString("F2");
            txtValorTotal.Text = aCompra.ValorTotal.ToString("F2");
            txtCondPgto.Text = aCompra.NomeCondPgto;

            if (aCompra.CondicaoPagamentoId.HasValue)
            {
                txtCondPgto.Tag = aCompra.CondicaoPagamentoId.Value;
            }

            lblDataCriacao.Text = aCompra.DataCadastro.HasValue ? $"Criado em: {aCompra.DataCadastro.Value:dd/MM/yyyy HH:mm}" : "Criado em: -";
            lblDataModificacao.Text = aCompra.DataAlteracao.HasValue ? $"Modificado em: {aCompra.DataAlteracao.Value:dd/MM/yyyy HH:mm}" : "Modificado em: -";
        }

        private async void txtIDFornecedor_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDFornecedor.Text))
            {
                txtFornecedor.Clear();
                return;
            }

            if (!int.TryParse(txtIDFornecedor.Text, out int id))
            {
                MessageBox.Show("O ID do Fornecedor deve ser um número.", "Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Fornecedor fornecedor = await fornecedorController.BuscarPorId(id);

            if (fornecedor != null)
            {
                txtFornecedor.Text = fornecedor.Nome;
            }
            else
            {
                MessageBox.Show("Fornecedor não encontrado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFornecedor.Clear();
            }
        }

        private async void txtIdProduto_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdProduto.Text))
            {
                txtProduto.Clear();
                txtValorUnitario.Clear();
                produtoSelecionado = null;
                return;
            }

            if (!int.TryParse(txtIdProduto.Text, out int id))
            {
                MessageBox.Show("O ID do produto deve ser um número.", "Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Produto produto = await produtoController.BuscarPorId(id);

            if (produto != null)
            {
                produtoSelecionado = produto;
                txtProduto.Text = produto.NomeProduto;
                txtValorUnitario.Text = produto.PrecoCusto.ToString("F2");
                txtQuantidade.Focus();
            }
            else
            {
                MessageBox.Show("Produto não encontrado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProduto.Clear();
                txtValorUnitario.Clear();
                produtoSelecionado = null;
            }
        }

        private async void txtIdCondPgto_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdCondPgto.Text))
            {
                txtCondPgto.Clear();
                listViewCondPgto.Items.Clear();
                txtCondPgto.Tag = null;
                return;
            }

            if (!int.TryParse(txtIdCondPgto.Text, out int id))
            {
                MessageBox.Show("O ID da Condição de Pagamento deve ser um número.", "Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CondicaoPagamento condicao = await condicaoPagamentoController.BuscarPorId(id);

            if (condicao != null)
            {
                txtCondPgto.Text = condicao.Descricao;
                txtCondPgto.Tag = condicao.Id;

                listViewCondPgto.Items.Clear();
                if (condicao.Parcelas != null)
                {
                    foreach (var parcela in condicao.Parcelas)
                    {
                        ListViewItem item = new ListViewItem(parcela.NumParcela.ToString());
                        item.SubItems.Add(parcela.PrazoDias.ToString());
                        item.SubItems.Add(parcela.Porcentagem.ToString("N2") + "%");
                        item.SubItems.Add(parcela.FormaPagamento?.Descricao ?? "N/D");
                        listViewCondPgto.Items.Add(item);
                    }
                }
            }
            else
            {
                MessageBox.Show("Condição de Pagamento não encontrada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCondPgto.Clear();
                listViewCondPgto.Items.Clear();
                txtCondPgto.Tag = null;
            }
        }

        public override void BloquearTxt()
        {
            HabilitarControles(parte1Controles, false);
            HabilitarControles(parte2Controles, false);
            HabilitarControles(parte3Controles, false);
        }

        public override void DesbloquearTxt()
        {
            HabilitarControles(parte1Controles, true);
            HabilitarControles(parte2Controles, true);
            HabilitarControles(parte3Controles, true);
        }

        private bool ValidarParte1()
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text) ||       
                string.IsNullOrWhiteSpace(txtSerie.Text) ||
                string.IsNullOrWhiteSpace(txtNumero.Text) ||
                string.IsNullOrWhiteSpace(txtIDFornecedor.Text))
            {
                return false;
            }

            return true; 
        }

        private void Parte1_ValidarTransicao(object sender, EventArgs e)
        {
            if (!parte2Controles.First().Enabled)
            {
                if (ValidarParte1())
                {
                    HabilitarControles(parte1Controles, false);
                    HabilitarControles(parte2Controles, true);
                    txtProduto.Focus(); 
                }
            }
        }

        private void CalcularTotalItem()
        {
            if (decimal.TryParse(txtQuantidade.Text, out decimal quantidade) &&
                decimal.TryParse(txtValorUnitario.Text, out decimal valorUnitario))
            {
                decimal totalItem = quantidade * valorUnitario;
                txtTotal.Text = totalItem.ToString("F2");
            }
            else
            {
                txtTotal.Text = "0.00";
            }
        }

        private void txtQuantidade_TextChanged(object sender, EventArgs e)
        {
            CalcularTotalItem();
        }

        private void txtValorUnitario_TextChanged(object sender, EventArgs e)
        {
            CalcularTotalItem();
        }

        private void btnAdicionarProduto_Click(object sender, EventArgs e)
        {
            if (produtoSelecionado == null)
            {
                MessageBox.Show("Por favor, pesquise e selecione um produto válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtQuantidade.Text, out decimal quantidade) || quantidade <= 0)
            {
                MessageBox.Show("Por favor, insira uma quantidade válida.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuantidade.Focus();
                return;
            }

            if (!decimal.TryParse(txtValorUnitario.Text, out decimal valorUnitario) || valorUnitario < 0)
            {
                MessageBox.Show("Por favor, insira um valor unitário válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtValorUnitario.Focus();
                return;
            }

            decimal totalItem = quantidade * valorUnitario;
            txtTotal.Text = totalItem.ToString("F2");

            ListViewItem item = new ListViewItem(produtoSelecionado.Id.ToString());
            item.SubItems.Add(produtoSelecionado.NomeProduto);
            item.SubItems.Add("UN");
            item.SubItems.Add(quantidade.ToString());
            item.SubItems.Add(valorUnitario.ToString("F2"));
            item.SubItems.Add(totalItem.ToString("F2"));
            listViewProdutos.Items.Add(item);

            produtoSelecionado = null;
            txtProduto.Clear();
            txtQuantidade.Clear();
            txtValorUnitario.Clear();
            txtTotal.Clear();
            txtProduto.Focus();

            if (listViewProdutos.Items.Count == 1)
            {
                HabilitarControles(parte1Controles, false);
                HabilitarControles(parte3Controles, true);
                btnSalvar.Enabled = true;
            }
        }

        private void btnRemoverProduto_Click(object sender, EventArgs e)
        {

            if (listViewProdutos.SelectedItems.Count > 0)
            {
                listViewProdutos.Items.Remove(listViewProdutos.SelectedItems[0]);
            }
            else
            {
                MessageBox.Show("Selecione um produto para remover.");
            }


            if (listViewProdutos.Items.Count == 0)
            {
                HabilitarControles(parte3Controles, false);
                btnSalvar.Enabled = false; 
            }
        }

        private void btnPesquisarFornecedor_Click(object sender, EventArgs e)
        {
            if (oFrmConsultaFornecedor == null)
            {
                MessageBox.Show("O formulário de consulta de fornecedor não foi inicializado corretamente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            oFrmConsultaFornecedor.ModoSelecao = true;

            var resultado = oFrmConsultaFornecedor.ShowDialog();

            if (resultado == DialogResult.OK && oFrmConsultaFornecedor.FornecedorSelecionado != null)
            {
                var fornecedor = oFrmConsultaFornecedor.FornecedorSelecionado;
                txtIDFornecedor.Text = fornecedor.Id.ToString();
                txtFornecedor.Text = fornecedor.Nome;
            }

            oFrmConsultaFornecedor.ModoSelecao = false;
        }

        private void btnPesquisarProduto_Click(object sender, EventArgs e)
        {
            if (oFrmConsultaProduto == null)
            {
                MessageBox.Show("O formulário de consulta de produto não foi inicializado corretamente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            oFrmConsultaProduto.ModoSelecao = true;
            var resultado = oFrmConsultaProduto.ShowDialog();

            if (resultado == DialogResult.OK && oFrmConsultaProduto.ProdutoSelecionado != null)
            {
                this.produtoSelecionado = oFrmConsultaProduto.ProdutoSelecionado;

                txtProduto.Text = this.produtoSelecionado.NomeProduto;
                txtIdProduto.Text = this.produtoSelecionado.Id.ToString();
                txtValorUnitario.Text = this.produtoSelecionado.PrecoCusto.ToString("F2");
                txtQuantidade.Focus();
            }

            oFrmConsultaProduto.ModoSelecao = false;
        }

        private void btnAdicionarCondPgto_Click(object sender, EventArgs e)
        {
            if (oFrmConsultaCondPgto == null)
            {
                MessageBox.Show("O formulário de consulta de Condição de Pagamento não foi inicializado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            oFrmConsultaCondPgto.ModoSelecao = true;
            var resultado = oFrmConsultaCondPgto.ShowDialog();

            if (resultado == DialogResult.OK && oFrmConsultaCondPgto.CondicaoSelecionado != null)
            {
                var condicao = oFrmConsultaCondPgto.CondicaoSelecionado;

                txtIdCondPgto.Text = condicao.Id.ToString();
                txtCondPgto.Text = condicao.Descricao;
                txtCondPgto.Tag = condicao.Id; 

                listViewCondPgto.Items.Clear();
                if (condicao.Parcelas != null)
                {
                    foreach (var parcela in condicao.Parcelas)
                    {
                        ListViewItem item = new ListViewItem(parcela.NumParcela.ToString());
                        item.SubItems.Add(parcela.PrazoDias.ToString());
                        item.SubItems.Add(parcela.Porcentagem.ToString("N2") + "%");
                        item.SubItems.Add(parcela.FormaPagamento?.Descricao ?? "N/D");
                        listViewCondPgto.Items.Add(item);
                    }
                }

                HabilitarControles(parte2Controles, false);
                btnLimparProduto.Enabled = true;

                txtFrete.Enabled = false;
                txtSeguro.Enabled = false;
                txtDespesas.Enabled = false;
                txtValorTotal.Enabled = false; 
            }

            oFrmConsultaCondPgto.ModoSelecao = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!ValidarParte1())
            {
                MessageBox.Show("Os dados do cabeçalho da nota (Modelo, Série, Número, Fornecedor) são obrigatórios.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (listViewProdutos.Items.Count == 0)
            {
                MessageBox.Show("É necessário adicionar pelo menos um produto à compra.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Compra novaCompra = new Compra();

                novaCompra.Modelo = txtCodigo.Text;
                novaCompra.Serie = txtSerie.Text;
                novaCompra.NumeroNota = int.Parse(txtNumero.Text);
                novaCompra.FornecedorId = int.Parse(txtIDFornecedor.Text);
                novaCompra.DataEmissao = dtpEmissao.Value;
                novaCompra.DataChegada = dtpChegada.Value;
                novaCompra.Ativo = !chkInativo.Checked;

                novaCompra.ValorFrete = string.IsNullOrWhiteSpace(txtFrete.Text) ? 0 : Convert.ToDecimal(txtFrete.Text);
                novaCompra.Seguro = string.IsNullOrWhiteSpace(txtSeguro.Text) ? 0 : Convert.ToDecimal(txtSeguro.Text);
                novaCompra.Despesas = string.IsNullOrWhiteSpace(txtDespesas.Text) ? 0 : Convert.ToDecimal(txtDespesas.Text);
                novaCompra.ValorTotal = string.IsNullOrWhiteSpace(txtValorTotal.Text) ? 0 : Convert.ToDecimal(txtValorTotal.Text);

                foreach (ListViewItem item in listViewProdutos.Items)
                {
                    ItemCompra novoItem = new ItemCompra
                    {
                        ProdutoId = int.Parse(item.SubItems[0].Text),
                        Quantidade = decimal.Parse(item.SubItems[3].Text),
                        ValorUnitario = decimal.Parse(item.SubItems[4].Text),
                        ValorTotalItem = decimal.Parse(item.SubItems[5].Text)
                    };
                    novaCompra.Itens.Add(novoItem);
                }

                if (txtCondPgto.Tag != null)
                {
                    novaCompra.CondicaoPagamentoId = (int)txtCondPgto.Tag;
                }

                controller.Salvar(novaCompra);

                MessageBox.Show("Compra salva com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (InvalidOperationException opEx)
            {
                MessageBox.Show(opEx.Message, "Erro de Operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao salvar a compra: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimparProduto_Click(object sender, EventArgs e)
        {
            LimparTxt();
        }

        private void btnLimparCondPgto_Click(object sender, EventArgs e)
        {
            listViewCondPgto.Items.Clear();
            txtCondPgto.Clear();
            txtIdCondPgto.Clear(); 
            txtCondPgto.Tag = null;

            HabilitarControles(parte2Controles, true);

            txtFrete.Enabled = true;
            txtSeguro.Enabled = true;
            txtDespesas.Enabled = true;
            txtValorTotal.Enabled = true;
        }
    }
}
