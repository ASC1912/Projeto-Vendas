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

        public frmCadastroFornecedor()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
            cbTipo.SelectedIndex = 0;
        }

        public void CarregarFornecedor(int id, string nome, string cpf_cnpj, string telefone, string email, string endereco, int numEndereco, string bairro, string complemento, string cep, string inscEst, string inscEstSubTrib, string tipo, string nomeCidade, int idCondicao, bool status)
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

            Fornecedor fornecedor = new Fornecedor
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
                InscricaoEstadual = txtInscEst.Text,
                InscricaoEstadualSubTrib = txtInscEstSubTrib.Text,
                IdCidade = Convert.ToInt32(cbCidade.SelectedValue),
                IdCondicao = Convert.ToInt32(cbCondPgto.SelectedValue),
                Status = !chkInativo.Checked,
            };

            try
            {
                controller.Salvar(fornecedor);
                MessageBox.Show("fornecedor salvo com sucesso!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar fornecedor: " + ex.Message);
            }
        }

        private void frmCadastroFornecedor_Load(object sender, EventArgs e)
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
