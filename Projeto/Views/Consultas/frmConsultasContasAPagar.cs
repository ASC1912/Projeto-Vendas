using Projeto.Controller;
using Projeto.Models;
using Projeto.Views.Cadastros;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto.Views.Consultas
{
    public partial class frmConsultasContasAPagar : Projeto.frmBaseConsulta
    {
        private ContasAPagarController controller = new ContasAPagarController();
        private frmCadastroContasAPagar oFrmCadastroContasAPagar;
        private bool estaCarregando = false; 

        public frmConsultasContasAPagar()
        {
            InitializeComponent();

            btnIncluir.Text = "Lançar Manual";
            btnEditar.Text = "Pagar/Baixar";
            btnDeletar.Text = "Estornar Pag.";

            oFrmCadastroContasAPagar = new frmCadastroContasAPagar();
        }

        public override void setFrmCadastro(object obj)
        {
            if (obj is frmCadastroContasAPagar frm)
            {
                oFrmCadastroContasAPagar = frm;
            }
        }

        private async Task CarregarContas()
        {
            //Para evitar duplicação de dados
            if (estaCarregando) return; 
            estaCarregando = true;      
                                        

            try
            {
                string statusSelecionado = cbStatus.SelectedItem?.ToString() ?? "Aberta";
                string termoBusca = txtPesquisar.Text;

                listView1.Items.Clear(); 

                List<ContasAPagar> contas = await controller.Listar(statusSelecionado, termoBusca); 

                listView1.BeginUpdate(); 
                foreach (var conta in contas)
                {
                    ListViewItem item = new ListViewItem(conta.NumeroParcela.ToString());
                    item.SubItems.Add(conta.Descricao ?? "");
                    item.SubItems.Add(conta.CompraModelo ?? "");
                    item.SubItems.Add(conta.CompraSerie ?? "");
                    item.SubItems.Add(conta.CompraNumeroNota.ToString());
                    item.SubItems.Add(conta.NomeFornecedor ?? "");
                    item.SubItems.Add(conta.ValorVencimento.ToString("C2"));
                    item.SubItems.Add(conta.NomeFormaPagamento ?? "");
                    item.SubItems.Add(conta.DataEmissao.ToShortDateString());
                    item.SubItems.Add(conta.DataVencimento.ToShortDateString());
                    item.SubItems.Add(conta.Ativo ? "Sim" : "Não");
                    item.SubItems.Add(conta.Status ?? "");
                    item.SubItems.Add(conta.DataPagamento?.ToShortDateString() ?? "");
                    item.SubItems.Add(conta.ValorPago?.ToString("C2") ?? "");
                    item.Tag = conta;
                    listView1.Items.Add(item);
                }
                listView1.EndUpdate(); 
            }
            catch (Exception ex)
            {
                if (listView1.InvokeRequired)
                {
                    listView1.Invoke((MethodInvoker)delegate { listView1.EndUpdate(); });
                }
                else
                {
                    listView1.EndUpdate();
                }
                MessageBox.Show("Erro ao carregar contas: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally // Libera a trava ao final
            {
                estaCarregando = false; // <<< LIBERA A TRAVA para futuras chamadas
            }
        }

        // --- EVENTOS ---

        private async void frmConsultasContasAPagar_Load(object sender, EventArgs e)
        {
            if (cbStatus.Items.Count > 0 && cbStatus.SelectedIndex == -1)
            {
                cbStatus.SelectedItem = "Aberta";
            }
            else if (cbStatus.Items.Count == 0) 
            {
                cbStatus.Items.AddRange(new object[] { "Aberta", "Paga", "Cancelada", "Todas" });
                cbStatus.SelectedItem = "Aberta";
            }

            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;

            await CarregarContas();
        }

        public async void btnPesquisar_Click(object sender, EventArgs e) 
        {
            await CarregarContas();
        }

        private async void cbStatus_SelectedIndexChanged(object sender, EventArgs e) 
        {
            await CarregarContas();
        }

        public async void btnIncluir_Click(object sender, EventArgs e) 
        {
            if (oFrmCadastroContasAPagar == null || oFrmCadastroContasAPagar.IsDisposed)
            {
                oFrmCadastroContasAPagar = new frmCadastroContasAPagar();
            }

            oFrmCadastroContasAPagar.ConhecaObj(null, controller);
            oFrmCadastroContasAPagar.LimparTxt();
            oFrmCadastroContasAPagar.DefinirModo("Lancamento");
            oFrmCadastroContasAPagar.ShowDialog();

            await CarregarContas();
        }

        public async void btnEditar_Click(object sender, EventArgs e) 
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecione uma conta para pagar/baixar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ContasAPagar contaSelecionada = (ContasAPagar)listView1.SelectedItems[0].Tag;

            if (contaSelecionada.Status == "Paga")
            {
                MessageBox.Show("Esta conta já foi paga.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (contaSelecionada.Status == "Cancelada" || !contaSelecionada.Ativo)
            {
                MessageBox.Show("Esta conta está cancelada e não pode ser paga.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (oFrmCadastroContasAPagar == null || oFrmCadastroContasAPagar.IsDisposed)
            {
                oFrmCadastroContasAPagar = new frmCadastroContasAPagar();
            }

            oFrmCadastroContasAPagar.ConhecaObj(contaSelecionada, controller);
            oFrmCadastroContasAPagar.CarregaTxt();
            oFrmCadastroContasAPagar.DefinirModo("Pagamento"); 
            oFrmCadastroContasAPagar.ShowDialog();

            await CarregarContas();
        }

        public async void btnDeletar_Click(object sender, EventArgs e) 
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecione uma conta paga para estornar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ContasAPagar contaSelecionada = (ContasAPagar)listView1.SelectedItems[0].Tag;

            if (contaSelecionada.Status != "Paga")
            {
                MessageBox.Show("Apenas contas com status 'Paga' podem ser estornadas.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (contaSelecionada.CompraModelo != "MANUAL")
            {
                MessageBox.Show("Contas geradas por Notas de Compra não podem ser estornadas individualmente.\nCancele a Nota de Compra.", "Operação Inválida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var confirmacao = MessageBox.Show($"Deseja realmente estornar o pagamento da conta '{contaSelecionada.Descricao}'?",
                                             "Confirmação de Estorno", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacao == DialogResult.Yes)
            {
                try
                {
                    await controller.Estornar(contaSelecionada);
                    MessageBox.Show("Pagamento estornado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await CarregarContas();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao estornar pagamento: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                btnEditar_Click(sender, e); 
            }
        }
    }
}