namespace Projeto.Views.Cadastros
{
    partial class frmCadastroVenda
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
            this.btnLimparProduto = new System.Windows.Forms.Button();
            this.btnRemoverProduto = new System.Windows.Forms.Button();
            this.btnEditarProduto = new System.Windows.Forms.Button();
            this.btnAdicionarProduto = new System.Windows.Forms.Button();
            this.lblTotalProduto = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.lblValorUnitario = new System.Windows.Forms.Label();
            this.txtValorUnitario = new System.Windows.Forms.TextBox();
            this.lblQuantidade = new System.Windows.Forms.Label();
            this.txtQuantidade = new System.Windows.Forms.TextBox();
            this.btnPesquisarProduto = new System.Windows.Forms.Button();
            this.lblDataSaida = new System.Windows.Forms.Label();
            this.dtpSaida = new System.Windows.Forms.DateTimePicker();
            this.lblDataEmissao = new System.Windows.Forms.Label();
            this.dtpEmissao = new System.Windows.Forms.DateTimePicker();
            this.btnPesquisarCliente = new System.Windows.Forms.Button();
            this.lblCliente = new System.Windows.Forms.Label();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.lblIdCliente = new System.Windows.Forms.Label();
            this.txtIdCliente = new System.Windows.Forms.TextBox();
            this.lblSerie = new System.Windows.Forms.Label();
            this.lblNumero = new System.Windows.Forms.Label();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.txtSerie = new System.Windows.Forms.TextBox();
            this.lblIdProduto = new System.Windows.Forms.Label();
            this.txtIdProduto = new System.Windows.Forms.TextBox();
            this.lblProduto = new System.Windows.Forms.Label();
            this.txtProduto = new System.Windows.Forms.TextBox();
            this.lblIdCondicaoPgto = new System.Windows.Forms.Label();
            this.txtIdCondPgto = new System.Windows.Forms.TextBox();
            this.lblValorTotal = new System.Windows.Forms.Label();
            this.txtValorTotal = new System.Windows.Forms.TextBox();
            this.lblTotalProdutos = new System.Windows.Forms.Label();
            this.btnLimparCondPgto = new System.Windows.Forms.Button();
            this.txtCondPgto = new System.Windows.Forms.TextBox();
            this.btnAdicionarCondPgto = new System.Windows.Forms.Button();
            this.listViewCondPgto = new System.Windows.Forms.ListView();
            this.NumParcela = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Prazo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FormaPagamento = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Valor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblFormaPagamento = new System.Windows.Forms.Label();
            this.listViewProdutos = new System.Windows.Forms.ListView();
            this.Codigo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Produto = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Unidade = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Quantidade = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PrecoUnitario = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Total = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblModelo = new System.Windows.Forms.Label();
            this.lblTotalCondiçãoPgto = new System.Windows.Forms.Label();
            this.lblMotivoCancelamento = new System.Windows.Forms.Label();
            this.btnPesquisarFuncionario = new System.Windows.Forms.Button();
            this.lblFuncionario = new System.Windows.Forms.Label();
            this.txtFuncionario = new System.Windows.Forms.TextBox();
            this.lblIDFuncionario = new System.Windows.Forms.Label();
            this.txtIdFuncionario = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtCodigo
            // 
            this.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodigo.MaxLength = 5;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnLimparProduto
            // 
            this.btnLimparProduto.Location = new System.Drawing.Point(1135, 152);
            this.btnLimparProduto.Name = "btnLimparProduto";
            this.btnLimparProduto.Size = new System.Drawing.Size(91, 23);
            this.btnLimparProduto.TabIndex = 146;
            this.btnLimparProduto.Text = "Limpar";
            this.btnLimparProduto.UseVisualStyleBackColor = true;
            this.btnLimparProduto.Click += new System.EventHandler(this.btnLimparProduto_Click);
            // 
            // btnRemoverProduto
            // 
            this.btnRemoverProduto.Location = new System.Drawing.Point(1023, 152);
            this.btnRemoverProduto.Name = "btnRemoverProduto";
            this.btnRemoverProduto.Size = new System.Drawing.Size(91, 23);
            this.btnRemoverProduto.TabIndex = 145;
            this.btnRemoverProduto.Text = "Remover";
            this.btnRemoverProduto.UseVisualStyleBackColor = true;
            this.btnRemoverProduto.Click += new System.EventHandler(this.btnRemoverProduto_Click);
            // 
            // btnEditarProduto
            // 
            this.btnEditarProduto.Location = new System.Drawing.Point(901, 152);
            this.btnEditarProduto.Name = "btnEditarProduto";
            this.btnEditarProduto.Size = new System.Drawing.Size(91, 23);
            this.btnEditarProduto.TabIndex = 144;
            this.btnEditarProduto.Text = "Editar";
            this.btnEditarProduto.UseVisualStyleBackColor = true;
            // 
            // btnAdicionarProduto
            // 
            this.btnAdicionarProduto.Location = new System.Drawing.Point(788, 152);
            this.btnAdicionarProduto.Name = "btnAdicionarProduto";
            this.btnAdicionarProduto.Size = new System.Drawing.Size(91, 23);
            this.btnAdicionarProduto.TabIndex = 143;
            this.btnAdicionarProduto.Text = "Adicionar";
            this.btnAdicionarProduto.UseVisualStyleBackColor = true;
            this.btnAdicionarProduto.Click += new System.EventHandler(this.btnAdicionarProduto_Click);
            // 
            // lblTotalProduto
            // 
            this.lblTotalProduto.AutoSize = true;
            this.lblTotalProduto.Location = new System.Drawing.Point(635, 132);
            this.lblTotalProduto.Name = "lblTotalProduto";
            this.lblTotalProduto.Size = new System.Drawing.Size(66, 16);
            this.lblTotalProduto.TabIndex = 155;
            this.lblTotalProduto.Text = "Total (R$)";
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(635, 151);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(126, 22);
            this.txtTotal.TabIndex = 142;
            // 
            // lblValorUnitario
            // 
            this.lblValorUnitario.AutoSize = true;
            this.lblValorUnitario.Location = new System.Drawing.Point(541, 133);
            this.lblValorUnitario.Name = "lblValorUnitario";
            this.lblValorUnitario.Size = new System.Drawing.Size(88, 16);
            this.lblValorUnitario.TabIndex = 154;
            this.lblValorUnitario.Text = "Valor Unitário";
            // 
            // txtValorUnitario
            // 
            this.txtValorUnitario.Location = new System.Drawing.Point(541, 152);
            this.txtValorUnitario.MaxLength = 15;
            this.txtValorUnitario.Name = "txtValorUnitario";
            this.txtValorUnitario.Size = new System.Drawing.Size(88, 22);
            this.txtValorUnitario.TabIndex = 141;
            this.txtValorUnitario.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtValorUnitario.TextChanged += new System.EventHandler(this.txtValorUnitario_TextChanged);
            // 
            // lblQuantidade
            // 
            this.lblQuantidade.AutoSize = true;
            this.lblQuantidade.Location = new System.Drawing.Point(415, 133);
            this.lblQuantidade.Name = "lblQuantidade";
            this.lblQuantidade.Size = new System.Drawing.Size(77, 16);
            this.lblQuantidade.TabIndex = 153;
            this.lblQuantidade.Text = "Quantidade";
            // 
            // txtQuantidade
            // 
            this.txtQuantidade.Location = new System.Drawing.Point(415, 152);
            this.txtQuantidade.MaxLength = 10;
            this.txtQuantidade.Name = "txtQuantidade";
            this.txtQuantidade.Size = new System.Drawing.Size(100, 22);
            this.txtQuantidade.TabIndex = 140;
            this.txtQuantidade.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQuantidade.TextChanged += new System.EventHandler(this.txtQuantidade_TextChanged);
            // 
            // btnPesquisarProduto
            // 
            this.btnPesquisarProduto.Location = new System.Drawing.Point(315, 152);
            this.btnPesquisarProduto.Name = "btnPesquisarProduto";
            this.btnPesquisarProduto.Size = new System.Drawing.Size(91, 23);
            this.btnPesquisarProduto.TabIndex = 139;
            this.btnPesquisarProduto.Text = "Pesquisar";
            this.btnPesquisarProduto.UseVisualStyleBackColor = true;
            this.btnPesquisarProduto.Click += new System.EventHandler(this.btnPesquisarProduto_Click);
            // 
            // lblDataSaida
            // 
            this.lblDataSaida.AutoSize = true;
            this.lblDataSaida.Location = new System.Drawing.Point(225, 66);
            this.lblDataSaida.Name = "lblDataSaida";
            this.lblDataSaida.Size = new System.Drawing.Size(94, 16);
            this.lblDataSaida.TabIndex = 152;
            this.lblDataSaida.Text = "Data de Saída";
            // 
            // dtpSaida
            // 
            this.dtpSaida.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpSaida.Location = new System.Drawing.Point(228, 85);
            this.dtpSaida.Name = "dtpSaida";
            this.dtpSaida.Size = new System.Drawing.Size(160, 22);
            this.dtpSaida.TabIndex = 137;
            this.dtpSaida.Value = new System.DateTime(2025, 7, 10, 0, 0, 0, 0);
            // 
            // lblDataEmissao
            // 
            this.lblDataEmissao.AutoSize = true;
            this.lblDataEmissao.Location = new System.Drawing.Point(8, 66);
            this.lblDataEmissao.Name = "lblDataEmissao";
            this.lblDataEmissao.Size = new System.Drawing.Size(111, 16);
            this.lblDataEmissao.TabIndex = 151;
            this.lblDataEmissao.Text = "Data de Emissão";
            // 
            // dtpEmissao
            // 
            this.dtpEmissao.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEmissao.Location = new System.Drawing.Point(11, 85);
            this.dtpEmissao.Name = "dtpEmissao";
            this.dtpEmissao.Size = new System.Drawing.Size(160, 22);
            this.dtpEmissao.TabIndex = 136;
            this.dtpEmissao.Value = new System.DateTime(2025, 7, 10, 0, 0, 0, 0);
            // 
            // btnPesquisarCliente
            // 
            this.btnPesquisarCliente.Location = new System.Drawing.Point(697, 27);
            this.btnPesquisarCliente.Name = "btnPesquisarCliente";
            this.btnPesquisarCliente.Size = new System.Drawing.Size(91, 23);
            this.btnPesquisarCliente.TabIndex = 135;
            this.btnPesquisarCliente.Text = "Pesquisar";
            this.btnPesquisarCliente.UseVisualStyleBackColor = true;
            this.btnPesquisarCliente.Click += new System.EventHandler(this.btnPesquisarCliente_Click);
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Location = new System.Drawing.Point(480, 8);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(53, 16);
            this.lblCliente.TabIndex = 150;
            this.lblCliente.Text = "Cliente*";
            // 
            // txtCliente
            // 
            this.txtCliente.Location = new System.Drawing.Point(480, 27);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.ReadOnly = true;
            this.txtCliente.Size = new System.Drawing.Size(211, 22);
            this.txtCliente.TabIndex = 134;
            // 
            // lblIdCliente
            // 
            this.lblIdCliente.AutoSize = true;
            this.lblIdCliente.Location = new System.Drawing.Point(414, 8);
            this.lblIdCliente.Name = "lblIdCliente";
            this.lblIdCliente.Size = new System.Drawing.Size(69, 16);
            this.lblIdCliente.TabIndex = 149;
            this.lblIdCliente.Text = "ID Cliente*";
            // 
            // txtIdCliente
            // 
            this.txtIdCliente.Location = new System.Drawing.Point(414, 27);
            this.txtIdCliente.MaxLength = 10;
            this.txtIdCliente.Name = "txtIdCliente";
            this.txtIdCliente.Size = new System.Drawing.Size(48, 22);
            this.txtIdCliente.TabIndex = 133;
            this.txtIdCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtIdCliente.Leave += new System.EventHandler(this.txtIdCliente_Leave);
            // 
            // lblSerie
            // 
            this.lblSerie.AutoSize = true;
            this.lblSerie.Location = new System.Drawing.Point(148, 8);
            this.lblSerie.Name = "lblSerie";
            this.lblSerie.Size = new System.Drawing.Size(44, 16);
            this.lblSerie.TabIndex = 148;
            this.lblSerie.Text = "Série*";
            // 
            // lblNumero
            // 
            this.lblNumero.AutoSize = true;
            this.lblNumero.Location = new System.Drawing.Point(288, 8);
            this.lblNumero.Name = "lblNumero";
            this.lblNumero.Size = new System.Drawing.Size(60, 16);
            this.lblNumero.TabIndex = 147;
            this.lblNumero.Text = "Número*";
            // 
            // txtNumero
            // 
            this.txtNumero.Location = new System.Drawing.Point(288, 27);
            this.txtNumero.MaxLength = 10;
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(100, 22);
            this.txtNumero.TabIndex = 132;
            // 
            // txtSerie
            // 
            this.txtSerie.Location = new System.Drawing.Point(148, 27);
            this.txtSerie.MaxLength = 5;
            this.txtSerie.Name = "txtSerie";
            this.txtSerie.Size = new System.Drawing.Size(100, 22);
            this.txtSerie.TabIndex = 131;
            // 
            // lblIdProduto
            // 
            this.lblIdProduto.AutoSize = true;
            this.lblIdProduto.Location = new System.Drawing.Point(11, 132);
            this.lblIdProduto.Name = "lblIdProduto";
            this.lblIdProduto.Size = new System.Drawing.Size(52, 16);
            this.lblIdProduto.TabIndex = 159;
            this.lblIdProduto.Text = "ID Prod";
            // 
            // txtIdProduto
            // 
            this.txtIdProduto.Location = new System.Drawing.Point(11, 151);
            this.txtIdProduto.MaxLength = 10;
            this.txtIdProduto.Name = "txtIdProduto";
            this.txtIdProduto.Size = new System.Drawing.Size(48, 22);
            this.txtIdProduto.TabIndex = 156;
            this.txtIdProduto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtIdProduto.Leave += new System.EventHandler(this.txtIdProduto_Leave);
            // 
            // lblProduto
            // 
            this.lblProduto.AutoSize = true;
            this.lblProduto.Location = new System.Drawing.Point(71, 132);
            this.lblProduto.Name = "lblProduto";
            this.lblProduto.Size = new System.Drawing.Size(54, 16);
            this.lblProduto.TabIndex = 158;
            this.lblProduto.Text = "Produto";
            // 
            // txtProduto
            // 
            this.txtProduto.Location = new System.Drawing.Point(71, 151);
            this.txtProduto.Name = "txtProduto";
            this.txtProduto.ReadOnly = true;
            this.txtProduto.Size = new System.Drawing.Size(211, 22);
            this.txtProduto.TabIndex = 157;
            // 
            // lblIdCondicaoPgto
            // 
            this.lblIdCondicaoPgto.AutoSize = true;
            this.lblIdCondicaoPgto.Location = new System.Drawing.Point(14, 419);
            this.lblIdCondicaoPgto.Name = "lblIdCondicaoPgto";
            this.lblIdCondicaoPgto.Size = new System.Drawing.Size(55, 16);
            this.lblIdCondicaoPgto.TabIndex = 176;
            this.lblIdCondicaoPgto.Text = "ID Cond";
            // 
            // txtIdCondPgto
            // 
            this.txtIdCondPgto.Location = new System.Drawing.Point(14, 438);
            this.txtIdCondPgto.MaxLength = 10;
            this.txtIdCondPgto.Name = "txtIdCondPgto";
            this.txtIdCondPgto.Size = new System.Drawing.Size(48, 22);
            this.txtIdCondPgto.TabIndex = 164;
            this.txtIdCondPgto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtIdCondPgto.Leave += new System.EventHandler(this.txtIdCondPgto_Leave);
            // 
            // lblValorTotal
            // 
            this.lblValorTotal.AutoSize = true;
            this.lblValorTotal.Location = new System.Drawing.Point(15, 375);
            this.lblValorTotal.Name = "lblValorTotal";
            this.lblValorTotal.Size = new System.Drawing.Size(78, 16);
            this.lblValorTotal.TabIndex = 175;
            this.lblValorTotal.Text = "Valor Total*";
            // 
            // txtValorTotal
            // 
            this.txtValorTotal.Location = new System.Drawing.Point(15, 394);
            this.txtValorTotal.Name = "txtValorTotal";
            this.txtValorTotal.ReadOnly = true;
            this.txtValorTotal.Size = new System.Drawing.Size(126, 22);
            this.txtValorTotal.TabIndex = 163;
            // 
            // lblTotalProdutos
            // 
            this.lblTotalProdutos.AutoSize = true;
            this.lblTotalProdutos.Location = new System.Drawing.Point(541, 365);
            this.lblTotalProdutos.Name = "lblTotalProdutos";
            this.lblTotalProdutos.Size = new System.Drawing.Size(66, 16);
            this.lblTotalProdutos.TabIndex = 174;
            this.lblTotalProdutos.Text = "Total (R$)";
            // 
            // btnLimparCondPgto
            // 
            this.btnLimparCondPgto.Location = new System.Drawing.Point(445, 438);
            this.btnLimparCondPgto.Name = "btnLimparCondPgto";
            this.btnLimparCondPgto.Size = new System.Drawing.Size(150, 23);
            this.btnLimparCondPgto.TabIndex = 167;
            this.btnLimparCondPgto.Text = "Limpar";
            this.btnLimparCondPgto.UseVisualStyleBackColor = true;
            this.btnLimparCondPgto.Click += new System.EventHandler(this.btnLimparCondPgto_Click);
            // 
            // txtCondPgto
            // 
            this.txtCondPgto.Location = new System.Drawing.Point(71, 439);
            this.txtCondPgto.Name = "txtCondPgto";
            this.txtCondPgto.ReadOnly = true;
            this.txtCondPgto.Size = new System.Drawing.Size(200, 22);
            this.txtCondPgto.TabIndex = 165;
            // 
            // btnAdicionarCondPgto
            // 
            this.btnAdicionarCondPgto.Location = new System.Drawing.Point(277, 438);
            this.btnAdicionarCondPgto.Name = "btnAdicionarCondPgto";
            this.btnAdicionarCondPgto.Size = new System.Drawing.Size(150, 23);
            this.btnAdicionarCondPgto.TabIndex = 166;
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
            this.listViewCondPgto.Location = new System.Drawing.Point(14, 467);
            this.listViewCondPgto.Name = "listViewCondPgto";
            this.listViewCondPgto.Size = new System.Drawing.Size(1241, 121);
            this.listViewCondPgto.TabIndex = 173;
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
            this.lblFormaPagamento.Location = new System.Drawing.Point(67, 419);
            this.lblFormaPagamento.Name = "lblFormaPagamento";
            this.lblFormaPagamento.Size = new System.Drawing.Size(162, 16);
            this.lblFormaPagamento.TabIndex = 172;
            this.lblFormaPagamento.Text = "Condição de Pagamento*";
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
            this.listViewProdutos.Location = new System.Drawing.Point(12, 199);
            this.listViewProdutos.Name = "listViewProdutos";
            this.listViewProdutos.Size = new System.Drawing.Size(1230, 163);
            this.listViewProdutos.TabIndex = 168;
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
            this.Quantidade.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Quantidade.Width = 48;
            // 
            // PrecoUnitario
            // 
            this.PrecoUnitario.Text = "Preço Unitário";
            this.PrecoUnitario.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.PrecoUnitario.Width = 111;
            // 
            // Total
            // 
            this.Total.Text = "Total";
            this.Total.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblModelo
            // 
            this.lblModelo.AutoSize = true;
            this.lblModelo.Location = new System.Drawing.Point(14, 8);
            this.lblModelo.Name = "lblModelo";
            this.lblModelo.Size = new System.Drawing.Size(58, 16);
            this.lblModelo.TabIndex = 177;
            this.lblModelo.Text = "Modelo*";
            // 
            // lblTotalCondiçãoPgto
            // 
            this.lblTotalCondiçãoPgto.AutoSize = true;
            this.lblTotalCondiçãoPgto.Location = new System.Drawing.Point(541, 594);
            this.lblTotalCondiçãoPgto.Name = "lblTotalCondiçãoPgto";
            this.lblTotalCondiçãoPgto.Size = new System.Drawing.Size(66, 16);
            this.lblTotalCondiçãoPgto.TabIndex = 178;
            this.lblTotalCondiçãoPgto.Text = "Total (R$)";
            // 
            // lblMotivoCancelamento
            // 
            this.lblMotivoCancelamento.AutoSize = true;
            this.lblMotivoCancelamento.Location = new System.Drawing.Point(303, 653);
            this.lblMotivoCancelamento.Name = "lblMotivoCancelamento";
            this.lblMotivoCancelamento.Size = new System.Drawing.Size(159, 16);
            this.lblMotivoCancelamento.TabIndex = 179;
            this.lblMotivoCancelamento.Text = "Motivo do Cancelamento:";
            // 
            // btnPesquisarFuncionario
            // 
            this.btnPesquisarFuncionario.Location = new System.Drawing.Point(1089, 27);
            this.btnPesquisarFuncionario.Name = "btnPesquisarFuncionario";
            this.btnPesquisarFuncionario.Size = new System.Drawing.Size(91, 23);
            this.btnPesquisarFuncionario.TabIndex = 182;
            this.btnPesquisarFuncionario.Text = "Pesquisar";
            this.btnPesquisarFuncionario.UseVisualStyleBackColor = true;
            this.btnPesquisarFuncionario.Click += new System.EventHandler(this.btnPesquisarFuncionario_Click);
            // 
            // lblFuncionario
            // 
            this.lblFuncionario.AutoSize = true;
            this.lblFuncionario.Location = new System.Drawing.Point(872, 8);
            this.lblFuncionario.Name = "lblFuncionario";
            this.lblFuncionario.Size = new System.Drawing.Size(82, 16);
            this.lblFuncionario.TabIndex = 184;
            this.lblFuncionario.Text = "Funcionário*";
            // 
            // txtFuncionario
            // 
            this.txtFuncionario.Location = new System.Drawing.Point(872, 27);
            this.txtFuncionario.Name = "txtFuncionario";
            this.txtFuncionario.ReadOnly = true;
            this.txtFuncionario.Size = new System.Drawing.Size(211, 22);
            this.txtFuncionario.TabIndex = 181;
            // 
            // lblIDFuncionario
            // 
            this.lblIDFuncionario.AutoSize = true;
            this.lblIDFuncionario.Location = new System.Drawing.Point(806, 8);
            this.lblIDFuncionario.Name = "lblIDFuncionario";
            this.lblIDFuncionario.Size = new System.Drawing.Size(60, 16);
            this.lblIDFuncionario.TabIndex = 183;
            this.lblIDFuncionario.Text = "ID Func.*";
            // 
            // txtIdFuncionario
            // 
            this.txtIdFuncionario.Location = new System.Drawing.Point(806, 27);
            this.txtIdFuncionario.MaxLength = 10;
            this.txtIdFuncionario.Name = "txtIdFuncionario";
            this.txtIdFuncionario.Size = new System.Drawing.Size(48, 22);
            this.txtIdFuncionario.TabIndex = 180;
            this.txtIdFuncionario.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // frmCadastroVenda
            // 
            //this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            //this.ClientSize = new System.Drawing.Size(1342, 681);
            this.Controls.Add(this.btnPesquisarFuncionario);
            this.Controls.Add(this.lblFuncionario);
            this.Controls.Add(this.txtFuncionario);
            this.Controls.Add(this.lblIDFuncionario);
            this.Controls.Add(this.txtIdFuncionario);
            this.Controls.Add(this.lblMotivoCancelamento);
            this.Controls.Add(this.lblTotalCondiçãoPgto);
            this.Controls.Add(this.lblModelo);
            this.Controls.Add(this.lblIdCondicaoPgto);
            this.Controls.Add(this.txtIdCondPgto);
            this.Controls.Add(this.lblValorTotal);
            this.Controls.Add(this.txtValorTotal);
            this.Controls.Add(this.lblTotalProdutos);
            this.Controls.Add(this.btnLimparCondPgto);
            this.Controls.Add(this.txtCondPgto);
            this.Controls.Add(this.btnAdicionarCondPgto);
            this.Controls.Add(this.listViewCondPgto);
            this.Controls.Add(this.lblFormaPagamento);
            this.Controls.Add(this.listViewProdutos);
            this.Controls.Add(this.lblIdProduto);
            this.Controls.Add(this.txtIdProduto);
            this.Controls.Add(this.lblProduto);
            this.Controls.Add(this.txtProduto);
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
            this.Controls.Add(this.lblDataSaida);
            this.Controls.Add(this.dtpSaida);
            this.Controls.Add(this.lblDataEmissao);
            this.Controls.Add(this.dtpEmissao);
            this.Controls.Add(this.btnPesquisarCliente);
            this.Controls.Add(this.lblCliente);
            this.Controls.Add(this.txtCliente);
            this.Controls.Add(this.lblIdCliente);
            this.Controls.Add(this.txtIdCliente);
            this.Controls.Add(this.lblSerie);
            this.Controls.Add(this.lblNumero);
            this.Controls.Add(this.txtNumero);
            this.Controls.Add(this.txtSerie);
            this.Name = "frmCadastroVenda";
            this.Text = "Cadastro Vendas";
            this.Controls.SetChildIndex(this.txtSerie, 0);
            this.Controls.SetChildIndex(this.txtNumero, 0);
            this.Controls.SetChildIndex(this.lblNumero, 0);
            this.Controls.SetChildIndex(this.lblSerie, 0);
            this.Controls.SetChildIndex(this.txtIdCliente, 0);
            this.Controls.SetChildIndex(this.lblIdCliente, 0);
            this.Controls.SetChildIndex(this.txtCliente, 0);
            this.Controls.SetChildIndex(this.lblCliente, 0);
            this.Controls.SetChildIndex(this.btnPesquisarCliente, 0);
            this.Controls.SetChildIndex(this.dtpEmissao, 0);
            this.Controls.SetChildIndex(this.lblDataEmissao, 0);
            this.Controls.SetChildIndex(this.dtpSaida, 0);
            this.Controls.SetChildIndex(this.lblDataSaida, 0);
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
            this.Controls.SetChildIndex(this.txtProduto, 0);
            this.Controls.SetChildIndex(this.lblProduto, 0);
            this.Controls.SetChildIndex(this.txtIdProduto, 0);
            this.Controls.SetChildIndex(this.lblIdProduto, 0);
            this.Controls.SetChildIndex(this.listViewProdutos, 0);
            this.Controls.SetChildIndex(this.lblFormaPagamento, 0);
            this.Controls.SetChildIndex(this.listViewCondPgto, 0);
            this.Controls.SetChildIndex(this.btnAdicionarCondPgto, 0);
            this.Controls.SetChildIndex(this.txtCondPgto, 0);
            this.Controls.SetChildIndex(this.btnLimparCondPgto, 0);
            this.Controls.SetChildIndex(this.lblTotalProdutos, 0);
            this.Controls.SetChildIndex(this.txtValorTotal, 0);
            this.Controls.SetChildIndex(this.lblValorTotal, 0);
            this.Controls.SetChildIndex(this.txtIdCondPgto, 0);
            this.Controls.SetChildIndex(this.lblIdCondicaoPgto, 0);
            this.Controls.SetChildIndex(this.lblModelo, 0);
            this.Controls.SetChildIndex(this.lblTotalCondiçãoPgto, 0);
            this.Controls.SetChildIndex(this.lblMotivoCancelamento, 0);
            this.Controls.SetChildIndex(this.txtIdFuncionario, 0);
            this.Controls.SetChildIndex(this.lblIDFuncionario, 0);
            this.Controls.SetChildIndex(this.txtFuncionario, 0);
            this.Controls.SetChildIndex(this.lblFuncionario, 0);
            this.Controls.SetChildIndex(this.btnPesquisarFuncionario, 0);
            this.Controls.SetChildIndex(this.btnSair, 0);
            this.Controls.SetChildIndex(this.txtCodigo, 0);
            this.Controls.SetChildIndex(this.chkInativo, 0);
            this.Controls.SetChildIndex(this.lblDataCriacao, 0);
            this.Controls.SetChildIndex(this.lblDataModificacao, 0);
            this.Controls.SetChildIndex(this.btnSalvar, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLimparProduto;
        private System.Windows.Forms.Button btnRemoverProduto;
        private System.Windows.Forms.Button btnEditarProduto;
        private System.Windows.Forms.Button btnAdicionarProduto;
        private System.Windows.Forms.Label lblTotalProduto;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label lblValorUnitario;
        private System.Windows.Forms.TextBox txtValorUnitario;
        private System.Windows.Forms.Label lblQuantidade;
        private System.Windows.Forms.TextBox txtQuantidade;
        private System.Windows.Forms.Button btnPesquisarProduto;
        private System.Windows.Forms.Label lblDataSaida;
        private System.Windows.Forms.DateTimePicker dtpSaida;
        private System.Windows.Forms.Label lblDataEmissao;
        private System.Windows.Forms.DateTimePicker dtpEmissao;
        private System.Windows.Forms.Button btnPesquisarCliente;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.Label lblIdCliente;
        private System.Windows.Forms.TextBox txtIdCliente;
        private System.Windows.Forms.Label lblSerie;
        private System.Windows.Forms.Label lblNumero;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.TextBox txtSerie;
        private System.Windows.Forms.Label lblIdProduto;
        private System.Windows.Forms.TextBox txtIdProduto;
        private System.Windows.Forms.Label lblProduto;
        private System.Windows.Forms.TextBox txtProduto;
        private System.Windows.Forms.Label lblIdCondicaoPgto;
        private System.Windows.Forms.TextBox txtIdCondPgto;
        private System.Windows.Forms.Label lblValorTotal;
        private System.Windows.Forms.TextBox txtValorTotal;
        private System.Windows.Forms.Label lblTotalProdutos;
        private System.Windows.Forms.Button btnLimparCondPgto;
        private System.Windows.Forms.TextBox txtCondPgto;
        private System.Windows.Forms.Button btnAdicionarCondPgto;
        protected System.Windows.Forms.ListView listViewCondPgto;
        private System.Windows.Forms.ColumnHeader NumParcela;
        private System.Windows.Forms.ColumnHeader Prazo;
        private System.Windows.Forms.ColumnHeader FormaPagamento;
        private System.Windows.Forms.ColumnHeader Valor;
        private System.Windows.Forms.Label lblFormaPagamento;
        protected System.Windows.Forms.ListView listViewProdutos;
        private System.Windows.Forms.ColumnHeader Codigo;
        private System.Windows.Forms.ColumnHeader Produto;
        private System.Windows.Forms.ColumnHeader Unidade;
        private System.Windows.Forms.ColumnHeader Quantidade;
        private System.Windows.Forms.ColumnHeader PrecoUnitario;
        private System.Windows.Forms.ColumnHeader Total;
        private System.Windows.Forms.Label lblModelo;
        private System.Windows.Forms.Label lblTotalCondiçãoPgto;
        private System.Windows.Forms.Label lblMotivoCancelamento;
        private System.Windows.Forms.Button btnPesquisarFuncionario;
        private System.Windows.Forms.Label lblFuncionario;
        private System.Windows.Forms.TextBox txtFuncionario;
        private System.Windows.Forms.Label lblIDFuncionario;
        private System.Windows.Forms.TextBox txtIdFuncionario;
    }
}
