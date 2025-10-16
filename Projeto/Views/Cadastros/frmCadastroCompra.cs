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
    public partial class frmCadastroCompra : Projeto.frmBase
    {
        #region Variáveis e Propriedades

        private Compra aCompra;
        private CompraController controller = new CompraController();
        private FornecedorController fornecedorController = new FornecedorController();
        private ProdutoController produtoController = new ProdutoController();
        private CondicaoPagamentoController condicaoPagamentoController = new CondicaoPagamentoController();
        private FormaPagamentoController formaPagamentoController = new FormaPagamentoController();

        private frmConsultaFornecedor oFrmConsultaFornecedor;
        private frmConsultaProduto oFrmConsultaProduto;
        private frmConsultaCondPgto oFrmConsultaCondPgto;
        private Produto produtoSelecionado;
        private int condicaoSelecionadoId = -1;

        private List<Control> parte1Controles;
        private List<Control> parte2Controles;
        private List<Control> parte3Controles;

        public bool modoCancelamento = false;

        #endregion

        #region Construtor e Inicialização

        public frmCadastroCompra()
        {
            InitializeComponent();
            AgruparControles();
            ConfigurarEstadoInicial();

            txtFrete.TextChanged += new System.EventHandler(this.txtCustosExtras_TextChanged);
            txtSeguro.TextChanged += new System.EventHandler(this.txtCustosExtras_TextChanged);
            txtDespesas.TextChanged += new System.EventHandler(this.txtCustosExtras_TextChanged);
        }

        #endregion

        #region Conexões com outros Formulários

        public void setFrmConsultaFornecedor(object obj)
        {
            if (obj != null) oFrmConsultaFornecedor = (frmConsultaFornecedor)obj;
        }

        public void setFrmConsultaProduto(object obj)
        {
            if (obj != null) oFrmConsultaProduto = (frmConsultaProduto)obj;
        }

        public void setFrmConsultaCondPgto(object obj)
        {
            if (obj != null) oFrmConsultaCondPgto = (frmConsultaCondPgto)obj;
        }

        #endregion

        #region Gestão de Estado do Formulário

        private void AgruparControles()
        {
            parte1Controles = new List<Control> { txtCodigo, txtSerie, txtNumero, txtIDFornecedor, txtFornecedor, btnPesquisarFornecedor, dtpEmissao, dtpChegada };
            parte2Controles = new List<Control> { txtIdProduto, txtProduto, btnPesquisarProduto, txtQuantidade, txtValorUnitario, txtTotal, btnAdicionarProduto, btnEditarProduto, btnRemoverProduto, btnLimparProduto, listViewProdutos };
            parte3Controles = new List<Control> { txtFrete, txtSeguro, txtDespesas, txtValorTotal, txtIdCondPgto, txtCondPgto, btnAdicionarCondPgto, btnLimparCondPgto, listViewCondPgto };
        }

        private void ConfigurarEstadoInicial()
        {
            HabilitarControles(parte1Controles, true);
            HabilitarControles(parte2Controles, true);
            HabilitarControles(parte3Controles, false);
            btnSalvar.Enabled = false;
        }

        private void HabilitarControles(List<Control> controles, bool habilitar)
        {
            foreach (var control in controles)
            {
                control.Enabled = habilitar;
            }
        }

        public override void ConhecaObj(object obj, object ctrl)
        {
            if (obj != null) aCompra = (Compra)obj;
            if (ctrl != null) controller = (CompraController)ctrl;
        }

        public override void LimparTxt()
        {
            txtCodigo.Text = "0";
            txtSerie.Clear();
            txtNumero.Clear();
            txtIDFornecedor.Clear();
            txtFornecedor.Clear();
            dtpEmissao.Value = DateTime.Now;
            dtpChegada.Value = DateTime.Now;
            LimparCamposItem();
            listViewProdutos.Items.Clear();
            txtFrete.Clear();
            txtSeguro.Clear();
            txtDespesas.Clear();
            txtValorTotal.Clear();
            LimparCamposCondPgto();

            lblMotivoCancelamento.Visible = false;
            lblMotivoCancelamento.Text = "Motivo do Cancelamento: ";

            ConfigurarEstadoInicial();
        }

        public override void CarregaTxt()
        {
            if (aCompra == null)
            {
                MessageBox.Show("Não foi possível carregar os dados da compra.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtCodigo.Text = aCompra.Modelo;
            txtSerie.Text = aCompra.Serie;
            txtNumero.Text = aCompra.NumeroNota.ToString();
            txtIDFornecedor.Text = aCompra.FornecedorId.ToString();
            txtFornecedor.Text = aCompra.NomeFornecedor;
            dtpEmissao.Value = aCompra.DataEmissao ?? DateTime.Now;
            dtpChegada.Value = aCompra.DataChegada ?? DateTime.Now;

            listViewProdutos.Items.Clear();
            if (aCompra.Itens != null)
            {
                foreach (var itemCompra in aCompra.Itens)
                {
                    ListViewItem item = new ListViewItem(itemCompra.ProdutoId.ToString());
                    item.SubItems.Add(itemCompra.NomeProduto);
                    item.SubItems.Add(itemCompra.NomeUnidadeMedida ?? "UN"); 
                    item.SubItems.Add(itemCompra.Quantidade.ToString());
                    item.SubItems.Add(itemCompra.ValorUnitario.ToString("F2"));
                    item.SubItems.Add(itemCompra.ValorTotalItem.ToString("F2"));
                    listViewProdutos.Items.Add(item);
                }
            }

            txtFrete.Text = aCompra.ValorFrete.ToString("F2");
            txtSeguro.Text = aCompra.Seguro.ToString("F2");
            txtDespesas.Text = aCompra.Despesas.ToString("F2");
            txtValorTotal.Text = aCompra.ValorTotal.ToString("F2");

            if (aCompra.CondicaoPagamentoId.HasValue)
            {
                txtIdCondPgto.Text = aCompra.CondicaoPagamentoId.Value.ToString();
                txtCondPgto.Text = aCompra.NomeCondPgto;
                condicaoSelecionadoId = aCompra.CondicaoPagamentoId.Value;
            }

            chkInativo.Checked = !aCompra.Ativo;
            lblDataCriacao.Text = aCompra.DataCadastro.HasValue ? $"Criado em: {aCompra.DataCadastro.Value:dd/MM/yyyy HH:mm}" : "Criado em: -";
            lblDataModificacao.Text = aCompra.DataAlteracao.HasValue ? $"Modificado em: {aCompra.DataAlteracao.Value:dd/MM/yyyy HH:mm}" : "Modificado em: -";

            if (!aCompra.Ativo)
            {
                lblMotivoCancelamento.Visible = true;
                lblMotivoCancelamento.Text = "Motivo do Cancelamento: " + aCompra.Observacao;
            }
            else
            {
                lblMotivoCancelamento.Visible = false;
                lblMotivoCancelamento.Text = "Motivo do Cancelamento: ";
            }

            CalculaTotalCompra();
        }

        public override void BloquearTxt()
        {
            HabilitarControles(parte1Controles, false);
            HabilitarControles(parte2Controles, false);
            HabilitarControles(parte3Controles, false);
        }

        public override void DesbloquearTxt()
        {
            HabilitarControles(parte1Controles, true);
            HabilitarControles(parte2Controles, true);
            HabilitarControles(parte3Controles, true);
        }
        #endregion

        #region Event Handlers de Controles

        private async void txtIDFornecedor_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDFornecedor.Text)) { txtFornecedor.Clear(); return; }
            if (int.TryParse(txtIDFornecedor.Text, out int id))
            {
                Fornecedor f = await fornecedorController.BuscarPorId(id);
                txtFornecedor.Text = f?.Nome;
                if (f == null) { MessageBox.Show("Fornecedor não encontrado."); txtFornecedor.Clear(); }
            }
            else { MessageBox.Show("ID de Fornecedor inválido."); txtFornecedor.Clear(); }
        }

        private async void txtIdProduto_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdProduto.Text)) { LimparCamposItemParcial(); return; }
            if (int.TryParse(txtIdProduto.Text, out int id))
            {
                Produto p = await produtoController.BuscarPorId(id);
                if (p != null)
                {
                    produtoSelecionado = p;
                    txtProduto.Text = p.NomeProduto;
                    txtValorUnitario.Text = p.PrecoCusto.ToString("F2");
                    txtQuantidade.Focus();
                }
                else { MessageBox.Show("Produto não encontrado."); LimparCamposItemParcial(); }
            }
            else { MessageBox.Show("ID do Produto inválido."); LimparCamposItemParcial(); }
        }

        private async void txtIdCondPgto_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdCondPgto.Text)) { LimparCamposCondPgto(); return; }
            if (int.TryParse(txtIdCondPgto.Text, out int id))
            {
                CondicaoPagamento cond = await condicaoPagamentoController.BuscarPorId(id);
                if (cond != null)
                {
                    txtCondPgto.Text = cond.Descricao;
                    condicaoSelecionadoId = cond.Id;
                    await AtualizarListViewCondPgto(cond);
                }
                else { MessageBox.Show("Condição de Pagamento não encontrada."); LimparCamposCondPgto(); }
            }
            else { MessageBox.Show("ID da Condição de Pagamento inválido."); LimparCamposCondPgto(); }
        }

        private void txtQuantidade_TextChanged(object sender, EventArgs e) => CalcularTotalItem();
        private void txtValorUnitario_TextChanged(object sender, EventArgs e) => CalcularTotalItem();
        private void txtCustosExtras_TextChanged(object sender, EventArgs e) => CalculaTotalCompra();

        private void btnAdicionarProduto_Click(object sender, EventArgs e)
        {
            if (!ValidarCabecalho())
            {
                MessageBox.Show("Preencha todos os dados do cabeçalho da nota antes de adicionar produtos.", "Dados Incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (produtoSelecionado == null || !decimal.TryParse(txtQuantidade.Text, out decimal qtd) || qtd <= 0 || !decimal.TryParse(txtValorUnitario.Text, out _))
            {
                MessageBox.Show("Selecione um produto e informe quantidade e valor válidos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ListViewItem item = new ListViewItem(produtoSelecionado.Id.ToString());
            item.SubItems.AddRange(new string[] {
                produtoSelecionado.NomeProduto,
                produtoSelecionado.NomeUnidadeMedida ?? "UN",
                txtQuantidade.Text,
                txtValorUnitario.Text,
                txtTotal.Text
            });
            listViewProdutos.Items.Add(item);

            LimparCamposItem();

            if (listViewProdutos.Items.Count > 0)
            {
                HabilitarControles(parte1Controles, false);
                HabilitarControles(parte3Controles, true);
                btnSalvar.Enabled = true;
            }
            CalculaTotalCompra();
        }

        private void btnRemoverProduto_Click(object sender, EventArgs e)
        {
            if (listViewProdutos.SelectedItems.Count > 0)
            {
                listViewProdutos.Items.Remove(listViewProdutos.SelectedItems[0]);
                CalculaTotalCompra();

                if (listViewProdutos.Items.Count == 0)
                {
                    ConfigurarEstadoInicial();
                }
            }
            else { MessageBox.Show("Selecione um produto para remover."); }
        }

        private void btnPesquisarFornecedor_Click(object sender, EventArgs e)
        {
            if (oFrmConsultaFornecedor == null) return;
            oFrmConsultaFornecedor.ModoSelecao = true;
            if (oFrmConsultaFornecedor.ShowDialog() == DialogResult.OK && oFrmConsultaFornecedor.FornecedorSelecionado != null)
            {
                var f = oFrmConsultaFornecedor.FornecedorSelecionado;
                txtIDFornecedor.Text = f.Id.ToString();
                txtFornecedor.Text = f.Nome;
            }
            oFrmConsultaFornecedor.ModoSelecao = false;
        }

        private void btnPesquisarProduto_Click(object sender, EventArgs e)
        {
            if (oFrmConsultaProduto == null) return;
            oFrmConsultaProduto.ModoSelecao = true;
            if (oFrmConsultaProduto.ShowDialog() == DialogResult.OK && oFrmConsultaProduto.ProdutoSelecionado != null)
            {
                produtoSelecionado = oFrmConsultaProduto.ProdutoSelecionado;
                txtIdProduto.Text = produtoSelecionado.Id.ToString();
                txtProduto.Text = produtoSelecionado.NomeProduto;
                txtValorUnitario.Text = produtoSelecionado.PrecoCusto.ToString("F2");
                txtQuantidade.Focus();
            }
            oFrmConsultaProduto.ModoSelecao = false;
        }

        private async void btnAdicionarCondPgto_Click(object sender, EventArgs e)
        {
            if (oFrmConsultaCondPgto == null) return;
            oFrmConsultaCondPgto.ModoSelecao = true;
            if (oFrmConsultaCondPgto.ShowDialog() == DialogResult.OK && oFrmConsultaCondPgto.CondicaoSelecionado != null)
            {
                var condicao = oFrmConsultaCondPgto.CondicaoSelecionado;
                txtIdCondPgto.Text = condicao.Id.ToString();
                txtCondPgto.Text = condicao.Descricao;
                condicaoSelecionadoId = condicao.Id;
                await AtualizarListViewCondPgto(condicao);
            }
            oFrmConsultaCondPgto.ModoSelecao = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (modoCancelamento)
            {
                using (Form prompt = new Form())
                {
                    prompt.Width = 500; prompt.Height = 250;
                    prompt.Text = "Motivo do Cancelamento";
                    prompt.StartPosition = FormStartPosition.CenterParent;
                    prompt.FormBorderStyle = FormBorderStyle.FixedDialog;
                    prompt.MinimizeBox = false; prompt.MaximizeBox = false;

                    Label textLabel = new Label() { Left = 50, Top = 20, Width = 400, Text = "Para cancelar esta nota, digite o motivo do cancelamento abaixo." };
                    TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400, Height = 80, Multiline = true, ScrollBars = ScrollBars.Vertical, MaxLength = 255 };
                    Button confirmation = new Button() { Text = "Confirmar", Left = 240, Width = 100, Top = 150, DialogResult = DialogResult.OK };
                    Button cancel = new Button() { Text = "Voltar", Left = 350, Width = 100, Top = 150, DialogResult = DialogResult.Cancel };

                    confirmation.Enabled = false;
                    textBox.TextChanged += (s, ev) => { confirmation.Enabled = !string.IsNullOrWhiteSpace(textBox.Text); };
                    confirmation.Click += (s, ev) => { prompt.Close(); };
                    cancel.Click += (s, ev) => { prompt.Close(); };

                    prompt.Controls.AddRange(new Control[] { textBox, confirmation, cancel, textLabel });
                    prompt.AcceptButton = confirmation;
                    prompt.CancelButton = cancel;

                    if (prompt.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            if (aCompra != null)
                            {
                                aCompra.Observacao = textBox.Text;
                                controller.Cancelar(aCompra);
                                MessageBox.Show("Compra cancelada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                            }
                        }
                        catch (Exception ex) { MessageBox.Show($"Erro ao cancelar a compra: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                }
                return;
            }

            if (!ValidarDadosGerais()) return;

            try
            {
                Compra novaCompra = MontarObjetoCompra();
                GerarParcelas(novaCompra);
                controller.Salvar(novaCompra);
                MessageBox.Show("Compra salva com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (InvalidOperationException opEx) { MessageBox.Show(opEx.Message, "Erro de Operação", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show("Ocorreu um erro ao salvar a compra: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnLimparProduto_Click(object sender, EventArgs e)
        {
            listViewProdutos.Items.Clear();
            ConfigurarEstadoInicial();
            CalculaTotalCompra();
        }

        private void btnLimparCondPgto_Click(object sender, EventArgs e) => LimparCamposCondPgto();

        #endregion

        #region Lógica de Cálculos e Suporte

        private void LimparCamposItem()
        {
            produtoSelecionado = null;
            txtIdProduto.Clear();
            txtProduto.Clear();
            txtQuantidade.Clear();
            txtValorUnitario.Clear();
            txtTotal.Clear();
            btnPesquisarProduto.Focus();
        }

        private void LimparCamposItemParcial()
        {
            txtProduto.Clear();
            txtValorUnitario.Clear();
            produtoSelecionado = null;
        }

        private void LimparCamposCondPgto()
        {
            txtIdCondPgto.Clear();
            txtCondPgto.Clear();
            listViewCondPgto.Items.Clear();
            condicaoSelecionadoId = -1;
        }

        private async Task AtualizarListViewCondPgto(CondicaoPagamento condicao)
        {
            listViewCondPgto.Items.Clear();
            if (condicao?.Parcelas != null)
            {
                foreach (var parcela in condicao.Parcelas)
                {
                    ListViewItem item = new ListViewItem(parcela.NumParcela.ToString());
                    item.SubItems.Add(parcela.PrazoDias.ToString());
                    item.SubItems.Add(parcela.Porcentagem.ToString("N2") + "%");
                    string desc = parcela.FormaPagamento?.Descricao ?? await formaPagamentoController.ObterDescricaoFormaPagamento(parcela.FormaPagamentoId);
                    item.SubItems.Add(desc);
                    listViewCondPgto.Items.Add(item);
                }
            }
            CalculaTotalCompra();
        }

        private bool ValidarCabecalho()
        {
            return !string.IsNullOrWhiteSpace(txtCodigo.Text) &&
                   !string.IsNullOrWhiteSpace(txtSerie.Text) &&
                   !string.IsNullOrWhiteSpace(txtNumero.Text) &&
                   !string.IsNullOrWhiteSpace(txtIDFornecedor.Text);
        }

        private bool ValidarDadosGerais()
        {
            if (!int.TryParse(txtNumero.Text, out _) || !int.TryParse(txtIDFornecedor.Text, out _))
            {
                MessageBox.Show("Os dados do cabeçalho da nota são obrigatórios e devem ser válidos.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (listViewProdutos.Items.Count == 0)
            {
                MessageBox.Show("É necessário adicionar pelo menos um produto à compra.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private Compra MontarObjetoCompra()
        {
            Compra compra = new Compra
            {
                Modelo = txtCodigo.Text,
                Serie = txtSerie.Text,
                NumeroNota = int.Parse(txtNumero.Text),
                FornecedorId = int.Parse(txtIDFornecedor.Text),
                DataEmissao = dtpEmissao.Value,
                DataChegada = dtpChegada.Value,
                Ativo = !chkInativo.Checked,
                ValorFrete = string.IsNullOrWhiteSpace(txtFrete.Text) ? 0 : Convert.ToDecimal(txtFrete.Text),
                Seguro = string.IsNullOrWhiteSpace(txtSeguro.Text) ? 0 : Convert.ToDecimal(txtSeguro.Text),
                Despesas = string.IsNullOrWhiteSpace(txtDespesas.Text) ? 0 : Convert.ToDecimal(txtDespesas.Text),
                ValorTotal = string.IsNullOrWhiteSpace(txtValorTotal.Text) ? 0 : Convert.ToDecimal(txtValorTotal.Text),
                CondicaoPagamentoId = condicaoSelecionadoId > 0 ? (int?)condicaoSelecionadoId : null
            };

            foreach (ListViewItem item in listViewProdutos.Items)
            {
                compra.Itens.Add(new ItemCompra
                {
                    ProdutoId = int.Parse(item.SubItems[0].Text),
                    Quantidade = decimal.Parse(item.SubItems[3].Text),
                    ValorUnitario = decimal.Parse(item.SubItems[4].Text),
                    ValorTotalItem = decimal.Parse(item.SubItems[5].Text)
                });
            }

            return compra;
        }

        private void CalcularTotalItem()
        {
            if (decimal.TryParse(txtQuantidade.Text, out decimal quantidade) &&
                decimal.TryParse(txtValorUnitario.Text, out decimal valorUnitario))
            {
                txtTotal.Text = (quantidade * valorUnitario).ToString("F2");
            }
            else
            {
                txtTotal.Text = "0.00";
            }
        }

        private void CalculaTotalCompra()
        {
            decimal totalProdutos = 0;
            foreach (ListViewItem item in listViewProdutos.Items)
            {
                totalProdutos += Convert.ToDecimal(item.SubItems[5].Text);
            }
            lblTotalProdutos.Text = $"Total Produtos (R$): {totalProdutos:F2}";

            decimal.TryParse(txtFrete.Text, out decimal frete);
            decimal.TryParse(txtSeguro.Text, out decimal seguro);
            decimal.TryParse(txtDespesas.Text, out decimal despesas);

            decimal valorTotalCompra = totalProdutos + frete + seguro + despesas;
            txtValorTotal.Text = valorTotalCompra.ToString("F2");

            CalcularEExibirParcelas();
        }

        private void CalcularEExibirParcelas()
        {
            decimal totalParcelas = 0;
            if (!decimal.TryParse(txtValorTotal.Text, out decimal valorTotalCompra) || valorTotalCompra <= 0)
            {
                foreach (ListViewItem item in listViewCondPgto.Items)
                {
                    while (item.SubItems.Count <= 4) item.SubItems.Add("");
                    item.SubItems[4].Text = "0,00";
                }
                lblTotalCondiçãoPgto.Text = "Total (R$): 0,00";
                return;
            }
            foreach (ListViewItem item in listViewCondPgto.Items)
            {
                decimal porcentagem = decimal.Parse(item.SubItems[2].Text.Replace("%", "").Trim());
                decimal valorParcela = valorTotalCompra * (porcentagem / 100);
                totalParcelas += valorParcela;
                while (item.SubItems.Count <= 4) item.SubItems.Add("");
                item.SubItems[4].Text = valorParcela.ToString("F2");
            }
            lblTotalCondiçãoPgto.Text = $"Total (R$): {totalParcelas:F2}";
        }

        #endregion

        #region Preparação de Dados

        private void GerarParcelas(Compra compra)
        {
            compra.ParcelasCompra.Clear();
            if (condicaoSelecionadoId <= 0 || listViewCondPgto.Items.Count == 0)
            {
                return;
            }
            DateTime dataBase = dtpEmissao.Value;
            foreach (ListViewItem item in listViewCondPgto.Items)
            {
                int numeroParcela = int.Parse(item.SubItems[0].Text);
                int prazoDias = int.Parse(item.SubItems[1].Text);
                decimal porcentagem = decimal.Parse(item.SubItems[2].Text.Replace("%", "").Trim());
                decimal valorParcela = compra.ValorTotal * (porcentagem / 100);
                DateTime dataVencimento = dataBase.AddDays(prazoDias);
                ParcelaCompra novaParcela = new ParcelaCompra
                {
                    CompraModelo = compra.Modelo,
                    CompraSerie = compra.Serie,
                    CompraNumeroNota = compra.NumeroNota,
                    CompraFornecedorId = compra.FornecedorId,
                    NumeroParcela = numeroParcela,
                    ValorParcela = Math.Round(valorParcela, 2),
                    DataVencimento = dataVencimento
                };
                compra.ParcelasCompra.Add(novaParcela);
            }
        }
        #endregion
    }
}