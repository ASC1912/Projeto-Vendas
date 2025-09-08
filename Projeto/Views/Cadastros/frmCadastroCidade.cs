using Projeto.Controller;
using Projeto.DAO;
using Projeto.Models;
using Projeto.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto.Views
{
    public partial class frmCadastroCidade : Projeto.frmBase
    {
        Cidade aCidade;
        frmConsultaEstado oFrmConsultaEstado;
        private EstadoController estadoController = new EstadoController();
        private CidadeController controller = new CidadeController();
        private int estadoSelecionadoId = -1;
        public bool modoEdicao = false;
        public bool modoExclusao = false;

        public frmCadastroCidade() : base()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
        }

        public override void ConhecaObj(object obj, object ctrl)
        {
            if (obj != null)
            {
                aCidade = (Cidade)obj;
            }
            if (ctrl != null)
            {
                controller = (CidadeController)ctrl;
            }
        }

        public void setFrmConsultaEstado(object obj)
        {
            if (obj != null)
            {
                oFrmConsultaEstado = (frmConsultaEstado)obj;
            }
        }

        public override void CarregaTxt()
        {
            txtCodigo.Text = aCidade.Id.ToString();
            txtNome.Text = aCidade.NomeCidade;
            txtEstado.Text = aCidade.EstadoNome;
            estadoSelecionadoId = aCidade.EstadoId;
            txtDDD.Text = aCidade.DDD;
            chkInativo.Checked = !aCidade.Ativo;
            lblDataCriacao.Text = aCidade.DataCadastro.HasValue ? $"Criado em: {aCidade.DataCadastro.Value:dd/MM/yyyy HH:mm}" : "Criado em: -";
            lblDataModificacao.Text = aCidade.DataAlteracao.HasValue ? $"Modificado em: {aCidade.DataAlteracao.Value:dd/MM/yyyy HH:mm}" : "Modificado em: -";
        }

        public override void BloquearTxt()
        {
            txtNome.Enabled = false;
            txtDDD.Enabled = false;
            btnBuscar.Enabled = false;
            chkInativo.Enabled = false;
        }

        public override void DesbloquearTxt()
        {
            txtNome.Enabled = true;
            txtDDD.Enabled = true;
            btnBuscar.Enabled = true;
            chkInativo.Enabled = true;
        }

        public override void LimparTxt()
        {
            txtCodigo.Text = "0";
            txtNome.Clear();
            txtEstado.Clear();
            txtDDD.Clear();
            estadoSelecionadoId = -1;
            chkInativo.Checked = false;

            DateTime agora = DateTime.Now;
            lblDataCriacao.Text = $"Criado em: {agora:dd/MM/yyyy HH:mm}";
            lblDataModificacao.Text = $"Modificado em: {agora:dd/MM/yyyy HH:mm}";
        }

        private void frmCadastroCidade_Load(object sender, EventArgs e)
        {
        }

        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            if (modoExclusao)
            {
                var confirmacao = MessageBox.Show("Tem certeza que deseja excluir esta Cidade?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmacao == DialogResult.Yes)
                {
                    try
                    {
                        int id = int.Parse(txtCodigo.Text);
                        await controller.Excluir(id);
                        MessageBox.Show("Cidade excluída com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("Cannot delete or update a parent row"))
                        {
                            MessageBox.Show("Não é possível excluir este item, pois existem registros vinculados a ele.", "Erro ao excluir", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show($"Erro ao excluir: {ex.Message}", "Erro ao excluir", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                if (!Validador.CampoObrigatorio(txtNome, "O nome da cidade é obrigatório.")) return;
                if (!Validador.ValidarIdSelecionado(estadoSelecionadoId, "Selecione um estado!")) return;

                try
                {
                    int id = string.IsNullOrWhiteSpace(txtCodigo.Text) ? 0 : Convert.ToInt32(txtCodigo.Text);
                    string nome = txtNome.Text;

                    List<Cidade> cidades = await controller.ListarCidade();

                    if (Validador.VerificarDuplicidade(cidades, item =>
                        item.NomeCidade.Trim().Equals(nome, StringComparison.OrdinalIgnoreCase) &&
                        item.EstadoId == estadoSelecionadoId &&
                        item.Id != id, "Já existe uma cidade com este nome cadastrada para este estado."))
                    {
                        txtNome.Focus();
                        return;
                    }

                    DateTime dataCriacao = id == 0 ? DateTime.Now : DateTime.Parse(lblDataCriacao.Text.Replace("Criado em: ", ""));
                    DateTime dataModificacao = DateTime.Now;

                    Cidade cidade = new Cidade
                    {
                        Id = id,
                        NomeCidade = nome,
                        DDD = txtDDD.Text, 
                        Ativo = !chkInativo.Checked,
                        DataCadastro = dataCriacao,
                        DataAlteracao = dataModificacao,
                        EstadoId = estadoSelecionadoId,
                        oEstado = new Estado { Id = estadoSelecionadoId }
                    };

                    await controller.Salvar(cidade);
                    MessageBox.Show("Cidade salva com sucesso!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao salvar cidade: " + ex.Message);
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            oFrmConsultaEstado.ModoSelecao = true;
            var resultado = oFrmConsultaEstado.ShowDialog();

            if (resultado == DialogResult.OK && oFrmConsultaEstado.EstadoSelecionado != null)
            {
                txtEstado.Text = oFrmConsultaEstado.EstadoSelecionado.NomeEstado;
                estadoSelecionadoId = oFrmConsultaEstado.EstadoSelecionado.Id;
            }
        }
    }
}