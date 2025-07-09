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
        public bool modoExclusao = false;

        public frmCadastroFrmPgto() : base()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
        }

        public void CarregarFormaPagamento(int id, string descricao, bool ativo, DateTime? dataCadastro, DateTime? dataAlteracao)
        {
            modoEdicao = true;

            txtCodigo.Text = id.ToString();
            txtDescricao.Text = descricao;
            chkInativo.Checked = !ativo;

            lblDataCriacao.Text = dataCadastro.HasValue
                ? $"Criado em: {dataCadastro.Value:dd/MM/yyyy HH:mm}"
                : "Criado em: -";

            lblDataModificacao.Text = dataAlteracao.HasValue
                ? $"Modificado em: {dataAlteracao.Value:dd/MM/yyyy HH:mm}"
                : "Modificado em: -";
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (modoExclusao)
            {
                var confirmacao = MessageBox.Show("Tem certeza que deseja excluir esta forma de pagamento?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmacao == DialogResult.Yes)
                {
                    try
                    {
                        int id = int.Parse(txtCodigo.Text);
                        FormaPagamentoController controller = new FormaPagamentoController();
                        controller.Excluir(id);
                        MessageBox.Show("Forma de pagamento excluída com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("Cannot delete or update a parent row"))
                        {
                            MessageBox.Show(
                                "Não é possível excluir este item, pois existem registros vinculados a ele.",
                                "Erro ao excluir",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                        }
                        else
                        {
                            MessageBox.Show($"Erro ao excluir: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
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
                        Ativo = status,
                        DataCadastro = dataCriacao,
                        DataAlteracao = dataModificacao
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


        private void frmCadastroFrmPgto_Load(object sender, EventArgs e)
        {
            if (modoExclusao)
            {
                btnSalvar.Text = "Deletar";
                txtDescricao.Enabled = false;
                chkInativo.Enabled = false;
            }
            else if (modoEdicao == false)
            {
                txtCodigo.Text = "0";

                DateTime agora = DateTime.Now;

                lblDataCriacao.Text = $"Criado em: {agora:dd/MM/yyyy HH:mm}";
                lblDataModificacao.Text = $"Modificado em: {agora:dd/MM/yyyy HH:mm}";
            }
        }
    }
}
