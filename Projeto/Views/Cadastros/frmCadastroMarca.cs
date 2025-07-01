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
        private MarcaController controller = new MarcaController();


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

                var marcas = controller.ListarMarcas();
                bool existeDuplicado = marcas.Exists(m =>
                    m.NomeMarca.Trim().Equals(marcaNome, StringComparison.OrdinalIgnoreCase)
                    && m.Id != id);

                if (existeDuplicado)
                {
                    MessageBox.Show("Já existe uma marca cadastrada com este nome.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNome.Focus();
                    return;
                }

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
