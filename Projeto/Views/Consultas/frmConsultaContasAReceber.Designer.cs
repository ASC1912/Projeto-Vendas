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
            this.Cliente});
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
            // frmConsultaContasAReceber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(1342, 681);
            this.Name = "frmConsultaContasAReceber";
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
    }
}
