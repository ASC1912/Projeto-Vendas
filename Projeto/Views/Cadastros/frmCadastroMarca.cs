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
        public bool modoExclusao = false; 
        private MarcaController controller = new MarcaController();


        public frmCadastroMarca() : base()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
        }

        public override void ConhecaObj(object obj, object ctrl)
        {
            if (ctrl != null)
            {
                controller = (MarcaController)ctrl;
            }
        }

        public override void BloquearTxt()
        {
            txtNome.Enabled = false;
            chkInativo.Enabled = false;
        }

        public override void DesbloquearTxt()
        {
            txtNome.Enabled = true;
            chkInativo.Enabled = true;
        }

        public override void LimparTxt()
        {
            txtCodigo.Text = "0";
            txtNome.Clear();
            chkInativo.Checked = false;

            DateTime agora = DateTime.Now;
            lblDataCriacao.Text = $"Criado em: {agora:dd/MM/yyyy HH:mm}";
            lblDataModificacao.Text = $"Modificado em: {agora:dd/MM/yyyy HH:mm}";
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
            if (modoExclusao)
            {
                var confirmacao = MessageBox.Show("Tem certeza que deseja excluir esta marca?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmacao == DialogResult.Yes)
                {
                    try
                    {
                        int id = int.Parse(txtCodigo.Text);
                        controller.Excluir(id);
                        MessageBox.Show("Marca excluída com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("Cannot delete or update a parent row"))
                        {
                            MessageBox.Show(
                                "Não é possível excluir este item, pois existem produtos vinculados a ele.",
                                "Erro ao excluir",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                        }
                        else
                        {
                            MessageBox.Show($"Erro ao excluir: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                if (!Validador.CampoObrigatorio(txtNome, "O nome da marca é obrigatório.")) return;

                try
                {
                    int id = string.IsNullOrEmpty(txtCodigo.Text) ? 0 : int.Parse(txtCodigo.Text);
                    string marcaNome = txtNome.Text;
                    bool ativo = !chkInativo.Checked;

                    var marcas = controller.ListarMarcas();
                    if (Validador.VerificarDuplicidade(marcas, m =>
                       m.NomeMarca.Trim().Equals(marcaNome, StringComparison.OrdinalIgnoreCase)
                       && m.Id != id, "Já existe uma marca cadastrada com este nome."))
                    {
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
        }

        private void frmCadastroMarca_Load(object sender, EventArgs e)
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
    }
}
