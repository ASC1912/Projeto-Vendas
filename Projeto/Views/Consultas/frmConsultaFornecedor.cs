using Projeto.Controller;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto.Views
{
    public partial class frmConsultaFornecedor : Projeto.frmBaseConsulta
    {
        private FornecedorController controller = new FornecedorController();
        private CidadeController cidadeController = new CidadeController();
        internal Fornecedor FornecedorSelecionado { get; private set; } 
        frmCadastroFornecedor oFrmCadastroFornecedor;


        public frmConsultaFornecedor() : base()
        {
            InitializeComponent();
        }

        public override void setFrmCadastro(object obj)
        {
            if (obj != null)
            {
                oFrmCadastroFornecedor = (frmCadastroFornecedor)obj;
            }
        }
        public override void ConhecaObj(object obj, object ctrl)
        {
            if (ctrl != null)
            {
                controller = (FornecedorController)ctrl;
            }
        }
        private async void btnIncluir_Click(object sender, EventArgs e)
        {
            oFrmCadastroFornecedor.modoEdicao = false;
            oFrmCadastroFornecedor.modoExclusao = false;
            oFrmCadastroFornecedor.LimparTxt();
            oFrmCadastroFornecedor.DesbloquearTxt();
            oFrmCadastroFornecedor.ShowDialog();
            await CarregarFornecedores();
        }

        private async Task CarregarFornecedores()
        {
            try
            {
                listView1.Items.Clear();
                List<Fornecedor> fornecedores = await controller.ListarFornecedor();

                foreach (var fornecedor in fornecedores)
                {
                    ListViewItem item = new ListViewItem(fornecedor.Id.ToString());
                    item.SubItems.Add(fornecedor.Tipo);
                    item.SubItems.Add(fornecedor.Nome);
                    item.SubItems.Add(fornecedor.Endereco);
                    item.SubItems.Add(fornecedor.NumeroEndereco?.ToString() ?? "");
                    item.SubItems.Add(fornecedor.Bairro);
                    item.SubItems.Add(fornecedor.Complemento);
                    item.SubItems.Add(fornecedor.CEP);
                    item.SubItems.Add(fornecedor.NomeCidade ?? "");
                    item.SubItems.Add(fornecedor.oCondicaoPagamento?.Descricao ?? "");
                    item.SubItems.Add(fornecedor.Telefone);
                    item.SubItems.Add(fornecedor.Email);
                    item.SubItems.Add(fornecedor.CPF_CNPJ);
                    item.SubItems.Add(fornecedor.InscricaoEstadual);
                    item.SubItems.Add(fornecedor.InscricaoEstadualSubTrib);
                    item.SubItems.Add(fornecedor.Ativo ? "Ativo" : "Inativo");
                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar fornecedor: " + ex.Message);
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
                    await CarregarFornecedores();
                }
                else if (int.TryParse(texto, out int id))
                {
                    Fornecedor fornecedor = await controller.BuscarPorId(id);

                    if (fornecedor != null)
                    {
                        ListViewItem item = new ListViewItem(fornecedor.Id.ToString());
                        item.SubItems.Add(fornecedor.Tipo);
                        item.SubItems.Add(fornecedor.Nome);
                        item.SubItems.Add(fornecedor.Endereco);
                        item.SubItems.Add(fornecedor.NumeroEndereco?.ToString() ?? "");
                        item.SubItems.Add(fornecedor.Bairro);
                        item.SubItems.Add(fornecedor.Complemento);
                        item.SubItems.Add(fornecedor.CEP);
                        item.SubItems.Add(fornecedor.NomeCidade ?? "");
                        item.SubItems.Add(fornecedor.DescricaoCondicao ?? "");
                        item.SubItems.Add(fornecedor.Telefone);
                        item.SubItems.Add(fornecedor.Email);
                        item.SubItems.Add(fornecedor.CPF_CNPJ);
                        item.SubItems.Add(fornecedor.InscricaoEstadual);
                        item.SubItems.Add(fornecedor.InscricaoEstadualSubTrib);
                        item.SubItems.Add(fornecedor.Ativo ? "Ativo" : "Inativo");
                        listView1.Items.Add(item);
                    }
                    else
                    {
                        MessageBox.Show("Fornecedor não encontrado.");
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

                Fornecedor fornecedor = await controller.BuscarPorId(id);

                if (fornecedor != null)
                {
                    oFrmCadastroFornecedor.modoEdicao = true;
                    oFrmCadastroFornecedor.modoExclusao = false;
                    oFrmCadastroFornecedor.ConhecaObj(fornecedor, controller);
                    oFrmCadastroFornecedor.CarregaTxt();
                    oFrmCadastroFornecedor.DesbloquearTxt();
                    oFrmCadastroFornecedor.ShowDialog();
                    await CarregarFornecedores();
                }
                else
                {
                    MessageBox.Show("Fornecedor não encontrado.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um fornecedor para editar.");
            }
        }

        private async void btnDeletar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);

                Fornecedor fornecedor = await controller.BuscarPorId(id);

                if (fornecedor != null)
                {
                    oFrmCadastroFornecedor.modoExclusao = true;
                    oFrmCadastroFornecedor.modoEdicao = false;
                    oFrmCadastroFornecedor.ConhecaObj(fornecedor, controller);
                    oFrmCadastroFornecedor.LimparTxt();
                    oFrmCadastroFornecedor.CarregaTxt();
                    oFrmCadastroFornecedor.BloquearTxt();
                    oFrmCadastroFornecedor.btnSalvar.Text = "Excluir";
                    oFrmCadastroFornecedor.ShowDialog();
                    oFrmCadastroFornecedor.btnSalvar.Text = "Salvar";
                    oFrmCadastroFornecedor.DesbloquearTxt();
                    await CarregarFornecedores();
                }
                else
                {
                    MessageBox.Show("Fornecedor não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um fornecedor para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private async void btnSelecionar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                try
                {
                    var itemSelecionado = listView1.SelectedItems[0];
                    int id = int.Parse(itemSelecionado.SubItems[0].Text);

                    FornecedorSelecionado = await controller.BuscarPorId(id);

                    if (FornecedorSelecionado != null)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Não foi possível carregar os dados do fornecedor selecionado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao selecionar o fornecedor: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um fornecedor na lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void frmConsultaFornecedor_Load(object sender, EventArgs e)
        {
            btnSelecionar.Visible = ModoSelecao;
            await CarregarFornecedores();

            foreach (ColumnHeader column in listView1.Columns)
            {
                column.TextAlign = HorizontalAlignment.Center;

                switch (column.Text)
                {
                    case "ID":
                        column.Width = 40;
                        column.TextAlign = HorizontalAlignment.Right; 
                        break;
                    case "Tipo":
                        column.Width = 60;
                        break;
                    case "Fornecedor":
                        column.Width = 200;
                        break;
                    case "Endereço":
                        column.Width = 200;
                        break;
                    case "Número":
                        column.Width = 60;
                        break;
                    case "Bairro":
                        column.Width = 150;
                        break;
                    case "Complemento":
                        column.Width = 130;
                        break;
                    case "CEP":
                        column.Width = 80;
                        break;
                    case "Cidade":
                        column.Width = 150;
                        break;
                    case "Telefone":
                        column.Width = 120;
                        break;
                    case "Email":
                        column.Width = 200;
                        break;
                    case "Condição Pagamento":
                        column.Width = 150;
                        break;
                    case "Ativo":
                        column.Width = 60;
                        break;
                    case "CPF/CNPJ":
                        column.Width = 130;
                        break;
                    default:
                        column.Width = 100;
                        break;
                }
            }
        }
    }
}