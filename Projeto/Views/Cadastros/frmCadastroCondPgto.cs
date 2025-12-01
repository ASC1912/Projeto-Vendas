using Projeto.Controller;
using Projeto.DAO;
using Projeto.Models;
using Projeto.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto
{
    public partial class frmCadastroCondPgto : Projeto.frmBase
    {
        Parcelamento aParcela;
        CondicaoPagamento aCondPgto;
        frmConsultaFrmPgto oFrmConsultaFrmPgto;
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

            this.txtQtdParcelas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress_ApenasNumerosInteiros);
            this.txtNumParcela.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress_ApenasNumerosInteiros);
            this.txtPrazoDias.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress_ApenasNumerosInteiros);
            this.txtIdFormaPagamento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress_ApenasNumerosInteiros);

            this.txtJuros.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress_ApenasNumerosEVirgula);
            this.txtMulta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress_ApenasNumerosEVirgula);
            this.txtDesconto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress_ApenasNumerosEVirgula);
            this.txtPorcentagem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress_ApenasNumerosEVirgula);
        }

        public override void ConhecaObj(object obj, object ctrl)
        {
            if (obj != null)
            {
                aCondPgto = (CondicaoPagamento)obj;
            }
            if (ctrl != null)
            {
                condicaoPagamentoController = (CondicaoPagamentoController)ctrl;
            }
        }
        public void setFrmConsultaFormaPagamento(object obj)
        {
            if (obj != null)
            {
                oFrmConsultaFrmPgto = (frmConsultaFrmPgto)obj;
            }
        }

        public override async void CarregaTxt()
        {
            txtCodigo.Text = aCondPgto.Id.ToString();
            txtDescricao.Text = aCondPgto.Descricao;
            txtQtdParcelas.Text = aCondPgto.QtdParcelas.ToString();
            txtJuros.Text = aCondPgto.Juros.ToString("F2");
            txtMulta.Text = aCondPgto.Multa.ToString("F2");
            txtDesconto.Text = aCondPgto.Desconto.ToString("F2");
            chkInativo.Checked = !aCondPgto.Ativo;
            lblDataCriacao.Text = aCondPgto.DataCadastro.HasValue ? $"Criado em: {aCondPgto.DataCadastro.Value:dd/MM/yyyy HH:mm}" : "Criado em: -";
            lblDataModificacao.Text = aCondPgto.DataAlteracao.HasValue ? $"Modificado em: {aCondPgto.DataAlteracao.Value:dd/MM/yyyy HH:mm}" : "Modificado em: -";

            listView1.Items.Clear();

            if (aCondPgto.Parcelas != null)
            {
                foreach (var parcela in aCondPgto.Parcelas)
                {
                    ListViewItem item = new ListViewItem(parcela.NumParcela.ToString());
                    item.SubItems.Add(parcela.PrazoDias.ToString());
                    item.SubItems.Add(parcela.Porcentagem.ToString("N2") + "%");

                    string descricaoFormaPagamento = parcela.FormaPagamento?.Descricao ?? "N/D";
                    item.SubItems.Add(descricaoFormaPagamento);

                    listView1.Items.Add(item);
                }
            }
        }
        public override void BloquearTxt()
        {
            txtDescricao.Enabled = false;
            txtQtdParcelas.Enabled = false;
            txtJuros.Enabled = false;
            txtMulta.Enabled = false;
            txtDesconto.Enabled = false;
            chkInativo.Enabled = false;

            txtNumParcela.Enabled = false;
            txtPorcentagem.Enabled = false;
            txtPrazoDias.Enabled = false;
            txtIdFormaPagamento.Enabled = false;
            txtFormaPagamento.Enabled = false;
            btnFormaPagamento.Enabled = false;
            btnGerarParcelas.Enabled = false;
            btnEditarParcela.Enabled = false;
            btnRemoverParcela.Enabled = false;
        }

        public override void DesbloquearTxt()
        {
            txtDescricao.Enabled = true;
            txtQtdParcelas.Enabled = true;
            txtJuros.Enabled = true;
            txtMulta.Enabled = true;
            txtDesconto.Enabled = true;
            chkInativo.Enabled = true;

            txtNumParcela.Enabled = true;
            txtPorcentagem.Enabled = true;
            txtPrazoDias.Enabled = true;
            txtIdFormaPagamento.Enabled = true;
            txtFormaPagamento.Enabled = true;
            btnFormaPagamento.Enabled = true;
            btnGerarParcelas.Enabled = true;
            btnEditarParcela.Enabled = true;
            btnRemoverParcela.Enabled = true;
        }

        public override void LimparTxt()
        {
            txtCodigo.Text = "0";
            txtDescricao.Clear();
            txtQtdParcelas.Clear();
            txtJuros.Clear();
            txtMulta.Clear();
            txtDesconto.Clear();
            chkInativo.Checked = false;

            txtNumParcela.Clear();
            txtPorcentagem.Clear();
            txtPrazoDias.Clear();
            txtIdFormaPagamento.Clear();
            txtFormaPagamento.Clear();
            formaPagamentoSelecionadaId = -1;
            listView1.Items.Clear();

            DateTime agora = DateTime.Now;
            lblDataCriacao.Text = $"Criado em: {agora:dd/MM/yyyy HH:mm}";
            lblDataModificacao.Text = $"Modificado em: {agora:dd/MM/yyyy HH:mm}";
        }

        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            await AdicionarCondicaoeParcela();
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
                if (!ValidarPorcentagemTotal()) return;
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

                    var condicoes = await condicaoPagamentoController.ListarCondicaoPagamento();

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

        private bool ValidarPorcentagemTotal() 
        {
            decimal somaPorcentagens = 0;
            foreach (ListViewItem item in listView1.Items)
            {
                somaPorcentagens += decimal.Parse(item.SubItems[2].Text.Replace("%", "").Trim());
            }

            if (Math.Round(somaPorcentagens, 2) != 100)
            {
                MessageBox.Show($"A soma das porcentagens das parcelas é {somaPorcentagens:N2}%. O total deve ser exatamente 100%.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            oFrmConsultaFrmPgto.ModoSelecao = true;
            if (oFrmConsultaFrmPgto.ShowDialog() == DialogResult.OK && oFrmConsultaFrmPgto.FormaSelecionada != null)
            {
                txtFormaPagamento.Text = oFrmConsultaFrmPgto.FormaSelecionada.Descricao;
                txtIdFormaPagamento.Text = oFrmConsultaFrmPgto.FormaSelecionada.Id.ToString();
                formaPagamentoSelecionadaId = oFrmConsultaFrmPgto.FormaSelecionada.Id;
            }
        }
        private async void txtIdFormaPagamento_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdFormaPagamento.Text))
            {
                txtFormaPagamento.Text = "";
                formaPagamentoSelecionadaId = -1;
                return;
            }

            if (!int.TryParse(txtIdFormaPagamento.Text, out int id))
            {
                MessageBox.Show("O campo ID Forma de Pagamento deve conter apenas números.");
                txtFormaPagamento.Text = "";
                formaPagamentoSelecionadaId = -1;
                return;
            }

            var formaPagamento = await formaPagamentoController.BuscarPorId(id);

            if (formaPagamento != null)
            {
                txtFormaPagamento.Text = formaPagamento.Descricao;
                formaPagamentoSelecionadaId = formaPagamento.Id;
            }
            else
            {
                MessageBox.Show("Forma de Pagamento não encontrada.");
                txtFormaPagamento.Text = "";
                formaPagamentoSelecionadaId = -1;
            }
        }


        private void frmCadastroCondPgto_Load(object sender, EventArgs e)
        {
        }
    }
}