namespace Projeto
{
    partial class frmCadastroCondPgto
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
            this.lblPrazo = new System.Windows.Forms.Label();
            this.txtPrazoDias = new System.Windows.Forms.TextBox();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.lblDescricao = new System.Windows.Forms.Label();
            this.txtDescricao = new System.Windows.Forms.TextBox();
            this.lblQtdParcelas = new System.Windows.Forms.Label();
            this.txtQtdParcelas = new System.Windows.Forms.TextBox();
            this.lblFormaPagamento = new System.Windows.Forms.Label();
            this.cbFormaPagamento = new System.Windows.Forms.ComboBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.NumParcela = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Prazo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Porcentagem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FormaPagamento = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblPorcentagem = new System.Windows.Forms.Label();
            this.txtPorcentagem = new System.Windows.Forms.TextBox();
            this.btnGerarParcelas = new System.Windows.Forms.Button();
            this.lblParcelas = new System.Windows.Forms.Label();
            this.lblNumParcela = new System.Windows.Forms.Label();
            this.txtNumParcela = new System.Windows.Forms.TextBox();
            this.btnEditarParcela = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSair
            // 
            this.btnSair.Location = new System.Drawing.Point(775, 507);
            // 
            // lblPrazo
            // 
            this.lblPrazo.AutoSize = true;
            this.lblPrazo.Location = new System.Drawing.Point(366, 119);
            this.lblPrazo.Name = "lblPrazo";
            this.lblPrazo.Size = new System.Drawing.Size(133, 16);
            this.lblPrazo.TabIndex = 10;
            this.lblPrazo.Text = "Prazo de pagamento";
            // 
            // txtPrazoDias
            // 
            this.txtPrazoDias.Location = new System.Drawing.Point(369, 138);
            this.txtPrazoDias.Name = "txtPrazoDias";
            this.txtPrazoDias.Size = new System.Drawing.Size(100, 22);
            this.txtPrazoDias.TabIndex = 9;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(689, 507);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(80, 35);
            this.btnSalvar.TabIndex = 8;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Location = new System.Drawing.Point(9, 8);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(51, 16);
            this.lblCodigo.TabIndex = 11;
            this.lblCodigo.Text = "Código";
            // 
            // lblDescricao
            // 
            this.lblDescricao.AutoSize = true;
            this.lblDescricao.Location = new System.Drawing.Point(9, 67);
            this.lblDescricao.Name = "lblDescricao";
            this.lblDescricao.Size = new System.Drawing.Size(69, 16);
            this.lblDescricao.TabIndex = 13;
            this.lblDescricao.Text = "Descrição";
            // 
            // txtDescricao
            // 
            this.txtDescricao.Location = new System.Drawing.Point(12, 86);
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Size = new System.Drawing.Size(100, 22);
            this.txtDescricao.TabIndex = 12;
            // 
            // lblQtdParcelas
            // 
            this.lblQtdParcelas.AutoSize = true;
            this.lblQtdParcelas.Location = new System.Drawing.Point(9, 131);
            this.lblQtdParcelas.Name = "lblQtdParcelas";
            this.lblQtdParcelas.Size = new System.Drawing.Size(153, 16);
            this.lblQtdParcelas.TabIndex = 15;
            this.lblQtdParcelas.Text = "Quantidade de Parcelas";
            // 
            // txtQtdParcelas
            // 
            this.txtQtdParcelas.Location = new System.Drawing.Point(12, 150);
            this.txtQtdParcelas.Name = "txtQtdParcelas";
            this.txtQtdParcelas.Size = new System.Drawing.Size(100, 22);
            this.txtQtdParcelas.TabIndex = 14;
            // 
            // lblFormaPagamento
            // 
            this.lblFormaPagamento.AutoSize = true;
            this.lblFormaPagamento.Location = new System.Drawing.Point(367, 184);
            this.lblFormaPagamento.Name = "lblFormaPagamento";
            this.lblFormaPagamento.Size = new System.Drawing.Size(138, 16);
            this.lblFormaPagamento.TabIndex = 17;
            this.lblFormaPagamento.Text = "Forma de Pagamento";
            // 
            // cbFormaPagamento
            // 
            this.cbFormaPagamento.FormattingEnabled = true;
            this.cbFormaPagamento.Location = new System.Drawing.Point(370, 203);
            this.cbFormaPagamento.Name = "cbFormaPagamento";
            this.cbFormaPagamento.Size = new System.Drawing.Size(121, 24);
            this.cbFormaPagamento.TabIndex = 16;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NumParcela,
            this.Prazo,
            this.Porcentagem,
            this.FormaPagamento});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(41, 256);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(624, 239);
            this.listView1.TabIndex = 18;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // NumParcela
            // 
            this.NumParcela.Text = "Num Parcela";
            this.NumParcela.Width = 99;
            // 
            // Prazo
            // 
            this.Prazo.Text = "Prazo";
            // 
            // Porcentagem
            // 
            this.Porcentagem.Text = "Porcentagem";
            this.Porcentagem.Width = 91;
            // 
            // FormaPagamento
            // 
            this.FormaPagamento.Text = "Forma de Pagamento";
            this.FormaPagamento.Width = 165;
            // 
            // lblPorcentagem
            // 
            this.lblPorcentagem.AutoSize = true;
            this.lblPorcentagem.Location = new System.Drawing.Point(366, 62);
            this.lblPorcentagem.Name = "lblPorcentagem";
            this.lblPorcentagem.Size = new System.Drawing.Size(88, 16);
            this.lblPorcentagem.TabIndex = 20;
            this.lblPorcentagem.Text = "Porcentagem";
            // 
            // txtPorcentagem
            // 
            this.txtPorcentagem.Location = new System.Drawing.Point(369, 81);
            this.txtPorcentagem.Name = "txtPorcentagem";
            this.txtPorcentagem.Size = new System.Drawing.Size(100, 22);
            this.txtPorcentagem.TabIndex = 19;
            // 
            // btnGerarParcelas
            // 
            this.btnGerarParcelas.Location = new System.Drawing.Point(546, 147);
            this.btnGerarParcelas.Name = "btnGerarParcelas";
            this.btnGerarParcelas.Size = new System.Drawing.Size(134, 23);
            this.btnGerarParcelas.TabIndex = 21;
            this.btnGerarParcelas.Text = "Gerar Parcelas";
            this.btnGerarParcelas.UseVisualStyleBackColor = true;
            this.btnGerarParcelas.Click += new System.EventHandler(this.btnGerarParcelas_Click);
            // 
            // lblParcelas
            // 
            this.lblParcelas.AutoSize = true;
            this.lblParcelas.Location = new System.Drawing.Point(273, 18);
            this.lblParcelas.Name = "lblParcelas";
            this.lblParcelas.Size = new System.Drawing.Size(61, 16);
            this.lblParcelas.TabIndex = 22;
            this.lblParcelas.Text = "Parcelas";
            // 
            // lblNumParcela
            // 
            this.lblNumParcela.AutoSize = true;
            this.lblNumParcela.Location = new System.Drawing.Point(366, 18);
            this.lblNumParcela.Name = "lblNumParcela";
            this.lblNumParcela.Size = new System.Drawing.Size(124, 16);
            this.lblNumParcela.TabIndex = 24;
            this.lblNumParcela.Text = "Número da Parcela";
            // 
            // txtNumParcela
            // 
            this.txtNumParcela.Location = new System.Drawing.Point(369, 37);
            this.txtNumParcela.Name = "txtNumParcela";
            this.txtNumParcela.Size = new System.Drawing.Size(100, 22);
            this.txtNumParcela.TabIndex = 23;
            // 
            // btnEditarParcela
            // 
            this.btnEditarParcela.Location = new System.Drawing.Point(546, 176);
            this.btnEditarParcela.Name = "btnEditarParcela";
            this.btnEditarParcela.Size = new System.Drawing.Size(134, 23);
            this.btnEditarParcela.TabIndex = 25;
            this.btnEditarParcela.Text = "Editar Parcela";
            this.btnEditarParcela.UseVisualStyleBackColor = true;
            this.btnEditarParcela.Click += new System.EventHandler(this.btnEditarParcela_Click);
            // 
            // frmCadastroCondPgto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(876, 554);
            this.Controls.Add(this.btnEditarParcela);
            this.Controls.Add(this.lblNumParcela);
            this.Controls.Add(this.txtNumParcela);
            this.Controls.Add(this.lblParcelas);
            this.Controls.Add(this.btnGerarParcelas);
            this.Controls.Add(this.lblPorcentagem);
            this.Controls.Add(this.txtPorcentagem);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.lblFormaPagamento);
            this.Controls.Add(this.cbFormaPagamento);
            this.Controls.Add(this.lblQtdParcelas);
            this.Controls.Add(this.txtQtdParcelas);
            this.Controls.Add(this.lblDescricao);
            this.Controls.Add(this.txtDescricao);
            this.Controls.Add(this.lblCodigo);
            this.Controls.Add(this.lblPrazo);
            this.Controls.Add(this.txtPrazoDias);
            this.Controls.Add(this.btnSalvar);
            this.Name = "frmCadastroCondPgto";
            this.Load += new System.EventHandler(this.frmCadastroCondPgto_Load);
            this.Controls.SetChildIndex(this.btnSair, 0);
            this.Controls.SetChildIndex(this.txtCodigo, 0);
            this.Controls.SetChildIndex(this.btnSalvar, 0);
            this.Controls.SetChildIndex(this.txtPrazoDias, 0);
            this.Controls.SetChildIndex(this.lblPrazo, 0);
            this.Controls.SetChildIndex(this.lblCodigo, 0);
            this.Controls.SetChildIndex(this.txtDescricao, 0);
            this.Controls.SetChildIndex(this.lblDescricao, 0);
            this.Controls.SetChildIndex(this.txtQtdParcelas, 0);
            this.Controls.SetChildIndex(this.lblQtdParcelas, 0);
            this.Controls.SetChildIndex(this.cbFormaPagamento, 0);
            this.Controls.SetChildIndex(this.lblFormaPagamento, 0);
            this.Controls.SetChildIndex(this.listView1, 0);
            this.Controls.SetChildIndex(this.txtPorcentagem, 0);
            this.Controls.SetChildIndex(this.lblPorcentagem, 0);
            this.Controls.SetChildIndex(this.btnGerarParcelas, 0);
            this.Controls.SetChildIndex(this.lblParcelas, 0);
            this.Controls.SetChildIndex(this.txtNumParcela, 0);
            this.Controls.SetChildIndex(this.lblNumParcela, 0);
            this.Controls.SetChildIndex(this.btnEditarParcela, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPrazo;
        private System.Windows.Forms.TextBox txtPrazoDias;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.Label lblDescricao;
        private System.Windows.Forms.TextBox txtDescricao;
        private System.Windows.Forms.Label lblQtdParcelas;
        private System.Windows.Forms.TextBox txtQtdParcelas;
        private System.Windows.Forms.Label lblFormaPagamento;
        private System.Windows.Forms.ComboBox cbFormaPagamento;
        protected System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label lblPorcentagem;
        private System.Windows.Forms.TextBox txtPorcentagem;
        private System.Windows.Forms.ColumnHeader NumParcela;
        private System.Windows.Forms.ColumnHeader Prazo;
        private System.Windows.Forms.ColumnHeader Porcentagem;
        private System.Windows.Forms.ColumnHeader FormaPagamento;
        private System.Windows.Forms.Button btnGerarParcelas;
        private System.Windows.Forms.Label lblParcelas;
        private System.Windows.Forms.Label lblNumParcela;
        private System.Windows.Forms.TextBox txtNumParcela;
        private System.Windows.Forms.Button btnEditarParcela;
    }
}
