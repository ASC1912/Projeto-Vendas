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
    public partial class frmCadastroMarca : Projeto.frmBase
    {
        public bool modoEdicao = false;
        public frmCadastroMarca() : base() 
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
        }

        public void CarregarMarca(int id, string nome, bool status, DateTime? dataCriacao, DateTime? dataModificacao)
        {
            modoEdicao = true;

            txtCodigo.Text = id.ToString();
            txtNome.Text = nome;
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
                bool status = !chkInativo.Checked;

                DateTime dataCriacao = id == 0
                    ? DateTime.Now
                    : DateTime.Parse(lblDataCriacao.Text.Replace("Criado em: ", ""));

                DateTime dataModificacao = DateTime.Now;

                Marca marca = new Marca
                {
                    Id = id,
                    Nome = nome,
                    Status = status,
                    DataCriacao = dataCriacao,
                    DataModificacao = dataModificacao
                };

                MarcaController controller = new MarcaController();
                controller.Salvar(marca);

                MessageBox.Show("Marca salva com sucesso!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private void frmCadastroMarca_Load(object sender, EventArgs e)
        {
            lblDataCriacao.Visible = modoEdicao;
            lblDataModificacao.Visible = modoEdicao;
        }

       
    }
}
