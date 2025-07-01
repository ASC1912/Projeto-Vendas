namespace Projeto.Views.Consultas
{
    partial class frmConsultaTransportadora
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
            this.Nome = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CPF_CNPJ = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Telefone = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Email = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Endereco = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Numero = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Bairro = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Complemento = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CEP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Tipo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.InscricaoEstadual = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.InscEstSubTrib = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IDCidade = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IDCondPgto = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ID,
            this.Nome,
            this.CPF_CNPJ,
            this.Telefone,
            this.Email,
            this.Endereco,
            this.Numero,
            this.Bairro,
            this.Complemento,
            this.CEP,
            this.Tipo,
            this.InscricaoEstadual,
            this.InscEstSubTrib,
            this.IDCidade,
            this.IDCondPgto,
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
            // 
            // Nome
            // 
            this.Nome.Text = "Transportadora";
            this.Nome.Width = 115;
            // 
            // CPF_CNPJ
            // 
            this.CPF_CNPJ.Text = "CPF/CNPJ";
            // 
            // Telefone
            // 
            this.Telefone.Text = "Telefone";
            // 
            // Email
            // 
            this.Email.Text = "Email";
            // 
            // Endereco
            // 
            this.Endereco.Text = "Endereco";
            // 
            // Numero
            // 
            this.Numero.Text = "Numero";
            // 
            // Bairro
            // 
            this.Bairro.Text = "Bairro";
            // 
            // Complemento
            // 
            this.Complemento.Text = "Complemento";
            // 
            // CEP
            // 
            this.CEP.Text = "CEP";
            // 
            // Tipo
            // 
            this.Tipo.Text = "Tipo";
            // 
            // InscricaoEstadual
            // 
            this.InscricaoEstadual.Text = "InscriçãoEstadual";
            // 
            // InscEstSubTrib
            // 
            this.InscEstSubTrib.Text = "InscEstSubTrib";
            // 
            // IDCidade
            // 
            this.IDCidade.Text = "Cidade";
            // 
            // IDCondPgto
            // 
            this.IDCondPgto.Text = "CondPgto";
            // 
            // Status
            // 
            this.Status.Text = "Ativo";
            // 
            // frmConsultaTransportadora
            // 
            //this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            //this.ClientSize = new System.Drawing.Size(1342, 681);
            this.Name = "frmConsultaTransportadora";
            this.Text = "Consulta Transportadoras";
            this.Load += new System.EventHandler(this.frmConsultaTransportadora_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader Nome;
        private System.Windows.Forms.ColumnHeader CPF_CNPJ;
        private System.Windows.Forms.ColumnHeader Telefone;
        private System.Windows.Forms.ColumnHeader Email;
        private System.Windows.Forms.ColumnHeader Endereco;
        private System.Windows.Forms.ColumnHeader Numero;
        private System.Windows.Forms.ColumnHeader Bairro;
        private System.Windows.Forms.ColumnHeader Complemento;
        private System.Windows.Forms.ColumnHeader CEP;
        private System.Windows.Forms.ColumnHeader Tipo;
        private System.Windows.Forms.ColumnHeader InscricaoEstadual;
        private System.Windows.Forms.ColumnHeader InscEstSubTrib;
        private System.Windows.Forms.ColumnHeader IDCidade;
        private System.Windows.Forms.ColumnHeader IDCondPgto;
        private System.Windows.Forms.ColumnHeader Status;
    }
}
