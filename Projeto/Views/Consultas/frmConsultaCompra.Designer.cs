namespace Projeto.Views.Consultas
{
    partial class frmConsultaCompra
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
            this.Fornecedor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DataEmissao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DataChegada = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ValorTotal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CondPgto = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IdFornecedor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IdCondPgto = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Modelo,
            this.Serie,
            this.Numero,
            this.IdFornecedor,
            this.Fornecedor,
            this.DataEmissao,
            this.DataChegada,
            this.ValorTotal,
            this.IdCondPgto,
            this.CondPgto,
            this.Status});
            // 
            // btnDeletar
            // 
            this.btnDeletar.Click += new System.EventHandler(this.btnDeletar_Click);
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
            // 
            // Fornecedor
            // 
            this.Fornecedor.Text = "Fornecedor";
            this.Fornecedor.Width = 200;
            // 
            // DataEmissao
            // 
            this.DataEmissao.Text = "Data de Emissão";
            this.DataEmissao.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DataEmissao.Width = 120;
            // 
            // DataChegada
            // 
            this.DataChegada.Text = "Data de Chegada";
            this.DataChegada.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DataChegada.Width = 120;
            // 
            // ValorTotal
            // 
            this.ValorTotal.Text = "ValorTotal";
            this.ValorTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ValorTotal.Width = 100;
            // 
            // CondPgto
            // 
            this.CondPgto.Text = "CondPgto";
            this.CondPgto.Width = 150;
            // 
            // Status
            // 
            this.Status.Text = "Status";
            this.Status.Width = 70;
            // 
            // IdFornecedor
            // 
            this.IdFornecedor.Text = "IdFornecedor";
            this.IdFornecedor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.IdFornecedor.Width = 100;
            // 
            // IdCondPgto
            // 
            this.IdCondPgto.Text = "IdCondPgto";
            this.IdCondPgto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.IdCondPgto.Width = 100;
            // 
            // frmConsultaCompra
            // 
            //this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            //this.ClientSize = new System.Drawing.Size(1342, 681);
            this.Name = "frmConsultaCompra";
            this.Text = "Consulta Compra";
            this.Load += new System.EventHandler(this.frmConsultaCompra_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColumnHeader Modelo;
        private System.Windows.Forms.ColumnHeader Serie;
        private System.Windows.Forms.ColumnHeader Numero;
        private System.Windows.Forms.ColumnHeader Fornecedor;
        private System.Windows.Forms.ColumnHeader DataEmissao;
        private System.Windows.Forms.ColumnHeader DataChegada;
        private System.Windows.Forms.ColumnHeader ValorTotal;
        private System.Windows.Forms.ColumnHeader CondPgto;
        private System.Windows.Forms.ColumnHeader Status;
        private System.Windows.Forms.ColumnHeader IdFornecedor;
        private System.Windows.Forms.ColumnHeader IdCondPgto;
    }
}
