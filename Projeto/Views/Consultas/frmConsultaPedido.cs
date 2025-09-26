using Projeto.Controller;
using Projeto.Models;
using Projeto.Views.Cadastros;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto.Views.Consultas
{
    public partial class frmConsultaPedido : frmBaseConsulta
    {
        private PedidoController controller = new PedidoController();
        private frmCadastroPedido oFrmCadastroPedido;

        public frmConsultaPedido()
        {
            InitializeComponent();
        }

        public override void setFrmCadastro(object obj)
        {
            if (obj != null)
            {
                oFrmCadastroPedido = (frmCadastroPedido)obj;
            }
        }

        public override void ConhecaObj(object obj, object ctrl)
        {
            if (ctrl != null)
            {
                controller = (PedidoController)ctrl;
            }
        }

        private async void frmConsultaPedido_Load(object sender, EventArgs e)
        {
            await CarregarPedidos();

            foreach (ColumnHeader column in listView1.Columns)
            {
                column.TextAlign = HorizontalAlignment.Center;
                switch (column.Text)
                {
                    case "ID":
                        column.Width = 60;
                        column.TextAlign = HorizontalAlignment.Right;
                        break;
                    case "Mesa":
                        column.Width = 80;
                        break;
                    case "Funcionário":
                        column.Width = 250;
                        break;
                    case "Data do Pedido":
                        column.Width = 150;
                        break;
                    case "Status":
                        column.Width = 120;
                        break;
                    case "Ativo":
                        column.Width = 80;
                        break;
                    default:
                        column.Width = 120;
                        break;
                }
            }
        }

        private async Task CarregarPedidos()
        {
            try
            {
                listView1.Items.Clear();
                List<Pedido> pedidos = await controller.ListarPedidos();

                foreach (var pedido in pedidos)
                {
                    ListViewItem item = new ListViewItem(pedido.Id.ToString());
                    item.SubItems.Add(pedido.MesaNumero.ToString());
                    item.SubItems.Add(pedido.NomeFuncionario);
                    item.SubItems.Add(pedido.DataPedido.ToString("g")); 
                    item.SubItems.Add(pedido.Status);
                    item.SubItems.Add(pedido.Ativo ? "Ativo" : "Inativo");
                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar pedidos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnIncluir_Click(object sender, EventArgs e)
        {
            oFrmCadastroPedido.modoEdicao = false;
            oFrmCadastroPedido.modoExclusao = false;
            oFrmCadastroPedido.LimparTxt();
            oFrmCadastroPedido.ShowDialog();
            await CarregarPedidos();
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);

                Pedido pedido = await controller.BuscarPorId(id);

                if (pedido != null)
                {
                    oFrmCadastroPedido.modoEdicao = true;
                    oFrmCadastroPedido.modoExclusao = false;
                    oFrmCadastroPedido.ConhecaObj(pedido, controller);
                    oFrmCadastroPedido.CarregaTxt();
                    oFrmCadastroPedido.ShowDialog();
                    await CarregarPedidos();
                }
                else
                {
                    MessageBox.Show("Pedido não encontrado.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um pedido para editar.");
            }
        }

        private async void btnDeletar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);

                Pedido pedido = await controller.BuscarPorId(id);

                if (pedido != null)
                {
                    oFrmCadastroPedido.modoExclusao = true;
                    oFrmCadastroPedido.modoEdicao = false;
                    oFrmCadastroPedido.ConhecaObj(pedido, controller);
                    oFrmCadastroPedido.CarregaTxt();      
                    oFrmCadastroPedido.BloquearTxt();     
                    oFrmCadastroPedido.btnSalvar.Text = "Excluir"; 

                    oFrmCadastroPedido.ShowDialog();

                    oFrmCadastroPedido.btnSalvar.Text = "Salvar";
                    oFrmCadastroPedido.DesbloquearTxt();
                    await CarregarPedidos();
                }
                else
                {
                    MessageBox.Show("Pedido não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um pedido para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                listView1.Items.Clear();
                string texto = txtPesquisar.Text.Trim();

                if (string.IsNullOrEmpty(texto))
                {
                    await CarregarPedidos();
                }
                else if (int.TryParse(texto, out int id))
                {
                    Pedido pedido = await controller.BuscarPorId(id);

                    if (pedido != null)
                    {
                        ListViewItem item = new ListViewItem(pedido.Id.ToString());
                        item.SubItems.Add(pedido.MesaNumero.ToString());
                        item.SubItems.Add(pedido.NomeFuncionario);
                        item.SubItems.Add(pedido.DataPedido.ToString("g"));
                        item.SubItems.Add(pedido.Status);
                        item.SubItems.Add(pedido.Ativo ? "Ativo" : "Inativo");
                        listView1.Items.Add(item);
                    }
                    else
                    {
                        MessageBox.Show("Pedido não encontrado.");
                    }
                }
                else
                {
                    MessageBox.Show("Digite um ID de pedido válido.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao pesquisar: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}