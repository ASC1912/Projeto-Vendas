using Projeto.Controller;
using Projeto.Models;
using Projeto.Views.Cadastros;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto.Views.Consultas
{
    public partial class frmConsultaMarca : Projeto.frmBaseConsulta
    {
        private MarcaController controller = new MarcaController();
        internal Marca MarcaSelecionado { get; private set; }
        private frmCadastroMarca oFrmCadastroMarca;

        public frmConsultaMarca()
        {
            InitializeComponent();
        }

        public override void setFrmCadastro(object obj)
        {
            if (obj != null)
            {
                oFrmCadastroMarca = (frmCadastroMarca)obj;
            }
        }

        public override void ConhecaObj(object obj, object ctrl)
        {
            if (ctrl != null)
            {
                controller = (MarcaController)ctrl;
            }
        }

        private async void frmConsultaMarca_Load(object sender, EventArgs e)
        {
            await CarregarMarcas();
            btnSelecionar.Visible = ModoSelecao;
        }

        private async Task CarregarMarcas()
        {
            try
            {
                listView1.Items.Clear();
                List<Marca> marcas = await controller.ListarMarcas();

                foreach (var marca in marcas)
                {
                    ListViewItem item = new ListViewItem("");
                    item.SubItems.Add(marca.Id.ToString());
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

        private async void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                listView1.Items.Clear();
                string texto = txtPesquisar.Text.Trim();

                if (string.IsNullOrEmpty(texto))
                {
                    await CarregarMarcas();
                }
                else if (int.TryParse(texto, out int id))
                {
                    Marca marca = await controller.BuscarPorId(id);

                    if (marca != null)
                    {
                        ListViewItem item = new ListViewItem("");
                        item.SubItems.Add(marca.Id.ToString());
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

        private async void btnIncluir_Click(object sender, EventArgs e)
        {
            oFrmCadastroMarca.modoEdicao = false;
            oFrmCadastroMarca.modoExclusao = false;
            oFrmCadastroMarca.LimparTxt();
            oFrmCadastroMarca.ShowDialog();
            await CarregarMarcas();
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);

                Marca marca = await controller.BuscarPorId(id);

                if (marca != null)
                {
                    oFrmCadastroMarca.modoEdicao = true;
                    oFrmCadastroMarca.modoExclusao = false;
                    oFrmCadastroMarca.ConhecaObj(marca, controller);
                    oFrmCadastroMarca.LimparTxt();
                    oFrmCadastroMarca.CarregaTxt();
                    oFrmCadastroMarca.ShowDialog();
                    await CarregarMarcas();
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

        private async void btnDeletar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);

                Marca marca = await controller.BuscarPorId(id);

                if (marca != null)
                {
                    oFrmCadastroMarca.modoExclusao = true;
                    oFrmCadastroMarca.modoEdicao = false;
                    oFrmCadastroMarca.ConhecaObj(marca, controller);
                    oFrmCadastroMarca.LimparTxt();
                    oFrmCadastroMarca.CarregaTxt();
                    oFrmCadastroMarca.BloquearTxt();
                    oFrmCadastroMarca.btnSalvar.Text = "Excluir";
                    oFrmCadastroMarca.ShowDialog();
                    oFrmCadastroMarca.btnSalvar.Text = "Salvar";
                    oFrmCadastroMarca.DesbloquearTxt();
                    await CarregarMarcas();
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

        private async void btnSelecionar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);

                MarcaSelecionado = await controller.BuscarPorId(id);

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