using Projeto.Controller;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Projeto.Utils;


namespace Projeto.Views
{
    public partial class frmCadastroCidade : Projeto.frmBase
    {
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

        public void CarregarCidade(int id, string nomeCidade, string nomeEstado, int estadoId, bool ativo, DateTime? dataCadastro, DateTime? dataAlteracao)
        {
            modoEdicao = true;

            txtCodigo.Text = id.ToString();
            txtNome.Text = nomeCidade;
            txtEstado.Text = nomeEstado;
            estadoSelecionadoId = estadoId;
            chkInativo.Checked = !ativo;

            lblDataCriacao.Text = dataCadastro.HasValue
                ? $"Criado em: {dataCadastro.Value:dd/MM/yyyy HH:mm}"
                : "Criado em: -";

            lblDataModificacao.Text = dataAlteracao.HasValue
                ? $"Modificado em: {dataAlteracao.Value:dd/MM/yyyy HH:mm}"
                : "Modificado em: -";
        }


        private void frmCadastroCidade_Load(object sender, EventArgs e)
        {
            if (modoExclusao)
            {
                btnSalvar.Text = "Deletar";
                txtNome.Enabled = false;
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
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (modoExclusao)
            {
                var confirmacao = MessageBox.Show("Tem certeza que deseja excluir esta Cidade?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmacao == DialogResult.Yes)
                {
                    try
                    {
                        int id = int.Parse(txtCodigo.Text);
                        controller.Excluir(id);
                        MessageBox.Show("Cidade excluída com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    int idEstado = estadoSelecionadoId;
                    bool status = !chkInativo.Checked;

                    var estadoSelecionado = estadoController.BuscarPorId(estadoSelecionadoId);

                    if (estadoSelecionado == null)
                    {
                        MessageBox.Show("Estado selecionado não encontrado. Selecione novamente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    List<Cidade> cidades = controller.ListarCidade();

                    if (Validador.VerificarDuplicidade(cidades, item =>
                        item.NomeCidade.Trim().Equals(nome, StringComparison.OrdinalIgnoreCase) &&
                        item.EstadoId == estadoSelecionadoId &&
                        item.Id != id, "Já existe uma cidade com este nome cadastrada para este estado."))
                    {
                        txtNome.Focus();
                        return;
                    }


                    DateTime dataCriacao = id == 0
                        ? DateTime.Now
                        : DateTime.Parse(lblDataCriacao.Text.Replace("Criado em: ", ""));

                    DateTime dataModificacao = DateTime.Now;

                    Cidade cidade = new Cidade
                    {
                        Id = id,
                        NomeCidade = nome,
                        EstadoId = idEstado,
                        Ativo = status,
                        DataCadastro = dataCriacao,
                        DataAlteracao = dataModificacao
                    };

                    controller.Salvar(cidade);
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
            frmConsultaEstado consultaEstado = new frmConsultaEstado();
            consultaEstado.ModoSelecao = true;

            var resultado = consultaEstado.ShowDialog();

            if (resultado == DialogResult.OK && consultaEstado.EstadoSelecionado != null)
            {
                txtEstado.Text = consultaEstado.EstadoSelecionado.NomeEstado;
                estadoSelecionadoId = consultaEstado.EstadoSelecionado.Id;
            }
        }

    }
}
