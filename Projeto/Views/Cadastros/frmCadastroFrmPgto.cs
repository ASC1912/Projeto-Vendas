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
using Projeto.Utils;

namespace Projeto
{
    public partial class frmCadastroFrmPgto : Projeto.frmBase
    {
        public bool modoEdicao = false;
        public frmCadastroFrmPgto() : base()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
        }

        public void CarregarFormaPagamento(int id, string descricao, bool status, DateTime? dataCriacao, DateTime? dataModificacao)
        {
            modoEdicao = true;

            txtCodigo.Text = id.ToString();
            txtDescricao.Text = descricao;
            chkInativo.Checked = !status;

            lblDataCriacao.Text = dataCriacao.HasValue
                ? $"Criado em: {dataCriacao.Value:dd/MM/yyyy HH:mm}"
                : "Criado em: -";

            lblDataModificacao.Text = dataModificacao.HasValue
                ? $"Modificado em: {dataModificacao.Value:dd/MM/yyyy HH:mm}"
                : "Modificado em: -";
        }


        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!Validador.CampoObrigatorio(txtDescricao, "A descrição é obrigatória.")) return;

            try
            {
                int id = string.IsNullOrEmpty(txtCodigo.Text) ? 0 : int.Parse(txtCodigo.Text);
                string descricao = txtDescricao.Text;
                bool status = !chkInativo.Checked;

                DateTime dataCriacao = id == 0
                    ? DateTime.Now
                    : DateTime.Parse(lblDataCriacao.Text.Replace("Criado em: ", ""));

                DateTime dataModificacao = DateTime.Now;

                FormaPagamento forma = new FormaPagamento
                {
                    Id = id,
                    Descricao = descricao,
                    Status = status,
                    DataCriacao = dataCriacao,
                    DataModificacao = dataModificacao
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


        private void frmCadastroFrmPgto_Load(object sender, EventArgs e)
        {
            lblDataCriacao.Visible = modoEdicao;
            lblDataModificacao.Visible = modoEdicao;
        }
    }
}
