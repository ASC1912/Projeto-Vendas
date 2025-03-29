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
    public partial class frmCadastroFornecedor : Projeto.frmBase
    {
        private CidadeController cidadeController = new CidadeController();
        private FornecedorController controller = new FornecedorController();
        public frmCadastroFornecedor()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
        }

        public void CarregarFornecedor(int id, string nome, string cpf_cnpj, string telefone, string email, string endereco, int numEndereco, string bairro, string complemento, string cep, string inscEst, string inscEstSubTrib, string tipo, string nomeCidade)
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
            txtTipo.Text = tipo;
            cbCidade.Text = nomeCidade;
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
                Tipo = txtTipo.Text,
                CEP = txtCEP.Text,
                InscricaoEstadual = txtInscEst.Text,
                InscricaoEstadualSubTrib = txtInscEstSubTrib.Text,
                IdCidade = Convert.ToInt32(cbCidade.SelectedValue)
            };

            try
            {
                controller.Salvar(fornecedor);
                MessageBox.Show("fornecedor salvo com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar fornecedor: " + ex.Message);
            }
        }

        private void frmCadastroFornecedor_Load(object sender, EventArgs e)
        {
            CarregarCidades();
        }
    }
}
