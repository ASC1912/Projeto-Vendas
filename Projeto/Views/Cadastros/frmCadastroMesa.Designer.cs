namespace Projeto.Views.Cadastros
{
    partial class frmCadastroMesa
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
            this.lblQtdCadeira = new System.Windows.Forms.Label();
            this.txtQtdCadeiras = new System.Windows.Forms.TextBox();
            this.lblLocalizacao = new System.Windows.Forms.Label();
            this.txtLocalizacao = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.lblNumeroMesa = new System.Windows.Forms.Label();
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
            // lblQtdCadeira
            // 
            this.lblQtdCadeira.AutoSize = true;
            this.lblQtdCadeira.Location = new System.Drawing.Point(9, 87);
            this.lblQtdCadeira.Name = "lblQtdCadeira";
            this.lblQtdCadeira.Size = new System.Drawing.Size(154, 16);
            this.lblQtdCadeira.TabIndex = 68;
            this.lblQtdCadeira.Text = "Quantidade de Cadeiras";
            // 
            // txtQtdCadeiras
            // 
            this.txtQtdCadeiras.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtQtdCadeiras.Location = new System.Drawing.Point(12, 106);
            this.txtQtdCadeiras.Name = "txtQtdCadeiras";
            this.txtQtdCadeiras.Size = new System.Drawing.Size(181, 22);
            this.txtQtdCadeiras.TabIndex = 67;
            // 
            // lblLocalizacao
            // 
            this.lblLocalizacao.AutoSize = true;
            this.lblLocalizacao.Location = new System.Drawing.Point(9, 166);
            this.lblLocalizacao.Name = "lblLocalizacao";
            this.lblLocalizacao.Size = new System.Drawing.Size(80, 16);
            this.lblLocalizacao.TabIndex = 70;
            this.lblLocalizacao.Text = "Localização";
            // 
            // txtLocalizacao
            // 
            this.txtLocalizacao.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLocalizacao.Location = new System.Drawing.Point(12, 185);
            this.txtLocalizacao.Name = "txtLocalizacao";
            this.txtLocalizacao.Size = new System.Drawing.Size(181, 22);
            this.txtLocalizacao.TabIndex = 69;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 250);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(44, 16);
            this.lblStatus.TabIndex = 72;
            this.lblStatus.Text = "Status";
            // 
            // cbStatus
            // 
            this.cbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Items.AddRange(new object[] {
            "LIVRE",
            "RESERVADO",
            "OCUPADO"});
            this.cbStatus.Location = new System.Drawing.Point(15, 269);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(121, 24);
            this.cbStatus.TabIndex = 73;
            // 
            // lblNumeroMesa
            // 
            this.lblNumeroMesa.AutoSize = true;
            this.lblNumeroMesa.Location = new System.Drawing.Point(12, 9);
            this.lblNumeroMesa.Name = "lblNumeroMesa";
            this.lblNumeroMesa.Size = new System.Drawing.Size(111, 16);
            this.lblNumeroMesa.TabIndex = 74;
            this.lblNumeroMesa.Text = "Número da Mesa";
            // 
            // frmCadastroMesa
            // 
            //this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            //this.ClientSize = new System.Drawing.Size(1342, 681);
            this.Controls.Add(this.lblNumeroMesa);
            this.Controls.Add(this.cbStatus);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblLocalizacao);
            this.Controls.Add(this.txtLocalizacao);
            this.Controls.Add(this.lblQtdCadeira);
            this.Controls.Add(this.txtQtdCadeiras);
            this.Name = "frmCadastroMesa";
            this.Text = "Cadastro Mesas";
            this.Controls.SetChildIndex(this.btnSair, 0);
            this.Controls.SetChildIndex(this.txtCodigo, 0);
            this.Controls.SetChildIndex(this.chkInativo, 0);
            this.Controls.SetChildIndex(this.lblDataCriacao, 0);
            this.Controls.SetChildIndex(this.lblDataModificacao, 0);
            this.Controls.SetChildIndex(this.btnSalvar, 0);
            this.Controls.SetChildIndex(this.txtQtdCadeiras, 0);
            this.Controls.SetChildIndex(this.lblQtdCadeira, 0);
            this.Controls.SetChildIndex(this.txtLocalizacao, 0);
            this.Controls.SetChildIndex(this.lblLocalizacao, 0);
            this.Controls.SetChildIndex(this.lblStatus, 0);
            this.Controls.SetChildIndex(this.cbStatus, 0);
            this.Controls.SetChildIndex(this.lblNumeroMesa, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblQtdCadeira;
        private System.Windows.Forms.TextBox txtQtdCadeiras;
        private System.Windows.Forms.Label lblLocalizacao;
        private System.Windows.Forms.TextBox txtLocalizacao;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.Label lblNumeroMesa;
    }
}
