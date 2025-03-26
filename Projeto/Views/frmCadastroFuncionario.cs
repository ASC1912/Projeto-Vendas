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
        public frmCadastroFuncionario()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
        }

        public void CarregarFuncionario(int id, string nome, string cpf_cnpj, string telefone, string email, string endereco, string cep, string cargo, decimal salario, string tipo, string nomeCidade)
        {
            txtCodigo.Text = id.ToString();
            txtNome.Text = nome;
            txtCPF.Text = cpf_cnpj;
            txtTelefone.Text = telefone;
            txtEmail.Text = email;
            txtEndereco.Text = endereco;
            txtCEP.Text = cep;
            txtCargo.Text = cargo;
            txtSalario.Text = salario.ToString();
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

            Funcionario funcionario = new Funcionario
            {
                Id = string.IsNullOrWhiteSpace(txtCodigo.Text) ? 0 : Convert.ToInt32(txtCodigo.Text),
                Nome = txtNome.Text,
                CPF_CNPJ = txtCPF.Text,
                Email = txtEmail.Text,
                Endereco = txtEndereco.Text,
                Telefone = txtTelefone.Text,
                Tipo = txtTipo.Text,
                CEP = txtCEP.Text,
                Cargo = txtCargo.Text,
                Salario = Convert.ToDecimal(txtSalario.Text),
                IdCidade = Convert.ToInt32(cbCidade.SelectedValue)
            };

            try
            {
                controller.Salvar(funcionario);
                MessageBox.Show("funcionario salvo com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar funcionário: " + ex.Message);
            }
        }

        private void frmCadastroFuncionario_Load(object sender, EventArgs e)
        {
            CarregarCidades();
        }
    }
}
