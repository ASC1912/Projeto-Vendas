using Projeto.Controller;
using Projeto.Models;
using Projeto.Views.Cadastros;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

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
            btnDeletar.Text = "Estornar";

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
            finally 
            {
                estaCarregando = false; // Libera a trava para futuras chamadas
                btnDeletar.Text = "Estornar";
            }
        }



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
                MessageBox.Show("Selecione uma conta na lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ContasAPagar contaSelecionada = (ContasAPagar)listView1.SelectedItems[0].Tag;

            try
            {
                switch (contaSelecionada.Status)
                {
                    case "Paga":
                        var confirmEstorno = MessageBox.Show($"Deseja realmente ESTORNAR o pagamento da conta '{contaSelecionada.Descricao}'?",
                                                             "Confirmação de Estorno", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (confirmEstorno == DialogResult.Yes)
                        {
                            await controller.Estornar(contaSelecionada);
                            MessageBox.Show("Pagamento estornado com sucesso! A conta está 'Aberta' novamente.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            await CarregarContas(); 
                        }
                        break;

                    case "Aberta":

                        string motivo = ""; 
                        using (Form prompt = new Form())
                        {
                            prompt.Width = 500;
                            prompt.Height = 250;
                            prompt.Text = "Motivo do Cancelamento";
                            prompt.StartPosition = FormStartPosition.CenterParent;
                            prompt.FormBorderStyle = FormBorderStyle.FixedDialog;
                            prompt.MinimizeBox = false;
                            prompt.MaximizeBox = false;

                            Label textLabel = new Label() { Left = 50, Top = 20, Width = 400, Text = "Para cancelar esta conta manual, digite o motivo:" };
                            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400, Height = 80, Multiline = true, ScrollBars = ScrollBars.Vertical, MaxLength = 255 };
                            Button confirmation = new Button() { Text = "Confirmar", Left = 240, Width = 100, Top = 150, DialogResult = DialogResult.OK };
                            Button cancel = new Button() { Text = "Voltar", Left = 350, Width = 100, Top = 150, DialogResult = DialogResult.Cancel };

                            // Habilita o "Confirmar" apenas se houver texto
                            confirmation.Enabled = false;
                            textBox.TextChanged += (s, ev) => { confirmation.Enabled = !string.IsNullOrWhiteSpace(textBox.Text); };

                            prompt.Controls.AddRange(new Control[] { textLabel, textBox, confirmation, cancel });
                            prompt.AcceptButton = confirmation;
                            prompt.CancelButton = cancel;

                            if (prompt.ShowDialog() == DialogResult.OK)
                            {
                                motivo = textBox.Text; 
                            }
                            else
                            {
                                MessageBox.Show("Cancelamento abortado.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return; 
                            }
                        } 
                        await controller.CancelarManual(contaSelecionada, motivo);
                        MessageBox.Show("Conta cancelada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await CarregarContas(); 

                        break;

                    case "Cancelada":
                        MessageBox.Show("Esta conta já está cancelada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    default:
                        MessageBox.Show($"O status '{contaSelecionada.Status}' não permite esta operação.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                }
            }
            catch (InvalidOperationException opEx) 
            {
                MessageBox.Show(opEx.Message, "Operação Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Erro ao processar a solicitação: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                btnEditar_Click(sender, e); 
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ContasAPagar contaSelecionada = (ContasAPagar)listView1.SelectedItems[0].Tag;

                if (contaSelecionada.Status == "Paga")
                {
                    btnDeletar.Text = "Estornar";
                }
                else if (contaSelecionada.Status == "Aberta")
                {
                    btnDeletar.Text = "Cancelar";
                }
                else
                {
                    btnDeletar.Text = "Estornar";
                }
            }
            else
            {
                btnDeletar.Text = "Estornar";
            }
        }
    }
}