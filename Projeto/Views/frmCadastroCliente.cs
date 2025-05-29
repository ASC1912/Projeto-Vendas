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

namespace Projeto.Views
{
    public partial class frmCadastroCliente : Projeto.frmBase
    {
        private CidadeController cidadeController = new CidadeController();
        private CondicaoPagamentoController condicaoPagamentoController = new CondicaoPagamentoController();
        private ClienteController controller = new ClienteController();
        public bool modoEdicao = false;
        private int cidadeSelecionadoId = -1;
        private int condicaoSelecionadoId = -1;


        public frmCadastroCliente() : base()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
            cbTipo.SelectedIndex = 0;
        }

        public void CarregarCliente(int id, string nome, string cpf_cnpj, string telefone, string email, string endereco,
                     int numeroEndereco, string bairro, string complemento, string cep, string tipo,
                     string nomeCidade, int idCidade, int idCondicao, bool status, string rg,
                     DateTime? dataCriacao, DateTime? dataModificacao)
        {
            txtCodigo.Text = id.ToString();
            txtNome.Text = nome;
            txtCPF.Text = cpf_cnpj;
            txtTelefone.Text = telefone;
            txtEmail.Text = email;
            txtEndereco.Text = endereco;
            txtNumEnd.Text = numeroEndereco.ToString();
            txtBairro.Text = bairro;
            txtComplemento.Text = complemento;
            txtCEP.Text = cep;
            cbTipo.Text = tipo;
            txtCidade.Text = nomeCidade;
            cidadeSelecionadoId = idCidade;
            txtCondicao.Text = idCondicao.ToString();
            condicaoSelecionadoId = idCondicao;
            chkInativo.Checked = !status;
            txtRG.Text = rg;

            lblDataCriacao.Text = dataCriacao.HasValue
                ? $"Criado em: {dataCriacao:dd/MM/yyyy HH:mm}"
                : "Criado em: -";

            lblDataModificacao.Text = dataModificacao.HasValue
                ? $"Modificado em: {dataModificacao:dd/MM/yyyy HH:mm}"
                : "Modificado em: -";
        }




        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!Validador.CampoObrigatorio(txtNome, "O nome é obrigatório.")) return;
            if (!Validador.CampoObrigatorio(txtCPF, "O CPF/CNPJ é obrigatório.")) return;
            if (!Validador.ValidarEmail(txtEmail)) return;
            if (!Validador.ValidarNumerico(txtNumEnd, "O número do endereço deve ser numérico.")) return;

            if (!Validador.ValidarCPF(txtCPF.Text))
            {
                MessageBox.Show("CPF inválido.");
                txtCPF.Focus();
                return;
            }

            try
            {
                int id = string.IsNullOrWhiteSpace(txtCodigo.Text) ? 0 : Convert.ToInt32(txtCodigo.Text);
                DateTime dataCriacao = id == 0
                    ? DateTime.Now
                    : DateTime.Parse(lblDataCriacao.Text.Replace("Criado em: ", ""));

                DateTime dataModificacao = DateTime.Now;

                Cliente cliente = new Cliente
                {
                    Id = id,
                    Nome = txtNome.Text,
                    CPF_CNPJ = txtCPF.Text,
                    Email = txtEmail.Text,
                    Endereco = txtEndereco.Text,
                    NumeroEndereco = string.IsNullOrWhiteSpace(txtNumEnd.Text) ? 0 : Convert.ToInt32(txtNumEnd.Text),
                    Bairro = txtBairro.Text,
                    Complemento = txtComplemento.Text,
                    Telefone = txtTelefone.Text,
                    Tipo = cbTipo.Text,
                    CEP = txtCEP.Text,
                    IdCidade = cidadeSelecionadoId,
                    IdCondicao = condicaoSelecionadoId,
                    Status = !chkInativo.Checked,
                    Rg = txtRG.Text,
                    DataCriacao = dataCriacao,
                    DataModificacao = dataModificacao
                };

                controller.Salvar(cliente);
                MessageBox.Show("Cliente salvo com sucesso!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar cliente: " + ex.Message);
            }
        }



        private void frmCadastroCliente_Load(object sender, EventArgs e)
        {

            if (modoEdicao == true)
            {
                cbTipo.Enabled = false;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            frmConsultaCidade consultaCidade = new frmConsultaCidade();
            consultaCidade.ModoSelecao = true;

            var resultado = consultaCidade.ShowDialog();

            if (resultado == DialogResult.OK && consultaCidade.CidadeSelecionado != null)
            {
                txtCidade.Text = consultaCidade.CidadeSelecionado.Nome;
                cidadeSelecionadoId = consultaCidade.CidadeSelecionado.Id;
            }
        }

        private void btnBuscarCond_Click(object sender, EventArgs e)
        {
            frmConsultaCondPgto consultaCondicao = new frmConsultaCondPgto();
            consultaCondicao.ModoSelecao = true;

            var resultado = consultaCondicao.ShowDialog();

            if (resultado == DialogResult.OK && consultaCondicao.CondicaoSelecionado != null)
            {
                txtCondicao.Text = consultaCondicao.CondicaoSelecionado.Id.ToString();
                condicaoSelecionadoId = consultaCondicao.CondicaoSelecionado.Id;
            }
        }
    }
}
