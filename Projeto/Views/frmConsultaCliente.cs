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
    public partial class frmConsultaCliente : Projeto.frmBaseConsulta
    {
        private ClienteController controller = new ClienteController();
        public frmConsultaCliente()
        {
            InitializeComponent();
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            frmCadastroCliente formCadastroCliente = new frmCadastroCliente();
            formCadastroCliente.ShowDialog();
        }

        private void CarregarClientes()
        {
            try
            {
                listView1.Items.Clear();
                List<Cliente> clientes = controller.ListarCliente();

                foreach (var cliente in clientes)
                {
                    ListViewItem item = new ListViewItem(cliente.Id.ToString());
                    item.SubItems.Add(cliente.Nome);
                    item.SubItems.Add(cliente.CPF_CNPJ);
                    item.SubItems.Add(cliente.Telefone);
                    item.SubItems.Add(cliente.Email);
                    item.SubItems.Add(cliente.Endereco);
                    item.SubItems.Add(cliente.NumeroEndereco?.ToString() ?? "");
                    item.SubItems.Add(cliente.Bairro);
                    item.SubItems.Add(cliente.Complemento);
                    item.SubItems.Add(cliente.CEP);
                    item.SubItems.Add(cliente.Tipo);
                    item.SubItems.Add(cliente.NomeCidade);
                    item.SubItems.Add(cliente.IdCondicao?.ToString() ?? "");
                    item.SubItems.Add(cliente.Status ? "Ativo" : "Inativo");
                    item.SubItems.Add(cliente.Rg);
                    item.SubItems.Add(cliente.DataCriacao?.ToString("dd/MM/yyyy HH:mm") ?? "");
                    item.SubItems.Add(cliente.DataModificacao?.ToString("dd/MM/yyyy HH:mm") ?? "");

                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar clientes: " + ex.Message);
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
                    CarregarClientes();
                }
                else if (int.TryParse(texto, out int id))
                {
                    Cliente cliente = controller.BuscarPorId(id);

                    if (cliente != null)
                    {
                        ListViewItem item = new ListViewItem(cliente.Id.ToString());
                        item.SubItems.Add(cliente.Nome);
                        item.SubItems.Add(cliente.CPF_CNPJ);
                        item.SubItems.Add(cliente.Rg);
                        item.SubItems.Add(cliente.Telefone);
                        item.SubItems.Add(cliente.Email);
                        item.SubItems.Add(cliente.Endereco);
                        item.SubItems.Add(cliente.NumeroEndereco?.ToString() ?? "");
                        item.SubItems.Add(cliente.Complemento);
                        item.SubItems.Add(cliente.Bairro);
                        item.SubItems.Add(cliente.CEP);
                        item.SubItems.Add(cliente.NomeCidade);
                        item.SubItems.Add(cliente.Tipo);
                        item.SubItems.Add(cliente.IdCondicao?.ToString() ?? "");
                        item.SubItems.Add(cliente.Status ? "Ativo" : "Inativo");
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

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);

                Cliente cliente = controller.BuscarPorId(id);

                if (cliente != null)
                {
                    Cidade cidade = cliente.IdCidade.HasValue
                        ? new CidadeController().BuscarPorId(cliente.IdCidade.Value)
                        : null;

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
                        cidade?.Nome ?? "Não encontrado",
                        cliente.IdCidade ?? 0,
                        cliente.IdCondicao ?? 0,
                        cliente.Status,
                        cliente.Rg,
                        cliente.DataCriacao,
                        cliente.DataModificacao
                    );

                    formCadastro.FormClosed += (s, args) => CarregarClientes();
                    formCadastro.ShowDialog();
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


        private void btnDeletar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);

                var confirmacao = MessageBox.Show("Tem certeza que deseja excluir esse Cliente?", "Confirmação", MessageBoxButtons.YesNo);
                if (confirmacao == DialogResult.Yes)
                {
                    try
                    {
                        controller.Excluir(id);
                        listView1.Items.Remove(itemSelecionado);
                        MessageBox.Show("Cliente excluído com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao excluir: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um cliente para excluir.");
            }
        }
    }
}
