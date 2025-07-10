namespace Projeto.Views.Cadastros
{
    partial class frmCadastroVeiculo
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
            this.lblCodigo = new System.Windows.Forms.Label();
            this.lblPlaca = new System.Windows.Forms.Label();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.lblMarca = new System.Windows.Forms.Label();
            this.lblModelo = new System.Windows.Forms.Label();
            this.txtModelo = new System.Windows.Forms.TextBox();
            this.lblAnoFabricacao = new System.Windows.Forms.Label();
            this.txtAnoFabricacao = new System.Windows.Forms.TextBox();
            this.lblCapacidadeCarga = new System.Windows.Forms.Label();
            this.txtCapacidadeCarga = new System.Windows.Forms.TextBox();
            this.txtTransportadora = new System.Windows.Forms.TextBox();
            this.btnTransportadora = new System.Windows.Forms.Button();
            this.lblTransportadora = new System.Windows.Forms.Label();
            this.txtMarca = new System.Windows.Forms.TextBox();
            this.btnMarca = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(12, 42);
            this.txtCodigo.TabIndex = 0;
            // 
            // btnSair
            // 
            this.btnSair.TabIndex = 11;
            // 
            // chkInativo
            // 
            this.chkInativo.TabIndex = 9;
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Location = new System.Drawing.Point(12, 23);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(51, 16);
            this.lblCodigo.TabIndex = 71;
            this.lblCodigo.Text = "Código";
            // 
            // lblPlaca
            // 
            this.lblPlaca.AutoSize = true;
            this.lblPlaca.Location = new System.Drawing.Point(9, 105);
            this.lblPlaca.Name = "lblPlaca";
            this.lblPlaca.Size = new System.Drawing.Size(47, 16);
            this.lblPlaca.TabIndex = 95;
            this.lblPlaca.Text = "Placa*";
            // 
            // txtPlaca
            // 
            this.txtPlaca.Location = new System.Drawing.Point(12, 124);
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.Size = new System.Drawing.Size(200, 22);
            this.txtPlaca.TabIndex = 1;
            // 
            // lblMarca
            // 
            this.lblMarca.AutoSize = true;
            this.lblMarca.Location = new System.Drawing.Point(517, 105);
            this.lblMarca.Name = "lblMarca";
            this.lblMarca.Size = new System.Drawing.Size(50, 16);
            this.lblMarca.TabIndex = 97;
            this.lblMarca.Text = "Marca*";
            // 
            // lblModelo
            // 
            this.lblModelo.AutoSize = true;
            this.lblModelo.Location = new System.Drawing.Point(257, 105);
            this.lblModelo.Name = "lblModelo";
            this.lblModelo.Size = new System.Drawing.Size(58, 16);
            this.lblModelo.TabIndex = 99;
            this.lblModelo.Text = "Modelo*";
            // 
            // txtModelo
            // 
            this.txtModelo.Location = new System.Drawing.Point(260, 124);
            this.txtModelo.Name = "txtModelo";
            this.txtModelo.Size = new System.Drawing.Size(200, 22);
            this.txtModelo.TabIndex = 2;
            // 
            // lblAnoFabricacao
            // 
            this.lblAnoFabricacao.AutoSize = true;
            this.lblAnoFabricacao.Location = new System.Drawing.Point(12, 186);
            this.lblAnoFabricacao.Name = "lblAnoFabricacao";
            this.lblAnoFabricacao.Size = new System.Drawing.Size(122, 16);
            this.lblAnoFabricacao.TabIndex = 101;
            this.lblAnoFabricacao.Text = "Ano de Fabricação";
            // 
            // txtAnoFabricacao
            // 
            this.txtAnoFabricacao.Location = new System.Drawing.Point(15, 205);
            this.txtAnoFabricacao.Name = "txtAnoFabricacao";
            this.txtAnoFabricacao.Size = new System.Drawing.Size(150, 22);
            this.txtAnoFabricacao.TabIndex = 5;
            // 
            // lblCapacidadeCarga
            // 
            this.lblCapacidadeCarga.AutoSize = true;
            this.lblCapacidadeCarga.Location = new System.Drawing.Point(257, 186);
            this.lblCapacidadeCarga.Name = "lblCapacidadeCarga";
            this.lblCapacidadeCarga.Size = new System.Drawing.Size(172, 16);
            this.lblCapacidadeCarga.TabIndex = 103;
            this.lblCapacidadeCarga.Text = "Capacidade de Carga (kg)*";
            // 
            // txtCapacidadeCarga
            // 
            this.txtCapacidadeCarga.Location = new System.Drawing.Point(260, 205);
            this.txtCapacidadeCarga.Name = "txtCapacidadeCarga";
            this.txtCapacidadeCarga.Size = new System.Drawing.Size(150, 22);
            this.txtCapacidadeCarga.TabIndex = 6;
            // 
            // txtTransportadora
            // 
            this.txtTransportadora.Location = new System.Drawing.Point(520, 205);
            this.txtTransportadora.Name = "txtTransportadora";
            this.txtTransportadora.ReadOnly = true;
            this.txtTransportadora.Size = new System.Drawing.Size(200, 22);
            this.txtTransportadora.TabIndex = 7;
            // 
            // btnTransportadora
            // 
            this.btnTransportadora.Location = new System.Drawing.Point(746, 202);
            this.btnTransportadora.Name = "btnTransportadora";
            this.btnTransportadora.Size = new System.Drawing.Size(195, 23);
            this.btnTransportadora.TabIndex = 8;
            this.btnTransportadora.Text = "Buscar Transportadora";
            this.btnTransportadora.UseVisualStyleBackColor = true;
            this.btnTransportadora.Click += new System.EventHandler(this.btnBuscarTransportadora_Click);
            // 
            // lblTransportadora
            // 
            this.lblTransportadora.AutoSize = true;
            this.lblTransportadora.Location = new System.Drawing.Point(517, 184);
            this.lblTransportadora.Name = "lblTransportadora";
            this.lblTransportadora.Size = new System.Drawing.Size(106, 16);
            this.lblTransportadora.TabIndex = 104;
            this.lblTransportadora.Text = "Transportadora*";
            // 
            // txtMarca
            // 
            this.txtMarca.Location = new System.Drawing.Point(520, 124);
            this.txtMarca.Name = "txtMarca";
            this.txtMarca.ReadOnly = true;
            this.txtMarca.Size = new System.Drawing.Size(200, 22);
            this.txtMarca.TabIndex = 3;
            // 
            // btnMarca
            // 
            this.btnMarca.Location = new System.Drawing.Point(746, 124);
            this.btnMarca.Name = "btnMarca";
            this.btnMarca.Size = new System.Drawing.Size(195, 23);
            this.btnMarca.TabIndex = 4;
            this.btnMarca.Text = "Buscar Marca";
            this.btnMarca.UseVisualStyleBackColor = true;
            this.btnMarca.Click += new System.EventHandler(this.btnBuscarMarca_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(1117, 634);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(80, 35);
            this.btnSalvar.TabIndex = 10;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // frmCadastroVeiculo
            // 
            //this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            //this.ClientSize = new System.Drawing.Size(1342, 681);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnMarca);
            this.Controls.Add(this.txtMarca);
            this.Controls.Add(this.txtTransportadora);
            this.Controls.Add(this.btnTransportadora);
            this.Controls.Add(this.lblTransportadora);
            this.Controls.Add(this.lblCapacidadeCarga);
            this.Controls.Add(this.txtCapacidadeCarga);
            this.Controls.Add(this.lblAnoFabricacao);
            this.Controls.Add(this.txtAnoFabricacao);
            this.Controls.Add(this.lblModelo);
            this.Controls.Add(this.txtModelo);
            this.Controls.Add(this.lblMarca);
            this.Controls.Add(this.lblPlaca);
            this.Controls.Add(this.txtPlaca);
            this.Controls.Add(this.lblCodigo);
            this.Name = "frmCadastroVeiculo";
            this.Text = "Cadastro Veículo";
            this.Load += new System.EventHandler(this.frmCadastroVeiculo_Load);
            this.Controls.SetChildIndex(this.btnSair, 0);
            this.Controls.SetChildIndex(this.txtCodigo, 0);
            this.Controls.SetChildIndex(this.chkInativo, 0);
            this.Controls.SetChildIndex(this.lblDataCriacao, 0);
            this.Controls.SetChildIndex(this.lblDataModificacao, 0);
            this.Controls.SetChildIndex(this.lblCodigo, 0);
            this.Controls.SetChildIndex(this.txtPlaca, 0);
            this.Controls.SetChildIndex(this.lblPlaca, 0);
            this.Controls.SetChildIndex(this.lblMarca, 0);
            this.Controls.SetChildIndex(this.txtModelo, 0);
            this.Controls.SetChildIndex(this.lblModelo, 0);
            this.Controls.SetChildIndex(this.txtAnoFabricacao, 0);
            this.Controls.SetChildIndex(this.lblAnoFabricacao, 0);
            this.Controls.SetChildIndex(this.txtCapacidadeCarga, 0);
            this.Controls.SetChildIndex(this.lblCapacidadeCarga, 0);
            this.Controls.SetChildIndex(this.lblTransportadora, 0);
            this.Controls.SetChildIndex(this.btnTransportadora, 0);
            this.Controls.SetChildIndex(this.txtTransportadora, 0);
            this.Controls.SetChildIndex(this.txtMarca, 0);
            this.Controls.SetChildIndex(this.btnMarca, 0);
            this.Controls.SetChildIndex(this.btnSalvar, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.Label lblPlaca;
        private System.Windows.Forms.TextBox txtPlaca;
        private System.Windows.Forms.Label lblMarca;
        private System.Windows.Forms.Label lblModelo;
        private System.Windows.Forms.TextBox txtModelo;
        private System.Windows.Forms.Label lblAnoFabricacao;
        private System.Windows.Forms.TextBox txtAnoFabricacao;
        private System.Windows.Forms.Label lblCapacidadeCarga;
        private System.Windows.Forms.TextBox txtCapacidadeCarga;
        private System.Windows.Forms.TextBox txtTransportadora;
        private System.Windows.Forms.Button btnTransportadora;
        private System.Windows.Forms.Label lblTransportadora;
        private System.Windows.Forms.TextBox txtMarca;
        private System.Windows.Forms.Button btnMarca;
        private System.Windows.Forms.Button btnSalvar;
    }
}
