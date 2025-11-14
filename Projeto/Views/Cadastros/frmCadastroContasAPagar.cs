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
        private ContasAPagar aConta;
        private ContasAPagarController controller;
        private CondicaoPagamento condicaoDaConta; 
        private string modoOperacao;

        private CompraController compraController = new CompraController();
        private FornecedorController fornecedorController = new FornecedorController();
        private FormaPagamentoController formaPagamentoController = new FormaPagamentoController();
        private frmConsultaFornecedor oFrmConsultaFornecedor;


        public frmCadastroContasAPagar()
        {
            InitializeComponent();

            txtFornecedor.ReadOnly = true;
            txtFormaPgto.ReadOnly = true;
            txtValorPago.ReadOnly = true; // Valor calculado

            dtpDataPagamento.ShowCheckBox = true;
            dtpDataPagamento.Checked = false;
            dtpDataPagamento.Format = DateTimePickerFormat.Short;
            dtpEmissao.Format = DateTimePickerFormat.Short;
            dtpVencimento.Format = DateTimePickerFormat.Short;

            dtpEmissao.ValueChanged += (s, e) => { dtpVencimento.MinDate = dtpEmissao.Value.Date; };
            dtpVencimento.MinDate = dtpEmissao.Value.Date;

            this.txtJuros.TextChanged += new System.EventHandler(this.RecalcularValorPago);
            this.txtMulta.TextChanged += new System.EventHandler(this.RecalcularValorPago);
            this.txtDesconto.TextChanged += new System.EventHandler(this.RecalcularValorPago);
            this.dtpDataPagamento.ValueChanged += new System.EventHandler(this.RecalcularValorPago);
            this.txtIDFornecedor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress_ApenasNumerosInteiros);
            this.txtIdFormaPgto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress_ApenasNumerosInteiros);

            if (this.Controls.ContainsKey("txtCodigo")) (this.Controls["txtCodigo"] as TextBox).MaxLength = 50;
            txtSerie.MaxLength = 50;
            txtNumero.MaxLength = 10;

            LimparTxt();
        }

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

        public void DefinirModo(string modo)
        {
            this.modoOperacao = modo;
            bool isPagamento = (modo == "Pagamento");
            bool isLancamento = (modo == "Lancamento");
            bool isCancelamento = (modo == "Cancelamento");

            this.Text = "Conta a Pagar";
            btnSalvar.Text = "Salvar";
            btnSalvar.Enabled = true; 

            txtIDFornecedor.Enabled = false;
            btnPesquisarFornecedor.Enabled = false;
            txtDescricao.Enabled = false;
            dtpEmissao.Enabled = false;
            dtpVencimento.Enabled = false;
            txtValorVencimento.Enabled = false;
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
                txtDescricao.Enabled = true;
                dtpEmissao.Enabled = true;
                dtpVencimento.Enabled = true;
                txtValorVencimento.Enabled = true;
                if (this.Controls.ContainsKey("txtCodigo")) this.Controls["txtCodigo"].Enabled = true;
                txtSerie.Enabled = true;
                txtNumero.Enabled = true;
                if (this.Controls.ContainsKey("txtCodigo")) (this.Controls["txtCodigo"] as TextBox).ReadOnly = false;
                txtSerie.ReadOnly = false;
                txtNumero.ReadOnly = false;
                chkInativo.Enabled = true; 

                this.Text = "Lançamento Manual de Conta a Pagar";
                btnSalvar.Text = "Salvar";
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

                this.Text = "Pagamento de Conta a Pagar";
                btnSalvar.Text = "Pagar";
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
          
            if (isPagamento && aConta != null && aConta.Status == "Aberta")
            {
                DateTime dataSugerida = DateTime.Now.Date;
                if (dataSugerida < aConta.DataEmissao.Date) dataSugerida = aConta.DataEmissao.Date;
                if (dtpDataPagamento.MaxDate < dataSugerida) dataSugerida = dtpDataPagamento.MaxDate;
                if (dtpDataPagamento.MinDate > dataSugerida) dataSugerida = dtpDataPagamento.MinDate;
                dtpDataPagamento.Value = dataSugerida;
                dtpDataPagamento.Checked = true;
                RecalcularValorPago(null, null);
            }
            else if (isLancamento)
            {
                if (this.Controls.ContainsKey("txtCodigo")) this.Controls["txtCodigo"].Text = "55";
                txtSerie.Text = "1";
                dtpDataPagamento.Checked = false;
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

        public override void LimparTxt()
        {
            base.LimparTxt();

            txtIDFornecedor.Clear();
            txtFornecedor.Clear();
            txtDescricao.Clear();
            dtpEmissao.Value = DateTime.Now;
            dtpVencimento.Value = DateTime.Now;
            txtValorVencimento.Text = "0,00";

            dtpDataPagamento.Value = DateTime.Now;
            dtpDataPagamento.Checked = false;
            txtValorPago.Text = "0,00";
            txtJuros.Text = "0,00";
            txtMulta.Text = "0,00";
            txtDesconto.Text = "0,00";
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
                var definicaoDaParcela = condicaoDaConta.Parcelas
                    .FirstOrDefault(p => p.NumParcela == aConta.NumeroParcela);

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
            txtDescricao.Text = aConta.Descricao;

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

            txtValorVencimento.Text = aConta.ValorVencimento.ToString("F2");

            if (this.Controls.ContainsKey("txtCodigo")) this.Controls["txtCodigo"].Text = aConta.CompraModelo ?? "";
            txtSerie.Text = aConta.CompraSerie ?? "";
            txtNumero.Text = aConta.CompraNumeroNota.ToString();

            dtpDataPagamento.Checked = aConta.DataPagamento.HasValue;
            dtpDataPagamento.Value = aConta.DataPagamento ?? DateTime.Now;
            txtJuros.Text = aConta.Juros?.ToString("F2") ?? "0,00";
            txtMulta.Text = aConta.Multa?.ToString("F2") ?? "0,00";
            txtDesconto.Text = aConta.Desconto?.ToString("F2") ?? "0,00";

            if (!formaPgtoPreenchida)
            {
                txtIdFormaPgto.Text = aConta.IdFormaPagamento?.ToString() ?? "";
                txtFormaPgto.Text = aConta.NomeFormaPagamento;
            }

            RecalcularValorPago(null, null);
            if (aConta.Status == "Paga" && aConta.ValorPago.HasValue)
            {
                txtValorPago.Text = aConta.ValorPago.Value.ToString("F2");
            }
        }

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

                    string msgConfirm = $"Confirma o pagamento de {valorPagoCalculado:C2} para a conta '{aConta.Descricao}'?";
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
                }
                else if (modoOperacao == "Lancamento")
                {
                    if (!Validador.CampoObrigatorio(txtIDFornecedor, "Fornecedor") ||
                        !Validador.CampoObrigatorio(txtDescricao, "Descrição"))
                    {
                        return;
                    }
                    if (!Validador.CampoObrigatorio(txtValorVencimento, "Valor da Conta") || Convert.ToDecimal(txtValorVencimento.Text) <= 0) return;

                    TextBox txtCodigo = this.Controls.ContainsKey("txtCodigo") ? this.Controls["txtCodigo"] as TextBox : null;
                    if (!Validador.CampoObrigatorio(txtCodigo, "Modelo") ||
                        !Validador.CampoObrigatorio(txtSerie, "Série") ||
                        !Validador.CampoObrigatorio(txtNumero, "Número Documento/Nota"))
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
                        Descricao = txtDescricao.Text,
                        DataEmissao = dtpEmissao.Value.Date,
                        DataVencimento = dtpVencimento.Value.Date,
                        ValorVencimento = Convert.ToDecimal(txtValorVencimento.Text),
                        Status = "Aberta",
                        Ativo = true
                    };

                    await controller.SalvarManual(novaContaManual);
                    MessageBox.Show("Conta lançada manualmente com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else if (modoOperacao == "Cancelamento")
                {
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
                    txtIDFornecedor.Text = fornecedorSelecionado.Id.ToString();
                    txtFornecedor.Text = fornecedorSelecionado.Nome;
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
            if (string.IsNullOrWhiteSpace(txtIDFornecedor.Text) || modoOperacao == "Pagamento")
            {
                if (modoOperacao == "Lancamento") txtFornecedor.Clear();
                return;
            }

            if (int.TryParse(txtIDFornecedor.Text, out int id))
            {
                try
                {
                    Fornecedor f = await fornecedorController.BuscarPorId(id);
                    if (f != null)
                    {
                        txtFornecedor.Text = f.Nome;
                    }
                    else
                    {
                        MessageBox.Show("Fornecedor não encontrado com este ID.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtFornecedor.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao buscar fornecedor: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtFornecedor.Clear();
                }
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

        private void RecalcularValorPago(object sender, EventArgs e)
        {
            if (modoOperacao != "Pagamento")
            {
                if (txtValorPago.Text != "0,00") txtValorPago.Text = "0,00";
                return;
            }

            if (!dtpDataPagamento.Checked)
            {
                txtValorPago.Text = "0,00";
                return;
            }

            try
            {
                decimal valorBase = aConta?.ValorVencimento ?? 0;
                if (valorBase <= 0 && decimal.TryParse(txtValorVencimento.Text, out decimal valorTela))
                {
                    valorBase = valorTela;
                }
                if (valorBase <= 0)
                {
                    txtValorPago.Text = "0,00";
                    return;
                }

                decimal jurosCalc = 0;
                decimal multaCalc = 0;
                decimal descontoCalc = 0;
                DateTime dataVenc = aConta?.DataVencimento.Date ?? dtpVencimento.Value.Date;
                DateTime dataPag = dtpDataPagamento.Value.Date;

                if (condicaoDaConta != null)
                {
                    if (dataPag > dataVenc)
                    {
                        if (condicaoDaConta.Juros > 0)
                            jurosCalc = valorBase * (condicaoDaConta.Juros / 100);

                        if (condicaoDaConta.Multa > 0)
                            multaCalc = valorBase * (condicaoDaConta.Multa / 100);
                    }
                    else 
                    {
                        if (condicaoDaConta.Desconto > 0)
                            descontoCalc = valorBase * (condicaoDaConta.Desconto / 100);
                    }

                    if (string.IsNullOrWhiteSpace(txtJuros.Text) || txtJuros.Text == "0,00")
                        txtJuros.Text = jurosCalc.ToString("F2");

                    if (string.IsNullOrWhiteSpace(txtMulta.Text) || txtMulta.Text == "0,00")
                        txtMulta.Text = multaCalc.ToString("F2");

                    if (string.IsNullOrWhiteSpace(txtDesconto.Text) || txtDesconto.Text == "0,00")
                        txtDesconto.Text = descontoCalc.ToString("F2");
                }

                Decimal.TryParse(txtJuros.Text, out decimal jurosFinal);
                Decimal.TryParse(txtMulta.Text, out decimal multaFinal);
                Decimal.TryParse(txtDesconto.Text, out decimal descontoFinal);

                decimal valorCalculado = valorBase;

                if (dataPag > dataVenc) 
                {
                    valorCalculado = valorBase + jurosFinal + multaFinal;
                }
                else 
                {
                    valorCalculado = valorBase - descontoFinal;
                }

                if (valorCalculado < 0) valorCalculado = 0;

                txtValorPago.Text = valorCalculado.ToString("F2");
            }
            catch
            {
                txtValorPago.Text = "0,00";
            }
        }


    }
}