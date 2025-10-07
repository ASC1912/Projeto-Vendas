using Projeto.Controller;
using Projeto.Models;
using Projeto.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto.Views.Cadastros
{
    public partial class frmCadastroUnidadeMedida : frmBase
    {
        private UnidadeMedida oUnidade;
        private UnidadeMedidaController controller = new UnidadeMedidaController();
        public bool modoEdicao = false;
        public bool modoExclusao = false;

        public frmCadastroUnidadeMedida()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
        }

        public override void ConhecaObj(object obj, object ctrl)
        {
            if (obj != null) oUnidade = (UnidadeMedida)obj;
            if (ctrl != null) controller = (UnidadeMedidaController)ctrl;
        }

        public override void LimparTxt()
        {
            txtCodigo.Text = "0";
            txtNome.Clear();
            txtSigla.Clear();
            chkInativo.Checked = false;
            DateTime agora = DateTime.Now;
            lblDataCriacao.Text = $"Criado em: {agora:dd/MM/yyyy HH:mm}";
            lblDataModificacao.Text = $"Modificado em: {agora:dd/MM/yyyy HH:mm}";
        }

        public override void CarregaTxt()
        {
            txtCodigo.Text = oUnidade.Id.ToString();
            txtNome.Text = oUnidade.NomeUnidade;
            txtSigla.Text = oUnidade.Sigla;
            chkInativo.Checked = !oUnidade.Ativo;
            lblDataCriacao.Text = oUnidade.DataCadastro.HasValue ? $"Criado em: {oUnidade.DataCadastro.Value:dd/MM/yyyy HH:mm}" : "Criado em: -";
            lblDataModificacao.Text = oUnidade.DataAlteracao.HasValue ? $"Modificado em: {oUnidade.DataAlteracao.Value:dd/MM/yyyy HH:mm}" : "Modificado em: -";
        }

        public override void BloquearTxt()
        {
            txtNome.Enabled = false;
            txtSigla.Enabled = false;
            chkInativo.Enabled = false;
        }

        public override void DesbloquearTxt()
        {
            txtNome.Enabled = true;
            txtSigla.Enabled = true;
            chkInativo.Enabled = true;
        }

        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            if (modoExclusao)
            {
                var confirmacao = MessageBox.Show("Tem certeza que deseja excluir este item?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmacao == DialogResult.Yes)
                {
                    try
                    {
                        await controller.Excluir(int.Parse(txtCodigo.Text));
                        MessageBox.Show("Unidade de Medida excluída com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (!Validador.CampoObrigatorio(txtSigla, "A sigla é obrigatória.")) return;

                try
                {
                    UnidadeMedida unidade = new UnidadeMedida
                    {
                        Id = int.Parse(txtCodigo.Text),
                        NomeUnidade = txtNome.Text,
                        Sigla = txtSigla.Text,
                        Ativo = !chkInativo.Checked
                    };

                    await controller.Salvar(unidade);
                    MessageBox.Show("Unidade de Medida salva com sucesso!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao salvar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}