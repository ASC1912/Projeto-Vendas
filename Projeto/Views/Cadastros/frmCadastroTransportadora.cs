using Projeto.Controller;
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

namespace Projeto.Views.Cadastros
{
    public partial class frmCadastroTransportadora : Projeto.frmBase
    {
        private CidadeController cidadeController = new CidadeController();
        private CondicaoPagamentoController condicaoPagamentoController = new CondicaoPagamentoController();
        private TransportadoraController controller = new TransportadoraController();
        public bool modoEdicao = false;
        private int cidadeSelecionadoId = -1;
        private int condicaoSelecionadoId = -1;
        public frmCadastroTransportadora() : base()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
            cbTipo.SelectedIndex = 0;
        }

        public void CarregarTransportadora(int id, string nome, string cpf_cnpj, string telefone, string email, string endereco,
                                int numEndereco, string bairro, string complemento, string cep, string inscEst,
                                string inscEstSubTrib, string tipo, string nomeCidade, int idCidade,  string descricaoCondicao, int idCondicao,
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
            txtIdCidade.Text = idCidade > 0 ? idCidade.ToString() : "";
            cidadeSelecionadoId = idCidade;
            txtCondicao.Text = descricaoCondicao;
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
            if (!Validador.CampoObrigatorio(txtNome, "O nome é obrigatório.")) return;
            if (!Validador.CampoObrigatorio(txtCPF, "O CPF/CNPJ é obrigatório.")) return;
            if (!Validador.CampoObrigatorio(txtEndereco, "O Endereço é obrigatório.")) return;
            if (!Validador.CampoObrigatorio(txtNumEnd, "O Número de endereço é obrigatório.")) return;
            if (!Validador.ValidarNumerico(txtNumEnd, "O número do endereço deve ser numérico.")) return;
            if (!Validador.CampoObrigatorio(txtBairro, "O Bairro é obrigatório.")) return;
            if (!Validador.ValidarEmail(txtEmail)) return;

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

                if (string.IsNullOrWhiteSpace(txtInscEst.Text))
                {
                    MessageBox.Show("Inscrição estadual é obrigatória para pessoa jurídica.");
                    txtInscEst.Focus();
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

                string cpfCnpjLimpo = new string(txtCPF.Text.Where(char.IsDigit).ToArray());

                List<Transportadora> transportadoras = controller.ListarTransportadora();
                bool existeDuplicado = transportadoras.Exists(t =>
                    new string(t.CPF_CNPJ.Where(char.IsDigit).ToArray()).Equals(cpfCnpjLimpo, StringComparison.OrdinalIgnoreCase)
                    && t.Id != id);

                if (existeDuplicado)
                {
                    MessageBox.Show("Já existe uma transportadora cadastrada com este CPF/CNPJ.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCPF.Focus();
                    return;
                }

                Transportadora transportadora = new Transportadora
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
                    Ativo = !chkInativo.Checked,
                    DataCadastro = dataCriacao,
                    DataAlteracao = dataModificacao
                };

                controller.Salvar(transportadora);
                MessageBox.Show("Transportadora salva com sucesso!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar transportadora: " + ex.Message);
            }
        }

        private void frmCadastroTransportadora_Load(object sender, EventArgs e)
        {
            if (modoEdicao)
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
    }
}
