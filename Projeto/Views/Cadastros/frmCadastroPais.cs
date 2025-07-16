using Projeto.Controller;
using Projeto.Models;
using Projeto.Utils;
using System;
using System.Linq; // Adicione este using
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto.Views
{
    public partial class frmCadastroPais : Projeto.frmBase
    {
        private readonly PaisController controller = new PaisController();
        public bool modoEdicao = false;
        public bool modoExclusao = false;

        public frmCadastroPais() : base()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
        }

        public void CarregarPais(int id, string nome, bool status, DateTime? dataCriacao, DateTime? dataModificacao)
        {
            modoEdicao = true;
            txtCodigo.Text = id.ToString();
            txtNome.Text = nome;
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
                    int id = string.IsNullOrEmpty(txtCodigo.Text) ? 0 : int.Parse(txtCodigo.Text);
                    string nome = txtNome.Text;
                    bool status = !chkInativo.Checked;

                    DateTime dataCriacao = id == 0 ? DateTime.Now : DateTime.Parse(lblDataCriacao.Text.Replace("Criado em: ", ""));
                    DateTime dataModificacao = DateTime.Now;

                    Pais pais = new Pais
                    {
                        Id = id,
                        NomePais = nome,
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