namespace Projeto.Views
{
    partial class frmCadastroCidade
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
            this.lblEstado = new System.Windows.Forms.Label();
            this.lblNome = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtEstado = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(9, 207);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(50, 16);
            this.lblEstado.TabIndex = 21;
            this.lblEstado.Text = "Estado";
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Location = new System.Drawing.Point(9, 101);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(51, 16);
            this.lblNome.TabIndex = 19;
            this.lblNome.Text = "Cidade";
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(12, 120);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(181, 22);
            this.txtNome.TabIndex = 18;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(1117, 634);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(80, 35);
            this.btnSalvar.TabIndex = 17;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Location = new System.Drawing.Point(9, 9);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(51, 16);
            this.lblCodigo.TabIndex = 22;
            this.lblCodigo.Text = "Código";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(228, 225);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(150, 23);
            this.btnBuscar.TabIndex = 66;
            this.btnBuscar.Text = "Buscar Estado";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtEstado
            // 
            this.txtEstado.Location = new System.Drawing.Point(12, 226);
            this.txtEstado.Name = "txtEstado";
            this.txtEstado.ReadOnly = true;
            this.txtEstado.Size = new System.Drawing.Size(181, 22);
            this.txtEstado.TabIndex = 67;
            // 
            // frmCadastroCidade
            // 
            //this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            //this.ClientSize = new System.Drawing.Size(1342, 681);
            this.Controls.Add(this.txtEstado);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.lblCodigo);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.btnSalvar);
            this.Name = "frmCadastroCidade";
            this.Text = "Cadastro Cidades";
            this.Load += new System.EventHandler(this.frmCadastroCidade_Load);
            this.Controls.SetChildIndex(this.lblDataModificacao, 0);
            this.Controls.SetChildIndex(this.chkInativo, 0);
            this.Controls.SetChildIndex(this.lblDataCriacao, 0);
            this.Controls.SetChildIndex(this.btnSair, 0);
            this.Controls.SetChildIndex(this.txtCodigo, 0);
            this.Controls.SetChildIndex(this.btnSalvar, 0);
            this.Controls.SetChildIndex(this.txtNome, 0);
            this.Controls.SetChildIndex(this.lblNome, 0);
            this.Controls.SetChildIndex(this.lblEstado, 0);
            this.Controls.SetChildIndex(this.lblCodigo, 0);
            this.Controls.SetChildIndex(this.btnBuscar, 0);
            this.Controls.SetChildIndex(this.txtEstado, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtEstado;
    }
}
