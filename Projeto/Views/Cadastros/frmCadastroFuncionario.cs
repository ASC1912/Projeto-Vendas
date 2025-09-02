using Projeto.Controller;
using Projeto.Models;
using Projeto.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto.Views
{
    public partial class frmCadastroFuncionario : Projeto.frmBase
    {
        frmConsultaCidade oFrmConsultaCidade;
        private CidadeController cidadeController = new CidadeController();
        private EstadoController estadoController = new EstadoController();
        private PaisController paisController = new PaisController();
        private FuncionarioController controller = new FuncionarioController();

        public bool modoEdicao = false;
        public bool modoExclusao = false;
        private int cidadeSelecionadoId = -1;

        public frmCadastroFuncionario() : base()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
            cbTipo.SelectedIndex = 0;
            cbGenero.SelectedIndex = 0;
            dtpDemissao.Checked = false;
            dtpAdmissao.MaxDate = DateTime.Now;
            dtpDemissao.MaxDate = DateTime.Now;
            dtpNascimento.MaxDate = DateTime.Now;
            cbTipo.SelectedIndexChanged += cbTipo_SelectedIndexChanged;
            cbTipo_SelectedIndexChanged(null, null);
        }

        public void setFrmConsultaCidade(object obj)
        {
            if (obj != null)
            {
                oFrmConsultaCidade = (frmConsultaCidade)obj;
            }
        }

        public override void BloquearTxt()
        {
            cbTipo.Enabled = false;
            txtNome.Enabled = false;
            txtApelido.Enabled = false;
            cbGenero.Enabled = false;
            dtpNascimento.Enabled = false;
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
            txtMatricula.Enabled = false;
            txtCargo.Enabled = false;
            txtSalario.Enabled = false;
            dtpAdmissao.Enabled = false;
            dtpDemissao.Enabled = false;
            chkInativo.Enabled = false;
        }

        public override void DesbloquearTxt()
        {
            cbTipo.Enabled = !modoEdicao; 
            txtNome.Enabled = true;
            txtApelido.Enabled = true;
            cbGenero.Enabled = true;
            dtpNascimento.Enabled = true;
            txtEndereco.Enabled = true;
            txtNumEnd.Enabled = true;
            txtBairro.Enabled = true;
            txtComplemento.Enabled = true;
            txtCEP.Enabled = true;
            txtIdCidade.Enabled = true;
            btnBuscar.Enabled = true;
            txtEmail.Enabled = true;
            txtTelefone.Enabled = true;
            txtCPF.Enabled = true;
            txtRG.Enabled = true;
            txtMatricula.Enabled = true;
            txtCargo.Enabled = true;
            txtSalario.Enabled = true;
            dtpAdmissao.Enabled = true;
            dtpDemissao.Enabled = true;
            chkInativo.Enabled = true;
        }

        public override void LimparTxt()
        {
            txtCodigo.Text = "0";
            cbTipo.SelectedIndex = 0;
            txtNome.Clear();
            txtApelido.Clear();
            cbGenero.SelectedIndex = 0;
            dtpNascimento.Value = DateTime.Today;
            txtEndereco.Clear();
            txtNumEnd.Clear();
            txtBairro.Clear();
            txtComplemento.Clear();
            txtCEP.Clear();
            txtIdCidade.Clear();
            txtCidade.Clear();
            cidadeSelecionadoId = -1;
            txtEmail.Clear();
            txtTelefone.Clear();
            txtCPF.Clear();
            txtRG.Clear();
            txtMatricula.Clear();
            txtCargo.Clear();
            txtSalario.Clear();
            dtpAdmissao.Value = DateTime.Today;
            dtpDemissao.Value = DateTime.Today;
            dtpDemissao.Checked = false;
            chkInativo.Checked = false;

            DateTime agora = DateTime.Now;
            lblDataCriacao.Text = $"Criado em: {agora:dd/MM/yyyy HH:mm}";
            lblDataModificacao.Text = $"Modificado em: {agora:dd/MM/yyyy HH:mm}";
        }

        public void CarregarFuncionario(
                                 int id, string nome, string apelido, string cpf_cnpj, string telefone, string email,
                                 string endereco, int numEndereco, string bairro, string complemento, string cep,
                                 string cargo, decimal salario, string matricula, string genero, string tipo,
                                 string nomeCidade, int idCidade, bool status, DateTime? dataAdmissao,
                                 DateTime? dataDemissao, DateTime? dataNascimento, string rg, DateTime? dataCriacao, DateTime? dataModificacao)
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
            if (dataNascimento.HasValue)
                dtpNascimento.Value = dataNascimento.Value;


            lblDataCriacao.Text = dataCriacao.HasValue
                ? $"Criado em: {dataCriacao.Value:dd/MM/yyyy HH:mm}"
                : "Criado em: -";

            lblDataModificacao.Text = dataModificacao.HasValue
                ? $"Modificado em: {dataModificacao.Value:dd/MM/yyyy HH:mm}"
                : "Modificado em: -";
        }

        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            if (modoExclusao)
            {
                var confirmacao = MessageBox.Show("Tem certeza que deseja excluir este Funcionário?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmacao == DialogResult.Yes)
                {
                    try
                    {
                        int id = int.Parse(txtCodigo.Text);
                        await controller.Excluir(id);
                        MessageBox.Show("Funcionário excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (!Validador.ValidarNumerico(txtNumEnd, "O número do endereço deve ser numérico.")) return;
                if (!Validador.CampoObrigatorio(txtBairro, "O Bairro é obrigatório.")) return;
                if (!Validador.ValidarEmail(txtEmail)) return;
                if (!Validador.CampoObrigatorio(txtCargo, "O cargo é obrigatório.")) return;
                if (!Validador.CampoObrigatorio(txtMatricula, "A matrícula é obrigatória.")) return;
                if (!Validador.CampoObrigatorio(txtSalario, "O salário é obrigatório.")) return;

                string tipoPessoa = cbTipo.Text.Trim();
                string documento = new string(txtCPF.Text.Where(char.IsDigit).ToArray());

                if (tipoPessoa == "Físico") { if (!Validador.ValidarCPF(documento)) { MessageBox.Show("CPF inválido."); txtCPF.Focus(); return; } }
                else if (tipoPessoa == "Jurídico") { if (!Validador.ValidarCNPJ(documento)) { MessageBox.Show("CNPJ inválido."); txtCPF.Focus(); return; } }
                else { MessageBox.Show("Tipo de pessoa inválido."); cbTipo.Focus(); return; }

                if (!Validador.ValidarIdSelecionado(cidadeSelecionadoId, "Selecione uma cidade.")) return;

                if (cidadeSelecionadoId > 0)
                {
                    var cidade = await cidadeController.BuscarPorId(cidadeSelecionadoId);
                    if (cidade != null && cidade.oEstado != null && cidade.oEstado.oPais != null)
                    {
                        if (cidade.oEstado.oPais.NomePais.Trim().Equals("Brasil", StringComparison.OrdinalIgnoreCase))
                        {
                            if (string.IsNullOrWhiteSpace(txtRG.Text))
                            {
                                MessageBox.Show("O campo RG é obrigatório para funcionários brasileiros.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtRG.Focus();
                                return;
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
                    string cpfCnpjLimpo = new string(txtCPF.Text.Where(char.IsDigit).ToArray());

                    List<Funcionario> funcionarios = await controller.ListarFuncionarios();
                    if (Validador.VerificarDuplicidade(funcionarios, f => new string(f.CPF_CNPJ.Where(char.IsDigit).ToArray()).Equals(cpfCnpjLimpo, StringComparison.OrdinalIgnoreCase) && f.Id != id, "Já existe um funcionário cadastrado com este CPF/CNPJ."))
                    {
                        txtCPF.Focus();
                        return;
                    }

                    Funcionario funcionario = new Funcionario
                    {
                        Id = id,
                        Nome = txtNome.Text,
                        CPF_CNPJ = txtCPF.Text,
                        Email = txtEmail.Text,
                        Endereco = txtEndereco.Text,
                        NumeroEndereco = string.IsNullOrWhiteSpace(txtNumEnd.Text) ? (int?)null : Convert.ToInt32(txtNumEnd.Text),
                        Bairro = txtBairro.Text,
                        Complemento = txtComplemento.Text,
                        Telefone = txtTelefone.Text,
                        Tipo = cbTipo.Text,
                        CEP = txtCEP.Text,
                        Cargo = txtCargo.Text,
                        Salario = Convert.ToDecimal(txtSalario.Text),
                        Ativo = !chkInativo.Checked,
                        DataAdmissao = dtpAdmissao.Value,
                        DataDemissao = dtpDemissao.Checked ? (DateTime?)dtpDemissao.Value : null,
                        DataNascimento = dtpNascimento.Value,
                        Rg = txtRG.Text,
                        Genero = genero,
                        Apelido = txtApelido.Text,
                        Matricula = txtMatricula.Text,
                        DataCadastro = id == 0 ? DateTime.Now : (DateTime?)null,
                        DataAlteracao = id > 0 ? DateTime.Now : (DateTime?)null,

                        // Propriedades para o DAO e API
                        CidadeId = cidadeSelecionadoId,
                        oCidade = new Cidade { Id = cidadeSelecionadoId }
                    };

                    await controller.Salvar(funcionario);
                    MessageBox.Show("Funcionário salvo com sucesso!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao salvar funcionário: " + ex.Message);
                }
            }
        }


        private void frmCadastroFuncionario_Load(object sender, EventArgs e)
        {
            if (modoExclusao)
            {
                btnSalvar.Text = "Deletar";
                BloquearTxt();
            }
            else if (modoEdicao)
            {
                btnSalvar.Text = "Salvar";
                DesbloquearTxt();
            }
            else 
            {
                btnSalvar.Text = "Salvar";
                DesbloquearTxt();
                LimparTxt();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            oFrmConsultaCidade.ModoSelecao = true;
            var resultado = oFrmConsultaCidade.ShowDialog();

            if (resultado == DialogResult.OK && oFrmConsultaCidade.CidadeSelecionado != null)
            {
                txtCidade.Text = oFrmConsultaCidade.CidadeSelecionado.NomeCidade;
                cidadeSelecionadoId = oFrmConsultaCidade.CidadeSelecionado.Id;
                txtIdCidade.Text = oFrmConsultaCidade.CidadeSelecionado.Id.ToString();
            }

            /*
            frmConsultaCidade consultaCidade = new frmConsultaCidade();
            consultaCidade.ModoSelecao = true;

            var resultado = consultaCidade.ShowDialog();

            if (resultado == DialogResult.OK && consultaCidade.CidadeSelecionado != null)
            {
                txtCidade.Text = consultaCidade.CidadeSelecionado.NomeCidade;
                cidadeSelecionadoId = consultaCidade.CidadeSelecionado.Id;
                txtIdCidade.Text = consultaCidade.CidadeSelecionado.Id.ToString();
            }
            */
        }

        private async void txtIdCidade_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdCidade.Text))
            {
                txtCidade.Text = "";
                cidadeSelecionadoId = -1;
                return;
            }

            if (!int.TryParse(txtIdCidade.Text, out int id))
            {
                MessageBox.Show("O campo ID Cidade deve conter apenas números.");
                txtCidade.Text = "";
                cidadeSelecionadoId = -1;
                return;
            }

            var cidade = await cidadeController.BuscarPorId(id);

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