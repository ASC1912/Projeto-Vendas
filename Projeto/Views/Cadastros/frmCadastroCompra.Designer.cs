namespace Projeto.Views.Cadastros
{
    partial class frmCadastroCompra
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
            this.lblModelo = new System.Windows.Forms.Label();
            this.btnPesquisarFornecedor = new System.Windows.Forms.Button();
            this.lblFornecedor = new System.Windows.Forms.Label();
            this.txtFornecedor = new System.Windows.Forms.TextBox();
            this.lblIdFornecedor = new System.Windows.Forms.Label();
            this.txtIDFornecedor = new System.Windows.Forms.TextBox();
            this.lblSerie = new System.Windows.Forms.Label();
            this.lblNumero = new System.Windows.Forms.Label();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.txtSerie = new System.Windows.Forms.TextBox();
            this.lblDataChegada = new System.Windows.Forms.Label();
            this.dtpChegada = new System.Windows.Forms.DateTimePicker();
            this.lblDataEmissao = new System.Windows.Forms.Label();
            this.dtpEmissao = new System.Windows.Forms.DateTimePicker();
            this.listViewProdutos = new System.Windows.Forms.ListView();
            this.Codigo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Produto = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Unidade = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Quantidade = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PrecoUnitario = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Total = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblProduto = new System.Windows.Forms.Label();
            this.txtProduto = new System.Windows.Forms.TextBox();
            this.btnPesquisarProduto = new System.Windows.Forms.Button();
            this.lblTotalProduto = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.lblValorUnitario = new System.Windows.Forms.Label();
            this.txtValorUnitario = new System.Windows.Forms.TextBox();
            this.lblQuantidade = new System.Windows.Forms.Label();
            this.txtQuantidade = new System.Windows.Forms.TextBox();
            this.btnAdicionarProduto = new System.Windows.Forms.Button();
            this.btnEditarProduto = new System.Windows.Forms.Button();
            this.btnRemoverProduto = new System.Windows.Forms.Button();
            this.btnLimparProduto = new System.Windows.Forms.Button();
            this.lblDespesas = new System.Windows.Forms.Label();
            this.txtDespesas = new System.Windows.Forms.TextBox();
            this.lblSeguro = new System.Windows.Forms.Label();
            this.txtSeguro = new System.Windows.Forms.TextBox();
            this.lblFrete = new System.Windows.Forms.Label();
            this.txtFrete = new System.Windows.Forms.TextBox();
            this.btnLimparCondPgto = new System.Windows.Forms.Button();
            this.txtCondPgto = new System.Windows.Forms.TextBox();
            this.btnAdicionarCondPgto = new System.Windows.Forms.Button();
            this.listViewCondPgto = new System.Windows.Forms.ListView();
            this.NumParcela = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Prazo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FormaPagamento = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Valor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblFormaPagamento = new System.Windows.Forms.Label();
            this.lblTotalProdutos = new System.Windows.Forms.Label();
            this.lblValorTotal = new System.Windows.Forms.Label();
            this.txtValorTotal = new System.Windows.Forms.TextBox();
            this.lblTotalCondiçãoPgto = new System.Windows.Forms.Label();
            this.lblIdProduto = new System.Windows.Forms.Label();
            this.txtIdProduto = new System.Windows.Forms.TextBox();
            this.lblIdCondicaoPgto = new System.Windows.Forms.Label();
            this.txtIdCondPgto = new System.Windows.Forms.TextBox();
            this.lblMotivoCancelamento = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtCodigo
            // 
            this.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodigo.Location = new System.Drawing.Point(12, 43);
            this.txtCodigo.MaxLength = 5;
            this.txtCodigo.TabIndex = 0;
            // 
            // btnSair
            // 
            this.btnSair.Location = new System.Drawing.Point(1232, 637);
            this.btnSair.TabIndex = 28;
            // 
            // chkInativo
            // 
            this.chkInativo.Location = new System.Drawing.Point(1232, 12);
            this.chkInativo.TabIndex = 26;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(1126, 637);
            this.btnSalvar.TabIndex = 27;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // lblModelo
            // 
            this.lblModelo.AutoSize = true;
            this.lblModelo.Location = new System.Drawing.Point(12, 25);
            this.lblModelo.Name = "lblModelo";
            this.lblModelo.Size = new System.Drawing.Size(53, 16);
            this.lblModelo.TabIndex = 98;
            this.lblModelo.Text = "Modelo";
            // 
            // btnPesquisarFornecedor
            // 
            this.btnPesquisarFornecedor.Location = new System.Drawing.Point(699, 43);
            this.btnPesquisarFornecedor.Name = "btnPesquisarFornecedor";
            this.btnPesquisarFornecedor.Size = new System.Drawing.Size(91, 23);
            this.btnPesquisarFornecedor.TabIndex = 5;
            this.btnPesquisarFornecedor.Text = "Pesquisar";
            this.btnPesquisarFornecedor.UseVisualStyleBackColor = true;
            this.btnPesquisarFornecedor.Click += new System.EventHandler(this.btnPesquisarFornecedor_Click);
            // 
            // lblFornecedor
            // 
            this.lblFornecedor.AutoSize = true;
            this.lblFornecedor.Location = new System.Drawing.Point(482, 24);
            this.lblFornecedor.Name = "lblFornecedor";
            this.lblFornecedor.Size = new System.Drawing.Size(77, 16);
            this.lblFornecedor.TabIndex = 115;
            this.lblFornecedor.Text = "Fornecedor";
            // 
            // txtFornecedor
            // 
            this.txtFornecedor.Location = new System.Drawing.Point(482, 43);
            this.txtFornecedor.Name = "txtFornecedor";
            this.txtFornecedor.ReadOnly = true;
            this.txtFornecedor.Size = new System.Drawing.Size(211, 22);
            this.txtFornecedor.TabIndex = 4;
            // 
            // lblIdFornecedor
            // 
            this.lblIdFornecedor.AutoSize = true;
            this.lblIdFornecedor.Location = new System.Drawing.Point(416, 24);
            this.lblIdFornecedor.Name = "lblIdFornecedor";
            this.lblIdFornecedor.Size = new System.Drawing.Size(53, 16);
            this.lblIdFornecedor.TabIndex = 113;
            this.lblIdFornecedor.Text = "ID Forn.";
            // 
            // txtIDFornecedor
            // 
            this.txtIDFornecedor.Location = new System.Drawing.Point(416, 43);
            this.txtIDFornecedor.Name = "txtIDFornecedor";
            this.txtIDFornecedor.Size = new System.Drawing.Size(48, 22);
            this.txtIDFornecedor.TabIndex = 3;
            this.txtIDFornecedor.Leave += new System.EventHandler(this.txtIDFornecedor_Leave);
            // 
            // lblSerie
            // 
            this.lblSerie.AutoSize = true;
            this.lblSerie.Location = new System.Drawing.Point(150, 24);
            this.lblSerie.Name = "lblSerie";
            this.lblSerie.Size = new System.Drawing.Size(39, 16);
            this.lblSerie.TabIndex = 111;
            this.lblSerie.Text = "Série";
            // 
            // lblNumero
            // 
            this.lblNumero.AutoSize = true;
            this.lblNumero.Location = new System.Drawing.Point(290, 24);
            this.lblNumero.Name = "lblNumero";
            this.lblNumero.Size = new System.Drawing.Size(55, 16);
            this.lblNumero.TabIndex = 110;
            this.lblNumero.Text = "Número";
            // 
            // txtNumero
            // 
            this.txtNumero.Location = new System.Drawing.Point(290, 43);
            this.txtNumero.MaxLength = 100;
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(100, 22);
            this.txtNumero.TabIndex = 2;
            // 
            // txtSerie
            // 
            this.txtSerie.Location = new System.Drawing.Point(150, 43);
            this.txtSerie.Name = "txtSerie";
            this.txtSerie.Size = new System.Drawing.Size(100, 22);
            this.txtSerie.TabIndex = 1;
            // 
            // lblDataChegada
            // 
            this.lblDataChegada.AutoSize = true;
            this.lblDataChegada.Location = new System.Drawing.Point(1025, 25);
            this.lblDataChegada.Name = "lblDataChegada";
            this.lblDataChegada.Size = new System.Drawing.Size(114, 16);
            this.lblDataChegada.TabIndex = 120;
            this.lblDataChegada.Text = "Data de Chegada";
            // 
            // dtpChegada
            // 
            this.dtpChegada.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpChegada.Location = new System.Drawing.Point(1028, 44);
            this.dtpChegada.Name = "dtpChegada";
            this.dtpChegada.Size = new System.Drawing.Size(160, 22);
            this.dtpChegada.TabIndex = 7;
            this.dtpChegada.Value = new System.DateTime(2025, 7, 10, 0, 0, 0, 0);
            // 
            // lblDataEmissao
            // 
            this.lblDataEmissao.AutoSize = true;
            this.lblDataEmissao.Location = new System.Drawing.Point(808, 25);
            this.lblDataEmissao.Name = "lblDataEmissao";
            this.lblDataEmissao.Size = new System.Drawing.Size(111, 16);
            this.lblDataEmissao.TabIndex = 118;
            this.lblDataEmissao.Text = "Data de Emissão";
            // 
            // dtpEmissao
            // 
            this.dtpEmissao.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEmissao.Location = new System.Drawing.Point(811, 44);
            this.dtpEmissao.Name = "dtpEmissao";
            this.dtpEmissao.Size = new System.Drawing.Size(160, 22);
            this.dtpEmissao.TabIndex = 6;
            this.dtpEmissao.Value = new System.DateTime(2025, 7, 10, 0, 0, 0, 0);
            // 
            // listViewProdutos
            // 
            this.listViewProdutos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Codigo,
            this.Produto,
            this.Unidade,
            this.Quantidade,
            this.PrecoUnitario,
            this.Total});
            this.listViewProdutos.FullRowSelect = true;
            this.listViewProdutos.GridLines = true;
            this.listViewProdutos.HideSelection = false;
            this.listViewProdutos.Location = new System.Drawing.Point(18, 171);
            this.listViewProdutos.Name = "listViewProdutos";
            this.listViewProdutos.Size = new System.Drawing.Size(1230, 163);
            this.listViewProdutos.TabIndex = 121;
            this.listViewProdutos.UseCompatibleStateImageBehavior = false;
            this.listViewProdutos.View = System.Windows.Forms.View.Details;
            // 
            // Codigo
            // 
            this.Codigo.Text = "Código";
            this.Codigo.Width = 99;
            // 
            // Produto
            // 
            this.Produto.Text = "Produto";
            this.Produto.Width = 93;
            // 
            // Unidade
            // 
            this.Unidade.Text = "Unidade";
            this.Unidade.Width = 100;
            // 
            // Quantidade
            // 
            this.Quantidade.Text = "Qtd";
            this.Quantidade.Width = 48;
            // 
            // PrecoUnitario
            // 
            this.PrecoUnitario.Text = "Preço Unitário";
            this.PrecoUnitario.Width = 111;
            // 
            // Total
            // 
            this.Total.Text = "Total";
            // 
            // lblProduto
            // 
            this.lblProduto.AutoSize = true;
            this.lblProduto.Location = new System.Drawing.Point(81, 93);
            this.lblProduto.Name = "lblProduto";
            this.lblProduto.Size = new System.Drawing.Size(54, 16);
            this.lblProduto.TabIndex = 123;
            this.lblProduto.Text = "Produto";
            // 
            // txtProduto
            // 
            this.txtProduto.Location = new System.Drawing.Point(81, 112);
            this.txtProduto.Name = "txtProduto";
            this.txtProduto.ReadOnly = true;
            this.txtProduto.Size = new System.Drawing.Size(211, 22);
            this.txtProduto.TabIndex = 9;
            // 
            // btnPesquisarProduto
            // 
            this.btnPesquisarProduto.Location = new System.Drawing.Point(320, 111);
            this.btnPesquisarProduto.Name = "btnPesquisarProduto";
            this.btnPesquisarProduto.Size = new System.Drawing.Size(91, 23);
            this.btnPesquisarProduto.TabIndex = 10;
            this.btnPesquisarProduto.Text = "Pesquisar";
            this.btnPesquisarProduto.UseVisualStyleBackColor = true;
            this.btnPesquisarProduto.Click += new System.EventHandler(this.btnPesquisarProduto_Click);
            // 
            // lblTotalProduto
            // 
            this.lblTotalProduto.AutoSize = true;
            this.lblTotalProduto.Location = new System.Drawing.Point(640, 91);
            this.lblTotalProduto.Name = "lblTotalProduto";
            this.lblTotalProduto.Size = new System.Drawing.Size(66, 16);
            this.lblTotalProduto.TabIndex = 130;
            this.lblTotalProduto.Text = "Total (R$)";
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(640, 110);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(126, 22);
            this.txtTotal.TabIndex = 13;
            // 
            // lblValorUnitario
            // 
            this.lblValorUnitario.AutoSize = true;
            this.lblValorUnitario.Location = new System.Drawing.Point(546, 92);
            this.lblValorUnitario.Name = "lblValorUnitario";
            this.lblValorUnitario.Size = new System.Drawing.Size(88, 16);
            this.lblValorUnitario.TabIndex = 128;
            this.lblValorUnitario.Text = "Valor Unitário";
            // 
            // txtValorUnitario
            // 
            this.txtValorUnitario.Location = new System.Drawing.Point(546, 111);
            this.txtValorUnitario.Name = "txtValorUnitario";
            this.txtValorUnitario.Size = new System.Drawing.Size(88, 22);
            this.txtValorUnitario.TabIndex = 12;
            this.txtValorUnitario.TextChanged += new System.EventHandler(this.txtValorUnitario_TextChanged);
            // 
            // lblQuantidade
            // 
            this.lblQuantidade.AutoSize = true;
            this.lblQuantidade.Location = new System.Drawing.Point(420, 92);
            this.lblQuantidade.Name = "lblQuantidade";
            this.lblQuantidade.Size = new System.Drawing.Size(77, 16);
            this.lblQuantidade.TabIndex = 126;
            this.lblQuantidade.Text = "Quantidade";
            // 
            // txtQuantidade
            // 
            this.txtQuantidade.Location = new System.Drawing.Point(420, 111);
            this.txtQuantidade.Name = "txtQuantidade";
            this.txtQuantidade.Size = new System.Drawing.Size(100, 22);
            this.txtQuantidade.TabIndex = 11;
            this.txtQuantidade.TextChanged += new System.EventHandler(this.txtQuantidade_TextChanged);
            // 
            // btnAdicionarProduto
            // 
            this.btnAdicionarProduto.Location = new System.Drawing.Point(793, 111);
            this.btnAdicionarProduto.Name = "btnAdicionarProduto";
            this.btnAdicionarProduto.Size = new System.Drawing.Size(91, 23);
            this.btnAdicionarProduto.TabIndex = 14;
            this.btnAdicionarProduto.Text = "Adicionar";
            this.btnAdicionarProduto.UseVisualStyleBackColor = true;
            this.btnAdicionarProduto.Click += new System.EventHandler(this.btnAdicionarProduto_Click);
            // 
            // btnEditarProduto
            // 
            this.btnEditarProduto.Location = new System.Drawing.Point(906, 111);
            this.btnEditarProduto.Name = "btnEditarProduto";
            this.btnEditarProduto.Size = new System.Drawing.Size(91, 23);
            this.btnEditarProduto.TabIndex = 15;
            this.btnEditarProduto.Text = "Editar";
            this.btnEditarProduto.UseVisualStyleBackColor = true;
            // 
            // btnRemoverProduto
            // 
            this.btnRemoverProduto.Location = new System.Drawing.Point(1028, 111);
            this.btnRemoverProduto.Name = "btnRemoverProduto";
            this.btnRemoverProduto.Size = new System.Drawing.Size(91, 23);
            this.btnRemoverProduto.TabIndex = 16;
            this.btnRemoverProduto.Text = "Remover";
            this.btnRemoverProduto.UseVisualStyleBackColor = true;
            this.btnRemoverProduto.Click += new System.EventHandler(this.btnRemoverProduto_Click);
            // 
            // btnLimparProduto
            // 
            this.btnLimparProduto.Location = new System.Drawing.Point(1140, 111);
            this.btnLimparProduto.Name = "btnLimparProduto";
            this.btnLimparProduto.Size = new System.Drawing.Size(91, 23);
            this.btnLimparProduto.TabIndex = 17;
            this.btnLimparProduto.Text = "Limpar";
            this.btnLimparProduto.UseVisualStyleBackColor = true;
            this.btnLimparProduto.Click += new System.EventHandler(this.btnLimparProduto_Click);
            // 
            // lblDespesas
            // 
            this.lblDespesas.AutoSize = true;
            this.lblDespesas.Location = new System.Drawing.Point(238, 358);
            this.lblDespesas.Name = "lblDespesas";
            this.lblDespesas.Size = new System.Drawing.Size(70, 16);
            this.lblDespesas.TabIndex = 140;
            this.lblDespesas.Text = "Despesas";
            // 
            // txtDespesas
            // 
            this.txtDespesas.Location = new System.Drawing.Point(238, 377);
            this.txtDespesas.Name = "txtDespesas";
            this.txtDespesas.Size = new System.Drawing.Size(126, 22);
            this.txtDespesas.TabIndex = 20;
            // 
            // lblSeguro
            // 
            this.lblSeguro.AutoSize = true;
            this.lblSeguro.Location = new System.Drawing.Point(131, 359);
            this.lblSeguro.Name = "lblSeguro";
            this.lblSeguro.Size = new System.Drawing.Size(51, 16);
            this.lblSeguro.TabIndex = 138;
            this.lblSeguro.Text = "Seguro";
            // 
            // txtSeguro
            // 
            this.txtSeguro.Location = new System.Drawing.Point(131, 378);
            this.txtSeguro.Name = "txtSeguro";
            this.txtSeguro.Size = new System.Drawing.Size(88, 22);
            this.txtSeguro.TabIndex = 19;
            // 
            // lblFrete
            // 
            this.lblFrete.AutoSize = true;
            this.lblFrete.Location = new System.Drawing.Point(18, 359);
            this.lblFrete.Name = "lblFrete";
            this.lblFrete.Size = new System.Drawing.Size(73, 16);
            this.lblFrete.TabIndex = 136;
            this.lblFrete.Text = "Valor Frete";
            // 
            // txtFrete
            // 
            this.txtFrete.Location = new System.Drawing.Point(18, 378);
            this.txtFrete.Name = "txtFrete";
            this.txtFrete.Size = new System.Drawing.Size(100, 22);
            this.txtFrete.TabIndex = 18;
            // 
            // btnLimparCondPgto
            // 
            this.btnLimparCondPgto.Location = new System.Drawing.Point(449, 438);
            this.btnLimparCondPgto.Name = "btnLimparCondPgto";
            this.btnLimparCondPgto.Size = new System.Drawing.Size(150, 23);
            this.btnLimparCondPgto.TabIndex = 25;
            this.btnLimparCondPgto.Text = "Limpar";
            this.btnLimparCondPgto.UseVisualStyleBackColor = true;
            this.btnLimparCondPgto.Click += new System.EventHandler(this.btnLimparCondPgto_Click);
            // 
            // txtCondPgto
            // 
            this.txtCondPgto.Location = new System.Drawing.Point(75, 439);
            this.txtCondPgto.Name = "txtCondPgto";
            this.txtCondPgto.ReadOnly = true;
            this.txtCondPgto.Size = new System.Drawing.Size(200, 22);
            this.txtCondPgto.TabIndex = 23;
            // 
            // btnAdicionarCondPgto
            // 
            this.btnAdicionarCondPgto.Location = new System.Drawing.Point(281, 438);
            this.btnAdicionarCondPgto.Name = "btnAdicionarCondPgto";
            this.btnAdicionarCondPgto.Size = new System.Drawing.Size(150, 23);
            this.btnAdicionarCondPgto.TabIndex = 24;
            this.btnAdicionarCondPgto.Text = "Adicionar";
            this.btnAdicionarCondPgto.UseVisualStyleBackColor = true;
            this.btnAdicionarCondPgto.Click += new System.EventHandler(this.btnAdicionarCondPgto_Click);
            // 
            // listViewCondPgto
            // 
            this.listViewCondPgto.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NumParcela,
            this.Prazo,
            this.FormaPagamento,
            this.Valor});
            this.listViewCondPgto.FullRowSelect = true;
            this.listViewCondPgto.GridLines = true;
            this.listViewCondPgto.HideSelection = false;
            this.listViewCondPgto.Location = new System.Drawing.Point(18, 467);
            this.listViewCondPgto.Name = "listViewCondPgto";
            this.listViewCondPgto.Size = new System.Drawing.Size(1241, 121);
            this.listViewCondPgto.TabIndex = 144;
            this.listViewCondPgto.UseCompatibleStateImageBehavior = false;
            this.listViewCondPgto.View = System.Windows.Forms.View.Details;
            // 
            // NumParcela
            // 
            this.NumParcela.Text = "Num Parcela";
            this.NumParcela.Width = 99;
            // 
            // Prazo
            // 
            this.Prazo.Text = "Data de Vencimento";
            this.Prazo.Width = 150;
            // 
            // FormaPagamento
            // 
            this.FormaPagamento.Text = "Forma de Pagamento";
            this.FormaPagamento.Width = 165;
            // 
            // Valor
            // 
            this.Valor.Text = "Valor";
            this.Valor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblFormaPagamento
            // 
            this.lblFormaPagamento.AutoSize = true;
            this.lblFormaPagamento.Location = new System.Drawing.Point(71, 419);
            this.lblFormaPagamento.Name = "lblFormaPagamento";
            this.lblFormaPagamento.Size = new System.Drawing.Size(162, 16);
            this.lblFormaPagamento.TabIndex = 143;
            this.lblFormaPagamento.Text = "Condição de Pagamento*";
            // 
            // lblTotalProdutos
            // 
            this.lblTotalProdutos.AutoSize = true;
            this.lblTotalProdutos.Location = new System.Drawing.Point(968, 337);
            this.lblTotalProdutos.Name = "lblTotalProdutos";
            this.lblTotalProdutos.Size = new System.Drawing.Size(66, 16);
            this.lblTotalProdutos.TabIndex = 146;
            this.lblTotalProdutos.Text = "Total (R$)";
            // 
            // lblValorTotal
            // 
            this.lblValorTotal.AutoSize = true;
            this.lblValorTotal.Location = new System.Drawing.Point(381, 358);
            this.lblValorTotal.Name = "lblValorTotal";
            this.lblValorTotal.Size = new System.Drawing.Size(73, 16);
            this.lblValorTotal.TabIndex = 148;
            this.lblValorTotal.Text = "Valor Total";
            // 
            // txtValorTotal
            // 
            this.txtValorTotal.Location = new System.Drawing.Point(381, 377);
            this.txtValorTotal.Name = "txtValorTotal";
            this.txtValorTotal.ReadOnly = true;
            this.txtValorTotal.Size = new System.Drawing.Size(126, 22);
            this.txtValorTotal.TabIndex = 21;
            // 
            // lblTotalCondiçãoPgto
            // 
            this.lblTotalCondiçãoPgto.AutoSize = true;
            this.lblTotalCondiçãoPgto.Location = new System.Drawing.Point(968, 591);
            this.lblTotalCondiçãoPgto.Name = "lblTotalCondiçãoPgto";
            this.lblTotalCondiçãoPgto.Size = new System.Drawing.Size(66, 16);
            this.lblTotalCondiçãoPgto.TabIndex = 149;
            this.lblTotalCondiçãoPgto.Text = "Total (R$)";
            // 
            // lblIdProduto
            // 
            this.lblIdProduto.AutoSize = true;
            this.lblIdProduto.Location = new System.Drawing.Point(21, 93);
            this.lblIdProduto.Name = "lblIdProduto";
            this.lblIdProduto.Size = new System.Drawing.Size(52, 16);
            this.lblIdProduto.TabIndex = 151;
            this.lblIdProduto.Text = "ID Prod";
            // 
            // txtIdProduto
            // 
            this.txtIdProduto.Location = new System.Drawing.Point(21, 112);
            this.txtIdProduto.Name = "txtIdProduto";
            this.txtIdProduto.Size = new System.Drawing.Size(48, 22);
            this.txtIdProduto.TabIndex = 8;
            this.txtIdProduto.Leave += new System.EventHandler(this.txtIdProduto_Leave);
            // 
            // lblIdCondicaoPgto
            // 
            this.lblIdCondicaoPgto.AutoSize = true;
            this.lblIdCondicaoPgto.Location = new System.Drawing.Point(18, 419);
            this.lblIdCondicaoPgto.Name = "lblIdCondicaoPgto";
            this.lblIdCondicaoPgto.Size = new System.Drawing.Size(55, 16);
            this.lblIdCondicaoPgto.TabIndex = 153;
            this.lblIdCondicaoPgto.Text = "ID Cond";
            // 
            // txtIdCondPgto
            // 
            this.txtIdCondPgto.Location = new System.Drawing.Point(18, 438);
            this.txtIdCondPgto.Name = "txtIdCondPgto";
            this.txtIdCondPgto.Size = new System.Drawing.Size(48, 22);
            this.txtIdCondPgto.TabIndex = 22;
            this.txtIdCondPgto.Leave += new System.EventHandler(this.txtIdCondPgto_Leave);
            // 
            // lblMotivoCancelamento
            // 
            this.lblMotivoCancelamento.AutoSize = true;
            this.lblMotivoCancelamento.Location = new System.Drawing.Point(317, 620);
            this.lblMotivoCancelamento.Name = "lblMotivoCancelamento";
            this.lblMotivoCancelamento.Size = new System.Drawing.Size(159, 16);
            this.lblMotivoCancelamento.TabIndex = 154;
            this.lblMotivoCancelamento.Text = "Motivo do Cancelamento:";
            // 
            // frmCadastroCompra
            // 
            //this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            //this.ClientSize = new System.Drawing.Size(1342, 681);
            this.Controls.Add(this.lblMotivoCancelamento);
            this.Controls.Add(this.lblIdCondicaoPgto);
            this.Controls.Add(this.txtIdCondPgto);
            this.Controls.Add(this.lblIdProduto);
            this.Controls.Add(this.txtIdProduto);
            this.Controls.Add(this.lblTotalCondiçãoPgto);
            this.Controls.Add(this.lblValorTotal);
            this.Controls.Add(this.txtValorTotal);
            this.Controls.Add(this.lblTotalProdutos);
            this.Controls.Add(this.btnLimparCondPgto);
            this.Controls.Add(this.txtCondPgto);
            this.Controls.Add(this.btnAdicionarCondPgto);
            this.Controls.Add(this.listViewCondPgto);
            this.Controls.Add(this.lblFormaPagamento);
            this.Controls.Add(this.lblDespesas);
            this.Controls.Add(this.txtDespesas);
            this.Controls.Add(this.lblSeguro);
            this.Controls.Add(this.txtSeguro);
            this.Controls.Add(this.lblFrete);
            this.Controls.Add(this.txtFrete);
            this.Controls.Add(this.btnLimparProduto);
            this.Controls.Add(this.btnRemoverProduto);
            this.Controls.Add(this.btnEditarProduto);
            this.Controls.Add(this.btnAdicionarProduto);
            this.Controls.Add(this.lblTotalProduto);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.lblValorUnitario);
            this.Controls.Add(this.txtValorUnitario);
            this.Controls.Add(this.lblQuantidade);
            this.Controls.Add(this.txtQuantidade);
            this.Controls.Add(this.btnPesquisarProduto);
            this.Controls.Add(this.lblProduto);
            this.Controls.Add(this.txtProduto);
            this.Controls.Add(this.listViewProdutos);
            this.Controls.Add(this.lblDataChegada);
            this.Controls.Add(this.dtpChegada);
            this.Controls.Add(this.lblDataEmissao);
            this.Controls.Add(this.dtpEmissao);
            this.Controls.Add(this.btnPesquisarFornecedor);
            this.Controls.Add(this.lblFornecedor);
            this.Controls.Add(this.txtFornecedor);
            this.Controls.Add(this.lblIdFornecedor);
            this.Controls.Add(this.txtIDFornecedor);
            this.Controls.Add(this.lblSerie);
            this.Controls.Add(this.lblNumero);
            this.Controls.Add(this.txtNumero);
            this.Controls.Add(this.txtSerie);
            this.Controls.Add(this.lblModelo);
            this.MaximumSize = new System.Drawing.Size(1900, 1000);
            this.Name = "frmCadastroCompra";
            this.Text = "Cadastro Compras";
            this.Controls.SetChildIndex(this.btnSalvar, 0);
            this.Controls.SetChildIndex(this.btnSair, 0);
            this.Controls.SetChildIndex(this.txtCodigo, 0);
            this.Controls.SetChildIndex(this.chkInativo, 0);
            this.Controls.SetChildIndex(this.lblDataCriacao, 0);
            this.Controls.SetChildIndex(this.lblDataModificacao, 0);
            this.Controls.SetChildIndex(this.lblModelo, 0);
            this.Controls.SetChildIndex(this.txtSerie, 0);
            this.Controls.SetChildIndex(this.txtNumero, 0);
            this.Controls.SetChildIndex(this.lblNumero, 0);
            this.Controls.SetChildIndex(this.lblSerie, 0);
            this.Controls.SetChildIndex(this.txtIDFornecedor, 0);
            this.Controls.SetChildIndex(this.lblIdFornecedor, 0);
            this.Controls.SetChildIndex(this.txtFornecedor, 0);
            this.Controls.SetChildIndex(this.lblFornecedor, 0);
            this.Controls.SetChildIndex(this.btnPesquisarFornecedor, 0);
            this.Controls.SetChildIndex(this.dtpEmissao, 0);
            this.Controls.SetChildIndex(this.lblDataEmissao, 0);
            this.Controls.SetChildIndex(this.dtpChegada, 0);
            this.Controls.SetChildIndex(this.lblDataChegada, 0);
            this.Controls.SetChildIndex(this.listViewProdutos, 0);
            this.Controls.SetChildIndex(this.txtProduto, 0);
            this.Controls.SetChildIndex(this.lblProduto, 0);
            this.Controls.SetChildIndex(this.btnPesquisarProduto, 0);
            this.Controls.SetChildIndex(this.txtQuantidade, 0);
            this.Controls.SetChildIndex(this.lblQuantidade, 0);
            this.Controls.SetChildIndex(this.txtValorUnitario, 0);
            this.Controls.SetChildIndex(this.lblValorUnitario, 0);
            this.Controls.SetChildIndex(this.txtTotal, 0);
            this.Controls.SetChildIndex(this.lblTotalProduto, 0);
            this.Controls.SetChildIndex(this.btnAdicionarProduto, 0);
            this.Controls.SetChildIndex(this.btnEditarProduto, 0);
            this.Controls.SetChildIndex(this.btnRemoverProduto, 0);
            this.Controls.SetChildIndex(this.btnLimparProduto, 0);
            this.Controls.SetChildIndex(this.txtFrete, 0);
            this.Controls.SetChildIndex(this.lblFrete, 0);
            this.Controls.SetChildIndex(this.txtSeguro, 0);
            this.Controls.SetChildIndex(this.lblSeguro, 0);
            this.Controls.SetChildIndex(this.txtDespesas, 0);
            this.Controls.SetChildIndex(this.lblDespesas, 0);
            this.Controls.SetChildIndex(this.lblFormaPagamento, 0);
            this.Controls.SetChildIndex(this.listViewCondPgto, 0);
            this.Controls.SetChildIndex(this.btnAdicionarCondPgto, 0);
            this.Controls.SetChildIndex(this.txtCondPgto, 0);
            this.Controls.SetChildIndex(this.btnLimparCondPgto, 0);
            this.Controls.SetChildIndex(this.lblTotalProdutos, 0);
            this.Controls.SetChildIndex(this.txtValorTotal, 0);
            this.Controls.SetChildIndex(this.lblValorTotal, 0);
            this.Controls.SetChildIndex(this.lblTotalCondiçãoPgto, 0);
            this.Controls.SetChildIndex(this.txtIdProduto, 0);
            this.Controls.SetChildIndex(this.lblIdProduto, 0);
            this.Controls.SetChildIndex(this.txtIdCondPgto, 0);
            this.Controls.SetChildIndex(this.lblIdCondicaoPgto, 0);
            this.Controls.SetChildIndex(this.lblMotivoCancelamento, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblModelo;
        private System.Windows.Forms.Button btnPesquisarFornecedor;
        private System.Windows.Forms.Label lblFornecedor;
        private System.Windows.Forms.TextBox txtFornecedor;
        private System.Windows.Forms.Label lblIdFornecedor;
        private System.Windows.Forms.TextBox txtIDFornecedor;
        private System.Windows.Forms.Label lblSerie;
        private System.Windows.Forms.Label lblNumero;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.TextBox txtSerie;
        private System.Windows.Forms.Label lblDataChegada;
        private System.Windows.Forms.DateTimePicker dtpChegada;
        private System.Windows.Forms.Label lblDataEmissao;
        private System.Windows.Forms.DateTimePicker dtpEmissao;
        protected System.Windows.Forms.ListView listViewProdutos;
        private System.Windows.Forms.ColumnHeader Codigo;
        private System.Windows.Forms.ColumnHeader Produto;
        private System.Windows.Forms.ColumnHeader Unidade;
        private System.Windows.Forms.ColumnHeader Quantidade;
        private System.Windows.Forms.ColumnHeader PrecoUnitario;
        private System.Windows.Forms.ColumnHeader Total;
        private System.Windows.Forms.Label lblProduto;
        private System.Windows.Forms.TextBox txtProduto;
        private System.Windows.Forms.Button btnPesquisarProduto;
        private System.Windows.Forms.Label lblTotalProduto;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label lblValorUnitario;
        private System.Windows.Forms.TextBox txtValorUnitario;
        private System.Windows.Forms.Label lblQuantidade;
        private System.Windows.Forms.TextBox txtQuantidade;
        private System.Windows.Forms.Button btnAdicionarProduto;
        private System.Windows.Forms.Button btnEditarProduto;
        private System.Windows.Forms.Button btnRemoverProduto;
        private System.Windows.Forms.Button btnLimparProduto;
        private System.Windows.Forms.Label lblDespesas;
        private System.Windows.Forms.TextBox txtDespesas;
        private System.Windows.Forms.Label lblSeguro;
        private System.Windows.Forms.TextBox txtSeguro;
        private System.Windows.Forms.Label lblFrete;
        private System.Windows.Forms.TextBox txtFrete;
        private System.Windows.Forms.Button btnLimparCondPgto;
        private System.Windows.Forms.TextBox txtCondPgto;
        private System.Windows.Forms.Button btnAdicionarCondPgto;
        protected System.Windows.Forms.ListView listViewCondPgto;
        private System.Windows.Forms.ColumnHeader NumParcela;
        private System.Windows.Forms.ColumnHeader Prazo;
        private System.Windows.Forms.ColumnHeader FormaPagamento;
        private System.Windows.Forms.Label lblFormaPagamento;
        private System.Windows.Forms.Label lblTotalProdutos;
        private System.Windows.Forms.Label lblValorTotal;
        private System.Windows.Forms.TextBox txtValorTotal;
        private System.Windows.Forms.Label lblTotalCondiçãoPgto;
        private System.Windows.Forms.Label lblIdProduto;
        private System.Windows.Forms.TextBox txtIdProduto;
        private System.Windows.Forms.Label lblIdCondicaoPgto;
        private System.Windows.Forms.TextBox txtIdCondPgto;
        private System.Windows.Forms.ColumnHeader Valor;
        private System.Windows.Forms.Label lblMotivoCancelamento;
    }
}
