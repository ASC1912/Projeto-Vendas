namespace Projeto.Views.Consultas
{
    partial class frmConsultaVenda
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
            this.Numero = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Cliente = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DataEmissao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DataSaida = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ValorTotal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CondicaoPagamento = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IdCliente = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IdCondPgto = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.Modelo,
            this.Serie,
            this.Numero,
            this.IdCliente,
            this.Cliente,
            this.DataEmissao,
            this.DataSaida,
            this.ValorTotal,
            this.IdCondPgto,
            this.CondicaoPagamento,
            this.Status});
            // 
            // btnDeletar
            // 
            this.btnDeletar.Text = "Cancelar";
            this.btnDeletar.Click += new System.EventHandler(this.btnDeletar_Click);
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Text = "Visualizar";
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnIncluir
            // 
            this.btnIncluir.Click += new System.EventHandler(this.btnIncluir_Click);
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
            // Numero
            // 
            this.Numero.Text = "Número";
            this.Numero.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Numero.Width = 100;
            // 
            // Cliente
            // 
            this.Cliente.Text = "Cliente";
            this.Cliente.Width = 280;
            // 
            // DataEmissao
            // 
            this.DataEmissao.Text = "Data de Emissão";
            this.DataEmissao.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DataEmissao.Width = 120;
            // 
            // DataSaida
            // 
            this.DataSaida.Text = "Data de Saída";
            this.DataSaida.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DataSaida.Width = 120;
            // 
            // ValorTotal
            // 
            this.ValorTotal.Text = "ValorTotal";
            this.ValorTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ValorTotal.Width = 100;
            // 
            // CondicaoPagamento
            // 
            this.CondicaoPagamento.Text = "CondPgto";
            this.CondicaoPagamento.Width = 120;
            // 
            // Status
            // 
            this.Status.Text = "Status";
            this.Status.Width = 70;
            // 
            // IdCliente
            // 
            this.IdCliente.Text = "IdCliente";
            this.IdCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.IdCliente.Width = 100;
            // 
            // IdCondPgto
            // 
            this.IdCondPgto.Text = "IdCondPgto";
            this.IdCondPgto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.IdCondPgto.Width = 100;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 0;
            // 
            // frmConsultaVenda
            // 
            //this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            //this.ClientSize = new System.Drawing.Size(1342, 681);
            this.Name = "frmConsultaVenda";
            this.Text = "Consulta Venda";
            this.Load += new System.EventHandler(this.frmConsultaVenda_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColumnHeader Modelo;
        private System.Windows.Forms.ColumnHeader Serie;
        private System.Windows.Forms.ColumnHeader Numero;
        private System.Windows.Forms.ColumnHeader Cliente;
        private System.Windows.Forms.ColumnHeader DataEmissao;
        private System.Windows.Forms.ColumnHeader DataSaida;
        private System.Windows.Forms.ColumnHeader ValorTotal;
        private System.Windows.Forms.ColumnHeader CondicaoPagamento;
        private System.Windows.Forms.ColumnHeader Status;
        private System.Windows.Forms.ColumnHeader IdCliente;
        private System.Windows.Forms.ColumnHeader IdCondPgto;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}
