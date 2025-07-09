using Projeto.Controller;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Projeto.Utils;


namespace Projeto.Views
{
    public partial class frmCadastroPais : Projeto.frmBase
    {
        PaisController controller = new PaisController();
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

            lblDataCriacao.Text = dataCriacao.HasValue
                ? $"Criado em: {dataCriacao.Value:dd/MM/yyyy HH:mm}"
                : "Criado em: -";

            lblDataModificacao.Text = dataModificacao.HasValue
                ? $"Modificado em: {dataModificacao.Value:dd/MM/yyyy HH:mm}"
                : "Modificado em: -";
        }


        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (modoExclusao)
            {
                var confirmacao = MessageBox.Show("Tem certeza que deseja excluir este País?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmacao == DialogResult.Yes)
                {
                    try
                    {
                        int id = int.Parse(txtCodigo.Text);
                        controller.Excluir(id);
                        MessageBox.Show("País excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            MessageBox.Show(
                                $"Erro ao excluir: {ex.Message}",
                                "Erro ao excluir",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );
                        }
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

                    List<Pais> paises = controller.ListarPais();
                    bool existeDuplicado = paises.Exists(p =>
                        string.Equals(p.NomePais.Trim(), nome, StringComparison.OrdinalIgnoreCase)
                        && p.Id != id);

                    if (existeDuplicado)
                    {
                        MessageBox.Show("Este item já está cadastrado", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtNome.Focus();
                        return;
                    }

                    DateTime dataCriacao = id == 0
                        ? DateTime.Now
                        : DateTime.Parse(lblDataCriacao.Text.Replace("Criado em: ", ""));

                    DateTime dataModificacao = DateTime.Now;

                    Pais pais = new Pais
                    {
                        Id = id,
                        NomePais = nome,
                        Ativo = status,
                        DataCadastro = dataCriacao,
                        DataAlteracao = dataModificacao
                    };

                    controller.Salvar(pais);

                    MessageBox.Show("País salvo com sucesso!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
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
