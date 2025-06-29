using Projeto.Controller;
using Projeto.Models;
using Projeto.Utils;
using System;
using System.Windows.Forms;

namespace Projeto.Views.Cadastros
{
    public partial class frmCadastroMarca : Projeto.frmBase
    {
        public bool modoEdicao = false;

        public frmCadastroMarca() : base()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
        }

        public void CarregarMarca(int id, string nomeMarca, bool ativo, DateTime? dataCadastro, DateTime? dataAlteracao)
        {
            modoEdicao = true;

            txtCodigo.Text = id.ToString();
            txtNome.Text = nomeMarca;
            chkInativo.Checked = !ativo;

            lblDataCriacao.Text = dataCadastro.HasValue
                ? $"Criado em: {dataCadastro.Value:dd/MM/yyyy HH:mm}"
                : "Criado em: -";

            lblDataModificacao.Text = dataAlteracao.HasValue
                ? $"Modificado em: {dataAlteracao.Value:dd/MM/yyyy HH:mm}"
                : "Modificado em: -";
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!Validador.CampoObrigatorio(txtNome, "O nome da marca é obrigatório.")) return;

            try
            {
                int id = string.IsNullOrEmpty(txtCodigo.Text) ? 0 : int.Parse(txtCodigo.Text);
                string marcaNome = txtNome.Text;
                bool ativo = !chkInativo.Checked;

                DateTime dataCadastro = id == 0
                    ? DateTime.Now
                    : DateTime.Parse(lblDataCriacao.Text.Replace("Criado em: ", ""));

                DateTime dataAlteracao = DateTime.Now;

                Marca marca = new Marca
                {
                    Id = id,
                    NomeMarca = marcaNome,
                    Ativo = ativo,
                    DataCadastro = dataCadastro,
                    DataAlteracao = dataAlteracao
                };

                MarcaController controller = new MarcaController();
                controller.Salvar(marca);

                MessageBox.Show("Marca salva com sucesso!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar marca: " + ex.Message);
            }
        }

        private void frmCadastroMarca_Load(object sender, EventArgs e)
        {
            if (modoEdicao == false)
            {
                txtCodigo.Text = "0";
                DateTime agora = DateTime.Now;

                lblDataCriacao.Text = $"Criado em: {agora:dd/MM/yyyy HH:mm}";
                lblDataModificacao.Text = $"Modificado em: {agora:dd/MM/yyyy HH:mm}";
            }
        }
    }
}
