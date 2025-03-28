﻿using Projeto.Controller;
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
                    item.SubItems.Add(cliente.CEP);
                    item.SubItems.Add(cliente.Tipo);
                    item.SubItems.Add(cliente.NomeCidade);

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
            CarregarClientes();
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);
                string nome = itemSelecionado.SubItems[1].Text;
                string cpf_cnpj = itemSelecionado.SubItems[2].Text;
                string telefone = itemSelecionado.SubItems[3].Text;
                string email = itemSelecionado.SubItems[4].Text;
                string endereco = itemSelecionado.SubItems[5].Text;
                string cep = itemSelecionado.SubItems[6].Text;
                string tipo = itemSelecionado.SubItems[7].Text;
                string nomeCidade = itemSelecionado.SubItems[8].Text;

                var formCadastro = new frmCadastroCliente();
                formCadastro.CarregarCliente(id, nome, cpf_cnpj, telefone, email, endereco, cep, tipo, nomeCidade);

                formCadastro.FormClosed += (s, args) => CarregarClientes();
                formCadastro.ShowDialog();
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
