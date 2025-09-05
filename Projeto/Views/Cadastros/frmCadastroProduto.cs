using Projeto.Controller;
using Projeto.DAO;
using Projeto.Models;
using Projeto.Utils;
using Projeto.Views.Consultas;
using System;
using System.Windows.Forms;

namespace Projeto.Views.Cadastros
{
    public partial class frmCadastroProduto : Projeto.frmBase
    {
        Produto oProduto;
        private frmConsultaGrupo oFrmConsultaGrupo;
        private frmConsultaMarca oFrmConsultaMarca;

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

        public override void ConhecaObj(object obj, object ctrl)
        {
            if (obj != null)
            {
                oProduto = (Produto)obj;
            }
            if (ctrl != null)
            {
                controller = (ProdutoController)ctrl;
            }
        }

        public void setFrmConsultaGrupo(object obj)
        {
            if (obj != null)
            {
                oFrmConsultaGrupo = (frmConsultaGrupo)obj;
            }
        }

        public void setFrmConsultaMarca(object obj)
        {
            if (obj != null)
            {
                oFrmConsultaMarca = (frmConsultaMarca)obj;
            }
        }

        public override void CarregaTxt()
        {
            txtCodigo.Text = oProduto.Id.ToString();
            txtNome.Text = oProduto.NomeProduto;
            txtDescricao.Text = oProduto.Descricao;
            txtPreco.Text = oProduto.Preco.ToString("F2");
            txtEstoque.Text = oProduto.Estoque.ToString();
            txtMarca.Text = oProduto.NomeMarca;
            marcaSelecionadoId = oProduto.IdMarca ?? -1; 
            txtGrupo.Text = oProduto.NomeGrupo;
            grupoSelecionadoId = oProduto.GrupoId ?? -1; 
            chkInativo.Checked = !oProduto.Ativo;
            lblDataCriacao.Text = oProduto.DataCadastro.HasValue ? $"Criado em: {oProduto.DataCadastro.Value:dd/MM/yyyy HH:mm}" : "Criado em: -";
            lblDataModificacao.Text = oProduto.DataAlteracao.HasValue ? $"Modificado em: {oProduto.DataAlteracao.Value:dd/MM/yyyy HH:mm}" : "Modificado em: -";
        }

        public override void BloquearTxt()
        {
            txtNome.Enabled = false;
            txtDescricao.Enabled = false;
            txtPreco.Enabled = false;
            txtEstoque.Enabled = false;
            btnMarca.Enabled = false;
            btnBuscarGrupo.Enabled = false;
            chkInativo.Enabled = false;
        }

        public override void DesbloquearTxt()
        {
            txtNome.Enabled = true;
            txtDescricao.Enabled = true;
            txtPreco.Enabled = true;
            txtEstoque.Enabled = true;
            btnMarca.Enabled = true;
            btnBuscarGrupo.Enabled = true;
            chkInativo.Enabled = true;
        }

        public override void LimparTxt()
        {
            txtCodigo.Text = "0";
            txtNome.Clear();
            txtDescricao.Clear();
            txtPreco.Clear();
            txtEstoque.Clear();
            txtMarca.Clear();
            txtGrupo.Clear();
            marcaSelecionadoId = -1;
            grupoSelecionadoId = -1;
            chkInativo.Checked = false;

            DateTime agora = DateTime.Now;
            lblDataCriacao.Text = $"Criado em: {agora:dd/MM/yyyy HH:mm}";
            lblDataModificacao.Text = $"Modificado em: {agora:dd/MM/yyyy HH:mm}";
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
                if (!Validador.ValidarIdSelecionado(grupoSelecionadoId, "Selecione um grupo.")) return;
                if (!Validador.ValidarIdSelecionado(marcaSelecionadoId, "Selecione uma marca.")) return;
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

                    if (Validador.VerificarDuplicidade(produtos, p =>
                        p.NomeProduto.Trim().Equals(nomeProduto, StringComparison.OrdinalIgnoreCase) &&
                        p.GrupoId == grupoSelecionadoId &&
                        p.IdMarca == marcaSelecionadoId &&
                        p.Id != id, "Já existe um produto com este nome cadastrado para este grupo e marca."))
                    {
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

        private void btnBuscarGrupo_Click(object sender, EventArgs e)
        {
            oFrmConsultaGrupo.ModoSelecao = true;
            if (oFrmConsultaGrupo.ShowDialog() == DialogResult.OK && oFrmConsultaGrupo.GrupoSelecionado != null)
            {
                txtGrupo.Text = oFrmConsultaGrupo.GrupoSelecionado.NomeGrupo;
                grupoSelecionadoId = oFrmConsultaGrupo.GrupoSelecionado.Id;
            }

            /*
            frmConsultaGrupo consultaGrupo = new frmConsultaGrupo();
            consultaGrupo.ModoSelecao = true;

            if (consultaGrupo.ShowDialog() == DialogResult.OK && consultaGrupo.GrupoSelecionado != null)
            {
                txtGrupo.Text = consultaGrupo.GrupoSelecionado.NomeGrupo;
                grupoSelecionadoId = consultaGrupo.GrupoSelecionado.Id;
            }
            */
        }

        private void btnMarca_Click(object sender, EventArgs e)
        {
            oFrmConsultaMarca.ModoSelecao = true;
            if (oFrmConsultaMarca.ShowDialog() == DialogResult.OK && oFrmConsultaMarca.MarcaSelecionado != null)
            {
                txtMarca.Text = oFrmConsultaMarca.MarcaSelecionado.NomeMarca;
                marcaSelecionadoId = oFrmConsultaMarca.MarcaSelecionado.Id;
            }

            /*
            frmConsultaMarca consultaMarca = new frmConsultaMarca();
            consultaMarca.ModoSelecao = true;

            if (consultaMarca.ShowDialog() == DialogResult.OK && consultaMarca.MarcaSelecionado != null)
            {
                txtMarca.Text = consultaMarca.MarcaSelecionado.NomeMarca;
                marcaSelecionadoId = consultaMarca.MarcaSelecionado.Id;
            }
            */
        }
    }
}
