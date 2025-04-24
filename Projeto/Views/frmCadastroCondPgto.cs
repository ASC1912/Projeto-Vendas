using MySql.Data.MySqlClient;
using Projeto.Controller;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Projeto
{
    public partial class frmCadastroCondPgto : Projeto.frmBase
    {
        private ParcelaController parcelaController = new ParcelaController(); 
        private FormaPagamentoController formaPagamentoController = new FormaPagamentoController();
        private int formaPagamentoSelecionadaId = -1;


        public frmCadastroCondPgto()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
            txtFormaPagamento.ReadOnly = true;
        }
        public void CarregarCondicaoPagamento(int id, string descricao, int qtdParcelas, decimal juros, decimal multa, decimal desconto)
        {
            txtCodigo.Text = id.ToString();
            txtDescricao.Text = descricao;
            txtQtdParcelas.Text = qtdParcelas.ToString();
            txtJuros.Text = juros.ToString("F2");
            txtMulta.Text = multa.ToString("F2");
            txtDesconto.Text = desconto.ToString("F2");

            CarregarParcelas(id);
        }
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            AdicionarCondicaoeParcela();
        }


        private void CarregarParcelas(int condicaoPagamentoId)
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
                    string descricaoFormaPagamento = formaPagamentoController.ObterDescricaoFormaPagamento(parcela.FormaPagamentoId);

                    item.SubItems.Add(descricaoFormaPagamento);
                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar parcelas: " + ex.Message);
            }
        }


        private void AdicionarCondicaoeParcela()
        {
            try
            {
                decimal desconto = string.IsNullOrWhiteSpace(txtDesconto.Text) ? 0 : Convert.ToDecimal(txtDesconto.Text);

                if (!ValidarPorcentagemTotal(desconto))
                {
                    return;
                }

                int id = string.IsNullOrEmpty(txtCodigo.Text) ? 0 : int.Parse(txtCodigo.Text);
                string descricao = txtDescricao.Text;
                int qtdParcelas = int.Parse(txtQtdParcelas.Text);
                int qtdParcelasView = listView1.Items.Count;
                decimal juros = string.IsNullOrWhiteSpace(txtJuros.Text) ? 0 : Convert.ToDecimal(txtJuros.Text);
                decimal multa = string.IsNullOrWhiteSpace(txtMulta.Text) ? 0 : Convert.ToDecimal(txtMulta.Text);

                if (qtdParcelas != qtdParcelasView)
                {
                    MessageBox.Show("O número de parcelas não corresponde a quantidade definida");
                    return;
                }

                CondicaoPagamento condicao = new CondicaoPagamento
                {
                    Id = id,
                    Descricao = descricao,
                    QtdParcelas = qtdParcelas,
                    Juros = juros,
                    Multa = multa,
                    Desconto = desconto
                };

                List<Parcelamento> parcelas = new List<Parcelamento>();
                FormaPagamentoController formaPagamentoController = new FormaPagamentoController();

                foreach (ListViewItem item in listView1.Items)
                {
                    int numParcela = int.Parse(item.SubItems[0].Text);
                    int prazoDias = int.Parse(item.SubItems[1].Text);
                    decimal porcentagem = decimal.Parse(item.SubItems[2].Text.Replace("%", ""));
                    string descricaoFormaPagamento = item.SubItems[3].Text;

                    int formaPagamentoId = formaPagamentoController.ObterIdFormaPagamento(descricaoFormaPagamento);

                    Parcelamento parcela = new Parcelamento
                    {
                        NumParcela = numParcela,
                        Porcentagem = porcentagem,
                        PrazoDias = prazoDias,
                        FormaPagamentoId = formaPagamentoId
                    };

                    parcelas.Add(parcela);
                }

                CondicaoPagamentoController controller = new CondicaoPagamentoController();
                controller.Salvar(condicao, parcelas);

                MessageBox.Show("Condição de pagamento e parcelas salvas com sucesso!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }


        private void AtualizarListViewParcelas(List<Parcelamento> parcelasTemp)
        {
            try
            {
                listView1.Items.Clear(); 

                foreach (var parcela in parcelasTemp)
                {
                    string descricaoFormaPagamento = formaPagamentoController.ObterDescricaoFormaPagamento(parcela.FormaPagamentoId);

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
                int qtdMaxParcelas = Convert.ToInt32(txtQtdParcelas.Text);
                int numParcelasAtuais = listView1.Items.Count;

                if (numParcelasAtuais >= qtdMaxParcelas)
                {
                    MessageBox.Show("Número máximo de parcelas atingido!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int numParcela = numParcelasAtuais + 1;
                decimal porcentagem = Convert.ToDecimal(txtPorcentagem.Text);
                int prazoDias = Convert.ToInt32(txtPrazoDias.Text);

                if (string.IsNullOrEmpty(txtFormaPagamento.Text) || formaPagamentoSelecionadaId == -1)
                {
                    MessageBox.Show("Selecione uma forma de pagamento válida!");
                    return;
                }

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

                string descricaoFormaPagamento = txtFormaPagamento.Text;

                ListViewItem item = new ListViewItem(numParcela.ToString());
                item.SubItems.Add(prazoDias.ToString());
                item.SubItems.Add(porcentagem.ToString("F2") + "%");
                item.SubItems.Add(descricaoFormaPagamento);

                listView1.Items.Add(item);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar parcela: " + ex.Message);
            }

        }


        private void btnEditarParcela_Click(object sender, EventArgs e)
        {
            try
            {
                int numParcelaEditada = Convert.ToInt32(txtNumParcela.Text);

                ListViewItem itemEditado = listView1.Items.Cast<ListViewItem>().FirstOrDefault(item => Convert.ToInt32(item.Text) == numParcelaEditada);

                if (itemEditado != null)
                {
                    decimal novaPorcentagem = Convert.ToDecimal(txtPorcentagem.Text);
                    int novoPrazoDias = Convert.ToInt32(txtPrazoDias.Text);
                    string NovoDescricaoFormaPagamento = txtFormaPagamento.Text;

                    itemEditado.SubItems[1].Text = novoPrazoDias.ToString();
                    itemEditado.SubItems[2].Text = novaPorcentagem.ToString("F2") + "%";
                    itemEditado.SubItems[3].Text = NovoDescricaoFormaPagamento;
                }

                else
                {
                    MessageBox.Show("Parcela não encontrada.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao editar parcela: " + ex.Message);
            }
        }

        private bool ValidarPorcentagemTotal(decimal desconto)
        {
            decimal somaPorcentagens = 0;

            foreach (ListViewItem item in listView1.Items)
            {
                string porcentagemTexto = item.SubItems[2].Text.Replace("%", "").Trim();
                if (decimal.TryParse(porcentagemTexto, out decimal porcentagem))
                {
                    somaPorcentagens += porcentagem;
                }
                else
                {
                    MessageBox.Show("Erro ao converter a porcentagem de uma parcela.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            decimal totalComDesconto = somaPorcentagens + desconto;

            if (Math.Round(totalComDesconto, 2) != 100)
            {
                MessageBox.Show(
                    $"A soma das porcentagens das parcelas é {somaPorcentagens:N2}% e o desconto é {desconto:N2}%. " +
                    $"O total deve ser exatamente 100%.",
                    "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            var resultado = consultaFrmPgto.ShowDialog();

            if (resultado == DialogResult.OK && consultaFrmPgto.FormaSelecionada != null)
            {
                txtFormaPagamento.Text = consultaFrmPgto.FormaSelecionada.Descricao;
                formaPagamentoSelecionadaId = consultaFrmPgto.FormaSelecionada.Id;
            }

        }
    }
}
