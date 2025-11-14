using Projeto.Controller;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto.Views
{
    public partial class frmConsultaCliente : Projeto.frmBaseConsulta
    {
        private ClienteController controller = new ClienteController();
        frmCadastroCliente oFrmCadastroCliente;
        internal Cliente ClienteSelecionado { get; private set; }

        public override void setFrmCadastro(object obj)
        {
            if (obj != null)
            {
                oFrmCadastroCliente = (frmCadastroCliente)obj;
            }
        }

        public override void ConhecaObj(object obj, object ctrl)
        {
            if (ctrl != null)
            {
                controller = (ClienteController)ctrl;
            }
        }

        public frmConsultaCliente() : base()
        {
            InitializeComponent();
        }

        private async void btnIncluir_Click(object sender, EventArgs e)
        {
            oFrmCadastroCliente.modoEdicao = false;
            oFrmCadastroCliente.modoExclusao = false;
            oFrmCadastroCliente.LimparTxt();
            oFrmCadastroCliente.DesbloquearTxt(); 
            oFrmCadastroCliente.ShowDialog();
            await CarregarClientes();
        }

        private async Task CarregarClientes()
        {
            try
            {
                listView1.Items.Clear();
                List<Cliente> clientes = await controller.ListarCliente();

                foreach (var cliente in clientes)
                {
                    ListViewItem item = new ListViewItem(cliente.Id.ToString());
                    item.SubItems.Add(cliente.Tipo);
                    item.SubItems.Add(cliente.Nome);
                    item.SubItems.Add(cliente.Genero);
                    item.SubItems.Add(cliente.Endereco);
                    item.SubItems.Add(cliente.NumeroEndereco?.ToString() ?? "");
                    item.SubItems.Add(cliente.Bairro);
                    item.SubItems.Add(cliente.Complemento);
                    item.SubItems.Add(cliente.CEP);
                    item.SubItems.Add(cliente.NomeCidade);
                    item.SubItems.Add(cliente.oCondicaoPagamento?.Descricao ?? "");
                    item.SubItems.Add(cliente.Telefone);
                    item.SubItems.Add(cliente.Email);
                    item.SubItems.Add(cliente.CPF_CNPJ);
                    item.SubItems.Add(cliente.Rg);
                    item.SubItems.Add(cliente.DataNascimento?.ToShortDateString() ?? "");
                    item.SubItems.Add(cliente.Ativo ? "Ativo" : "Inativo");

                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar clientes: " + ex.Message);
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
                    await CarregarClientes();
                }
                else if (int.TryParse(texto, out int id))
                {
                    Cliente cliente = await controller.BuscarPorId(id);

                    if (cliente != null)
                    {
                        ListViewItem item = new ListViewItem(cliente.Id.ToString());
                        item.SubItems.Add(cliente.Tipo);
                        item.SubItems.Add(cliente.Nome);
                        item.SubItems.Add(cliente.Genero);
                        item.SubItems.Add(cliente.Endereco);
                        item.SubItems.Add(cliente.NumeroEndereco?.ToString() ?? "");
                        item.SubItems.Add(cliente.Complemento);
                        item.SubItems.Add(cliente.Bairro);
                        item.SubItems.Add(cliente.CEP);
                        item.SubItems.Add(cliente.NomeCidade);
                        item.SubItems.Add(cliente.DescricaoCondicao ?? "");
                        item.SubItems.Add(cliente.Telefone);
                        item.SubItems.Add(cliente.Email);
                        item.SubItems.Add(cliente.CPF_CNPJ);
                        item.SubItems.Add(cliente.Rg);
                        item.SubItems.Add(cliente.DataNascimento?.ToShortDateString() ?? "");
                        item.SubItems.Add(cliente.Ativo ? "Ativo" : "Inativo");
                        listView1.Items.Add(item);
                    }
                    else
                    {
                        MessageBox.Show("Cliente não encontrado.");
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

                Cliente cliente = await controller.BuscarPorId(id);

                if (cliente != null)
                {
                    oFrmCadastroCliente.modoEdicao = true;
                    oFrmCadastroCliente.modoExclusao = false;
                    oFrmCadastroCliente.ConhecaObj(cliente, controller);
                    oFrmCadastroCliente.CarregaTxt();
                    oFrmCadastroCliente.DesbloquearTxt(); 
                    oFrmCadastroCliente.ShowDialog();
                    await CarregarClientes();
                }
                else
                {
                    MessageBox.Show("Cliente não encontrado.");
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

                Cliente cliente = await controller.BuscarPorId(id);

                if (cliente != null)
                {
                    oFrmCadastroCliente.modoExclusao = true;
                    oFrmCadastroCliente.modoEdicao = false;
                    oFrmCadastroCliente.ConhecaObj(cliente, controller);
                    oFrmCadastroCliente.LimparTxt();
                    oFrmCadastroCliente.CarregaTxt();
                    oFrmCadastroCliente.BloquearTxt();
                    oFrmCadastroCliente.btnSalvar.Text = "Excluir";
                    oFrmCadastroCliente.ShowDialog();
                    oFrmCadastroCliente.btnSalvar.Text = "Salvar";
                    oFrmCadastroCliente.DesbloquearTxt();
                    await CarregarClientes();
                }
                else
                {
                    MessageBox.Show("Cliente não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um cliente para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void frmConsultaCliente_Load(object sender, EventArgs e)
        {
            await CarregarClientes();
            btnSelecionar.Visible = ModoSelecao; 
        }

        private async void btnSelecionar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                try
                {
                    var itemSelecionado = listView1.SelectedItems[0];
                    int id = int.Parse(itemSelecionado.SubItems[0].Text);

                    ClienteSelecionado = await controller.BuscarPorId(id);

                    if (ClienteSelecionado != null)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Não foi possível carregar os dados do cliente selecionado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao selecionar o cliente: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um cliente na lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}