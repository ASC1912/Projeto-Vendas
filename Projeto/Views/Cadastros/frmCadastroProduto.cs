﻿using Projeto.Controller;
using Projeto.Models;
using Projeto.Utils;
using Projeto.Views.Consultas;
using System;
using System.Windows.Forms;

namespace Projeto.Views.Cadastros
{
    public partial class frmCadastroProduto : Projeto.frmBase
    {
        private ProdutoController controller = new ProdutoController();
        private GrupoController grupoController = new GrupoController();
        private MarcaController marcaController = new MarcaController();
        public bool modoEdicao = false;
        public bool modoExclusao = false;
        private int marcaSelecionadoId = -1;
        private int grupoSelecionadoId = -1;

        public frmCadastroProduto() : base()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
        }

        public void CarregarProduto(int id, string nomeProduto, string descricao, decimal preco, int estoque, int idMarca, string nomeMarca, int idGrupo, string nomeGrupo, bool ativo, DateTime? dataCadastro, DateTime? dataAlteracao)
        {
            modoEdicao = true;

            txtCodigo.Text = id.ToString();
            txtNome.Text = nomeProduto;
            txtDescricao.Text = descricao;
            txtPreco.Text = preco.ToString("F2");
            txtEstoque.Text = estoque.ToString();
            marcaSelecionadoId = idMarca;
            txtMarca.Text = nomeMarca;
            grupoSelecionadoId = idGrupo;
            txtGrupo.Text = nomeGrupo;
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
                var confirmacao = MessageBox.Show("Tem certeza que deseja excluir este produto?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmacao == DialogResult.Yes)
                {
                    try
                    {
                        int id = int.Parse(txtCodigo.Text);
                        controller.Excluir(id);
                        MessageBox.Show("Produto excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (!Validador.CampoObrigatorio(txtPreco, "O preço é obrigatório.")) return;
                if (!Validador.CampoObrigatorio(txtMarca, "A marca é obrigatória.")) return;
                if (!Validador.CampoObrigatorio(txtGrupo, "O grupo é obrigatório.")) return;
                if (!Validador.ValidarNumerico(txtEstoque, "O estoque deve ser um número válido.")) return;

                try
                {
                    int id = string.IsNullOrEmpty(txtCodigo.Text) ? 0 : int.Parse(txtCodigo.Text);
                    string nomeProduto = txtNome.Text;
                    string descricao = txtDescricao.Text;
                    decimal preco = string.IsNullOrWhiteSpace(txtPreco.Text) ? 0 : Convert.ToDecimal(txtPreco.Text);
                    int estoque = string.IsNullOrWhiteSpace(txtEstoque.Text) ? 0 : Convert.ToInt32(txtEstoque.Text);
                    bool ativo = !chkInativo.Checked;

                    var produtos = controller.ListarProdutos();
                    bool existeDuplicado = produtos.Exists(p =>
                        p.NomeProduto.Trim().Equals(nomeProduto, StringComparison.OrdinalIgnoreCase) &&
                        p.GrupoId == grupoSelecionadoId &&
                        p.IdMarca == marcaSelecionadoId &&
                        p.Id != id);

                    if (existeDuplicado)
                    {
                        MessageBox.Show("Já existe um produto com este nome cadastrado para este grupo e marca.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtNome.Focus();
                        return;
                    }

                    DateTime dataCadastro = id == 0
                        ? DateTime.Now
                        : DateTime.Parse(lblDataCriacao.Text.Replace("Criado em: ", ""));

                    DateTime dataAlteracao = DateTime.Now;

                    Produto produto = new Produto
                    {
                        Id = id,
                        NomeProduto = nomeProduto,
                        Descricao = descricao,
                        Preco = preco,
                        Estoque = estoque,
                        IdMarca = marcaSelecionadoId,
                        GrupoId = grupoSelecionadoId,
                        Ativo = ativo,
                        DataCadastro = dataCadastro,
                        DataAlteracao = dataAlteracao
                    };

                    controller.Salvar(produto);

                    MessageBox.Show("Produto salvo com sucesso!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                }
            }
        }

        private void frmCadastroProduto_Load(object sender, EventArgs e)
        {
            if (modoExclusao)
            {
                btnSalvar.Text = "Deletar";
                txtNome.Enabled = false;
                txtDescricao.Enabled = false;
                txtPreco.Enabled = false;
                txtEstoque.Enabled = false;
                btnMarca.Enabled = false;
                btnBuscarGrupo.Enabled = false;
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

        private void btnBuscarGrupo_Click(object sender, EventArgs e)
        {
            frmConsultaGrupo consultaGrupo = new frmConsultaGrupo();
            consultaGrupo.ModoSelecao = true;

            if (consultaGrupo.ShowDialog() == DialogResult.OK && consultaGrupo.GrupoSelecionado != null)
            {
                txtGrupo.Text = consultaGrupo.GrupoSelecionado.NomeGrupo;
                grupoSelecionadoId = consultaGrupo.GrupoSelecionado.Id;
            }
        }

        private void btnMarca_Click(object sender, EventArgs e)
        {
            frmConsultaMarca consultaMarca = new frmConsultaMarca();
            consultaMarca.ModoSelecao = true;

            if (consultaMarca.ShowDialog() == DialogResult.OK && consultaMarca.MarcaSelecionado != null)
            {
                txtMarca.Text = consultaMarca.MarcaSelecionado.NomeMarca;
                marcaSelecionadoId = consultaMarca.MarcaSelecionado.Id;
            }
        }
    }
}
