namespace Projeto.Views.Cadastros
{
    partial class frmCadastroProduto
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
            this.lblCodigo = new System.Windows.Forms.Label();
            this.lblNome = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.lblDescricao = new System.Windows.Forms.Label();
            this.txtUnidade = new System.Windows.Forms.TextBox();
            this.txtGrupo = new System.Windows.Forms.TextBox();
            this.btnBuscarGrupo = new System.Windows.Forms.Button();
            this.lblGrupo = new System.Windows.Forms.Label();
            this.txtMarca = new System.Windows.Forms.TextBox();
            this.btnMarca = new System.Windows.Forms.Button();
            this.lblMarca = new System.Windows.Forms.Label();
            this.lblPrecoCusto = new System.Windows.Forms.Label();
            this.txtPrecoCusto = new System.Windows.Forms.TextBox();
            this.lblEstoque = new System.Windows.Forms.Label();
            this.txtEstoque = new System.Windows.Forms.TextBox();
            this.lblValorVenda = new System.Windows.Forms.Label();
            this.txtPrecoVenda = new System.Windows.Forms.TextBox();
            this.btnUnidade = new System.Windows.Forms.Button();
            this.IDCidade = new System.Windows.Forms.Label();
            this.txtIdGrupo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIdUnidade = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIdMarca = new System.Windows.Forms.TextBox();
            this.lblPorcentagemLucro = new System.Windows.Forms.Label();
            this.txtPorcentagemLucro = new System.Windows.Forms.TextBox();
            this.txtCodFornecedor = new System.Windows.Forms.TextBox();
            this.listVFornecedores = new System.Windows.Forms.ListView();
            this.clmCodFornecedor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmNomeRazaoSocial = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmTipo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnPesquisarFornecedor = new System.Windows.Forms.Button();
            this.txtFornecedor = new System.Windows.Forms.TextBox();
            this.lblFornecedor = new System.Windows.Forms.Label();
            this.btnRemoverFornecedor = new System.Windows.Forms.Button();
            this.btnAdicionarFornecedor = new System.Windows.Forms.Button();
            this.lblIdFornecedor = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtCodigo
            // 
            this.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            // 
            // btnSair
            // 
            this.btnSair.TabIndex = 10;
            // 
            // chkInativo
            // 
            this.chkInativo.TabIndex = 8;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(1109, 634);
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Location = new System.Drawing.Point(9, 9);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(51, 16);
            this.lblCodigo.TabIndex = 66;
            this.lblCodigo.Text = "Código";
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Location = new System.Drawing.Point(9, 72);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(59, 16);
            this.lblNome.TabIndex = 68;
            this.lblNome.Text = "Produto*";
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(12, 91);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(200, 22);
            this.txtNome.TabIndex = 0;
            // 
            // lblDescricao
            // 
            this.lblDescricao.AutoSize = true;
            this.lblDescricao.Location = new System.Drawing.Point(96, 137);
            this.lblDescricao.Name = "lblDescricao";
            this.lblDescricao.Size = new System.Drawing.Size(59, 16);
            this.lblDescricao.TabIndex = 70;
            this.lblDescricao.Text = "Unidade";
            // 
            // txtUnidade
            // 
            this.txtUnidade.Location = new System.Drawing.Point(99, 156);
            this.txtUnidade.Name = "txtUnidade";
            this.txtUnidade.ReadOnly = true;
            this.txtUnidade.Size = new System.Drawing.Size(200, 22);
            this.txtUnidade.TabIndex = 1;
            // 
            // txtGrupo
            // 
            this.txtGrupo.Location = new System.Drawing.Point(91, 352);
            this.txtGrupo.Name = "txtGrupo";
            this.txtGrupo.ReadOnly = true;
            this.txtGrupo.Size = new System.Drawing.Size(200, 22);
            this.txtGrupo.TabIndex = 2;
            // 
            // btnBuscarGrupo
            // 
            this.btnBuscarGrupo.Location = new System.Drawing.Point(324, 351);
            this.btnBuscarGrupo.Name = "btnBuscarGrupo";
            this.btnBuscarGrupo.Size = new System.Drawing.Size(150, 23);
            this.btnBuscarGrupo.TabIndex = 3;
            this.btnBuscarGrupo.Text = "Buscar Grupo";
            this.btnBuscarGrupo.UseVisualStyleBackColor = true;
            this.btnBuscarGrupo.Click += new System.EventHandler(this.btnBuscarGrupo_Click);
            // 
            // lblGrupo
            // 
            this.lblGrupo.AutoSize = true;
            this.lblGrupo.Location = new System.Drawing.Point(88, 332);
            this.lblGrupo.Name = "lblGrupo";
            this.lblGrupo.Size = new System.Drawing.Size(49, 16);
            this.lblGrupo.TabIndex = 72;
            this.lblGrupo.Text = "Grupo*";
            // 
            // txtMarca
            // 
            this.txtMarca.Location = new System.Drawing.Point(91, 427);
            this.txtMarca.Name = "txtMarca";
            this.txtMarca.ReadOnly = true;
            this.txtMarca.Size = new System.Drawing.Size(200, 22);
            this.txtMarca.TabIndex = 5;
            // 
            // btnMarca
            // 
            this.btnMarca.Location = new System.Drawing.Point(324, 426);
            this.btnMarca.Name = "btnMarca";
            this.btnMarca.Size = new System.Drawing.Size(150, 23);
            this.btnMarca.TabIndex = 6;
            this.btnMarca.Text = "Buscar Marca";
            this.btnMarca.UseVisualStyleBackColor = true;
            this.btnMarca.Click += new System.EventHandler(this.btnMarca_Click);
            // 
            // lblMarca
            // 
            this.lblMarca.AutoSize = true;
            this.lblMarca.Location = new System.Drawing.Point(88, 407);
            this.lblMarca.Name = "lblMarca";
            this.lblMarca.Size = new System.Drawing.Size(45, 16);
            this.lblMarca.TabIndex = 75;
            this.lblMarca.Text = "Marca";
            // 
            // lblPrecoCusto
            // 
            this.lblPrecoCusto.AutoSize = true;
            this.lblPrecoCusto.Location = new System.Drawing.Point(12, 213);
            this.lblPrecoCusto.Name = "lblPrecoCusto";
            this.lblPrecoCusto.Size = new System.Drawing.Size(132, 16);
            this.lblPrecoCusto.TabIndex = 79;
            this.lblPrecoCusto.Text = "Preço de Custo (R$)*";
            // 
            // txtPrecoCusto
            // 
            this.txtPrecoCusto.Location = new System.Drawing.Point(15, 232);
            this.txtPrecoCusto.Name = "txtPrecoCusto";
            this.txtPrecoCusto.Size = new System.Drawing.Size(150, 22);
            this.txtPrecoCusto.TabIndex = 4;
            // 
            // lblEstoque
            // 
            this.lblEstoque.AutoSize = true;
            this.lblEstoque.Location = new System.Drawing.Point(12, 277);
            this.lblEstoque.Name = "lblEstoque";
            this.lblEstoque.Size = new System.Drawing.Size(62, 16);
            this.lblEstoque.TabIndex = 81;
            this.lblEstoque.Text = "Estoque*";
            // 
            // txtEstoque
            // 
            this.txtEstoque.Location = new System.Drawing.Point(15, 296);
            this.txtEstoque.Name = "txtEstoque";
            this.txtEstoque.Size = new System.Drawing.Size(150, 22);
            this.txtEstoque.TabIndex = 7;
            // 
            // lblValorVenda
            // 
            this.lblValorVenda.AutoSize = true;
            this.lblValorVenda.Location = new System.Drawing.Point(215, 209);
            this.lblValorVenda.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblValorVenda.Name = "lblValorVenda";
            this.lblValorVenda.Size = new System.Drawing.Size(141, 16);
            this.lblValorVenda.TabIndex = 127;
            this.lblValorVenda.Text = "Preço de Venda (R$) *";
            // 
            // txtPrecoVenda
            // 
            this.txtPrecoVenda.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPrecoVenda.Location = new System.Drawing.Point(219, 232);
            this.txtPrecoVenda.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtPrecoVenda.Name = "txtPrecoVenda";
            this.txtPrecoVenda.ShortcutsEnabled = false;
            this.txtPrecoVenda.Size = new System.Drawing.Size(163, 22);
            this.txtPrecoVenda.TabIndex = 126;
            // 
            // btnUnidade
            // 
            this.btnUnidade.Location = new System.Drawing.Point(305, 156);
            this.btnUnidade.Name = "btnUnidade";
            this.btnUnidade.Size = new System.Drawing.Size(150, 23);
            this.btnUnidade.TabIndex = 128;
            this.btnUnidade.Text = "Buscar Unidade";
            this.btnUnidade.UseVisualStyleBackColor = true;
            this.btnUnidade.Click += new System.EventHandler(this.btnUnidade_Click);
            // 
            // IDCidade
            // 
            this.IDCidade.AutoSize = true;
            this.IDCidade.Location = new System.Drawing.Point(12, 334);
            this.IDCidade.Name = "IDCidade";
            this.IDCidade.Size = new System.Drawing.Size(65, 16);
            this.IDCidade.TabIndex = 130;
            this.IDCidade.Text = "ID Grupo*";
            // 
            // txtIdGrupo
            // 
            this.txtIdGrupo.Location = new System.Drawing.Point(15, 353);
            this.txtIdGrupo.Name = "txtIdGrupo";
            this.txtIdGrupo.Size = new System.Drawing.Size(67, 22);
            this.txtIdGrupo.TabIndex = 129;
            this.txtIdGrupo.Leave += new System.EventHandler(this.txtIdGrupo_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 132;
            this.label1.Text = "ID Unidade*";
            // 
            // txtIdUnidade
            // 
            this.txtIdUnidade.Location = new System.Drawing.Point(14, 156);
            this.txtIdUnidade.Name = "txtIdUnidade";
            this.txtIdUnidade.Size = new System.Drawing.Size(67, 22);
            this.txtIdUnidade.TabIndex = 131;
            this.txtIdUnidade.Leave += new System.EventHandler(this.txtIdUnidade_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 408);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 16);
            this.label2.TabIndex = 134;
            this.label2.Text = "ID Marca*";
            // 
            // txtIdMarca
            // 
            this.txtIdMarca.Location = new System.Drawing.Point(15, 427);
            this.txtIdMarca.Name = "txtIdMarca";
            this.txtIdMarca.Size = new System.Drawing.Size(67, 22);
            this.txtIdMarca.TabIndex = 133;
            this.txtIdMarca.Leave += new System.EventHandler(this.txtIdMarca_Leave);
            // 
            // lblPorcentagemLucro
            // 
            this.lblPorcentagemLucro.AutoSize = true;
            this.lblPorcentagemLucro.Location = new System.Drawing.Point(215, 273);
            this.lblPorcentagemLucro.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPorcentagemLucro.Name = "lblPorcentagemLucro";
            this.lblPorcentagemLucro.Size = new System.Drawing.Size(143, 16);
            this.lblPorcentagemLucro.TabIndex = 137;
            this.lblPorcentagemLucro.Text = "Margem de Lucro (%) *";
            // 
            // txtPorcentagemLucro
            // 
            this.txtPorcentagemLucro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPorcentagemLucro.Location = new System.Drawing.Point(219, 296);
            this.txtPorcentagemLucro.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtPorcentagemLucro.Name = "txtPorcentagemLucro";
            this.txtPorcentagemLucro.ShortcutsEnabled = false;
            this.txtPorcentagemLucro.Size = new System.Drawing.Size(257, 22);
            this.txtPorcentagemLucro.TabIndex = 135;
            this.txtPorcentagemLucro.Text = "0";
            // 
            // txtCodFornecedor
            // 
            this.txtCodFornecedor.Location = new System.Drawing.Point(574, 83);
            this.txtCodFornecedor.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodFornecedor.Name = "txtCodFornecedor";
            this.txtCodFornecedor.ShortcutsEnabled = false;
            this.txtCodFornecedor.Size = new System.Drawing.Size(79, 22);
            this.txtCodFornecedor.TabIndex = 143;
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
            this.listVFornecedores.Location = new System.Drawing.Point(579, 111);
            this.listVFornecedores.Margin = new System.Windows.Forms.Padding(4);
            this.listVFornecedores.Name = "listVFornecedores";
            this.listVFornecedores.Size = new System.Drawing.Size(721, 462);
            this.listVFornecedores.TabIndex = 142;
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
            this.btnPesquisarFornecedor.Location = new System.Drawing.Point(1020, 80);
            this.btnPesquisarFornecedor.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnPesquisarFornecedor.Name = "btnPesquisarFornecedor";
            this.btnPesquisarFornecedor.Size = new System.Drawing.Size(65, 28);
            this.btnPesquisarFornecedor.TabIndex = 140;
            this.btnPesquisarFornecedor.Text = "🔎";
            this.btnPesquisarFornecedor.UseVisualStyleBackColor = true;
            this.btnPesquisarFornecedor.Click += new System.EventHandler(this.btnPesquisarFornecedor_Click);
            // 
            // txtFornecedor
            // 
            this.txtFornecedor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFornecedor.Location = new System.Drawing.Point(662, 83);
            this.txtFornecedor.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtFornecedor.Name = "txtFornecedor";
            this.txtFornecedor.ReadOnly = true;
            this.txtFornecedor.ShortcutsEnabled = false;
            this.txtFornecedor.Size = new System.Drawing.Size(351, 22);
            this.txtFornecedor.TabIndex = 139;
            // 
            // lblFornecedor
            // 
            this.lblFornecedor.AutoSize = true;
            this.lblFornecedor.Location = new System.Drawing.Point(659, 65);
            this.lblFornecedor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFornecedor.Name = "lblFornecedor";
            this.lblFornecedor.Size = new System.Drawing.Size(85, 16);
            this.lblFornecedor.TabIndex = 141;
            this.lblFornecedor.Text = "Fornecedor *";
            // 
            // btnRemoverFornecedor
            // 
            this.btnRemoverFornecedor.Location = new System.Drawing.Point(1200, 80);
            this.btnRemoverFornecedor.Margin = new System.Windows.Forms.Padding(4);
            this.btnRemoverFornecedor.Name = "btnRemoverFornecedor";
            this.btnRemoverFornecedor.Size = new System.Drawing.Size(100, 28);
            this.btnRemoverFornecedor.TabIndex = 144;
            this.btnRemoverFornecedor.Text = "Remover";
            this.btnRemoverFornecedor.UseVisualStyleBackColor = true;
            this.btnRemoverFornecedor.Click += new System.EventHandler(this.btnRemoverFornecedor_Click);
            // 
            // btnAdicionarFornecedor
            // 
            this.btnAdicionarFornecedor.Location = new System.Drawing.Point(1093, 80);
            this.btnAdicionarFornecedor.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdicionarFornecedor.Name = "btnAdicionarFornecedor";
            this.btnAdicionarFornecedor.Size = new System.Drawing.Size(99, 28);
            this.btnAdicionarFornecedor.TabIndex = 145;
            this.btnAdicionarFornecedor.Text = "Adicionar ";
            this.btnAdicionarFornecedor.UseVisualStyleBackColor = true;
            this.btnAdicionarFornecedor.Click += new System.EventHandler(this.btnAdicionarFornecedor_Click);
            // 
            // lblIdFornecedor
            // 
            this.lblIdFornecedor.AutoSize = true;
            this.lblIdFornecedor.Location = new System.Drawing.Point(571, 65);
            this.lblIdFornecedor.Name = "lblIdFornecedor";
            this.lblIdFornecedor.Size = new System.Drawing.Size(55, 16);
            this.lblIdFornecedor.TabIndex = 146;
            this.lblIdFornecedor.Text = "ID Forn*";
            // 
            // frmCadastroProduto
            // 
            //this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            //this.ClientSize = new System.Drawing.Size(1342, 681);
            this.Controls.Add(this.lblIdFornecedor);
            this.Controls.Add(this.btnRemoverFornecedor);
            this.Controls.Add(this.btnAdicionarFornecedor);
            this.Controls.Add(this.txtCodFornecedor);
            this.Controls.Add(this.listVFornecedores);
            this.Controls.Add(this.btnPesquisarFornecedor);
            this.Controls.Add(this.txtFornecedor);
            this.Controls.Add(this.lblFornecedor);
            this.Controls.Add(this.lblPorcentagemLucro);
            this.Controls.Add(this.txtPorcentagemLucro);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtIdMarca);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtIdUnidade);
            this.Controls.Add(this.IDCidade);
            this.Controls.Add(this.txtIdGrupo);
            this.Controls.Add(this.btnUnidade);
            this.Controls.Add(this.lblValorVenda);
            this.Controls.Add(this.txtPrecoVenda);
            this.Controls.Add(this.lblEstoque);
            this.Controls.Add(this.txtEstoque);
            this.Controls.Add(this.lblPrecoCusto);
            this.Controls.Add(this.txtPrecoCusto);
            this.Controls.Add(this.txtMarca);
            this.Controls.Add(this.btnMarca);
            this.Controls.Add(this.lblMarca);
            this.Controls.Add(this.txtGrupo);
            this.Controls.Add(this.btnBuscarGrupo);
            this.Controls.Add(this.lblGrupo);
            this.Controls.Add(this.lblDescricao);
            this.Controls.Add(this.txtUnidade);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.lblCodigo);
            this.Name = "frmCadastroProduto";
            this.Text = "Cadastro Produtos";
            this.Load += new System.EventHandler(this.frmCadastroProduto_Load);
            this.Controls.SetChildIndex(this.btnSalvar, 0);
            this.Controls.SetChildIndex(this.btnSair, 0);
            this.Controls.SetChildIndex(this.txtCodigo, 0);
            this.Controls.SetChildIndex(this.chkInativo, 0);
            this.Controls.SetChildIndex(this.lblDataCriacao, 0);
            this.Controls.SetChildIndex(this.lblDataModificacao, 0);
            this.Controls.SetChildIndex(this.lblCodigo, 0);
            this.Controls.SetChildIndex(this.txtNome, 0);
            this.Controls.SetChildIndex(this.lblNome, 0);
            this.Controls.SetChildIndex(this.txtUnidade, 0);
            this.Controls.SetChildIndex(this.lblDescricao, 0);
            this.Controls.SetChildIndex(this.lblGrupo, 0);
            this.Controls.SetChildIndex(this.btnBuscarGrupo, 0);
            this.Controls.SetChildIndex(this.txtGrupo, 0);
            this.Controls.SetChildIndex(this.lblMarca, 0);
            this.Controls.SetChildIndex(this.btnMarca, 0);
            this.Controls.SetChildIndex(this.txtMarca, 0);
            this.Controls.SetChildIndex(this.txtPrecoCusto, 0);
            this.Controls.SetChildIndex(this.lblPrecoCusto, 0);
            this.Controls.SetChildIndex(this.txtEstoque, 0);
            this.Controls.SetChildIndex(this.lblEstoque, 0);
            this.Controls.SetChildIndex(this.txtPrecoVenda, 0);
            this.Controls.SetChildIndex(this.lblValorVenda, 0);
            this.Controls.SetChildIndex(this.btnUnidade, 0);
            this.Controls.SetChildIndex(this.txtIdGrupo, 0);
            this.Controls.SetChildIndex(this.IDCidade, 0);
            this.Controls.SetChildIndex(this.txtIdUnidade, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.txtIdMarca, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtPorcentagemLucro, 0);
            this.Controls.SetChildIndex(this.lblPorcentagemLucro, 0);
            this.Controls.SetChildIndex(this.lblFornecedor, 0);
            this.Controls.SetChildIndex(this.txtFornecedor, 0);
            this.Controls.SetChildIndex(this.btnPesquisarFornecedor, 0);
            this.Controls.SetChildIndex(this.listVFornecedores, 0);
            this.Controls.SetChildIndex(this.txtCodFornecedor, 0);
            this.Controls.SetChildIndex(this.btnAdicionarFornecedor, 0);
            this.Controls.SetChildIndex(this.btnRemoverFornecedor, 0);
            this.Controls.SetChildIndex(this.lblIdFornecedor, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lblDescricao;
        private System.Windows.Forms.TextBox txtUnidade;
        private System.Windows.Forms.TextBox txtGrupo;
        private System.Windows.Forms.Button btnBuscarGrupo;
        private System.Windows.Forms.Label lblGrupo;
        private System.Windows.Forms.TextBox txtMarca;
        private System.Windows.Forms.Button btnMarca;
        private System.Windows.Forms.Label lblMarca;
        private System.Windows.Forms.Label lblPrecoCusto;
        private System.Windows.Forms.TextBox txtPrecoCusto;
        private System.Windows.Forms.Label lblEstoque;
        private System.Windows.Forms.TextBox txtEstoque;
        private System.Windows.Forms.Label lblValorVenda;
        private System.Windows.Forms.TextBox txtPrecoVenda;
        private System.Windows.Forms.Button btnUnidade;
        private System.Windows.Forms.Label IDCidade;
        private System.Windows.Forms.TextBox txtIdGrupo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIdUnidade;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIdMarca;
        private System.Windows.Forms.Label lblPorcentagemLucro;
        private System.Windows.Forms.TextBox txtPorcentagemLucro;
        private System.Windows.Forms.TextBox txtCodFornecedor;
        private System.Windows.Forms.ListView listVFornecedores;
        private System.Windows.Forms.ColumnHeader clmCodFornecedor;
        private System.Windows.Forms.ColumnHeader clmNomeRazaoSocial;
        private System.Windows.Forms.ColumnHeader clmTipo;
        private System.Windows.Forms.Button btnPesquisarFornecedor;
        private System.Windows.Forms.TextBox txtFornecedor;
        private System.Windows.Forms.Label lblFornecedor;
        private System.Windows.Forms.Button btnRemoverFornecedor;
        private System.Windows.Forms.Button btnAdicionarFornecedor;
        private System.Windows.Forms.Label lblIdFornecedor;
    }
}
