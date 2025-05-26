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
    public partial class frmCadastroFuncionario : Projeto.frmBase
    {
        private CidadeController cidadeController = new CidadeController();
        private FuncionarioController controller = new FuncionarioController();
        public bool modoEdicao = false;
        private int cidadeSelecionadoId = -1;


        public frmCadastroFuncionario()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
            cbTipo.SelectedIndex = 0;
            dtpDemissao.Checked = false;
        }

        public void CarregarFuncionario(int id, string nome, string cpf_cnpj, string telefone, string email, string endereco, int numEndereco, string bairro, string complemento, string cep, string cargo, decimal salario, string tipo, string nomeCidade, int idCidade, bool status, DateTime? dataAdmissao, DateTime? dataDemissao, string rg, DateTime? dataCriacao, DateTime? dataModificacao)
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
            cbTipo.Text = tipo;
            txtCidade.Text = nomeCidade;
            cidadeSelecionadoId = idCidade;
            txtCargo.Text = cargo;
            txtSalario.Text = salario.ToString();
            txtRG.Text = rg;
            chkInativo.Checked = !status;

            if (dataAdmissao.HasValue)
                dtpAdmissao.Value = dataAdmissao.Value;

            if (dataDemissao.HasValue)
            {
                dtpDemissao.Value = dataDemissao.Value;
                dtpDemissao.Checked = true;
            }

            lblDataCriacao.Text = dataCriacao.HasValue
                ? $"Criado em: {dataCriacao:dd/MM/yyyy HH:mm}"
                : "Criado em: -";

            lblDataModificacao.Text = dataModificacao.HasValue
                ? $"Modificado em: {dataModificacao:dd/MM/yyyy HH:mm}"
                : "Modificado em: -";
        }



        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = string.IsNullOrWhiteSpace(txtCodigo.Text) ? 0 : Convert.ToInt32(txtCodigo.Text);
                string nome = txtNome.Text;
                bool status = !chkInativo.Checked;

                DateTime dataCriacao = id == 0
                    ? DateTime.Now
                    : DateTime.Parse(lblDataCriacao.Text.Replace("Criado em: ", ""));

                DateTime dataModificacao = DateTime.Now;

                Funcionario funcionario = new Funcionario
                {
                    Id = id,
                    Nome = nome,
                    CPF_CNPJ = txtCPF.Text,
                    Email = txtEmail.Text,
                    Endereco = txtEndereco.Text,
                    NumeroEndereco = string.IsNullOrWhiteSpace(txtNumEnd.Text) ? 0 : Convert.ToInt32(txtNumEnd.Text),
                    Bairro = txtBairro.Text,
                    Complemento = txtComplemento.Text,
                    Telefone = txtTelefone.Text,
                    Tipo = cbTipo.Text,
                    CEP = txtCEP.Text,
                    Cargo = txtCargo.Text,
                    Salario = Convert.ToDecimal(txtSalario.Text),
                    IdCidade = cidadeSelecionadoId,
                    Status = status,
                    DataAdmissao = dtpAdmissao.Value,
                    DataDemissao = dtpDemissao.Checked ? (DateTime?)dtpDemissao.Value : null,
                    Rg = txtRG.Text,
                    DataCriacao = dataCriacao,
                    DataModificacao = dataModificacao
                };

                controller.Salvar(funcionario);
                MessageBox.Show("Funcionário salvo com sucesso!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar funcionário: " + ex.Message);
            }
        }

        private void frmCadastroFuncionario_Load(object sender, EventArgs e)
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
    }
}
