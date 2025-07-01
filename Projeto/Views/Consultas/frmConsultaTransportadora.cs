using Projeto.Controller;
using Projeto.Models;
using Projeto.Views.Cadastros;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Projeto.Views.Consultas
{
    public partial class frmConsultaTransportadora : Projeto.frmBaseConsulta
    {
        private TransportadoraController controller = new TransportadoraController();
        public bool ModoSelecao { get; set; } = false; 
        internal Transportadora TransportadoraSelecionada { get; private set; } 

        public frmConsultaTransportadora() : base()
        {
            InitializeComponent();
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            frmCadastroTransportadora formCadastroTransportadora = new frmCadastroTransportadora();
            formCadastroTransportadora.FormClosed += (s, args) => CarregarTransportadoras();
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
                    item.SubItems.Add(transportadora.Tipo);
                    item.SubItems.Add(transportadora.Nome);
                    item.SubItems.Add(transportadora.Endereco);
                    item.SubItems.Add(transportadora.NumeroEndereco?.ToString() ?? "");
                    item.SubItems.Add(transportadora.Bairro);
                    item.SubItems.Add(transportadora.Complemento);
                    item.SubItems.Add(transportadora.CEP);
                    item.SubItems.Add(transportadora.NomeCidade);
                    item.SubItems.Add(transportadora.DescricaoCondicao);
                    item.SubItems.Add(transportadora.Telefone);
                    item.SubItems.Add(transportadora.Email);
                    item.SubItems.Add(transportadora.CPF_CNPJ);
                    item.SubItems.Add(transportadora.InscricaoEstadual);
                    item.SubItems.Add(transportadora.InscricaoEstadualSubTrib);
                    item.SubItems.Add(transportadora.Ativo ? "Ativo" : "Inativo");

                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar transportadoras: " + ex.Message);
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
                        item.SubItems.Add(transportadora.Tipo);
                        item.SubItems.Add(transportadora.Nome);
                        item.SubItems.Add(transportadora.Endereco);
                        item.SubItems.Add(transportadora.NumeroEndereco?.ToString() ?? "");
                        item.SubItems.Add(transportadora.Bairro);
                        item.SubItems.Add(transportadora.Complemento);
                        item.SubItems.Add(transportadora.CEP);
                        item.SubItems.Add(transportadora.NomeCidade);
                        item.SubItems.Add(transportadora.DescricaoCondicao);
                        item.SubItems.Add(transportadora.Telefone);
                        item.SubItems.Add(transportadora.Email);
                        item.SubItems.Add(transportadora.CPF_CNPJ);
                        item.SubItems.Add(transportadora.InscricaoEstadual);
                        item.SubItems.Add(transportadora.InscricaoEstadualSubTrib);
                        item.SubItems.Add(transportadora.Ativo ? "Ativo" : "Inativo");

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
                        cidade?.NomeCidade ?? "Não encontrado",
                        transportadora.IdCidade ?? 0,
                        transportadora.DescricaoCondicao ?? "Não encontrado",
                        transportadora.IdCondicao ?? 0,
                        transportadora.Ativo,
                        transportadora.DataCadastro,
                        transportadora.DataAlteracao
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
                        MessageBox.Show("Transportadora excluída com sucesso!");
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

        private void frmConsultaTransportadora_Load(object sender, EventArgs e)
        {
            CarregarTransportadoras();

            if (btnSelecionar != null)
            {
                btnSelecionar.Visible = ModoSelecao;
            }

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
                    case "Transportadora":
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
                    case "CondPgto":
                        column.Width = 150;
                        break;
                    case "Status":
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

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);

                TransportadoraSelecionada = controller.BuscarPorId(id);

                if (TransportadoraSelecionada != null)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Erro ao carregar os dados da transportadora.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma transportadora.");
            }
        }
    }
}
