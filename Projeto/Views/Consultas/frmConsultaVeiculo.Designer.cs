namespace Projeto.Views.Consultas
{
    partial class frmConsultaVeiculo
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
            this.Placa = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Marca = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Modelo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AnoFabricacao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CapacidadeCarga = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IDTransportadora = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Ativo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.ID,
            this.Placa,
            this.Marca,
            this.Modelo,
            this.AnoFabricacao,
            this.CapacidadeCarga,
            this.IDTransportadora,
            this.Ativo});
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
            // Placa
            // 
            this.Placa.Text = "Placa";
            this.Placa.Width = 100;
            // 
            // Marca
            // 
            this.Marca.Text = "Marca";
            this.Marca.Width = 150;
            // 
            // Modelo
            // 
            this.Modelo.Text = "Modelo";
            this.Modelo.Width = 150;
            // 
            // AnoFabricacao
            // 
            this.AnoFabricacao.Text = "Ano de Fabricação";
            this.AnoFabricacao.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.AnoFabricacao.Width = 130;
            // 
            // CapacidadeCarga
            // 
            this.CapacidadeCarga.Text = "Capacidade (kg)";
            this.CapacidadeCarga.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.CapacidadeCarga.Width = 100;
            // 
            // IDTransportadora
            // 
            this.IDTransportadora.Text = "Transportadora";
            this.IDTransportadora.Width = 200;
            // 
            // Ativo
            // 
            this.Ativo.Text = "Ativo";
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 0;
            // 
            // frmConsultaVeiculo
            // 
            //this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            //this.ClientSize = new System.Drawing.Size(1342, 681);
            this.Name = "frmConsultaVeiculo";
            this.Text = "Consulta Veículo";
            this.Load += new System.EventHandler(this.frmConsultaVeiculo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader Placa;
        private System.Windows.Forms.ColumnHeader Marca;
        private System.Windows.Forms.ColumnHeader Modelo;
        private System.Windows.Forms.ColumnHeader AnoFabricacao;
        private System.Windows.Forms.ColumnHeader CapacidadeCarga;
        private System.Windows.Forms.ColumnHeader IDTransportadora;
        private System.Windows.Forms.ColumnHeader Ativo;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}
