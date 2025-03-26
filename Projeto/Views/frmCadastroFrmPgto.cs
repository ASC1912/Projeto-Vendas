using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Projeto.Models;
using Projeto.Controller;

namespace Projeto
{
    public partial class frmCadastroFrmPgto : Projeto.frmBase
    {

        public frmCadastroFrmPgto()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
        }

        public void CarregarFormaPagamento(int id, string descricao)
        {
            txtCodigo.Text = id.ToString();
            txtDescricao.Text = descricao;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = string.IsNullOrEmpty(txtCodigo.Text) ? 0 : int.Parse(txtCodigo.Text);
                string descricao = txtDescricao.Text;

                FormaPagamento forma = new FormaPagamento
                {
                    Id = id,
                    Descricao = descricao
                };

                FormaPagamentoController controller = new FormaPagamentoController();
                controller.Salvar(forma);

                MessageBox.Show("Forma de pagamento salva com sucesso!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }

        }
    }
}
