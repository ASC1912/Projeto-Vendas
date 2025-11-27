namespace Projeto.Views.Consultas
{
    partial class frmConsultaContasAReceber
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
            this.Modelo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Serie = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NumeroNota = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NumeroParcela = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IdCliente = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Cliente = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ValorParcela = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FormaPagamento = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DataEmissao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DataVencimento = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Ativo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DataPagamento = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ValorPago = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chkCancelada = new System.Windows.Forms.CheckBox();
            this.chkAberta = new System.Windows.Forms.CheckBox();
            this.chkPaga = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Modelo,
            this.Serie,
            this.NumeroNota,
            this.NumeroParcela,
            this.IdCliente,
            this.Cliente,
            this.ValorParcela,
            this.FormaPagamento,
            this.DataEmissao,
            this.DataVencimento,
            this.Ativo,
            this.Status,
            this.DataPagamento,
            this.ValorPago});
            // 
            // Modelo
            // 
            this.Modelo.Text = "Modelo";
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
            this.NumeroNota.Width = 130;
            // 
            // NumeroParcela
            // 
            this.NumeroParcela.Text = "Número da Parcela";
            this.NumeroParcela.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumeroParcela.Width = 130;
            // 
            // IdCliente
            // 
            this.IdCliente.Text = "IdCliente";
            this.IdCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.IdCliente.Width = 100;
            // 
            // Cliente
            // 
            this.Cliente.Text = "Cliente";
            this.Cliente.Width = 200;
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
            this.DataEmissao.Width = 130;
            // 
            // DataVencimento
            // 
            this.DataVencimento.Text = "Data de Vencimento";
            this.DataVencimento.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DataVencimento.Width = 150;
            // 
            // Ativo
            // 
            this.Ativo.Text = "Ativo";
            this.Ativo.Width = 70;
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
            // chkCancelada
            // 
            this.chkCancelada.AutoSize = true;
            this.chkCancelada.Checked = true;
            this.chkCancelada.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCancelada.Location = new System.Drawing.Point(824, 55);
            this.chkCancelada.Name = "chkCancelada";
            this.chkCancelada.Size = new System.Drawing.Size(95, 20);
            this.chkCancelada.TabIndex = 56;
            this.chkCancelada.Text = "Cancelada";
            this.chkCancelada.UseVisualStyleBackColor = true;
            // 
            // chkAberta
            // 
            this.chkAberta.AutoSize = true;
            this.chkAberta.Checked = true;
            this.chkAberta.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAberta.Location = new System.Drawing.Point(673, 55);
            this.chkAberta.Name = "chkAberta";
            this.chkAberta.Size = new System.Drawing.Size(69, 20);
            this.chkAberta.TabIndex = 55;
            this.chkAberta.Text = "Aberta";
            this.chkAberta.UseVisualStyleBackColor = true;
            // 
            // chkPaga
            // 
            this.chkPaga.AutoSize = true;
            this.chkPaga.Checked = true;
            this.chkPaga.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPaga.Location = new System.Drawing.Point(519, 55);
            this.chkPaga.Name = "chkPaga";
            this.chkPaga.Size = new System.Drawing.Size(62, 20);
            this.chkPaga.TabIndex = 54;
            this.chkPaga.Text = "Paga";
            this.chkPaga.UseVisualStyleBackColor = true;
            // 
            // frmConsultaContasAReceber
            // 
            //this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            //this.ClientSize = new System.Drawing.Size(1342, 681);
            this.Controls.Add(this.chkCancelada);
            this.Controls.Add(this.chkAberta);
            this.Controls.Add(this.chkPaga);
            this.Name = "frmConsultaContasAReceber";
            this.Load += new System.EventHandler(this.frmConsultaContasAReceber_Load);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColumnHeader Modelo;
        private System.Windows.Forms.ColumnHeader Serie;
        private System.Windows.Forms.ColumnHeader NumeroNota;
        private System.Windows.Forms.ColumnHeader NumeroParcela;
        private System.Windows.Forms.ColumnHeader IdCliente;
        private System.Windows.Forms.ColumnHeader Cliente;
        private System.Windows.Forms.ColumnHeader ValorParcela;
        private System.Windows.Forms.ColumnHeader FormaPagamento;
        private System.Windows.Forms.ColumnHeader DataEmissao;
        private System.Windows.Forms.ColumnHeader DataVencimento;
        private System.Windows.Forms.ColumnHeader Ativo;
        private System.Windows.Forms.ColumnHeader Status;
        private System.Windows.Forms.ColumnHeader DataPagamento;
        private System.Windows.Forms.ColumnHeader ValorPago;
        private System.Windows.Forms.CheckBox chkCancelada;
        private System.Windows.Forms.CheckBox chkAberta;
        private System.Windows.Forms.CheckBox chkPaga;
    }
}
