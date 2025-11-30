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
            this.DataNascimento = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnSelecionar = new System.Windows.Forms.Button();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
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
            this.DataNascimento,
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
            // Nome
            // 
            this.Nome.Text = "Funcionário";
            this.Nome.Width = 200;
            // 
            // Telefone
            // 
            this.Telefone.Text = "Telefone";
            this.Telefone.Width = 76;
            // 
            // Email
            // 
            this.Email.Text = "Email";
            this.Email.Width = 200;
            // 
            // CEP
            // 
            this.CEP.Text = "CEP";
            this.CEP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.CEP.Width = 80;
            // 
            // Tipo
            // 
            this.Tipo.Text = "Tipo";
            // 
            // IDCidade
            // 
            this.IDCidade.Text = "Cidade";
            this.IDCidade.Width = 150;
            // 
            // Cargo
            // 
            this.Cargo.Text = "Cargo";
            this.Cargo.Width = 130;
            // 
            // Salario
            // 
            this.Salario.Text = "Salário";
            this.Salario.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Salario.Width = 100;
            // 
            // Endereco
            // 
            this.Endereco.Text = "Endereço";
            this.Endereco.Width = 200;
            // 
            // Bairro
            // 
            this.Bairro.Text = "Bairro";
            this.Bairro.Width = 150;
            // 
            // Numero
            // 
            this.Numero.Text = "Número";
            this.Numero.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Complemento
            // 
            this.Complemento.Text = "Complemento";
            this.Complemento.Width = 130;
            // 
            // DataAdmissao
            // 
            this.DataAdmissao.Text = "Data de Admissão";
            this.DataAdmissao.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DataAdmissao.Width = 120;
            // 
            // DataDemissao
            // 
            this.DataDemissao.Text = "Data de Demissão";
            this.DataDemissao.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DataDemissao.Width = 120;
            // 
            // Status
            // 
            this.Status.Text = "Ativo";
            this.Status.Width = 100;
            // 
            // Gênero
            // 
            this.Gênero.Text = "Gênero";
            // 
            // Apelido
            // 
            this.Apelido.Text = "Apelido";
            this.Apelido.Width = 100;
            // 
            // Matricula
            // 
            this.Matricula.Text = "Matrícula";
            // 
            // CPF_CNPJ
            // 
            this.CPF_CNPJ.Text = "CPF/CNPJ";
            this.CPF_CNPJ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.CPF_CNPJ.Width = 100;
            // 
            // RG
            // 
            this.RG.Text = "RG";
            this.RG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.RG.Width = 100;
            // 
            // DataNascimento
            // 
            this.DataNascimento.Text = "DataNascimento";
            this.DataNascimento.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DataNascimento.Width = 120;
            // 
            // btnSelecionar
            // 
            this.btnSelecionar.Location = new System.Drawing.Point(848, 644);
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
            // frmConsultaFuncionario
            // 
            //this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            //this.ClientSize = new System.Drawing.Size(1342, 681);
            this.Controls.Add(this.btnSelecionar);
            this.Name = "frmConsultaFuncionario";
            this.Text = "Consulta Funcionarios";
            this.Load += new System.EventHandler(this.frmConsultaFuncionario_Load);
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
        private System.Windows.Forms.ColumnHeader DataNascimento;
        private System.Windows.Forms.Button btnSelecionar;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}
