using Projeto.Controller;
using Projeto.Models;
using System;
using System.Collections.Generic; 
using System.Windows.Forms;
using Projeto.Utils;

namespace Projeto.Views
{
    public partial class frmCadastroEstado : Projeto.frmBase
    {

        private PaisController paisController = new PaisController();
        private EstadoController controller = new EstadoController();
        private int paisSelecionadoId = -1;
        public bool modoEdicao = false;
        public bool modoExclusao = false;

        public frmCadastroEstado() : base()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
        }

        public void CarregarEstado(int id, string nomeEstado, string uf, string nomePais, int paisId, bool ativo, DateTime? dataCadastro, DateTime? dataAlteracao)
        {
            modoEdicao = true;
            txtCodigo.Text = id.ToString();
            txtNome.Text = nomeEstado;
            txtUF.Text = uf;
            txtPais.Text = nomePais;
            paisSelecionadoId = paisId;
            chkInativo.Checked = !ativo;
            lblDataCriacao.Text = dataCadastro.HasValue ? $"Criado em: {dataCadastro.Value:dd/MM/yyyy HH:mm}" : "Criado em: -";
            lblDataModificacao.Text = dataAlteracao.HasValue ? $"Modificado em: {dataAlteracao.Value:dd/MM/yyyy HH:mm}" : "Modificado em: -";
        }

        private void frmCadastroEstado_Load(object sender, EventArgs e)
        {
            if (modoExclusao)
            {
                btnSalvar.Text = "Deletar";
                txtNome.Enabled = false;
                txtUF.Enabled = false;
                btnBuscar.Enabled = false;
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

        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            if (modoExclusao)
            {
                var confirmacao = MessageBox.Show("Tem certeza que deseja excluir este Estado?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmacao == DialogResult.Yes)
                {
                    try
                    {
                        int id = int.Parse(txtCodigo.Text);
                        await controller.Excluir(id);
                        MessageBox.Show("Estado excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (!Validador.CampoObrigatorio(txtNome, "O nome do estado é obrigatório.")) return;
                if (!Validador.ValidarIdSelecionado(paisSelecionadoId, "Selecione um país!")) return;

                try
                {
                    int id = string.IsNullOrWhiteSpace(txtCodigo.Text) ? 0 : Convert.ToInt32(txtCodigo.Text);
                    string nome = txtNome.Text;

                    List<Estado> estados = await controller.ListarEstado();

                    if (Validador.VerificarDuplicidade(estados, item =>
                        item.NomeEstado.Trim().Equals(nome, StringComparison.OrdinalIgnoreCase)
                        && item.PaisId == paisSelecionadoId
                        && item.Id != id, "Já existe um estado com este nome cadastrado para este país."))
                    {
                        txtNome.Focus();
                        return;
                    }

                    string uf = txtUF.Text.Trim().ToUpper();
                    if (string.IsNullOrWhiteSpace(uf) || uf.Length != 2)
                    {
                        MessageBox.Show("Informe a sigla UF com 2 letras.");
                        return;
                    }

                    DateTime dataCriacao = id == 0 ? DateTime.Now : DateTime.Parse(lblDataCriacao.Text.Replace("Criado em: ", ""));
                    DateTime dataModificacao = DateTime.Now;

                    Estado estado = new Estado
                    {
                        Id = id,
                        NomeEstado = nome,
                        UF = uf,
                        Ativo = !chkInativo.Checked,
                        DataCadastro = dataCriacao,
                        DataAlteracao = dataModificacao,

                        PaisId = paisSelecionadoId,

                        oPais = new Pais { Id = paisSelecionadoId }
                    };

                    await controller.Salvar(estado);
                    MessageBox.Show("Estado salvo com sucesso!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao salvar estado: " + ex.Message);
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            frmConsultaPais consultaPais = new frmConsultaPais();
            consultaPais.ModoSelecao = true;

            var resultado = consultaPais.ShowDialog();

            if (resultado == DialogResult.OK && consultaPais.PaisSelecionado != null)
            {
                txtPais.Text = consultaPais.PaisSelecionado.NomePais;
                paisSelecionadoId = consultaPais.PaisSelecionado.Id;
            }
        }
    }
}