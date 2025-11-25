using Projeto.Controller;
using Projeto.Models;
using Projeto.Views.Cadastros;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Projeto.Views.Consultas
{
    public partial class frmConsultaCompra : Projeto.frmBaseConsulta
    {
        private CompraController controller = new CompraController();
        private frmCadastroCompra oFrmCadastroCompra;

        public frmConsultaCompra()
        {
            InitializeComponent();
        }

        public override void setFrmCadastro(object obj)
        {
            if (obj != null)
            {
                oFrmCadastroCompra = (frmCadastroCompra)obj;
            }
        }

        public override void ConhecaObj(object obj, object ctrl)
        {
            if (ctrl != null)
            {
                controller = (CompraController)ctrl;
            }
        }

        private void frmConsultaCompra_Load(object sender, EventArgs e)
        {
            btnDeletar.Text = "Cancelar"; 

            CarregarCompras();
            ConfigurarColunas();
        }

        private void ConfigurarColunas()
        {
        }

        private void CarregarCompras()
        {
            try
            {
                listView1.Items.Clear();
                List<Compra> compras = controller.Listar();

                foreach (var compra in compras)
                {
                    ListViewItem item = new ListViewItem(compra.Modelo);
                    item.SubItems.Add(compra.Serie);
                    item.SubItems.Add(compra.NumeroNota.ToString());
                    item.SubItems.Add(compra.FornecedorId.ToString());
                    item.SubItems.Add(compra.NomeFornecedor);
                    item.SubItems.Add(compra.DataEmissao?.ToString("dd/MM/yyyy"));
                    item.SubItems.Add(compra.DataChegada?.ToString("dd/MM/yyyy"));
                    item.SubItems.Add(compra.ValorTotal.ToString("C2"));
                    item.SubItems.Add(compra.CondicaoPagamentoId.ToString());
                    item.SubItems.Add(compra.NomeCondPgto);
                    item.SubItems.Add(compra.Ativo ? "Ativo" : "Cancelado");

                    item.Tag = compra;

                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar as compras: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            oFrmCadastroCompra.modoCancelamento = false;
            oFrmCadastroCompra.LimparTxt();
            oFrmCadastroCompra.btnSalvar.Text = "Salvar";
            oFrmCadastroCompra.ShowDialog();
            CarregarCompras();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var compraSelecionada = (Compra)listView1.SelectedItems[0].Tag;

                Compra compraCompleta = controller.BuscarPorChave(
                    compraSelecionada.Modelo,
                    compraSelecionada.Serie,
                    compraSelecionada.NumeroNota,
                    compraSelecionada.FornecedorId
                );

                if (compraCompleta != null)
                {
                    oFrmCadastroCompra.modoCancelamento = false;
                    oFrmCadastroCompra.ConhecaObj(compraCompleta, controller);
                    oFrmCadastroCompra.CarregaTxt();
                    oFrmCadastroCompra.BloquearTxt();
                    oFrmCadastroCompra.btnSalvar.Enabled = false;
                    oFrmCadastroCompra.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Não foi possível carregar os detalhes da compra.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma compra para visualizar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var compraSelecionada = (Compra)listView1.SelectedItems[0].Tag;

                if (!compraSelecionada.Ativo)
                {
                    MessageBox.Show("Esta compra já está cancelada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Compra compraCompleta = controller.BuscarPorChave(
                    compraSelecionada.Modelo,
                    compraSelecionada.Serie,
                    compraSelecionada.NumeroNota,
                    compraSelecionada.FornecedorId
                );

                if (compraCompleta != null)
                {
                    oFrmCadastroCompra.modoCancelamento = true;
                    oFrmCadastroCompra.ConhecaObj(compraCompleta, controller);
                    oFrmCadastroCompra.CarregaTxt();
                    oFrmCadastroCompra.BloquearTxt();
                    oFrmCadastroCompra.btnSalvar.Enabled = true; 
                    oFrmCadastroCompra.btnSalvar.Text = "Cancelar Compra";
                    oFrmCadastroCompra.ShowDialog();

                    oFrmCadastroCompra.btnSalvar.Text = "Salvar";
                    oFrmCadastroCompra.DesbloquearTxt();
                    oFrmCadastroCompra.modoCancelamento = false;

                    CarregarCompras(); 
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma compra para cancelar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}