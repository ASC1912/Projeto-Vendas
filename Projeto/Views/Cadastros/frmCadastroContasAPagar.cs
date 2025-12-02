using MySql.Data.MySqlClient;
using Projeto.Controller;
using Projeto.Models;
using Projeto.Utils;
using Projeto.Views.Consultas;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto.Views.Cadastros
{
    public partial class frmCadastroContasAPagar : Projeto.frmBase
    {
        #region 1. Variáveis e Construtor

        private ContasAPagar aConta;
        private ContasAPagarController controller;
        private CondicaoPagamento condicaoDaConta;
        private string modoOperacao;

        private CompraController compraController = new CompraController();
        private FornecedorController fornecedorController = new FornecedorController();
        private FormaPagamentoController formaPagamentoController = new FormaPagamentoController();
        private frmConsultaFornecedor oFrmConsultaFornecedor;

        private CondicaoPagamentoController condicaoPagamentoController = new CondicaoPagamentoController();
        private CondicaoPagamento condicaoDoFornecedor;

        public frmCadastroContasAPagar()
        {
            InitializeComponent();

            txtFornecedor.ReadOnly = true;
            txtFormaPgto.ReadOnly = true;
            txtValorPago.ReadOnly = true;

            dtpDataPagamento.ShowCheckBox = true;
            dtpDataPagamento.Checked = false;
            dtpDataPagamento.Format = DateTimePickerFormat.Short;
            dtpEmissao.Format = DateTimePickerFormat.Short;
            dtpVencimento.Format = DateTimePickerFormat.Short;
            dtpEmissao.MaxDate = DateTime.Now;

            dtpEmissao.ValueChanged += (s, e) => { dtpVencimento.MinDate = dtpEmissao.Value.Date; };
            dtpVencimento.MinDate = dtpEmissao.Value.Date;
            this.dtpDataPagamento.ValueChanged += new System.EventHandler(this.dtpDataPagamento_ValueChanged);

            txtJuros.Leave += (s, e) => AtualizarTotalComValoresManuais();
            txtMulta.Leave += (s, e) => AtualizarTotalComValoresManuais();
            txtDesconto.Leave += (s, e) => AtualizarTotalComValoresManuais();

            this.txtIDFornecedor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress_ApenasNumerosInteiros);
            this.txtIdFormaPgto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress_ApenasNumerosInteiros);

            if (this.Controls.ContainsKey("txtCodigo")) (this.Controls["txtCodigo"] as TextBox).MaxLength = 50;
            txtSerie.MaxLength = 50;
            txtNumero.MaxLength = 10;

            LimparTxt();
        }

        #endregion

        #region 2. Métodos da Interface (Herança e Configuração)

        public override void ConhecaObj(object obj, object ctrl)
        {
            aConta = (obj as ContasAPagar) ?? new ContasAPagar();
            controller = ctrl as ContasAPagarController;
        }

        public void setFrmConsultaFornecedor(object obj)
        {
            if (obj != null)
            {
                oFrmConsultaFornecedor = (frmConsultaFornecedor)obj;
            }
        }

        public override void LimparTxt()
        {
            base.LimparTxt();

            txtIDFornecedor.Clear();
            txtFornecedor.Clear();

            dtpEmissao.MinDate = DateTimePicker.MinimumDateTime;
            dtpEmissao.MaxDate = DateTimePicker.MaximumDateTime;

            dtpEmissao.Value = DateTime.Today;

            dtpEmissao.MaxDate = DateTime.Now;

            dtpVencimento.MinDate = dtpEmissao.Value.Date; 
            dtpVencimento.MaxDate = DateTimePicker.MaximumDateTime;
            dtpVencimento.Value = DateTime.Today;

            txtValorAPagar.Text = "0,00";

            dtpDataPagamento.MinDate = DateTimePicker.MinimumDateTime; 
            dtpDataPagamento.MaxDate = DateTimePicker.MaximumDateTime;
            dtpDataPagamento.Value = DateTime.Today;
            dtpDataPagamento.Checked = false;

            txtValorPago.Text = "0,00";
            txtJuros.Text = "0,00";
            txtMulta.Text = "0,00";
            txtDesconto.Text = "0,00";

            if (this.Controls.ContainsKey("txtJurosPorcentagem")) txtJurosPorcentagem.Text = "0,00%";
            if (this.Controls.ContainsKey("txtMultaPorcentagem")) txtMultaPorcentagem.Text = "0,00%";
            if (this.Controls.ContainsKey("txtDescontoPorcentagem")) txtDescontoPorcentagem.Text = "0,00%";

            txtIdFormaPgto.Clear();
            txtFormaPgto.Clear();

            if (this.Controls.ContainsKey("txtCodigo")) this.Controls["txtCodigo"].Text = "55";
            txtSerie.Text = "1";
            txtNumero.Clear();

            aConta = new ContasAPagar();
            DefinirModo("Lancamento");
        }

        public async override void CarregaTxt()
        {
            if (aConta == null) return;

            bool formaPgtoPreenchida = false;
            condicaoDaConta = null;

            if (!string.IsNullOrEmpty(aConta.CompraModelo))
            {
                try
                {
                    var compra = compraController.BuscarPorChave(aConta.CompraModelo, aConta.CompraSerie, aConta.CompraNumeroNota, aConta.CompraFornecedorId);
                    if (compra != null && compra.CondicaoPagamentoId.HasValue)
                    {
                        condicaoDaConta = await new CondicaoPagamentoController().BuscarPorId(compra.CondicaoPagamentoId.Value);
                    }
                }
                catch { condicaoDaConta = null; }
            }

            if (condicaoDaConta != null && condicaoDaConta.Parcelas != null)
            {
                var definicaoDaParcela = condicaoDaConta.Parcelas.FirstOrDefault(p => p.NumParcela == aConta.NumeroParcela);

                if (definicaoDaParcela != null)
                {
                    var formaPgto = await formaPagamentoController.BuscarPorId(definicaoDaParcela.FormaPagamentoId);
                    if (formaPgto != null)
                    {
                        txtIdFormaPgto.Text = formaPgto.Id.ToString();
                        txtFormaPgto.Text = formaPgto.Descricao;
                        formaPgtoPreenchida = true;
                    }
                }
            }

            txtIDFornecedor.Text = aConta.FornecedorId.ToString();
            txtFornecedor.Text = aConta.NomeFornecedor;

            try
            {
                dtpEmissao.MinDate = DateTimePicker.MinimumDateTime;
                dtpEmissao.MaxDate = DateTimePicker.MaximumDateTime;
                dtpEmissao.Value = aConta.DataEmissao;

                dtpVencimento.MinDate = dtpEmissao.Value.Date;
                dtpVencimento.MaxDate = DateTimePicker.MaximumDateTime;
                if (aConta.DataVencimento < dtpVencimento.MinDate) dtpVencimento.MinDate = aConta.DataVencimento.Date;
                dtpVencimento.Value = aConta.DataVencimento;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Atenção: Houve um problema ao carregar as datas desta conta.\nErro: {ex.Message}", "Data Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            txtValorAPagar.Text = aConta.ValorVencimento.ToString("F2");

            if (this.Controls.ContainsKey("txtCodigo")) this.Controls["txtCodigo"].Text = aConta.CompraModelo ?? "";
            txtSerie.Text = aConta.CompraSerie ?? "";
            txtNumero.Text = aConta.CompraNumeroNota.ToString();

            dtpDataPagamento.Checked = aConta.DataPagamento.HasValue;
            dtpDataPagamento.Value = aConta.DataPagamento ?? DateTime.Now;

            if (aConta.Status == "Paga")
            {
                txtJuros.Text = (aConta.Juros ?? 0).ToString("F2");
                txtMulta.Text = (aConta.Multa ?? 0).ToString("F2");
                txtDesconto.Text = (aConta.Desconto ?? 0).ToString("F2");

                txtJurosPorcentagem.Text = "0,00%";
                txtMultaPorcentagem.Text = "0,00%";
                txtDescontoPorcentagem.Text = "0,00%";
            }
            else
            {
                txtJurosPorcentagem.Text = (aConta.Juros ?? 0).ToString("N2") + "%";
                txtMultaPorcentagem.Text = (aConta.Multa ?? 0).ToString("N2") + "%";
                txtDescontoPorcentagem.Text = (aConta.Desconto ?? 0).ToString("N2") + "%";

                txtJuros.Text = "0,00";
                txtMulta.Text = "0,00";
                txtDesconto.Text = "0,00";
            }

            if (!formaPgtoPreenchida)
            {
                txtIdFormaPgto.Text = aConta.IdFormaPagamento?.ToString() ?? "";
                txtFormaPgto.Text = aConta.NomeFormaPagamento;
            }

            if (aConta.Status == "Paga" && aConta.ValorPago.HasValue)
            {
                txtValorPago.Text = aConta.ValorPago.Value.ToString("F2");
            }
            else
            {
                RecalcularValorPago();
            }
        }

        #endregion

        #region 3. Gerenciamento de Estado (Modos de Operação)

        public void DefinirModo(string modo)
        {
            this.modoOperacao = modo;
            bool isPagamento = (modo == "Pagamento");
            bool isLancamento = (modo == "Lancamento");
            bool isCancelamento = (modo == "Cancelamento");
            bool isVisualizacao = (modo == "Visualizacao");


            this.Text = "Conta a Pagar";
            btnSalvar.Text = "Salvar";
            btnSalvar.Visible = true;
            btnSalvar.Enabled = true;

            txtIDFornecedor.Enabled = false;
            btnPesquisarFornecedor.Enabled = false;
            dtpEmissao.Enabled = false;
            dtpVencimento.Enabled = false;
            txtValorAPagar.Enabled = false;
            if (this.Controls.ContainsKey("txtCodigo")) this.Controls["txtCodigo"].Enabled = false;
            txtSerie.Enabled = false;
            txtNumero.Enabled = false;
            if (this.Controls.ContainsKey("txtCodigo")) (this.Controls["txtCodigo"] as TextBox).ReadOnly = true;
            txtSerie.ReadOnly = true;
            txtNumero.ReadOnly = true;
            dtpDataPagamento.Enabled = false;
            txtValorPago.ReadOnly = true;
            txtJuros.Enabled = false;
            txtMulta.Enabled = false;
            txtDesconto.Enabled = false;
            txtIdFormaPgto.Enabled = false;
            btnPesquisarFormaPgto.Enabled = false;
            chkInativo.Enabled = false;

            if (isLancamento)
            {
                txtIDFornecedor.Enabled = true;
                btnPesquisarFornecedor.Enabled = true;
                dtpEmissao.Enabled = true;
                dtpVencimento.Enabled = true;
                txtValorAPagar.Enabled = true;
                if (this.Controls.ContainsKey("txtCodigo")) this.Controls["txtCodigo"].Enabled = true;
                txtSerie.Enabled = true;
                txtNumero.Enabled = true;
                if (this.Controls.ContainsKey("txtCodigo")) (this.Controls["txtCodigo"] as TextBox).ReadOnly = false;
                txtSerie.ReadOnly = false;
                txtNumero.ReadOnly = false;
                chkInativo.Enabled = true;

                txtJuros.ReadOnly = false;
                txtMulta.ReadOnly = false;
                txtDesconto.ReadOnly = false;

                this.Text = "Lançamento Manual de Conta a Pagar";
                btnSalvar.Text = "Salvar";

                if (this.Controls.ContainsKey("txtCodigo")) this.Controls["txtCodigo"].Text = "55";
                txtSerie.Text = "1";
                dtpDataPagamento.Checked = false;
            }
            else if (isPagamento)
            {
                dtpDataPagamento.Enabled = true;
                txtValorPago.Enabled = true;
                txtJuros.Enabled = true;
                txtMulta.Enabled = true;
                txtDesconto.Enabled = true;
                txtIdFormaPgto.Enabled = true;
                btnPesquisarFormaPgto.Enabled = true;

                txtJuros.ReadOnly = false;
                txtMulta.ReadOnly = false;
                txtDesconto.ReadOnly = false;
                txtIdFormaPgto.ReadOnly = false;
                txtValorPago.ReadOnly = false;

                this.Text = "Pagamento de Conta a Pagar";
                btnSalvar.Text = "Pagar";

                if (aConta != null && aConta.Status == "Aberta")
                {
                    DateTime dataSugerida = DateTime.Now.Date;
                    if (dataSugerida < aConta.DataEmissao.Date) dataSugerida = aConta.DataEmissao.Date;

                    if (dtpDataPagamento.MaxDate < dataSugerida) dataSugerida = dtpDataPagamento.MaxDate;
                    if (dtpDataPagamento.MinDate > dataSugerida) dataSugerida = dtpDataPagamento.MinDate;

                    dtpDataPagamento.Value = dataSugerida;
                    dtpDataPagamento.Checked = true;
                    RecalcularValorPago();
                }
            }
            else if (isCancelamento)
            {
                this.Text = "Cancelar Conta Manual";
                btnSalvar.Text = "Cancelar Conta";

                if (Controls.ContainsKey("lblMotivoCancelamento"))
                {
                    Controls["lblMotivoCancelamento"].Text = "Motivo do Cancelamento:";
                    Controls["lblMotivoCancelamento"].Visible = true;
                }
            }
            else if (isVisualizacao)
            {
                this.Text = "Detalhes da Conta";

                btnSalvar.Visible = false;


                dtpDataPagamento.Enabled = false;
                txtValorPago.ReadOnly = true;
                txtJuros.ReadOnly = true;
                txtMulta.ReadOnly = true;
                txtDesconto.ReadOnly = true;
                txtIdFormaPgto.ReadOnly = true;

                btnPesquisarFormaPgto.Enabled = false;
                btnPesquisarFornecedor.Enabled = false;

                dtpDataPagamento.Checked = (aConta.Status == "Paga");
            }

            if (aConta != null && aConta.Status == "Cancelada")
            {
                if (Controls.ContainsKey("lblMotivoCancelamento"))
                {
                    Controls["lblMotivoCancelamento"].Text = "Motivo: " + aConta.MotivoCancelamento;
                    Controls["lblMotivoCancelamento"].Visible = true;
                }
                btnSalvar.Enabled = false;
            }
            else if (!isCancelamento)
            {
                if (Controls.ContainsKey("lblMotivoCancelamento"))
                {
                    Controls["lblMotivoCancelamento"].Visible = false;
                }
            }
        }

        #endregion

        #region 4. Eventos de Controles (Botões e Campos)

        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            if (controller == null)
            {
                MessageBox.Show("Erro interno: Controller não inicializado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (modoOperacao == "Pagamento")
                {
                    #region Lógica de Salvar Pagamento
                    if (!dtpDataPagamento.Checked)
                    {
                        MessageBox.Show("Selecione a Data do Pagamento.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dtpDataPagamento.Focus();
                        return;
                    }
                    if (!Validador.CampoObrigatorio(txtIdFormaPgto, "Forma de Pagamento"))
                    {
                        return;
                    }
                    if (!decimal.TryParse(txtValorPago.Text, out decimal valorPagoCalculado) || valorPagoCalculado < 0)
                    {
                        MessageBox.Show("O Valor Pago calculado é inválido. Verifique os valores de juros, multa e desconto.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtJuros.Focus();
                        return;
                    }

                    string msgConfirm = $"Confirma o pagamento de {valorPagoCalculado:C2} para a conta escolhida?";
                    if (MessageBox.Show(msgConfirm, "Confirmar Pagamento", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }

                    aConta.DataPagamento = dtpDataPagamento.Value.Date;
                    aConta.ValorPago = valorPagoCalculado;

                    aConta.Juros = decimal.TryParse(txtJuros.Text, out decimal j) && j != 0 ? j : (decimal?)null;
                    aConta.Multa = decimal.TryParse(txtMulta.Text, out decimal m) && m != 0 ? m : (decimal?)null;
                    aConta.Desconto = decimal.TryParse(txtDesconto.Text, out decimal d) && d != 0 ? d : (decimal?)null;
                    aConta.IdFormaPagamento = int.Parse(txtIdFormaPgto.Text);
                    aConta.Status = "Paga";

                    await controller.Pagar(aConta);
                    MessageBox.Show("Conta paga com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    #endregion
                }
                else if (modoOperacao == "Lancamento")
                {
                    #region Lógica de Salvar Lançamento
                    if (!Validador.CampoObrigatorio(txtIDFornecedor, "Fornecedor"))
                    {
                        return;
                    }
                    if (!Validador.CampoObrigatorio(txtValorAPagar, "Valor da Conta") || Convert.ToDecimal(txtValorAPagar.Text) <= 0) return;

                    TextBox txtCodigo = this.Controls.ContainsKey("txtCodigo") ? this.Controls["txtCodigo"] as TextBox : null;
                    if (!Validador.CampoObrigatorio(txtCodigo, "Modelo é obrigatório") ||
                        !Validador.CampoObrigatorio(txtSerie, "Série é obrigatório") ||
                        !Validador.CampoObrigatorio(txtNumero, "Número Documento/Nota é obrigatório"))
                    {
                        return;
                    }
                    if (!int.TryParse(txtNumero.Text, out int numeroNotaValidado))
                    {
                        MessageBox.Show("O campo 'Número Documento/Nota' deve conter apenas números inteiros.", "Formato Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtNumero.Focus();
                        return;
                    }

                    ContasAPagar novaContaManual = new ContasAPagar
                    {
                        CompraModelo = txtCodigo?.Text ?? "ERR",
                        CompraSerie = txtSerie.Text,
                        CompraNumeroNota = numeroNotaValidado,
                        CompraFornecedorId = int.Parse(txtIDFornecedor.Text),
                        NumeroParcela = 1,

                        FornecedorId = int.Parse(txtIDFornecedor.Text),
                        DataEmissao = dtpEmissao.Value.Date,
                        DataVencimento = dtpVencimento.Value.Date,
                        ValorVencimento = Convert.ToDecimal(txtValorAPagar.Text),
                        Status = "Aberta",
                        Ativo = true,

                        Juros = condicaoDoFornecedor?.Juros,
                        Multa = condicaoDoFornecedor?.Multa,
                        Desconto = condicaoDoFornecedor?.Desconto,
                        IdFormaPagamento = int.TryParse(txtIdFormaPgto.Text, out int formaPgtoId) ? (int?)formaPgtoId : null
                    };

                    await controller.SalvarManual(novaContaManual);
                    MessageBox.Show("Conta lançada manualmente com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    #endregion
                }
                else if (modoOperacao == "Cancelamento")
                {
                    #region Lógica de Cancelamento
                    string motivo = "";

                    using (Form prompt = new Form())
                    {
                        prompt.Width = 500; prompt.Height = 250;
                        prompt.Text = "Motivo do Cancelamento";
                        prompt.StartPosition = FormStartPosition.CenterParent;
                        prompt.FormBorderStyle = FormBorderStyle.FixedDialog;
                        prompt.MinimizeBox = false; prompt.MaximizeBox = false;

                        Label textLabel = new Label() { Left = 50, Top = 20, Width = 400, Text = "Para cancelar esta conta, digite o motivo:" };
                        TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400, Height = 80, Multiline = true, ScrollBars = ScrollBars.Vertical, MaxLength = 255 };
                        Button confirmation = new Button() { Text = "Confirmar", Left = 240, Width = 100, Top = 150, DialogResult = DialogResult.OK };
                        Button cancel = new Button() { Text = "Voltar", Left = 350, Width = 100, Top = 150, DialogResult = DialogResult.Cancel };

                        confirmation.Enabled = false;
                        textBox.TextChanged += (s, ev) => { confirmation.Enabled = !string.IsNullOrWhiteSpace(textBox.Text); };

                        prompt.Controls.AddRange(new Control[] { textLabel, textBox, confirmation, cancel });
                        prompt.AcceptButton = confirmation;
                        prompt.CancelButton = cancel;

                        if (prompt.ShowDialog() == DialogResult.OK)
                        {
                            motivo = textBox.Text;

                            await controller.CancelarManual(aConta, motivo);
                            MessageBox.Show("Conta cancelada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                    }
                    #endregion
                }
            }
            catch (InvalidOperationException opEx)
            {
                MessageBox.Show(opEx.Message, "Operação Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (MySqlException sqlEx) when (sqlEx.Number == 1062 && modoOperacao == "Lancamento")
            {
                MessageBox.Show(
                    "Erro: Já existe uma conta registrada com esta combinação de Modelo, Série, Número da Nota, Fornecedor e Parcela.\nVerifique os dados informados.",
                    "Chave Duplicada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                txtNumero.Focus();
                txtNumero.SelectAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro inesperado: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnPesquisarFornecedor_Click(object sender, EventArgs e)
        {
            if (oFrmConsultaFornecedor == null)
            {
                MessageBox.Show("Erro: Formulário de consulta de fornecedor não foi inicializado corretamente.", "Erro de Configuração", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            oFrmConsultaFornecedor.ModoSelecao = true;
            if (oFrmConsultaFornecedor.ShowDialog() == DialogResult.OK)
            {
                var fornecedorSelecionado = oFrmConsultaFornecedor.FornecedorSelecionado;

                if (fornecedorSelecionado != null && fornecedorSelecionado.Id > 0)
                {
                    await CarregarDadosFornecedor(fornecedorSelecionado.Id);
                }
                else
                {
                    txtIDFornecedor.Clear();
                    txtFornecedor.Clear();
                }
            }
            oFrmConsultaFornecedor.ModoSelecao = false;
        }

        private async void btnPesquisarFormaPgto_Click(object sender, EventArgs e)
        {
            using (var frmConsulta = new frmConsultaFrmPgto())
            {
                frmConsulta.ModoSelecao = true;
                if (frmConsulta.ShowDialog() == DialogResult.OK)
                {
                    var formaSelecionada = frmConsulta.FormaSelecionada;

                    if (formaSelecionada != null && formaSelecionada.Id > 0)
                    {
                        txtIdFormaPgto.Text = formaSelecionada.Id.ToString();
                        txtFormaPgto.Text = formaSelecionada.Descricao;
                    }
                    else
                    {
                        txtIdFormaPgto.Clear();
                        txtFormaPgto.Clear();
                    }
                }
            }
        }

        private async void txtIDFornecedor_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDFornecedor.Text))
            {
                if (modoOperacao == "Lancamento")
                {
                    txtFornecedor.Clear();
                    condicaoDoFornecedor = null;
                    txtJuros.Text = "0,00";
                    txtMulta.Text = "0,00";
                    txtDesconto.Text = "0,00";

                    txtJurosPorcentagem.Text = "0,00%";
                    txtMultaPorcentagem.Text = "0,00%";
                    txtDescontoPorcentagem.Text = "0,00%";

                    txtIdFormaPgto.Clear();
                    txtFormaPgto.Clear();
                }
                return;
            }

            if (modoOperacao == "Pagamento") return;

            if (int.TryParse(txtIDFornecedor.Text, out int id))
            {
                await CarregarDadosFornecedor(id);
            }
            else
            {
                MessageBox.Show("ID do Fornecedor inválido. Digite apenas números.", "Erro de Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFornecedor.Clear();
            }
        }

        private async void txtIdFormaPgto_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdFormaPgto.Text) || modoOperacao == "Lancamento")
            {
                if (modoOperacao == "Pagamento") txtFormaPgto.Clear();
                return;
            }

            if (int.TryParse(txtIdFormaPgto.Text, out int id))
            {
                try
                {
                    FormaPagamento forma = await formaPagamentoController.BuscarPorId(id);
                    if (forma != null)
                    {
                        txtFormaPgto.Text = forma.Descricao;
                    }
                    else
                    {
                        MessageBox.Show("Forma de Pagamento não encontrada com este ID.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtFormaPgto.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao buscar forma de pagamento: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtFormaPgto.Clear();
                }
            }
            else
            {
                MessageBox.Show("ID da Forma de Pagamento inválido. Digite apenas números.", "Erro de Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFormaPgto.Clear();
            }
        }

        private void dtpDataPagamento_ValueChanged(object sender, EventArgs e)
        {
            RecalcularValorPago();
        }

        #endregion

        #region 5. Lógica de Negócio e Cálculos

        private void AtualizarTotalComValoresManuais()
        {
            decimal.TryParse(txtValorAPagar.Text, out decimal valorVencimento);
            decimal.TryParse(txtJuros.Text, out decimal jurosReais);
            decimal.TryParse(txtMulta.Text, out decimal multaReais);
            decimal.TryParse(txtDesconto.Text, out decimal descontoReais);

            decimal valorFinal = valorVencimento + jurosReais + multaReais - descontoReais;

            if (valorFinal < 0) valorFinal = 0;

            txtValorPago.Text = valorFinal.ToString("F2");
        }

        private void RecalcularValorPago()
        {
            if (modoOperacao != "Pagamento" || !dtpDataPagamento.Checked)
            {
                txtValorPago.Text = "0,00";

                if (aConta != null)
                {
                    if (aConta.Status == "Paga")
                    {
                        txtJuros.Text = (aConta.Juros ?? 0).ToString("F2");
                        txtMulta.Text = (aConta.Multa ?? 0).ToString("F2");
                        txtDesconto.Text = (aConta.Desconto ?? 0).ToString("F2");
                    }
                    else
                    {
                        txtJuros.Text = "0,00";
                        txtMulta.Text = "0,00";
                        txtDesconto.Text = "0,00";
                    }
                }
                return;
            }


            DateTime dataPagamento = dtpDataPagamento.Value.Date;
            DateTime dataVencimento = dtpVencimento.Value.Date;

            decimal.TryParse(txtValorAPagar.Text, out decimal valorVencimento);

            decimal taxaJuros = aConta.Juros ?? 0;
            decimal taxaMulta = aConta.Multa ?? 0;
            decimal taxaDesconto = aConta.Desconto ?? 0;

            decimal jurosAplicado = 0;
            decimal multaAplicada = 0;
            decimal descontoAplicado = 0;

            if (dataPagamento > dataVencimento)
            {
                if (taxaJuros > 0)
                {
                    int diasAtraso = (dataPagamento - dataVencimento).Days;
                    decimal jurosPorDia = (valorVencimento * (taxaJuros / 100)) / 30;
                    jurosAplicado = jurosPorDia * diasAtraso;
                }
                if (taxaMulta > 0)
                {
                    multaAplicada = valorVencimento * (taxaMulta / 100);
                }
            }
            else if (dataPagamento <= dataVencimento)
            {
                if (taxaDesconto > 0)
                {
                    descontoAplicado = valorVencimento * (taxaDesconto / 100);
                }
            }

            decimal valorPago = valorVencimento + jurosAplicado + multaAplicada - descontoAplicado;
            if (valorPago < 0) valorPago = 0;

            txtValorPago.Text = valorPago.ToString("F2");
            txtJuros.Text = jurosAplicado.ToString("F2");
            txtMulta.Text = multaAplicada.ToString("F2");
            txtDesconto.Text = descontoAplicado.ToString("F2");
        }

        #endregion

        #region 6. Métodos Auxiliares e Consultas

        private async Task CarregarDadosFornecedor(int id)
        {
            if (modoOperacao == "Lancamento")
            {
                condicaoDoFornecedor = null;
                txtJuros.Text = "0,00";
                txtMulta.Text = "0,00";
                txtDesconto.Text = "0,00";
                txtIdFormaPgto.Clear();
                txtFormaPgto.Clear();
            }

            try
            {
                Fornecedor f = await fornecedorController.BuscarPorId(id);
                if (f != null)
                {
                    txtFornecedor.Text = f.Nome;
                    txtIDFornecedor.Text = f.Id.ToString();

                    if (f.IdCondicao.HasValue && f.IdCondicao > 0)
                    {
                        condicaoDoFornecedor = await condicaoPagamentoController.BuscarPorId(f.IdCondicao.Value);

                        if (condicaoDoFornecedor != null)
                        {
                            txtJurosPorcentagem.Text = condicaoDoFornecedor.Juros.ToString("F2") + "%";
                            txtMultaPorcentagem.Text = condicaoDoFornecedor.Multa.ToString("F2") + "%";
                            txtDescontoPorcentagem.Text = condicaoDoFornecedor.Desconto.ToString("F2") + "%";

                            txtJuros.Text = "0,00";
                            txtMulta.Text = "0,00";
                            txtDesconto.Text = "0,00";

                            var primeiraParcela = condicaoDoFornecedor.Parcelas?
                                .OrderBy(p => p.NumParcela)
                                .FirstOrDefault();

                            if (primeiraParcela != null && primeiraParcela.FormaPagamentoId > 0)
                            {
                                FormaPagamento forma = await formaPagamentoController.BuscarPorId(primeiraParcela.FormaPagamentoId);
                                if (forma != null)
                                {
                                    txtIdFormaPgto.Text = forma.Id.ToString();
                                    txtFormaPgto.Text = forma.Descricao;
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Fornecedor não encontrado com este ID.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtFornecedor.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao buscar dados do fornecedor: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFornecedor.Clear();
            }
        }

        #endregion
    }
}