using Projeto.Controller;
using Projeto.Models;
using Projeto.Views.Cadastros;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto.Views.Consultas
{
    public partial class frmConsultaUnidadeMedida : frmBaseConsulta
    {
        private UnidadeMedidaController controller = new UnidadeMedidaController();
        private frmCadastroUnidadeMedida oFrmCadastroUnidadeMedida;
        internal UnidadeMedida UnidadeSelecionada { get; private set; }

        public frmConsultaUnidadeMedida()
        {
            InitializeComponent();
        }

        public override void setFrmCadastro(object obj)
        {
            if (obj != null) oFrmCadastroUnidadeMedida = (frmCadastroUnidadeMedida)obj;
        }

        public override void ConhecaObj(object obj, object ctrl)
        {
            if (ctrl != null) controller = (UnidadeMedidaController)ctrl;
        }

        private async void frmConsultaUnidadeMedida_Load(object sender, EventArgs e)
        {
            btnSelecionar.Visible = ModoSelecao;
            await CarregarUnidades();

        }

        private async Task CarregarUnidades()
        {
            try
            {
                listView1.Items.Clear();
                List<UnidadeMedida> unidades = await controller.ListarUnidades();

                foreach (var unidade in unidades)
                {
                    ListViewItem item = new ListViewItem("");
                    item.SubItems.Add(unidade.Id.ToString());
                    item.SubItems.Add(unidade.NomeUnidade);
                    item.SubItems.Add(unidade.Sigla);
                    item.SubItems.Add(unidade.Ativo ? "Ativo" : "Inativo");
                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os dados: " + ex.Message);
            }
        }

        private async void btnIncluir_Click(object sender, EventArgs e)
        {
            oFrmCadastroUnidadeMedida.modoEdicao = false;
            oFrmCadastroUnidadeMedida.modoExclusao = false;
            oFrmCadastroUnidadeMedida.LimparTxt();
            oFrmCadastroUnidadeMedida.ShowDialog();
            await CarregarUnidades();
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int id = int.Parse(listView1.SelectedItems[0].SubItems[1].Text);
                UnidadeMedida unidade = await controller.BuscarPorId(id);
                if (unidade != null)
                {
                    oFrmCadastroUnidadeMedida.modoEdicao = true;
                    oFrmCadastroUnidadeMedida.modoExclusao = false;
                    oFrmCadastroUnidadeMedida.ConhecaObj(unidade, controller);
                    oFrmCadastroUnidadeMedida.CarregaTxt();
                    oFrmCadastroUnidadeMedida.ShowDialog();
                    await CarregarUnidades();
                }
            }
            else
            {
                MessageBox.Show("Selecione um item para editar.");
            }
        }

        private async void btnDeletar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int id = int.Parse(listView1.SelectedItems[0].SubItems[1].Text);
                UnidadeMedida unidade = await controller.BuscarPorId(id);
                if (unidade != null)
                {
                    oFrmCadastroUnidadeMedida.modoExclusao = true;
                    oFrmCadastroUnidadeMedida.ConhecaObj(unidade, controller);
                    oFrmCadastroUnidadeMedida.CarregaTxt();
                    oFrmCadastroUnidadeMedida.BloquearTxt();
                    oFrmCadastroUnidadeMedida.btnSalvar.Text = "Excluir";
                    oFrmCadastroUnidadeMedida.ShowDialog();
                    oFrmCadastroUnidadeMedida.btnSalvar.Text = "Salvar";
                    oFrmCadastroUnidadeMedida.DesbloquearTxt();
                    await CarregarUnidades();
                }
            }
            else
            {
                MessageBox.Show("Selecione um item para excluir.");
            }
        }

        private async void btnSelecionar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int id = int.Parse(listView1.SelectedItems[0].SubItems[1].Text);
                UnidadeSelecionada = await controller.BuscarPorId(id);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor, selecione um item.");
            }
        }

        private async void btnPesquisar_Click(object sender, EventArgs e)
        {
            await CarregarUnidades();
        }
    }
}