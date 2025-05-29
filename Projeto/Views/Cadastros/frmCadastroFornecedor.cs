using Projeto.Controller;
using Projeto.DAO;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Projeto.Views
{
    public partial class frmCadastroFornecedor : Projeto.frmBase
    {
        private CidadeController cidadeController = new CidadeController();
        private CondicaoPagamentoController condicaoPagamentoController = new CondicaoPagamentoController();
        private FornecedorController controller = new FornecedorController();
        public bool modoEdicao = false;
        private int cidadeSelecionadoId = -1;
        private int condicaoSelecionadoId = -1;



        public frmCadastroFornecedor() : base()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
            cbTipo.SelectedIndex = 0;
        }

        public void CarregarFornecedor(int id, string nome, string cpf_cnpj, string telefone, string email, string endereco,
                                int numEndereco, string bairro, string complemento, string cep, string inscEst,
                                string inscEstSubTrib, string tipo, string nomeCidade, int idCidade, int idCondicao,
                                bool status, DateTime? dataCriacao, DateTime? dataModificacao)
        {
            txtCodigo.Text = id.ToString();
            txtNome.Text = nome;
            txtCPF.Text = cpf_cnpj;
            txtTelefone.Text = telefone;
            txtEmail.Text = email;
            txtEndereco.Text = endereco;
            txtNumEnd.Text = numEndereco.ToString();
            txtBairro.Text = bairro;
            txtComplemento.Text = complemento;
            txtCEP.Text = cep;
            txtInscEst.Text = inscEst;
            txtInscEstSubTrib.Text = inscEstSubTrib;
            cbTipo.Text = tipo;
            txtCidade.Text = nomeCidade;
            cidadeSelecionadoId = idCidade;
            txtCondicao.Text = idCondicao.ToString();
            condicaoSelecionadoId = idCondicao;
            chkInativo.Checked = !status;

            lblDataCriacao.Text = dataCriacao.HasValue
                ? $"Criado em: {dataCriacao:dd/MM/yyyy HH:mm}"
                : "Criado em: -";

            lblDataModificacao.Text = dataModificacao.HasValue
                ? $"Modificado em: {dataModificacao:dd/MM/yyyy HH:mm}"
                : "Modificado em: -";
        }




        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (cidadeSelecionadoId <= 0)
            {
                MessageBox.Show("Selecione uma cidade antes de salvar!");
                return;
            }

            try
            {
                int id = string.IsNullOrWhiteSpace(txtCodigo.Text) ? 0 : Convert.ToInt32(txtCodigo.Text);
                DateTime dataCriacao = id == 0
                    ? DateTime.Now
                    : DateTime.Parse(lblDataCriacao.Text.Replace("Criado em: ", ""));

                DateTime dataModificacao = DateTime.Now;

                Fornecedor fornecedor = new Fornecedor
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
                    InscricaoEstadual = txtInscEst.Text,
                    InscricaoEstadualSubTrib = txtInscEstSubTrib.Text,
                    IdCidade = cidadeSelecionadoId,
                    IdCondicao = condicaoSelecionadoId > 0 ? (int?)condicaoSelecionadoId : null,
                    Status = !chkInativo.Checked,
                    DataCriacao = dataCriacao,
                    DataModificacao = dataModificacao
                };

                controller.Salvar(fornecedor);
                MessageBox.Show("Fornecedor salvo com sucesso!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar fornecedor: " + ex.Message);
            }
        }




        private void frmCadastroFornecedor_Load(object sender, EventArgs e)
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
