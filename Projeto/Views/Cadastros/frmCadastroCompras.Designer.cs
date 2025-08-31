namespace Projeto.Views.Cadastros
{
    partial class frmCadastroCompras
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
            this.lblModelo = new System.Windows.Forms.Label();
            this.btnPesquisar = new System.Windows.Forms.Button();
            this.lblFornecedor = new System.Windows.Forms.Label();
            this.txtFornecedor = new System.Windows.Forms.TextBox();
            this.lblIdFornecedor = new System.Windows.Forms.Label();
            this.txtIDFornecedor = new System.Windows.Forms.TextBox();
            this.lblSerie = new System.Windows.Forms.Label();
            this.lblNumero = new System.Windows.Forms.Label();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.txtSerie = new System.Windows.Forms.TextBox();
            this.lblDataChegada = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.lblDataEmissao = new System.Windows.Forms.Label();
            this.dtpNascimento = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(12, 43);
            // 
            // chkInativo
            // 
            this.chkInativo.Location = new System.Drawing.Point(1232, 12);
            // 
            // lblModelo
            // 
            this.lblModelo.AutoSize = true;
            this.lblModelo.Location = new System.Drawing.Point(12, 25);
            this.lblModelo.Name = "lblModelo";
            this.lblModelo.Size = new System.Drawing.Size(53, 16);
            this.lblModelo.TabIndex = 98;
            this.lblModelo.Text = "Modelo";
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.Location = new System.Drawing.Point(699, 43);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(91, 23);
            this.btnPesquisar.TabIndex = 116;
            this.btnPesquisar.Text = "Pesquisar";
            this.btnPesquisar.UseVisualStyleBackColor = true;
            // 
            // lblFornecedor
            // 
            this.lblFornecedor.AutoSize = true;
            this.lblFornecedor.Location = new System.Drawing.Point(482, 24);
            this.lblFornecedor.Name = "lblFornecedor";
            this.lblFornecedor.Size = new System.Drawing.Size(77, 16);
            this.lblFornecedor.TabIndex = 115;
            this.lblFornecedor.Text = "Fornecedor";
            // 
            // txtFornecedor
            // 
            this.txtFornecedor.Location = new System.Drawing.Point(482, 43);
            this.txtFornecedor.Name = "txtFornecedor";
            this.txtFornecedor.Size = new System.Drawing.Size(211, 22);
            this.txtFornecedor.TabIndex = 114;
            // 
            // lblIdFornecedor
            // 
            this.lblIdFornecedor.AutoSize = true;
            this.lblIdFornecedor.Location = new System.Drawing.Point(416, 24);
            this.lblIdFornecedor.Name = "lblIdFornecedor";
            this.lblIdFornecedor.Size = new System.Drawing.Size(53, 16);
            this.lblIdFornecedor.TabIndex = 113;
            this.lblIdFornecedor.Text = "ID Forn.";
            // 
            // txtIDFornecedor
            // 
            this.txtIDFornecedor.Location = new System.Drawing.Point(416, 43);
            this.txtIDFornecedor.Name = "txtIDFornecedor";
            this.txtIDFornecedor.Size = new System.Drawing.Size(48, 22);
            this.txtIDFornecedor.TabIndex = 112;
            // 
            // lblSerie
            // 
            this.lblSerie.AutoSize = true;
            this.lblSerie.Location = new System.Drawing.Point(150, 24);
            this.lblSerie.Name = "lblSerie";
            this.lblSerie.Size = new System.Drawing.Size(39, 16);
            this.lblSerie.TabIndex = 111;
            this.lblSerie.Text = "Série";
            // 
            // lblNumero
            // 
            this.lblNumero.AutoSize = true;
            this.lblNumero.Location = new System.Drawing.Point(290, 24);
            this.lblNumero.Name = "lblNumero";
            this.lblNumero.Size = new System.Drawing.Size(55, 16);
            this.lblNumero.TabIndex = 110;
            this.lblNumero.Text = "Número";
            // 
            // txtNumero
            // 
            this.txtNumero.Location = new System.Drawing.Point(290, 43);
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(100, 22);
            this.txtNumero.TabIndex = 109;
            // 
            // txtSerie
            // 
            this.txtSerie.Location = new System.Drawing.Point(150, 43);
            this.txtSerie.Name = "txtSerie";
            this.txtSerie.Size = new System.Drawing.Size(100, 22);
            this.txtSerie.TabIndex = 108;
            // 
            // lblDataChegada
            // 
            this.lblDataChegada.AutoSize = true;
            this.lblDataChegada.Location = new System.Drawing.Point(1025, 25);
            this.lblDataChegada.Name = "lblDataChegada";
            this.lblDataChegada.Size = new System.Drawing.Size(114, 16);
            this.lblDataChegada.TabIndex = 120;
            this.lblDataChegada.Text = "Data de Chegada";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(1028, 44);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(160, 22);
            this.dateTimePicker1.TabIndex = 119;
            this.dateTimePicker1.Value = new System.DateTime(2025, 7, 10, 0, 0, 0, 0);
            // 
            // lblDataEmissao
            // 
            this.lblDataEmissao.AutoSize = true;
            this.lblDataEmissao.Location = new System.Drawing.Point(808, 25);
            this.lblDataEmissao.Name = "lblDataEmissao";
            this.lblDataEmissao.Size = new System.Drawing.Size(111, 16);
            this.lblDataEmissao.TabIndex = 118;
            this.lblDataEmissao.Text = "Data de Emissão";
            // 
            // dtpNascimento
            // 
            this.dtpNascimento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNascimento.Location = new System.Drawing.Point(811, 44);
            this.dtpNascimento.Name = "dtpNascimento";
            this.dtpNascimento.Size = new System.Drawing.Size(160, 22);
            this.dtpNascimento.TabIndex = 117;
            this.dtpNascimento.Value = new System.DateTime(2025, 7, 10, 0, 0, 0, 0);
            // 
            // frmCadastroCompras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(1342, 681);
            this.Controls.Add(this.lblDataChegada);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.lblDataEmissao);
            this.Controls.Add(this.dtpNascimento);
            this.Controls.Add(this.btnPesquisar);
            this.Controls.Add(this.lblFornecedor);
            this.Controls.Add(this.txtFornecedor);
            this.Controls.Add(this.lblIdFornecedor);
            this.Controls.Add(this.txtIDFornecedor);
            this.Controls.Add(this.lblSerie);
            this.Controls.Add(this.lblNumero);
            this.Controls.Add(this.txtNumero);
            this.Controls.Add(this.txtSerie);
            this.Controls.Add(this.lblModelo);
            this.Name = "frmCadastroCompras";
            this.Controls.SetChildIndex(this.btnSair, 0);
            this.Controls.SetChildIndex(this.txtCodigo, 0);
            this.Controls.SetChildIndex(this.chkInativo, 0);
            this.Controls.SetChildIndex(this.lblDataCriacao, 0);
            this.Controls.SetChildIndex(this.lblDataModificacao, 0);
            this.Controls.SetChildIndex(this.lblModelo, 0);
            this.Controls.SetChildIndex(this.txtSerie, 0);
            this.Controls.SetChildIndex(this.txtNumero, 0);
            this.Controls.SetChildIndex(this.lblNumero, 0);
            this.Controls.SetChildIndex(this.lblSerie, 0);
            this.Controls.SetChildIndex(this.txtIDFornecedor, 0);
            this.Controls.SetChildIndex(this.lblIdFornecedor, 0);
            this.Controls.SetChildIndex(this.txtFornecedor, 0);
            this.Controls.SetChildIndex(this.lblFornecedor, 0);
            this.Controls.SetChildIndex(this.btnPesquisar, 0);
            this.Controls.SetChildIndex(this.dtpNascimento, 0);
            this.Controls.SetChildIndex(this.lblDataEmissao, 0);
            this.Controls.SetChildIndex(this.dateTimePicker1, 0);
            this.Controls.SetChildIndex(this.lblDataChegada, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblModelo;
        private System.Windows.Forms.Button btnPesquisar;
        private System.Windows.Forms.Label lblFornecedor;
        private System.Windows.Forms.TextBox txtFornecedor;
        private System.Windows.Forms.Label lblIdFornecedor;
        private System.Windows.Forms.TextBox txtIDFornecedor;
        private System.Windows.Forms.Label lblSerie;
        private System.Windows.Forms.Label lblNumero;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.TextBox txtSerie;
        private System.Windows.Forms.Label lblDataChegada;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label lblDataEmissao;
        private System.Windows.Forms.DateTimePicker dtpNascimento;
    }
}
