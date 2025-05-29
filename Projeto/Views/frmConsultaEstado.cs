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
        public bool ModoSelecao { get; set; } = false;
        internal Estado EstadoSelecionado { get; private set; }



        public frmConsultaEstado() : base()
        {
            InitializeComponent();
        }

        private void frmConsultaEstado_Load(object sender, EventArgs e)
        {
            btnSelecionar.Visible = ModoSelecao;
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
                    item.SubItems.Add(estado.Status ? "Ativo" : "Inativo");

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
            try
            {
                listView1.Items.Clear();
                string texto = txtPesquisar.Text.Trim();

                if (string.IsNullOrEmpty(texto))
                {
                    CarregarEstados();
                }
                else if (int.TryParse(texto, out int id))
                {
                    Estado estado = controller.BuscarPorId(id);

                    if (estado != null)
                    {
                        ListViewItem item = new ListViewItem(estado.Id.ToString());
                        item.SubItems.Add(estado.Nome);
                        item.SubItems.Add(estado.PaisNome);
                        item.SubItems.Add(estado.Status ? "Ativo" : "Inativo");
                        listView1.Items.Add(item);
                    }
                    else
                    {
                        MessageBox.Show("Estado não encontrado.");
                    }
                }
                else
                {
                    MessageBox.Show("Digite um ID válido (número inteiro).");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao pesquisar: " + ex.Message);
            }
        }


        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);

                Estado estado = controller.BuscarPorId(id);

                if (estado != null)
                {
                    var formCadastro = new frmCadastroEstado();
                    formCadastro.CarregarEstado(
                        estado.Id,
                        estado.Nome,
                        estado.PaisNome,
                        estado.Status,
                        estado.DataCriacao,
                        estado.DataModificacao
                    );

                    formCadastro.FormClosed += (s, args) => CarregarEstados();
                    formCadastro.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Estado não encontrado.");
                }
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

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);
                string nome = itemSelecionado.SubItems[1].Text;
                string pais = itemSelecionado.SubItems[2].Text;

                EstadoSelecionado = new Estado
                {
                    Id = id,
                    Nome = nome,
                    PaisNome = pais,
                };

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor, selecione um nome.");
            }
        }

        
    }
}
