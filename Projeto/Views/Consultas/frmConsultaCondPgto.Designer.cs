namespace Projeto
{
    partial class frmConsultaCondPgto
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
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Descricao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Qtd_parcelas = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Juros = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Multa = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Desconto = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnSelecionar = new System.Windows.Forms.Button();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.ID,
            this.Descricao,
            this.Qtd_parcelas,
            this.Juros,
            this.Multa,
            this.Desconto,
            this.Status});
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
            // ID
            // 
            this.ID.Text = "ID";
            this.ID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ID.Width = 50;
            // 
            // Descricao
            // 
            this.Descricao.Text = "Descrição";
            this.Descricao.Width = 200;
            // 
            // Qtd_parcelas
            // 
            this.Qtd_parcelas.Text = "Qtd_Parcelas";
            this.Qtd_parcelas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Qtd_parcelas.Width = 80;
            // 
            // Juros
            // 
            this.Juros.Text = "Juros";
            this.Juros.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Multa
            // 
            this.Multa.Text = "Multa";
            this.Multa.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Desconto
            // 
            this.Desconto.Text = "Desconto";
            this.Desconto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Status
            // 
            this.Status.Text = "Ativo";
            this.Status.Width = 50;
            // 
            // btnSelecionar
            // 
            this.btnSelecionar.Location = new System.Drawing.Point(846, 644);
            this.btnSelecionar.Name = "btnSelecionar";
            this.btnSelecionar.Size = new System.Drawing.Size(90, 23);
            this.btnSelecionar.TabIndex = 9;
            this.btnSelecionar.Text = "Selecionar";
            this.btnSelecionar.UseVisualStyleBackColor = true;
            this.btnSelecionar.Click += new System.EventHandler(this.btnSelecionar_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 0;
            // 
            // frmConsultaCondPgto
            // 
            //this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            //this.ClientSize = new System.Drawing.Size(1342, 681);
            this.Controls.Add(this.btnSelecionar);
            this.Name = "frmConsultaCondPgto";
            this.Text = "Consulta Condições de Pagamento";
            this.Load += new System.EventHandler(this.frmConsultaCondPgto_Load);
            this.Controls.SetChildIndex(this.listView1, 0);
            this.Controls.SetChildIndex(this.txtPesquisar, 0);
            this.Controls.SetChildIndex(this.btnSair, 0);
            this.Controls.SetChildIndex(this.btnPesquisar, 0);
            this.Controls.SetChildIndex(this.btnDeletar, 0);
            this.Controls.SetChildIndex(this.btnEditar, 0);
            this.Controls.SetChildIndex(this.btnIncluir, 0);
            this.Controls.SetChildIndex(this.btnSelecionar, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader Descricao;
        private System.Windows.Forms.ColumnHeader Qtd_parcelas;
        private System.Windows.Forms.ColumnHeader Juros;
        private System.Windows.Forms.ColumnHeader Multa;
        private System.Windows.Forms.ColumnHeader Desconto;
        private System.Windows.Forms.ColumnHeader Status;
        private System.Windows.Forms.Button btnSelecionar;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}
