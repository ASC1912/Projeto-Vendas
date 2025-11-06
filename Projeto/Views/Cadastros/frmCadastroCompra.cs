using Projeto.Controller;
using Projeto.Models;
using Projeto.Utils;
using Projeto.Views.Consultas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto.Views.Cadastros
{
    public partial class frmCadastroCompra : Projeto.frmBase
    {
        #region Variáveis e Propriedades

        private Compra aCompra = new Compra();
        private CompraController controller = new CompraController();
        private FornecedorController fornecedorController = new FornecedorController();
        private ProdutoController produtoController = new ProdutoController();
        private CondicaoPagamentoController condicaoPagamentoController = new CondicaoPagamentoController();
        private FormaPagamentoController formaPagamentoController = new FormaPagamentoController();

        private frmConsultaFornecedor oFrmConsultaFornecedor;
        private frmConsultaProduto oFrmConsultaProduto;
        private frmConsultaCondPgto oFrmConsultaCondPgto;

        private Produto produtoSelecionado;
        private CondicaoPagamento condicaoPagamentoSelecionada;

        private List<Control> parte1Controles;
        private List<Control> parte2Controles;
        private List<Control> parte3Controles;

        public bool modoCancelamento = false;

        #endregion

        #region Construtor e Inicialização

        public frmCadastroCompra()
        {
            InitializeComponent();

            DateTime dataAtual = DateTime.Now;

            dtpEmissao.Value = dataAtual;
            dtpChegada.Value = dataAtual;

            dtpEmissao.MaxDate = dataAtual;

            dtpChegada.MinDate = dataAtual.Date;

            AgruparControles();
            ConfigurarEstadoInicial();

            txtFrete.TextChanged += new System.EventHandler(this.txtCustosExtras_TextChanged);
            txtSeguro.TextChanged += new System.EventHandler(this.txtCustosExtras_TextChanged);
            txtDespesas.TextChanged += new System.EventHandler(this.txtCustosExtras_TextChanged);

            this.dtpEmissao.ValueChanged += new System.EventHandler(this.dtpEmissao_ValueChanged);
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
            HabilitarControles(parte2Controles, false);
            HabilitarControles(parte3Controles, false);
            btnSalvar.Enabled = false;
        }

        private void AtualizarEstadoDosControles()
        {
            bool cabecalhoValido = !string.IsNullOrWhiteSpace(txtCodigo.Text) &&
                                 !string.IsNullOrWhiteSpace(txtSerie.Text) &&
                                 !string.IsNullOrWhiteSpace(txtNumero.Text) &&
                                 !string.IsNullOrWhiteSpace(txtIDFornecedor.Text);

            bool temItens = listViewProdutos.Items.Count > 0;

            HabilitarControles(parte2Controles, cabecalhoValido);
            HabilitarControles(parte3Controles, temItens);
            btnSalvar.Enabled = temItens;
        }

        private void HabilitarControles(List<Control> controles, bool habilitar)
        {
            foreach (var control in controles)
            {
                control.Enabled = habilitar;
            }
        }

        public override void BloquearTxt()
        {
            HabilitarControles(parte1Controles, false);
            HabilitarControles(parte2Controles, false);
            HabilitarControles(parte3Controles, false);
            btnSalvar.Enabled = false;
        }

        public override void DesbloquearTxt()
        {
            HabilitarControles(parte1Controles, true);
        }

        #endregion

        #region Carregamento e Limpeza de Dados

        public override void ConhecaObj(object obj, object ctrl)
        {
            if (obj != null) aCompra = (Compra)obj;
            if (ctrl != null) controller = (CompraController)ctrl;
        }

        public override void LimparTxt()
        {
            aCompra = new Compra();
            txtCodigo.Text = "0";
            txtSerie.Clear();
            txtNumero.Clear();
            txtIDFornecedor.Clear();
            txtFornecedor.Clear();

            DateTime dataAgora = DateTime.Now;

            dtpEmissao.MaxDate = dataAgora;
            dtpEmissao.Value = dataAgora;
            dtpChegada.Value = dataAgora;

            LimparCamposItem();
            listViewProdutos.Items.Clear();

            txtFrete.Text = "0,00";
            txtSeguro.Text = "0,00";
            txtDespesas.Text = "0,00";
            txtValorTotal.Clear();
            LimparCamposCondPgto();

            lblMotivoCancelamento.Visible = false;
            lblMotivoCancelamento.Text = "Motivo do Cancelamento: ";

            ConfigurarEstadoInicial();
            CalculaTotalCompra();
        }

        public override async void CarregaTxt()
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

            DateTime dataEmissao = aCompra.DataEmissao ?? DateTime.Now;
            if (dataEmissao > dtpEmissao.MaxDate)
            {
                dtpEmissao.MaxDate = dataEmissao;
            }
            dtpEmissao.Value = dataEmissao;

            DateTime dataChegada = aCompra.DataChegada ?? DateTime.Now;
            if (dataChegada < dtpChegada.MinDate)
            {
                dtpChegada.MinDate = dataChegada;
            }
            dtpChegada.Value = dataChegada;

            listViewProdutos.Items.Clear();
            if (aCompra.Itens != null)
            {
                foreach (var itemCompra in aCompra.Itens)
                {
                    ListViewItem item = new ListViewItem(itemCompra.ProdutoId.ToString());
                    item.SubItems.AddRange(new string[] {
                        itemCompra.NomeProduto,
                        itemCompra.NomeUnidadeMedida ?? "UN",
                        itemCompra.Quantidade.ToString(),
                        itemCompra.ValorUnitario.ToString("F2"),
                        itemCompra.ValorTotalItem.ToString("F2")
                    });
                    listViewProdutos.Items.Add(item);
                }
            }

            txtFrete.Text = aCompra.ValorFrete.ToString("F2");
            txtSeguro.Text = aCompra.Seguro.ToString("F2");
            txtDespesas.Text = aCompra.Despesas.ToString("F2");

            if (aCompra.CondicaoPagamentoId.HasValue)
            {
                condicaoPagamentoSelecionada = await condicaoPagamentoController.BuscarPorId(aCompra.CondicaoPagamentoId.Value);
                if (condicaoPagamentoSelecionada != null)
                {
                    txtIdCondPgto.Text = condicaoPagamentoSelecionada.Id.ToString();
                    txtCondPgto.Text = condicaoPagamentoSelecionada.Descricao;
                    await AtualizarListViewCondPgto(condicaoPagamentoSelecionada);
                }
            }

            chkInativo.Checked = !aCompra.Ativo;
            lblDataCriacao.Text = aCompra.DataCadastro.HasValue ? $"Criado em: {aCompra.DataCadastro.Value:dd/MM/yyyy HH:mm}" : "Criado em: -";
            lblDataModificacao.Text = aCompra.DataAlteracao.HasValue ? $"Modificado em: {aCompra.DataAlteracao.Value:dd/MM/yyyy HH:mm}" : "Modificado em: -";

            if (!aCompra.Ativo)
            {
                lblMotivoCancelamento.Visible = true;
                lblMotivoCancelamento.Text = "Motivo do Cancelamento: " + aCompra.Observacao;
                BloquearTxt();
                btnSalvar.Enabled = false;
            }
            else
            {
                lblMotivoCancelamento.Visible = false;

                if (txtSerie.Enabled)
                {
                    AtualizarEstadoDosControles();
                }
            }

            CalculaTotalCompra();
        }

        #endregion

        #region Eventos de Controles

        private void dtpEmissao_ValueChanged(object sender, EventArgs e)
        {
            dtpChegada.MinDate = dtpEmissao.Value.Date;
            if (dtpChegada.Value.Date < dtpEmissao.Value.Date)
            {
                dtpChegada.Value = dtpEmissao.Value.Date;
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (modoCancelamento)
            {
                using (Form prompt = new Form())
                {
                    prompt.Width = 500; prompt.Height = 250;
                    prompt.Text = "Motivo do Cancelamento";
                    prompt.StartPosition = FormStartPosition.CenterParent;
                    prompt.FormBorderStyle = FormBorderStyle.FixedDialog;
                    prompt.MinimizeBox = false; prompt.MaximizeBox = false;

                    Label textLabel = new Label() { Left = 50, Top = 20, Width = 400, Text = "Para cancelar esta nota, digite o motivo do cancelamento abaixo." };
                    TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400, Height = 80, Multiline = true, ScrollBars = ScrollBars.Vertical, MaxLength = 255 };
                    Button confirmation = new Button() { Text = "Confirmar", Left = 240, Width = 100, Top = 150, DialogResult = DialogResult.OK };
                    Button cancel = new Button() { Text = "Voltar", Left = 350, Width = 100, Top = 150, DialogResult = DialogResult.Cancel };

                    confirmation.Enabled = false;
                    textBox.TextChanged += (s, ev) => { confirmation.Enabled = !string.IsNullOrWhiteSpace(textBox.Text); };
                    confirmation.Click += (s, ev) => { prompt.Close(); };
                    cancel.Click += (s, ev) => { prompt.Close(); };

                    prompt.Controls.AddRange(new Control[] { textBox, confirmation, cancel, textLabel });
                    prompt.AcceptButton = confirmation;
                    prompt.CancelButton = cancel;

                    if (prompt.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            if (aCompra != null)
                            {
                                aCompra.Observacao = textBox.Text;
                                controller.Cancelar(aCompra);
                                MessageBox.Show("Compra cancelada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                            }
                        }
                        catch (Exception ex) { MessageBox.Show($"Erro ao cancelar a compra: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                }
                return;
            }

            if (dtpEmissao.Value.Date > DateTime.Now.Date)
            {
                MessageBox.Show("A Data de Emissão não pode ser posterior à data atual.", "Data Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpEmissao.Focus();
                return;
            }

            if (dtpChegada.Value.Date < dtpEmissao.Value.Date)
            {
                MessageBox.Show("A Data de Chegada não pode ser anterior à Data de Emissão.", "Data Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpChegada.Focus();
                return;
            }

            if (!ValidarDadosGerais()) return;

            try
            {
                Compra novaCompra = MontarObjetoCompra();
                GerarParcelas(novaCompra);
                controller.Salvar(novaCompra);
                MessageBox.Show("Compra salva com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (InvalidOperationException opEx) { MessageBox.Show(opEx.Message, "Erro de Operação", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show("Ocorreu um erro ao salvar a compra: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private async void txtIDFornecedor_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDFornecedor.Text)) { txtFornecedor.Clear(); return; }
            if (int.TryParse(txtIDFornecedor.Text, out int id))
            {
                Fornecedor f = await fornecedorController.BuscarPorId(id);
                txtFornecedor.Text = f?.Nome;
                if (f == null) { MessageBox.Show("Fornecedor não encontrado."); txtFornecedor.Clear(); }
            }
            else { MessageBox.Show("ID de Fornecedor inválido."); txtFornecedor.Clear(); }
            AtualizarEstadoDosControles();
        }

        private async void txtIdProduto_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdProduto.Text)) { LimparCamposItemParcial(); return; }
            if (int.TryParse(txtIdProduto.Text, out int id))
            {
                Produto p = await produtoController.BuscarPorId(id);
                if (p != null)
                {
                    produtoSelecionado = p;
                    txtProduto.Text = p.NomeProduto;
                    txtValorUnitario.Text = p.PrecoCusto.ToString("F2");
                    txtQuantidade.Text = "1";
                    txtQuantidade.Focus();
                }
                else { MessageBox.Show("Produto não encontrado."); LimparCamposItemParcial(); }
            }
            else { MessageBox.Show("ID do Produto inválido."); LimparCamposItemParcial(); }
        }

        private async void txtIdCondPgto_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdCondPgto.Text)) { LimparCamposCondPgto(); return; }
            if (int.TryParse(txtIdCondPgto.Text, out int id))
            {
                condicaoPagamentoSelecionada = await condicaoPagamentoController.BuscarPorId(id);
                if (condicaoPagamentoSelecionada != null)
                {
                    txtCondPgto.Text = condicaoPagamentoSelecionada.Descricao;
                    await AtualizarListViewCondPgto(condicaoPagamentoSelecionada);
                }
                else { MessageBox.Show("Condição de Pagamento não encontrada."); LimparCamposCondPgto(); }
            }
            else { MessageBox.Show("ID da Condição de Pagamento inválido."); LimparCamposCondPgto(); }
        }

        private void txtQuantidade_TextChanged(object sender, EventArgs e) => CalcularTotalItem();
        private void txtValorUnitario_TextChanged(object sender, EventArgs e) => CalcularTotalItem();
        private void txtCustosExtras_TextChanged(object sender, EventArgs e) => CalculaTotalCompra();

        private void btnAdicionarProduto_Click(object sender, EventArgs e)
        {
            if (!ValidarCabecalho())
            {
                MessageBox.Show("Preencha todos os dados do cabeçalho da nota antes de adicionar produtos.", "Dados Incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (produtoSelecionado == null || !decimal.TryParse(txtQuantidade.Text, out decimal qtd) || qtd <= 0 || !decimal.TryParse(txtValorUnitario.Text, out _))
            {
                MessageBox.Show("Selecione um produto e informe quantidade e valor válidos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ListViewItem item = new ListViewItem(produtoSelecionado.Id.ToString());
            item.SubItems.AddRange(new string[] {
                produtoSelecionado.NomeProduto,
                produtoSelecionado.NomeUnidadeMedida ?? "UN",
                txtQuantidade.Text,
                txtValorUnitario.Text,
                txtTotal.Text
            });
            listViewProdutos.Items.Add(item);

            LimparCamposItem();
            CalculaTotalCompra();
            AtualizarEstadoDosControles();
        }

        private void btnRemoverProduto_Click(object sender, EventArgs e)
        {
            if (listViewProdutos.SelectedItems.Count > 0)
            {
                listViewProdutos.Items.Remove(listViewProdutos.SelectedItems[0]);
                CalculaTotalCompra();
                AtualizarEstadoDosControles();
            }
            else { MessageBox.Show("Selecione um produto para remover."); }
        }

        private void btnPesquisarFornecedor_Click(object sender, EventArgs e)
        {
            if (oFrmConsultaFornecedor == null) oFrmConsultaFornecedor = new frmConsultaFornecedor();
            oFrmConsultaFornecedor.ModoSelecao = true;
            if (oFrmConsultaFornecedor.ShowDialog() == DialogResult.OK && oFrmConsultaFornecedor.FornecedorSelecionado != null)
            {
                var f = oFrmConsultaFornecedor.FornecedorSelecionado;
                txtIDFornecedor.Text = f.Id.ToString();
                txtFornecedor.Text = f.Nome;
                txtIDFornecedor_Leave(sender, e); 
            }
            oFrmConsultaFornecedor.ModoSelecao = false;
        }

        private void btnPesquisarProduto_Click(object sender, EventArgs e)
        {
            if (oFrmConsultaProduto == null) oFrmConsultaProduto = new frmConsultaProduto();
            oFrmConsultaProduto.ModoSelecao = true;
            if (oFrmConsultaProduto.ShowDialog() == DialogResult.OK && oFrmConsultaProduto.ProdutoSelecionado != null)
            {
                produtoSelecionado = oFrmConsultaProduto.ProdutoSelecionado;
                txtIdProduto.Text = produtoSelecionado.Id.ToString();
                txtProduto.Text = produtoSelecionado.NomeProduto;
                txtValorUnitario.Text = produtoSelecionado.PrecoCusto.ToString("F2");
                txtQuantidade.Text = "1";
                txtQuantidade.Focus();
            }
            oFrmConsultaProduto.ModoSelecao = false;
        }

        private async void btnAdicionarCondPgto_Click(object sender, EventArgs e)
        {
            if (oFrmConsultaCondPgto == null) oFrmConsultaCondPgto = new frmConsultaCondPgto();
            oFrmConsultaCondPgto.ModoSelecao = true;
            if (oFrmConsultaCondPgto.ShowDialog() == DialogResult.OK && oFrmConsultaCondPgto.CondicaoSelecionado != null)
            {
                condicaoPagamentoSelecionada = oFrmConsultaCondPgto.CondicaoSelecionado;
                txtIdCondPgto.Text = condicaoPagamentoSelecionada.Id.ToString();
                txtCondPgto.Text = condicaoPagamentoSelecionada.Descricao;
                await AtualizarListViewCondPgto(condicaoPagamentoSelecionada);
            }
            oFrmConsultaCondPgto.ModoSelecao = false;
        }

        private void btnLimparProduto_Click(object sender, EventArgs e)
        {
            listViewProdutos.Items.Clear();
            CalculaTotalCompra();
            AtualizarEstadoDosControles();
        }

        private void btnLimparCondPgto_Click(object sender, EventArgs e) => LimparCamposCondPgto();

        #endregion

        #region Lógica de Cálculos e Suporte

        private void LimparCamposItem()
        {
            produtoSelecionado = null;
            txtIdProduto.Clear();
            txtProduto.Clear();
            txtQuantidade.Clear();
            txtValorUnitario.Clear();
            txtTotal.Clear();
            btnPesquisarProduto.Focus();
        }

        private void LimparCamposItemParcial()
        {
            txtProduto.Clear();
            txtValorUnitario.Clear();
            produtoSelecionado = null;
        }

        private void LimparCamposCondPgto()
        {
            txtIdCondPgto.Clear();
            txtCondPgto.Clear();
            listViewCondPgto.Items.Clear();
            condicaoPagamentoSelecionada = null;
            CalculaTotalCompra();
        }

        private async Task AtualizarListViewCondPgto(CondicaoPagamento condicao)
        {
            listViewCondPgto.Items.Clear();

            DateTime dataBase = dtpEmissao.Value.Date;

            if (condicao?.Parcelas != null)
            {
                foreach (var parcela in condicao.Parcelas.OrderBy(p => p.NumParcela))
                {
                    ListViewItem item = new ListViewItem(parcela.NumParcela.ToString());

                    DateTime dataVencimento = dataBase.AddDays(parcela.PrazoDias);
                    item.SubItems.Add(dataVencimento.ToString("dd/MM/yyyy"));

                    string desc = parcela.FormaPagamento?.Descricao ?? await formaPagamentoController.ObterDescricaoFormaPagamento(parcela.FormaPagamentoId);
                    item.SubItems.Add(desc);
                    listViewCondPgto.Items.Add(item);
                }
            }
            CalculaTotalCompra();
        }
        private void CalcularTotalItem()
        {
            if (decimal.TryParse(txtQuantidade.Text, out decimal quantidade) && decimal.TryParse(txtValorUnitario.Text, out decimal valorUnitario))
            {
                txtTotal.Text = (quantidade * valorUnitario).ToString("F2");
            }
            else
            {
                txtTotal.Text = "0,00";
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
            if (!decimal.TryParse(txtValorTotal.Text, out decimal valorTotalCompra) || valorTotalCompra <= 0 ||
                condicaoPagamentoSelecionada == null || condicaoPagamentoSelecionada.Parcelas == null || condicaoPagamentoSelecionada.Parcelas.Count == 0)
            {
                foreach (ListViewItem item in listViewCondPgto.Items)
                {
                    while (item.SubItems.Count <= 3) item.SubItems.Add("");
                    item.SubItems[3].Text = "0,00";
                }
                lblTotalCondiçãoPgto.Text = "Total (R$): 0,00";
                return;
            }

            List<decimal> valoresCalculados = new List<decimal>();
            decimal valorAcumulado = 0;
            var parcelasOrdenadas = condicaoPagamentoSelecionada.Parcelas.OrderBy(p => p.NumParcela);

            foreach (var parcelaDefinicao in parcelasOrdenadas)
            {
                decimal porcentagem = parcelaDefinicao.Porcentagem;
                decimal valorParcela = valorTotalCompra * (porcentagem / 100);

                valorParcela = Math.Round(valorParcela, 2);
                valorAcumulado += valorParcela;

                if (parcelaDefinicao.NumParcela == parcelasOrdenadas.Last().NumParcela)
                {
                    valorParcela += (valorTotalCompra - valorAcumulado);
                }
                valoresCalculados.Add(valorParcela);
                totalParcelas += valorParcela;
            }

            int index = 0;
            foreach (ListViewItem item in listViewCondPgto.Items)
            {
                if (index < valoresCalculados.Count)
                {
                    while (item.SubItems.Count <= 3) item.SubItems.Add("");
                    item.SubItems[3].Text = valoresCalculados[index].ToString("F2");
                    index++;
                }
            }

            lblTotalCondiçãoPgto.Text = $"Total (R$): {totalParcelas:F2}";
        }
        #endregion

        #region Preparação de Dados para Salvar

        private bool ValidarCabecalho()
        {
            return !string.IsNullOrWhiteSpace(txtCodigo.Text) &&
                   !string.IsNullOrWhiteSpace(txtSerie.Text) &&
                   !string.IsNullOrWhiteSpace(txtNumero.Text) &&
                   !string.IsNullOrWhiteSpace(txtIDFornecedor.Text);
        }

        private bool ValidarDadosGerais()
        {
            if (!int.TryParse(txtNumero.Text, out _) || !int.TryParse(txtIDFornecedor.Text, out _))
            {
                MessageBox.Show("Os dados do cabeçalho da nota são obrigatórios e devem ser válidos.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (listViewProdutos.Items.Count == 0)
            {
                MessageBox.Show("É necessário adicionar pelo menos um produto à compra.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (condicaoPagamentoSelecionada == null)
            {
                MessageBox.Show("É necessário selecionar uma condição de pagamento.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                CondicaoPagamentoId = condicaoPagamentoSelecionada?.Id
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

        private void GerarParcelas(Compra compra)
        {
            compra.Parcelas.Clear();

            if (condicaoPagamentoSelecionada == null || condicaoPagamentoSelecionada.Parcelas == null || condicaoPagamentoSelecionada.Parcelas.Count == 0)
            {
                return;
            }

            DateTime dataBase = dtpEmissao.Value.Date; 
            decimal valorTotalCompra = compra.ValorTotal;
            decimal valorAcumulado = 0; 

            // Ordena as definições de parcela pelo número da parcela
            var parcelasOrdenadas = condicaoPagamentoSelecionada.Parcelas.OrderBy(p => p.NumParcela);

            foreach (var parcelaDefinicao in parcelasOrdenadas)
            {
                decimal valorParcela = valorTotalCompra * (parcelaDefinicao.Porcentagem / 100);

                valorParcela = Math.Round(valorParcela, 2);
                valorAcumulado += valorParcela;

                // Ajuste para garantir que a soma feche EXATAMENTE no valor total
                // Verifica se é a última parcela da definição
                if (parcelaDefinicao.NumParcela == parcelasOrdenadas.Last().NumParcela)
                {
                    valorParcela += (valorTotalCompra - valorAcumulado);
                }


                DateTime dataVencimento = dataBase.AddDays(parcelaDefinicao.PrazoDias);

                ContasAPagar novaConta = new ContasAPagar
                {
                    CompraModelo = compra.Modelo,
                    CompraSerie = compra.Serie,
                    CompraNumeroNota = compra.NumeroNota,
                    CompraFornecedorId = compra.FornecedorId,
                    NumeroParcela = parcelaDefinicao.NumParcela,
                    ValorVencimento = valorParcela, 
                    DataVencimento = dataVencimento,
                    Descricao = $"Parcela {parcelaDefinicao.NumParcela}/{condicaoPagamentoSelecionada.Parcelas.Count} NFe {compra.NumeroNota}",                                                                                                                   
                    Ativo = true,
                    Status = "Aberta",

                    Juros = condicaoPagamentoSelecionada.Juros,
                    Multa = condicaoPagamentoSelecionada.Multa,
                    Desconto = condicaoPagamentoSelecionada.Desconto
                };
                compra.Parcelas.Add(novaConta); 


            }

            decimal totalParcelasGeradas = compra.Parcelas.Sum(p => p.ValorVencimento);
            if (Math.Abs(totalParcelasGeradas - valorTotalCompra) > 0.01m)
            {
                MessageBox.Show($"Erro de arredondamento: O total das parcelas ({totalParcelasGeradas:C2}) não bate com o total da compra ({valorTotalCompra:C2}).", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                compra.Parcelas.Clear(); 
            }
        }
        #endregion
    }
}