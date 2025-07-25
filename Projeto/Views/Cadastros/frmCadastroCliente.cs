﻿using Projeto.Controller;
using Projeto.Models;
using Projeto.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Projeto.Views
{
    public partial class frmCadastroCliente : Projeto.frmBase
    {
        private CidadeController cidadeController = new CidadeController();
        private EstadoController estadoController = new EstadoController();
        private PaisController paisController = new PaisController();
        private CondicaoPagamentoController condicaoPagamentoController = new CondicaoPagamentoController();
        private ClienteController controller = new ClienteController();
        public bool modoEdicao = false;
        public bool modoExclusao = false;
        private int cidadeSelecionadoId = -1;
        private int condicaoSelecionadoId = -1;


        public frmCadastroCliente() : base()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
            cbTipo.SelectedIndex = 0;
            cbGenero.SelectedIndex = 0;
            dtpNascimento.MaxDate = DateTime.Now;
            cbTipo_SelectedIndexChanged(null, null);
        }

        public void CarregarCliente(int id, string nome, string cpfCnpj, string telefone, string email, string endereco,
                 int numeroEndereco, string bairro, string complemento, string cep, string tipo, string genero,
                 string nomeCidade, int idCidade, string descricaoCondicao, int idCondicao, bool ativo, string rg,
                 DateTime? dataNascimento, DateTime? dataCadastro, DateTime? dataAlteracao)
        {
            txtCodigo.Text = id.ToString();
            txtNome.Text = nome;
            txtCPF.Text = cpfCnpj;
            txtTelefone.Text = telefone;
            txtEmail.Text = email;
            txtEndereco.Text = endereco;
            txtNumEnd.Text = numeroEndereco.ToString();
            txtBairro.Text = bairro;
            txtComplemento.Text = complemento;
            txtCEP.Text = cep;
            cbTipo.Text = tipo;
            txtCidade.Text = nomeCidade;
            txtIdCidade.Text = idCidade > 0 ? idCidade.ToString() : "";
            cidadeSelecionadoId = idCidade;
            txtCondicao.Text = descricaoCondicao;
            condicaoSelecionadoId = idCondicao;
            chkInativo.Checked = !ativo;
            txtRG.Text = rg;
            if (dataNascimento.HasValue)
                dtpNascimento.Value = dataNascimento.Value;

            if (!string.IsNullOrWhiteSpace(genero))
            {
                cbGenero.SelectedItem = genero.Equals("M", StringComparison.OrdinalIgnoreCase) ? "Masculino" : "Feminino";
            }
            else
            {
                cbGenero.SelectedIndex = -1;
            }

            lblDataCriacao.Text = dataCadastro.HasValue
                ? $"Criado em: {dataCadastro.Value:dd/MM/yyyy HH:mm}"
                : "Criado em: -";

            lblDataModificacao.Text = dataAlteracao.HasValue
                ? $"Modificado em: {dataAlteracao.Value:dd/MM/yyyy HH:mm}"
                : "Modificado em: -";
        }


        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (modoExclusao)
            {
                var confirmacao = MessageBox.Show("Tem certeza que deseja excluir este Cliente?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmacao == DialogResult.Yes)
                {
                    try
                    {
                        int id = int.Parse(txtCodigo.Text);
                        controller.Excluir(id);
                        MessageBox.Show("Cliente excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao excluir: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                if (!Validador.CampoObrigatorio(txtNome, "O nome é obrigatório.")) return;
                if (!Validador.CampoObrigatorio(txtCPF, "O CPF/CNPJ é obrigatório.")) return;
                if (!Validador.CampoObrigatorio(txtEndereco, "O Endereço é obrigatório.")) return;
                if (!Validador.CampoObrigatorio(txtNumEnd, "O Número de endereço é obrigatório.")) return;
                if (!Validador.CampoObrigatorio(txtBairro, "O Bairro é obrigatório.")) return;
                if (!Validador.ValidarEmail(txtEmail)) return;
                if (!Validador.ValidarNumerico(txtNumEnd, "O número do endereço deve ser numérico.")) return;

                string tipoPessoa = cbTipo.Text.Trim();
                string documento = new string(txtCPF.Text.Where(char.IsDigit).ToArray());

                if (tipoPessoa == "Físico")
                {
                    if (!Validador.ValidarCPF(documento))
                    {
                        MessageBox.Show("CPF inválido.");
                        txtCPF.Focus();
                        return;
                    }
                }
                else if (tipoPessoa == "Jurídico")
                {
                    if (!Validador.ValidarCNPJ(documento))
                    {
                        MessageBox.Show("CNPJ inválido.");
                        txtCPF.Focus();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Tipo de pessoa inválido.");
                    cbTipo.Focus();
                    return;
                }

                if (cidadeSelecionadoId <= 0)
                {
                    MessageBox.Show("Selecione uma cidade.");
                    return;
                }
                /*
                if (cidadeSelecionadoId > 0)
                {
                    var cidade = cidadeController.BuscarPorId(cidadeSelecionadoId);
                    if (cidade != null)
                    {
                        var estado = estadoController.BuscarPorId(cidade.EstadoId);
                        if (estado != null)
                        {
                            var pais = paisController.BuscarPorId(estado.PaisId);
                            if (pais != null && pais.NomePais.Trim().Equals("Brasil", StringComparison.OrdinalIgnoreCase))
                            {
                                if (string.IsNullOrWhiteSpace(txtRG.Text))
                                {
                                    MessageBox.Show("O campo RG é obrigatório para clientes brasileiros.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    txtRG.Focus();
                                    return;
                                }
                            }
                        }
                    }
                }
                */

                if (condicaoSelecionadoId <= 0)
                {
                    MessageBox.Show("Selecione uma condição de pagamento.");
                    return;
                }

                string genero = "";
                if (cbGenero.SelectedItem != null)
                {
                    string generoSelecionado = cbGenero.SelectedItem.ToString();
                    genero = generoSelecionado.StartsWith("M", StringComparison.OrdinalIgnoreCase) ? "M" : "F";
                }
                else
                {
                    MessageBox.Show("Selecione o gênero.");
                    return;
                }

                try
                {
                    int id = string.IsNullOrWhiteSpace(txtCodigo.Text) ? 0 : Convert.ToInt32(txtCodigo.Text);
                    DateTime dataCriacao = id == 0
                        ? DateTime.Now
                        : DateTime.Parse(lblDataCriacao.Text.Replace("Criado em: ", ""));

                    DateTime dataModificacao = DateTime.Now;

                    string cpfCnpjLimpo = new string(txtCPF.Text.Where(char.IsDigit).ToArray());

                    List<Cliente> clientes = controller.ListarCliente();
                    bool existeDuplicado = clientes.Exists(c =>
                        new string(c.CPF_CNPJ.Where(char.IsDigit).ToArray()).Equals(cpfCnpjLimpo, StringComparison.OrdinalIgnoreCase)
                        && c.Id != id);

                    if (existeDuplicado)
                    {
                        MessageBox.Show("Já existe um cliente cadastrado com este CPF/CNPJ.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCPF.Focus();
                        return;
                    }

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
                        Genero = genero,
                        CEP = txtCEP.Text,
                        CidadeId = cidadeSelecionadoId,
                        IdCondicao = condicaoSelecionadoId,
                        Ativo = !chkInativo.Checked,
                        Rg = txtRG.Text,
                        DataNascimento = dtpNascimento.Value,
                        DataCadastro = dataCriacao,
                        DataAlteracao = dataModificacao
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
        }



        private void frmCadastroCliente_Load(object sender, EventArgs e)
        {
            cbTipo.SelectedIndexChanged += cbTipo_SelectedIndexChanged;

            if (modoExclusao)
            {
                btnSalvar.Text = "Deletar";
                cbTipo.Enabled = false;
                txtNome.Enabled = false;
                cbGenero.Enabled = false;
                txtEndereco.Enabled = false;
                txtNumEnd.Enabled = false;
                txtBairro.Enabled = false;
                txtComplemento.Enabled = false;
                txtCEP.Enabled = false;
                txtIdCidade.Enabled = false;
                btnBuscar.Enabled = false;
                txtEmail.Enabled = false;
                txtTelefone.Enabled = false;
                txtCPF.Enabled = false;
                txtRG.Enabled = false;
                btnBuscarCond.Enabled = false;
                chkInativo.Enabled = false;
                dtpNascimento.Enabled = false;
            }
            else if (modoEdicao)
            {
                cbTipo.Enabled = false;
            }
            else
            {
                txtCodigo.Text = "0";

                DateTime agora = DateTime.Now;

                lblDataCriacao.Text = $"Criado em: {agora:dd/MM/yyyy HH:mm}";
                lblDataModificacao.Text = $"Modificado em: {agora:dd/MM/yyyy HH:mm}";
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            frmConsultaCidade consultaCidade = new frmConsultaCidade();
            consultaCidade.ModoSelecao = true;

            var resultado = consultaCidade.ShowDialog();

            if (resultado == DialogResult.OK && consultaCidade.CidadeSelecionado != null)
            {
                txtCidade.Text = consultaCidade.CidadeSelecionado.NomeCidade;
                cidadeSelecionadoId = consultaCidade.CidadeSelecionado.Id;
                txtIdCidade.Text = consultaCidade.CidadeSelecionado.Id.ToString();
            }
        }

        private void btnBuscarCond_Click(object sender, EventArgs e)
        {
            frmConsultaCondPgto consultaCondicao = new frmConsultaCondPgto();
            consultaCondicao.ModoSelecao = true;

            var resultado = consultaCondicao.ShowDialog();

            if (resultado == DialogResult.OK && consultaCondicao.CondicaoSelecionado != null)
            {
                txtCondicao.Text = consultaCondicao.CondicaoSelecionado.Descricao;
                condicaoSelecionadoId = consultaCondicao.CondicaoSelecionado.Id;
            }
        }

        private void txtIdCidade_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdCidade.Text))
            {
                txtCidade.Text = "";
                cidadeSelecionadoId = -1;
                return;
            }

            if (!Validador.ValidarNumerico(txtIdCidade, "O campo ID Cidade deve conter apenas números."))
            {
                txtCidade.Text = "";
                cidadeSelecionadoId = -1;
                return;
            }

            int id = int.Parse(txtIdCidade.Text);
            var cidade = cidadeController.BuscarPorId(id);

            if (cidade != null)
            {
                txtCidade.Text = cidade.NomeCidade;
                cidadeSelecionadoId = cidade.Id;
            }
            else
            {
                MessageBox.Show("Cidade não encontrada.");
                txtCidade.Text = "";
                cidadeSelecionadoId = -1;
            }
        }


        private void cbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTipo.Text == "Jurídico")
            {
                lblCPF.Text = "CNPJ";
            }
            else
            {
                lblCPF.Text = "CPF";
            }
        }

    }
}