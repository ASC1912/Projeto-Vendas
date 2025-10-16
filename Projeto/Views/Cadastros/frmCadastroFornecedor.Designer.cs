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
            this.label1 = new System.Windows.Forms.Label();
            this.txtIdCondPgto = new System.Windows.Forms.TextBox();
            this.lblIdVeiculo = new System.Windows.Forms.Label();
            this.btnRemoverFornecedor = new System.Windows.Forms.Button();
            this.btnAdicionarFornecedor = new System.Windows.Forms.Button();
            this.txtIdVeiculo = new System.Windows.Forms.TextBox();
            this.listVFornecedores = new System.Windows.Forms.ListView();
            this.clmCodFornecedor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmNomeRazaoSocial = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmTipo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnPesquisarFornecedor = new System.Windows.Forms.Button();
            this.txtVeiculo = new System.Windows.Forms.TextBox();
            this.lblVeiculo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtCodigo
            // 
            this.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
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
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(1104, 634);
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
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
            this.lblCondPgto.Location = new System.Drawing.Point(633, 228);
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
            "FÍSICO",
            "JURÍDICO"});
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
            this.txtCondicao.Location = new System.Drawing.Point(636, 250);
            this.txtCondicao.Name = "txtCondicao";
            this.txtCondicao.ReadOnly = true;
            this.txtCondicao.Size = new System.Drawing.Size(200, 22);
            this.txtCondicao.TabIndex = 16;
            // 
            // btnBuscarCond
            // 
            this.btnBuscarCond.Location = new System.Drawing.Point(842, 249);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(542, 230);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 93;
            this.label1.Text = "ID CongPgto*";
            // 
            // txtIdCondPgto
            // 
            this.txtIdCondPgto.Location = new System.Drawing.Point(545, 249);
            this.txtIdCondPgto.Name = "txtIdCondPgto";
            this.txtIdCondPgto.Size = new System.Drawing.Size(67, 22);
            this.txtIdCondPgto.TabIndex = 92;
            this.txtIdCondPgto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtIdCondPgto.Leave += new System.EventHandler(this.txtIdCondPgto_Leave);
            // 
            // lblIdVeiculo
            // 
            this.lblIdVeiculo.AutoSize = true;
            this.lblIdVeiculo.Location = new System.Drawing.Point(16, 281);
            this.lblIdVeiculo.Name = "lblIdVeiculo";
            this.lblIdVeiculo.Size = new System.Drawing.Size(73, 16);
            this.lblIdVeiculo.TabIndex = 154;
            this.lblIdVeiculo.Text = "ID Veículo*";
            // 
            // btnRemoverFornecedor
            // 
            this.btnRemoverFornecedor.Location = new System.Drawing.Point(645, 296);
            this.btnRemoverFornecedor.Margin = new System.Windows.Forms.Padding(4);
            this.btnRemoverFornecedor.Name = "btnRemoverFornecedor";
            this.btnRemoverFornecedor.Size = new System.Drawing.Size(100, 28);
            this.btnRemoverFornecedor.TabIndex = 152;
            this.btnRemoverFornecedor.Text = "Remover";
            this.btnRemoverFornecedor.UseVisualStyleBackColor = true;
            // 
            // btnAdicionarFornecedor
            // 
            this.btnAdicionarFornecedor.Location = new System.Drawing.Point(538, 296);
            this.btnAdicionarFornecedor.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdicionarFornecedor.Name = "btnAdicionarFornecedor";
            this.btnAdicionarFornecedor.Size = new System.Drawing.Size(99, 28);
            this.btnAdicionarFornecedor.TabIndex = 153;
            this.btnAdicionarFornecedor.Text = "Adicionar ";
            this.btnAdicionarFornecedor.UseVisualStyleBackColor = true;
            // 
            // txtIdVeiculo
            // 
            this.txtIdVeiculo.Location = new System.Drawing.Point(19, 299);
            this.txtIdVeiculo.Margin = new System.Windows.Forms.Padding(4);
            this.txtIdVeiculo.Name = "txtIdVeiculo";
            this.txtIdVeiculo.ShortcutsEnabled = false;
            this.txtIdVeiculo.Size = new System.Drawing.Size(79, 22);
            this.txtIdVeiculo.TabIndex = 151;
            // 
            // listVFornecedores
            // 
            this.listVFornecedores.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmCodFornecedor,
            this.clmNomeRazaoSocial,
            this.clmTipo});
            this.listVFornecedores.FullRowSelect = true;
            this.listVFornecedores.GridLines = true;
            this.listVFornecedores.HideSelection = false;
            this.listVFornecedores.Location = new System.Drawing.Point(19, 332);
            this.listVFornecedores.Margin = new System.Windows.Forms.Padding(4);
            this.listVFornecedores.Name = "listVFornecedores";
            this.listVFornecedores.Size = new System.Drawing.Size(721, 232);
            this.listVFornecedores.TabIndex = 150;
            this.listVFornecedores.UseCompatibleStateImageBehavior = false;
            this.listVFornecedores.View = System.Windows.Forms.View.Details;
            // 
            // clmCodFornecedor
            // 
            this.clmCodFornecedor.Text = "Cód. Fornecedor";
            this.clmCodFornecedor.Width = 100;
            // 
            // clmNomeRazaoSocial
            // 
            this.clmNomeRazaoSocial.Text = "Nome/Razão Social";
            this.clmNomeRazaoSocial.Width = 200;
            // 
            // clmTipo
            // 
            this.clmTipo.Text = "Tipo";
            this.clmTipo.Width = 100;
            // 
            // btnPesquisarFornecedor
            // 
            this.btnPesquisarFornecedor.Location = new System.Drawing.Point(465, 296);
            this.btnPesquisarFornecedor.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnPesquisarFornecedor.Name = "btnPesquisarFornecedor";
            this.btnPesquisarFornecedor.Size = new System.Drawing.Size(65, 28);
            this.btnPesquisarFornecedor.TabIndex = 148;
            this.btnPesquisarFornecedor.Text = "🔎";
            this.btnPesquisarFornecedor.UseVisualStyleBackColor = true;
            // 
            // txtVeiculo
            // 
            this.txtVeiculo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtVeiculo.Location = new System.Drawing.Point(107, 299);
            this.txtVeiculo.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtVeiculo.Name = "txtVeiculo";
            this.txtVeiculo.ReadOnly = true;
            this.txtVeiculo.ShortcutsEnabled = false;
            this.txtVeiculo.Size = new System.Drawing.Size(351, 22);
            this.txtVeiculo.TabIndex = 147;
            // 
            // lblVeiculo
            // 
            this.lblVeiculo.AutoSize = true;
            this.lblVeiculo.Location = new System.Drawing.Point(104, 281);
            this.lblVeiculo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVeiculo.Name = "lblVeiculo";
            this.lblVeiculo.Size = new System.Drawing.Size(57, 16);
            this.lblVeiculo.TabIndex = 149;
            this.lblVeiculo.Text = "Veículo*";
            // 
            // frmCadastroFornecedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(1342, 681);
            this.Controls.Add(this.lblIdVeiculo);
            this.Controls.Add(this.btnRemoverFornecedor);
            this.Controls.Add(this.btnAdicionarFornecedor);
            this.Controls.Add(this.txtIdVeiculo);
            this.Controls.Add(this.listVFornecedores);
            this.Controls.Add(this.btnPesquisarFornecedor);
            this.Controls.Add(this.txtVeiculo);
            this.Controls.Add(this.lblVeiculo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtIdCondPgto);
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
            this.Controls.Add(this.lblCidade);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.txtNome);
            this.Name = "frmCadastroFornecedor";
            this.Text = "Cadastro Fornecedores";
            this.Load += new System.EventHandler(this.frmCadastroFornecedor_Load);
            this.Controls.SetChildIndex(this.btnSalvar, 0);
            this.Controls.SetChildIndex(this.lblDataModificacao, 0);
            this.Controls.SetChildIndex(this.chkInativo, 0);
            this.Controls.SetChildIndex(this.lblDataCriacao, 0);
            this.Controls.SetChildIndex(this.txtNome, 0);
            this.Controls.SetChildIndex(this.lblNome, 0);
            this.Controls.SetChildIndex(this.lblCidade, 0);
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
            this.Controls.SetChildIndex(this.txtIdCondPgto, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.lblVeiculo, 0);
            this.Controls.SetChildIndex(this.txtVeiculo, 0);
            this.Controls.SetChildIndex(this.btnPesquisarFornecedor, 0);
            this.Controls.SetChildIndex(this.listVFornecedores, 0);
            this.Controls.SetChildIndex(this.txtIdVeiculo, 0);
            this.Controls.SetChildIndex(this.btnAdicionarFornecedor, 0);
            this.Controls.SetChildIndex(this.btnRemoverFornecedor, 0);
            this.Controls.SetChildIndex(this.lblIdVeiculo, 0);
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIdCondPgto;
        private System.Windows.Forms.Label lblIdVeiculo;
        private System.Windows.Forms.Button btnRemoverFornecedor;
        private System.Windows.Forms.Button btnAdicionarFornecedor;
        private System.Windows.Forms.TextBox txtIdVeiculo;
        private System.Windows.Forms.ListView listVFornecedores;
        private System.Windows.Forms.ColumnHeader clmCodFornecedor;
        private System.Windows.Forms.ColumnHeader clmNomeRazaoSocial;
        private System.Windows.Forms.ColumnHeader clmTipo;
        private System.Windows.Forms.Button btnPesquisarFornecedor;
        private System.Windows.Forms.TextBox txtVeiculo;
        private System.Windows.Forms.Label lblVeiculo;
    }
}
