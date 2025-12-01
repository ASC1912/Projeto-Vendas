using Projeto.Controller;
using Projeto.Models;
using Projeto.Views.Cadastros;
using System;
using System.Collections.Generic;
using System.Threading.Tasks; 
using System.Windows.Forms;

namespace Projeto.Views.Consultas
{
    public partial class frmConsultaGrupo : Projeto.frmBaseConsulta
    {
        private GrupoController controller = new GrupoController();
        internal Grupo GrupoSelecionado { get; private set; }
        private frmCadastroGrupo oFrmCadastroGrupo;

        public frmConsultaGrupo()
        {
            InitializeComponent();
        }

        public override void setFrmCadastro(object obj)
        {
            if (obj != null)
            {
                oFrmCadastroGrupo = (frmCadastroGrupo)obj;
            }
        }

        public override void ConhecaObj(object obj, object ctrl)
        {
            if (ctrl != null)
            {
                controller = (GrupoController)ctrl;
            }
        }

        private async void frmConsultaGrupo_Load(object sender, EventArgs e)
        {
            await CarregarGrupos();
            btnSelecionar.Visible = ModoSelecao;
        }
        private async Task CarregarGrupos()
        {
            try
            {
                listView1.Items.Clear();
                List<Grupo> grupos = await controller.ListarGrupos();

                foreach (var grupo in grupos)
                {
                    ListViewItem item = new ListViewItem("");
                    item.SubItems.Add(grupo.Id.ToString());
                    item.SubItems.Add(grupo.NomeGrupo);
                    item.SubItems.Add(grupo.Descricao);
                    item.SubItems.Add(grupo.Ativo ? "Ativo" : "Inativo");
                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os grupos: " + ex.Message);
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
                    await CarregarGrupos();
                }
                else if (int.TryParse(texto, out int id))
                {
                    Grupo grupo = await controller.BuscarPorId(id);

                    if (grupo != null)
                    {
                        ListViewItem item = new ListViewItem("");
                        item.SubItems.Add(grupo.Id.ToString());
                        item.SubItems.Add(grupo.NomeGrupo);
                        item.SubItems.Add(grupo.Descricao);
                        item.SubItems.Add(grupo.Ativo ? "Ativo" : "Inativo");
                        listView1.Items.Add(item);
                    }
                    else
                    {
                        MessageBox.Show("Grupo não encontrado.");
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
            oFrmCadastroGrupo.modoEdicao = false;
            oFrmCadastroGrupo.modoExclusao = false;
            oFrmCadastroGrupo.LimparTxt();
            oFrmCadastroGrupo.ShowDialog();
            await CarregarGrupos();
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[1].Text);

                Grupo grupo = await controller.BuscarPorId(id);

                if (grupo != null)
                {
                    oFrmCadastroGrupo.modoEdicao = true;
                    oFrmCadastroGrupo.modoExclusao = false;
                    oFrmCadastroGrupo.ConhecaObj(grupo, controller);
                    oFrmCadastroGrupo.LimparTxt();
                    oFrmCadastroGrupo.CarregaTxt();
                    oFrmCadastroGrupo.ShowDialog();
                    await CarregarGrupos();
                }
                else
                {
                    MessageBox.Show("Grupo não encontrado.");
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
                int id = int.Parse(itemSelecionado.SubItems[1].Text);

                Grupo grupo = await controller.BuscarPorId(id);

                if (grupo != null)
                {
                    oFrmCadastroGrupo.modoExclusao = true;
                    oFrmCadastroGrupo.modoEdicao = false;
                    oFrmCadastroGrupo.ConhecaObj(grupo, controller);
                    oFrmCadastroGrupo.LimparTxt();
                    oFrmCadastroGrupo.CarregaTxt();
                    oFrmCadastroGrupo.BloquearTxt();
                    oFrmCadastroGrupo.btnSalvar.Text = "Excluir";
                    oFrmCadastroGrupo.ShowDialog();
                    oFrmCadastroGrupo.btnSalvar.Text = "Salvar";
                    oFrmCadastroGrupo.DesbloquearTxt();
                    await CarregarGrupos();
                }
                else
                {
                    MessageBox.Show("Grupo não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um grupo para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnSelecionar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[1].Text);

                GrupoSelecionado = await controller.BuscarPorId(id);

                if (GrupoSelecionado != null)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Erro ao carregar os dados do grupo.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um grupo.");
            }
        }
    }
}