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
            oFrmCadastroCliente.FormClosed += async (s, args) => await CarregarClientes();
            oFrmCadastroCliente.ShowDialog();

            /*
            frmCadastroCliente formCadastroCliente = new frmCadastroCliente();
            formCadastroCliente.FormClosed += async (s, args) => await CarregarClientes();
            formCadastroCliente.ShowDialog();
            */
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
                    item.SubItems.Add(cliente.DescricaoCondicao ?? "");
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
                    oFrmCadastroCliente.CarregarCliente(
                        cliente.Id, cliente.Nome, cliente.CPF_CNPJ, cliente.Telefone, cliente.Email,
                        cliente.Endereco, cliente.NumeroEndereco ?? 0, cliente.Bairro, cliente.Complemento, cliente.CEP,
                        cliente.Tipo, cliente.Genero, cliente.NomeCidade ?? "Não encontrado", cliente.CidadeId ?? 0,
                        cliente.DescricaoCondicao ?? "Não encontrada", cliente.IdCondicao ?? 0, cliente.Ativo, cliente.Rg,
                        cliente.DataNascimento, cliente.DataCadastro, cliente.DataAlteracao
                    );

                    oFrmCadastroCliente.FormClosed += (s, args) => CarregarClientes();
                    oFrmCadastroCliente.ShowDialog();

                    /*
                    var formCadastro = new frmCadastroCliente();
                    formCadastro.modoEdicao = true;
                    formCadastro.CarregarCliente(
                        cliente.Id,
                        cliente.Nome,
                        cliente.CPF_CNPJ,
                        cliente.Telefone,
                        cliente.Email,
                        cliente.Endereco,
                        cliente.NumeroEndereco ?? 0,
                        cliente.Bairro,
                        cliente.Complemento,
                        cliente.CEP,
                        cliente.Tipo,
                        cliente.Genero,
                        cliente.NomeCidade ?? "Não encontrado",
                        cliente.CidadeId ?? 0,
                        cliente.DescricaoCondicao ?? "Não encontrada",
                        cliente.IdCondicao ?? 0,
                        cliente.Ativo,
                        cliente.Rg,
                        cliente.DataNascimento,
                        cliente.DataCadastro,
                        cliente.DataAlteracao
                    );

                    formCadastro.FormClosed += (s, args) => CarregarClientes();
                    formCadastro.ShowDialog();

                    */
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
                    oFrmCadastroCliente.CarregarCliente(
                        cliente.Id, cliente.Nome, cliente.CPF_CNPJ, cliente.Telefone, cliente.Email,
                        cliente.Endereco, cliente.NumeroEndereco ?? 0, cliente.Bairro, cliente.Complemento, cliente.CEP,
                        cliente.Tipo, cliente.Genero, cliente.NomeCidade ?? "Não encontrado", cliente.CidadeId ?? 0,
                        cliente.DescricaoCondicao ?? "Não encontrada", cliente.IdCondicao ?? 0, cliente.Ativo, cliente.Rg,
                        cliente.DataNascimento, cliente.DataCadastro, cliente.DataAlteracao
                    );

                    oFrmCadastroCliente.FormClosed += (s, args) => CarregarClientes();
                    oFrmCadastroCliente.ShowDialog();

                    /*
                    var formCadastro = new frmCadastroCliente
                    {
                        modoExclusao = true
                    };

                    formCadastro.CarregarCliente(
                        cliente.Id,
                        cliente.Nome,
                        cliente.CPF_CNPJ,
                        cliente.Telefone,
                        cliente.Email,
                        cliente.Endereco,
                        cliente.NumeroEndereco ?? 0,
                        cliente.Bairro,
                        cliente.Complemento,
                        cliente.CEP,
                        cliente.Tipo,
                        cliente.Genero,
                        cliente.NomeCidade ?? "Não encontrado",
                        cliente.CidadeId ?? 0,
                        cliente.DescricaoCondicao ?? "Não encontrada",
                        cliente.IdCondicao ?? 0,
                        cliente.Ativo,
                        cliente.Rg,
                        cliente.DataNascimento,
                        cliente.DataCadastro,
                        cliente.DataAlteracao
                    );

                    formCadastro.FormClosed += (s, args) => CarregarClientes();
                    formCadastro.ShowDialog();
                    */
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

        private void frmConsultaCliente_Load(object sender, EventArgs e)
        {
            CarregarClientes();

            foreach (ColumnHeader column in listView1.Columns)
            {
                switch (column.Text)
                {
                    case "ID":
                        column.Width = 40;
                        break;
                    case "Tipo":
                        column.Width = 60;
                        break;
                    case "Cliente":
                        column.Width = 200;
                        break;
                    case "Gênero":
                        column.Width = 60;
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
                    case "Condição Pag.":
                        column.Width = 150;
                        break;
                    case "Telefone":
                        column.Width = 120;
                        break;
                    case "Email":
                        column.Width = 200;
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