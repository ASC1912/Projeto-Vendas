using Projeto.Controller;
using Projeto.Models;
using Projeto.Utils;
using Projeto.Views.Consultas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Projeto.Views.Cadastros
{
    public partial class frmCadastroProduto : Projeto.frmBase
    {
        private ProdutoController controller = new ProdutoController();
        private GrupoController grupoController = new GrupoController();
        private MarcaController marcaController = new MarcaController();
        public bool modoEdicao = false;
        private int marcaSelecionadoId = -1;
        private int grupoSelecionadoId = -1;

        public frmCadastroProduto() : base()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
        }

        public void CarregarProduto(int id, string nome,  string descricao, decimal preco, int estoque, int idMarca, string nomeMarca, int idGrupo, string nomeGrupo, bool status, DateTime? dataCriacao, DateTime? dataModificacao)
        {
            modoEdicao = true;

            txtCodigo.Text = id.ToString();
            txtNome.Text = nome;
            txtDescricao.Text = descricao;
            txtPreco.Text = preco.ToString("F2");
            txtEstoque.Text = estoque.ToString();
            marcaSelecionadoId = idMarca;
            txtMarca.Text = nomeMarca;
            grupoSelecionadoId = idGrupo;
            txtGrupo.Text = nomeGrupo;
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
            if (!Validador.CampoObrigatorio(txtNome, "O nome é obrigatório.")) return;
            if (!Validador.ValidarNumerico(txtEstoque, "O estoque deve ser um número válido.")) return;


            try
            {
                int id = string.IsNullOrEmpty(txtCodigo.Text) ? 0 : int.Parse(txtCodigo.Text);
                string nome = txtNome.Text;
                string descricao = txtDescricao.Text;
                decimal preco = string.IsNullOrWhiteSpace(txtPreco.Text) ? 0 : Convert.ToDecimal(txtPreco.Text);
                int estoque = string.IsNullOrWhiteSpace(txtEstoque.Text) ? 0 : Convert.ToInt32(txtEstoque.Text);
                bool status = !chkInativo.Checked;

                DateTime dataCriacao = id == 0
                    ? DateTime.Now
                    : DateTime.Parse(lblDataCriacao.Text.Replace("Criado em: ", ""));

                DateTime dataModificacao = DateTime.Now;

                Produto produto = new Produto
                {
                    Id = id,
                    Nome = nome,
                    Descricao = descricao,
                    Preco = preco,
                    Estoque = estoque,
                    IdMarca = marcaSelecionadoId,
                    IdGrupo = grupoSelecionadoId,
                    Status = status,
                    DataCriacao = dataCriacao,
                    DataModificacao = dataModificacao
                };

                ProdutoController controller = new ProdutoController();
                controller.Salvar(produto);

                MessageBox.Show("Produto salvo com sucesso!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private void frmCadastroProduto_Load(object sender, EventArgs e)
        {
            lblDataCriacao.Visible = modoEdicao;
            lblDataModificacao.Visible = modoEdicao;
        }

        private void btnBuscarGrupo_Click(object sender, EventArgs e)
        {
            frmConsultaGrupo consultaGrupo = new frmConsultaGrupo();
            consultaGrupo.ModoSelecao = true;

            var resultado = consultaGrupo.ShowDialog();

            if (resultado == DialogResult.OK && consultaGrupo.GrupoSelecionado != null)
            {
                txtGrupo.Text = consultaGrupo.GrupoSelecionado.Nome;
                grupoSelecionadoId = consultaGrupo.GrupoSelecionado.Id;
            }
        }

        private void btnMarca_Click(object sender, EventArgs e)
        {
            frmConsultaMarca consultaMarca = new frmConsultaMarca();
            consultaMarca.ModoSelecao = true;

            var resultado = consultaMarca.ShowDialog();

            if (resultado == DialogResult.OK && consultaMarca.MarcaSelecionado != null)
            {
                txtMarca.Text = consultaMarca.MarcaSelecionado.Nome;
                marcaSelecionadoId = consultaMarca.MarcaSelecionado.Id;
            }
        }
    }
}
