using Projeto.Controller;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto.Views
{
    public partial class frmConsultaPais : Projeto.frmBaseConsulta
    {
        frmCadastroPais oFrmCadastroPais;
        Pais oPais;
        private PaisController controller = new PaisController();
        public bool ModoSelecao { get; set; } = false;
        internal Pais PaisSelecionado { get; private set; }

        public frmConsultaPais() : base()
        {
            InitializeComponent();
        }

        public override void ConhecaObj(object obj, object ctrl)
        {
            if (obj != null)
            {
                oPais = (Pais)obj;
            }
            if (ctrl != null)
            {
                controller = (PaisController)ctrl;
            }
        }

        public override void setFrmCadastro(object obj)
        {
            if (obj != null)
            {
                oFrmCadastroPais = (frmCadastroPais)obj;
            }
        }

        private async void frmConsultaPais_Load(object sender, EventArgs e)
        {
            btnSelecionar.Visible = ModoSelecao;
            await CarregarPaises();

            foreach (ColumnHeader column in listView1.Columns)
            {
                switch (column.Text)
                {
                    case "ID":
                        column.Width = 50;
                        break;
                    case "País":
                        column.Width = 150;
                        break;
                    case "Sigla": 
                        column.Width = 60;
                        break;
                    case "DDI": 
                        column.Width = 60;
                        break;
                    case "Moeda": 
                        column.Width = 70;
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
            oFrmCadastroPais.FormClosed += async (s, args) => await CarregarPaises();
            oFrmCadastroPais.ConhecaObj(oPais, controller);
            oFrmCadastroPais.ShowDialog();

            /*
            frmCadastroPais formCadastroPais = new frmCadastroPais();
            formCadastroPais.FormClosed += async (s, args) => await CarregarPaises();
            formCadastroPais.ShowDialog();
            */
        }

        private async Task CarregarPaises()
        {
            try
            {
                listView1.Items.Clear();
                List<Pais> paises = await controller.ListarPais();

                foreach (var pais in paises)
                {
                    ListViewItem item = new ListViewItem(pais.Id.ToString());
                    item.SubItems.Add(pais.NomePais);
                    item.SubItems.Add(pais.Sigla);      
                    item.SubItems.Add(pais.DDI);        
                    item.SubItems.Add(pais.Moeda);     
                    item.SubItems.Add(pais.Ativo ? "Ativo" : "Inativo");
                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar países. Verifique se a API está em execução.\n\nDetalhes: " + ex.Message, "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    await CarregarPaises();
                }
                else if (int.TryParse(texto, out int id))
                {
                    Pais pais = await controller.BuscarPorId(id);
                    if (pais != null)
                    {
                        ListViewItem item = new ListViewItem(pais.Id.ToString());
                        item.SubItems.Add(pais.NomePais);
                        item.SubItems.Add(pais.Sigla);      
                        item.SubItems.Add(pais.DDI);        
                        item.SubItems.Add(pais.Moeda);      
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
                MessageBox.Show("Erro ao pesquisar: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);
                Pais pais = await controller.BuscarPorId(id);

                if (pais != null)
                {
                    oFrmCadastroPais.modoEdicao = true;
                    oFrmCadastroPais.CarregarPais(pais.Id, pais.NomePais, pais.Sigla, pais.DDI, pais.Moeda, pais.Ativo, pais.DataCadastro, pais.DataAlteracao);
                    oFrmCadastroPais.FormClosed += async (s, args) => await CarregarPaises();
                    oFrmCadastroPais.ConhecaObj(oPais, controller);
                    oFrmCadastroPais.ShowDialog();

                    /*
                    var formCadastro = new frmCadastroPais { modoEdicao = true };
                    formCadastro.CarregarPais(pais.Id, pais.NomePais, pais.Sigla, pais.DDI, pais.Moeda, pais.Ativo, pais.DataCadastro, pais.DataAlteracao);
                    formCadastro.FormClosed += async (s, args) => await CarregarPaises();
                    formCadastro.ShowDialog();
                    */
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

        private async void btnDeletar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);
                Pais pais = await controller.BuscarPorId(id);

                if (pais != null)
                {
                    oFrmCadastroPais.modoExclusao = true;
                    oFrmCadastroPais.CarregarPais(pais.Id, pais.NomePais, pais.Sigla, pais.DDI, pais.Moeda, pais.Ativo, pais.DataCadastro, pais.DataAlteracao);
                    oFrmCadastroPais.FormClosed += async (s, args) => await CarregarPaises();
                    oFrmCadastroPais.ShowDialog();

                    /*
                    var formCadastro = new frmCadastroPais { modoExclusao = true };
                    formCadastro.CarregarPais(pais.Id, pais.NomePais, pais.Sigla, pais.DDI, pais.Moeda, pais.Ativo, pais.DataCadastro, pais.DataAlteracao);
                    formCadastro.FormClosed += async (s, args) => await CarregarPaises();
                    formCadastro.ShowDialog();
                    */
                }
                else
                {
                    MessageBox.Show("País não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um país para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnSelecionar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);
                PaisSelecionado = await controller.BuscarPorId(id);
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