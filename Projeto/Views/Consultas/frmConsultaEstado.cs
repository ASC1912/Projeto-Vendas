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
        frmCadastroEstado oFrmCadastroEstado;
        private EstadoController controller = new EstadoController();
        internal Estado EstadoSelecionado { get; private set; }

        public frmConsultaEstado() : base()
        {
            InitializeComponent();
        }

        public override void setFrmCadastro(object obj)
        {
            if (obj != null)
            {
                oFrmCadastroEstado = (frmCadastroEstado)obj;
            }
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
                        column.TextAlign = HorizontalAlignment.Right;
                        break;
                    case "Estado":
                        column.Width = 150;
                        column.TextAlign = HorizontalAlignment.Center;
                        break;
                    case "UF":
                        column.Width = 50;
                        column.TextAlign = HorizontalAlignment.Center;
                        break;
                    case "País":
                        column.Width = 150;
                        column.TextAlign = HorizontalAlignment.Center;
                        break;
                    case "Ativo":
                        column.Width = 50;
                        column.TextAlign = HorizontalAlignment.Center;
                        break;
                    default:
                        column.Width = 100;
                        break;
                }
            }
        }

        private async void btnIncluir_Click(object sender, EventArgs e)
        {
            oFrmCadastroEstado.modoEdicao = false;
            oFrmCadastroEstado.modoExclusao = false;
            oFrmCadastroEstado.LimparTxt();
            oFrmCadastroEstado.ShowDialog();
            await CarregarEstados();
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
                    oFrmCadastroEstado.modoEdicao = true;
                    oFrmCadastroEstado.modoExclusao = false;
                    oFrmCadastroEstado.ConhecaObj(estado, controller);
                    oFrmCadastroEstado.LimparTxt();
                    oFrmCadastroEstado.CarregaTxt();
                    oFrmCadastroEstado.ShowDialog();
                    await CarregarEstados();
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
                    oFrmCadastroEstado.modoExclusao = true;
                    oFrmCadastroEstado.modoEdicao = false;
                    oFrmCadastroEstado.ConhecaObj(estado, controller);
                    oFrmCadastroEstado.LimparTxt();
                    oFrmCadastroEstado.CarregaTxt();
                    oFrmCadastroEstado.BloquearTxt();
                    oFrmCadastroEstado.btnSalvar.Text = "Excluir";
                    oFrmCadastroEstado.ShowDialog();
                    oFrmCadastroEstado.btnSalvar.Text = "Salvar";
                    oFrmCadastroEstado.DesbloquearTxt();
                    await CarregarEstados();
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