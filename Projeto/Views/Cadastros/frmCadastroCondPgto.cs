using Projeto.Controller;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; // Adicionado
using System.Windows.Forms;
using Projeto.Utils;

namespace Projeto
{
    public partial class frmCadastroCondPgto : Projeto.frmBase
    {
        private ParcelaController parcelaController = new ParcelaController();
        private FormaPagamentoController formaPagamentoController = new FormaPagamentoController();
        private CondicaoPagamentoController condicaoPagamentoController = new CondicaoPagamentoController();
        private int formaPagamentoSelecionadaId = -1;
        public bool modoEdicao = false;
        public bool modoExclusao = false;

        public frmCadastroCondPgto() : base()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
            txtFormaPagamento.ReadOnly = true;
        }

        public async void CarregarCondicaoPagamento(int id, string descricao, int qtdParcelas, decimal juros, decimal multa, decimal desconto, bool ativo, DateTime? dataCadastro, DateTime? dataAlteracao)
        {
            modoEdicao = true;
            txtCodigo.Text = id.ToString();
            txtDescricao.Text = descricao;
            txtQtdParcelas.Text = qtdParcelas.ToString();
            txtJuros.Text = juros.ToString("F2");
            txtMulta.Text = multa.ToString("F2");
            txtDesconto.Text = desconto.ToString("F2");
            chkInativo.Checked = !ativo;
            lblDataCriacao.Text = dataCadastro.HasValue ? $"Criado em: {dataCadastro.Value:dd/MM/yyyy HH:mm}" : "Criado em: -";
            lblDataModificacao.Text = dataAlteracao.HasValue ? $"Modificado em: {dataAlteracao.Value:dd/MM/yyyy HH:mm}" : "Modificado em: -";
            await CarregarParcelas(id);
        }

        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            await AdicionarCondicaoeParcela();
        }

        private async Task CarregarParcelas(int condicaoPagamentoId)
        {
            try
            {
                listView1.Items.Clear();
                List<Parcelamento> parcelas = parcelaController.ListarParcelas(condicaoPagamentoId);
                foreach (var parcela in parcelas)
                {
                    ListViewItem item = new ListViewItem(parcela.NumParcela.ToString());
                    item.SubItems.Add(parcela.PrazoDias.ToString());
                    item.SubItems.Add(parcela.Porcentagem.ToString("N2") + "%");
                    string descricaoFormaPagamento = await formaPagamentoController.ObterDescricaoFormaPagamento(parcela.FormaPagamentoId); 
                    item.SubItems.Add(descricaoFormaPagamento);
                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar parcelas: " + ex.Message);
            }
        }

        private async Task AdicionarCondicaoeParcela()
        {
            if (modoExclusao)
            {
                var confirmacao = MessageBox.Show("Tem certeza que deseja excluir esta Condição de Pagamento?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmacao == DialogResult.Yes)
                {
                    try
                    {
                        int id = int.Parse(txtCodigo.Text);
                        await condicaoPagamentoController.Excluir(id); 
                        MessageBox.Show("Condição de pagamento excluída com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("Cannot delete or update a parent row"))
                        {
                            MessageBox.Show("Não é possível excluir este item, pois existem registros (clientes, fornecedores, etc.) vinculados a ele.", "Erro ao excluir", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show($"Erro ao excluir: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                if (!Validador.CampoObrigatorio(txtDescricao, "A descrição é obrigatória.")) return;
                if (!Validador.CampoObrigatorio(txtQtdParcelas, "A quantidade de parcelas é obrigatória.")) return;
                if (!Validador.CampoObrigatorio(txtJuros, "O Juros é obrigatório.")) return;
                if (!Validador.CampoObrigatorio(txtMulta, "A multa é obrigatória.")) return;
                if (!Validador.CampoObrigatorio(txtDesconto, "O desconto é obrigatório.")) return;
                if (!Validador.ValidarNumerico(txtQtdParcelas, "A quantidade de parcelas deve ser um número.")) return;
                decimal desconto = string.IsNullOrWhiteSpace(txtDesconto.Text) ? 0 : Convert.ToDecimal(txtDesconto.Text);
                if (!ValidarPorcentagemTotal(desconto)) return;
                int qtdParcelas = int.Parse(txtQtdParcelas.Text);
                if (qtdParcelas != listView1.Items.Count)
                {
                    MessageBox.Show("O número de parcelas não corresponde à quantidade definida.");
                    return;
                }

                try
                {
                    int id = string.IsNullOrEmpty(txtCodigo.Text) ? 0 : int.Parse(txtCodigo.Text);
                    string descricao = txtDescricao.Text;

                    var condicoes = await condicaoPagamentoController.ListarCondicaoPagamento(); // **CORRIGIDO COM AWAIT**

                    if (Validador.VerificarDuplicidade(condicoes, c => c.Descricao.Trim().Equals(descricao.Trim(), StringComparison.OrdinalIgnoreCase) && c.Id != id, "Já existe uma condição de pagamento com esta descrição."))
                    {
                        txtDescricao.Focus();
                        return;
                    }

                    CondicaoPagamento condicao = new CondicaoPagamento
                    {
                        Id = id,
                        Descricao = descricao,
                        QtdParcelas = qtdParcelas,
                        Juros = string.IsNullOrWhiteSpace(txtJuros.Text) ? 0 : Convert.ToDecimal(txtJuros.Text),
                        Multa = string.IsNullOrWhiteSpace(txtMulta.Text) ? 0 : Convert.ToDecimal(txtMulta.Text),
                        Desconto = desconto,
                        Ativo = !chkInativo.Checked,
                        DataCadastro = id == 0 ? DateTime.Now : DateTime.Parse(lblDataCriacao.Text.Replace("Criado em: ", "")),
                        DataAlteracao = DateTime.Now
                    };

                    List<Parcelamento> parcelas = new List<Parcelamento>();
                    foreach (ListViewItem item in listView1.Items)
                    {
                        int numParcela = int.Parse(item.SubItems[0].Text);
                        int prazoDias = int.Parse(item.SubItems[1].Text);
                        decimal porcentagem = decimal.Parse(item.SubItems[2].Text.Replace("%", ""));
                        string descricaoFormaPagamento = item.SubItems[3].Text;

                        int formaPagamentoId = await formaPagamentoController.ObterIdFormaPagamento(descricaoFormaPagamento); 

                        parcelas.Add(new Parcelamento
                        {
                            NumParcela = numParcela,
                            Porcentagem = porcentagem,
                            PrazoDias = prazoDias,
                            FormaPagamentoId = formaPagamentoId,
                            FormaPagamento = new FormaPagamento { Id = formaPagamentoId } 
                        });
                    }

                    await condicaoPagamentoController.Salvar(condicao, parcelas); 

                    MessageBox.Show("Condição de pagamento e parcelas salvas com sucesso!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);
                }
            }
        }

        private async void AtualizarListViewParcelas(List<Parcelamento> parcelasTemp)
        {
            try
            {
                listView1.Items.Clear();
                foreach (var parcela in parcelasTemp)
                {
                    string descricaoFormaPagamento = await formaPagamentoController.ObterDescricaoFormaPagamento(parcela.FormaPagamentoId); // **CORRIGIDO COM AWAIT**
                    ListViewItem item = new ListViewItem(parcela.NumParcela.ToString());
                    item.SubItems.Add(parcela.PrazoDias.ToString());
                    item.SubItems.Add(parcela.Porcentagem.ToString("F2") + "%");
                    item.SubItems.Add(descricaoFormaPagamento);
                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar a ListView: " + ex.Message);
            }
        }

        private void btnGerarParcelas_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtQtdParcelas.Text, out int qtdMaxParcelas) || qtdMaxParcelas <= 0) { MessageBox.Show("Informe uma quantidade de parcelas válida.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (listView1.Items.Count >= qtdMaxParcelas) { MessageBox.Show("Número máximo de parcelas atingido!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (string.IsNullOrEmpty(txtFormaPagamento.Text) || formaPagamentoSelecionadaId == -1) { MessageBox.Show("Selecione uma forma de pagamento válida!"); return; }
                if (!decimal.TryParse(txtPorcentagem.Text, out decimal porcentagem) || !int.TryParse(txtPrazoDias.Text, out int prazoDias)) { MessageBox.Show("Porcentagem e Prazo devem ser números válidos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (listView1.Items.Count > 0)
                {
                    var ultimaParcela = listView1.Items[listView1.Items.Count - 1];
                    int prazoUltimaParcela = int.Parse(ultimaParcela.SubItems[1].Text);
                    if (prazoDias <= prazoUltimaParcela)
                    {
                        MessageBox.Show("O prazo da nova parcela deve ser maior que o prazo da parcela anterior.", "Prazo inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                ListViewItem item = new ListViewItem((listView1.Items.Count + 1).ToString());
                item.SubItems.Add(prazoDias.ToString());
                item.SubItems.Add(porcentagem.ToString("F2") + "%");
                item.SubItems.Add(txtFormaPagamento.Text);
                listView1.Items.Add(item);
            }
            catch (Exception ex) { MessageBox.Show("Erro ao adicionar parcela: " + ex.Message); }
        }

        private void btnEditarParcela_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtNumParcela.Text, out int numParcelaEditada)) { MessageBox.Show("Número da parcela inválido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                ListViewItem itemEditado = listView1.Items.Cast<ListViewItem>().FirstOrDefault(item => int.Parse(item.Text) == numParcelaEditada);
                if (itemEditado != null)
                {
                    itemEditado.SubItems[1].Text = txtPrazoDias.Text;
                    itemEditado.SubItems[2].Text = Convert.ToDecimal(txtPorcentagem.Text).ToString("F2") + "%";
                    itemEditado.SubItems[3].Text = txtFormaPagamento.Text;
                }
                else { MessageBox.Show("Parcela não encontrada."); }
            }
            catch (Exception ex) { MessageBox.Show("Erro ao editar parcela: " + ex.Message); }
        }

        private bool ValidarPorcentagemTotal(decimal desconto)
        {
            decimal somaPorcentagens = 0;
            foreach (ListViewItem item in listView1.Items)
            {
                somaPorcentagens += decimal.Parse(item.SubItems[2].Text.Replace("%", "").Trim());
            }
            if (Math.Round(somaPorcentagens + desconto, 2) != 100)
            {
                MessageBox.Show($"A soma das porcentagens das parcelas é {somaPorcentagens:N2}% e o desconto é {desconto:N2}%. O total deve ser exatamente 100%.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnRemoverParcela_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count > 0)
            {
                listView1.Items.RemoveAt(listView1.Items.Count - 1);
            }
            else
            {
                MessageBox.Show("Não há parcelas para remover.");
            }
        }

        private void btnFormaPagamento_Click(object sender, EventArgs e)
        {
            frmConsultaFrmPgto consultaFrmPgto = new frmConsultaFrmPgto();
            consultaFrmPgto.ModoSelecao = true;
            if (consultaFrmPgto.ShowDialog() == DialogResult.OK && consultaFrmPgto.FormaSelecionada != null)
            {
                txtFormaPagamento.Text = consultaFrmPgto.FormaSelecionada.Descricao;
                formaPagamentoSelecionadaId = consultaFrmPgto.FormaSelecionada.Id;
            }
        }

        private void frmCadastroCondPgto_Load(object sender, EventArgs e)
        {
            if (modoExclusao)
            {
                btnSalvar.Text = "Deletar";
                txtDescricao.Enabled = false;
                txtQtdParcelas.Enabled = false;
                txtJuros.Enabled = false;
                txtMulta.Enabled = false;
                txtDesconto.Enabled = false;
                chkInativo.Enabled = false;
                txtNumParcela.Enabled = false;
                txtPorcentagem.Enabled = false;
                txtPrazoDias.Enabled = false;
                txtFormaPagamento.Enabled = false;
                btnFormaPagamento.Enabled = false;
                btnGerarParcelas.Enabled = false;
                btnEditarParcela.Enabled = false;
                btnRemoverParcela.Enabled = false;
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