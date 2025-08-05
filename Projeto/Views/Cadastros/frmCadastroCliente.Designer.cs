namespace Projeto.Views
{
    partial class frmCadastroCliente
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
            this.lblCidade = new System.Windows.Forms.Label();
            this.lblNome = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.lblCPF = new System.Windows.Forms.Label();
            this.txtCPF = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblTelefone = new System.Windows.Forms.Label();
            this.txtTelefone = new System.Windows.Forms.TextBox();
            this.lblTipo = new System.Windows.Forms.Label();
            this.lblCEP = new System.Windows.Forms.Label();
            this.txtCEP = new System.Windows.Forms.TextBox();
            this.lblEndereco = new System.Windows.Forms.Label();
            this.txtEndereco = new System.Windows.Forms.TextBox();
            this.lblBairro = new System.Windows.Forms.Label();
            this.txtBairro = new System.Windows.Forms.TextBox();
            this.lblNumEndereco = new System.Windows.Forms.Label();
            this.txtNumEnd = new System.Windows.Forms.TextBox();
            this.lblComplemento = new System.Windows.Forms.Label();
            this.txtComplemento = new System.Windows.Forms.TextBox();
            this.lblCondPgto = new System.Windows.Forms.Label();
            this.cbTipo = new System.Windows.Forms.ComboBox();
            this.lblRG = new System.Windows.Forms.Label();
            this.txtRG = new System.Windows.Forms.TextBox();
            this.txtCidade = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtCondicao = new System.Windows.Forms.TextBox();
            this.btnBuscarCond = new System.Windows.Forms.Button();
            this.IDCidade = new System.Windows.Forms.Label();
            this.txtIdCidade = new System.Windows.Forms.TextBox();
            this.cbGenero = new System.Windows.Forms.ComboBox();
            this.lblGenero = new System.Windows.Forms.Label();
            this.lblDataNascimento = new System.Windows.Forms.Label();
            this.dtpNascimento = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(12, 38);
            this.txtCodigo.Size = new System.Drawing.Size(121, 22);
            this.txtCodigo.TabIndex = 0;
            // 
            // btnSair
            // 
            this.btnSair.TabIndex = 21;
            // 
            // chkInativo
            // 
            this.chkInativo.TabIndex = 19;
            // 
            // lblCidade
            // 
            this.lblCidade.AutoSize = true;
            this.lblCidade.Location = new System.Drawing.Point(82, 165);
            this.lblCidade.Name = "lblCidade";
            this.lblCidade.Size = new System.Drawing.Size(56, 16);
            this.lblCidade.TabIndex = 20;
            this.lblCidade.Text = "Cidade*";
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Location = new System.Drawing.Point(340, 19);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(53, 16);
            this.lblNome.TabIndex = 18;
            this.lblNome.Text = "Cliente*";
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(343, 38);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(200, 22);
            this.txtNome.TabIndex = 2;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(1104, 634);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(80, 35);
            this.btnSalvar.TabIndex = 20;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Location = new System.Drawing.Point(12, 19);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(51, 16);
            this.lblCodigo.TabIndex = 22;
            this.lblCodigo.Text = "Código";
            // 
            // lblCPF
            // 
            this.lblCPF.AutoSize = true;
            this.lblCPF.Location = new System.Drawing.Point(12, 267);
            this.lblCPF.Name = "lblCPF";
            this.lblCPF.Size = new System.Drawing.Size(38, 16);
            this.lblCPF.TabIndex = 24;
            this.lblCPF.Text = "CPF*";
            // 
            // txtCPF
            // 
            this.txtCPF.Location = new System.Drawing.Point(15, 286);
            this.txtCPF.MaxLength = 18;
            this.txtCPF.Name = "txtCPF";
            this.txtCPF.Size = new System.Drawing.Size(150, 22);
            this.txtCPF.TabIndex = 15;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(454, 167);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(46, 16);
            this.lblEmail.TabIndex = 26;
            this.lblEmail.Text = "Email*";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(457, 186);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(200, 22);
            this.txtEmail.TabIndex = 13;
            // 
            // lblTelefone
            // 
            this.lblTelefone.AutoSize = true;
            this.lblTelefone.Location = new System.Drawing.Point(684, 167);
            this.lblTelefone.Name = "lblTelefone";
            this.lblTelefone.Size = new System.Drawing.Size(61, 16);
            this.lblTelefone.TabIndex = 28;
            this.lblTelefone.Text = "Telefone";
            // 
            // txtTelefone
            // 
            this.txtTelefone.Location = new System.Drawing.Point(687, 186);
            this.txtTelefone.Name = "txtTelefone";
            this.txtTelefone.Size = new System.Drawing.Size(150, 22);
            this.txtTelefone.TabIndex = 14;
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.Location = new System.Drawing.Point(170, 19);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(40, 16);
            this.lblTipo.TabIndex = 30;
            this.lblTipo.Text = "Tipo*";
            // 
            // lblCEP
            // 
            this.lblCEP.AutoSize = true;
            this.lblCEP.Location = new System.Drawing.Point(744, 95);
            this.lblCEP.Name = "lblCEP";
            this.lblCEP.Size = new System.Drawing.Size(34, 16);
            this.lblCEP.TabIndex = 32;
            this.lblCEP.Text = "CEP";
            // 
            // txtCEP
            // 
            this.txtCEP.Location = new System.Drawing.Point(747, 114);
            this.txtCEP.MaxLength = 9;
            this.txtCEP.Name = "txtCEP";
            this.txtCEP.Size = new System.Drawing.Size(150, 22);
            this.txtCEP.TabIndex = 9;
            // 
            // lblEndereco
            // 
            this.lblEndereco.AutoSize = true;
            this.lblEndereco.Location = new System.Drawing.Point(9, 95);
            this.lblEndereco.Name = "lblEndereco";
            this.lblEndereco.Size = new System.Drawing.Size(71, 16);
            this.lblEndereco.TabIndex = 34;
            this.lblEndereco.Text = "Endereço*";
            // 
            // txtEndereco
            // 
            this.txtEndereco.Location = new System.Drawing.Point(12, 114);
            this.txtEndereco.Name = "txtEndereco";
            this.txtEndereco.Size = new System.Drawing.Size(213, 22);
            this.txtEndereco.TabIndex = 5;
            // 
            // lblBairro
            // 
            this.lblBairro.AutoSize = true;
            this.lblBairro.Location = new System.Drawing.Point(340, 95);
            this.lblBairro.Name = "lblBairro";
            this.lblBairro.Size = new System.Drawing.Size(43, 16);
            this.lblBairro.TabIndex = 36;
            this.lblBairro.Text = "Bairro";
            // 
            // txtBairro
            // 
            this.txtBairro.Location = new System.Drawing.Point(343, 114);
            this.txtBairro.Name = "txtBairro";
            this.txtBairro.Size = new System.Drawing.Size(200, 22);
            this.txtBairro.TabIndex = 7;
            // 
            // lblNumEndereco
            // 
            this.lblNumEndereco.AutoSize = true;
            this.lblNumEndereco.Location = new System.Drawing.Point(239, 95);
            this.lblNumEndereco.Name = "lblNumEndereco";
            this.lblNumEndereco.Size = new System.Drawing.Size(60, 16);
            this.lblNumEndereco.TabIndex = 38;
            this.lblNumEndereco.Text = "Número*";
            // 
            // txtNumEnd
            // 
            this.txtNumEnd.Location = new System.Drawing.Point(242, 114);
            this.txtNumEnd.Name = "txtNumEnd";
            this.txtNumEnd.Size = new System.Drawing.Size(52, 22);
            this.txtNumEnd.TabIndex = 6;
            // 
            // lblComplemento
            // 
            this.lblComplemento.AutoSize = true;
            this.lblComplemento.Location = new System.Drawing.Point(567, 95);
            this.lblComplemento.Name = "lblComplemento";
            this.lblComplemento.Size = new System.Drawing.Size(91, 16);
            this.lblComplemento.TabIndex = 40;
            this.lblComplemento.Text = "Complemento";
            // 
            // txtComplemento
            // 
            this.txtComplemento.Location = new System.Drawing.Point(570, 114);
            this.txtComplemento.Name = "txtComplemento";
            this.txtComplemento.Size = new System.Drawing.Size(150, 22);
            this.txtComplemento.TabIndex = 8;
            // 
            // lblCondPgto
            // 
            this.lblCondPgto.AutoSize = true;
            this.lblCondPgto.Location = new System.Drawing.Point(454, 266);
            this.lblCondPgto.Name = "lblCondPgto";
            this.lblCondPgto.Size = new System.Drawing.Size(138, 16);
            this.lblCondPgto.TabIndex = 42;
            this.lblCondPgto.Text = "Condição Pagamento";
            // 
            // cbTipo
            // 
            this.cbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipo.FormattingEnabled = true;
            this.cbTipo.Items.AddRange(new object[] {
            "Físico",
            "Jurídico"});
            this.cbTipo.Location = new System.Drawing.Point(173, 38);
            this.cbTipo.Name = "cbTipo";
            this.cbTipo.Size = new System.Drawing.Size(121, 24);
            this.cbTipo.TabIndex = 1;
            // 
            // lblRG
            // 
            this.lblRG.AutoSize = true;
            this.lblRG.Location = new System.Drawing.Point(170, 267);
            this.lblRG.Name = "lblRG";
            this.lblRG.Size = new System.Drawing.Size(27, 16);
            this.lblRG.TabIndex = 66;
            this.lblRG.Text = "RG";
            // 
            // txtRG
            // 
            this.txtRG.Location = new System.Drawing.Point(173, 286);
            this.txtRG.MaxLength = 20;
            this.txtRG.Name = "txtRG";
            this.txtRG.Size = new System.Drawing.Size(150, 22);
            this.txtRG.TabIndex = 16;
            // 
            // txtCidade
            // 
            this.txtCidade.Location = new System.Drawing.Point(85, 185);
            this.txtCidade.Name = "txtCidade";
            this.txtCidade.ReadOnly = true;
            this.txtCidade.Size = new System.Drawing.Size(200, 22);
            this.txtCidade.TabIndex = 11;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(308, 185);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(121, 23);
            this.btnBuscar.TabIndex = 12;
            this.btnBuscar.Text = "Buscar Cidade";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtCondicao
            // 
            this.txtCondicao.Location = new System.Drawing.Point(457, 286);
            this.txtCondicao.Name = "txtCondicao";
            this.txtCondicao.ReadOnly = true;
            this.txtCondicao.Size = new System.Drawing.Size(200, 22);
            this.txtCondicao.TabIndex = 17;
            // 
            // btnBuscarCond
            // 
            this.btnBuscarCond.Location = new System.Drawing.Point(687, 285);
            this.btnBuscarCond.Name = "btnBuscarCond";
            this.btnBuscarCond.Size = new System.Drawing.Size(150, 23);
            this.btnBuscarCond.TabIndex = 18;
            this.btnBuscarCond.Text = "Buscar Condição";
            this.btnBuscarCond.UseVisualStyleBackColor = true;
            this.btnBuscarCond.Click += new System.EventHandler(this.btnBuscarCond_Click);
            // 
            // IDCidade
            // 
            this.IDCidade.AutoSize = true;
            this.IDCidade.Location = new System.Drawing.Point(9, 165);
            this.IDCidade.Name = "IDCidade";
            this.IDCidade.Size = new System.Drawing.Size(72, 16);
            this.IDCidade.TabIndex = 73;
            this.IDCidade.Text = "ID Cidade*";
            // 
            // txtIdCidade
            // 
            this.txtIdCidade.Location = new System.Drawing.Point(12, 184);
            this.txtIdCidade.Name = "txtIdCidade";
            this.txtIdCidade.Size = new System.Drawing.Size(67, 22);
            this.txtIdCidade.TabIndex = 10;
            this.txtIdCidade.Leave += new System.EventHandler(this.txtIdCidade_Leave);
            // 
            // cbGenero
            // 
            this.cbGenero.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGenero.FormattingEnabled = true;
            this.cbGenero.Items.AddRange(new object[] {
            "Masculino",
            "Feminino"});
            this.cbGenero.Location = new System.Drawing.Point(570, 36);
            this.cbGenero.Name = "cbGenero";
            this.cbGenero.Size = new System.Drawing.Size(121, 24);
            this.cbGenero.TabIndex = 3;
            // 
            // lblGenero
            // 
            this.lblGenero.AutoSize = true;
            this.lblGenero.Location = new System.Drawing.Point(567, 17);
            this.lblGenero.Name = "lblGenero";
            this.lblGenero.Size = new System.Drawing.Size(57, 16);
            this.lblGenero.TabIndex = 87;
            this.lblGenero.Text = "Gênero*";
            // 
            // lblDataNascimento
            // 
            this.lblDataNascimento.AutoSize = true;
            this.lblDataNascimento.Location = new System.Drawing.Point(744, 17);
            this.lblDataNascimento.Name = "lblDataNascimento";
            this.lblDataNascimento.Size = new System.Drawing.Size(130, 16);
            this.lblDataNascimento.TabIndex = 89;
            this.lblDataNascimento.Text = "Data de Nascimento";
            // 
            // dtpNascimento
            // 
            this.dtpNascimento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNascimento.Location = new System.Drawing.Point(747, 36);
            this.dtpNascimento.Name = "dtpNascimento";
            this.dtpNascimento.Size = new System.Drawing.Size(160, 22);
            this.dtpNascimento.TabIndex = 4;
            this.dtpNascimento.Value = new System.DateTime(2025, 7, 10, 0, 0, 0, 0);
            // 
            // frmCadastroCliente
            // 
            //this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            //this.ClientSize = new System.Drawing.Size(1342, 681);
            this.Controls.Add(this.lblDataNascimento);
            this.Controls.Add(this.dtpNascimento);
            this.Controls.Add(this.cbGenero);
            this.Controls.Add(this.lblGenero);
            this.Controls.Add(this.IDCidade);
            this.Controls.Add(this.txtIdCidade);
            this.Controls.Add(this.txtCondicao);
            this.Controls.Add(this.btnBuscarCond);
            this.Controls.Add(this.txtCidade);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.lblRG);
            this.Controls.Add(this.txtRG);
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
            this.Controls.Add(this.lblCEP);
            this.Controls.Add(this.txtCEP);
            this.Controls.Add(this.lblTipo);
            this.Controls.Add(this.lblTelefone);
            this.Controls.Add(this.txtTelefone);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblCPF);
            this.Controls.Add(this.txtCPF);
            this.Controls.Add(this.lblCodigo);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.lblCidade);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.txtNome);
            this.Name = "frmCadastroCliente";
            this.Text = "Cadastro Clientes";
            this.Load += new System.EventHandler(this.frmCadastroCliente_Load);
            this.Controls.SetChildIndex(this.lblDataModificacao, 0);
            this.Controls.SetChildIndex(this.chkInativo, 0);
            this.Controls.SetChildIndex(this.lblDataCriacao, 0);
            this.Controls.SetChildIndex(this.btnSair, 0);
            this.Controls.SetChildIndex(this.txtCodigo, 0);
            this.Controls.SetChildIndex(this.txtNome, 0);
            this.Controls.SetChildIndex(this.lblNome, 0);
            this.Controls.SetChildIndex(this.lblCidade, 0);
            this.Controls.SetChildIndex(this.btnSalvar, 0);
            this.Controls.SetChildIndex(this.lblCodigo, 0);
            this.Controls.SetChildIndex(this.txtCPF, 0);
            this.Controls.SetChildIndex(this.lblCPF, 0);
            this.Controls.SetChildIndex(this.txtEmail, 0);
            this.Controls.SetChildIndex(this.lblEmail, 0);
            this.Controls.SetChildIndex(this.txtTelefone, 0);
            this.Controls.SetChildIndex(this.lblTelefone, 0);
            this.Controls.SetChildIndex(this.lblTipo, 0);
            this.Controls.SetChildIndex(this.txtCEP, 0);
            this.Controls.SetChildIndex(this.lblCEP, 0);
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
            this.Controls.SetChildIndex(this.txtRG, 0);
            this.Controls.SetChildIndex(this.lblRG, 0);
            this.Controls.SetChildIndex(this.btnBuscar, 0);
            this.Controls.SetChildIndex(this.txtCidade, 0);
            this.Controls.SetChildIndex(this.btnBuscarCond, 0);
            this.Controls.SetChildIndex(this.txtCondicao, 0);
            this.Controls.SetChildIndex(this.txtIdCidade, 0);
            this.Controls.SetChildIndex(this.IDCidade, 0);
            this.Controls.SetChildIndex(this.lblGenero, 0);
            this.Controls.SetChildIndex(this.cbGenero, 0);
            this.Controls.SetChildIndex(this.dtpNascimento, 0);
            this.Controls.SetChildIndex(this.lblDataNascimento, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCidade;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.Label lblCPF;
        private System.Windows.Forms.TextBox txtCPF;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblTelefone;
        private System.Windows.Forms.TextBox txtTelefone;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.Label lblCEP;
        private System.Windows.Forms.TextBox txtCEP;
        private System.Windows.Forms.Label lblEndereco;
        private System.Windows.Forms.TextBox txtEndereco;
        private System.Windows.Forms.Label lblBairro;
        private System.Windows.Forms.TextBox txtBairro;
        private System.Windows.Forms.Label lblNumEndereco;
        private System.Windows.Forms.TextBox txtNumEnd;
        private System.Windows.Forms.Label lblComplemento;
        private System.Windows.Forms.TextBox txtComplemento;
        private System.Windows.Forms.Label lblCondPgto;
        private System.Windows.Forms.ComboBox cbTipo;
        private System.Windows.Forms.Label lblRG;
        private System.Windows.Forms.TextBox txtRG;
        private System.Windows.Forms.TextBox txtCidade;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtCondicao;
        private System.Windows.Forms.Button btnBuscarCond;
        private System.Windows.Forms.Label IDCidade;
        private System.Windows.Forms.TextBox txtIdCidade;
        private System.Windows.Forms.ComboBox cbGenero;
        private System.Windows.Forms.Label lblGenero;
        private System.Windows.Forms.Label lblDataNascimento;
        private System.Windows.Forms.DateTimePicker dtpNascimento;
    }
}
