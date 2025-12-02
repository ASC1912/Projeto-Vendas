using Projeto.Controller;
using Projeto.Models;
using Projeto.Views.Consultas;
using System;
using System.Windows.Forms;

namespace Projeto.Views.Cadastros
{
    public partial class frmCadastroContasAReceber : Projeto.frmBase
    {
        public bool ModoBaixa { get; set; } = false;
        public bool ModoVisualizacao { get; set; } = false;

        private ContasAReceber aConta;
        private ContasAReceberController controller;

        private frmConsultaCliente oFrmConsultaCliente;
        private frmConsultaFrmPgto oFrmConsultaFrmPgto;

        public frmCadastroContasAReceber()
        {
            InitializeComponent();

            if (this.Controls.Find("btnSalvar", true).Length > 0)
            {
                this.Controls.Find("btnSalvar", true)[0].Click += btnSalvar_Click;
            }

            this.btnPesquisarCliente.Click += btnPesquisarCliente_Click;
            this.btnPesquisarFormaPgto.Click += btnPesquisarFormaPgto_Click;

            ConfigurarCamposMonetarios();

            dtpDataPagamento.ValueChanged += (s, e) => RecalcularValorPago();

            txtJuros.Leave += (s, e) => AtualizarTotalComValoresManuais();
            txtMulta.Leave += (s, e) => AtualizarTotalComValoresManuais();
            txtDesconto.Leave += (s, e) => AtualizarTotalComValoresManuais();
        }

        public void setFrmConsultaCliente(object obj) { oFrmConsultaCliente = (frmConsultaCliente)obj; }
        public void setFrmConsultaFormaPagamento(object obj) { oFrmConsultaFrmPgto = (frmConsultaFrmPgto)obj; }

        private void ConfigurarCamposMonetarios()
        {
            txtValorVencimento.KeyPress += TextBox_KeyPress_Moeda;
            txtValorPago.KeyPress += TextBox_KeyPress_Moeda;
            txtJuros.KeyPress += TextBox_KeyPress_Moeda;
            txtMulta.KeyPress += TextBox_KeyPress_Moeda;
            txtDesconto.KeyPress += TextBox_KeyPress_Moeda;
        }

        public override void ConhecaObj(object obj, object ctrl)
        {
            aConta = (ContasAReceber)obj;
            controller = (ContasAReceberController)ctrl;
            CarregaTxt();
        }

        public override void LimparTxt()
        {
            base.LimparTxt();

            txtValorVencimento.Text = "0,00";
            txtValorPago.Text = "0,00";
            txtJuros.Text = "0,00";
            txtMulta.Text = "0,00";
            txtDesconto.Text = "0,00";

            if (this.Controls.ContainsKey("txtJurosPorcentagem")) txtJurosPorcentagem.Text = "0,00%";
            if (this.Controls.ContainsKey("txtMultaPorcentagem")) txtMultaPorcentagem.Text = "0,00%";
            if (this.Controls.ContainsKey("txtDescontoPorcentagem")) txtDescontoPorcentagem.Text = "0,00%";

            dtpEmissao.Value = DateTime.Now;
            dtpVencimento.Value = DateTime.Now;
            dtpDataPagamento.Value = DateTime.Now;

            aConta = new ContasAReceber();

            if (lblMotivoCancelamento != null) lblMotivoCancelamento.Visible = false;
        }

        public override void CarregaTxt()
        {
            if (aConta == null) return;

            txtCodigo.Text = aConta.VendaModelo;
            txtSerie.Text = aConta.VendaSerie;
            txtNumero.Text = aConta.VendaNumeroNota.ToString();
            txtIdCliente.Text = aConta.ClienteId.ToString();
            txtCliente.Text = aConta.NomeCliente;

            dtpEmissao.MinDate = DateTimePicker.MinimumDateTime;
            dtpEmissao.MaxDate = DateTimePicker.MaximumDateTime;
            dtpEmissao.Value = aConta.DataEmissao;

            dtpVencimento.MinDate = DateTimePicker.MinimumDateTime;
            dtpVencimento.MaxDate = DateTimePicker.MaximumDateTime;
            dtpVencimento.Value = aConta.DataVencimento;

            txtValorVencimento.Text = aConta.ValorVencimento.ToString("F2");
            txtIdFormaPgto.Text = aConta.IdFormaPagamento?.ToString() ?? "";
            txtFormaPgto.Text = aConta.NomeFormaPagamento ?? "";

            if (!string.IsNullOrEmpty(aConta.MotivoCancelamento))
            {
                lblMotivoCancelamento.Text = "Motivo: " + aConta.MotivoCancelamento;
                lblMotivoCancelamento.Visible = true;
            }
            else lblMotivoCancelamento.Visible = false;

            Control[] btns = this.Controls.Find("btnSalvar", true);
            Button btnSalvar = btns.Length > 0 ? (Button)btns[0] : null;

            if (ModoBaixa)
            {
                this.Text = "Baixa de Conta a Receber";
                if (btnSalvar != null)
                {
                    btnSalvar.Text = "Confirmar Baixa";
                    btnSalvar.Visible = true;
                }

                BloquearTxt();
                DesbloquearCamposDePagamento();

                dtpDataPagamento.MinDate = DateTimePicker.MinimumDateTime;
                dtpDataPagamento.MaxDate = DateTimePicker.MaximumDateTime;
                dtpDataPagamento.Value = DateTime.Today;   respeitar o MaxDate

                txtJurosPorcentagem.Text = (aConta.Juros ?? 0).ToString("N2") + "%";
                txtMultaPorcentagem.Text = (aConta.Multa ?? 0).ToString("N2") + "%";
                txtDescontoPorcentagem.Text = (aConta.Desconto ?? 0).ToString("N2") + "%";

                RecalcularValorPago();
            }
            else if (ModoVisualizacao)
            {
                this.Text = "Visualização de Conta";
                if (btnSalvar != null) btnSalvar.Visible = false;
                BloquearTxt();

                dtpDataPagamento.MinDate = DateTimePicker.MinimumDateTime;
                dtpDataPagamento.MaxDate = DateTimePicker.MaximumDateTime;
                dtpDataPagamento.Value = aConta.DataPagamento ?? DateTime.Now;

             
                bool contaQuitada = (aConta.Status == "Recebida" || aConta.Status == "Paga" || aConta.DataPagamento.HasValue);

                if (contaQuitada)
                {
                    txtValorPago.Text = (aConta.ValorPago ?? 0).ToString("F2");

                    txtJuros.Text = (aConta.Juros ?? 0).ToString("F2");
                    txtMulta.Text = (aConta.Multa ?? 0).ToString("F2");
                    txtDesconto.Text = (aConta.Desconto ?? 0).ToString("F2");

                    txtJurosPorcentagem.Text = "0,00%";
                    txtMultaPorcentagem.Text = "0,00%";
                    txtDescontoPorcentagem.Text = "0,00%";
                }
                else
                {
                    txtValorPago.Text = "0,00";

                    txtJuros.Text = "0,00";
                    txtMulta.Text = "0,00";
                    txtDesconto.Text = "0,00";

                    txtJurosPorcentagem.Text = (aConta.Juros ?? 0).ToString("N2") + "%";
                    txtMultaPorcentagem.Text = (aConta.Multa ?? 0).ToString("N2") + "%";
                    txtDescontoPorcentagem.Text = (aConta.Desconto ?? 0).ToString("N2") + "%";
                }
            }
        }

        private void RecalcularValorPago()
        {
            if (!ModoBaixa) return;
            if (aConta == null) return;

            DateTime dataPagamento = dtpDataPagamento.Value.Date;
            DateTime dataVencimento = dtpVencimento.Value.Date;

            decimal.TryParse(txtValorVencimento.Text, out decimal valorOriginal);

            decimal percJuros = aConta.Juros ?? 0;
            decimal percMulta = aConta.Multa ?? 0;
            decimal percDesconto = aConta.Desconto ?? 0;

            decimal valorJuros = 0;
            decimal valorMulta = 0;
            decimal valorDesconto = 0;

            if (dataPagamento > dataVencimento)
            {
                if (percJuros > 0)
                {
                    int diasAtraso = (dataPagamento - dataVencimento).Days;
                    valorJuros = (valorOriginal * (percJuros / 100)) / 30 * diasAtraso;
                }

                if (percMulta > 0)
                {
                    valorMulta = valorOriginal * (percMulta / 100);
                }
            }
            else
            {
                if (percDesconto > 0)
                {
                    valorDesconto = valorOriginal * (percDesconto / 100);
                }
            }

            decimal totalPagar = valorOriginal + valorJuros + valorMulta - valorDesconto;
            if (totalPagar < 0) totalPagar = 0;

            txtJuros.Text = valorJuros.ToString("F2");
            txtMulta.Text = valorMulta.ToString("F2");
            txtDesconto.Text = valorDesconto.ToString("F2");
            txtValorPago.Text = totalPagar.ToString("F2");
        }

        private void AtualizarTotalComValoresManuais()
        {
            decimal.TryParse(txtValorVencimento.Text, out decimal vlrOriginal);
            decimal.TryParse(txtJuros.Text, out decimal juros);
            decimal.TryParse(txtMulta.Text, out decimal multa);
            decimal.TryParse(txtDesconto.Text, out decimal desconto);

            decimal total = vlrOriginal + juros + multa - desconto;
            if (total < 0) total = 0;

            txtValorPago.Text = total.ToString("F2");
        }

        public override void BloquearTxt()
        {
            base.BloquearTxt();
            txtCodigo.Enabled = false;
            txtSerie.Enabled = false;
            txtNumero.Enabled = false;
            txtIdCliente.Enabled = false;
            txtCliente.Enabled = false;
            btnPesquisarCliente.Enabled = false;
            dtpEmissao.Enabled = false;
            dtpVencimento.Enabled = false;
            txtValorVencimento.Enabled = false;
            txtIdFormaPgto.Enabled = false;
            txtFormaPgto.Enabled = false;
            btnPesquisarFormaPgto.Enabled = false;

            dtpDataPagamento.Enabled = false;
            txtValorPago.Enabled = false;
            txtJuros.Enabled = false;
            txtMulta.Enabled = false;
            txtDesconto.Enabled = false;
        }

        private void DesbloquearCamposDePagamento()
        {
            dtpDataPagamento.Enabled = true;
            txtValorPago.Enabled = true;
            txtJuros.Enabled = true;
            txtMulta.Enabled = true;
            txtDesconto.Enabled = true;
            txtIdFormaPgto.Enabled = true; 
            btnPesquisarFormaPgto.Enabled = true;

            txtValorPago.ReadOnly = false; 
            txtJuros.ReadOnly = false;
            txtMulta.ReadOnly = false;
            txtDesconto.ReadOnly = false;
            txtIdFormaPgto.ReadOnly = false;

            txtValorPago.Focus();
        }

        public override void DesbloquearTxt() { base.DesbloquearTxt(); }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (ModoVisualizacao) return;

            if (ModoBaixa)
            {
                try
                {
                    if (!decimal.TryParse(txtValorPago.Text, out decimal vlrPago))
                    {
                        MessageBox.Show("Valor Pago inválido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtValorPago.Focus();
                        return;
                    }

                    aConta.DataPagamento = dtpDataPagamento.Value;
                    aConta.ValorPago = vlrPago;

                    decimal.TryParse(txtJuros.Text, out decimal juros);
                    aConta.Juros = juros; 

                    decimal.TryParse(txtMulta.Text, out decimal multa);
                    aConta.Multa = multa;

                    decimal.TryParse(txtDesconto.Text, out decimal desconto);
                    aConta.Desconto = desconto;

                    if (int.TryParse(txtIdFormaPgto.Text, out int idForma))
                    {
                        aConta.IdFormaPagamento = idForma;
                    }

                    controller.Receber(aConta);

                    MessageBox.Show("Baixa realizada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao baixar conta: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnPesquisarCliente_Click(object sender, EventArgs e)
        {
            if (oFrmConsultaCliente == null) return;
            oFrmConsultaCliente.ModoSelecao = true;
            if (oFrmConsultaCliente.ShowDialog() == DialogResult.OK && oFrmConsultaCliente.ClienteSelecionado != null)
            {
                var c = oFrmConsultaCliente.ClienteSelecionado;
                txtIdCliente.Text = c.Id.ToString();
                txtCliente.Text = c.Nome;
            }
            oFrmConsultaCliente.ModoSelecao = false;
        }

        private void btnPesquisarFormaPgto_Click(object sender, EventArgs e)
        {
            if (oFrmConsultaFrmPgto == null) return;
            oFrmConsultaFrmPgto.ModoSelecao = true;
            if (oFrmConsultaFrmPgto.ShowDialog() == DialogResult.OK && oFrmConsultaFrmPgto.FormaSelecionada != null)
            {
                var f = oFrmConsultaFrmPgto.FormaSelecionada;
                txtIdFormaPgto.Text = f.Id.ToString();
                txtFormaPgto.Text = f.Descricao;
            }
            oFrmConsultaFrmPgto.ModoSelecao = false;
        }

        private void TextBox_KeyPress_Moeda(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ',')) e.Handled = true;
            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1)) e.Handled = true;
        }
    }
}