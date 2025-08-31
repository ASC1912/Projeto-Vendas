using Projeto.Controller;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto.Views
{
    public partial class frmConsultaCidade : Projeto.frmBaseConsulta
    {
        frmCadastroCidade oFrmCadastroCidade;
        private CidadeController controller = new CidadeController();
        public bool ModoSelecao { get; set; } = false;
        internal Cidade CidadeSelecionado { get; private set; }

        public frmConsultaCidade() : base()
        {
            InitializeComponent();
        }

        public override void setFrmCadastro(object obj)
        {
            if (obj != null)
            {
                oFrmCadastroCidade = (frmCadastroCidade)obj;
            }
        }
        private async void frmConsultaCidade_Load(object sender, EventArgs e)
        {
            await CarregarCidades();
            btnSelecionar.Visible = ModoSelecao;

            foreach (ColumnHeader column in listView1.Columns)
            {
                switch (column.Text)
                {
                    case "ID":
                        column.Width = 40;
                        break;
                    case "Cidade":
                        column.Width = 170;
                        break;
                    case "Estado":
                        column.Width = 150;
                        break;
                    case "DDD": 
                        column.Width = 50;
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
            oFrmCadastroCidade.FormClosed += async (s, args) => await CarregarCidades();
            oFrmCadastroCidade.ShowDialog();

            /*
            frmCadastroCidade formCadastroCidade = new frmCadastroCidade();
            formCadastroCidade.FormClosed += async (s, args) => await CarregarCidades();
            formCadastroCidade.ShowDialog();
            */
        }

        private async Task CarregarCidades()
        {
            try
            {
                listView1.Items.Clear();
                List<Cidade> cidades = await controller.ListarCidade();

                foreach (var cidade in cidades)
                {
                    ListViewItem item = new ListViewItem(cidade.Id.ToString());
                    item.SubItems.Add(cidade.NomeCidade);
                    item.SubItems.Add(cidade.EstadoNome);
                    item.SubItems.Add(cidade.DDD); 
                    item.SubItems.Add(cidade.Ativo ? "Ativo" : "Inativo");
                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar cidades: " + ex.Message);
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
                    await CarregarCidades();
                }
                else if (int.TryParse(texto, out int id))
                {
                    Cidade cidade = await controller.BuscarPorId(id);

                    if (cidade != null)
                    {
                        ListViewItem item = new ListViewItem(cidade.Id.ToString());
                        item.SubItems.Add(cidade.NomeCidade);
                        item.SubItems.Add(cidade.EstadoNome);
                        item.SubItems.Add(cidade.DDD); 
                        item.SubItems.Add(cidade.Ativo ? "Ativo" : "Inativo");
                        listView1.Items.Add(item);
                    }
                    else
                    {
                        MessageBox.Show("Cidade não encontrada.");
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
                Cidade cidade = await controller.BuscarPorId(id);

                if (cidade != null)
                {
                    var formCadastro = new frmCadastroCidade { modoEdicao = true };
                    formCadastro.CarregarCidade(cidade.Id, cidade.NomeCidade, cidade.EstadoNome, cidade.EstadoId, cidade.DDD, cidade.Ativo, cidade.DataCadastro, cidade.DataAlteracao);
                    formCadastro.FormClosed += async (s, args) => await CarregarCidades();
                    formCadastro.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Cidade não encontrada.");
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
                Cidade cidade = await controller.BuscarPorId(id);

                if (cidade != null)
                {
                    var formCadastro = new frmCadastroCidade { modoExclusao = true };
                    formCadastro.CarregarCidade(cidade.Id, cidade.NomeCidade, cidade.EstadoNome, cidade.EstadoId, cidade.DDD, cidade.Ativo, cidade.DataCadastro, cidade.DataAlteracao);
                    formCadastro.FormClosed += async (s, args) => await CarregarCidades();
                    formCadastro.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Cidade não encontrada.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma cidade para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnSelecionar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);
                CidadeSelecionado = await controller.BuscarPorId(id);
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