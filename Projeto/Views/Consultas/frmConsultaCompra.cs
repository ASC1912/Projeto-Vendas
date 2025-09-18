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
            btnEditar.Enabled = false;
            btnDeletar.Text = "Cancelar";

            CarregarCompras();
            ConfigurarColunas();
        }

        private void ConfigurarColunas()
        {
            foreach (ColumnHeader column in listView1.Columns)
            {
                switch (column.Name) 
                {
                    case "Modelo":
                        column.Width = 80;
                        column.TextAlign = HorizontalAlignment.Center;
                        break;

                    case "Serie":
                        column.Width = 80;
                        column.TextAlign = HorizontalAlignment.Center;
                        break;

                    case "Numero": 
                        column.Width = 100;
                        column.TextAlign = HorizontalAlignment.Center;
                        break;

                    case "Fornecedor":
                        column.Width = 280; 
                        column.TextAlign = HorizontalAlignment.Center; 
                        break;

                    case "DataEmissao":
                        column.Width = 120;
                        column.TextAlign = HorizontalAlignment.Center;
                        break;

                    case "DataChegada":
                        column.Width = 120;
                        column.TextAlign = HorizontalAlignment.Center;
                        break;

                    case "ValorTotal":
                        column.Width = 120;
                        column.TextAlign = HorizontalAlignment.Center; 
                        break;

                    case "CondPgto":
                        column.Width = 180;
                        column.TextAlign = HorizontalAlignment.Center;
                        break;

                    case "Status":
                        column.Width = 80;
                        column.TextAlign = HorizontalAlignment.Center;
                        break;
                }
            }
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
                    item.SubItems.Add(compra.NomeFornecedor);
                    item.SubItems.Add(compra.DataEmissao?.ToString("dd/MM/yyyy"));
                    item.SubItems.Add(compra.DataChegada?.ToString("dd/MM/yyyy"));
                    item.SubItems.Add(compra.ValorTotal.ToString("C2")); 
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
            oFrmCadastroCompra.LimparTxt(); 
            oFrmCadastroCompra.ShowDialog();
            CarregarCompras(); 
        }

        private void btnDeletar_Click(object sender, EventArgs e) 
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Por favor, selecione uma compra para cancelar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmacao = MessageBox.Show("Tem certeza que deseja cancelar esta compra? Esta ação não pode ser desfeita.", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmacao == DialogResult.Yes)
            {
                try
                {
                    Compra compraSelecionada = (Compra)listView1.SelectedItems[0].Tag;

                    if (!compraSelecionada.Ativo)
                    {
                        MessageBox.Show("Esta compra já está cancelada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    controller.Cancelar(compraSelecionada);
                    MessageBox.Show("Compra cancelada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CarregarCompras(); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao cancelar a compra: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}