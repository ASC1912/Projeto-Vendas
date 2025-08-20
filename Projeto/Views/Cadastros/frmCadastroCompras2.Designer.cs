namespace Projeto.Views.Cadastros
{
    partial class frmCadastroCompras2
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
            this.btnSair = new System.Windows.Forms.Button();
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblDesconto = new System.Windows.Forms.Label();
            this.txtDesconto = new System.Windows.Forms.TextBox();
            this.lblMulta = new System.Windows.Forms.Label();
            this.txtMulta = new System.Windows.Forms.TextBox();
            this.txtSerie = new System.Windows.Forms.TextBox();
            this.txtCondPgto = new System.Windows.Forms.TextBox();
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.lblNumParcela = new System.Windows.Forms.Label();
            this.txtNumParcela = new System.Windows.Forms.TextBox();
            this.lblPorcentagem = new System.Windows.Forms.Label();
            this.txtPorcentagem = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.NumParcela = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Prazo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Porcentagem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FormaPagamento = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblFormaPagamento = new System.Windows.Forms.Label();
            this.lblPrazo = new System.Windows.Forms.Label();
            this.txtPrazoDias = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnSair
            // 
            this.btnSair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSair.Location = new System.Drawing.Point(1224, 625);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(80, 35);
            this.btnSair.TabIndex = 3;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = true;
            // 
            // listView2
            // 
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listView2.FullRowSelect = true;
            this.listView2.GridLines = true;
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(692, 37);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(540, 239);
            this.listView2.TabIndex = 99;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Num Parcela";
            this.columnHeader1.Width = 99;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Prazo Dias";
            this.columnHeader2.Width = 93;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Porcentagem";
            this.columnHeader3.Width = 100;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Forma de Pagamento";
            this.columnHeader4.Width = 165;
            // 
            // lblDesconto
            // 
            this.lblDesconto.AutoSize = true;
            this.lblDesconto.Location = new System.Drawing.Point(284, 158);
            this.lblDesconto.Name = "lblDesconto";
            this.lblDesconto.Size = new System.Drawing.Size(65, 16);
            this.lblDesconto.TabIndex = 98;
            this.lblDesconto.Text = "Desconto";
            // 
            // txtDesconto
            // 
            this.txtDesconto.Location = new System.Drawing.Point(284, 177);
            this.txtDesconto.Name = "txtDesconto";
            this.txtDesconto.Size = new System.Drawing.Size(100, 22);
            this.txtDesconto.TabIndex = 86;
            // 
            // lblMulta
            // 
            this.lblMulta.AutoSize = true;
            this.lblMulta.Location = new System.Drawing.Point(284, 96);
            this.lblMulta.Name = "lblMulta";
            this.lblMulta.Size = new System.Drawing.Size(39, 16);
            this.lblMulta.TabIndex = 97;
            this.lblMulta.Text = "Multa";
            // 
            // txtMulta
            // 
            this.txtMulta.Location = new System.Drawing.Point(284, 115);
            this.txtMulta.Name = "txtMulta";
            this.txtMulta.Size = new System.Drawing.Size(100, 22);
            this.txtMulta.TabIndex = 85;
            // 
            // txtSerie
            // 
            this.txtSerie.Location = new System.Drawing.Point(284, 56);
            this.txtSerie.Name = "txtSerie";
            this.txtSerie.Size = new System.Drawing.Size(100, 22);
            this.txtSerie.TabIndex = 84;
            // 
            // txtCondPgto
            // 
            this.txtCondPgto.Location = new System.Drawing.Point(58, 248);
            this.txtCondPgto.Name = "txtCondPgto";
            this.txtCondPgto.Size = new System.Drawing.Size(200, 22);
            this.txtCondPgto.TabIndex = 89;
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Location = new System.Drawing.Point(264, 247);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(150, 23);
            this.btnAdicionar.TabIndex = 90;
            this.btnAdicionar.Text = "Adicionar";
            this.btnAdicionar.UseVisualStyleBackColor = true;
            // 
            // lblNumParcela
            // 
            this.lblNumParcela.AutoSize = true;
            this.lblNumParcela.Location = new System.Drawing.Point(528, 51);
            this.lblNumParcela.Name = "lblNumParcela";
            this.lblNumParcela.Size = new System.Drawing.Size(124, 16);
            this.lblNumParcela.TabIndex = 96;
            this.lblNumParcela.Text = "Número da Parcela";
            // 
            // txtNumParcela
            // 
            this.txtNumParcela.Location = new System.Drawing.Point(531, 70);
            this.txtNumParcela.Name = "txtNumParcela";
            this.txtNumParcela.Size = new System.Drawing.Size(100, 22);
            this.txtNumParcela.TabIndex = 95;
            // 
            // lblPorcentagem
            // 
            this.lblPorcentagem.AutoSize = true;
            this.lblPorcentagem.Location = new System.Drawing.Point(528, 95);
            this.lblPorcentagem.Name = "lblPorcentagem";
            this.lblPorcentagem.Size = new System.Drawing.Size(93, 16);
            this.lblPorcentagem.TabIndex = 94;
            this.lblPorcentagem.Text = "Porcentagem*";
            // 
            // txtPorcentagem
            // 
            this.txtPorcentagem.Location = new System.Drawing.Point(531, 114);
            this.txtPorcentagem.Name = "txtPorcentagem";
            this.txtPorcentagem.Size = new System.Drawing.Size(100, 22);
            this.txtPorcentagem.TabIndex = 87;
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
            this.listView1.Location = new System.Drawing.Point(55, 285);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(835, 239);
            this.listView1.TabIndex = 93;
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
            // lblFormaPagamento
            // 
            this.lblFormaPagamento.AutoSize = true;
            this.lblFormaPagamento.Location = new System.Drawing.Point(54, 228);
            this.lblFormaPagamento.Name = "lblFormaPagamento";
            this.lblFormaPagamento.Size = new System.Drawing.Size(162, 16);
            this.lblFormaPagamento.TabIndex = 92;
            this.lblFormaPagamento.Text = "Condição de Pagamento*";
            // 
            // lblPrazo
            // 
            this.lblPrazo.AutoSize = true;
            this.lblPrazo.Location = new System.Drawing.Point(528, 152);
            this.lblPrazo.Name = "lblPrazo";
            this.lblPrazo.Size = new System.Drawing.Size(138, 16);
            this.lblPrazo.TabIndex = 91;
            this.lblPrazo.Text = "Prazo de pagamento*";
            // 
            // txtPrazoDias
            // 
            this.txtPrazoDias.Location = new System.Drawing.Point(531, 171);
            this.txtPrazoDias.Name = "txtPrazoDias";
            this.txtPrazoDias.Size = new System.Drawing.Size(100, 22);
            this.txtPrazoDias.TabIndex = 88;
            // 
            // frmCadastroCompras2
            // 
            //this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1342, 681);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.lblDesconto);
            this.Controls.Add(this.txtDesconto);
            this.Controls.Add(this.lblMulta);
            this.Controls.Add(this.txtMulta);
            this.Controls.Add(this.txtSerie);
            this.Controls.Add(this.txtCondPgto);
            this.Controls.Add(this.btnAdicionar);
            this.Controls.Add(this.lblNumParcela);
            this.Controls.Add(this.txtNumParcela);
            this.Controls.Add(this.lblPorcentagem);
            this.Controls.Add(this.txtPorcentagem);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.lblFormaPagamento);
            this.Controls.Add(this.lblPrazo);
            this.Controls.Add(this.txtPrazoDias);
            this.Controls.Add(this.btnSair);
            this.Name = "frmCadastroCompras2";
            this.Text = "frmCadastroCompras2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Button btnSair;
        protected System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label lblDesconto;
        private System.Windows.Forms.TextBox txtDesconto;
        private System.Windows.Forms.Label lblMulta;
        private System.Windows.Forms.TextBox txtMulta;
        private System.Windows.Forms.TextBox txtSerie;
        private System.Windows.Forms.TextBox txtCondPgto;
        private System.Windows.Forms.Button btnAdicionar;
        private System.Windows.Forms.Label lblNumParcela;
        private System.Windows.Forms.TextBox txtNumParcela;
        private System.Windows.Forms.Label lblPorcentagem;
        private System.Windows.Forms.TextBox txtPorcentagem;
        protected System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader NumParcela;
        private System.Windows.Forms.ColumnHeader Prazo;
        private System.Windows.Forms.ColumnHeader Porcentagem;
        private System.Windows.Forms.ColumnHeader FormaPagamento;
        private System.Windows.Forms.Label lblFormaPagamento;
        private System.Windows.Forms.Label lblPrazo;
        private System.Windows.Forms.TextBox txtPrazoDias;
    }
}