using Projeto.Controller;
using Projeto.DAO;
using Projeto.Models;
using Projeto.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; // Adicionado
using System.Windows.Forms;

namespace Projeto.Views
{
    public partial class frmCadastroCliente : Projeto.frmBase
    {
        Cliente oCliente;
        frmConsultaCidade oFrmConsultaCidade;
        frmConsultaCondPgto oFrmConsultaCondPgto;

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
            cbTipo.SelectedIndexChanged += cbTipo_SelectedIndexChanged;
            cbTipo_SelectedIndexChanged(null, null);
        }

        public override void ConhecaObj(object obj, object ctrl)
        {
            if (obj != null)
            {
                oCliente = (Cliente)obj;
            }
            if (ctrl != null)
            {
                controller = (ClienteController)ctrl;
            }
        }

        public void setFrmConsultaCidade(object obj)
        {
            if (obj != null)
            {
                oFrmConsultaCidade = (frmConsultaCidade)obj;
            }
        }

        public void setFrmConsultaCondPgto(object obj)
        {
            if (obj != null)
            {
                oFrmConsultaCondPgto = (frmConsultaCondPgto)obj;
            }
        }

        public override void CarregaTxt()
        {
            txtCodigo.Text = oCliente.Id.ToString();
            txtNome.Text = oCliente.Nome;
            cbGenero.Text = oCliente.Genero;
            dtpNascimento.Value = oCliente.DataNascimento.Value;
            txtEndereco.Text = oCliente.Endereco;
            txtNumEnd.Text = oCliente.NumeroEndereco.ToString();
            txtBairro.Text = oCliente.Bairro;
            txtComplemento.Text = oCliente.Complemento;
            txtCEP.Text = oCliente.CEP;
            txtIdCidade.Text = oCliente.CidadeId.ToString();
            txtCidade.Text = oCliente.NomeCidade;
            txtEmail.Text = oCliente.Email;
            txtTelefone.Text = oCliente.Telefone;
            txtCPF.Text = oCliente.CPF_CNPJ;
            txtRG.Text = oCliente.Rg;
            txtCondicao.Text = oCliente.DescricaoCondicao;
            chkInativo.Checked = oCliente.Ativo;
            lblDataCriacao.Text = oCliente.DataCadastro.HasValue ? $"Criado em: {oCliente.DataCadastro.Value:dd/MM/yyyy HH:mm}" : "Criado em: -";
            lblDataModificacao.Text = oCliente.DataAlteracao.HasValue ? $"Modificado em: {oCliente.DataAlteracao.Value:dd/MM/yyyy HH:mm}" : "Modificado em: -";
        }

        public override void BloquearTxt()
        {
            cbTipo.Enabled = false;
            txtNome.Enabled = false;
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
            txtCondicao.Enabled = false;
            btnBuscarCond.Enabled = false;
            chkInativo.Enabled = false;
        }

        public override void DesbloquearTxt()
        {
            cbTipo.Enabled = !modoEdicao;

            txtNome.Enabled = true;
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
            txtCondicao.Enabled = true;
            btnBuscarCond.Enabled = true;
            chkInativo.Enabled = true;
        }

        public override void LimparTxt()
        {
            txtCodigo.Text = "0";
            cbTipo.SelectedIndex = 0;
            txtNome.Clear();
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
            txtCondicao.Clear();
            condicaoSelecionadoId = -1;
            chkInativo.Checked = false;

            DateTime agora = DateTime.Now;
            lblDataCriacao.Text = $"Criado em: {agora:dd/MM/yyyy HH:mm}";
            lblDataModificacao.Text = $"Modificado em: {agora:dd/MM/yyyy HH:mm}";
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

        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            if (modoExclusao)
            {
                var confirmacao = MessageBox.Show("Tem certeza que deseja excluir este Cliente?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmacao == DialogResult.Yes)
                {
                    try
                    {
                        int id = int.Parse(txtCodigo.Text);

                        await controller.Excluir(id);
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

                if (!Validador.ValidarIdSelecionado(cidadeSelecionadoId, "Selecione uma cidade.")) return;

                if (cidadeSelecionadoId > 0)
                {
                    var cidade = await cidadeController.BuscarPorId(cidadeSelecionadoId);
                    if (cidade != null)
                    {
                        var estado = await estadoController.BuscarPorId(cidade.EstadoId);
                        if (estado != null)
                        {
                            var pais = await paisController.BuscarPorId(estado.PaisId);
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

                if (!Validador.ValidarIdSelecionado(condicaoSelecionadoId, "Selecione uma condição de pagamento")) return;

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
                    DateTime dataCriacao = id == 0 ? DateTime.Now : DateTime.Parse(lblDataCriacao.Text.Replace("Criado em: ", ""));
                    DateTime dataModificacao = DateTime.Now;
                    string cpfCnpjLimpo = new string(txtCPF.Text.Where(char.IsDigit).ToArray());

                    List<Cliente> clientes = await controller.ListarCliente();

                    if (Validador.VerificarDuplicidade(clientes, c =>
                        new string(c.CPF_CNPJ.Where(char.IsDigit).ToArray()).Equals(cpfCnpjLimpo, StringComparison.OrdinalIgnoreCase)
                        && c.Id != id, "Já existe um cliente cadastrado com este CPF/CNPJ."))
                    {
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


                    await controller.Salvar(cliente);
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

        private void btnBuscarCond_Click(object sender, EventArgs e)
        {
            oFrmConsultaCondPgto.ModoSelecao = true;
            var resultado = oFrmConsultaCondPgto.ShowDialog();

            if (resultado == DialogResult.OK && oFrmConsultaCondPgto.CondicaoSelecionado != null)
            {
                txtCondicao.Text = oFrmConsultaCondPgto.CondicaoSelecionado.Descricao;
                condicaoSelecionadoId = oFrmConsultaCondPgto.CondicaoSelecionado.Id;
            }

            /*
            frmConsultaCondPgto consultaCondicao = new frmConsultaCondPgto();
            consultaCondicao.ModoSelecao = true;

            var resultado = consultaCondicao.ShowDialog();

            if (resultado == DialogResult.OK && consultaCondicao.CondicaoSelecionado != null)
            {
                txtCondicao.Text = consultaCondicao.CondicaoSelecionado.Descricao;
                condicaoSelecionadoId = consultaCondicao.CondicaoSelecionado.Id;
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

            if (!Validador.ValidarNumerico(txtIdCidade, "O campo ID Cidade deve conter apenas números."))
            {
                txtCidade.Text = "";
                cidadeSelecionadoId = -1;
                return;
            }

            int id = int.Parse(txtIdCidade.Text);
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