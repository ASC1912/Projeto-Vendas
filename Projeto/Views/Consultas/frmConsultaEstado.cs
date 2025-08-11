using Projeto.Controller;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        private async void frmConsultaEstado_Load(object sender, EventArgs e)
        {
            await CarregarEstados();
            btnSelecionar.Visible = ModoSelecao;

            foreach (ColumnHeader column in listView1.Columns)
            {
                switch (column.Text)
                {
                    case "ID":
                        column.Width = 50;
                        break;
                    case "Estado":
                        column.Width = 150;
                        break;
                    case "UF":
                        column.Width = 50;
                        break;
                    case "País":
                        column.Width = 150;
                        break;
                    case "Ativo":
                        column.Width = 50;
                        break;
                    default:
                        column.Width = 100;
                        break;
                }
            }
        }

        private async void btnIncluir_Click(object sender, EventArgs e)
        {
            frmCadastroEstado formCadastroEstado = new frmCadastroEstado();
            formCadastroEstado.FormClosed += async (s, args) => await CarregarEstados();
            formCadastroEstado.ShowDialog();
        }

        private async Task CarregarEstados()
        {
            try
            {
                listView1.Items.Clear();
                List<Estado> estados = await controller.ListarEstado();

                foreach (var estado in estados)
                {
                    ListViewItem item = new ListViewItem(estado.Id.ToString());
                    item.SubItems.Add(estado.NomeEstado);
                    item.SubItems.Add(estado.UF);
                    item.SubItems.Add(estado.PaisNome);
                    item.SubItems.Add(estado.Ativo ? "Ativo" : "Inativo");
                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar estados: " + ex.Message);
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
                    await CarregarEstados();
                }
                else if (int.TryParse(texto, out int id))
                {
                    Estado estado = await controller.BuscarPorId(id);

                    if (estado != null)
                    {
                        ListViewItem item = new ListViewItem(estado.Id.ToString());
                        item.SubItems.Add(estado.NomeEstado);
                        item.SubItems.Add(estado.UF);
                        item.SubItems.Add(estado.PaisNome);
                        item.SubItems.Add(estado.Ativo ? "Ativo" : "Inativo");
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

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);
                Estado estado = await controller.BuscarPorId(id);

                if (estado != null)
                {
                    var formCadastro = new frmCadastroEstado();
                    formCadastro.modoEdicao = true;
                    formCadastro.CarregarEstado(
                        estado.Id, estado.NomeEstado, estado.UF, estado.PaisNome,
                        estado.PaisId, estado.Ativo, estado.DataCadastro, estado.DataAlteracao
                    );
                    formCadastro.FormClosed += async (s, args) => await CarregarEstados();
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

        private async void btnDeletar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);
                Estado estado = await controller.BuscarPorId(id);

                if (estado != null)
                {
                    var formCadastro = new frmCadastroEstado { modoExclusao = true };
                    formCadastro.CarregarEstado(
                        estado.Id, estado.NomeEstado, estado.UF, estado.PaisNome,
                        estado.PaisId, estado.Ativo, estado.DataCadastro, estado.DataAlteracao
                    );
                    formCadastro.FormClosed += async (s, args) => await CarregarEstados();
                    formCadastro.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Estado não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um estado para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnSelecionar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);
                EstadoSelecionado = await controller.BuscarPorId(id);
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