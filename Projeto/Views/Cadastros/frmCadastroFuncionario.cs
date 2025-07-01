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

namespace Projeto.Views
{
    public partial class frmCadastroFuncionario : Projeto.frmBase
    {
        private CidadeController cidadeController = new CidadeController();
        private EstadoController estadoController = new EstadoController();
        private PaisController paisController = new PaisController();
        private FuncionarioController controller = new FuncionarioController();
        public bool modoEdicao = false;
        private int cidadeSelecionadoId = -1;


        public frmCadastroFuncionario() : base()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
            cbTipo.SelectedIndex = 0;
            cbGenero.SelectedIndex = 0;
            dtpDemissao.Checked = false;
            cbGenero.SelectedIndex = 0;
        }

        public void CarregarFuncionario(
                     int id, string nome, string apelido, string cpf_cnpj, string telefone, string email,
                     string endereco, int numEndereco, string bairro, string complemento, string cep,
                     string cargo, decimal salario, string matricula, string genero, string tipo,
                     string nomeCidade, int idCidade, bool status, DateTime? dataAdmissao,
                     DateTime? dataDemissao, string rg, DateTime? dataCriacao, DateTime? dataModificacao)
        {
            txtCodigo.Text = id.ToString();
            txtNome.Text = nome;
            txtApelido.Text = apelido;
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
            txtIdCidade.Text = idCidade > 0 ? idCidade.ToString() : "";
            cidadeSelecionadoId = idCidade;
            txtCargo.Text = cargo;
            txtSalario.Text = salario.ToString();
            txtMatricula.Text = matricula;
            txtRG.Text = rg;
            chkInativo.Checked = !status;

            if (!string.IsNullOrWhiteSpace(genero))
            {
                cbGenero.SelectedItem = genero.Equals("M", StringComparison.OrdinalIgnoreCase) ? "Masculino" : "Feminino";
            }
            else
            {
                cbGenero.SelectedIndex = -1;
            }

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
            if (!Validador.CampoObrigatorio(txtNome, "O nome é obrigatório.")) return;
            if (!Validador.CampoObrigatorio(txtCPF, "O CPF/CNPJ é obrigatório.")) return;
            if (!Validador.CampoObrigatorio(txtEndereco, "O Endereço é obrigatório.")) return;
            if (!Validador.CampoObrigatorio(txtNumEnd, "O Número de endereço é obrigatório.")) return;
            if (!Validador.CampoObrigatorio(txtMatricula, "A matrícula é obrigatória.")) return;
            if (!Validador.CampoObrigatorio(txtSalario, "O salário é obrigatório.")) return;
            if (!Validador.ValidarNumerico(txtNumEnd, "O número do endereço deve ser numérico.")) return;
            if (!Validador.CampoObrigatorio(txtBairro, "O Bairro é obrigatório.")) return;
            if (!Validador.ValidarEmail(txtEmail)) return;
            if (!Validador.CampoObrigatorio(txtCargo, "O cargo é obrigatório.")) return;

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
                                MessageBox.Show("O campo RG é obrigatório para funcionarios brasileiros.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtRG.Focus();
                                return;
                            }
                        }
                    }
                }
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
                    Ativo = status,
                    DataAdmissao = dtpAdmissao.Value,
                    DataDemissao = dtpDemissao.Checked ? (DateTime?)dtpDemissao.Value : null,
                    Rg = txtRG.Text,
                    Genero = genero,
                    Apelido = txtApelido.Text,
                    Matricula = txtMatricula.Text,
                    DataCadastro = dataCriacao,
                    DataAlteracao = dataModificacao
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
