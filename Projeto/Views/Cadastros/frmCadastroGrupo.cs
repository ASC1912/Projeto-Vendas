using Projeto.Controller;
using Projeto.Models;
using Projeto.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Projeto.Views.Cadastros
{
    public partial class frmCadastroGrupo : Projeto.frmBase
    {
        public bool modoEdicao = false;
        public frmCadastroGrupo()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
        }

        public void CarregarGrupo(int id, string nome, string descricao, bool status, DateTime? dataCriacao, DateTime? dataModificacao)
        {
            modoEdicao = true;

            txtCodigo.Text = id.ToString();
            txtNome.Text = nome;
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
            if (!Validador.CampoObrigatorio(txtNome, "O nome é obrigatório.")) return;

            try
            {
                int id = string.IsNullOrEmpty(txtCodigo.Text) ? 0 : int.Parse(txtCodigo.Text);
                string nome = txtNome.Text;
                string descricao = txtDescricao.Text;
                bool status = !chkInativo.Checked;

                DateTime dataCriacao = id == 0
                    ? DateTime.Now
                    : DateTime.Parse(lblDataCriacao.Text.Replace("Criado em: ", ""));

                DateTime dataModificacao = DateTime.Now;

                Grupo grupo = new Grupo
                {
                    Id = id,
                    Nome = nome,
                    Descricao = descricao,
                    Status = status,
                    DataCriacao = dataCriacao,
                    DataModificacao = dataModificacao
                };

                GrupoController controller = new GrupoController();
                controller.Salvar(grupo);

                MessageBox.Show("Grupo salvo com sucesso!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private void frmCadastroGrupo_Load(object sender, EventArgs e)
        {
            lblDataCriacao.Visible = modoEdicao;
            lblDataModificacao.Visible = modoEdicao;
        }
    }
}
