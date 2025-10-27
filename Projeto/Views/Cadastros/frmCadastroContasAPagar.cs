using Projeto.Controller;
using Projeto.Models;
using Projeto.Utils;
using Projeto.Views.Consultas; 
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto.Views.Cadastros 
{
    public partial class frmCadastroContasAPagar : Projeto.frmBase 
    {
        private ContasAPagar aConta;
        private ContasAPagarController controller;
        private string modoOperacao;

        private FornecedorController fornecedorController = new FornecedorController();
        private FormaPagamentoController formaPagamentoController = new FormaPagamentoController();

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

            dtpEmissao.ValueChanged += (s, e) => { dtpVencimento.MinDate = dtpEmissao.Value.Date; };
            dtpVencimento.MinDate = dtpEmissao.Value.Date;

            // Conecta eventos para recalcular (se não fez no designer)
            this.txtJuros.TextChanged += new System.EventHandler(this.RecalcularValorPago);
            this.txtMulta.TextChanged += new System.EventHandler(this.RecalcularValorPago);
            this.txtDesconto.TextChanged += new System.EventHandler(this.RecalcularValorPago);
            this.dtpDataPagamento.ValueChanged += new System.EventHandler(this.RecalcularValorPago);

            // Campos da chave compra ReadOnly (Modelo é txtCodigo herdado?)
            if (this.Controls.ContainsKey("txtCodigo")) this.Controls["txtCodigo"].Enabled = false;
            txtSerie.ReadOnly = true;
            txtNumero.ReadOnly = true;
            txtSerie.Enabled = false; // Desabilitado também
            txtNumero.Enabled = false;// Desabilitado também
        }

        public override void ConhecaObj(object obj, object ctrl)
        {
            aConta = (obj as ContasAPagar) ?? new ContasAPagar();
            controller = ctrl as ContasAPagarController;
            // Opcional: Chamar CarregarNomesAuxiliares se necessário
        }

        // --- MÉTODO CHAVE: Define o modo da tela ---
        public void DefinirModo(string modo)
        {
            this.modoOperacao = modo;
            bool isPagamento = (modo == "Pagamento");

            this.Text = isPagamento ? "Pagamento de Conta a Pagar" : "Lançamento Manual de Conta a Pagar";
            btnSalvar.Text = isPagamento ? "Pagar" : "Salvar"; // Usa btnSalvar da base

            // Habilita/Desabilita campos
            txtIDFornecedor.Enabled = !isPagamento;
            btnPesquisarFornecedor.Enabled = !isPagamento;
            txtDescricao.Enabled = !isPagamento;
            dtpEmissao.Enabled = !isPagamento;
            dtpVencimento.Enabled = !isPagamento;
            txtValorVencimento.Enabled = !isPagamento; // Usa nome do seu Designer

            // Campos da chave da compra sempre desabilitados
            if (this.Controls.ContainsKey("txtCodigo")) this.Controls["txtCodigo"].Enabled = false;
            txtSerie.Enabled = false;
            txtNumero.Enabled = false;

            // Campos de Pagamento
            dtpDataPagamento.Enabled = isPagamento;
            txtValorPago.Enabled = isPagamento;
            txtValorPago.ReadOnly = isPagamento; // ReadOnly no modo Pagamento
            txtJuros.Enabled = isPagamento;
            txtMulta.Enabled = isPagamento;
            txtDesconto.Enabled = isPagamento;
            txtIdFormaPgto.Enabled = isPagamento;
            btnPesquisarFormaPgto.Enabled = isPagamento;

            if (isPagamento && aConta != null && aConta.Status == "Aberta")
            {
                DateTime dataSugerida = DateTime.Now.Date;
                if (dataSugerida < aConta.DataEmissao.Date) dataSugerida = aConta.DataEmissao.Date;
                dtpDataPagamento.Value = dataSugerida;
                dtpDataPagamento.Checked = true;
                RecalcularValorPago(null, null);
            }
            else if (!isPagamento) // Modo Lançamento
            {
                dtpDataPagamento.Checked = false;
                txtValorPago.Text = "0,00";
                txtJuros.Text = "0,00";
                txtMulta.Text = "0,00";
                txtDesconto.Text = "0,00";
                txtIdFormaPgto.Clear();
                txtFormaPgto.Clear();
            }
            else if (isPagamento)
            { // Modo Pagamento mas conta inválida ou já paga
                dtpDataPagamento.Checked = aConta?.DataPagamento.HasValue ?? false;
                RecalcularValorPago(null, null); // Calcula com base nos dados carregados
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
            txtValorVencimento.Text = "0,00"; // Usa nome do seu Designer

            dtpDataPagamento.Value = DateTime.Now;
            dtpDataPagamento.Checked = false;
            txtValorPago.Text = "0,00";
            txtJuros.Text = "0,00";
            txtMulta.Text = "0,00";
            txtDesconto.Text = "0,00";
            txtIdFormaPgto.Clear();
            txtFormaPgto.Clear();

            if (this.Controls.ContainsKey("txtCodigo")) this.Controls["txtCodigo"].Text = "";
            txtSerie.Clear();
            txtNumero.Clear();

            aConta = new ContasAPagar();
            DefinirModo("Lancamento");
        }

        public override void CarregaTxt()
        {
            if (aConta == null) return;

            txtIDFornecedor.Text = aConta.FornecedorId.ToString();
            txtFornecedor.Text = aConta.NomeFornecedor;
            txtDescricao.Text = aConta.Descricao;

            // Ajuste Min/Max das datas antes de setar o Value
            try
            {
                if (aConta.DataEmissao < dtpEmissao.MinDate) dtpEmissao.MinDate = aConta.DataEmissao.Date.AddYears(-1); // Ajuste range se necessário
                if (aConta.DataEmissao > dtpEmissao.MaxDate) dtpEmissao.MaxDate = aConta.DataEmissao.Date.AddYears(1);
                dtpEmissao.Value = aConta.DataEmissao;

                dtpVencimento.MinDate = dtpEmissao.Value.Date;
                if (aConta.DataVencimento < dtpVencimento.MinDate) dtpVencimento.MinDate = aConta.DataVencimento.Date;
                dtpVencimento.Value = aConta.DataVencimento;
            }
            catch { /* Ignora erro de data inválida no carregamento inicial */ }


            txtValorVencimento.Text = aConta.ValorVencimento.ToString("F2"); // Usa nome do seu Designer

            if (this.Controls.ContainsKey("txtCodigo")) this.Controls["txtCodigo"].Text = aConta.CompraModelo ?? "";
            txtSerie.Text = aConta.CompraSerie ?? "";
            txtNumero.Text = aConta.CompraNumeroNota > 0 ? aConta.CompraNumeroNota.ToString() : "";

            // Carrega dados de pagamento
            dtpDataPagamento.Checked = aConta.DataPagamento.HasValue;
            dtpDataPagamento.Value = aConta.DataPagamento ?? DateTime.Now;
            txtJuros.Text = aConta.Juros?.ToString("F2") ?? "0,00";
            txtMulta.Text = aConta.Multa?.ToString("F2") ?? "0,00";
            txtDesconto.Text = aConta.Desconto?.ToString("F2") ?? "0,00";
            txtIdFormaPgto.Text = aConta.IdFormaPagamento?.ToString() ?? "";
            txtFormaPgto.Text = aConta.NomeFormaPagamento;

            RecalcularValorPago(null, null); // Calcula valor a pagar
            if (aConta.Status == "Paga" && aConta.ValorPago.HasValue)
            {
                txtValorPago.Text = aConta.ValorPago.Value.ToString("F2"); // Mostra valor histórico se paga
            }
        }

        // --- BOTÃO SALVAR / PAGAR ---
        private async void btnSalvar_Click(object sender, EventArgs e) // Adicionado async
        {
            if (controller == null)
            {
                MessageBox.Show("Erro interno: Controller não inicializado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Validador.CampoObrigatorio(txtIDFornecedor, "Fornecedor") ||
                !Validador.CampoObrigatorio(txtDescricao, "Descrição"))
            {
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
                    if (!Validador.CampoObrigatorio(txtIdFormaPgto, "Forma de Pagamento")) return;

                    if (!decimal.TryParse(txtValorPago.Text, out decimal valorPagoCalculado) || valorPagoCalculado < 0)
                    {
                        MessageBox.Show("O Valor Pago calculado é inválido.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                else // Modo Lançamento
                {
                    if (!Validador.CampoObrigatorio(txtValorVencimento, "Valor da Conta") || Convert.ToDecimal(txtValorVencimento.Text) <= 0) return; // Usa nome do seu Designer

                    ContasAPagar novaContaManual = new ContasAPagar
                    {
                        FornecedorId = int.Parse(txtIDFornecedor.Text),
                        Descricao = txtDescricao.Text,
                        DataEmissao = dtpEmissao.Value.Date,
                        DataVencimento = dtpVencimento.Value.Date,
                        ValorVencimento = Convert.ToDecimal(txtValorVencimento.Text), // Usa nome do seu Designer
                        Status = "Aberta",
                        Ativo = true
                    };

                    await controller.SalvarManual(novaContaManual);
                    MessageBox.Show("Conta lançada manualmente com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Verifique os valores numéricos. Use vírgula decimal.\nDetalhe:" + ex.Message, "Erro de Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao processar a conta: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // --- EVENTOS AUXILIARES ---

        // Pesquisar Fornecedor (AJUSTADO PARA USAR SEU RETORNO)
        private async void btnPesquisarFornecedor_Click(object sender, EventArgs e)
        {
            using (var frmConsulta = new frmConsultaFornecedor()) // Usa sua consulta
            {
                frmConsulta.ModoSelecao = true; // Ativa modo seleção
                if (frmConsulta.ShowDialog() == DialogResult.OK)
                {
                    // CORREÇÃO: Usa a propriedade FornecedorSelecionado
                    var fornecedorSelecionado = frmConsulta.FornecedorSelecionado;

                    if (fornecedorSelecionado != null && fornecedorSelecionado.Id > 0)
                    {
                        txtIDFornecedor.Text = fornecedorSelecionado.Id.ToString();
                        txtFornecedor.Text = fornecedorSelecionado.Nome; // Usa seu txtFornecedor
                    }
                    else
                    {
                        txtIDFornecedor.Clear();
                        txtFornecedor.Clear();
                    }
                }
            }
        }

        // Pesquisar Forma de Pagamento (AJUSTADO PARA USAR SEU RETORNO)
        private async void btnPesquisarFormaPgto_Click(object sender, EventArgs e)
        {
            using (var frmConsulta = new frmConsultaFrmPgto()) // Usa sua consulta
            {
                frmConsulta.ModoSelecao = true; // Ativa modo seleção
                if (frmConsulta.ShowDialog() == DialogResult.OK)
                {
                    // CORREÇÃO: Usa a propriedade FormaSelecionada
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

        // --- EVENTOS Leave PARA BUSCA AUTOMÁTICA ---

        private async void txtIDFornecedor_Leave(object sender, EventArgs e)
        {
            // Verifica se o campo ID está vazio ou se o modo é Pagamento (onde não se busca)
            if (string.IsNullOrWhiteSpace(txtIDFornecedor.Text) || modoOperacao == "Pagamento")
            {
                // Se vazio no modo Lançamento, limpa o nome
                if (modoOperacao == "Lancamento") txtFornecedor.Clear();
                return;
            }

            // Tenta converter o ID para número
            if (int.TryParse(txtIDFornecedor.Text, out int id))
            {
                try
                {
                    // Busca o fornecedor pelo ID usando o controller
                    Fornecedor f = await fornecedorController.BuscarPorId(id);
                    if (f != null)
                    {
                        txtFornecedor.Text = f.Nome; // Exibe o nome encontrado
                    }
                    else
                    {
                        MessageBox.Show("Fornecedor não encontrado com este ID.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtFornecedor.Clear(); // Limpa o nome se não encontrou
                        // Opcional: Limpar o ID também? txtIDFornecedor.Clear(); txtIDFornecedor.Focus();
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
                txtFornecedor.Clear(); // Limpa o nome se o ID for inválido
                                       // Opcional: Limpar o ID também? txtIDFornecedor.Clear(); txtIDFornecedor.Focus();
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

        // --- CÁLCULO DO VALOR PAGO ---
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
                { // Usa nome do seu Designer
                    valorBase = valorTela;
                }
                if (valorBase <= 0)
                {
                    txtValorPago.Text = "0,00";
                    return;
                }

                Decimal.TryParse(txtJuros.Text, out decimal juros);
                Decimal.TryParse(txtMulta.Text, out decimal multa);
                Decimal.TryParse(txtDesconto.Text, out decimal desconto);

                decimal valorCalculado = valorBase;
                valorCalculado += (valorBase * juros / 100); // Juros

                DateTime dataVenc = aConta?.DataVencimento.Date ?? dtpVencimento.Value.Date;
                if (dtpDataPagamento.Value.Date > dataVenc)
                {
                    valorCalculado += (valorBase * multa / 100); // Multa
                }
                else
                {
                    valorCalculado -= (valorBase * desconto / 100); // Desconto
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