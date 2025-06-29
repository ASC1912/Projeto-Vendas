using Projeto.Controller;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Projeto.Utils;


namespace Projeto.Views
{
    public partial class frmCadastroPais : Projeto.frmBase
    {
        public bool modoEdicao = false;

        public frmCadastroPais() : base()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
        }

        public void CarregarPais(int id, string nome, bool status, DateTime? dataCriacao, DateTime? dataModificacao)
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
            if (!Validador.CampoObrigatorio(txtNome, "O nome do país é obrigatório.")) return;

            try
            {
                int id = string.IsNullOrEmpty(txtCodigo.Text) ? 0 : int.Parse(txtCodigo.Text);
                string nome = txtNome.Text;
                bool status = !chkInativo.Checked;

                DateTime dataCriacao = id == 0
                    ? DateTime.Now
                    : DateTime.Parse(lblDataCriacao.Text.Replace("Criado em: ", ""));

                DateTime dataModificacao = DateTime.Now;

                Pais pais = new Pais
                {
                    Id = id,
                    NomePais = nome,
                    Ativo = status,
                    DataCadastro = dataCriacao,
                    DataAlteracao = dataModificacao
                };

                PaisController controller = new PaisController();
                controller.Salvar(pais);

                MessageBox.Show("País salvo com sucesso!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private void frmCadastroPais_Load(object sender, EventArgs e)
        {
            if (modoEdicao == false)
            {
                txtCodigo.Text = "0";

                DateTime agora = DateTime.Now;

                lblDataCriacao.Text = $"Criado em: {agora:dd/MM/yyyy HH:mm}";
                lblDataModificacao.Text = $"Modificado em: {agora:dd/MM/yyyy HH:mm}";
            }
          
        }
    }
}
