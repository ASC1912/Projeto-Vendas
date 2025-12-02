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
    public partial class frmCadastroVenda : Projeto.frmBase
    {
        #region Variáveis e Propriedades

        private Venda aVenda = new Venda();
        private VendaController controller = new VendaController();

        private ClienteController clienteController = new ClienteController();
        private ProdutoController produtoController = new ProdutoController();
        private CondicaoPagamentoController condicaoPagamentoController = new CondicaoPagamentoController();
        private FormaPagamentoController formaPagamentoController = new FormaPagamentoController();

        private frmConsultaCliente oFrmConsultaCliente;
        private frmConsultaProduto oFrmConsultaProduto;
        private frmConsultaCondPgto oFrmConsultaCondPgto;
        private frmConsultaFuncionario oFrmConsultaFuncionario; 

        private Produto produtoSelecionado;
        private CondicaoPagamento condicaoPagamentoSelecionada;

        private List<Control> parte1Controles;
        private List<Control> parte2Controles;
        private List<Control> parte3Controles;

        public bool modoCancelamento = false;
        public bool modoVisualizacao = false;
        private bool modoEdicaoItem = false;

        #endregion

        #region Construtor e Inicialização

        public frmCadastroVenda()
        {
            InitializeComponent();

            DateTime dataAtual = DateTime.Now;

            dtpEmissao.Value = dataAtual;
            dtpSaida.Value = dataAtual;

            dtpEmissao.MaxDate = dataAtual;
            dtpSaida.MinDate = dataAtual.Date;

            AgruparControles();
            ConfigurarEstadoInicial();

            this.txtCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress_ApenasNumerosInteiros);
            this.txtSerie.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress_ApenasNumerosInteiros);
            this.txtNumero.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress_ApenasNumerosInteiros);


            this.txtQuantidade.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress_ApenasNumerosInteiros);
            this.txtValorUnitario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress_ApenasNumerosEVirgula);


            this.txtIdCliente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress_ApenasNumerosInteiros);
            this.txtIdProduto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress_ApenasNumerosInteiros);
            this.txtIdCondPgto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress_ApenasNumerosInteiros);

            if (this.Controls.Find("txtIdFuncionario", true).Length > 0)
                this.Controls.Find("txtIdFuncionario", true)[0].KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress_ApenasNumerosInteiros);


            this.dtpEmissao.ValueChanged += new System.EventHandler(this.dtpEmissao_ValueChanged);

            this.listViewProdutos.SelectedIndexChanged += new System.EventHandler(this.listViewProdutos_SelectedIndexChanged);
            this.btnEditarProduto.Click += new System.EventHandler(this.btnEditarProduto_Click);

            this.txtIdFuncionario.TextChanged += (s, e) => AtualizarEstadoDosControles();
            this.txtNumero.TextChanged += (s, e) => AtualizarEstadoDosControles();
            this.txtSerie.TextChanged += (s, e) => AtualizarEstadoDosControles();
            this.txtCodigo.TextChanged += (s, e) => AtualizarEstadoDosControles();
        }

        #endregion

        #region Conexões com outros Formulários

        public void setFrmConsultaCliente(object obj) { if (obj != null) oFrmConsultaCliente = (frmConsultaCliente)obj; }
        public void setFrmConsultaProduto(object obj) { if (obj != null) oFrmConsultaProduto = (frmConsultaProduto)obj; }
        public void setFrmConsultaCondPgto(object obj) { if (obj != null) oFrmConsultaCondPgto = (frmConsultaCondPgto)obj; }
        public void setFrmConsultaFuncionario(object obj) { if (obj != null) oFrmConsultaFuncionario = (frmConsultaFuncionario)obj; }

        #endregion

        #region Gestão de Estado do Formulário

        private void AgruparControles()
        {
            parte1Controles = new List<Control> { txtCodigo, txtSerie, txtNumero, txtIdCliente, txtCliente, btnPesquisarCliente, dtpEmissao, dtpSaida };

            if (this.Controls.Find("txtIdFuncionario", true).Length > 0) parte1Controles.Add(this.Controls.Find("txtIdFuncionario", true)[0]);
            if (this.Controls.Find("txtFuncionario", true).Length > 0) parte1Controles.Add(this.Controls.Find("txtFuncionario", true)[0]);
            if (this.Controls.Find("btnPesquisarFuncionario", true).Length > 0) parte1Controles.Add(this.Controls.Find("btnPesquisarFuncionario", true)[0]);

            parte2Controles = new List<Control> { txtIdProduto, txtProduto, btnPesquisarProduto, txtQuantidade, txtValorUnitario, txtTotal, btnAdicionarProduto, btnEditarProduto, btnRemoverProduto, btnLimparProduto, listViewProdutos };

            parte3Controles = new List<Control> { txtValorTotal, txtIdCondPgto, txtCondPgto, btnAdicionarCondPgto, btnLimparCondPgto, listViewCondPgto };
        }

        private void ConfigurarEstadoInicial()
        {
            HabilitarControles(parte1Controles, true);
            HabilitarControles(parte2Controles, false);
            HabilitarControles(parte3Controles, false);
            btnSalvar.Enabled = false;
        }

        private void AtualizarEstadoDosControles()
        {
            bool cabecalhoValido = !string.IsNullOrWhiteSpace(txtCodigo.Text) &&
                                   !string.IsNullOrWhiteSpace(txtSerie.Text) &&
                                   !string.IsNullOrWhiteSpace(txtNumero.Text) &&
                                   !string.IsNullOrWhiteSpace(txtIdCliente.Text) &&
                                   !string.IsNullOrWhiteSpace(txtIdFuncionario.Text); 

            bool temItens = listViewProdutos.Items.Count > 0;

            HabilitarControles(parte2Controles, cabecalhoValido);
            HabilitarControles(parte3Controles, temItens);
            btnSalvar.Enabled = temItens;
        }

        private void HabilitarControles(List<Control> controles, bool habilitar)
        {
            foreach (var control in controles)
            {
                control.Enabled = habilitar;
            }
        }

        public override void BloquearTxt()
        {
            HabilitarControles(parte1Controles, false);
            HabilitarControles(parte2Controles, false);
            HabilitarControles(parte3Controles, false);
            btnSalvar.Enabled = false;
        }

        public override void DesbloquearTxt()
        {
            HabilitarControles(parte1Controles, true);
        }

        #endregion

        #region Carregamento e Limpeza de Dados

        public override void ConhecaObj(object obj, object ctrl)
        {
            if (obj != null) aVenda = (Venda)obj;
            if (ctrl != null) controller = (VendaController)ctrl;
        }

        public override void LimparTxt()
        {
            modoVisualizacao = false;

            aVenda = new Venda();
            txtCodigo.Text = "55";
            txtSerie.Text = "1";
            txtNumero.Clear();
            txtIdCliente.Clear();
            txtCliente.Clear();

            if (this.Controls.Find("txtIdFuncionario", true).Length > 0) this.Controls.Find("txtIdFuncionario", true)[0].Text = "";
            if (this.Controls.Find("txtFuncionario", true).Length > 0) this.Controls.Find("txtFuncionario", true)[0].Text = "";

            DateTime dataAgora = DateTime.Now;

            dtpEmissao.MaxDate = dataAgora;
            dtpEmissao.Value = dataAgora;
            dtpSaida.MinDate = dataAgora.Date;
            dtpSaida.Value = dataAgora;

            LimparCamposItem();
            listViewProdutos.Items.Clear();

            txtValorTotal.Clear();
            LimparCamposCondPgto();

            lblMotivoCancelamento.Visible = false;
            lblMotivoCancelamento.Text = "Motivo do Cancelamento: ";

            ConfigurarEstadoInicial();
            CalculaTotalVenda();
        }

        public override async void CarregaTxt()
        {
            if (aVenda == null)
            {
                MessageBox.Show("Não foi possível carregar os dados da venda.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtCodigo.Text = aVenda.Modelo;
            txtSerie.Text = aVenda.Serie;
            txtNumero.Text = aVenda.NumeroNota.ToString();
            txtIdCliente.Text = aVenda.ClienteId.ToString();
            txtCliente.Text = aVenda.NomeCliente;

            if (this.Controls.Find("txtIdFuncionario", true).Length > 0)
                this.Controls.Find("txtIdFuncionario", true)[0].Text = aVenda.FuncionarioId.ToString();

            if (this.Controls.Find("txtFuncionario", true).Length > 0)
                this.Controls.Find("txtFuncionario", true)[0].Text = aVenda.NomeFuncionario;

            DateTime dataEmissao = aVenda.DataEmissao ?? DateTime.Now;
            if (dataEmissao > dtpEmissao.MaxDate) dtpEmissao.MaxDate = dataEmissao;
            dtpEmissao.Value = dataEmissao;

            DateTime dataSaida = aVenda.DataSaida ?? DateTime.Now;
            if (dataSaida < dtpSaida.MinDate) dtpSaida.MinDate = dataSaida;
            dtpSaida.Value = dataSaida;

            listViewProdutos.Items.Clear();
            if (aVenda.Itens != null)
            {
                foreach (var itemVenda in aVenda.Itens)
                {
                    ListViewItem item = new ListViewItem(itemVenda.ProdutoId.ToString());
                    item.SubItems.AddRange(new string[] {
                        itemVenda.NomeProduto,
                        itemVenda.NomeUnidadeMedida ?? "UN",
                        itemVenda.Quantidade.ToString(),
                        itemVenda.ValorUnitario.ToString("F2"),
                        itemVenda.ValorTotalItem.ToString("F2")
                    });
                    listViewProdutos.Items.Add(item);
                }
            }


            if (aVenda.CondicaoPagamentoId.HasValue)
            {
                condicaoPagamentoSelecionada = await condicaoPagamentoController.BuscarPorId(aVenda.CondicaoPagamentoId.Value);
                if (condicaoPagamentoSelecionada != null)
                {
                    txtIdCondPgto.Text = condicaoPagamentoSelecionada.Id.ToString();
                    txtCondPgto.Text = condicaoPagamentoSelecionada.Descricao;
                    await AtualizarListViewCondPgto(condicaoPagamentoSelecionada);
                }
            }

            chkInativo.Checked = !aVenda.Ativo;
            lblDataCriacao.Text = aVenda.DataCadastro.HasValue ? $"Criado em: {aVenda.DataCadastro.Value:dd/MM/yyyy HH:mm}" : "Criado em: -";
            lblDataModificacao.Text = aVenda.DataAlteracao.HasValue ? $"Modificado em: {aVenda.DataAlteracao.Value:dd/MM/yyyy HH:mm}" : "Modificado em: -";

            if (!aVenda.Ativo)
            {
                lblMotivoCancelamento.Visible = true;
                lblMotivoCancelamento.Text = "Motivo do Cancelamento: " + aVenda.Observacao;
                BloquearTxt();
                btnSalvar.Enabled = false;
            }
            else
            {
                lblMotivoCancelamento.Visible = false;

                if (!modoVisualizacao && !modoCancelamento)
                {
                    AtualizarEstadoDosControles();
                }
            }

            CalculaTotalVenda();
        }

        #endregion

        #region Eventos de Controles

        private void dtpEmissao_ValueChanged(object sender, EventArgs e)
        {
            dtpSaida.MinDate = dtpEmissao.Value.Date;
            if (dtpSaida.Value.Date < dtpEmissao.Value.Date)
            {
                dtpSaida.Value = dtpEmissao.Value.Date;
            }
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
                            if (aVenda != null)
                            {
                                aVenda.Observacao = textBox.Text;
                                controller.Cancelar(aVenda);
                                MessageBox.Show("Venda cancelada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                            }
                        }
                        catch (Exception ex) { MessageBox.Show($"Erro ao cancelar a venda: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                }
                return;
            }

            if (!Validador.CampoObrigatorio(txtCodigo, "O Modelo é obrigatório.")) return; 
            if (!Validador.CampoObrigatorio(txtSerie, "A Série é obrigatória.")) return;
            if (!Validador.CampoObrigatorio(txtNumero, "O Número da Nota é obrigatório.")) return;

            if (!Validador.CampoObrigatorio(txtIdCliente, "Selecione um Cliente.")) return;
            if (!Validador.CampoObrigatorio(txtIdFuncionario, "Selecione um Funcionário.")) return;

            if (dtpEmissao.Value.Date > DateTime.Now.Date)
            {
                MessageBox.Show("A Data de Emissão não pode ser futura.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpEmissao.Focus();
                return;
            }
            if (dtpSaida.Value.Date < dtpEmissao.Value.Date)
            {
                MessageBox.Show("A Data de Saída não pode ser anterior à Emissão.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpSaida.Focus();
                return;
            }

            if (!ValidarDadosGerais()) return;

            try
            {
                Venda novaVenda = MontarObjetoVenda();
                GerarParcelas(novaVenda);
                controller.Salvar(novaVenda);
                MessageBox.Show("Venda salva com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (InvalidOperationException opEx) { MessageBox.Show(opEx.Message, "Erro de Operação", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { MessageBox.Show("Ocorreu um erro ao salvar a venda: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private async void txtIdCliente_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdCliente.Text))
            {
                txtCliente.Clear();
                LimparCamposCondPgto();
                return;
            }

            if (int.TryParse(txtIdCliente.Text, out int id))
            {
                await CarregarDadosCliente(id);
            }
            else { MessageBox.Show("ID de Cliente inválido."); txtCliente.Clear(); }
            AtualizarEstadoDosControles();
        }

        private async Task CarregarDadosCliente(int id)
        {
            try
            {
                Cliente c = await clienteController.BuscarPorId(id);
                if (c != null)
                {
                    txtIdCliente.Text = c.Id.ToString();
                    txtCliente.Text = c.Nome;

                    if (c.IdCondicao.HasValue && c.IdCondicao > 0)
                    {
                        var condicao = await condicaoPagamentoController.BuscarPorId(c.IdCondicao.Value);
                        if (condicao != null)
                        {
                            condicaoPagamentoSelecionada = condicao;
                            txtIdCondPgto.Text = condicao.Id.ToString();
                            txtCondPgto.Text = condicao.Descricao;
                            await AtualizarListViewCondPgto(condicao);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Cliente não encontrado.");
                    txtIdCliente.Clear();
                    txtCliente.Clear();
                    LimparCamposCondPgto();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar cliente: " + ex.Message);
            }
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
                    txtValorUnitario.Text = p.PrecoVenda.ToString("F2");
                    txtQuantidade.Text = "1";
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
                condicaoPagamentoSelecionada = await condicaoPagamentoController.BuscarPorId(id);
                if (condicaoPagamentoSelecionada != null)
                {
                    txtCondPgto.Text = condicaoPagamentoSelecionada.Descricao;
                    await AtualizarListViewCondPgto(condicaoPagamentoSelecionada);
                }
                else { MessageBox.Show("Condição de Pagamento não encontrada."); LimparCamposCondPgto(); }
            }
            else { MessageBox.Show("ID da Condição de Pagamento inválido."); LimparCamposCondPgto(); }
        }

        private void txtQuantidade_TextChanged(object sender, EventArgs e) => CalcularTotalItem();
        private void txtValorUnitario_TextChanged(object sender, EventArgs e) => CalcularTotalItem();


        private void btnAdicionarProduto_Click(object sender, EventArgs e)
        {
            if (!ValidarCabecalho())
            {
                MessageBox.Show("Preencha todos os dados do cabeçalho da nota antes de adicionar produtos.", "Dados Incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (produtoSelecionado == null || !decimal.TryParse(txtQuantidade.Text, out decimal newQty) || newQty <= 0 || !decimal.TryParse(txtValorUnitario.Text, out decimal vlrUnitarioDigitado))
            {
                MessageBox.Show("Selecione um produto e informe quantidade e valor válidos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idProdutoAdicionar = produtoSelecionado.Id;
            int currentStock = produtoSelecionado.Estoque;

            decimal qtyAlreadyInList = 0;
            foreach (ListViewItem item in listViewProdutos.Items)
            {
                int idProdutoNaLista = int.Parse(item.SubItems[0].Text);
                if (idProdutoNaLista == idProdutoAdicionar)
                {
                    qtyAlreadyInList += decimal.Parse(item.SubItems[3].Text);
                }
            }

            decimal totalRequestedQty = qtyAlreadyInList + newQty;

            if (totalRequestedQty > currentStock)
            {
                MessageBox.Show($"Estoque insuficiente para o produto '{produtoSelecionado.NomeProduto}'.\n\nEstoque Disponível: {currentStock}\nQuantidade já na lista: {qtyAlreadyInList}\nQuantidade Solicitada (total): {totalRequestedQty}",
                                "Estoque Insuficiente",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            bool itemEncontradoEAgrupado = false;
            foreach (ListViewItem item in listViewProdutos.Items)
            {
                int idProdutoNaLista = int.Parse(item.SubItems[0].Text);
                decimal vlrUnitarioNaLista = decimal.Parse(item.SubItems[4].Text);

                if (idProdutoNaLista == idProdutoAdicionar && vlrUnitarioNaLista == vlrUnitarioDigitado)
                {
                    decimal oldQty = decimal.Parse(item.SubItems[3].Text);
                    decimal totalQtyAgrupado = oldQty + newQty;
                    decimal totalValueAgrupado = totalQtyAgrupado * vlrUnitarioDigitado;

                    item.SubItems[3].Text = totalQtyAgrupado.ToString();
                    item.SubItems[5].Text = totalValueAgrupado.ToString("F2");

                    itemEncontradoEAgrupado = true;
                    break;
                }
            }

            if (!itemEncontradoEAgrupado)
            {
                ListViewItem item = new ListViewItem(produtoSelecionado.Id.ToString());
                item.SubItems.AddRange(new string[] {
                produtoSelecionado.NomeProduto,
                produtoSelecionado.NomeUnidadeMedida ?? "UN",
                newQty.ToString(),
                vlrUnitarioDigitado.ToString("F2"),
                txtTotal.Text
            });
                listViewProdutos.Items.Add(item);
            }

            LimparCamposItem();
            CalculaTotalVenda();
            AtualizarEstadoDosControles();
        }


        private void btnRemoverProduto_Click(object sender, EventArgs e)
        {
            if (modoEdicaoItem)
            {
                LimparCamposItem();
            }
            else
            {
                if (listViewProdutos.SelectedItems.Count > 0)
                {
                    listViewProdutos.Items.Remove(listViewProdutos.SelectedItems[0]);
                    CalculaTotalVenda();
                    AtualizarEstadoDosControles();
                    LimparCamposItem();
                }
                else
                {
                    MessageBox.Show("Selecione um produto para remover.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private async void btnPesquisarCliente_Click(object sender, EventArgs e)
        {
            if (oFrmConsultaCliente == null) oFrmConsultaCliente = new frmConsultaCliente();
            oFrmConsultaCliente.ModoSelecao = true;
            if (oFrmConsultaCliente.ShowDialog() == DialogResult.OK && oFrmConsultaCliente.ClienteSelecionado != null)
            {
                var c = oFrmConsultaCliente.ClienteSelecionado;
                await CarregarDadosCliente(c.Id);
            }
            oFrmConsultaCliente.ModoSelecao = false;
            AtualizarEstadoDosControles();
        }

        private void btnPesquisarProduto_Click(object sender, EventArgs e)
        {
            if (oFrmConsultaProduto == null) oFrmConsultaProduto = new frmConsultaProduto();
            oFrmConsultaProduto.ModoSelecao = true;
            if (oFrmConsultaProduto.ShowDialog() == DialogResult.OK && oFrmConsultaProduto.ProdutoSelecionado != null)
            {
                produtoSelecionado = oFrmConsultaProduto.ProdutoSelecionado;
                txtIdProduto.Text = produtoSelecionado.Id.ToString();
                txtProduto.Text = produtoSelecionado.NomeProduto;
                txtValorUnitario.Text = produtoSelecionado.PrecoVenda.ToString("F2");
                txtQuantidade.Text = "1";
                txtQuantidade.Focus();
            }
            oFrmConsultaProduto.ModoSelecao = false;
        }

        private void btnPesquisarFuncionario_Click(object sender, EventArgs e)
        {
            if (oFrmConsultaFuncionario == null)
            {
                MessageBox.Show("Tela de consulta de funcionários não configurada.");
                return;
            }

            oFrmConsultaFuncionario.ModoSelecao = true;

            if (oFrmConsultaFuncionario.ShowDialog() == DialogResult.OK && oFrmConsultaFuncionario.FuncionarioSelecionado != null)
            {
                var f = oFrmConsultaFuncionario.FuncionarioSelecionado;

                if (this.Controls.Find("txtIdFuncionario", true).Length > 0)
                    this.Controls.Find("txtIdFuncionario", true)[0].Text = f.Id.ToString();

                if (this.Controls.Find("txtFuncionario", true).Length > 0)
                    this.Controls.Find("txtFuncionario", true)[0].Text = f.Nome;
            }

            oFrmConsultaFuncionario.ModoSelecao = false;
        }

        private async void btnAdicionarCondPgto_Click(object sender, EventArgs e)
        {
            if (oFrmConsultaCondPgto == null) oFrmConsultaCondPgto = new frmConsultaCondPgto();
            oFrmConsultaCondPgto.ModoSelecao = true;
            if (oFrmConsultaCondPgto.ShowDialog() == DialogResult.OK && oFrmConsultaCondPgto.CondicaoSelecionado != null)
            {
                condicaoPagamentoSelecionada = oFrmConsultaCondPgto.CondicaoSelecionado;
                txtIdCondPgto.Text = condicaoPagamentoSelecionada.Id.ToString();
                txtCondPgto.Text = condicaoPagamentoSelecionada.Descricao;
                await AtualizarListViewCondPgto(condicaoPagamentoSelecionada);
            }
            oFrmConsultaCondPgto.ModoSelecao = false;
        }

        private void btnLimparProduto_Click(object sender, EventArgs e)
        {
            listViewProdutos.Items.Clear();
            CalculaTotalVenda();
            AtualizarEstadoDosControles();
        }

        private void btnLimparCondPgto_Click(object sender, EventArgs e) => LimparCamposCondPgto();


        private async void listViewProdutos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewProdutos.SelectedItems.Count > 0)
            {
                ListViewItem item = listViewProdutos.SelectedItems[0];

                txtIdProduto.Text = item.SubItems[0].Text;
                txtProduto.Text = item.SubItems[1].Text;
                txtQuantidade.Text = item.SubItems[3].Text;
                txtValorUnitario.Text = item.SubItems[4].Text;

                if (int.TryParse(item.SubItems[0].Text, out int id))
                {
                    produtoSelecionado = await produtoController.BuscarPorId(id);
                }

                modoEdicaoItem = true;
                btnAdicionarProduto.Enabled = false;
                btnEditarProduto.Enabled = true;
                btnRemoverProduto.Text = "Cancelar";
                txtIdProduto.Enabled = false;
                btnPesquisarProduto.Enabled = false;
            }
        }

        private void btnEditarProduto_Click(object sender, EventArgs e)
        {
            if (listViewProdutos.SelectedItems.Count == 0)
            {
                MessageBox.Show("Nenhum item selecionado para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (produtoSelecionado == null || !decimal.TryParse(txtQuantidade.Text, out decimal newQty) || newQty <= 0 || !decimal.TryParse(txtValorUnitario.Text, out decimal vlrUnitarioDigitado))
            {
                MessageBox.Show("Os dados do produto para edição são inválidos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ListViewItem itemSelecionado = listViewProdutos.SelectedItems[0];
            int idProdutoEditar = produtoSelecionado.Id;
            int currentStock = produtoSelecionado.Estoque;
            decimal qtyAlreadyInList = 0;

            foreach (ListViewItem item in listViewProdutos.Items)
            {
                if (item == itemSelecionado) continue;

                int idProdutoNaLista = int.Parse(item.SubItems[0].Text);
                if (idProdutoNaLista == idProdutoEditar)
                {
                    qtyAlreadyInList += decimal.Parse(item.SubItems[3].Text);
                }
            }

            decimal totalRequestedQty = qtyAlreadyInList + newQty;

            if (totalRequestedQty > currentStock)
            {
                MessageBox.Show($"Estoque insuficiente para o produto '{produtoSelecionado.NomeProduto}'.\n\nEstoque Disponível: {currentStock}\nQtd. em outras linhas: {qtyAlreadyInList}\nQtd. Solicitada nesta linha: {newQty}",
                                "Estoque Insuficiente",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            decimal oldPrice = decimal.Parse(itemSelecionado.SubItems[4].Text);
            if (oldPrice != vlrUnitarioDigitado)
            {
                MessageBox.Show("Não é permitido alterar o preço unitário de um item na edição.\n\nRemova este item e adicione-o novamente com o novo preço.", "Alteração de Preço Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtValorUnitario.Text = oldPrice.ToString("F2");
                return;
            }

            decimal newTotalValue = newQty * vlrUnitarioDigitado;
            itemSelecionado.SubItems[3].Text = newQty.ToString();
            itemSelecionado.SubItems[5].Text = newTotalValue.ToString("F2");

            LimparCamposItem();
            CalculaTotalVenda();
        }

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

            modoEdicaoItem = false;
            btnAdicionarProduto.Enabled = true;
            btnEditarProduto.Enabled = false;
            btnRemoverProduto.Text = "Remover";

            txtIdProduto.Enabled = true;
            btnPesquisarProduto.Enabled = true;

            if (listViewProdutos.SelectedItems.Count > 0)
            {
                listViewProdutos.SelectedItems[0].Selected = false;
            }
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
            condicaoPagamentoSelecionada = null;
            CalculaTotalVenda();
        }

        private async Task AtualizarListViewCondPgto(CondicaoPagamento condicao)
        {
            listViewCondPgto.Items.Clear();

            DateTime dataBase = dtpEmissao.Value.Date;

            if (condicao?.Parcelas != null)
            {
                foreach (var parcela in condicao.Parcelas.OrderBy(p => p.NumParcela))
                {
                    ListViewItem item = new ListViewItem(parcela.NumParcela.ToString());

                    DateTime dataVencimento = dataBase.AddDays(parcela.PrazoDias);
                    item.SubItems.Add(dataVencimento.ToString("dd/MM/yyyy"));

                    string desc = parcela.FormaPagamento?.Descricao ?? await formaPagamentoController.ObterDescricaoFormaPagamento(parcela.FormaPagamentoId);
                    item.SubItems.Add(desc);
                    listViewCondPgto.Items.Add(item);
                }
            }
            CalculaTotalVenda();
        }

        private void CalcularTotalItem()
        {
            if (decimal.TryParse(txtQuantidade.Text, out decimal quantidade) && decimal.TryParse(txtValorUnitario.Text, out decimal valorUnitario))
            {
                txtTotal.Text = (quantidade * valorUnitario).ToString("F2");
            }
            else
            {
                txtTotal.Text = "0,00";
            }
        }

        private void CalculaTotalVenda()
        {
            decimal totalProdutos = 0;
            foreach (ListViewItem item in listViewProdutos.Items)
            {
                totalProdutos += Convert.ToDecimal(item.SubItems[5].Text);
            }
            lblTotalProdutos.Text = $"Total Produtos (R$): {totalProdutos:F2}";

            // REMOVIDO: Frete
            // decimal.TryParse(txtFrete.Text, out decimal frete);

            decimal valorTotalVenda = totalProdutos; // + frete (removido)
            txtValorTotal.Text = valorTotalVenda.ToString("F2");

            CalcularEExibirParcelas();
        }

        private void CalcularEExibirParcelas()
        {
            decimal totalParcelas = 0;
            if (!decimal.TryParse(txtValorTotal.Text, out decimal valorTotalVenda) || valorTotalVenda <= 0 ||
              condicaoPagamentoSelecionada == null || condicaoPagamentoSelecionada.Parcelas == null || condicaoPagamentoSelecionada.Parcelas.Count == 0)
            {
                foreach (ListViewItem item in listViewCondPgto.Items)
                {
                    while (item.SubItems.Count <= 3) item.SubItems.Add("");
                    item.SubItems[3].Text = "0,00";
                }
                lblTotalCondiçãoPgto.Text = "Total (R$): 0,00";
                return;
            }

            List<decimal> valoresCalculados = new List<decimal>();
            decimal valorAcumulado = 0;
            var parcelasOrdenadas = condicaoPagamentoSelecionada.Parcelas.OrderBy(p => p.NumParcela);

            foreach (var parcelaDefinicao in parcelasOrdenadas)
            {
                decimal porcentagem = parcelaDefinicao.Porcentagem;
                decimal valorParcela = valorTotalVenda * (porcentagem / 100);

                valorParcela = Math.Round(valorParcela, 2);
                valorAcumulado += valorParcela;

                if (parcelaDefinicao.NumParcela == parcelasOrdenadas.Last().NumParcela)
                {
                    valorParcela += (valorTotalVenda - valorAcumulado);
                }
                valoresCalculados.Add(valorParcela);
                totalParcelas += valorParcela;
            }

            int index = 0;
            foreach (ListViewItem item in listViewCondPgto.Items)
            {
                if (index < valoresCalculados.Count)
                {
                    while (item.SubItems.Count <= 3) item.SubItems.Add("");
                    item.SubItems[3].Text = valoresCalculados[index].ToString("F2");
                    index++;
                }
            }

            lblTotalCondiçãoPgto.Text = $"Total (R$): {totalParcelas:F2}";
        }

        #endregion

        #region Preparação de Dados para Salvar

        private bool ValidarCabecalho()
        {
            return !string.IsNullOrWhiteSpace(txtCodigo.Text) &&
                   !string.IsNullOrWhiteSpace(txtSerie.Text) &&
                   !string.IsNullOrWhiteSpace(txtNumero.Text) &&
                   !string.IsNullOrWhiteSpace(txtIdCliente.Text);
        }

        private bool ValidarDadosGerais()
        {
            if (!int.TryParse(txtNumero.Text, out _) || !int.TryParse(txtIdCliente.Text, out _))
            {
                MessageBox.Show("Os dados do cabeçalho da nota são obrigatórios e devem ser válidos.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (this.Controls.Find("txtIdFuncionario", true).Length > 0)
            {
                if (!int.TryParse(this.Controls.Find("txtIdFuncionario", true)[0].Text, out _))
                {
                    MessageBox.Show("Selecione um funcionário.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            if (listViewProdutos.Items.Count == 0)
            {
                MessageBox.Show("É necessário adicionar pelo menos um produto à venda.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (condicaoPagamentoSelecionada == null)
            {
                MessageBox.Show("É necessário selecionar uma condição de pagamento.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private Venda MontarObjetoVenda()
        {
            Venda venda = new Venda
            {
                Modelo = txtCodigo.Text,
                Serie = txtSerie.Text,
                NumeroNota = int.Parse(txtNumero.Text),
                ClienteId = int.Parse(txtIdCliente.Text),
                DataEmissao = dtpEmissao.Value,
                DataSaida = dtpSaida.Value,
                Ativo = !chkInativo.Checked,
                ValorTotal = string.IsNullOrWhiteSpace(txtValorTotal.Text) ? 0 : Convert.ToDecimal(txtValorTotal.Text),
                CondicaoPagamentoId = condicaoPagamentoSelecionada?.Id
            };

            if (this.Controls.Find("txtIdFuncionario", true).Length > 0)
            {
                if (int.TryParse(this.Controls.Find("txtIdFuncionario", true)[0].Text, out int idFunc))
                    venda.FuncionarioId = idFunc;
            }

            foreach (ListViewItem item in listViewProdutos.Items)
            {
                venda.Itens.Add(new ItemVenda
                {
                    ProdutoId = int.Parse(item.SubItems[0].Text),
                    Quantidade = decimal.Parse(item.SubItems[3].Text),
                    ValorUnitario = decimal.Parse(item.SubItems[4].Text),
                    ValorTotalItem = decimal.Parse(item.SubItems[5].Text)
                });
            }

            return venda;
        }

        private void GerarParcelas(Venda venda)
        {
            venda.Parcelas.Clear();
            if (condicaoPagamentoSelecionada == null || condicaoPagamentoSelecionada.Parcelas == null) return;

            DateTime dataBase = dtpEmissao.Value.Date;
            decimal valorTotalVenda = venda.ValorTotal;
            decimal valorAcumulado = 0;

            var parcelasOrdenadas = condicaoPagamentoSelecionada.Parcelas.OrderBy(p => p.NumParcela).ToList();

            foreach (var parcelaDefinicao in parcelasOrdenadas)
            {
                decimal valorParcela = valorTotalVenda * (parcelaDefinicao.Porcentagem / 100);
                valorParcela = Math.Round(valorParcela, 2);
                valorAcumulado += valorParcela;

                if (parcelaDefinicao.NumParcela == parcelasOrdenadas.Last().NumParcela)
                {
                    valorParcela += (valorTotalVenda - valorAcumulado);
                }

                DateTime dataVencimento = dataBase.AddDays(parcelaDefinicao.PrazoDias);

                ContasAReceber novaConta = new ContasAReceber
                {
                    VendaModelo = venda.Modelo,
                    VendaSerie = venda.Serie,
                    VendaNumeroNota = venda.NumeroNota,
                    VendaClienteId = venda.ClienteId,
                    NumeroParcela = parcelaDefinicao.NumParcela,
                    ValorVencimento = valorParcela,
                    DataVencimento = dataVencimento,
                    DataEmissao = DateTime.Now,
                    Descricao = $"Parcela {parcelaDefinicao.NumParcela}/{parcelasOrdenadas.Count} NFe {venda.NumeroNota}",
                    Ativo = true,
                    Status = "Aberta",
                    ClienteId = venda.ClienteId,
                    IdFormaPagamento = parcelaDefinicao.FormaPagamentoId,

                    Juros = condicaoPagamentoSelecionada.Juros,
                    Multa = condicaoPagamentoSelecionada.Multa,
                    Desconto = condicaoPagamentoSelecionada.Desconto
                };

                venda.Parcelas.Add(novaConta);
            }

            decimal totalParcelasGeradas = venda.Parcelas.Sum(p => p.ValorVencimento);
            if (Math.Abs(totalParcelasGeradas - valorTotalVenda) > 0.01m)
            {
                MessageBox.Show($"Erro de arredondamento: O total das parcelas ({totalParcelasGeradas:C2}) não bate com o total da venda ({valorTotalVenda:C2}).", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                venda.Parcelas.Clear();
            }
        }

        #endregion
    }
}