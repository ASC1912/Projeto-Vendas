using Projeto.Controller;
using Projeto.Models;
using Projeto.Utils;
using Projeto.Views.Consultas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Projeto.Views.Cadastros
{
    public partial class frmCadastroCompra : Projeto.frmBase
    {
        #region Variáveis e Propriedades

        private Compra aCompra;
        private CompraController controller = new CompraController();
        private FornecedorController fornecedorController = new FornecedorController();
        private ProdutoController produtoController = new ProdutoController();
        private CondicaoPagamentoController condicaoPagamentoController = new CondicaoPagamentoController();

        private frmConsultaFornecedor oFrmConsultaFornecedor;
        private frmConsultaProduto oFrmConsultaProduto;
        private frmConsultaCondPgto oFrmConsultaCondPgto;
        private Produto produtoSelecionado;
        private int condicaoSelecionadoId = -1;

        private List<Control> parte1Controles;
        private List<Control> parte2Controles;
        private List<Control> parte3Controles;

        #endregion



        #region Construtor e Inicialização

        public frmCadastroCompra()
        {
            InitializeComponent();
            AgruparControles();
            ConfigurarEstadoInicial();

            txtFrete.TextChanged += new System.EventHandler(this.txtCustosExtras_TextChanged);
            txtSeguro.TextChanged += new System.EventHandler(this.txtCustosExtras_TextChanged);
            txtDespesas.TextChanged += new System.EventHandler(this.txtCustosExtras_TextChanged);
        }

        #endregion



        #region Conexões com outros Formulários

        public void setFrmConsultaFornecedor(object obj)
        {
            if (obj != null) oFrmConsultaFornecedor = (frmConsultaFornecedor)obj;
        }

        public void setFrmConsultaProduto(object obj)
        {
            if (obj != null) oFrmConsultaProduto = (frmConsultaProduto)obj;
        }

        public void setFrmConsultaCondPgto(object obj)
        {
            if (obj != null) oFrmConsultaCondPgto = (frmConsultaCondPgto)obj;
        }

        #endregion



        #region Gestão de Estado do Formulário

        private void AgruparControles()
        {
            parte1Controles = new List<Control> { txtCodigo, txtSerie, txtNumero, txtIDFornecedor, txtFornecedor, btnPesquisarFornecedor, dtpEmissao, dtpChegada };
            parte2Controles = new List<Control> { txtIdProduto, txtProduto, btnPesquisarProduto, txtQuantidade, txtValorUnitario, txtTotal, btnAdicionarProduto, btnEditarProduto, btnRemoverProduto, btnLimparProduto, listViewProdutos };
            parte3Controles = new List<Control> { txtFrete, txtSeguro, txtDespesas, txtValorTotal, txtIdCondPgto, txtCondPgto, btnAdicionarCondPgto, btnLimparCondPgto, listViewCondPgto };
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
            if (obj != null) aCompra = (Compra)obj;
            if (ctrl != null) controller = (CompraController)ctrl;
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
            condicaoSelecionadoId = -1;
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

            if (aCompra.CondicaoPagamentoId.HasValue)
            {
                txtIdCondPgto.Text = aCompra.CondicaoPagamentoId.Value.ToString();
                txtCondPgto.Text = aCompra.NomeCondPgto;
                condicaoSelecionadoId = aCompra.CondicaoPagamentoId.Value;
            }

            lblDataCriacao.Text = aCompra.DataCadastro.HasValue ? $"Criado em: {aCompra.DataCadastro.Value:dd/MM/yyyy HH:mm}" : "Criado em: -";
            lblDataModificacao.Text = aCompra.DataAlteracao.HasValue ? $"Modificado em: {aCompra.DataAlteracao.Value:dd/MM/yyyy HH:mm}" : "Modificado em: -";

            CalculaTotalCompra();
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
        #endregion



        #region Event Handlers de Controles

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
                condicaoSelecionadoId = -1;
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
                condicaoSelecionadoId = condicao.Id;
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
                CalculaTotalCompra();
            }
            else
            {
                MessageBox.Show("Condição de Pagamento não encontrada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCondPgto.Clear();
                listViewCondPgto.Items.Clear();
                condicaoSelecionadoId = -1;
            }
        }

        private void txtQuantidade_TextChanged(object sender, EventArgs e) => CalcularTotalItem();
        private void txtValorUnitario_TextChanged(object sender, EventArgs e) => CalcularTotalItem();
        private void txtCustosExtras_TextChanged(object sender, EventArgs e) => CalculaTotalCompra();

        private void btnAdicionarProduto_Click(object sender, EventArgs e)
        {
            if (!ValidarCabecalho())
            {
                MessageBox.Show("Por favor, preencha todos os dados do cabeçalho da nota (Modelo, Série, Número e Fornecedor) antes de adicionar produtos.", "Dados Incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }

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
            item.SubItems.Add(produtoSelecionado.NomeUnidadeMedida ?? "UN"); 
            item.SubItems.Add(quantidade.ToString());
            item.SubItems.Add(valorUnitario.ToString("F2"));
            item.SubItems.Add(totalItem.ToString("F2"));
            listViewProdutos.Items.Add(item);
            produtoSelecionado = null;
            txtProduto.Clear();
            txtIdProduto.Clear();
            txtQuantidade.Clear();
            txtValorUnitario.Clear();
            txtTotal.Clear();
            txtProduto.Focus();
            if (listViewProdutos.Items.Count >= 1)
            {
                HabilitarControles(parte1Controles, false);
                HabilitarControles(parte3Controles, true);
                btnSalvar.Enabled = true;
            }
            CalculaTotalCompra();
        }

        private void btnRemoverProduto_Click(object sender, EventArgs e)
        {
            if (listViewProdutos.SelectedItems.Count > 0)
            {
                listViewProdutos.Items.Remove(listViewProdutos.SelectedItems[0]);
                CalculaTotalCompra();
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
            if (oFrmConsultaFornecedor == null) return;
            oFrmConsultaFornecedor.ModoSelecao = true;
            if (oFrmConsultaFornecedor.ShowDialog() == DialogResult.OK && oFrmConsultaFornecedor.FornecedorSelecionado != null)
            {
                var fornecedor = oFrmConsultaFornecedor.FornecedorSelecionado;
                txtIDFornecedor.Text = fornecedor.Id.ToString();
                txtFornecedor.Text = fornecedor.Nome;
            }
            oFrmConsultaFornecedor.ModoSelecao = false;
        }

        private void btnPesquisarProduto_Click(object sender, EventArgs e)
        {
            if (oFrmConsultaProduto == null) return;
            oFrmConsultaProduto.ModoSelecao = true;
            if (oFrmConsultaProduto.ShowDialog() == DialogResult.OK && oFrmConsultaProduto.ProdutoSelecionado != null)
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
            if (oFrmConsultaCondPgto == null) return;
            oFrmConsultaCondPgto.ModoSelecao = true;
            if (oFrmConsultaCondPgto.ShowDialog() == DialogResult.OK && oFrmConsultaCondPgto.CondicaoSelecionado != null)
            {
                var condicao = oFrmConsultaCondPgto.CondicaoSelecionado;
                txtIdCondPgto.Text = condicao.Id.ToString();
                txtCondPgto.Text = condicao.Descricao;
                condicaoSelecionadoId = condicao.Id;
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
                CalculaTotalCompra();
            }
            oFrmConsultaCondPgto.ModoSelecao = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!ValidarDadosGerais())
            {
                return;
            }
            try
            {
                Compra novaCompra = MontarObjetoCompra();

                GerarParcelas(novaCompra);

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

       
        private void btnLimparProduto_Click(object sender, EventArgs e) => LimparTxt();

        private void btnLimparCondPgto_Click(object sender, EventArgs e)
        {
            listViewCondPgto.Items.Clear();
            txtCondPgto.Clear();
            txtIdCondPgto.Clear();
            condicaoSelecionadoId = -1;
        }
        #endregion



        #region Lógica de Cálculos e Suporte

        private bool ValidarCabecalho()
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

        private bool ValidarDadosGerais()
        {
            if (!int.TryParse(txtNumero.Text, out _) || !int.TryParse(txtIDFornecedor.Text, out _))
            {
                MessageBox.Show("Os dados do cabeçalho da nota (Modelo, Série, Número, Fornecedor) são obrigatórios e devem ser válidos.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (listViewProdutos.Items.Count == 0)
            {
                MessageBox.Show("É necessário adicionar pelo menos um produto à compra.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }


        private Compra MontarObjetoCompra()
        {
            Compra compra = new Compra
            {
                Modelo = txtCodigo.Text,
                Serie = txtSerie.Text,
                NumeroNota = int.Parse(txtNumero.Text),
                FornecedorId = int.Parse(txtIDFornecedor.Text),
                DataEmissao = dtpEmissao.Value,
                DataChegada = dtpChegada.Value,
                Ativo = !chkInativo.Checked,
                ValorFrete = string.IsNullOrWhiteSpace(txtFrete.Text) ? 0 : Convert.ToDecimal(txtFrete.Text),
                Seguro = string.IsNullOrWhiteSpace(txtSeguro.Text) ? 0 : Convert.ToDecimal(txtSeguro.Text),
                Despesas = string.IsNullOrWhiteSpace(txtDespesas.Text) ? 0 : Convert.ToDecimal(txtDespesas.Text),
                ValorTotal = string.IsNullOrWhiteSpace(txtValorTotal.Text) ? 0 : Convert.ToDecimal(txtValorTotal.Text),
                CondicaoPagamentoId = condicaoSelecionadoId > 0 ? (int?)condicaoSelecionadoId : null
            };

            foreach (ListViewItem item in listViewProdutos.Items)
            {
                compra.Itens.Add(new ItemCompra
                {
                    ProdutoId = int.Parse(item.SubItems[0].Text),
                    Quantidade = decimal.Parse(item.SubItems[3].Text),
                    ValorUnitario = decimal.Parse(item.SubItems[4].Text),
                    ValorTotalItem = decimal.Parse(item.SubItems[5].Text)
                });
            }

            return compra;
        }

        private void CalcularTotalItem()
        {
            if (decimal.TryParse(txtQuantidade.Text, out decimal quantidade) &&
                decimal.TryParse(txtValorUnitario.Text, out decimal valorUnitario))
            {
                txtTotal.Text = (quantidade * valorUnitario).ToString("F2");
            }
            else
            {
                txtTotal.Text = "0.00";
            }
        }

        private void CalculaTotalCompra()
        {
            decimal totalProdutos = 0;
            foreach (ListViewItem item in listViewProdutos.Items)
            {
                totalProdutos += Convert.ToDecimal(item.SubItems[5].Text);
            }
            lblTotalProdutos.Text = $"Total Produtos (R$): {totalProdutos:F2}";

            decimal.TryParse(txtFrete.Text, out decimal frete);
            decimal.TryParse(txtSeguro.Text, out decimal seguro);
            decimal.TryParse(txtDespesas.Text, out decimal despesas);

            decimal valorTotalCompra = totalProdutos + frete + seguro + despesas;
            txtValorTotal.Text = valorTotalCompra.ToString("F2");

            CalcularEExibirParcelas();
        }

        private void CalcularEExibirParcelas()
        {
            decimal totalParcelas = 0;
            if (!decimal.TryParse(txtValorTotal.Text, out decimal valorTotalCompra) || valorTotalCompra <= 0)
            {
                foreach (ListViewItem item in listViewCondPgto.Items)
                {
                    while (item.SubItems.Count <= 4) item.SubItems.Add("");
                    item.SubItems[4].Text = "0,00";
                }
                lblTotalCondiçãoPgto.Text = "Total (R$): 0,00";
                return;
            }
            foreach (ListViewItem item in listViewCondPgto.Items)
            {
                decimal porcentagem = decimal.Parse(item.SubItems[2].Text.Replace("%", "").Trim());
                decimal valorParcela = valorTotalCompra * (porcentagem / 100);
                totalParcelas += valorParcela;
                while (item.SubItems.Count <= 4) item.SubItems.Add("");
                item.SubItems[4].Text = valorParcela.ToString("F2");
            }
            lblTotalCondiçãoPgto.Text = $"Total (R$): {totalParcelas:F2}";
        }

        #endregion



        #region Preparação de Dados

        private void GerarParcelas(Compra compra)
        {
            compra.ParcelasCompra.Clear();
            if (condicaoSelecionadoId <= 0 || listViewCondPgto.Items.Count == 0)
            {
                return;
            }
            DateTime dataBase = dtpEmissao.Value;
            foreach (ListViewItem item in listViewCondPgto.Items)
            {
                int numeroParcela = int.Parse(item.SubItems[0].Text);
                int prazoDias = int.Parse(item.SubItems[1].Text);
                decimal porcentagem = decimal.Parse(item.SubItems[2].Text.Replace("%", "").Trim());
                decimal valorParcela = compra.ValorTotal * (porcentagem / 100);
                DateTime dataVencimento = dataBase.AddDays(prazoDias);
                ParcelaCompra novaParcela = new ParcelaCompra
                {
                    CompraModelo = compra.Modelo,
                    CompraSerie = compra.Serie,
                    CompraNumeroNota = compra.NumeroNota,
                    CompraFornecedorId = compra.FornecedorId,
                    NumeroParcela = numeroParcela,
                    ValorParcela = Math.Round(valorParcela, 2),
                    DataVencimento = dataVencimento
                };
                compra.ParcelasCompra.Add(novaParcela);
            }
        }

        #endregion
    }
}