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
            this.btnRemoverParcela = new System.Windows.Forms.Button();
            this.btnFormaPagamento = new System.Windows.Forms.Button();
            this.txtFormaPagamento = new System.Windows.Forms.TextBox();
            this.lblJuros = new System.Windows.Forms.Label();
            this.txtJuros = new System.Windows.Forms.TextBox();
            this.lblMulta = new System.Windows.Forms.Label();
            this.txtMulta = new System.Windows.Forms.TextBox();
            this.lblDesconto = new System.Windows.Forms.Label();
            this.txtDesconto = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblPrazo
            // 
            this.lblPrazo.AutoSize = true;
            this.lblPrazo.Location = new System.Drawing.Point(435, 123);
            this.lblPrazo.Name = "lblPrazo";
            this.lblPrazo.Size = new System.Drawing.Size(133, 16);
            this.lblPrazo.TabIndex = 10;
            this.lblPrazo.Text = "Prazo de pagamento";
            // 
            // txtPrazoDias
            // 
            this.txtPrazoDias.Location = new System.Drawing.Point(438, 142);
            this.txtPrazoDias.Name = "txtPrazoDias";
            this.txtPrazoDias.Size = new System.Drawing.Size(100, 22);
            this.txtPrazoDias.TabIndex = 9;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(1119, 634);
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
            this.lblDescricao.Location = new System.Drawing.Point(12, 67);
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
            this.lblFormaPagamento.Location = new System.Drawing.Point(436, 188);
            this.lblFormaPagamento.Name = "lblFormaPagamento";
            this.lblFormaPagamento.Size = new System.Drawing.Size(138, 16);
            this.lblFormaPagamento.TabIndex = 17;
            this.lblFormaPagamento.Text = "Forma de Pagamento";
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
            this.listView1.Size = new System.Drawing.Size(835, 239);
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
            this.Prazo.Text = "Prazo Dias";
            this.Prazo.Width = 93;
            // 
            // Porcentagem
            // 
            this.Porcentagem.Text = "Porcentagem";
            this.Porcentagem.Width = 100;
            // 
            // FormaPagamento
            // 
            this.FormaPagamento.Text = "Forma de Pagamento";
            this.FormaPagamento.Width = 165;
            // 
            // lblPorcentagem
            // 
            this.lblPorcentagem.AutoSize = true;
            this.lblPorcentagem.Location = new System.Drawing.Point(435, 66);
            this.lblPorcentagem.Name = "lblPorcentagem";
            this.lblPorcentagem.Size = new System.Drawing.Size(88, 16);
            this.lblPorcentagem.TabIndex = 20;
            this.lblPorcentagem.Text = "Porcentagem";
            // 
            // txtPorcentagem
            // 
            this.txtPorcentagem.Location = new System.Drawing.Point(438, 85);
            this.txtPorcentagem.Name = "txtPorcentagem";
            this.txtPorcentagem.Size = new System.Drawing.Size(100, 22);
            this.txtPorcentagem.TabIndex = 19;
            // 
            // btnGerarParcelas
            // 
            this.btnGerarParcelas.Location = new System.Drawing.Point(704, 150);
            this.btnGerarParcelas.Name = "btnGerarParcelas";
            this.btnGerarParcelas.Size = new System.Drawing.Size(172, 23);
            this.btnGerarParcelas.TabIndex = 21;
            this.btnGerarParcelas.Text = "Inserir Parcela";
            this.btnGerarParcelas.UseVisualStyleBackColor = true;
            this.btnGerarParcelas.Click += new System.EventHandler(this.btnGerarParcelas_Click);
            // 
            // lblParcelas
            // 
            this.lblParcelas.AutoSize = true;
            this.lblParcelas.Location = new System.Drawing.Point(597, 10);
            this.lblParcelas.Name = "lblParcelas";
            this.lblParcelas.Size = new System.Drawing.Size(61, 16);
            this.lblParcelas.TabIndex = 22;
            this.lblParcelas.Text = "Parcelas";
            // 
            // lblNumParcela
            // 
            this.lblNumParcela.AutoSize = true;
            this.lblNumParcela.Location = new System.Drawing.Point(435, 22);
            this.lblNumParcela.Name = "lblNumParcela";
            this.lblNumParcela.Size = new System.Drawing.Size(124, 16);
            this.lblNumParcela.TabIndex = 24;
            this.lblNumParcela.Text = "Número da Parcela";
            // 
            // txtNumParcela
            // 
            this.txtNumParcela.Location = new System.Drawing.Point(438, 41);
            this.txtNumParcela.Name = "txtNumParcela";
            this.txtNumParcela.Size = new System.Drawing.Size(100, 22);
            this.txtNumParcela.TabIndex = 23;
            // 
            // btnEditarParcela
            // 
            this.btnEditarParcela.Location = new System.Drawing.Point(704, 179);
            this.btnEditarParcela.Name = "btnEditarParcela";
            this.btnEditarParcela.Size = new System.Drawing.Size(172, 23);
            this.btnEditarParcela.TabIndex = 25;
            this.btnEditarParcela.Text = "Editar Parcela";
            this.btnEditarParcela.UseVisualStyleBackColor = true;
            this.btnEditarParcela.Click += new System.EventHandler(this.btnEditarParcela_Click);
            // 
            // btnRemoverParcela
            // 
            this.btnRemoverParcela.Location = new System.Drawing.Point(704, 208);
            this.btnRemoverParcela.Name = "btnRemoverParcela";
            this.btnRemoverParcela.Size = new System.Drawing.Size(172, 23);
            this.btnRemoverParcela.TabIndex = 26;
            this.btnRemoverParcela.Text = "Remover Última Parcela";
            this.btnRemoverParcela.UseVisualStyleBackColor = true;
            this.btnRemoverParcela.Click += new System.EventHandler(this.btnRemoverParcela_Click);
            // 
            // btnFormaPagamento
            // 
            this.btnFormaPagamento.Location = new System.Drawing.Point(546, 207);
            this.btnFormaPagamento.Name = "btnFormaPagamento";
            this.btnFormaPagamento.Size = new System.Drawing.Size(150, 23);
            this.btnFormaPagamento.TabIndex = 27;
            this.btnFormaPagamento.Text = "Buscar FrmPgto";
            this.btnFormaPagamento.UseVisualStyleBackColor = true;
            this.btnFormaPagamento.Click += new System.EventHandler(this.btnFormaPagamento_Click);
            // 
            // txtFormaPagamento
            // 
            this.txtFormaPagamento.Location = new System.Drawing.Point(440, 208);
            this.txtFormaPagamento.Name = "txtFormaPagamento";
            this.txtFormaPagamento.ReadOnly = true;
            this.txtFormaPagamento.Size = new System.Drawing.Size(100, 22);
            this.txtFormaPagamento.TabIndex = 28;
            // 
            // lblJuros
            // 
            this.lblJuros.AutoSize = true;
            this.lblJuros.Location = new System.Drawing.Point(191, 8);
            this.lblJuros.Name = "lblJuros";
            this.lblJuros.Size = new System.Drawing.Size(40, 16);
            this.lblJuros.TabIndex = 30;
            this.lblJuros.Text = "Juros";
            // 
            // txtJuros
            // 
            this.txtJuros.Location = new System.Drawing.Point(191, 27);
            this.txtJuros.Name = "txtJuros";
            this.txtJuros.Size = new System.Drawing.Size(100, 22);
            this.txtJuros.TabIndex = 29;
            // 
            // lblMulta
            // 
            this.lblMulta.AutoSize = true;
            this.lblMulta.Location = new System.Drawing.Point(191, 67);
            this.lblMulta.Name = "lblMulta";
            this.lblMulta.Size = new System.Drawing.Size(39, 16);
            this.lblMulta.TabIndex = 32;
            this.lblMulta.Text = "Multa";
            // 
            // txtMulta
            // 
            this.txtMulta.Location = new System.Drawing.Point(191, 86);
            this.txtMulta.Name = "txtMulta";
            this.txtMulta.Size = new System.Drawing.Size(100, 22);
            this.txtMulta.TabIndex = 31;
            // 
            // lblDesconto
            // 
            this.lblDesconto.AutoSize = true;
            this.lblDesconto.Location = new System.Drawing.Point(191, 129);
            this.lblDesconto.Name = "lblDesconto";
            this.lblDesconto.Size = new System.Drawing.Size(65, 16);
            this.lblDesconto.TabIndex = 34;
            this.lblDesconto.Text = "Desconto";
            // 
            // txtDesconto
            // 
            this.txtDesconto.Location = new System.Drawing.Point(191, 148);
            this.txtDesconto.Name = "txtDesconto";
            this.txtDesconto.Size = new System.Drawing.Size(100, 22);
            this.txtDesconto.TabIndex = 33;
            // 
            // frmCadastroCondPgto
            // 
            //this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            //this.ClientSize = new System.Drawing.Size(1342, 681);
            this.Controls.Add(this.lblDesconto);
            this.Controls.Add(this.txtDesconto);
            this.Controls.Add(this.lblMulta);
            this.Controls.Add(this.txtMulta);
            this.Controls.Add(this.lblJuros);
            this.Controls.Add(this.txtJuros);
            this.Controls.Add(this.txtFormaPagamento);
            this.Controls.Add(this.btnFormaPagamento);
            this.Controls.Add(this.btnRemoverParcela);
            this.Controls.Add(this.btnEditarParcela);
            this.Controls.Add(this.lblNumParcela);
            this.Controls.Add(this.txtNumParcela);
            this.Controls.Add(this.lblParcelas);
            this.Controls.Add(this.btnGerarParcelas);
            this.Controls.Add(this.lblPorcentagem);
            this.Controls.Add(this.txtPorcentagem);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.lblFormaPagamento);
            this.Controls.Add(this.lblQtdParcelas);
            this.Controls.Add(this.txtQtdParcelas);
            this.Controls.Add(this.lblDescricao);
            this.Controls.Add(this.txtDescricao);
            this.Controls.Add(this.lblCodigo);
            this.Controls.Add(this.lblPrazo);
            this.Controls.Add(this.txtPrazoDias);
            this.Controls.Add(this.btnSalvar);
            this.Name = "frmCadastroCondPgto";
            this.Text = "Cadastro Condições de Pagamento";
            this.Controls.SetChildIndex(this.lblDataModificacao, 0);
            this.Controls.SetChildIndex(this.chkInativo, 0);
            this.Controls.SetChildIndex(this.lblDataCriacao, 0);
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
            this.Controls.SetChildIndex(this.lblFormaPagamento, 0);
            this.Controls.SetChildIndex(this.listView1, 0);
            this.Controls.SetChildIndex(this.txtPorcentagem, 0);
            this.Controls.SetChildIndex(this.lblPorcentagem, 0);
            this.Controls.SetChildIndex(this.btnGerarParcelas, 0);
            this.Controls.SetChildIndex(this.lblParcelas, 0);
            this.Controls.SetChildIndex(this.txtNumParcela, 0);
            this.Controls.SetChildIndex(this.lblNumParcela, 0);
            this.Controls.SetChildIndex(this.btnEditarParcela, 0);
            this.Controls.SetChildIndex(this.btnRemoverParcela, 0);
            this.Controls.SetChildIndex(this.btnFormaPagamento, 0);
            this.Controls.SetChildIndex(this.txtFormaPagamento, 0);
            this.Controls.SetChildIndex(this.txtJuros, 0);
            this.Controls.SetChildIndex(this.lblJuros, 0);
            this.Controls.SetChildIndex(this.txtMulta, 0);
            this.Controls.SetChildIndex(this.lblMulta, 0);
            this.Controls.SetChildIndex(this.txtDesconto, 0);
            this.Controls.SetChildIndex(this.lblDesconto, 0);
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
        private System.Windows.Forms.Button btnRemoverParcela;
        private System.Windows.Forms.Button btnFormaPagamento;
        private System.Windows.Forms.TextBox txtFormaPagamento;
        private System.Windows.Forms.Label lblJuros;
        private System.Windows.Forms.TextBox txtJuros;
        private System.Windows.Forms.Label lblMulta;
        private System.Windows.Forms.TextBox txtMulta;
        private System.Windows.Forms.Label lblDesconto;
        private System.Windows.Forms.TextBox txtDesconto;
    }
}
