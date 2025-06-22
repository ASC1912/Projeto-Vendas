using Projeto.Controller;
using Projeto.Models;
using Projeto.Views.Cadastros;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Projeto.Views.Consultas
{
    public partial class frmConsultaGrupo : Projeto.frmBaseConsulta
    {
        private GrupoController controller = new GrupoController();
        public bool ModoSelecao { get; set; } = false;
        internal Grupo GrupoSelecionado { get; private set; }


        public frmConsultaGrupo()
        {
            InitializeComponent();
        }

        private void frmConsultaGrupo_Load(object sender, EventArgs e)
        {
            btnSelecionar.Visible = ModoSelecao;
        }

        private void CarregarGrupos()
        {
            try
            {
                listView1.Items.Clear();
                List<Grupo> grupos = controller.ListarGrupos();

                foreach (var grupo in grupos)
                {
                    ListViewItem item = new ListViewItem(grupo.Id.ToString());
                    item.SubItems.Add(grupo.Nome);
                    item.SubItems.Add(grupo.Descricao);
                    item.SubItems.Add(grupo.Status ? "Ativo" : "Inativo");
                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os grupos: " + ex.Message);
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
                    CarregarGrupos();
                }
                else if (int.TryParse(texto, out int id))
                {
                    Grupo grupo = controller.BuscarPorId(id);

                    if (grupo != null)
                    {
                        ListViewItem item = new ListViewItem(grupo.Id.ToString());
                        item.SubItems.Add(grupo.Nome);
                        item.SubItems.Add(grupo.Descricao);
                        item.SubItems.Add(grupo.Status ? "Ativo" : "Inativo");
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

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            frmCadastroGrupo formCadastroGrupo = new frmCadastroGrupo();
            formCadastroGrupo.ShowDialog();
            CarregarGrupos();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);

                Grupo grupo = controller.BuscarPorId(id);

                if (grupo != null)
                {
                    var formCadastro = new frmCadastroGrupo();
                    formCadastro.CarregarGrupo(
                        grupo.Id,
                        grupo.Nome,
                        grupo.Descricao,
                        grupo.Status,
                        grupo.DataCriacao,
                        grupo.DataModificacao
                    );

                    formCadastro.FormClosed += (s, args) => CarregarGrupos();
                    formCadastro.ShowDialog();
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

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);

                var confirmacao = MessageBox.Show("Tem certeza que deseja excluir esse grupo?", "Confirmação", MessageBoxButtons.YesNo);
                if (confirmacao == DialogResult.Yes)
                {
                    try
                    {
                        controller.Excluir(id);
                        listView1.Items.Remove(itemSelecionado);
                        MessageBox.Show("Grupo excluído com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao excluir: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um grupo para excluir.");
            }
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);

                GrupoSelecionado = controller.BuscarPorId(id);

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
