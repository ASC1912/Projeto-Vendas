namespace Projeto
{
    partial class Form1
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
            this.cbFormaPagamento = new System.Windows.Forms.ComboBox();
            this.lblFormaPagamento = new System.Windows.Forms.Label();
            this.lblCondicaoPagamento = new System.Windows.Forms.Label();
            this.cbCondicaoPagamento = new System.Windows.Forms.ComboBox();
            this.numParcelas = new System.Windows.Forms.NumericUpDown();
            this.lblNumeroParcelas = new System.Windows.Forms.Label();
            this.lblPorcentagem = new System.Windows.Forms.Label();
            this.numPorcentagem = new System.Windows.Forms.NumericUpDown();
            this.chkVista = new System.Windows.Forms.CheckBox();
            this.btnGerarParcelas = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.cadastroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formaDePagamentoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.condiçãoDePagamentoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.consultaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formaDePagamentoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.condiçãoDePagamentoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAtualizar = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.numParcelas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPorcentagem)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbFormaPagamento
            // 
            this.cbFormaPagamento.FormattingEnabled = true;
            this.cbFormaPagamento.Location = new System.Drawing.Point(68, 79);
            this.cbFormaPagamento.Name = "cbFormaPagamento";
            this.cbFormaPagamento.Size = new System.Drawing.Size(121, 24);
            this.cbFormaPagamento.TabIndex = 0;
            // 
            // lblFormaPagamento
            // 
            this.lblFormaPagamento.AutoSize = true;
            this.lblFormaPagamento.Location = new System.Drawing.Point(65, 60);
            this.lblFormaPagamento.Name = "lblFormaPagamento";
            this.lblFormaPagamento.Size = new System.Drawing.Size(138, 16);
            this.lblFormaPagamento.TabIndex = 1;
            this.lblFormaPagamento.Text = "Forma de Pagamento";
            // 
            // lblCondicaoPagamento
            // 
            this.lblCondicaoPagamento.AutoSize = true;
            this.lblCondicaoPagamento.Location = new System.Drawing.Point(382, 60);
            this.lblCondicaoPagamento.Name = "lblCondicaoPagamento";
            this.lblCondicaoPagamento.Size = new System.Drawing.Size(157, 16);
            this.lblCondicaoPagamento.TabIndex = 3;
            this.lblCondicaoPagamento.Text = "Condição de Pagamento";
            // 
            // cbCondicaoPagamento
            // 
            this.cbCondicaoPagamento.FormattingEnabled = true;
            this.cbCondicaoPagamento.Location = new System.Drawing.Point(385, 79);
            this.cbCondicaoPagamento.Name = "cbCondicaoPagamento";
            this.cbCondicaoPagamento.Size = new System.Drawing.Size(121, 24);
            this.cbCondicaoPagamento.TabIndex = 2;
            // 
            // numParcelas
            // 
            this.numParcelas.Location = new System.Drawing.Point(68, 161);
            this.numParcelas.Name = "numParcelas";
            this.numParcelas.Size = new System.Drawing.Size(120, 22);
            this.numParcelas.TabIndex = 5;
            // 
            // lblNumeroParcelas
            // 
            this.lblNumeroParcelas.AutoSize = true;
            this.lblNumeroParcelas.Location = new System.Drawing.Point(65, 142);
            this.lblNumeroParcelas.Name = "lblNumeroParcelas";
            this.lblNumeroParcelas.Size = new System.Drawing.Size(131, 16);
            this.lblNumeroParcelas.TabIndex = 6;
            this.lblNumeroParcelas.Text = "Número de Parcelas";
            // 
            // lblPorcentagem
            // 
            this.lblPorcentagem.AutoSize = true;
            this.lblPorcentagem.Location = new System.Drawing.Point(382, 142);
            this.lblPorcentagem.Name = "lblPorcentagem";
            this.lblPorcentagem.Size = new System.Drawing.Size(88, 16);
            this.lblPorcentagem.TabIndex = 8;
            this.lblPorcentagem.Text = "Porcentagem";
            // 
            // numPorcentagem
            // 
            this.numPorcentagem.Location = new System.Drawing.Point(385, 161);
            this.numPorcentagem.Name = "numPorcentagem";
            this.numPorcentagem.Size = new System.Drawing.Size(120, 22);
            this.numPorcentagem.TabIndex = 7;
            // 
            // chkVista
            // 
            this.chkVista.AutoSize = true;
            this.chkVista.Location = new System.Drawing.Point(536, 161);
            this.chkVista.Name = "chkVista";
            this.chkVista.Size = new System.Drawing.Size(71, 20);
            this.chkVista.TabIndex = 9;
            this.chkVista.Text = "À Vista";
            this.chkVista.UseVisualStyleBackColor = true;
            // 
            // btnGerarParcelas
            // 
            this.btnGerarParcelas.Location = new System.Drawing.Point(677, 159);
            this.btnGerarParcelas.Name = "btnGerarParcelas";
            this.btnGerarParcelas.Size = new System.Drawing.Size(75, 23);
            this.btnGerarParcelas.TabIndex = 10;
            this.btnGerarParcelas.Text = "Gerar";
            this.btnGerarParcelas.UseVisualStyleBackColor = true;
            this.btnGerarParcelas.Click += new System.EventHandler(this.btnGerarParcelas_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cadastroToolStripMenuItem,
            this.consultaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(838, 28);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // cadastroToolStripMenuItem
            // 
            this.cadastroToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.formaDePagamentoToolStripMenuItem,
            this.condiçãoDePagamentoToolStripMenuItem});
            this.cadastroToolStripMenuItem.Name = "cadastroToolStripMenuItem";
            this.cadastroToolStripMenuItem.Size = new System.Drawing.Size(82, 24);
            this.cadastroToolStripMenuItem.Text = "Cadastro";
            // 
            // formaDePagamentoToolStripMenuItem
            // 
            this.formaDePagamentoToolStripMenuItem.Name = "formaDePagamentoToolStripMenuItem";
            this.formaDePagamentoToolStripMenuItem.Size = new System.Drawing.Size(255, 26);
            this.formaDePagamentoToolStripMenuItem.Text = "Forma de Pagamento";
            this.formaDePagamentoToolStripMenuItem.Click += new System.EventHandler(this.formaDePagamentoToolStripMenuItem_Click);
            // 
            // condiçãoDePagamentoToolStripMenuItem
            // 
            this.condiçãoDePagamentoToolStripMenuItem.Name = "condiçãoDePagamentoToolStripMenuItem";
            this.condiçãoDePagamentoToolStripMenuItem.Size = new System.Drawing.Size(255, 26);
            this.condiçãoDePagamentoToolStripMenuItem.Text = "Condição de Pagamento";
            this.condiçãoDePagamentoToolStripMenuItem.Click += new System.EventHandler(this.condiçãoDePagamentoToolStripMenuItem_Click);
            // 
            // consultaToolStripMenuItem
            // 
            this.consultaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.formaDePagamentoToolStripMenuItem1,
            this.condiçãoDePagamentoToolStripMenuItem1});
            this.consultaToolStripMenuItem.Name = "consultaToolStripMenuItem";
            this.consultaToolStripMenuItem.Size = new System.Drawing.Size(80, 24);
            this.consultaToolStripMenuItem.Text = "Consulta";
            // 
            // formaDePagamentoToolStripMenuItem1
            // 
            this.formaDePagamentoToolStripMenuItem1.Name = "formaDePagamentoToolStripMenuItem1";
            this.formaDePagamentoToolStripMenuItem1.Size = new System.Drawing.Size(255, 26);
            this.formaDePagamentoToolStripMenuItem1.Text = "Forma de Pagamento";
            this.formaDePagamentoToolStripMenuItem1.Click += new System.EventHandler(this.formaDePagamentoToolStripMenuItem1_Click);
            // 
            // condiçãoDePagamentoToolStripMenuItem1
            // 
            this.condiçãoDePagamentoToolStripMenuItem1.Name = "condiçãoDePagamentoToolStripMenuItem1";
            this.condiçãoDePagamentoToolStripMenuItem1.Size = new System.Drawing.Size(255, 26);
            this.condiçãoDePagamentoToolStripMenuItem1.Text = "Condição de Pagamento";
            this.condiçãoDePagamentoToolStripMenuItem1.Click += new System.EventHandler(this.condiçãoDePagamentoToolStripMenuItem1_Click);
            // 
            // btnAtualizar
            // 
            this.btnAtualizar.Location = new System.Drawing.Point(677, 80);
            this.btnAtualizar.Name = "btnAtualizar";
            this.btnAtualizar.Size = new System.Drawing.Size(75, 23);
            this.btnAtualizar.TabIndex = 12;
            this.btnAtualizar.Text = "Atualizar";
            this.btnAtualizar.UseVisualStyleBackColor = true;
            this.btnAtualizar.Click += new System.EventHandler(this.btnAtualizar_Click);
            // 
            // listView1
            // 
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(68, 225);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(624, 239);
            this.listView1.TabIndex = 13;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 476);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.btnAtualizar);
            this.Controls.Add(this.btnGerarParcelas);
            this.Controls.Add(this.chkVista);
            this.Controls.Add(this.lblPorcentagem);
            this.Controls.Add(this.numPorcentagem);
            this.Controls.Add(this.lblNumeroParcelas);
            this.Controls.Add(this.numParcelas);
            this.Controls.Add(this.lblCondicaoPagamento);
            this.Controls.Add(this.cbCondicaoPagamento);
            this.Controls.Add(this.lblFormaPagamento);
            this.Controls.Add(this.cbFormaPagamento);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Sistema";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numParcelas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPorcentagem)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbFormaPagamento;
        private System.Windows.Forms.Label lblFormaPagamento;
        private System.Windows.Forms.Label lblCondicaoPagamento;
        private System.Windows.Forms.ComboBox cbCondicaoPagamento;
        private System.Windows.Forms.NumericUpDown numParcelas;
        private System.Windows.Forms.Label lblNumeroParcelas;
        private System.Windows.Forms.Label lblPorcentagem;
        private System.Windows.Forms.NumericUpDown numPorcentagem;
        private System.Windows.Forms.CheckBox chkVista;
        private System.Windows.Forms.Button btnGerarParcelas;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cadastroToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formaDePagamentoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem condiçãoDePagamentoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem consultaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formaDePagamentoToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem condiçãoDePagamentoToolStripMenuItem1;
        private System.Windows.Forms.Button btnAtualizar;
        protected System.Windows.Forms.ListView listView1;
    }
}

