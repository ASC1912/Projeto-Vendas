using Projeto.Controller;
using Projeto.DAO;
using Projeto.Models;
using Projeto.Utils;
using System;
using System.Windows.Forms;

namespace Projeto.Views.Cadastros
{
    public partial class frmCadastroGrupo : Projeto.frmBase
    {
        Grupo oGrupo;
        public bool modoEdicao = false;
        public bool modoExclusao = false;
        private GrupoController controller = new GrupoController();

        public frmCadastroGrupo()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
        }

        public override void ConhecaObj(object obj, object ctrl)
        {
            if (obj != null)
            {
                oGrupo = (Grupo)obj;
            }
            if (ctrl != null)
            {
                controller = (GrupoController)ctrl;
            }
        }

        public override void CarregaTxt()
        {
            txtCodigo.Text = oGrupo.Id.ToString();
            txtNome.Text = oGrupo.NomeGrupo;
            txtDescricao.Text = oGrupo.Descricao;
            chkInativo.Checked = oGrupo.Ativo;
            lblDataCriacao.Text = oGrupo.DataCadastro.HasValue ? $"Criado em: {oGrupo.DataCadastro.Value:dd/MM/yyyy HH:mm}" : "Criado em: -";
            lblDataModificacao.Text = oGrupo.DataAlteracao.HasValue ? $"Modificado em: {oGrupo.DataAlteracao.Value:dd/MM/yyyy HH:mm}" : "Modificado em: -";
        }
        public override void BloquearTxt()
        {
            txtNome.Enabled = false;
            txtDescricao.Enabled = false;
            chkInativo.Enabled = false;
        }

        public override void DesbloquearTxt()
        {
            txtNome.Enabled = true;
            txtDescricao.Enabled = true;
            chkInativo.Enabled = true;
        }

        public override void LimparTxt()
        {
            txtCodigo.Text = "0";
            txtNome.Clear();
            txtDescricao.Clear();
            chkInativo.Checked = false;

            DateTime agora = DateTime.Now;
            lblDataCriacao.Text = $"Criado em: {agora:dd/MM/yyyy HH:mm}";
            lblDataModificacao.Text = $"Modificado em: {agora:dd/MM/yyyy HH:mm}";
        }


        public void CarregarGrupo(int id, string nomeGrupo, string descricao, bool ativo, DateTime? dataCadastro, DateTime? dataAlteracao)
        {
            modoEdicao = true;
            txtCodigo.Text = id.ToString();
            txtNome.Text = nomeGrupo;
            txtDescricao.Text = descricao;
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
                var confirmacao = MessageBox.Show("Tem certeza que deseja excluir este grupo?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmacao == DialogResult.Yes)
                {
                    try
                    {
                        int id = int.Parse(txtCodigo.Text);
                        controller.Excluir(id);
                        MessageBox.Show("Grupo excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (!Validador.CampoObrigatorio(txtNome, "O nome é obrigatório.")) return;

                try
                {
                    int id = string.IsNullOrEmpty(txtCodigo.Text) ? 0 : int.Parse(txtCodigo.Text);
                    string nome = txtNome.Text;

                    var grupos = controller.ListarGrupos();
                    if (Validador.VerificarDuplicidade(grupos, g =>
                        g.NomeGrupo.Trim().Equals(nome, StringComparison.OrdinalIgnoreCase)
                        && g.Id != id, "Já existe um grupo cadastrado com este nome."))
                    {
                        txtNome.Focus();
                        return; 
                    }

                    DateTime dataCriacao = id == 0
                        ? DateTime.Now
                        : DateTime.Parse(lblDataCriacao.Text.Replace("Criado em: ", ""));

                    DateTime dataModificacao = DateTime.Now;

                    Grupo grupo = new Grupo
                    {
                        Id = id,
                        NomeGrupo = nome,
                        Descricao = txtDescricao.Text,
                        Ativo = !chkInativo.Checked,
                        DataCadastro = dataCriacao,
                        DataAlteracao = dataModificacao
                    };

                    controller.Salvar(grupo);

                    MessageBox.Show("Grupo salvo com sucesso!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                }
            }
        }

        private void frmCadastroGrupo_Load(object sender, EventArgs e)
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