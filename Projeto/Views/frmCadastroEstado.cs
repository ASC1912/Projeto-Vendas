using Projeto.Controller;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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

        public void CarregarEstado(int id, string nome, string nomePais, bool status, DateTime? dataCriacao, DateTime? dataModificacao)
        {
           modoEdicao = true;

           txtCodigo.Text = id.ToString();
           txtNome.Text = nome;
           txtPais.Text = nomePais;
           chkInativo.Checked = !status;


            lblDataCriacao.Text = dataCriacao.HasValue
                ? $"Criado em: {dataCriacao.Value:dd/MM/yyyy HH:mm}"
                : "Criado em: -";

            lblDataModificacao.Text = dataModificacao.HasValue
                ? $"Modificado em: {dataModificacao.Value:dd/MM/yyyy HH:mm}"
                : "Modificado em: -";
        }

        private void frmCadastroEstado_Load(object sender, EventArgs e)
        {
            lblDataCriacao.Visible = modoEdicao;
            lblDataModificacao.Visible = modoEdicao;

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (paisSelecionadoId <= 0)
            {
                MessageBox.Show("Selecione um país!");
                return;
            }

            try
            {
                int id = string.IsNullOrWhiteSpace(txtCodigo.Text) ? 0 : Convert.ToInt32(txtCodigo.Text);
                string nome = txtNome.Text;
                int idPais = paisSelecionadoId;
                bool status = !chkInativo.Checked;

                DateTime dataCriacao = id == 0
                    ? DateTime.Now
                    : DateTime.Parse(lblDataCriacao.Text.Replace("Criado em: ", ""));

                DateTime dataModificacao = DateTime.Now;

                Estado estado = new Estado
                {
                    Id = id,
                    Nome = nome,
                    IdPais = idPais,
                    Status = status,
                    DataCriacao = dataCriacao,
                    DataModificacao = dataModificacao
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
                txtPais.Text = consultaPais.PaisSelecionado.Nome;
                paisSelecionadoId = consultaPais.PaisSelecionado.Id;
            }
        }
    }
}
