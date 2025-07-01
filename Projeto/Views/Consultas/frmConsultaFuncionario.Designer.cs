namespace Projeto.Views
{
    partial class frmConsultaFuncionario
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
            this.Telefone = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Email = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CEP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Tipo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IDCidade = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Cargo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Salario = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Endereco = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Bairro = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Numero = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Complemento = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DataAdmissao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DataDemissao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Gênero = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Apelido = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Matricula = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CPF_CNPJ = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RG = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ID,
            this.Tipo,
            this.Nome,
            this.Apelido,
            this.Gênero,
            this.Endereco,
            this.Numero,
            this.Bairro,
            this.Complemento,
            this.CEP,
            this.IDCidade,
            this.Email,
            this.Telefone,
            this.Matricula,
            this.Cargo,
            this.Salario,
            this.CPF_CNPJ,
            this.RG,
            this.DataAdmissao,
            this.DataDemissao,
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
            this.Nome.Text = "Funcionário";
            this.Nome.Width = 88;
            // 
            // Telefone
            // 
            this.Telefone.Text = "Telefone";
            this.Telefone.Width = 76;
            // 
            // Email
            // 
            this.Email.Text = "Email";
            // 
            // CEP
            // 
            this.CEP.Text = "CEP";
            // 
            // Tipo
            // 
            this.Tipo.Text = "Tipo";
            // 
            // IDCidade
            // 
            this.IDCidade.Text = "Cidade";
            this.IDCidade.Width = 74;
            // 
            // Cargo
            // 
            this.Cargo.Text = "Cargo";
            // 
            // Salario
            // 
            this.Salario.Text = "Salário";
            // 
            // Endereco
            // 
            this.Endereco.Text = "Endereço";
            this.Endereco.Width = 74;
            // 
            // Bairro
            // 
            this.Bairro.Text = "Bairro";
            // 
            // Numero
            // 
            this.Numero.Text = "Número";
            // 
            // Complemento
            // 
            this.Complemento.Text = "Complemento";
            this.Complemento.Width = 104;
            // 
            // DataAdmissao
            // 
            this.DataAdmissao.Text = "Data de Admissão";
            this.DataAdmissao.Width = 126;
            // 
            // DataDemissao
            // 
            this.DataDemissao.Text = "Data de Demissão";
            this.DataDemissao.Width = 133;
            // 
            // Status
            // 
            this.Status.Text = "Ativo";
            // 
            // Gênero
            // 
            this.Gênero.Text = "Gênero";
            // 
            // Apelido
            // 
            this.Apelido.Text = "Apelido";
            // 
            // Matricula
            // 
            this.Matricula.Text = "Matrícula";
            // 
            // CPF_CNPJ
            // 
            this.CPF_CNPJ.Text = "CPF/CNPJ";
            // 
            // RG
            // 
            this.RG.Text = "RG";
            // 
            // frmConsultaFuncionario
            // 
            //this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            //this.ClientSize = new System.Drawing.Size(1342, 681);
            this.Name = "frmConsultaFuncionario";
            this.Text = "Consulta Funcionarios";
            this.Load += new System.EventHandler(this.frmConsultaFuncionario_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader Nome;
        private System.Windows.Forms.ColumnHeader Telefone;
        private System.Windows.Forms.ColumnHeader Email;
        private System.Windows.Forms.ColumnHeader CEP;
        private System.Windows.Forms.ColumnHeader Tipo;
        private System.Windows.Forms.ColumnHeader IDCidade;
        private System.Windows.Forms.ColumnHeader Endereco;
        private System.Windows.Forms.ColumnHeader Cargo;
        private System.Windows.Forms.ColumnHeader Salario;
        private System.Windows.Forms.ColumnHeader Bairro;
        private System.Windows.Forms.ColumnHeader Numero;
        private System.Windows.Forms.ColumnHeader Complemento;
        private System.Windows.Forms.ColumnHeader DataAdmissao;
        private System.Windows.Forms.ColumnHeader DataDemissao;
        private System.Windows.Forms.ColumnHeader Status;
        private System.Windows.Forms.ColumnHeader Gênero;
        private System.Windows.Forms.ColumnHeader Apelido;
        private System.Windows.Forms.ColumnHeader Matricula;
        private System.Windows.Forms.ColumnHeader CPF_CNPJ;
        private System.Windows.Forms.ColumnHeader RG;
    }
}
