using Projeto.Controller;
using Projeto.DAO;
using Projeto.Models;
using Projeto.Utils;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Projeto.Views
{
    public partial class frmCadastroPais : Projeto.frmBase
    {
        Pais oPais;
        PaisController controller = new PaisController();
        public bool modoEdicao = false;
        public bool modoExclusao = false;

        public frmCadastroPais() : base()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
        }

        public override void ConhecaObj(object obj, object ctrl)
        {
            if (obj != null)
            {
                oPais = (Pais)obj;
            }
            if (ctrl != null)
            {
                controller = (PaisController)ctrl;
            }
        }

        public override void LimparTxt()
        {
            txtCodigo.Text = "0";
            DateTime agora = DateTime.Now;
            lblDataCriacao.Text = $"Criado em: {agora:dd/MM/yyyy HH:mm}";
            lblDataModificacao.Text = $"Modificado em: {agora:dd/MM/yyyy HH:mm}";
            txtSigla.Clear();
            txtDDI.Clear();
            txtMoeda.Clear();   
        }

        public override void CarregaTxt()
        {
            txtCodigo.Text = oPais.Id.ToString();
            txtNome.Text = oPais.NomePais;
            txtSigla.Text = oPais.Sigla;
            txtDDI.Text = oPais.DDI;
            txtMoeda.Text = oPais.Moeda;
            chkInativo.Checked = oPais.Ativo;
            lblDataCriacao.Text = oPais.DataCadastro.HasValue ? $"Criado em: {oPais.DataCadastro.Value:dd/MM/yyyy HH:mm}" : "Criado em: -";
            lblDataModificacao.Text = oPais.DataAlteracao.HasValue ? $"Modificado em: {oPais.DataAlteracao.Value:dd/MM/yyyy HH:mm}" : "Modificado em: -";
        }

        public override void BloquearTxt()
        {
            txtNome.Enabled = false;
            txtSigla.Enabled = false;
            txtDDI.Enabled = false;
            txtMoeda.Enabled = false;
            chkInativo.Enabled = false;
        }

        public override void DesbloquearTxt()
        {
            txtNome.Enabled = true;
            txtSigla.Enabled = true;
            txtDDI.Enabled = true;
            txtMoeda.Enabled = true;
            chkInativo.Enabled = true;
        }

        public void CarregarPais(int id, string nome, string sigla, string ddi, string moeda, bool status, DateTime? dataCriacao, DateTime? dataModificacao)
        {
            modoEdicao = true;
            txtCodigo.Text = id.ToString();
            txtNome.Text = nome;
            txtSigla.Text = sigla;
            txtDDI.Text = ddi;
            txtMoeda.Text = moeda;
            chkInativo.Checked = !status;

            lblDataCriacao.Text = dataCriacao.HasValue ? $"Criado em: {dataCriacao.Value:dd/MM/yyyy HH:mm}" : "Criado em: -";
            lblDataModificacao.Text = dataModificacao.HasValue ? $"Modificado em: {dataModificacao.Value:dd/MM/yyyy HH:mm}" : "Modificado em: -";
        }

        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            if (modoExclusao)
            {
                var confirmacao = MessageBox.Show("Tem certeza que deseja excluir este País?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmacao == DialogResult.Yes)
                {
                    try
                    {
                        int id = int.Parse(txtCodigo.Text);
                        await controller.Excluir(id);
                        MessageBox.Show("País excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (!Validador.CampoObrigatorio(txtNome, "O nome do país é obrigatório.")) return;

                try
                {
                    LimparTxt();

                    int id = string.IsNullOrEmpty(txtCodigo.Text) ? 0 : int.Parse(txtCodigo.Text);
                    string nome = txtNome.Text;
                    string sigla = txtSigla.Text;
                    string ddi = txtDDI.Text;
                    string moeda = txtMoeda.Text;

                    var paises = await controller.ListarPais();
                    if (Validador.VerificarDuplicidade(paises, p =>
                        p.NomePais.Trim().Equals(nome, StringComparison.OrdinalIgnoreCase)
                        && p.Id != id, "Já existe um país cadastrado com este nome."))
                    {
                        txtNome.Focus();
                        return;
                    }

                    bool status = !chkInativo.Checked;

                    DateTime dataCriacao = id == 0 ? DateTime.Now : DateTime.Parse(lblDataCriacao.Text.Replace("Criado em: ", ""));
                    DateTime dataModificacao = DateTime.Now;

                    Pais pais = new Pais
                    {
                        Id = id,
                        NomePais = nome,
                        Sigla = sigla,
                        DDI = ddi,
                        Moeda = moeda,
                        Ativo = status,
                        DataCadastro = dataCriacao,
                        DataAlteracao = dataModificacao
                    };

                    await controller.Salvar(pais);

                    MessageBox.Show("País salvo com sucesso!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao salvar país: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void frmCadastroPais_Load(object sender, EventArgs e)
        {
            if (modoExclusao)
            {
                btnSalvar.Text = "Deletar";
                txtNome.Enabled = false;
                txtSigla.Enabled = false;
                txtDDI.Enabled = false;
                txtMoeda.Enabled = false;
                chkInativo.Enabled = false;
            }
            else if (modoEdicao == false)
            {
                txtCodigo.Text = "0";
                DateTime agora = DateTime.Now;
                lblDataCriacao.Text = $"Criado em: {agora:dd/MM/yyyy HH:mm}";
                lblDataModificacao.Text = $"Modificado em: {agora:dd/MM/yyyy HH:mm}";
            }
        }
    }
}