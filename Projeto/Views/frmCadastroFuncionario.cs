﻿using Projeto.Controller;
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

        public frmCadastroFuncionario()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
            cbTipo.SelectedIndex = 0;
            dtpDemissao.Checked = false;
        }

        public void CarregarFuncionario(int id, string nome, string cpf_cnpj, string telefone, string email, string endereco, int numEndereco, string bairro, string complemento, string cep, string cargo, decimal salario, string tipo, string nomeCidade, bool status, DateTime? dataAdmissao, DateTime? dataDemissao)
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
            txtCargo.Text = cargo;
            txtSalario.Text = salario.ToString();
            cbTipo.Text = tipo;
            cbCidade.Text = nomeCidade;

            chkInativo.Checked = !status;
            if (dataAdmissao.HasValue)
                dtpAdmissao.Value = dataAdmissao.Value;

            if (dataDemissao.HasValue)
            {
                dtpDemissao.Value = dataDemissao.Value;
                dtpDemissao.Checked = true;
            }
            else
            {
                dtpDemissao.Checked = false;
            }

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
                NumeroEndereco = string.IsNullOrWhiteSpace(txtNumEnd.Text) ? 0 : Convert.ToInt32(txtNumEnd.Text),
                Bairro = txtBairro.Text,
                Complemento = txtComplemento.Text,
                Telefone = txtTelefone.Text,
                Tipo = cbTipo.Text,
                CEP = txtCEP.Text,
                Cargo = txtCargo.Text,
                Salario = Convert.ToDecimal(txtSalario.Text),
                IdCidade = Convert.ToInt32(cbCidade.SelectedValue),
                Status = !chkInativo.Checked,
                DataAdmissao = dtpAdmissao.Value,
                DataDemissao = dtpDemissao.Checked ? (DateTime?)dtpDemissao.Value : null,
            };

            try
            {
                controller.Salvar(funcionario);
                MessageBox.Show("funcionario salvo com sucesso!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar funcionário: " + ex.Message);
            }
        }

        private void frmCadastroFuncionario_Load(object sender, EventArgs e)
        {
            CarregarCidades();

            if (modoEdicao == true)
            {
                cbTipo.Enabled = false;
            }
        }
    }
}
