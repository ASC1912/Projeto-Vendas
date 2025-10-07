using Projeto.Controller;
using Projeto.Models;
using Projeto.Utils;
using Projeto.Views.Consultas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto.Views.Cadastros
{
    public partial class frmCadastroProduto : frmBase
    {
        // Declaração de Models e Controllers
        Produto oProduto;
        private ProdutoController controller = new ProdutoController();
        private ProdutoFornecedorController pfController = new ProdutoFornecedorController();
        private FornecedorController fornController = new FornecedorController();

        // Formulários de Consulta
        private frmConsultaGrupo oFrmConsultaGrupo;
        private frmConsultaMarca oFrmConsultaMarca;
        private frmConsultaUnidadeMedida oFrmConsultaUnidadeMedida;
        private frmConsultaFornecedor oFrmConsultaFornecedor;

        // Lista temporária para gerenciar os fornecedores na tela
        private List<ProdutoFornecedor> listaProdutoFornecedor = new List<ProdutoFornecedor>();

        // IDs selecionados
        private int marcaSelecionadoId = -1;
        private int grupoSelecionadoId = -1;
        private int unidadeSelecionadaId = -1;

        public bool modoEdicao = false;
        public bool modoExclusao = false;

        public frmCadastroProduto() : base()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;

            // Configurações dos campos de texto
            txtUnidade.ReadOnly = true;
            txtMarca.ReadOnly = true;
            txtGrupo.ReadOnly = true;
            txtFornecedor.ReadOnly = true;
            txtPrecoVenda.ReadOnly = true; // Renomeado de txtValorVenda
            txtValorCompraAnterior.ReadOnly = true;

            // Adiciona os eventos para o cálculo automático de preço
            txtPrecoCusto.TextChanged += CalcularPrecoVenda;
            txtPorcentagemLucro.TextChanged += CalcularPrecoVenda;
            txtPrecoVenda.Leave += CalcularPorcentagemLucro;
        }

        #region Conexões com outros formulários
        public override void ConhecaObj(object obj, object ctrl)
        {
            if (obj != null) oProduto = (Produto)obj;
            if (ctrl != null) controller = (ProdutoController)ctrl;
        }

        public void setFrmConsultaGrupo(object obj)
        {
            if (obj != null) oFrmConsultaGrupo = (frmConsultaGrupo)obj;
        }

        public void setFrmConsultaMarca(object obj)
        {
            if (obj != null) oFrmConsultaMarca = (frmConsultaMarca)obj;
        }

        public void setFrmConsultaUnidadeMedida(object obj)
        {
            if (obj != null) oFrmConsultaUnidadeMedida = (frmConsultaUnidadeMedida)obj;
        }

        public void setFrmConsultaFornecedor(object obj)
        {
            if (obj != null) oFrmConsultaFornecedor = (frmConsultaFornecedor)obj;
        }
        #endregion

        #region Manipulação da Interface (UI)
        public async override void CarregaTxt()
        {
            txtCodigo.Text = oProduto.Id.ToString();
            txtNome.Text = oProduto.NomeProduto;
            txtEstoque.Text = oProduto.Estoque.ToString();
            txtPrecoCusto.Text = oProduto.PrecoCusto.ToString("F2");
            txtPrecoVenda.Text = oProduto.PrecoVenda.ToString("F2");
            txtPorcentagemLucro.Text = oProduto.PorcentagemLucro.ToString("F2");

            txtIdUnidade.Text = oProduto.UnidadeMedidaId?.ToString();
            txtUnidade.Text = oProduto.NomeUnidadeMedida;
            unidadeSelecionadaId = oProduto.UnidadeMedidaId ?? -1;

            txtIdMarca.Text = oProduto.IdMarca?.ToString();
            txtMarca.Text = oProduto.NomeMarca;
            marcaSelecionadoId = oProduto.IdMarca ?? -1;

            txtIdGrupo.Text = oProduto.GrupoId?.ToString();
            txtGrupo.Text = oProduto.NomeGrupo;
            grupoSelecionadoId = oProduto.GrupoId ?? -1;

            chkInativo.Checked = !oProduto.Ativo;
            lblDataCriacao.Text = oProduto.DataCadastro.HasValue ? $"Criado em: {oProduto.DataCadastro.Value:dd/MM/yyyy HH:mm}" : "Criado em: -";
            lblDataModificacao.Text = oProduto.DataAlteracao.HasValue ? $"Modificado em: {oProduto.DataAlteracao.Value:dd/MM/yyyy HH:mm}" : "Modificado em: -";

            if (oProduto.Id > 0)
            {
                listaProdutoFornecedor = await pfController.ListarPorProduto(oProduto.Id);
                await CarregarFornecedoresNaListView();
            }
        }

        public override void LimparTxt()
        {
            txtCodigo.Text = "0";
            txtNome.Clear();
            txtUnidade.Clear();
            txtIdUnidade.Clear();
            txtPrecoCusto.Text = "0,00";
            txtPrecoVenda.Text = "0,00";
            txtPorcentagemLucro.Text = "0";
            txtEstoque.Clear();
            txtMarca.Clear();
            txtIdMarca.Clear();
            txtGrupo.Clear();
            txtIdGrupo.Clear();
            txtFornecedor.Clear();
            txtCodFornecedor.Clear();
            listVFornecedores.Items.Clear();
            listaProdutoFornecedor.Clear();

            marcaSelecionadoId = -1;
            grupoSelecionadoId = -1;
            unidadeSelecionadaId = -1;

            chkInativo.Checked = false;
            DateTime agora = DateTime.Now;
            lblDataCriacao.Text = $"Criado em: {agora:dd/MM/yyyy HH:mm}";
            lblDataModificacao.Text = $"Modificado em: {agora:dd/MM/yyyy HH:mm}";
        }
        #endregion

        #region Eventos de Clique dos Botões
        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            if (modoExclusao)
            {
                var confirmacao = MessageBox.Show("Tem certeza que deseja excluir este produto?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmacao == DialogResult.Yes)
                {
                    try
                    {
                        // Antes de excluir o produto, removemos os vínculos
                        var fornecedoresVinculados = await pfController.ListarPorProduto(int.Parse(txtCodigo.Text));
                        foreach (var vinculo in fornecedoresVinculados)
                        {
                            await pfController.Excluir(vinculo);
                        }

                        // Agora excluímos o produto
                        await controller.Excluir(int.Parse(txtCodigo.Text));

                        MessageBox.Show("Produto excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao excluir: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                return;
            }

            if (!Validador.CampoObrigatorio(txtNome, "O nome do produto é obrigatório.")) return;
            if (!Validador.CampoObrigatorio(txtPrecoCusto, "O preço de custo é obrigatório.")) return;
            if (!Validador.ValidarIdSelecionado(unidadeSelecionadaId, "Selecione uma Unidade de Medida.")) return;
            if (!Validador.ValidarIdSelecionado(grupoSelecionadoId, "Selecione um Grupo.")) return;
            if (!Validador.ValidarNumerico(txtEstoque, "O estoque deve ser um número válido.")) return;

            try
            {
                Produto produto = new Produto
                {
                    Id = string.IsNullOrEmpty(txtCodigo.Text) ? 0 : int.Parse(txtCodigo.Text),
                    NomeProduto = txtNome.Text,
                    PrecoCusto = Convert.ToDecimal(txtPrecoCusto.Text),
                    PrecoVenda = Convert.ToDecimal(txtPrecoVenda.Text),
                    PorcentagemLucro = Convert.ToDecimal(txtPorcentagemLucro.Text),
                    Estoque = string.IsNullOrWhiteSpace(txtEstoque.Text) ? 0 : Convert.ToInt32(txtEstoque.Text),
                    Ativo = !chkInativo.Checked,
                    IdMarca = marcaSelecionadoId > 0 ? (int?)marcaSelecionadoId : null,
                    GrupoId = grupoSelecionadoId,
                    UnidadeMedidaId = unidadeSelecionadaId,
                };

                await controller.Salvar(produto);

                // AVISO: A lógica de sincronizar fornecedores para um NOVO produto precisaria
                // de uma segunda busca para obter o ID. Deixaremos isso para um próximo passo.
                // Por enquanto, a sincronização funcionará apenas na EDIÇÃO.
                if (produto.Id > 0)
                {
                    var fornecedoresNoBanco = await pfController.ListarPorProduto(produto.Id);

                    foreach (var relacaoBanco in fornecedoresNoBanco)
                    {
                        if (!listaProdutoFornecedor.Any(itemTela => itemTela.FornecedorId == relacaoBanco.FornecedorId))
                        {
                            await pfController.Excluir(relacaoBanco);
                        }
                    }

                    foreach (var relacaoTela in listaProdutoFornecedor)
                    {
                        relacaoTela.ProdutoId = produto.Id;
                        await pfController.Salvar(relacaoTela);
                    }
                }

                MessageBox.Show("Produto salvo com sucesso!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar o produto: " + ex.Message);
            }
        }

        private void btnUnidade_Click(object sender, EventArgs e)
        {
            oFrmConsultaUnidadeMedida.ModoSelecao = true;
            if (oFrmConsultaUnidadeMedida.ShowDialog() == DialogResult.OK && oFrmConsultaUnidadeMedida.UnidadeSelecionada != null)
            {
                var unidade = oFrmConsultaUnidadeMedida.UnidadeSelecionada;
                txtUnidade.Text = unidade.NomeUnidade;
                txtIdUnidade.Text = unidade.Id.ToString();
                unidadeSelecionadaId = unidade.Id;
            }
        }

        private void btnBuscarGrupo_Click(object sender, EventArgs e)
        {
            oFrmConsultaGrupo.ModoSelecao = true;
            if (oFrmConsultaGrupo.ShowDialog() == DialogResult.OK && oFrmConsultaGrupo.GrupoSelecionado != null)
            {
                var grupo = oFrmConsultaGrupo.GrupoSelecionado;
                txtGrupo.Text = grupo.NomeGrupo;
                txtIdGrupo.Text = grupo.Id.ToString();
                grupoSelecionadoId = grupo.Id;
            }
        }

        private void btnMarca_Click(object sender, EventArgs e)
        {
            oFrmConsultaMarca.ModoSelecao = true;
            if (oFrmConsultaMarca.ShowDialog() == DialogResult.OK && oFrmConsultaMarca.MarcaSelecionado != null)
            {
                var marca = oFrmConsultaMarca.MarcaSelecionado;
                txtMarca.Text = marca.NomeMarca;
                txtIdMarca.Text = marca.Id.ToString();
                marcaSelecionadoId = marca.Id;
            }
        }

        private async void btnPesquisarFornecedor_Click(object sender, EventArgs e)
        {
            oFrmConsultaFornecedor.ModoSelecao = true;
            if (oFrmConsultaFornecedor.ShowDialog() == DialogResult.OK && oFrmConsultaFornecedor.FornecedorSelecionado != null)
            {
                var fornecedor = oFrmConsultaFornecedor.FornecedorSelecionado;

                if (listaProdutoFornecedor.Any(pf => pf.FornecedorId == fornecedor.Id))
                {
                    MessageBox.Show("Este fornecedor já foi adicionado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                listaProdutoFornecedor.Add(new ProdutoFornecedor { FornecedorId = fornecedor.Id });
                await CarregarFornecedoresNaListView();
            }
        }
        #endregion

        #region Lógica de Cálculo e Eventos de Leave
        private void CalcularPrecoVenda(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtPrecoCusto.Text, out decimal precoCusto) &&
                decimal.TryParse(txtPorcentagemLucro.Text, out decimal porcentagem))
            {
                decimal valorVenda = precoCusto * (1 + (porcentagem / 100));
                txtPrecoVenda.Text = valorVenda.ToString("F2");
            }
            else
            {
                txtPrecoVenda.Text = "0,00";
            }
        }

        private void CalcularPorcentagemLucro(object sender, EventArgs e)
        {
            if (this.ActiveControl != txtPorcentagemLucro)
            {
                if (decimal.TryParse(txtPrecoCusto.Text, out decimal precoCusto) &&
                    decimal.TryParse(txtPrecoVenda.Text, out decimal valorVenda))
                {
                    if (precoCusto > 0)
                    {
                        decimal percentualLucro = ((valorVenda / precoCusto) - 1) * 100;
                        txtPorcentagemLucro.Text = percentualLucro.ToString("F2");
                    }
                    else
                    {
                        txtPorcentagemLucro.Text = "0,00";
                    }
                }
            }
        }

        private async void txtIdUnidade_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdUnidade.Text)) { txtUnidade.Clear(); unidadeSelecionadaId = -1; return; }
            if (int.TryParse(txtIdUnidade.Text, out int id))
            {
                UnidadeMedida unidade = await new UnidadeMedidaController().BuscarPorId(id);
                if (unidade != null) { txtUnidade.Text = unidade.NomeUnidade; unidadeSelecionadaId = unidade.Id; }
                else { MessageBox.Show("Unidade não encontrada."); txtUnidade.Clear(); txtIdUnidade.Clear(); unidadeSelecionadaId = -1; }
            }
            else
            {
                MessageBox.Show("ID da Unidade de Medida inválido.");
                txtUnidade.Clear();
                txtIdUnidade.Clear();
                unidadeSelecionadaId = -1;
            }
        }

        private async void txtIdMarca_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdMarca.Text)) { txtMarca.Clear(); marcaSelecionadoId = -1; return; }
            if (int.TryParse(txtIdMarca.Text, out int id))
            {
                Marca marca = await new MarcaController().BuscarPorId(id);
                if (marca != null) { txtMarca.Text = marca.NomeMarca; marcaSelecionadoId = marca.Id; }
                else { MessageBox.Show("Marca não encontrada."); txtMarca.Clear(); txtIdMarca.Clear(); marcaSelecionadoId = -1; }
            }
            else
            {
                MessageBox.Show("ID da Marca inválido.");
                txtMarca.Clear();
                txtIdMarca.Clear();
                marcaSelecionadoId = -1;
            }
        }

        private async void txtIdGrupo_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdGrupo.Text)) { txtGrupo.Clear(); grupoSelecionadoId = -1; return; }
            if (int.TryParse(txtIdGrupo.Text, out int id))
            {
                Grupo grupo = await new GrupoController().BuscarPorId(id);
                if (grupo != null) { txtGrupo.Text = grupo.NomeGrupo; grupoSelecionadoId = grupo.Id; }
                else { MessageBox.Show("Grupo não encontrado."); txtGrupo.Clear(); txtIdGrupo.Clear(); grupoSelecionadoId = -1; }
            }
            else
            {
                MessageBox.Show("ID do Grupo inválido.");
                txtGrupo.Clear();
                txtIdGrupo.Clear();
                grupoSelecionadoId = -1;
            }
        }
        #endregion

        #region Métodos Auxiliares
        private async Task CarregarFornecedoresNaListView()
        {
            listVFornecedores.Items.Clear();
            foreach (var relacao in listaProdutoFornecedor)
            {
                Fornecedor f = await fornController.BuscarPorId(relacao.FornecedorId);
                if (f != null)
                {
                    ListViewItem item = new ListViewItem(f.Id.ToString());
                    item.SubItems.Add(f.Nome);
                    item.SubItems.Add(f.Tipo);
                    listVFornecedores.Items.Add(item);
                }
            }
        }

        private void frmCadastroProduto_Load(object sender, EventArgs e)
        {
            // Este método pode ser usado para futuras inicializações.
        }
        #endregion
    }
}