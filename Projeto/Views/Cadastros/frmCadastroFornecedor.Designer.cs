namespace Projeto.Views
{
    partial class frmCadastroFornecedor
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
            this.lblCEP = new System.Windows.Forms.Label();
            this.txtCEP = new System.Windows.Forms.TextBox();
            this.lblTipo = new System.Windows.Forms.Label();
            this.lblTelefone = new System.Windows.Forms.Label();
            this.txtTelefone = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblCPF = new System.Windows.Forms.Label();
            this.txtCPF = new System.Windows.Forms.TextBox();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.lblCidade = new System.Windows.Forms.Label();
            this.lblNome = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.lblInscEstSubTrib = new System.Windows.Forms.Label();
            this.txtInscEstSubTrib = new System.Windows.Forms.TextBox();
            this.lblInscEst = new System.Windows.Forms.Label();
            this.txtInscEst = new System.Windows.Forms.TextBox();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.lblEndereco = new System.Windows.Forms.Label();
            this.txtEndereco = new System.Windows.Forms.TextBox();
            this.lblComplemento = new System.Windows.Forms.Label();
            this.txtComplemento = new System.Windows.Forms.TextBox();
            this.lblNumEndereco = new System.Windows.Forms.Label();
            this.txtNumEnd = new System.Windows.Forms.TextBox();
            this.lblBairro = new System.Windows.Forms.Label();
            this.txtBairro = new System.Windows.Forms.TextBox();
            this.lblCondPgto = new System.Windows.Forms.Label();
            this.cbTipo = new System.Windows.Forms.ComboBox();
            this.txtCidade = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtCondicao = new System.Windows.Forms.TextBox();
            this.btnBuscarCond = new System.Windows.Forms.Button();
            this.IDCidade = new System.Windows.Forms.Label();
            this.txtIdCidade = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(12, 39);
            this.txtCodigo.Size = new System.Drawing.Size(100, 22);
            this.txtCodigo.TabIndex = 0;
            // 
            // btnSair
            // 
            this.btnSair.TabIndex = 20;
            // 
            // chkInativo
            // 
            this.chkInativo.TabIndex = 18;
            // 
            // lblCEP
            // 
            this.lblCEP.AutoSize = true;
            this.lblCEP.Location = new System.Drawing.Point(718, 86);
            this.lblCEP.Name = "lblCEP";
            this.lblCEP.Size = new System.Drawing.Size(34, 16);
            this.lblCEP.TabIndex = 47;
            this.lblCEP.Text = "CEP";
            // 
            // txtCEP
            // 
            this.txtCEP.Location = new System.Drawing.Point(721, 105);
            this.txtCEP.MaxLength = 9;
            this.txtCEP.Name = "txtCEP";
            this.txtCEP.Size = new System.Drawing.Size(150, 22);
            this.txtCEP.TabIndex = 7;
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.Location = new System.Drawing.Point(172, 23);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(40, 16);
            this.lblTipo.TabIndex = 45;
            this.lblTipo.Text = "Tipo*";
            // 
            // lblTelefone
            // 
            this.lblTelefone.AutoSize = true;
            this.lblTelefone.Location = new System.Drawing.Point(748, 158);
            this.lblTelefone.Name = "lblTelefone";
            this.lblTelefone.Size = new System.Drawing.Size(61, 16);
            this.lblTelefone.TabIndex = 43;
            this.lblTelefone.Text = "Telefone";
            // 
            // txtTelefone
            // 
            this.txtTelefone.Location = new System.Drawing.Point(751, 177);
            this.txtTelefone.Name = "txtTelefone";
            this.txtTelefone.Size = new System.Drawing.Size(150, 22);
            this.txtTelefone.TabIndex = 12;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(542, 157);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(46, 16);
            this.lblEmail.TabIndex = 41;
            this.lblEmail.Text = "Email*";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(545, 176);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(200, 22);
            this.txtEmail.TabIndex = 11;
            // 
            // lblCPF
            // 
            this.lblCPF.AutoSize = true;
            this.lblCPF.Location = new System.Drawing.Point(12, 230);
            this.lblCPF.Name = "lblCPF";
            this.lblCPF.Size = new System.Drawing.Size(38, 16);
            this.lblCPF.TabIndex = 39;
            this.lblCPF.Text = "CPF*";
            // 
            // txtCPF
            // 
            this.txtCPF.Location = new System.Drawing.Point(15, 249);
            this.txtCPF.MaxLength = 18;
            this.txtCPF.Name = "txtCPF";
            this.txtCPF.Size = new System.Drawing.Size(150, 22);
            this.txtCPF.TabIndex = 13;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(1117, 634);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(80, 35);
            this.btnSalvar.TabIndex = 19;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // lblCidade
            // 
            this.lblCidade.AutoSize = true;
            this.lblCidade.Location = new System.Drawing.Point(93, 158);
            this.lblCidade.Name = "lblCidade";
            this.lblCidade.Size = new System.Drawing.Size(56, 16);
            this.lblCidade.TabIndex = 36;
            this.lblCidade.Text = "Cidade*";
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Location = new System.Drawing.Point(318, 22);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(82, 16);
            this.lblNome.TabIndex = 34;
            this.lblNome.Text = "Fornecedor*";
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(321, 41);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(200, 22);
            this.txtNome.TabIndex = 2;
            // 
            // lblInscEstSubTrib
            // 
            this.lblInscEstSubTrib.AutoSize = true;
            this.lblInscEstSubTrib.Location = new System.Drawing.Point(341, 230);
            this.lblInscEstSubTrib.Name = "lblInscEstSubTrib";
            this.lblInscEstSubTrib.Size = new System.Drawing.Size(107, 16);
            this.lblInscEstSubTrib.TabIndex = 51;
            this.lblInscEstSubTrib.Text = "Insc Est Sub Trib";
            // 
            // txtInscEstSubTrib
            // 
            this.txtInscEstSubTrib.Location = new System.Drawing.Point(344, 249);
            this.txtInscEstSubTrib.MaxLength = 30;
            this.txtInscEstSubTrib.Name = "txtInscEstSubTrib";
            this.txtInscEstSubTrib.Size = new System.Drawing.Size(150, 22);
            this.txtInscEstSubTrib.TabIndex = 15;
            // 
            // lblInscEst
            // 
            this.lblInscEst.AutoSize = true;
            this.lblInscEst.Location = new System.Drawing.Point(172, 230);
            this.lblInscEst.Name = "lblInscEst";
            this.lblInscEst.Size = new System.Drawing.Size(122, 16);
            this.lblInscEst.TabIndex = 49;
            this.lblInscEst.Text = "Inscrição Estadual*";
            // 
            // txtInscEst
            // 
            this.txtInscEst.Location = new System.Drawing.Point(175, 249);
            this.txtInscEst.MaxLength = 30;
            this.txtInscEst.Name = "txtInscEst";
            this.txtInscEst.Size = new System.Drawing.Size(150, 22);
            this.txtInscEst.TabIndex = 14;
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Location = new System.Drawing.Point(12, 21);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(51, 16);
            this.lblCodigo.TabIndex = 52;
            this.lblCodigo.Text = "Código";
            // 
            // lblEndereco
            // 
            this.lblEndereco.AutoSize = true;
            this.lblEndereco.Location = new System.Drawing.Point(9, 86);
            this.lblEndereco.Name = "lblEndereco";
            this.lblEndereco.Size = new System.Drawing.Size(71, 16);
            this.lblEndereco.TabIndex = 54;
            this.lblEndereco.Text = "Endereço*";
            // 
            // txtEndereco
            // 
            this.txtEndereco.Location = new System.Drawing.Point(12, 105);
            this.txtEndereco.Name = "txtEndereco";
            this.txtEndereco.Size = new System.Drawing.Size(200, 22);
            this.txtEndereco.TabIndex = 3;
            // 
            // lblComplemento
            // 
            this.lblComplemento.AutoSize = true;
            this.lblComplemento.Location = new System.Drawing.Point(542, 86);
            this.lblComplemento.Name = "lblComplemento";
            this.lblComplemento.Size = new System.Drawing.Size(91, 16);
            this.lblComplemento.TabIndex = 60;
            this.lblComplemento.Text = "Complemento";
            // 
            // txtComplemento
            // 
            this.txtComplemento.Location = new System.Drawing.Point(545, 105);
            this.txtComplemento.Name = "txtComplemento";
            this.txtComplemento.Size = new System.Drawing.Size(150, 22);
            this.txtComplemento.TabIndex = 6;
            // 
            // lblNumEndereco
            // 
            this.lblNumEndereco.AutoSize = true;
            this.lblNumEndereco.Location = new System.Drawing.Point(223, 86);
            this.lblNumEndereco.Name = "lblNumEndereco";
            this.lblNumEndereco.Size = new System.Drawing.Size(60, 16);
            this.lblNumEndereco.TabIndex = 58;
            this.lblNumEndereco.Text = "Número*";
            // 
            // txtNumEnd
            // 
            this.txtNumEnd.Location = new System.Drawing.Point(226, 105);
            this.txtNumEnd.Name = "txtNumEnd";
            this.txtNumEnd.Size = new System.Drawing.Size(70, 22);
            this.txtNumEnd.TabIndex = 4;
            // 
            // lblBairro
            // 
            this.lblBairro.AutoSize = true;
            this.lblBairro.Location = new System.Drawing.Point(318, 86);
            this.lblBairro.Name = "lblBairro";
            this.lblBairro.Size = new System.Drawing.Size(43, 16);
            this.lblBairro.TabIndex = 56;
            this.lblBairro.Text = "Bairro";
            // 
            // txtBairro
            // 
            this.txtBairro.Location = new System.Drawing.Point(321, 105);
            this.txtBairro.Name = "txtBairro";
            this.txtBairro.Size = new System.Drawing.Size(200, 22);
            this.txtBairro.TabIndex = 5;
            // 
            // lblCondPgto
            // 
            this.lblCondPgto.AutoSize = true;
            this.lblCondPgto.Location = new System.Drawing.Point(542, 228);
            this.lblCondPgto.Name = "lblCondPgto";
            this.lblCondPgto.Size = new System.Drawing.Size(143, 16);
            this.lblCondPgto.TabIndex = 62;
            this.lblCondPgto.Text = "Condição Pagamento*";
            // 
            // cbTipo
            // 
            this.cbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipo.FormattingEnabled = true;
            this.cbTipo.Items.AddRange(new object[] {
            "Físico",
            "Jurídico"});
            this.cbTipo.Location = new System.Drawing.Point(175, 39);
            this.cbTipo.Name = "cbTipo";
            this.cbTipo.Size = new System.Drawing.Size(121, 24);
            this.cbTipo.TabIndex = 1;
            // 
            // txtCidade
            // 
            this.txtCidade.Location = new System.Drawing.Point(96, 177);
            this.txtCidade.Name = "txtCidade";
            this.txtCidade.ReadOnly = true;
            this.txtCidade.Size = new System.Drawing.Size(200, 22);
            this.txtCidade.TabIndex = 9;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(321, 175);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(150, 23);
            this.btnBuscar.TabIndex = 10;
            this.btnBuscar.Text = "Buscar Cidade";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtCondicao
            // 
            this.txtCondicao.Location = new System.Drawing.Point(545, 249);
            this.txtCondicao.Name = "txtCondicao";
            this.txtCondicao.ReadOnly = true;
            this.txtCondicao.Size = new System.Drawing.Size(200, 22);
            this.txtCondicao.TabIndex = 16;
            // 
            // btnBuscarCond
            // 
            this.btnBuscarCond.Location = new System.Drawing.Point(751, 249);
            this.btnBuscarCond.Name = "btnBuscarCond";
            this.btnBuscarCond.Size = new System.Drawing.Size(150, 23);
            this.btnBuscarCond.TabIndex = 17;
            this.btnBuscarCond.Text = "Buscar Condição";
            this.btnBuscarCond.UseVisualStyleBackColor = true;
            this.btnBuscarCond.Click += new System.EventHandler(this.btnBuscarCond_Click);
            // 
            // IDCidade
            // 
            this.IDCidade.AutoSize = true;
            this.IDCidade.Location = new System.Drawing.Point(9, 158);
            this.IDCidade.Name = "IDCidade";
            this.IDCidade.Size = new System.Drawing.Size(72, 16);
            this.IDCidade.TabIndex = 78;
            this.IDCidade.Text = "ID Cidade*";
            // 
            // txtIdCidade
            // 
            this.txtIdCidade.Location = new System.Drawing.Point(12, 177);
            this.txtIdCidade.Name = "txtIdCidade";
            this.txtIdCidade.Size = new System.Drawing.Size(67, 22);
            this.txtIdCidade.TabIndex = 8;
            this.txtIdCidade.Leave += new System.EventHandler(this.txtIdCidade_Leave);
            // 
            // frmCadastroFornecedor
            // 
            //this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            //this.ClientSize = new System.Drawing.Size(1342, 681);
            this.Controls.Add(this.IDCidade);
            this.Controls.Add(this.txtIdCidade);
            this.Controls.Add(this.txtCondicao);
            this.Controls.Add(this.btnBuscarCond);
            this.Controls.Add(this.txtCidade);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.cbTipo);
            this.Controls.Add(this.lblCondPgto);
            this.Controls.Add(this.lblComplemento);
            this.Controls.Add(this.txtComplemento);
            this.Controls.Add(this.lblNumEndereco);
            this.Controls.Add(this.txtNumEnd);
            this.Controls.Add(this.lblBairro);
            this.Controls.Add(this.txtBairro);
            this.Controls.Add(this.lblEndereco);
            this.Controls.Add(this.txtEndereco);
            this.Controls.Add(this.lblCodigo);
            this.Controls.Add(this.lblInscEstSubTrib);
            this.Controls.Add(this.txtInscEstSubTrib);
            this.Controls.Add(this.lblInscEst);
            this.Controls.Add(this.txtInscEst);
            this.Controls.Add(this.lblCEP);
            this.Controls.Add(this.txtCEP);
            this.Controls.Add(this.lblTipo);
            this.Controls.Add(this.lblTelefone);
            this.Controls.Add(this.txtTelefone);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblCPF);
            this.Controls.Add(this.txtCPF);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.lblCidade);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.txtNome);
            this.Name = "frmCadastroFornecedor";
            this.Text = "Cadastro Fornecedores";
            this.Load += new System.EventHandler(this.frmCadastroFornecedor_Load);
            this.Controls.SetChildIndex(this.lblDataModificacao, 0);
            this.Controls.SetChildIndex(this.chkInativo, 0);
            this.Controls.SetChildIndex(this.lblDataCriacao, 0);
            this.Controls.SetChildIndex(this.txtNome, 0);
            this.Controls.SetChildIndex(this.lblNome, 0);
            this.Controls.SetChildIndex(this.lblCidade, 0);
            this.Controls.SetChildIndex(this.btnSalvar, 0);
            this.Controls.SetChildIndex(this.txtCPF, 0);
            this.Controls.SetChildIndex(this.lblCPF, 0);
            this.Controls.SetChildIndex(this.txtEmail, 0);
            this.Controls.SetChildIndex(this.lblEmail, 0);
            this.Controls.SetChildIndex(this.txtTelefone, 0);
            this.Controls.SetChildIndex(this.lblTelefone, 0);
            this.Controls.SetChildIndex(this.lblTipo, 0);
            this.Controls.SetChildIndex(this.txtCEP, 0);
            this.Controls.SetChildIndex(this.lblCEP, 0);
            this.Controls.SetChildIndex(this.txtInscEst, 0);
            this.Controls.SetChildIndex(this.lblInscEst, 0);
            this.Controls.SetChildIndex(this.txtInscEstSubTrib, 0);
            this.Controls.SetChildIndex(this.lblInscEstSubTrib, 0);
            this.Controls.SetChildIndex(this.lblCodigo, 0);
            this.Controls.SetChildIndex(this.btnSair, 0);
            this.Controls.SetChildIndex(this.txtCodigo, 0);
            this.Controls.SetChildIndex(this.txtEndereco, 0);
            this.Controls.SetChildIndex(this.lblEndereco, 0);
            this.Controls.SetChildIndex(this.txtBairro, 0);
            this.Controls.SetChildIndex(this.lblBairro, 0);
            this.Controls.SetChildIndex(this.txtNumEnd, 0);
            this.Controls.SetChildIndex(this.lblNumEndereco, 0);
            this.Controls.SetChildIndex(this.txtComplemento, 0);
            this.Controls.SetChildIndex(this.lblComplemento, 0);
            this.Controls.SetChildIndex(this.lblCondPgto, 0);
            this.Controls.SetChildIndex(this.cbTipo, 0);
            this.Controls.SetChildIndex(this.btnBuscar, 0);
            this.Controls.SetChildIndex(this.txtCidade, 0);
            this.Controls.SetChildIndex(this.btnBuscarCond, 0);
            this.Controls.SetChildIndex(this.txtCondicao, 0);
            this.Controls.SetChildIndex(this.txtIdCidade, 0);
            this.Controls.SetChildIndex(this.IDCidade, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCEP;
        private System.Windows.Forms.TextBox txtCEP;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.Label lblTelefone;
        private System.Windows.Forms.TextBox txtTelefone;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblCPF;
        private System.Windows.Forms.TextBox txtCPF;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Label lblCidade;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lblInscEstSubTrib;
        private System.Windows.Forms.TextBox txtInscEstSubTrib;
        private System.Windows.Forms.Label lblInscEst;
        private System.Windows.Forms.TextBox txtInscEst;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.Label lblEndereco;
        private System.Windows.Forms.TextBox txtEndereco;
        private System.Windows.Forms.Label lblComplemento;
        private System.Windows.Forms.TextBox txtComplemento;
        private System.Windows.Forms.Label lblNumEndereco;
        private System.Windows.Forms.TextBox txtNumEnd;
        private System.Windows.Forms.Label lblBairro;
        private System.Windows.Forms.TextBox txtBairro;
        private System.Windows.Forms.Label lblCondPgto;
        private System.Windows.Forms.ComboBox cbTipo;
        private System.Windows.Forms.TextBox txtCidade;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtCondicao;
        private System.Windows.Forms.Button btnBuscarCond;
        private System.Windows.Forms.Label IDCidade;
        private System.Windows.Forms.TextBox txtIdCidade;
    }
}
