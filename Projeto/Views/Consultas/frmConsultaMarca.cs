using Projeto.Controller;
using Projeto.Models;
using Projeto.Views.Cadastros;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Projeto.Views.Consultas
{
    public partial class frmConsultaMarca : Projeto.frmBaseConsulta
    {
        private MarcaController controller = new MarcaController();
        public bool ModoSelecao { get; set; } = false;
        internal Marca MarcaSelecionado { get; private set; }

        public frmConsultaMarca()
        {
            InitializeComponent();
        }

        private void frmConsultaMarca_Load(object sender, EventArgs e)
        {
            CarregarMarcas();

            btnSelecionar.Visible = ModoSelecao;

            foreach (ColumnHeader column in listView1.Columns)
            {
                switch (column.Text)
                {
                    case "ID":
                        column.Width = 50;
                        break;
                    case "Marca":
                        column.Width = 150;
                        break;
                    case "Ativo":
                        column.Width = 60;
                        break;
                    default:
                        column.Width = 100;
                        break;
                }
            }

            CarregarMarcas();
        }

        private void CarregarMarcas()
        {
            try
            {
                listView1.Items.Clear();
                List<Marca> marcas = controller.ListarMarcas();

                foreach (var marca in marcas)
                {
                    ListViewItem item = new ListViewItem(marca.Id.ToString());
                    item.SubItems.Add(marca.NomeMarca);
                    item.SubItems.Add(marca.Ativo ? "Ativo" : "Inativo");
                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar as marcas: " + ex.Message);
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
                    CarregarMarcas();
                }
                else if (int.TryParse(texto, out int id))
                {
                    Marca marca = controller.BuscarPorId(id);

                    if (marca != null)
                    {
                        ListViewItem item = new ListViewItem(marca.Id.ToString());
                        item.SubItems.Add(marca.NomeMarca);
                        item.SubItems.Add(marca.Ativo ? "Ativo" : "Inativo");
                        listView1.Items.Add(item);
                    }
                    else
                    {
                        MessageBox.Show("Marca não encontrada.");
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

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            frmCadastroMarca formCadastroMarca = new frmCadastroMarca();
            formCadastroMarca.FormClosed += (s, args) => CarregarMarcas();
            formCadastroMarca.ShowDialog();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);

                Marca marca = controller.BuscarPorId(id);

                if (marca != null)
                {
                    var formCadastro = new frmCadastroMarca();
                    formCadastro.CarregarMarca(
                        marca.Id,
                        marca.NomeMarca,
                        marca.Ativo,
                        marca.DataCadastro,
                        marca.DataAlteracao
                    );

                    formCadastro.FormClosed += (s, args) => CarregarMarcas();
                    formCadastro.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Marca não encontrada.");
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

                Marca marca = controller.BuscarPorId(id);

                if (marca != null)
                {
                    var formCadastro = new frmCadastroMarca
                    {
                        modoExclusao = true 
                    };

                    formCadastro.CarregarMarca(
                        marca.Id,
                        marca.NomeMarca,
                        marca.Ativo,
                        marca.DataCadastro,
                        marca.DataAlteracao
                    );

                    formCadastro.FormClosed += (s, args) => CarregarMarcas();
                    formCadastro.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Marca não encontrada.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma marca para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);

                MarcaSelecionado = controller.BuscarPorId(id);

                if (MarcaSelecionado != null)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Erro ao carregar os dados da marca.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma marca.");
            }
        }
    }
}
