namespace Projeto.Views.Consultas
{
    partial class frmConsultasContasAPagar
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
            this.NumeroParcela = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Modelo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Serie = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NumeroNota = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Fornecedor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ValorParcela = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FormaPagamento = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DataEmissao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DataVencimento = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Ativo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Descricao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DataPagamento = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ValorPago = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NumeroParcela,
            this.Descricao,
            this.Modelo,
            this.Serie,
            this.NumeroNota,
            this.Fornecedor,
            this.ValorParcela,
            this.FormaPagamento,
            this.DataEmissao,
            this.DataVencimento,
            this.Ativo,
            this.Status,
            this.DataPagamento,
            this.ValorPago});
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // btnDeletar
            // 
            this.btnDeletar.Click += new System.EventHandler(this.btnDeletar_Click);
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnIncluir
            // 
            this.btnIncluir.Click += new System.EventHandler(this.btnIncluir_Click);
            // 
            // NumeroParcela
            // 
            this.NumeroParcela.Text = "Número da Parcela";
            this.NumeroParcela.Width = 130;
            // 
            // Modelo
            // 
            this.Modelo.Text = "Modelo";
            this.Modelo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Serie
            // 
            this.Serie.Text = "Série";
            this.Serie.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // NumeroNota
            // 
            this.NumeroNota.Text = "Número da Nota";
            this.NumeroNota.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumeroNota.Width = 120;
            // 
            // Fornecedor
            // 
            this.Fornecedor.Text = "Fornecedor";
            this.Fornecedor.Width = 200;
            // 
            // ValorParcela
            // 
            this.ValorParcela.Text = "Valor da Parcela";
            this.ValorParcela.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ValorParcela.Width = 130;
            // 
            // FormaPagamento
            // 
            this.FormaPagamento.Text = "Forma de Pagamento";
            this.FormaPagamento.Width = 150;
            // 
            // DataEmissao
            // 
            this.DataEmissao.Text = "Data de Emissão";
            this.DataEmissao.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DataEmissao.Width = 140;
            // 
            // DataVencimento
            // 
            this.DataVencimento.Text = "Data de Vencimento";
            this.DataVencimento.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DataVencimento.Width = 140;
            // 
            // Ativo
            // 
            this.Ativo.Text = "Ativo";
            // 
            // Descricao
            // 
            this.Descricao.Text = "Descrição";
            this.Descricao.Width = 200;
            // 
            // Status
            // 
            this.Status.Text = "Status";
            // 
            // DataPagamento
            // 
            this.DataPagamento.Text = "Data de Pagamento";
            this.DataPagamento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.DataPagamento.Width = 140;
            // 
            // ValorPago
            // 
            this.ValorPago.Text = "Valor Pago";
            this.ValorPago.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ValorPago.Width = 130;
            // 
            // cbStatus
            // 
            this.cbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Items.AddRange(new object[] {
            "Aberta",
            "Paga",
            "Cancelada",
            "Todos"});
            this.cbStatus.Location = new System.Drawing.Point(1190, 55);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(121, 24);
            this.cbStatus.TabIndex = 46;
            this.cbStatus.SelectedIndexChanged += new System.EventHandler(this.cbStatus_SelectedIndexChanged);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(1187, 39);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(44, 16);
            this.lblStatus.TabIndex = 47;
            this.lblStatus.Text = "Status";
            // 
            // frmConsultasContasAPagar
            // 
            //this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            //this.ClientSize = new System.Drawing.Size(1342, 681);
            this.Controls.Add(this.cbStatus);
            this.Controls.Add(this.lblStatus);
            this.Name = "frmConsultasContasAPagar";
            this.Text = "Consulta Contas A Pagar";
            this.Load += new System.EventHandler(this.frmConsultasContasAPagar_Load);
            this.Controls.SetChildIndex(this.listView1, 0);
            this.Controls.SetChildIndex(this.txtPesquisar, 0);
            this.Controls.SetChildIndex(this.btnSair, 0);
            this.Controls.SetChildIndex(this.btnPesquisar, 0);
            this.Controls.SetChildIndex(this.btnDeletar, 0);
            this.Controls.SetChildIndex(this.btnEditar, 0);
            this.Controls.SetChildIndex(this.btnIncluir, 0);
            this.Controls.SetChildIndex(this.lblStatus, 0);
            this.Controls.SetChildIndex(this.cbStatus, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColumnHeader NumeroParcela;
        private System.Windows.Forms.ColumnHeader Modelo;
        private System.Windows.Forms.ColumnHeader Serie;
        private System.Windows.Forms.ColumnHeader NumeroNota;
        private System.Windows.Forms.ColumnHeader Fornecedor;
        private System.Windows.Forms.ColumnHeader ValorParcela;
        private System.Windows.Forms.ColumnHeader FormaPagamento;
        private System.Windows.Forms.ColumnHeader DataEmissao;
        private System.Windows.Forms.ColumnHeader DataVencimento;
        private System.Windows.Forms.ColumnHeader Ativo;
        private System.Windows.Forms.ColumnHeader Descricao;
        private System.Windows.Forms.ColumnHeader Status;
        private System.Windows.Forms.ColumnHeader DataPagamento;
        private System.Windows.Forms.ColumnHeader ValorPago;
        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.Label lblStatus;
    }
}
