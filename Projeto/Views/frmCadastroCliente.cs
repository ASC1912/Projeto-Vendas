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
    public partial class frmCadastroCliente : Projeto.frmBase
    {
        private CidadeController cidadeController = new CidadeController();
        private CondicaoPagamentoController condicaoPagamentoController = new CondicaoPagamentoController();
        private ClienteController controller = new ClienteController();
        public bool modoEdicao = false;
        public frmCadastroCliente()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
            cbTipo.SelectedIndex = 0;
        }

        public void CarregarCliente(int id, string nome, string cpf_cnpj, string telefone, string email, string endereco, int numeroEndereco, string bairro, string complemento,  string cep, string tipo, string nomeCidade, int idCondicao, bool status)
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
            cbCidade.Text = nomeCidade;
            cbCondPgto.Text = idCondicao.ToString();
            chkInativo.Checked = !status;
        }

        private void CarregarCidades()
        {
            try
            {
                List<Cidade> listaCidades = cidadeController.ListarCidade();

                cbCidade.DisplayMember = "Nome";
                cbCidade.ValueMember = "Id";
                cbCidade.DataSource = listaCidades;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar Cidades: " + ex.Message);
            }
        }

        private void CarregarCondicoesPagamento()
        {
            try
            {
                List<CondicaoPagamento> listaCondicoes = condicaoPagamentoController.ListarCondicaoPagamento();

                cbCondPgto.DisplayMember = "Id";
                cbCondPgto.ValueMember = "Id";
                cbCondPgto.DataSource = listaCondicoes;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar condições de pagamento: " + ex.Message);
            }
        }


        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (cbCidade.SelectedValue == null)
            {
                MessageBox.Show("Selecione uma cidade!");
                return;
            }

            Cliente cliente = new Cliente
            {
                Id = string.IsNullOrWhiteSpace(txtCodigo.Text) ? 0 : Convert.ToInt32(txtCodigo.Text),
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
                IdCidade = Convert.ToInt32(cbCidade.SelectedValue),
                IdCondicao = Convert.ToInt32(cbCondPgto.SelectedValue),
                Status = !chkInativo.Checked,

            };

            try
            {
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
            CarregarCidades();
            CarregarCondicoesPagamento();

            if (modoEdicao == true)
            {
                cbTipo.Enabled = false;
            }
        }
    }
}
