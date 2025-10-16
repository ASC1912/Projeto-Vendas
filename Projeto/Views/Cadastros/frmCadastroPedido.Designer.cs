namespace Projeto.Views.Cadastros
{
    partial class frmCadastroPedido
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
            this.lblFuncionario = new System.Windows.Forms.Label();
            this.txtFuncionario = new System.Windows.Forms.TextBox();
            this.lblMesa = new System.Windows.Forms.Label();
            this.txtMesa = new System.Windows.Forms.TextBox();
            this.lblTotalPedido = new System.Windows.Forms.Label();
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
            this.lblProduto = new System.Windows.Forms.Label();
            this.txtProduto = new System.Windows.Forms.TextBox();
            this.listViewProdutos = new System.Windows.Forms.ListView();
            this.Codigo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Produto = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Unidade = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Quantidade = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PrecoUnitario = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Total = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblCodigo = new System.Windows.Forms.Label();
            this.lblObservacao = new System.Windows.Forms.Label();
            this.txtObservacao = new System.Windows.Forms.TextBox();
            this.btnPesquisarMesa = new System.Windows.Forms.Button();
            this.btnPesquisarFuncionario = new System.Windows.Forms.Button();
            this.lblQuantidadeClientes = new System.Windows.Forms.Label();
            this.txtQuantidadeClientes = new System.Windows.Forms.TextBox();
            this.chkFinalizado = new System.Windows.Forms.CheckBox();
            this.lblIdFuncionario = new System.Windows.Forms.Label();
            this.txtIdFuncionario = new System.Windows.Forms.TextBox();
            this.lblIdProduto = new System.Windows.Forms.Label();
            this.txtIdProduto = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtCodigo
            // 
            this.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            // 
            // btnSalvar
            // 
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // lblFuncionario
            // 
            this.lblFuncionario.AutoSize = true;
            this.lblFuncionario.Location = new System.Drawing.Point(522, 72);
            this.lblFuncionario.Name = "lblFuncionario";
            this.lblFuncionario.Size = new System.Drawing.Size(77, 16);
            this.lblFuncionario.TabIndex = 77;
            this.lblFuncionario.Text = "Funcionário";
            // 
            // txtFuncionario
            // 
            this.txtFuncionario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFuncionario.Location = new System.Drawing.Point(525, 91);
            this.txtFuncionario.Name = "txtFuncionario";
            this.txtFuncionario.ReadOnly = true;
            this.txtFuncionario.Size = new System.Drawing.Size(200, 22);
            this.txtFuncionario.TabIndex = 73;
            // 
            // lblMesa
            // 
            this.lblMesa.AutoSize = true;
            this.lblMesa.Location = new System.Drawing.Point(9, 71);
            this.lblMesa.Name = "lblMesa";
            this.lblMesa.Size = new System.Drawing.Size(111, 16);
            this.lblMesa.TabIndex = 76;
            this.lblMesa.Text = "Número da Mesa";
            // 
            // txtMesa
            // 
            this.txtMesa.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMesa.Location = new System.Drawing.Point(12, 90);
            this.txtMesa.Name = "txtMesa";
            this.txtMesa.ReadOnly = true;
            this.txtMesa.Size = new System.Drawing.Size(200, 22);
            this.txtMesa.TabIndex = 72;
            // 
            // lblTotalPedido
            // 
            this.lblTotalPedido.AutoSize = true;
            this.lblTotalPedido.Location = new System.Drawing.Point(965, 433);
            this.lblTotalPedido.Name = "lblTotalPedido";
            this.lblTotalPedido.Size = new System.Drawing.Size(66, 16);
            this.lblTotalPedido.TabIndex = 161;
            this.lblTotalPedido.Text = "Total (R$)";
            // 
            // btnLimparProduto
            // 
            this.btnLimparProduto.Location = new System.Drawing.Point(1112, 218);
            this.btnLimparProduto.Name = "btnLimparProduto";
            this.btnLimparProduto.Size = new System.Drawing.Size(91, 23);
            this.btnLimparProduto.TabIndex = 160;
            this.btnLimparProduto.Text = "Limpar";
            this.btnLimparProduto.UseVisualStyleBackColor = true;
            this.btnLimparProduto.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // btnRemoverProduto
            // 
            this.btnRemoverProduto.Location = new System.Drawing.Point(1015, 217);
            this.btnRemoverProduto.Name = "btnRemoverProduto";
            this.btnRemoverProduto.Size = new System.Drawing.Size(91, 23);
            this.btnRemoverProduto.TabIndex = 159;
            this.btnRemoverProduto.Text = "Remover";
            this.btnRemoverProduto.UseVisualStyleBackColor = true;
            this.btnRemoverProduto.Click += new System.EventHandler(this.btnRemover_Click);
            // 
            // btnEditarProduto
            // 
            this.btnEditarProduto.Location = new System.Drawing.Point(918, 218);
            this.btnEditarProduto.Name = "btnEditarProduto";
            this.btnEditarProduto.Size = new System.Drawing.Size(91, 23);
            this.btnEditarProduto.TabIndex = 158;
            this.btnEditarProduto.Text = "Editar";
            this.btnEditarProduto.UseVisualStyleBackColor = true;
            this.btnEditarProduto.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnAdicionarProduto
            // 
            this.btnAdicionarProduto.Location = new System.Drawing.Point(821, 218);
            this.btnAdicionarProduto.Name = "btnAdicionarProduto";
            this.btnAdicionarProduto.Size = new System.Drawing.Size(91, 23);
            this.btnAdicionarProduto.TabIndex = 157;
            this.btnAdicionarProduto.Text = "Adicionar";
            this.btnAdicionarProduto.UseVisualStyleBackColor = true;
            this.btnAdicionarProduto.Click += new System.EventHandler(this.btnAdicionar_Click);
            // 
            // lblTotalProduto
            // 
            this.lblTotalProduto.AutoSize = true;
            this.lblTotalProduto.Location = new System.Drawing.Point(668, 198);
            this.lblTotalProduto.Name = "lblTotalProduto";
            this.lblTotalProduto.Size = new System.Drawing.Size(66, 16);
            this.lblTotalProduto.TabIndex = 156;
            this.lblTotalProduto.Text = "Total (R$)";
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(668, 217);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(126, 22);
            this.txtTotal.TabIndex = 155;
            // 
            // lblValorUnitario
            // 
            this.lblValorUnitario.AutoSize = true;
            this.lblValorUnitario.Location = new System.Drawing.Point(574, 199);
            this.lblValorUnitario.Name = "lblValorUnitario";
            this.lblValorUnitario.Size = new System.Drawing.Size(88, 16);
            this.lblValorUnitario.TabIndex = 154;
            this.lblValorUnitario.Text = "Valor Unitário";
            // 
            // txtValorUnitario
            // 
            this.txtValorUnitario.Location = new System.Drawing.Point(574, 218);
            this.txtValorUnitario.Name = "txtValorUnitario";
            this.txtValorUnitario.Size = new System.Drawing.Size(88, 22);
            this.txtValorUnitario.TabIndex = 153;
            // 
            // lblQuantidade
            // 
            this.lblQuantidade.AutoSize = true;
            this.lblQuantidade.Location = new System.Drawing.Point(448, 199);
            this.lblQuantidade.Name = "lblQuantidade";
            this.lblQuantidade.Size = new System.Drawing.Size(77, 16);
            this.lblQuantidade.TabIndex = 152;
            this.lblQuantidade.Text = "Quantidade";
            // 
            // txtQuantidade
            // 
            this.txtQuantidade.Location = new System.Drawing.Point(448, 218);
            this.txtQuantidade.Name = "txtQuantidade";
            this.txtQuantidade.Size = new System.Drawing.Size(100, 22);
            this.txtQuantidade.TabIndex = 151;
            // 
            // btnPesquisarProduto
            // 
            this.btnPesquisarProduto.Location = new System.Drawing.Point(327, 216);
            this.btnPesquisarProduto.Name = "btnPesquisarProduto";
            this.btnPesquisarProduto.Size = new System.Drawing.Size(91, 23);
            this.btnPesquisarProduto.TabIndex = 150;
            this.btnPesquisarProduto.Text = "Pesquisar";
            this.btnPesquisarProduto.UseVisualStyleBackColor = true;
            this.btnPesquisarProduto.Click += new System.EventHandler(this.btnPesquisarProduto_Click);
            // 
            // lblProduto
            // 
            this.lblProduto.AutoSize = true;
            this.lblProduto.Location = new System.Drawing.Point(88, 198);
            this.lblProduto.Name = "lblProduto";
            this.lblProduto.Size = new System.Drawing.Size(54, 16);
            this.lblProduto.TabIndex = 149;
            this.lblProduto.Text = "Produto";
            // 
            // txtProduto
            // 
            this.txtProduto.Location = new System.Drawing.Point(88, 217);
            this.txtProduto.Name = "txtProduto";
            this.txtProduto.ReadOnly = true;
            this.txtProduto.Size = new System.Drawing.Size(211, 22);
            this.txtProduto.TabIndex = 148;
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
            this.listViewProdutos.Location = new System.Drawing.Point(15, 267);
            this.listViewProdutos.Name = "listViewProdutos";
            this.listViewProdutos.Size = new System.Drawing.Size(1230, 163);
            this.listViewProdutos.TabIndex = 147;
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
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Location = new System.Drawing.Point(12, 8);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(51, 16);
            this.lblCodigo.TabIndex = 162;
            this.lblCodigo.Text = "Código";
            // 
            // lblObservacao
            // 
            this.lblObservacao.AutoSize = true;
            this.lblObservacao.Location = new System.Drawing.Point(9, 133);
            this.lblObservacao.Name = "lblObservacao";
            this.lblObservacao.Size = new System.Drawing.Size(82, 16);
            this.lblObservacao.TabIndex = 164;
            this.lblObservacao.Text = "Observação";
            // 
            // txtObservacao
            // 
            this.txtObservacao.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtObservacao.Location = new System.Drawing.Point(12, 152);
            this.txtObservacao.Name = "txtObservacao";
            this.txtObservacao.Size = new System.Drawing.Size(200, 22);
            this.txtObservacao.TabIndex = 163;
            // 
            // btnPesquisarMesa
            // 
            this.btnPesquisarMesa.Location = new System.Drawing.Point(251, 89);
            this.btnPesquisarMesa.Name = "btnPesquisarMesa";
            this.btnPesquisarMesa.Size = new System.Drawing.Size(91, 23);
            this.btnPesquisarMesa.TabIndex = 165;
            this.btnPesquisarMesa.Text = "Pesquisar";
            this.btnPesquisarMesa.UseVisualStyleBackColor = true;
            this.btnPesquisarMesa.Click += new System.EventHandler(this.btnPesquisarMesa_Click);
            // 
            // btnPesquisarFuncionario
            // 
            this.btnPesquisarFuncionario.Location = new System.Drawing.Point(745, 91);
            this.btnPesquisarFuncionario.Name = "btnPesquisarFuncionario";
            this.btnPesquisarFuncionario.Size = new System.Drawing.Size(91, 23);
            this.btnPesquisarFuncionario.TabIndex = 166;
            this.btnPesquisarFuncionario.Text = "Pesquisar";
            this.btnPesquisarFuncionario.UseVisualStyleBackColor = true;
            this.btnPesquisarFuncionario.Click += new System.EventHandler(this.btnPesquisarFuncionario_Click);
            // 
            // lblQuantidadeClientes
            // 
            this.lblQuantidadeClientes.AutoSize = true;
            this.lblQuantidadeClientes.Location = new System.Drawing.Point(417, 133);
            this.lblQuantidadeClientes.Name = "lblQuantidadeClientes";
            this.lblQuantidadeClientes.Size = new System.Drawing.Size(147, 16);
            this.lblQuantidadeClientes.TabIndex = 168;
            this.lblQuantidadeClientes.Text = "Quantidade de Clientes";
            // 
            // txtQuantidadeClientes
            // 
            this.txtQuantidadeClientes.Location = new System.Drawing.Point(417, 152);
            this.txtQuantidadeClientes.Name = "txtQuantidadeClientes";
            this.txtQuantidadeClientes.Size = new System.Drawing.Size(100, 22);
            this.txtQuantidadeClientes.TabIndex = 167;
            // 
            // chkFinalizado
            // 
            this.chkFinalizado.AutoSize = true;
            this.chkFinalizado.Location = new System.Drawing.Point(888, 94);
            this.chkFinalizado.Name = "chkFinalizado";
            this.chkFinalizado.Size = new System.Drawing.Size(91, 20);
            this.chkFinalizado.TabIndex = 169;
            this.chkFinalizado.Text = "Finalizado";
            this.chkFinalizado.UseVisualStyleBackColor = true;
            // 
            // lblIdFuncionario
            // 
            this.lblIdFuncionario.AutoSize = true;
            this.lblIdFuncionario.Location = new System.Drawing.Point(414, 72);
            this.lblIdFuncionario.Name = "lblIdFuncionario";
            this.lblIdFuncionario.Size = new System.Drawing.Size(98, 16);
            this.lblIdFuncionario.TabIndex = 173;
            this.lblIdFuncionario.Text = "ID Funcionário*";
            // 
            // txtIdFuncionario
            // 
            this.txtIdFuncionario.Location = new System.Drawing.Point(417, 91);
            this.txtIdFuncionario.Name = "txtIdFuncionario";
            this.txtIdFuncionario.Size = new System.Drawing.Size(95, 22);
            this.txtIdFuncionario.TabIndex = 172;
            this.txtIdFuncionario.Leave += new System.EventHandler(this.txtIdFuncionario_Leave);
            // 
            // lblIdProduto
            // 
            this.lblIdProduto.AutoSize = true;
            this.lblIdProduto.Location = new System.Drawing.Point(9, 199);
            this.lblIdProduto.Name = "lblIdProduto";
            this.lblIdProduto.Size = new System.Drawing.Size(75, 16);
            this.lblIdProduto.TabIndex = 175;
            this.lblIdProduto.Text = "ID Produto*";
            // 
            // txtIdProduto
            // 
            this.txtIdProduto.Location = new System.Drawing.Point(12, 218);
            this.txtIdProduto.Name = "txtIdProduto";
            this.txtIdProduto.Size = new System.Drawing.Size(67, 22);
            this.txtIdProduto.TabIndex = 174;
            this.txtIdProduto.Leave += new System.EventHandler(this.txtIdProduto_Leave);
            // 
            // frmCadastroPedido
            // 
            //this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            //this.ClientSize = new System.Drawing.Size(1342, 681);
            this.Controls.Add(this.lblIdProduto);
            this.Controls.Add(this.txtIdProduto);
            this.Controls.Add(this.lblIdFuncionario);
            this.Controls.Add(this.txtIdFuncionario);
            this.Controls.Add(this.chkFinalizado);
            this.Controls.Add(this.lblQuantidadeClientes);
            this.Controls.Add(this.txtQuantidadeClientes);
            this.Controls.Add(this.btnPesquisarFuncionario);
            this.Controls.Add(this.btnPesquisarMesa);
            this.Controls.Add(this.lblObservacao);
            this.Controls.Add(this.txtObservacao);
            this.Controls.Add(this.lblCodigo);
            this.Controls.Add(this.lblTotalPedido);
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
            this.Controls.Add(this.lblFuncionario);
            this.Controls.Add(this.txtFuncionario);
            this.Controls.Add(this.lblMesa);
            this.Controls.Add(this.txtMesa);
            this.Name = "frmCadastroPedido";
            this.Text = "Cadastro Produtos";
            this.Controls.SetChildIndex(this.txtMesa, 0);
            this.Controls.SetChildIndex(this.lblMesa, 0);
            this.Controls.SetChildIndex(this.txtFuncionario, 0);
            this.Controls.SetChildIndex(this.lblFuncionario, 0);
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
            this.Controls.SetChildIndex(this.lblTotalPedido, 0);
            this.Controls.SetChildIndex(this.lblCodigo, 0);
            this.Controls.SetChildIndex(this.txtObservacao, 0);
            this.Controls.SetChildIndex(this.lblObservacao, 0);
            this.Controls.SetChildIndex(this.btnPesquisarMesa, 0);
            this.Controls.SetChildIndex(this.btnPesquisarFuncionario, 0);
            this.Controls.SetChildIndex(this.txtQuantidadeClientes, 0);
            this.Controls.SetChildIndex(this.lblQuantidadeClientes, 0);
            this.Controls.SetChildIndex(this.chkFinalizado, 0);
            this.Controls.SetChildIndex(this.btnSair, 0);
            this.Controls.SetChildIndex(this.txtCodigo, 0);
            this.Controls.SetChildIndex(this.chkInativo, 0);
            this.Controls.SetChildIndex(this.lblDataCriacao, 0);
            this.Controls.SetChildIndex(this.lblDataModificacao, 0);
            this.Controls.SetChildIndex(this.btnSalvar, 0);
            this.Controls.SetChildIndex(this.txtIdFuncionario, 0);
            this.Controls.SetChildIndex(this.lblIdFuncionario, 0);
            this.Controls.SetChildIndex(this.txtIdProduto, 0);
            this.Controls.SetChildIndex(this.lblIdProduto, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblFuncionario;
        private System.Windows.Forms.TextBox txtFuncionario;
        private System.Windows.Forms.Label lblMesa;
        private System.Windows.Forms.TextBox txtMesa;
        private System.Windows.Forms.Label lblTotalPedido;
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
        private System.Windows.Forms.Label lblProduto;
        private System.Windows.Forms.TextBox txtProduto;
        protected System.Windows.Forms.ListView listViewProdutos;
        private System.Windows.Forms.ColumnHeader Codigo;
        private System.Windows.Forms.ColumnHeader Produto;
        private System.Windows.Forms.ColumnHeader Unidade;
        private System.Windows.Forms.ColumnHeader Quantidade;
        private System.Windows.Forms.ColumnHeader PrecoUnitario;
        private System.Windows.Forms.ColumnHeader Total;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.Label lblObservacao;
        private System.Windows.Forms.TextBox txtObservacao;
        private System.Windows.Forms.Button btnPesquisarMesa;
        private System.Windows.Forms.Button btnPesquisarFuncionario;
        private System.Windows.Forms.Label lblQuantidadeClientes;
        private System.Windows.Forms.TextBox txtQuantidadeClientes;
        private System.Windows.Forms.CheckBox chkFinalizado;
        private System.Windows.Forms.Label lblIdFuncionario;
        private System.Windows.Forms.TextBox txtIdFuncionario;
        private System.Windows.Forms.Label lblIdProduto;
        private System.Windows.Forms.TextBox txtIdProduto;
    }
}
