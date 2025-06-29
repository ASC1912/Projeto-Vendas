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
    public partial class frmConsultaPais : Projeto.frmBaseConsulta
    {
        private PaisController controller = new PaisController();
        public bool ModoSelecao { get; set; } = false;
        internal Pais PaisSelecionado { get; private set; }


        public frmConsultaPais() : base()
        {
            InitializeComponent();
        }

        private void frmConsultaPais_Load(object sender, EventArgs e)
        {
            btnSelecionar.Visible = ModoSelecao;

            foreach (ColumnHeader column in listView1.Columns)
            {
                switch (column.Text)
                {
                    case "ID":
                        column.Width = 50;
                        break;
                    case "Nome":
                        column.Width = 100;
                        break;
                    case "Status":
                        column.Width = 50;
                        break;
                    default:
                        column.Width = 100;
                        break;
                }
            }
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            frmCadastroPais formCadastroPais = new frmCadastroPais();
            formCadastroPais.ShowDialog();
        }

        private void CarregarPaises()
        {
            try
            {
                listView1.Items.Clear();
                List<Pais> paises = controller.ListarPais();

                foreach (var pais in paises)
                {
                    ListViewItem item = new ListViewItem(pais.Id.ToString());
                    item.SubItems.Add(pais.NomePais);
                    item.SubItems.Add(pais.Ativo ? "Ativo" : "Inativo");
                    listView1.Items.Add(item);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os paises: " + ex.Message);
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
                    CarregarPaises(); 
                }
                else if (int.TryParse(texto, out int id))
                {
                    Pais pais = controller.BuscarPorId(id);

                    if (pais != null)
                    {
                        ListViewItem item = new ListViewItem(pais.Id.ToString());
                        item.SubItems.Add(pais.NomePais);
                        item.SubItems.Add(pais.Ativo ? "Ativo" : "Inativo");
                        listView1.Items.Add(item);
                    }
                    else
                    {
                        MessageBox.Show("País não encontrado.");
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

                Pais pais = controller.BuscarPorId(id);

                if (pais != null)
                {
                    var formCadastro = new frmCadastroPais();
                    formCadastro.modoEdicao = true;

                    formCadastro.CarregarPais(
                        pais.Id,
                        pais.NomePais,
                        pais.Ativo,
                        pais.DataCadastro,
                        pais.DataAlteracao
                    );


                    formCadastro.FormClosed += (s, args) => CarregarPaises();
                    formCadastro.ShowDialog();
                }
                else
                {
                    MessageBox.Show("País não encontrado.");
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

                var confirmacao = MessageBox.Show("Tem certeza que deseja excluir esse País?", "Confirmação", MessageBoxButtons.YesNo);
                if (confirmacao == DialogResult.Yes)
                {
                    try
                    {
                        controller.Excluir(id);
                        listView1.Items.Remove(itemSelecionado);
                        MessageBox.Show("País excluído com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao excluir: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um país para excluir.");
            }
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);
                string nome = itemSelecionado.SubItems[1].Text;

                PaisSelecionado = new Pais
                {
                    Id = id,
                    NomePais = nome
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
