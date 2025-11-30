using Projeto.Controller;
using Projeto.Models;
using Projeto.Views.Cadastros;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Projeto.Views.Consultas
{
    public partial class frmConsultaContasAReceber : Projeto.frmBaseConsulta
    {
        private ContasAReceberController controller = new ContasAReceberController();
        private List<ContasAReceber> listaOriginal = new List<ContasAReceber>();

        public frmConsultaContasAReceber()
        {
            InitializeComponent();
            ConfigurarListView();
            ConfigurarEventosBotoes();
            ConfigurarFiltros();
        }

        private void frmConsultaContasAReceber_Load(object sender, EventArgs e)
        {
            Pesquisar();
        }

        private void ConfigurarListView()
        {
            this.listView1.FullRowSelect = true;
            this.listView1.MultiSelect = false;
            this.listView1.View = View.Details;
        }

        private void ConfigurarEventosBotoes()
        {
            if (this.Controls.Find("btnIncluir", true).Length > 0)
            {
                Button btnBaixar = (Button)this.Controls.Find("btnIncluir", true)[0];
                btnBaixar.Text = "Baixar";
                btnBaixar.Click += (s, e) => ExecutarBaixa();
            }

            if (this.Controls.Find("btnEditar", true).Length > 0)
            {
                Button btnVisualizar = (Button)this.Controls.Find("btnEditar", true)[0];
                btnVisualizar.Text = "Visualizar";
                btnVisualizar.Click += (s, e) => ExecutarVisualizacao();
            }

            if (this.Controls.Find("btnDeletar", true).Length > 0)
            {
                Button btnExcluir = (Button)this.Controls.Find("btnDeletar", true)[0];
                btnExcluir.Click += (s, e) => BloquearExclusao();
            }

            if (this.Controls.Find("btnPesquisar", true).Length > 0)
            {
                Button btnPesquisar = (Button)this.Controls.Find("btnPesquisar", true)[0];
                btnPesquisar.Click += (s, e) => Pesquisar();
            }
        }

        private void ConfigurarFiltros()
        {
            Control[] chkAberto = this.Controls.Find("chkAberta", true);
            if (chkAberto.Length > 0) ((CheckBox)chkAberto[0]).CheckedChanged += (s, e) => Pesquisar();

            Control[] chkPaga = this.Controls.Find("chkPaga", true);
            if (chkPaga.Length > 0) ((CheckBox)chkPaga[0]).CheckedChanged += (s, e) => Pesquisar();

            Control[] chkCancelada = this.Controls.Find("chkCancelada", true);
            if (chkCancelada.Length > 0) ((CheckBox)chkCancelada[0]).CheckedChanged += (s, e) => Pesquisar();
        }

        public void AtualizaLista()
        {
            Pesquisar();
        }

        public void Pesquisar()
        {
            try
            {
                listView1.Items.Clear();

                string busca = "";
                if (this.Controls.Find("txtPesquisar", true).Length > 0)
                    busca = this.Controls.Find("txtPesquisar", true)[0].Text;

                List<string> statusFiltro = new List<string>();

                CheckBox chkAberta = this.Controls.Find("chkAberta", true).FirstOrDefault() as CheckBox;
                CheckBox chkPaga = this.Controls.Find("chkPaga", true).FirstOrDefault() as CheckBox;

                if (chkAberta != null && chkAberta.Checked) statusFiltro.Add("Aberta");
                if (chkPaga != null && chkPaga.Checked) statusFiltro.Add("Paga");

                if (statusFiltro.Count == 0 && chkAberta == null)
                {
                    statusFiltro.Add("Aberta");
                    statusFiltro.Add("Paga");
                }
                else if (statusFiltro.Count == 0)
                {
                    return; 
                }

                listaOriginal = controller.Listar(statusFiltro, busca);

                foreach (var conta in listaOriginal)
                {
                    ListViewItem item = new ListViewItem("");
                    item.SubItems.Add(conta.VendaModelo);
                    item.SubItems.Add(conta.VendaSerie);                                    
                    item.SubItems.Add(conta.VendaNumeroNota.ToString());                    
                    item.SubItems.Add(conta.NumeroParcela.ToString());                      
                    item.SubItems.Add(conta.ClienteId.ToString());                          
                    item.SubItems.Add(conta.NomeCliente);                                   
                    item.SubItems.Add(conta.ValorVencimento.ToString("F2"));                
                    item.SubItems.Add(conta.NomeFormaPagamento ?? "");                      
                    item.SubItems.Add(conta.DataEmissao.ToString("dd/MM/yyyy"));            
                    item.SubItems.Add(conta.DataVencimento.ToString("dd/MM/yyyy"));         
                    item.SubItems.Add(conta.Ativo ? "Sim" : "Não");                         
                    item.SubItems.Add(conta.Status);                                        

                    item.SubItems.Add(conta.DataPagamento.HasValue ? conta.DataPagamento.Value.ToString("dd/MM/yyyy") : "");

                    item.SubItems.Add(conta.ValorPago.HasValue ? conta.ValorPago.Value.ToString("F2") : "");

                    item.Tag = conta;
                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao pesquisar: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExecutarBaixa()
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecione uma conta na lista para realizar a baixa.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ListViewItem item = listView1.SelectedItems[0];
            ContasAReceber contaSelecionada = (ContasAReceber)item.Tag;

            if (contaSelecionada.Status == "Paga")
            {
                MessageBox.Show("Esta conta já foi baixada/paga.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!contaSelecionada.Ativo)
            {
                MessageBox.Show("Esta conta está cancelada e não pode ser baixada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (contaSelecionada.NumeroParcela > 1)
            {
                bool parcelaAnteriorPendente = listaOriginal.Any(c =>
                    c.VendaModelo == contaSelecionada.VendaModelo &&
                    c.VendaSerie == contaSelecionada.VendaSerie &&
                    c.VendaNumeroNota == contaSelecionada.VendaNumeroNota &&
                    c.ClienteId == contaSelecionada.ClienteId &&
                    c.NumeroParcela < contaSelecionada.NumeroParcela &&
                    c.Status == "Aberta" &&
                    c.Ativo == true
                );

                if (parcelaAnteriorPendente)
                {
                    MessageBox.Show("Não é possível baixar esta parcela.\nExiste uma parcela anterior desta venda que ainda está em aberto.",
                                    "Sequência Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            using (frmCadastroContasAReceber frm = new frmCadastroContasAReceber())
            {
                frm.ModoBaixa = true;
                frm.ConhecaObj(contaSelecionada, controller);

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Pesquisar(); 
                }
            }
        }

        private void ExecutarVisualizacao()
        {
            if (listView1.SelectedItems.Count == 0) return;

            ListViewItem item = listView1.SelectedItems[0];
            ContasAReceber conta = (ContasAReceber)item.Tag;

            using (frmCadastroContasAReceber frm = new frmCadastroContasAReceber())
            {
                frm.ModoVisualizacao = true;
                frm.ConhecaObj(conta, controller);
                frm.ShowDialog();
            }
        }

        private void BloquearExclusao()
        {
            MessageBox.Show("Para cancelar esta cobrança, você deve cancelar a Nota Fiscal de Venda inteira na tela de Consulta de Vendas.",
                            "Ação Não Permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}