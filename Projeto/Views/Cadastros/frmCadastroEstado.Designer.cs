﻿namespace Projeto.Views
{
    partial class frmCadastroEstado
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
            this.lblNome = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.lblPais = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtPais = new System.Windows.Forms.TextBox();
            this.lblUF = new System.Windows.Forms.Label();
            this.txtUF = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtCodigo
            // 
            this.txtCodigo.TabIndex = 0;
            // 
            // btnSair
            // 
            this.btnSair.TabIndex = 7;
            // 
            // chkInativo
            // 
            this.chkInativo.TabIndex = 5;
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Location = new System.Drawing.Point(9, 91);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(55, 16);
            this.lblNome.TabIndex = 13;
            this.lblNome.Text = "Estado*";
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(12, 110);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(200, 22);
            this.txtNome.TabIndex = 1;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(1121, 634);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(80, 35);
            this.btnSalvar.TabIndex = 6;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Location = new System.Drawing.Point(12, 9);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(51, 16);
            this.lblCodigo.TabIndex = 14;
            this.lblCodigo.Text = "Código";
            // 
            // lblPais
            // 
            this.lblPais.AutoSize = true;
            this.lblPais.Location = new System.Drawing.Point(12, 164);
            this.lblPais.Name = "lblPais";
            this.lblPais.Size = new System.Drawing.Size(39, 16);
            this.lblPais.TabIndex = 16;
            this.lblPais.Text = "País*";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(265, 183);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(150, 23);
            this.btnBuscar.TabIndex = 4;
            this.btnBuscar.Text = "Buscar País";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtPais
            // 
            this.txtPais.Location = new System.Drawing.Point(12, 184);
            this.txtPais.Name = "txtPais";
            this.txtPais.ReadOnly = true;
            this.txtPais.Size = new System.Drawing.Size(200, 22);
            this.txtPais.TabIndex = 3;
            // 
            // lblUF
            // 
            this.lblUF.AutoSize = true;
            this.lblUF.Location = new System.Drawing.Point(262, 91);
            this.lblUF.Name = "lblUF";
            this.lblUF.Size = new System.Drawing.Size(30, 16);
            this.lblUF.TabIndex = 68;
            this.lblUF.Text = "UF*";
            // 
            // txtUF
            // 
            this.txtUF.Location = new System.Drawing.Point(265, 110);
            this.txtUF.Name = "txtUF";
            this.txtUF.Size = new System.Drawing.Size(70, 22);
            this.txtUF.TabIndex = 2;
            // 
            // frmCadastroEstado
            // 
            //this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            //this.ClientSize = new System.Drawing.Size(1342, 681);
            this.Controls.Add(this.lblUF);
            this.Controls.Add(this.txtUF);
            this.Controls.Add(this.txtPais);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.lblPais);
            this.Controls.Add(this.lblCodigo);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.btnSalvar);
            this.Name = "frmCadastroEstado";
            this.Text = "Cadastro Estados";
            this.Load += new System.EventHandler(this.frmCadastroEstado_Load);
            this.Controls.SetChildIndex(this.lblDataModificacao, 0);
            this.Controls.SetChildIndex(this.chkInativo, 0);
            this.Controls.SetChildIndex(this.lblDataCriacao, 0);
            this.Controls.SetChildIndex(this.btnSair, 0);
            this.Controls.SetChildIndex(this.txtCodigo, 0);
            this.Controls.SetChildIndex(this.btnSalvar, 0);
            this.Controls.SetChildIndex(this.txtNome, 0);
            this.Controls.SetChildIndex(this.lblNome, 0);
            this.Controls.SetChildIndex(this.lblCodigo, 0);
            this.Controls.SetChildIndex(this.lblPais, 0);
            this.Controls.SetChildIndex(this.btnBuscar, 0);
            this.Controls.SetChildIndex(this.txtPais, 0);
            this.Controls.SetChildIndex(this.txtUF, 0);
            this.Controls.SetChildIndex(this.lblUF, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.Label lblPais;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtPais;
        private System.Windows.Forms.Label lblUF;
        private System.Windows.Forms.TextBox txtUF;
    }
}
