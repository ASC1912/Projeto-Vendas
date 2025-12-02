using Projeto.Controller;
using Projeto.Models;
using Projeto.Views.Cadastros; 
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Projeto.Views.Consultas
{
    public partial class frmConsultaVenda : Projeto.frmBaseConsulta
    {
        private VendaController controller = new VendaController();
        private frmCadastroVenda oFrmCadastroVenda;

        public frmConsultaVenda()
        {
            InitializeComponent();
        }

        public override void setFrmCadastro(object obj)
        {
            if (obj != null)
            {
                oFrmCadastroVenda = (frmCadastroVenda)obj;
            }
        }

        public override void ConhecaObj(object obj, object ctrl)
        {
            if (ctrl != null)
            {
                controller = (VendaController)ctrl;
            }
        }

        private void frmConsultaVenda_Load(object sender, EventArgs e) 
        {
            btnDeletar.Text = "Cancelar";

            CarregarVendas(); 
        }

      
        private void CarregarVendas()
        {
            try
            {
                listView1.Items.Clear();
                List<Venda> vendas = controller.Listar(); 

                foreach (var venda in vendas) 
                {
                    ListViewItem item = new ListViewItem("");
                    item.SubItems.Add(venda.Modelo);
                    item.SubItems.Add(venda.Serie);
                    item.SubItems.Add(venda.NumeroNota.ToString());
                    item.SubItems.Add(venda.ClienteId.ToString());
                    item.SubItems.Add(venda.NomeCliente); 
                    item.SubItems.Add(venda.DataEmissao?.ToString("dd/MM/yyyy"));
                    item.SubItems.Add(venda.DataSaida?.ToString("dd/MM/yyyy")); 
                    item.SubItems.Add(venda.ValorTotal.ToString("C2"));
                    item.SubItems.Add(venda.CondicaoPagamentoId.HasValue ? venda.CondicaoPagamentoId.Value.ToString() : "0");
                    item.SubItems.Add(venda.NomeCondPgto);
                    item.SubItems.Add(venda.Ativo ? "Ativo" : "Cancelado");

                    item.Tag = venda; 

                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar as vendas: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            oFrmCadastroVenda.modoCancelamento = false;
            oFrmCadastroVenda.modoVisualizacao = false;
            oFrmCadastroVenda.LimparTxt();
            oFrmCadastroVenda.btnSalvar.Text = "Salvar";
            oFrmCadastroVenda.ShowDialog();
            CarregarVendas(); 
        }

        private void btnEditar_Click(object sender, EventArgs e) 
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var vendaSelecionada = (Venda)listView1.SelectedItems[0].Tag; 

                Venda vendaCompleta = controller.BuscarPorChave(
                    vendaSelecionada.Modelo,
                    vendaSelecionada.Serie,
                    vendaSelecionada.NumeroNota,
                    vendaSelecionada.ClienteId
                );

                if (vendaCompleta != null)
                {
                    oFrmCadastroVenda.modoCancelamento = false;
                    oFrmCadastroVenda.modoVisualizacao = true;
                    oFrmCadastroVenda.ConhecaObj(vendaCompleta, controller);
                    oFrmCadastroVenda.CarregaTxt();
                    oFrmCadastroVenda.BloquearTxt();
                    oFrmCadastroVenda.btnSalvar.Enabled = false;
                    oFrmCadastroVenda.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Não foi possível carregar os detalhes da venda.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma venda para visualizar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
            }
        }

        private void btnDeletar_Click(object sender, EventArgs e) 
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var vendaSelecionada = (Venda)listView1.SelectedItems[0].Tag; 

                if (!vendaSelecionada.Ativo)
                {
                    MessageBox.Show("Esta venda já está cancelada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                    return;
                }

                Venda vendaCompleta = controller.BuscarPorChave(
                    vendaSelecionada.Modelo,
                    vendaSelecionada.Serie,
                    vendaSelecionada.NumeroNota,
                    vendaSelecionada.ClienteId
                );

                if (vendaCompleta != null)
                {
                    oFrmCadastroVenda.modoCancelamento = true;
                    oFrmCadastroVenda.modoVisualizacao = false;
                    oFrmCadastroVenda.ConhecaObj(vendaCompleta, controller);
                    oFrmCadastroVenda.CarregaTxt();
                    oFrmCadastroVenda.BloquearTxt();
                    oFrmCadastroVenda.btnSalvar.Enabled = true;
                    oFrmCadastroVenda.btnSalvar.Text = "Cancelar Venda"; 
                    oFrmCadastroVenda.ShowDialog();

                    oFrmCadastroVenda.btnSalvar.Text = "Salvar";
                    oFrmCadastroVenda.DesbloquearTxt();
                    oFrmCadastroVenda.modoCancelamento = false;

                    CarregarVendas(); 
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma venda para cancelar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                string termo = txtPesquisar.Text; 
                List<Venda> lista = controller.Pesquisar(termo);

                listView1.Items.Clear();

                foreach (var venda in lista)
                {
                    ListViewItem item = new ListViewItem("");
                    item.SubItems.Add(venda.Modelo);
                    item.SubItems.Add(venda.Serie);
                    item.SubItems.Add(venda.NumeroNota.ToString());
                    item.SubItems.Add(venda.ClienteId.ToString());
                    item.SubItems.Add(venda.NomeCliente);
                    item.SubItems.Add(venda.DataEmissao?.ToString("dd/MM/yyyy"));
                    item.SubItems.Add(venda.DataSaida?.ToString("dd/MM/yyyy"));
                    item.SubItems.Add(venda.ValorTotal.ToString("C2"));
                    item.SubItems.Add(venda.CondicaoPagamentoId.HasValue ? venda.CondicaoPagamentoId.Value.ToString() : "0");
                    item.SubItems.Add(venda.NomeCondPgto);
                    item.SubItems.Add(venda.Ativo ? "Ativo" : "Cancelado");

                    item.Tag = venda;
                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao pesquisar: " + ex.Message);
            }
        }


    }
}