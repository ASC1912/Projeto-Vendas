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
    public partial class frmConsultaTransportadora : Projeto.frmBaseConsulta
    {
        private TransportadoraController controller = new TransportadoraController();
        public frmConsultaTransportadora() : base() 
        {
            InitializeComponent();
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            frmCadastroTransportadora formCadastroTransportadora = new frmCadastroTransportadora();
            formCadastroTransportadora.ShowDialog();
        }

        private void CarregarTransportadoras()
        {
            try
            {
                listView1.Items.Clear();
                List<Transportadora> transportadoras = controller.ListarTransportadora();

                foreach (var transportadora in transportadoras)
                {
                    ListViewItem item = new ListViewItem(transportadora.Id.ToString());
                    item.SubItems.Add(transportadora.Nome);
                    item.SubItems.Add(transportadora.CPF_CNPJ);
                    item.SubItems.Add(transportadora.Telefone);
                    item.SubItems.Add(transportadora.Email);
                    item.SubItems.Add(transportadora.Endereco);
                    item.SubItems.Add(transportadora.NumeroEndereco.ToString());
                    item.SubItems.Add(transportadora.Bairro);
                    item.SubItems.Add(transportadora.Complemento);
                    item.SubItems.Add(transportadora.CEP);
                    item.SubItems.Add(transportadora.Tipo);
                    item.SubItems.Add(transportadora.InscricaoEstadual);
                    item.SubItems.Add(transportadora.InscricaoEstadualSubTrib);
                    item.SubItems.Add(transportadora.NomeCidade);
                    item.SubItems.Add(transportadora.IdCondicao.ToString());
                    item.SubItems.Add(transportadora.Status ? "Ativo" : "Inativo");
                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar transportadora: " + ex.Message);
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
                    CarregarTransportadoras();
                }
                else if (int.TryParse(texto, out int id))
                {
                    Transportadora transportadora = controller.BuscarPorId(id);

                    if (transportadora != null)
                    {
                        ListViewItem item = new ListViewItem(transportadora.Id.ToString());
                        item.SubItems.Add(transportadora.Nome);
                        item.SubItems.Add(transportadora.CPF_CNPJ);
                        item.SubItems.Add(transportadora.Telefone);
                        item.SubItems.Add(transportadora.Email);
                        item.SubItems.Add(transportadora.Endereco);
                        item.SubItems.Add(transportadora.NumeroEndereco.ToString());
                        item.SubItems.Add(transportadora.Bairro);
                        item.SubItems.Add(transportadora.Complemento);
                        item.SubItems.Add(transportadora.CEP);
                        item.SubItems.Add(transportadora.Tipo);
                        item.SubItems.Add(transportadora.InscricaoEstadual);
                        item.SubItems.Add(transportadora.InscricaoEstadualSubTrib);
                        item.SubItems.Add(transportadora.NomeCidade);
                        item.SubItems.Add(transportadora.IdCondicao?.ToString() ?? "");
                        item.SubItems.Add(transportadora.Status ? "Ativo" : "Inativo");
                        listView1.Items.Add(item);
                    }
                    else
                    {
                        MessageBox.Show("Transportadora não encontrada.");
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

                Transportadora transportadora = controller.BuscarPorId(id);

                if (transportadora != null)
                {
                    Cidade cidade = transportadora.IdCidade.HasValue
                        ? new CidadeController().BuscarPorId(transportadora.IdCidade.Value)
                        : null;

                    var formCadastro = new frmCadastroTransportadora();
                    formCadastro.modoEdicao = true;
                    formCadastro.CarregarTransportadora(
                        transportadora.Id,
                        transportadora.Nome,
                        transportadora.CPF_CNPJ,
                        transportadora.Telefone,
                        transportadora.Email,
                        transportadora.Endereco,
                        transportadora.NumeroEndereco ?? 0,
                        transportadora.Bairro,
                        transportadora.Complemento,
                        transportadora.CEP,
                        transportadora.InscricaoEstadual,
                        transportadora.InscricaoEstadualSubTrib,
                        transportadora.Tipo,
                        cidade?.Nome ?? "Não encontrado",
                        transportadora.IdCidade ?? 0,
                        transportadora.IdCondicao ?? 0,
                        transportadora.Status,
                        transportadora.DataCriacao,
                        transportadora.DataModificacao
                    );

                    formCadastro.FormClosed += (s, args) => CarregarTransportadoras();
                    formCadastro.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Transportadora não encontrada.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma transportadora para editar.");
            }
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);

                var confirmacao = MessageBox.Show("Tem certeza que deseja excluir essa Transportadora?", "Confirmação", MessageBoxButtons.YesNo);
                if (confirmacao == DialogResult.Yes)
                {
                    try
                    {
                        controller.Excluir(id);
                        listView1.Items.Remove(itemSelecionado);
                        MessageBox.Show("Transportador excluído com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao excluir: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma transportadora para excluir.");
            }
        }
    }
}
