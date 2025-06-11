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
    public partial class frmCadastroCidade : Projeto.frmBase
    {
        private EstadoController estadoController = new EstadoController();
        private CidadeController controller = new CidadeController();
        private int estadoSelecionadoId = -1;
        public bool modoEdicao = false;

        public frmCadastroCidade() : base()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
        }

        public void CarregarCidade(int id, string nome, string nomeEstado, bool status, DateTime? dataCriacao, DateTime? dataModificacao)
        {
            modoEdicao = true;

            txtCodigo.Text = id.ToString();
            txtNome.Text = nome;
            txtEstado.Text = nomeEstado;
            chkInativo.Checked = !status;

            lblDataCriacao.Text = dataCriacao.HasValue
                ? $"Criado em: {dataCriacao.Value:dd/MM/yyyy HH:mm}"
                : "Criado em: -";

            lblDataModificacao.Text = dataModificacao.HasValue
                ? $"Modificado em: {dataModificacao.Value:dd/MM/yyyy HH:mm}"
                : "Modificado em: -";
        }


        private void frmCadastroCidade_Load(object sender, EventArgs e)
        {
            lblDataCriacao.Visible = modoEdicao;
            lblDataModificacao.Visible = modoEdicao;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!Validador.CampoObrigatorio(txtNome, "O nome da cidade é obrigatório.")) return;

            if (estadoSelecionadoId <= 0)
            {
                MessageBox.Show("Selecione um estado!");
                return;
            }

            try
            {
                int id = string.IsNullOrWhiteSpace(txtCodigo.Text) ? 0 : Convert.ToInt32(txtCodigo.Text);
                string nome = txtNome.Text;
                int idEstado = estadoSelecionadoId;
                bool status = !chkInativo.Checked;

                DateTime dataCriacao = id == 0
                    ? DateTime.Now
                    : DateTime.Parse(lblDataCriacao.Text.Replace("Criado em: ", ""));

                DateTime dataModificacao = DateTime.Now;

                Cidade cidade = new Cidade
                {
                    Id = id,
                    Nome = nome,
                    IdEstado = idEstado,
                    Status = status,
                    DataCriacao = dataCriacao,
                    DataModificacao = dataModificacao
                };

                controller.Salvar(cidade);
                MessageBox.Show("Cidade salva com sucesso!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar cidade: " + ex.Message);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            frmConsultaEstado consultaEstado = new frmConsultaEstado();
            consultaEstado.ModoSelecao = true;

            var resultado = consultaEstado.ShowDialog();

            if (resultado == DialogResult.OK && consultaEstado.EstadoSelecionado != null)
            {
                txtEstado.Text = consultaEstado.EstadoSelecionado.Nome;
                estadoSelecionadoId = consultaEstado.EstadoSelecionado.Id;
            }
        }

    }
}
