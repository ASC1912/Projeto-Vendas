namespace Projeto.Views.Consultas
{
    partial class frmConsultaContasAPagar
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
            this.Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DataPagamento = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ValorPago = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chkPaga = new System.Windows.Forms.CheckBox();
            this.chkAberta = new System.Windows.Forms.CheckBox();
            this.chkCancelada = new System.Windows.Forms.CheckBox();
            this.IdFornecedor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnVisualizar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.Modelo,
            this.Serie,
            this.NumeroNota,
            this.NumeroParcela,
            this.IdFornecedor,
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
            this.NumeroParcela.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            // Status
            // 
            this.Status.Text = "Status";
            this.Status.Width = 70;
            // 
            // DataPagamento
            // 
            this.DataPagamento.Text = "Data de Pagamento";
            this.DataPagamento.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DataPagamento.Width = 140;
            // 
            // ValorPago
            // 
            this.ValorPago.Text = "Valor Pago";
            this.ValorPago.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ValorPago.Width = 130;
            // 
            // chkPaga
            // 
            this.chkPaga.AutoSize = true;
            this.chkPaga.Checked = true;
            this.chkPaga.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPaga.Location = new System.Drawing.Point(555, 57);
            this.chkPaga.Name = "chkPaga";
            this.chkPaga.Size = new System.Drawing.Size(62, 20);
            this.chkPaga.TabIndex = 51;
            this.chkPaga.Text = "Paga";
            this.chkPaga.UseVisualStyleBackColor = true;
            this.chkPaga.CheckedChanged += new System.EventHandler(this.chkPaga_CheckedChanged);
            // 
            // chkAberta
            // 
            this.chkAberta.AutoSize = true;
            this.chkAberta.Checked = true;
            this.chkAberta.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAberta.Location = new System.Drawing.Point(709, 57);
            this.chkAberta.Name = "chkAberta";
            this.chkAberta.Size = new System.Drawing.Size(69, 20);
            this.chkAberta.TabIndex = 52;
            this.chkAberta.Text = "Aberta";
            this.chkAberta.UseVisualStyleBackColor = true;
            this.chkAberta.CheckedChanged += new System.EventHandler(this.chkAberta_CheckedChanged);
            // 
            // chkCancelada
            // 
            this.chkCancelada.AutoSize = true;
            this.chkCancelada.Checked = true;
            this.chkCancelada.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCancelada.Location = new System.Drawing.Point(860, 57);
            this.chkCancelada.Name = "chkCancelada";
            this.chkCancelada.Size = new System.Drawing.Size(95, 20);
            this.chkCancelada.TabIndex = 53;
            this.chkCancelada.Text = "Cancelada";
            this.chkCancelada.UseVisualStyleBackColor = true;
            this.chkCancelada.CheckedChanged += new System.EventHandler(this.chkCancelada_CheckedChanged);
            // 
            // IdFornecedor
            // 
            this.IdFornecedor.Text = "IdFornecedor";
            this.IdFornecedor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.IdFornecedor.Width = 100;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 0;
            // 
            // btnVisualizar
            // 
            this.btnVisualizar.Location = new System.Drawing.Point(844, 644);
            this.btnVisualizar.Name = "btnVisualizar";
            this.btnVisualizar.Size = new System.Drawing.Size(92, 23);
            this.btnVisualizar.TabIndex = 54;
            this.btnVisualizar.Text = "Visualizar";
            this.btnVisualizar.UseVisualStyleBackColor = true;
            this.btnVisualizar.Click += new System.EventHandler(this.btnVisualizar_Click);
            // 
            // frmConsultaContasAPagar
            // 
            //this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            //this.ClientSize = new System.Drawing.Size(1342, 681);
            this.Controls.Add(this.btnVisualizar);
            this.Controls.Add(this.chkCancelada);
            this.Controls.Add(this.chkAberta);
            this.Controls.Add(this.chkPaga);
            this.Name = "frmConsultaContasAPagar";
            this.Text = "Consulta Contas A Pagar";
            this.Load += new System.EventHandler(this.frmConsultasContasAPagar_Load);
            this.Controls.SetChildIndex(this.listView1, 0);
            this.Controls.SetChildIndex(this.txtPesquisar, 0);
            this.Controls.SetChildIndex(this.btnSair, 0);
            this.Controls.SetChildIndex(this.btnPesquisar, 0);
            this.Controls.SetChildIndex(this.btnDeletar, 0);
            this.Controls.SetChildIndex(this.btnEditar, 0);
            this.Controls.SetChildIndex(this.btnIncluir, 0);
            this.Controls.SetChildIndex(this.chkPaga, 0);
            this.Controls.SetChildIndex(this.chkAberta, 0);
            this.Controls.SetChildIndex(this.chkCancelada, 0);
            this.Controls.SetChildIndex(this.btnVisualizar, 0);
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
        private System.Windows.Forms.ColumnHeader Status;
        private System.Windows.Forms.ColumnHeader DataPagamento;
        private System.Windows.Forms.ColumnHeader ValorPago;
        private System.Windows.Forms.CheckBox chkPaga;
        private System.Windows.Forms.CheckBox chkAberta;
        private System.Windows.Forms.CheckBox chkCancelada;
        private System.Windows.Forms.ColumnHeader IdFornecedor;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        protected System.Windows.Forms.Button btnVisualizar;
    }
}
