using Projeto.Controller;
using Projeto.Models;
using Projeto.Utils;
using Projeto.Views.Consultas;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto.Views.Cadastros
{
    public partial class frmCadastroTransportadora : Projeto.frmBase
    {
        Transportadora aTransportadora;
        frmConsultaCidade oFrmConsultaCidade;
        frmConsultaCondPgto oFrmConsultaCondPgto;

        private CidadeController cidadeController = new CidadeController();
        private CondicaoPagamentoController condicaoPagamentoController = new CondicaoPagamentoController();
        private TransportadoraController controller = new TransportadoraController();
        public bool modoEdicao = false;
        public bool modoExclusao = false;
        private int cidadeSelecionadoId = -1;
        private int condicaoSelecionadoId = -1;

        public frmCadastroTransportadora() : base()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
            cbTipo.SelectedIndex = 0;
            cbTipo.SelectedIndexChanged += cbTipo_SelectedIndexChanged;
            cbTipo_SelectedIndexChanged(null, null); // Chama o evento para definir o rótulo inicial
        }

        public override void ConhecaObj(object obj, object ctrl)
        {
            if (obj != null)
            {
                aTransportadora = (Transportadora)obj;
            }
            if (ctrl != null)
            {
                controller = (TransportadoraController)ctrl;
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
            txtCodigo.Text = aTransportadora.Id.ToString();
            cbTipo.Text = aTransportadora.Tipo;
            txtNome.Text = aTransportadora.Nome;
            txtEndereco.Text = aTransportadora.Endereco;
            txtNumEnd.Text = aTransportadora.NumeroEndereco.ToString();
            txtBairro.Text = aTransportadora.Bairro;
            txtComplemento.Text = aTransportadora.Complemento;
            txtCEP.Text = aTransportadora.CEP;
            txtIdCidade.Text = aTransportadora.CidadeId.ToString();
            cidadeSelecionadoId = aTransportadora.CidadeId ?? -1;
            txtCidade.Text = aTransportadora.NomeCidade;
            txtEmail.Text = aTransportadora.Email;
            txtTelefone.Text = aTransportadora.Telefone;
            txtCPF.Text = aTransportadora.CPF_CNPJ;
            txtInscEst.Text = aTransportadora.InscricaoEstadual;
            txtInscEstSubTrib.Text = aTransportadora.InscricaoEstadualSubTrib;
            txtCondicao.Text = aTransportadora.oCondicaoPagamento?.Descricao;
            condicaoSelecionadoId = aTransportadora.IdCondicao ?? -1;
            chkInativo.Checked = !aTransportadora.Ativo;
            lblDataCriacao.Text = aTransportadora.DataCadastro.HasValue ? $"Criado em: {aTransportadora.DataCadastro.Value:dd/MM/yyyy HH:mm}" : "Criado em: -";
            lblDataModificacao.Text = aTransportadora.DataAlteracao.HasValue ? $"Modificado em: {aTransportadora.DataAlteracao.Value:dd/MM/yyyy HH:mm}" : "Modificado em: -";
        }

        public override void BloquearTxt()
        {
            cbTipo.Enabled = false;
            txtNome.Enabled = false;
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
            txtInscEst.Enabled = false;
            txtInscEstSubTrib.Enabled = false;
            txtCondicao.Enabled = false;
            btnBuscarCond.Enabled = false;
            chkInativo.Enabled = false;
        }

        public override void DesbloquearTxt()
        {
            cbTipo.Enabled = !modoEdicao;
            txtNome.Enabled = true;
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
            txtInscEst.Enabled = true;
            txtInscEstSubTrib.Enabled = true;
            txtCondicao.Enabled = true;
            btnBuscarCond.Enabled = true;
            chkInativo.Enabled = true;
        }

        public override void LimparTxt()
        {
            txtCodigo.Text = "0";
            cbTipo.SelectedIndex = 0;
            txtNome.Clear();
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
            txtInscEst.Clear();
            txtInscEstSubTrib.Clear();
            txtCondicao.Clear();
            condicaoSelecionadoId = -1;
            chkInativo.Checked = false;
            DateTime agora = DateTime.Now;
            lblDataCriacao.Text = $"Criado em: {agora:dd/MM/yyyy HH:mm}";
            lblDataModificacao.Text = $"Modificado em: {agora:dd/MM/yyyy HH:mm}";
        }

        private void cbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblCPF.Text = (cbTipo.Text == "JURÍDICO") ? "CNPJ" : "CPF";
        }

        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            if (modoExclusao)
            {
                var confirmacao = MessageBox.Show("Tem certeza que deseja excluir esta Transportadora?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmacao == DialogResult.Yes)
                {
                    try
                    {
                        int id = int.Parse(txtCodigo.Text);
                        await controller.Excluir(id);
                        MessageBox.Show("Transportadora excluída com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Não é possível excluir este item, pois existem registros vinculados a ele.", "Erro ao excluir", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                string tipoPessoa = cbTipo.Text.Trim();
                string documento = new string(txtCPF.Text.Where(char.IsDigit).ToArray());

                if (tipoPessoa == "FÍSICO" && !Validador.ValidarCPF(documento))
                {
                    MessageBox.Show("CPF inválido.");
                    txtCPF.Focus();
                    return;
                }
                else if (tipoPessoa == "JURÍDICO" && !Validador.ValidarCNPJ(documento))
                {
                    MessageBox.Show("CNPJ inválido.");
                    txtCPF.Focus();
                    return;
                }

                if (!Validador.ValidarIdSelecionado(cidadeSelecionadoId, "Selecione uma cidade.")) return;
                if (!Validador.ValidarIdSelecionado(condicaoSelecionadoId, "Selecione uma condição de pagamento.")) return;

                try
                {
                    int id = string.IsNullOrWhiteSpace(txtCodigo.Text) ? 0 : Convert.ToInt32(txtCodigo.Text);

                    var transportadoras = await controller.ListarTransportadoras();
                    if (Validador.VerificarDuplicidade(transportadoras, t => new string(t.CPF_CNPJ.Where(char.IsDigit).ToArray()).Equals(documento) && t.Id != id, "Já existe uma transportadora cadastrada com este CPF/CNPJ."))
                    {
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
                        CidadeId = cidadeSelecionadoId,
                        IdCondicao = condicaoSelecionadoId > 0 ? (int?)condicaoSelecionadoId : null,
                        Ativo = !chkInativo.Checked,
                        DataCadastro = id == 0 ? DateTime.Now : aTransportadora.DataCadastro,
                        DataAlteracao = id > 0 ? DateTime.Now : (DateTime?)null
                    };

                    await controller.Salvar(transportadora);
                    MessageBox.Show("Transportadora salva com sucesso!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao salvar transportadora: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            oFrmConsultaCidade.ModoSelecao = true;
            if (oFrmConsultaCidade.ShowDialog() == DialogResult.OK && oFrmConsultaCidade.CidadeSelecionado != null)
            {
                txtCidade.Text = oFrmConsultaCidade.CidadeSelecionado.NomeCidade;
                cidadeSelecionadoId = oFrmConsultaCidade.CidadeSelecionado.Id;
                txtIdCidade.Text = oFrmConsultaCidade.CidadeSelecionado.Id.ToString();
            }
        }

        private void btnBuscarCond_Click(object sender, EventArgs e)
        {
            oFrmConsultaCondPgto.ModoSelecao = true;
            if (oFrmConsultaCondPgto.ShowDialog() == DialogResult.OK && oFrmConsultaCondPgto.CondicaoSelecionado != null)
            {
                txtCondicao.Text = oFrmConsultaCondPgto.CondicaoSelecionado.Descricao;
                condicaoSelecionadoId = oFrmConsultaCondPgto.CondicaoSelecionado.Id;
            }
        }

        private async void txtIdCidade_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdCidade.Text))
            {
                txtCidade.Clear();
                cidadeSelecionadoId = -1;
                return;
            }

            if (int.TryParse(txtIdCidade.Text, out int id))
            {
                var cidade = await cidadeController.BuscarPorId(id);
                if (cidade != null)
                {
                    txtCidade.Text = cidade.NomeCidade;
                    cidadeSelecionadoId = cidade.Id;
                }
                else
                {
                    MessageBox.Show("Cidade não encontrada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCidade.Clear();
                    cidadeSelecionadoId = -1;
                }
            }
            else
            {
                MessageBox.Show("ID da cidade inválido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCidade.Clear();
                cidadeSelecionadoId = -1;
            }
        }

        private void frmCadastroTransportadora_Load(object sender, EventArgs e)
        {
        }
    }
}