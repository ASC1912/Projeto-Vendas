using Projeto.Controller;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Projeto.Views
{
    public partial class frmConsultaEstado : Projeto.frmBaseConsulta
    {
        private EstadoController controller = new EstadoController();

        public frmConsultaEstado()
        {
            InitializeComponent();
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            frmCadastroEstado formCadastroEstado = new frmCadastroEstado();
            formCadastroEstado.ShowDialog();
        }
        private void CarregarEstados()
        {
            try
            {
                listView1.Items.Clear();
                List<Estado> estados = controller.ListarEstado();

                foreach (var estado in estados)
                {
                    ListViewItem item = new ListViewItem(estado.Id.ToString());
                    item.SubItems.Add(estado.Nome);
                    item.SubItems.Add(estado.PaisNome); 

                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar estados: " + ex.Message);
            }
        }
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            CarregarEstados();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);
                string nome = itemSelecionado.SubItems[1].Text;
                string nomePais = itemSelecionado.SubItems[2].Text;

                var formCadastro = new frmCadastroEstado();
                formCadastro.CarregarEstado(id, nome, nomePais);

                formCadastro.FormClosed += (s, args) => CarregarEstados();
                formCadastro.ShowDialog();
            }
            else
            {
                MessageBox.Show("Por favor, selecione um item para editar.");
            }
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);

                var confirmacao = MessageBox.Show("Tem certeza que deseja excluir esse Estado?", "Confirmação", MessageBoxButtons.YesNo);
                if (confirmacao == DialogResult.Yes)
                {
                    try
                    {
                        controller.Excluir(id);
                        listView1.Items.Remove(itemSelecionado);
                        MessageBox.Show("Estado excluído com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao excluir: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um estado para excluir.");
            }
        }
    }
}
