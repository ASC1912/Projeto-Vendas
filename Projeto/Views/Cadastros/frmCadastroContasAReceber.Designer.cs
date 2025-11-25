namespace Projeto.Views.Cadastros
{
    partial class frmCadastroContasAReceber
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblSerie = new System.Windows.Forms.Label();
            this.lblNumero = new System.Windows.Forms.Label();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.txtSerie = new System.Windows.Forms.TextBox();
            this.lblDataVencimento = new System.Windows.Forms.Label();
            this.dtpVencimento = new System.Windows.Forms.DateTimePicker();
            this.lblDataEmissao = new System.Windows.Forms.Label();
            this.dtpEmissao = new System.Windows.Forms.DateTimePicker();
            this.btnPesquisarCliente = new System.Windows.Forms.Button();
            this.lblCliente = new System.Windows.Forms.Label();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.lblIdCliente = new System.Windows.Forms.Label();
            this.txtIDCliente = new System.Windows.Forms.TextBox();
            this.lblModelo = new System.Windows.Forms.Label();
            this.lblMotivoCancelamento = new System.Windows.Forms.Label();
            this.lblDataPagamento = new System.Windows.Forms.Label();
            this.dtpDataPagamento = new System.Windows.Forms.DateTimePicker();
            this.lblValorPago = new System.Windows.Forms.Label();
            this.txtValorPago = new System.Windows.Forms.TextBox();
            this.lblDesconto = new System.Windows.Forms.Label();
            this.txtDesconto = new System.Windows.Forms.TextBox();
            this.lblMulta = new System.Windows.Forms.Label();
            this.txtMulta = new System.Windows.Forms.TextBox();
            this.lblJuros = new System.Windows.Forms.Label();
            this.txtJuros = new System.Windows.Forms.TextBox();
            this.btnPesquisarFormaPgto = new System.Windows.Forms.Button();
            this.lblFormaPgto = new System.Windows.Forms.Label();
            this.txtFormaPgto = new System.Windows.Forms.TextBox();
            this.lblIdFormaPgto = new System.Windows.Forms.Label();
            this.txtIdFormaPgto = new System.Windows.Forms.TextBox();
            this.lblValorVencimento = new System.Windows.Forms.Label();
            this.txtValorVencimento = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtCodigo
            // 
            this.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            // 
            // lblSerie
            // 
            this.lblSerie.AutoSize = true;
            this.lblSerie.Location = new System.Drawing.Point(159, 9);
            this.lblSerie.Name = "lblSerie";
            this.lblSerie.Size = new System.Drawing.Size(39, 16);
            this.lblSerie.TabIndex = 159;
            this.lblSerie.Text = "Série";
            // 
            // lblNumero
            // 
            this.lblNumero.AutoSize = true;
            this.lblNumero.Location = new System.Drawing.Point(299, 9);
            this.lblNumero.Name = "lblNumero";
            this.lblNumero.Size = new System.Drawing.Size(55, 16);
            this.lblNumero.TabIndex = 158;
            this.lblNumero.Text = "Número";
            // 
            // txtNumero
            // 
            this.txtNumero.Location = new System.Drawing.Point(299, 28);
            this.txtNumero.MaxLength = 10;
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(100, 22);
            this.txtNumero.TabIndex = 148;
            // 
            // txtSerie
            // 
            this.txtSerie.Location = new System.Drawing.Point(159, 28);
            this.txtSerie.MaxLength = 5;
            this.txtSerie.Name = "txtSerie";
            this.txtSerie.Size = new System.Drawing.Size(100, 22);
            this.txtSerie.TabIndex = 147;
            // 
            // lblDataVencimento
            // 
            this.lblDataVencimento.AutoSize = true;
            this.lblDataVencimento.Location = new System.Drawing.Point(1033, 9);
            this.lblDataVencimento.Name = "lblDataVencimento";
            this.lblDataVencimento.Size = new System.Drawing.Size(129, 16);
            this.lblDataVencimento.TabIndex = 157;
            this.lblDataVencimento.Text = "Data de Vencimento";
            // 
            // dtpVencimento
            // 
            this.dtpVencimento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpVencimento.Location = new System.Drawing.Point(1036, 28);
            this.dtpVencimento.Name = "dtpVencimento";
            this.dtpVencimento.Size = new System.Drawing.Size(160, 22);
            this.dtpVencimento.TabIndex = 153;
            this.dtpVencimento.Value = new System.DateTime(2025, 7, 10, 0, 0, 0, 0);
            // 
            // lblDataEmissao
            // 
            this.lblDataEmissao.AutoSize = true;
            this.lblDataEmissao.Location = new System.Drawing.Point(816, 9);
            this.lblDataEmissao.Name = "lblDataEmissao";
            this.lblDataEmissao.Size = new System.Drawing.Size(111, 16);
            this.lblDataEmissao.TabIndex = 156;
            this.lblDataEmissao.Text = "Data de Emissão";
            // 
            // dtpEmissao
            // 
            this.dtpEmissao.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEmissao.Location = new System.Drawing.Point(819, 28);
            this.dtpEmissao.Name = "dtpEmissao";
            this.dtpEmissao.Size = new System.Drawing.Size(160, 22);
            this.dtpEmissao.TabIndex = 152;
            this.dtpEmissao.Value = new System.DateTime(2025, 7, 10, 0, 0, 0, 0);
            // 
            // btnPesquisarCliente
            // 
            this.btnPesquisarCliente.Location = new System.Drawing.Point(707, 27);
            this.btnPesquisarCliente.Name = "btnPesquisarCliente";
            this.btnPesquisarCliente.Size = new System.Drawing.Size(91, 23);
            this.btnPesquisarCliente.TabIndex = 151;
            this.btnPesquisarCliente.Text = "Pesquisar";
            this.btnPesquisarCliente.UseVisualStyleBackColor = true;
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Location = new System.Drawing.Point(490, 8);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(48, 16);
            this.lblCliente.TabIndex = 155;
            this.lblCliente.Text = "Cliente";
            // 
            // txtCliente
            // 
            this.txtCliente.Location = new System.Drawing.Point(490, 27);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.ReadOnly = true;
            this.txtCliente.Size = new System.Drawing.Size(211, 22);
            this.txtCliente.TabIndex = 150;
            // 
            // lblIdCliente
            // 
            this.lblIdCliente.AutoSize = true;
            this.lblIdCliente.Location = new System.Drawing.Point(424, 8);
            this.lblIdCliente.Name = "lblIdCliente";
            this.lblIdCliente.Size = new System.Drawing.Size(64, 16);
            this.lblIdCliente.TabIndex = 154;
            this.lblIdCliente.Text = "ID Cliente";
            // 
            // txtIDCliente
            // 
            this.txtIDCliente.Location = new System.Drawing.Point(424, 27);
            this.txtIDCliente.Name = "txtIDCliente";
            this.txtIDCliente.Size = new System.Drawing.Size(48, 22);
            this.txtIDCliente.TabIndex = 149;
            // 
            // lblModelo
            // 
            this.lblModelo.AutoSize = true;
            this.lblModelo.Location = new System.Drawing.Point(12, 8);
            this.lblModelo.Name = "lblModelo";
            this.lblModelo.Size = new System.Drawing.Size(53, 16);
            this.lblModelo.TabIndex = 160;
            this.lblModelo.Text = "Modelo";
            // 
            // lblMotivoCancelamento
            // 
            this.lblMotivoCancelamento.AutoSize = true;
            this.lblMotivoCancelamento.Location = new System.Drawing.Point(15, 299);
            this.lblMotivoCancelamento.Name = "lblMotivoCancelamento";
            this.lblMotivoCancelamento.Size = new System.Drawing.Size(159, 16);
            this.lblMotivoCancelamento.TabIndex = 178;
            this.lblMotivoCancelamento.Text = "Motivo do Cancelamento:";
            // 
            // lblDataPagamento
            // 
            this.lblDataPagamento.AutoSize = true;
            this.lblDataPagamento.Location = new System.Drawing.Point(658, 165);
            this.lblDataPagamento.Name = "lblDataPagamento";
            this.lblDataPagamento.Size = new System.Drawing.Size(128, 16);
            this.lblDataPagamento.TabIndex = 177;
            this.lblDataPagamento.Text = "Data de Pagamento";
            // 
            // dtpDataPagamento
            // 
            this.dtpDataPagamento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataPagamento.Location = new System.Drawing.Point(661, 184);
            this.dtpDataPagamento.Name = "dtpDataPagamento";
            this.dtpDataPagamento.Size = new System.Drawing.Size(160, 22);
            this.dtpDataPagamento.TabIndex = 169;
            this.dtpDataPagamento.Value = new System.DateTime(2025, 7, 10, 0, 0, 0, 0);
            // 
            // lblValorPago
            // 
            this.lblValorPago.AutoSize = true;
            this.lblValorPago.Location = new System.Drawing.Point(514, 165);
            this.lblValorPago.Name = "lblValorPago";
            this.lblValorPago.Size = new System.Drawing.Size(103, 16);
            this.lblValorPago.TabIndex = 176;
            this.lblValorPago.Text = "Valor Pago (R$)";
            // 
            // txtValorPago
            // 
            this.txtValorPago.Location = new System.Drawing.Point(514, 184);
            this.txtValorPago.Name = "txtValorPago";
            this.txtValorPago.Size = new System.Drawing.Size(88, 22);
            this.txtValorPago.TabIndex = 168;
            // 
            // lblDesconto
            // 
            this.lblDesconto.AutoSize = true;
            this.lblDesconto.Location = new System.Drawing.Point(283, 165);
            this.lblDesconto.Name = "lblDesconto";
            this.lblDesconto.Size = new System.Drawing.Size(65, 16);
            this.lblDesconto.TabIndex = 175;
            this.lblDesconto.Text = "Desconto";
            // 
            // txtDesconto
            // 
            this.txtDesconto.Location = new System.Drawing.Point(283, 184);
            this.txtDesconto.Name = "txtDesconto";
            this.txtDesconto.Size = new System.Drawing.Size(65, 22);
            this.txtDesconto.TabIndex = 167;
            // 
            // lblMulta
            // 
            this.lblMulta.AutoSize = true;
            this.lblMulta.Location = new System.Drawing.Point(149, 165);
            this.lblMulta.Name = "lblMulta";
            this.lblMulta.Size = new System.Drawing.Size(39, 16);
            this.lblMulta.TabIndex = 174;
            this.lblMulta.Text = "Multa";
            // 
            // txtMulta
            // 
            this.txtMulta.Location = new System.Drawing.Point(149, 184);
            this.txtMulta.Name = "txtMulta";
            this.txtMulta.Size = new System.Drawing.Size(62, 22);
            this.txtMulta.TabIndex = 166;
            // 
            // lblJuros
            // 
            this.lblJuros.AutoSize = true;
            this.lblJuros.Location = new System.Drawing.Point(15, 165);
            this.lblJuros.Name = "lblJuros";
            this.lblJuros.Size = new System.Drawing.Size(40, 16);
            this.lblJuros.TabIndex = 173;
            this.lblJuros.Text = "Juros";
            // 
            // txtJuros
            // 
            this.txtJuros.Location = new System.Drawing.Point(15, 184);
            this.txtJuros.Name = "txtJuros";
            this.txtJuros.Size = new System.Drawing.Size(69, 22);
            this.txtJuros.TabIndex = 165;
            // 
            // btnPesquisarFormaPgto
            // 
            this.btnPesquisarFormaPgto.Location = new System.Drawing.Point(363, 98);
            this.btnPesquisarFormaPgto.Name = "btnPesquisarFormaPgto";
            this.btnPesquisarFormaPgto.Size = new System.Drawing.Size(91, 23);
            this.btnPesquisarFormaPgto.TabIndex = 164;
            this.btnPesquisarFormaPgto.Text = "Pesquisar";
            this.btnPesquisarFormaPgto.UseVisualStyleBackColor = true;
            // 
            // lblFormaPgto
            // 
            this.lblFormaPgto.AutoSize = true;
            this.lblFormaPgto.Location = new System.Drawing.Point(146, 79);
            this.lblFormaPgto.Name = "lblFormaPgto";
            this.lblFormaPgto.Size = new System.Drawing.Size(138, 16);
            this.lblFormaPgto.TabIndex = 172;
            this.lblFormaPgto.Text = "Forma de Pagamento";
            // 
            // txtFormaPgto
            // 
            this.txtFormaPgto.Location = new System.Drawing.Point(146, 98);
            this.txtFormaPgto.Name = "txtFormaPgto";
            this.txtFormaPgto.ReadOnly = true;
            this.txtFormaPgto.Size = new System.Drawing.Size(211, 22);
            this.txtFormaPgto.TabIndex = 163;
            // 
            // lblIdFormaPgto
            // 
            this.lblIdFormaPgto.AutoSize = true;
            this.lblIdFormaPgto.Location = new System.Drawing.Point(12, 79);
            this.lblIdFormaPgto.Name = "lblIdFormaPgto";
            this.lblIdFormaPgto.Size = new System.Drawing.Size(90, 16);
            this.lblIdFormaPgto.TabIndex = 171;
            this.lblIdFormaPgto.Text = "ID FormaPgto";
            // 
            // txtIdFormaPgto
            // 
            this.txtIdFormaPgto.Location = new System.Drawing.Point(15, 98);
            this.txtIdFormaPgto.Name = "txtIdFormaPgto";
            this.txtIdFormaPgto.Size = new System.Drawing.Size(69, 22);
            this.txtIdFormaPgto.TabIndex = 162;
            // 
            // lblValorVencimento
            // 
            this.lblValorVencimento.AutoSize = true;
            this.lblValorVencimento.Location = new System.Drawing.Point(517, 79);
            this.lblValorVencimento.Name = "lblValorVencimento";
            this.lblValorVencimento.Size = new System.Drawing.Size(118, 16);
            this.lblValorVencimento.TabIndex = 170;
            this.lblValorVencimento.Text = "Valor a Pagar (R$)";
            // 
            // txtValorVencimento
            // 
            this.txtValorVencimento.Location = new System.Drawing.Point(517, 98);
            this.txtValorVencimento.Name = "txtValorVencimento";
            this.txtValorVencimento.Size = new System.Drawing.Size(88, 22);
            this.txtValorVencimento.TabIndex = 161;
            // 
            // frmCadastroContasAReceber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(1342, 681);
            this.Controls.Add(this.lblMotivoCancelamento);
            this.Controls.Add(this.lblDataPagamento);
            this.Controls.Add(this.dtpDataPagamento);
            this.Controls.Add(this.lblValorPago);
            this.Controls.Add(this.txtValorPago);
            this.Controls.Add(this.lblDesconto);
            this.Controls.Add(this.txtDesconto);
            this.Controls.Add(this.lblMulta);
            this.Controls.Add(this.txtMulta);
            this.Controls.Add(this.lblJuros);
            this.Controls.Add(this.txtJuros);
            this.Controls.Add(this.btnPesquisarFormaPgto);
            this.Controls.Add(this.lblFormaPgto);
            this.Controls.Add(this.txtFormaPgto);
            this.Controls.Add(this.lblIdFormaPgto);
            this.Controls.Add(this.txtIdFormaPgto);
            this.Controls.Add(this.lblValorVencimento);
            this.Controls.Add(this.txtValorVencimento);
            this.Controls.Add(this.lblModelo);
            this.Controls.Add(this.lblSerie);
            this.Controls.Add(this.lblNumero);
            this.Controls.Add(this.txtNumero);
            this.Controls.Add(this.txtSerie);
            this.Controls.Add(this.lblDataVencimento);
            this.Controls.Add(this.dtpVencimento);
            this.Controls.Add(this.lblDataEmissao);
            this.Controls.Add(this.dtpEmissao);
            this.Controls.Add(this.btnPesquisarCliente);
            this.Controls.Add(this.lblCliente);
            this.Controls.Add(this.txtCliente);
            this.Controls.Add(this.lblIdCliente);
            this.Controls.Add(this.txtIDCliente);
            this.Name = "frmCadastroContasAReceber";
            this.Controls.SetChildIndex(this.txtIDCliente, 0);
            this.Controls.SetChildIndex(this.lblIdCliente, 0);
            this.Controls.SetChildIndex(this.txtCliente, 0);
            this.Controls.SetChildIndex(this.lblCliente, 0);
            this.Controls.SetChildIndex(this.btnPesquisarCliente, 0);
            this.Controls.SetChildIndex(this.dtpEmissao, 0);
            this.Controls.SetChildIndex(this.lblDataEmissao, 0);
            this.Controls.SetChildIndex(this.dtpVencimento, 0);
            this.Controls.SetChildIndex(this.lblDataVencimento, 0);
            this.Controls.SetChildIndex(this.txtSerie, 0);
            this.Controls.SetChildIndex(this.txtNumero, 0);
            this.Controls.SetChildIndex(this.lblNumero, 0);
            this.Controls.SetChildIndex(this.lblSerie, 0);
            this.Controls.SetChildIndex(this.btnSair, 0);
            this.Controls.SetChildIndex(this.txtCodigo, 0);
            this.Controls.SetChildIndex(this.chkInativo, 0);
            this.Controls.SetChildIndex(this.lblDataCriacao, 0);
            this.Controls.SetChildIndex(this.lblDataModificacao, 0);
            this.Controls.SetChildIndex(this.btnSalvar, 0);
            this.Controls.SetChildIndex(this.lblModelo, 0);
            this.Controls.SetChildIndex(this.txtValorVencimento, 0);
            this.Controls.SetChildIndex(this.lblValorVencimento, 0);
            this.Controls.SetChildIndex(this.txtIdFormaPgto, 0);
            this.Controls.SetChildIndex(this.lblIdFormaPgto, 0);
            this.Controls.SetChildIndex(this.txtFormaPgto, 0);
            this.Controls.SetChildIndex(this.lblFormaPgto, 0);
            this.Controls.SetChildIndex(this.btnPesquisarFormaPgto, 0);
            this.Controls.SetChildIndex(this.txtJuros, 0);
            this.Controls.SetChildIndex(this.lblJuros, 0);
            this.Controls.SetChildIndex(this.txtMulta, 0);
            this.Controls.SetChildIndex(this.lblMulta, 0);
            this.Controls.SetChildIndex(this.txtDesconto, 0);
            this.Controls.SetChildIndex(this.lblDesconto, 0);
            this.Controls.SetChildIndex(this.txtValorPago, 0);
            this.Controls.SetChildIndex(this.lblValorPago, 0);
            this.Controls.SetChildIndex(this.dtpDataPagamento, 0);
            this.Controls.SetChildIndex(this.lblDataPagamento, 0);
            this.Controls.SetChildIndex(this.lblMotivoCancelamento, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSerie;
        private System.Windows.Forms.Label lblNumero;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.TextBox txtSerie;
        private System.Windows.Forms.Label lblDataVencimento;
        private System.Windows.Forms.DateTimePicker dtpVencimento;
        private System.Windows.Forms.Label lblDataEmissao;
        private System.Windows.Forms.DateTimePicker dtpEmissao;
        private System.Windows.Forms.Button btnPesquisarCliente;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.Label lblIdCliente;
        private System.Windows.Forms.TextBox txtIDCliente;
        private System.Windows.Forms.Label lblModelo;
        private System.Windows.Forms.Label lblMotivoCancelamento;
        private System.Windows.Forms.Label lblDataPagamento;
        private System.Windows.Forms.DateTimePicker dtpDataPagamento;
        private System.Windows.Forms.Label lblValorPago;
        private System.Windows.Forms.TextBox txtValorPago;
        private System.Windows.Forms.Label lblDesconto;
        private System.Windows.Forms.TextBox txtDesconto;
        private System.Windows.Forms.Label lblMulta;
        private System.Windows.Forms.TextBox txtMulta;
        private System.Windows.Forms.Label lblJuros;
        private System.Windows.Forms.TextBox txtJuros;
        private System.Windows.Forms.Button btnPesquisarFormaPgto;
        private System.Windows.Forms.Label lblFormaPgto;
        private System.Windows.Forms.TextBox txtFormaPgto;
        private System.Windows.Forms.Label lblIdFormaPgto;
        private System.Windows.Forms.TextBox txtIdFormaPgto;
        private System.Windows.Forms.Label lblValorVencimento;
        private System.Windows.Forms.TextBox txtValorVencimento;
    }
}
