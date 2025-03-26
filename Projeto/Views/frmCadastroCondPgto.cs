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


        public frmCadastroCondPgto()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
        }
        private void frmCadastroCondPgto_Load(object sender, EventArgs e)
        {
            CarregarFormasDePagamento();
        }
        public void CarregarCondicaoPagamento(int id, string descricao, int qtdParcelas)
        {
            txtCodigo.Text = id.ToString();
            txtDescricao.Text = descricao.ToString();
            txtQtdParcelas.Text = qtdParcelas.ToString();

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


        private void CarregarFormasDePagamento()
        {
            try
            {
                List<FormaPagamento> listaFormasPagamento = formaPagamentoController.ListarFormaPagamento();

                cbFormaPagamento.DisplayMember = "Descricao";  
                cbFormaPagamento.ValueMember = "Id";           
                cbFormaPagamento.DataSource = listaFormasPagamento; 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar formas de pagamento: " + ex.Message);
            }
        }


        private void AdicionarCondicaoeParcela()
        {
            try
            {
                int id = string.IsNullOrEmpty(txtCodigo.Text) ? 0 : int.Parse(txtCodigo.Text);
                string descricao = txtDescricao.Text;
                int qtdParcelas = int.Parse(txtQtdParcelas.Text);

                CondicaoPagamento condicao = new CondicaoPagamento
                {
                    Id = id,
                    Descricao = descricao,
                    QtdParcelas = qtdParcelas
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
                int formaPagamentoId = Convert.ToInt32(cbFormaPagamento.SelectedValue);
                string descricaoFormaPagamento = formaPagamentoController.ObterDescricaoFormaPagamento(formaPagamentoId);

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
                    int novaFormaPagamentoId = Convert.ToInt32(cbFormaPagamento.SelectedValue);

                    string descricaoFormaPagamento = formaPagamentoController.ObterDescricaoFormaPagamento(novaFormaPagamentoId);

                    itemEditado.SubItems[1].Text = novoPrazoDias.ToString();
                    itemEditado.SubItems[2].Text = novaPorcentagem.ToString("F2") + "%";
                    itemEditado.SubItems[3].Text = descricaoFormaPagamento; 
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

        
    }
}
