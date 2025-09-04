using Projeto.Controller;
using Projeto.DAO;
using Projeto.Models;
using Projeto.Utils;
using System;
using System.Windows.Forms;

namespace Projeto
{
    public partial class frmCadastroFrmPgto : Projeto.frmBase
    {
        FormaPagamento oFormaPgto;
        public bool modoEdicao = false;
        public bool modoExclusao = false;
        private FormaPagamentoController controller = new FormaPagamentoController();

        public frmCadastroFrmPgto() : base()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
        }

        public override void ConhecaObj(object obj, object ctrl)
        {
            if (obj != null)
            {
                oFormaPgto = (FormaPagamento)obj;
            }
            if (ctrl != null)
            {
                controller = (FormaPagamentoController)ctrl;
            }
        }

        public override void CarregaTxt()
        {
            txtCodigo.Text = oFormaPgto.Id.ToString();
            txtDescricao.Text = oFormaPgto.Descricao;
            chkInativo.Checked = oFormaPgto.Ativo;
            lblDataCriacao.Text = oFormaPgto.DataCadastro.HasValue ? $"Criado em: {oFormaPgto.DataCadastro.Value:dd/MM/yyyy HH:mm}" : "Criado em: -";
            lblDataModificacao.Text = oFormaPgto.DataAlteracao.HasValue ? $"Modificado em: {oFormaPgto.DataAlteracao.Value:dd/MM/yyyy HH:mm}" : "Modificado em: -";
        }

        public override void BloquearTxt()
        {
            txtDescricao.Enabled = false;
            chkInativo.Enabled = false;
        }

        public override void DesbloquearTxt()
        {
            txtDescricao.Enabled = true;
            chkInativo.Enabled = true;
        }

        public override void LimparTxt()
        {
            txtCodigo.Text = "0";
            txtDescricao.Clear();
            chkInativo.Checked = false;

            DateTime agora = DateTime.Now;
            lblDataCriacao.Text = $"Criado em: {agora:dd/MM/yyyy HH:mm}";
            lblDataModificacao.Text = $"Modificado em: {agora:dd/MM/yyyy HH:mm}";
        }

       
        public void CarregarFormaPagamento(int id, string descricao, bool ativo, DateTime? dataCadastro, DateTime? dataAlteracao)
        {
            modoEdicao = true;
            txtCodigo.Text = id.ToString();
            txtDescricao.Text = descricao;
            chkInativo.Checked = !ativo;

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
                var confirmacao = MessageBox.Show("Tem certeza que deseja excluir esta forma de pagamento?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmacao == DialogResult.Yes)
                {
                    try
                    {
                        int id = int.Parse(txtCodigo.Text);
                        await controller.Excluir(id); 
                        MessageBox.Show("Forma de pagamento excluída com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("Cannot delete or update a parent row"))
                        {
                            MessageBox.Show(
                                "Não é possível excluir este item, pois existem registros vinculados a ele.",
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
                if (!Validador.CampoObrigatorio(txtDescricao, "A descrição é obrigatória.")) return;

                try
                {
                    int id = string.IsNullOrEmpty(txtCodigo.Text) ? 0 : int.Parse(txtCodigo.Text);
                    string descricao = txtDescricao.Text;

                    var formasPagamento = await controller.ListarFormaPagamento();
                    if (Validador.VerificarDuplicidade(formasPagamento, f =>
                        f.Descricao.Trim().Equals(descricao.Trim(), StringComparison.OrdinalIgnoreCase)
                        && f.Id != id, "Já existe uma forma de pagamento com esta descrição."))
                    {
                        txtDescricao.Focus();
                        return;
                    }

                    bool status = !chkInativo.Checked;
                    DateTime dataCriacao = id == 0 ? DateTime.Now : DateTime.Parse(lblDataCriacao.Text.Replace("Criado em: ", ""));
                    DateTime dataModificacao = DateTime.Now;

                    FormaPagamento forma = new FormaPagamento
                    {
                        Id = id,
                        Descricao = descricao,
                        Ativo = status,
                        DataCadastro = dataCriacao,
                        DataAlteracao = dataModificacao
                    };

                    await controller.Salvar(forma); 

                    MessageBox.Show("Forma de pagamento salva com sucesso!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                }
            }
        }

        private void frmCadastroFrmPgto_Load(object sender, EventArgs e)
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