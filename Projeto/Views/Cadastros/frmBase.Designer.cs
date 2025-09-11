namespace Projeto
{
    partial class frmBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.btnSair = new System.Windows.Forms.Button();
            this.chkInativo = new System.Windows.Forms.CheckBox();
            this.lblDataCriacao = new System.Windows.Forms.Label();
            this.lblDataModificacao = new System.Windows.Forms.Label();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(12, 27);
            this.txtCodigo.MaxLength = 99;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(119, 22);
            this.txtCodigo.TabIndex = 3;
            // 
            // btnSair
            // 
            this.btnSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSair.Location = new System.Drawing.Point(1220, 634);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(80, 35);
            this.btnSair.TabIndex = 2;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // chkInativo
            // 
            this.chkInativo.AutoSize = true;
            this.chkInativo.Location = new System.Drawing.Point(1220, 30);
            this.chkInativo.Name = "chkInativo";
            this.chkInativo.Size = new System.Drawing.Size(68, 20);
            this.chkInativo.TabIndex = 63;
            this.chkInativo.Text = "Inativo";
            this.chkInativo.UseVisualStyleBackColor = true;
            // 
            // lblDataCriacao
            // 
            this.lblDataCriacao.AutoSize = true;
            this.lblDataCriacao.Location = new System.Drawing.Point(12, 620);
            this.lblDataCriacao.Name = "lblDataCriacao";
            this.lblDataCriacao.Size = new System.Drawing.Size(72, 16);
            this.lblDataCriacao.TabIndex = 64;
            this.lblDataCriacao.Text = "Criado em:";
            // 
            // lblDataModificacao
            // 
            this.lblDataModificacao.AutoSize = true;
            this.lblDataModificacao.Location = new System.Drawing.Point(12, 656);
            this.lblDataModificacao.Name = "lblDataModificacao";
            this.lblDataModificacao.Size = new System.Drawing.Size(99, 16);
            this.lblDataModificacao.TabIndex = 65;
            this.lblDataModificacao.Text = "Modificado em:";
            // 
            // btnSalvar
            // 
            this.btnSalvar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvar.Location = new System.Drawing.Point(1108, 634);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(80, 35);
            this.btnSalvar.TabIndex = 66;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            // 
            // frmBase
            // 
            this.ClientSize = new System.Drawing.Size(1342, 681);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.lblDataModificacao);
            this.Controls.Add(this.lblDataCriacao);
            this.Controls.Add(this.chkInativo);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.btnSair);
            this.Name = "frmBase";
            this.Text = "frmBase";
            this.Load += new System.EventHandler(this.frmBase_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txtCodigo;
        protected System.Windows.Forms.Button btnSair;
        protected System.Windows.Forms.CheckBox chkInativo;
        protected System.Windows.Forms.Label lblDataCriacao;
        protected System.Windows.Forms.Label lblDataModificacao;
        public System.Windows.Forms.Button btnSalvar;
    }
}