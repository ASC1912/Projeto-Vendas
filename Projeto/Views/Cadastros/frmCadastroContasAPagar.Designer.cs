namespace Projeto.Views.Cadastros
{
    partial class frmCadastroContasAPagar
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
            this.lblDataVencimento = new System.Windows.Forms.Label();
            this.dtpVencimento = new System.Windows.Forms.DateTimePicker();
            this.lblDataEmissao = new System.Windows.Forms.Label();
            this.dtpEmissao = new System.Windows.Forms.DateTimePicker();
            this.btnPesquisarFornecedor = new System.Windows.Forms.Button();
            this.lblFornecedor = new System.Windows.Forms.Label();
            this.txtFornecedor = new System.Windows.Forms.TextBox();
            this.lblIdFornecedor = new System.Windows.Forms.Label();
            this.txtIDFornecedor = new System.Windows.Forms.TextBox();
            this.lblValorVencimento = new System.Windows.Forms.Label();
            this.txtValorVencimento = new System.Windows.Forms.TextBox();
            this.lblDescricao = new System.Windows.Forms.Label();
            this.txtDescricao = new System.Windows.Forms.TextBox();
            this.btnPesquisarFormaPgto = new System.Windows.Forms.Button();
            this.lblFormaPgto = new System.Windows.Forms.Label();
            this.txtFormaPgto = new System.Windows.Forms.TextBox();
            this.lblIdFormaPgto = new System.Windows.Forms.Label();
            this.txtIdFormaPgto = new System.Windows.Forms.TextBox();
            this.lblSerie = new System.Windows.Forms.Label();
            this.lblNumero = new System.Windows.Forms.Label();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.txtSerie = new System.Windows.Forms.TextBox();
            this.lblModelo = new System.Windows.Forms.Label();
            this.lblDesconto = new System.Windows.Forms.Label();
            this.txtDesconto = new System.Windows.Forms.TextBox();
            this.lblMulta = new System.Windows.Forms.Label();
            this.txtMulta = new System.Windows.Forms.TextBox();
            this.lblJuros = new System.Windows.Forms.Label();
            this.txtJuros = new System.Windows.Forms.TextBox();
            this.lblValorPago = new System.Windows.Forms.Label();
            this.txtValorPago = new System.Windows.Forms.TextBox();
            this.lblDataPagamento = new System.Windows.Forms.Label();
            this.dtpDataPagamento = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // txtCodigo
            // 
            this.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // lblDataVencimento
            // 
            this.lblDataVencimento.AutoSize = true;
            this.lblDataVencimento.Location = new System.Drawing.Point(1038, 11);
            this.lblDataVencimento.Name = "lblDataVencimento";
            this.lblDataVencimento.Size = new System.Drawing.Size(129, 16);
            this.lblDataVencimento.TabIndex = 133;
            this.lblDataVencimento.Text = "Data de Vencimento";
            // 
            // dtpVencimento
            // 
            this.dtpVencimento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpVencimento.Location = new System.Drawing.Point(1041, 30);
            this.dtpVencimento.Name = "dtpVencimento";
            this.dtpVencimento.Size = new System.Drawing.Size(160, 22);
            this.dtpVencimento.TabIndex = 127;
            this.dtpVencimento.Value = new System.DateTime(2025, 7, 10, 0, 0, 0, 0);
            // 
            // lblDataEmissao
            // 
            this.lblDataEmissao.AutoSize = true;
            this.lblDataEmissao.Location = new System.Drawing.Point(821, 11);
            this.lblDataEmissao.Name = "lblDataEmissao";
            this.lblDataEmissao.Size = new System.Drawing.Size(111, 16);
            this.lblDataEmissao.TabIndex = 132;
            this.lblDataEmissao.Text = "Data de Emissão";
            // 
            // dtpEmissao
            // 
            this.dtpEmissao.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEmissao.Location = new System.Drawing.Point(824, 30);
            this.dtpEmissao.Name = "dtpEmissao";
            this.dtpEmissao.Size = new System.Drawing.Size(160, 22);
            this.dtpEmissao.TabIndex = 126;
            this.dtpEmissao.Value = new System.DateTime(2025, 7, 10, 0, 0, 0, 0);
            // 
            // btnPesquisarFornecedor
            // 
            this.btnPesquisarFornecedor.Location = new System.Drawing.Point(712, 29);
            this.btnPesquisarFornecedor.Name = "btnPesquisarFornecedor";
            this.btnPesquisarFornecedor.Size = new System.Drawing.Size(91, 23);
            this.btnPesquisarFornecedor.TabIndex = 125;
            this.btnPesquisarFornecedor.Text = "Pesquisar";
            this.btnPesquisarFornecedor.UseVisualStyleBackColor = true;
            this.btnPesquisarFornecedor.Click += new System.EventHandler(this.btnPesquisarFornecedor_Click);
            // 
            // lblFornecedor
            // 
            this.lblFornecedor.AutoSize = true;
            this.lblFornecedor.Location = new System.Drawing.Point(495, 10);
            this.lblFornecedor.Name = "lblFornecedor";
            this.lblFornecedor.Size = new System.Drawing.Size(77, 16);
            this.lblFornecedor.TabIndex = 131;
            this.lblFornecedor.Text = "Fornecedor";
            // 
            // txtFornecedor
            // 
            this.txtFornecedor.Location = new System.Drawing.Point(495, 29);
            this.txtFornecedor.Name = "txtFornecedor";
            this.txtFornecedor.ReadOnly = true;
            this.txtFornecedor.Size = new System.Drawing.Size(211, 22);
            this.txtFornecedor.TabIndex = 124;
            // 
            // lblIdFornecedor
            // 
            this.lblIdFornecedor.AutoSize = true;
            this.lblIdFornecedor.Location = new System.Drawing.Point(429, 10);
            this.lblIdFornecedor.Name = "lblIdFornecedor";
            this.lblIdFornecedor.Size = new System.Drawing.Size(53, 16);
            this.lblIdFornecedor.TabIndex = 130;
            this.lblIdFornecedor.Text = "ID Forn.";
            // 
            // txtIDFornecedor
            // 
            this.txtIDFornecedor.Location = new System.Drawing.Point(429, 29);
            this.txtIDFornecedor.Name = "txtIDFornecedor";
            this.txtIDFornecedor.Size = new System.Drawing.Size(48, 22);
            this.txtIDFornecedor.TabIndex = 123;
            this.txtIDFornecedor.Leave += new System.EventHandler(this.txtIDFornecedor_Leave);
            // 
            // lblValorVencimento
            // 
            this.lblValorVencimento.AutoSize = true;
            this.lblValorVencimento.Location = new System.Drawing.Point(307, 93);
            this.lblValorVencimento.Name = "lblValorVencimento";
            this.lblValorVencimento.Size = new System.Drawing.Size(118, 16);
            this.lblValorVencimento.TabIndex = 137;
            this.lblValorVencimento.Text = "Valor a Pagar (R$)";
            // 
            // txtValorVencimento
            // 
            this.txtValorVencimento.Location = new System.Drawing.Point(307, 112);
            this.txtValorVencimento.Name = "txtValorVencimento";
            this.txtValorVencimento.Size = new System.Drawing.Size(88, 22);
            this.txtValorVencimento.TabIndex = 135;
            // 
            // lblDescricao
            // 
            this.lblDescricao.AutoSize = true;
            this.lblDescricao.Location = new System.Drawing.Point(15, 93);
            this.lblDescricao.Name = "lblDescricao";
            this.lblDescricao.Size = new System.Drawing.Size(69, 16);
            this.lblDescricao.TabIndex = 136;
            this.lblDescricao.Text = "Descrição";
            // 
            // txtDescricao
            // 
            this.txtDescricao.Location = new System.Drawing.Point(15, 112);
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Size = new System.Drawing.Size(249, 22);
            this.txtDescricao.TabIndex = 134;
            // 
            // btnPesquisarFormaPgto
            // 
            this.btnPesquisarFormaPgto.Location = new System.Drawing.Point(363, 212);
            this.btnPesquisarFormaPgto.Name = "btnPesquisarFormaPgto";
            this.btnPesquisarFormaPgto.Size = new System.Drawing.Size(91, 23);
            this.btnPesquisarFormaPgto.TabIndex = 140;
            this.btnPesquisarFormaPgto.Text = "Pesquisar";
            this.btnPesquisarFormaPgto.UseVisualStyleBackColor = true;
            this.btnPesquisarFormaPgto.Click += new System.EventHandler(this.btnPesquisarFormaPgto_Click);
            // 
            // lblFormaPgto
            // 
            this.lblFormaPgto.AutoSize = true;
            this.lblFormaPgto.Location = new System.Drawing.Point(146, 193);
            this.lblFormaPgto.Name = "lblFormaPgto";
            this.lblFormaPgto.Size = new System.Drawing.Size(138, 16);
            this.lblFormaPgto.TabIndex = 142;
            this.lblFormaPgto.Text = "Forma de Pagamento";
            // 
            // txtFormaPgto
            // 
            this.txtFormaPgto.Location = new System.Drawing.Point(146, 212);
            this.txtFormaPgto.Name = "txtFormaPgto";
            this.txtFormaPgto.ReadOnly = true;
            this.txtFormaPgto.Size = new System.Drawing.Size(211, 22);
            this.txtFormaPgto.TabIndex = 139;
            // 
            // lblIdFormaPgto
            // 
            this.lblIdFormaPgto.AutoSize = true;
            this.lblIdFormaPgto.Location = new System.Drawing.Point(12, 193);
            this.lblIdFormaPgto.Name = "lblIdFormaPgto";
            this.lblIdFormaPgto.Size = new System.Drawing.Size(90, 16);
            this.lblIdFormaPgto.TabIndex = 141;
            this.lblIdFormaPgto.Text = "ID FormaPgto";
            // 
            // txtIdFormaPgto
            // 
            this.txtIdFormaPgto.Location = new System.Drawing.Point(15, 212);
            this.txtIdFormaPgto.Name = "txtIdFormaPgto";
            this.txtIdFormaPgto.Size = new System.Drawing.Size(87, 22);
            this.txtIdFormaPgto.TabIndex = 138;
            this.txtIdFormaPgto.Leave += new System.EventHandler(this.txtIdFormaPgto_Leave);
            // 
            // lblSerie
            // 
            this.lblSerie.AutoSize = true;
            this.lblSerie.Location = new System.Drawing.Point(164, 11);
            this.lblSerie.Name = "lblSerie";
            this.lblSerie.Size = new System.Drawing.Size(39, 16);
            this.lblSerie.TabIndex = 146;
            this.lblSerie.Text = "Série";
            // 
            // lblNumero
            // 
            this.lblNumero.AutoSize = true;
            this.lblNumero.Location = new System.Drawing.Point(304, 11);
            this.lblNumero.Name = "lblNumero";
            this.lblNumero.Size = new System.Drawing.Size(55, 16);
            this.lblNumero.TabIndex = 145;
            this.lblNumero.Text = "Número";
            // 
            // txtNumero
            // 
            this.txtNumero.Location = new System.Drawing.Point(304, 30);
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(100, 22);
            this.txtNumero.TabIndex = 144;
            // 
            // txtSerie
            // 
            this.txtSerie.Location = new System.Drawing.Point(164, 30);
            this.txtSerie.Name = "txtSerie";
            this.txtSerie.Size = new System.Drawing.Size(100, 22);
            this.txtSerie.TabIndex = 143;
            // 
            // lblModelo
            // 
            this.lblModelo.AutoSize = true;
            this.lblModelo.Location = new System.Drawing.Point(15, 8);
            this.lblModelo.Name = "lblModelo";
            this.lblModelo.Size = new System.Drawing.Size(53, 16);
            this.lblModelo.TabIndex = 148;
            this.lblModelo.Text = "Modelo";
            // 
            // lblDesconto
            // 
            this.lblDesconto.AutoSize = true;
            this.lblDesconto.Location = new System.Drawing.Point(283, 279);
            this.lblDesconto.Name = "lblDesconto";
            this.lblDesconto.Size = new System.Drawing.Size(65, 16);
            this.lblDesconto.TabIndex = 154;
            this.lblDesconto.Text = "Desconto";
            // 
            // txtDesconto
            // 
            this.txtDesconto.Location = new System.Drawing.Point(283, 298);
            this.txtDesconto.Name = "txtDesconto";
            this.txtDesconto.Size = new System.Drawing.Size(100, 22);
            this.txtDesconto.TabIndex = 151;
            // 
            // lblMulta
            // 
            this.lblMulta.AutoSize = true;
            this.lblMulta.Location = new System.Drawing.Point(149, 279);
            this.lblMulta.Name = "lblMulta";
            this.lblMulta.Size = new System.Drawing.Size(39, 16);
            this.lblMulta.TabIndex = 153;
            this.lblMulta.Text = "Multa";
            // 
            // txtMulta
            // 
            this.txtMulta.Location = new System.Drawing.Point(149, 298);
            this.txtMulta.Name = "txtMulta";
            this.txtMulta.Size = new System.Drawing.Size(100, 22);
            this.txtMulta.TabIndex = 150;
            // 
            // lblJuros
            // 
            this.lblJuros.AutoSize = true;
            this.lblJuros.Location = new System.Drawing.Point(15, 279);
            this.lblJuros.Name = "lblJuros";
            this.lblJuros.Size = new System.Drawing.Size(40, 16);
            this.lblJuros.TabIndex = 152;
            this.lblJuros.Text = "Juros";
            // 
            // txtJuros
            // 
            this.txtJuros.Location = new System.Drawing.Point(15, 298);
            this.txtJuros.Name = "txtJuros";
            this.txtJuros.Size = new System.Drawing.Size(100, 22);
            this.txtJuros.TabIndex = 149;
            // 
            // lblValorPago
            // 
            this.lblValorPago.AutoSize = true;
            this.lblValorPago.Location = new System.Drawing.Point(514, 279);
            this.lblValorPago.Name = "lblValorPago";
            this.lblValorPago.Size = new System.Drawing.Size(103, 16);
            this.lblValorPago.TabIndex = 156;
            this.lblValorPago.Text = "Valor Pago (R$)";
            // 
            // txtValorPago
            // 
            this.txtValorPago.Location = new System.Drawing.Point(514, 298);
            this.txtValorPago.Name = "txtValorPago";
            this.txtValorPago.Size = new System.Drawing.Size(88, 22);
            this.txtValorPago.TabIndex = 155;
            // 
            // lblDataPagamento
            // 
            this.lblDataPagamento.AutoSize = true;
            this.lblDataPagamento.Location = new System.Drawing.Point(658, 279);
            this.lblDataPagamento.Name = "lblDataPagamento";
            this.lblDataPagamento.Size = new System.Drawing.Size(128, 16);
            this.lblDataPagamento.TabIndex = 159;
            this.lblDataPagamento.Text = "Data de Pagamento";
            // 
            // dtpDataPagamento
            // 
            this.dtpDataPagamento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataPagamento.Location = new System.Drawing.Point(661, 298);
            this.dtpDataPagamento.Name = "dtpDataPagamento";
            this.dtpDataPagamento.Size = new System.Drawing.Size(160, 22);
            this.dtpDataPagamento.TabIndex = 158;
            this.dtpDataPagamento.Value = new System.DateTime(2025, 7, 10, 0, 0, 0, 0);
            // 
            // frmCadastroContasAPagar
            // 
            //this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            //this.ClientSize = new System.Drawing.Size(1342, 681);
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
            this.Controls.Add(this.lblModelo);
            this.Controls.Add(this.lblSerie);
            this.Controls.Add(this.lblNumero);
            this.Controls.Add(this.txtNumero);
            this.Controls.Add(this.txtSerie);
            this.Controls.Add(this.btnPesquisarFormaPgto);
            this.Controls.Add(this.lblFormaPgto);
            this.Controls.Add(this.txtFormaPgto);
            this.Controls.Add(this.lblIdFormaPgto);
            this.Controls.Add(this.txtIdFormaPgto);
            this.Controls.Add(this.lblValorVencimento);
            this.Controls.Add(this.txtValorVencimento);
            this.Controls.Add(this.lblDescricao);
            this.Controls.Add(this.txtDescricao);
            this.Controls.Add(this.lblDataVencimento);
            this.Controls.Add(this.dtpVencimento);
            this.Controls.Add(this.lblDataEmissao);
            this.Controls.Add(this.dtpEmissao);
            this.Controls.Add(this.btnPesquisarFornecedor);
            this.Controls.Add(this.lblFornecedor);
            this.Controls.Add(this.txtFornecedor);
            this.Controls.Add(this.lblIdFornecedor);
            this.Controls.Add(this.txtIDFornecedor);
            this.Name = "frmCadastroContasAPagar";
            this.Controls.SetChildIndex(this.btnSair, 0);
            this.Controls.SetChildIndex(this.txtCodigo, 0);
            this.Controls.SetChildIndex(this.chkInativo, 0);
            this.Controls.SetChildIndex(this.lblDataCriacao, 0);
            this.Controls.SetChildIndex(this.lblDataModificacao, 0);
            this.Controls.SetChildIndex(this.btnSalvar, 0);
            this.Controls.SetChildIndex(this.txtIDFornecedor, 0);
            this.Controls.SetChildIndex(this.lblIdFornecedor, 0);
            this.Controls.SetChildIndex(this.txtFornecedor, 0);
            this.Controls.SetChildIndex(this.lblFornecedor, 0);
            this.Controls.SetChildIndex(this.btnPesquisarFornecedor, 0);
            this.Controls.SetChildIndex(this.dtpEmissao, 0);
            this.Controls.SetChildIndex(this.lblDataEmissao, 0);
            this.Controls.SetChildIndex(this.dtpVencimento, 0);
            this.Controls.SetChildIndex(this.lblDataVencimento, 0);
            this.Controls.SetChildIndex(this.txtDescricao, 0);
            this.Controls.SetChildIndex(this.lblDescricao, 0);
            this.Controls.SetChildIndex(this.txtValorVencimento, 0);
            this.Controls.SetChildIndex(this.lblValorVencimento, 0);
            this.Controls.SetChildIndex(this.txtIdFormaPgto, 0);
            this.Controls.SetChildIndex(this.lblIdFormaPgto, 0);
            this.Controls.SetChildIndex(this.txtFormaPgto, 0);
            this.Controls.SetChildIndex(this.lblFormaPgto, 0);
            this.Controls.SetChildIndex(this.btnPesquisarFormaPgto, 0);
            this.Controls.SetChildIndex(this.txtSerie, 0);
            this.Controls.SetChildIndex(this.txtNumero, 0);
            this.Controls.SetChildIndex(this.lblNumero, 0);
            this.Controls.SetChildIndex(this.lblSerie, 0);
            this.Controls.SetChildIndex(this.lblModelo, 0);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDataVencimento;
        private System.Windows.Forms.DateTimePicker dtpVencimento;
        private System.Windows.Forms.Label lblDataEmissao;
        private System.Windows.Forms.DateTimePicker dtpEmissao;
        private System.Windows.Forms.Button btnPesquisarFornecedor;
        private System.Windows.Forms.Label lblFornecedor;
        private System.Windows.Forms.TextBox txtFornecedor;
        private System.Windows.Forms.Label lblIdFornecedor;
        private System.Windows.Forms.TextBox txtIDFornecedor;
        private System.Windows.Forms.Label lblValorVencimento;
        private System.Windows.Forms.TextBox txtValorVencimento;
        private System.Windows.Forms.Label lblDescricao;
        private System.Windows.Forms.TextBox txtDescricao;
        private System.Windows.Forms.Button btnPesquisarFormaPgto;
        private System.Windows.Forms.Label lblFormaPgto;
        private System.Windows.Forms.TextBox txtFormaPgto;
        private System.Windows.Forms.Label lblIdFormaPgto;
        private System.Windows.Forms.TextBox txtIdFormaPgto;
        private System.Windows.Forms.Label lblSerie;
        private System.Windows.Forms.Label lblNumero;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.TextBox txtSerie;
        private System.Windows.Forms.Label lblModelo;
        private System.Windows.Forms.Label lblDesconto;
        private System.Windows.Forms.TextBox txtDesconto;
        private System.Windows.Forms.Label lblMulta;
        private System.Windows.Forms.TextBox txtMulta;
        private System.Windows.Forms.Label lblJuros;
        private System.Windows.Forms.TextBox txtJuros;
        private System.Windows.Forms.Label lblValorPago;
        private System.Windows.Forms.TextBox txtValorPago;
        private System.Windows.Forms.Label lblDataPagamento;
        private System.Windows.Forms.DateTimePicker dtpDataPagamento;
    }
}
