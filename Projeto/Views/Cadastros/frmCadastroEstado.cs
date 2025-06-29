using Projeto.Controller;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Projeto.Utils;


namespace Projeto.Views
{
    public partial class frmCadastroEstado : Projeto.frmBase
    {
        private PaisController paisController = new PaisController();
        private EstadoController controller = new EstadoController();
        private int paisSelecionadoId = -1;
        public bool modoEdicao = false;


        public frmCadastroEstado() : base()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
        }

        public void CarregarEstado(int id, string nomeEstado, string uf, string nomePais, int paisId, bool ativo, DateTime? dataCadastro, DateTime? dataAlteracao)
        {
            modoEdicao = true;

            txtCodigo.Text = id.ToString();
            txtNome.Text = nomeEstado;
            txtUF.Text = uf;
            txtPais.Text = nomePais;
            paisSelecionadoId = paisId;
            chkInativo.Checked = !ativo;

            lblDataCriacao.Text = dataCadastro.HasValue
                ? $"Criado em: {dataCadastro.Value:dd/MM/yyyy HH:mm}"
                : "Criado em: -";

            lblDataModificacao.Text = dataAlteracao.HasValue
                ? $"Modificado em: {dataAlteracao.Value:dd/MM/yyyy HH:mm}"
                : "Modificado em: -";
        }


        private void frmCadastroEstado_Load(object sender, EventArgs e)
        {
            if (modoEdicao == false)
            {
                txtCodigo.Text = "0";

                DateTime agora = DateTime.Now;

                lblDataCriacao.Text = $"Criado em: {agora:dd/MM/yyyy HH:mm}";
                lblDataModificacao.Text = $"Modificado em: {agora:dd/MM/yyyy HH:mm}";
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!Validador.CampoObrigatorio(txtNome, "O nome do estado é obrigatório.")) return;

            if (paisSelecionadoId <= 0)
            {
                MessageBox.Show("Selecione um país!");
                return;
            }

            try
            {
                int id = string.IsNullOrWhiteSpace(txtCodigo.Text) ? 0 : Convert.ToInt32(txtCodigo.Text);
                string nome = txtNome.Text;

                string uf = txtUF.Text.Trim().ToUpper();

                if (string.IsNullOrWhiteSpace(uf) || uf.Length != 2)
                {
                    MessageBox.Show("Informe a sigla UF com 2 letras.");
                    return;
                }

                int idPais = paisSelecionadoId;
                bool status = !chkInativo.Checked;

                DateTime dataCriacao = id == 0
                    ? DateTime.Now
                    : DateTime.Parse(lblDataCriacao.Text.Replace("Criado em: ", ""));

                DateTime dataModificacao = DateTime.Now;

                Estado estado = new Estado
                {
                    Id = id,
                    NomeEstado = nome,
                    UF = uf,
                    PaisId = idPais,
                    Ativo = status,
                    DataCadastro = dataCriacao,
                    DataAlteracao = dataModificacao
                };

                controller.Salvar(estado);
                MessageBox.Show("Estado salvo com sucesso!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar estado: " + ex.Message);
            }
        }




        private void btnBuscar_Click(object sender, EventArgs e)
        {
            frmConsultaPais consultaPais = new frmConsultaPais();
            consultaPais.ModoSelecao = true;

            var resultado = consultaPais.ShowDialog();

            if (resultado == DialogResult.OK && consultaPais.PaisSelecionado != null)
            {
                txtPais.Text = consultaPais.PaisSelecionado.NomePais;
                paisSelecionadoId = consultaPais.PaisSelecionado.Id;
            }
        }
    }
}
