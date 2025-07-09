using Projeto.Controller;
using Projeto.Models;
using Projeto.Views.Cadastros;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Projeto.Views.Consultas
{
    public partial class frmConsultaVeiculo : Projeto.frmBaseConsulta
    {
        private VeiculoController controller = new VeiculoController();

        public frmConsultaVeiculo() : base()
        {
            InitializeComponent();
        }

        private void frmConsultaVeiculo_Load(object sender, EventArgs e)
        {
            CarregarVeiculos();

            foreach (ColumnHeader column in listView1.Columns)
            {
                switch (column.Text)
                {
                    case "ID":
                        column.Width = 50;
                        break;
                    case "Placa":
                        column.Width = 100;
                        break;
                    case "Marca":
                        column.Width = 150;
                        break;
                    case "Modelo":
                        column.Width = 150;
                        break;
                    case "Ano":
                        column.Width = 70;
                        break;
                    case "Capacidade (kg)":
                        column.Width = 100;
                        break;
                    case "Transportadora":
                        column.Width = 200;
                        break;
                    case "Ativo":
                        column.Width = 60;
                        break;
                    default:
                        column.Width = 100;
                        break;
                }
            }
        }

        private void CarregarVeiculos()
        {
            try
            {
                listView1.Items.Clear();
                List<Veiculo> veiculos = controller.ListarVeiculos();

                foreach (var veiculo in veiculos)
                {
                    ListViewItem item = new ListViewItem(veiculo.Id.ToString());
                    item.SubItems.Add(veiculo.Placa);
                    item.SubItems.Add(veiculo.NomeMarca ?? ""); 
                    item.SubItems.Add(veiculo.Modelo);
                    item.SubItems.Add(veiculo.AnoFabricacao?.ToString() ?? "");
                    item.SubItems.Add(veiculo.CapacidadeCargaKg?.ToString("F2") ?? "");
                    item.SubItems.Add(veiculo.NomeTransportadora);
                    item.SubItems.Add(veiculo.Ativo ? "Ativo" : "Inativo");

                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar veículos: " + ex.Message);
            }
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            frmCadastroVeiculo formCadastro = new frmCadastroVeiculo();
            formCadastro.FormClosed += (s, args) => CarregarVeiculos();
            formCadastro.ShowDialog();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);

                Veiculo veiculo = controller.BuscarPorId(id);

                if (veiculo != null)
                {
                    var formCadastro = new frmCadastroVeiculo();
                    formCadastro.modoEdicao = true;
                    // --- ALTERAÇÃO NA CHAMADA DO MÉTODO ---
                    formCadastro.CarregarVeiculo(
                        veiculo.Id,
                        veiculo.Placa,
                        veiculo.Modelo,
                        veiculo.AnoFabricacao,
                        veiculo.CapacidadeCargaKg,
                        veiculo.Ativo,
                        veiculo.TransportadoraId,
                        veiculo.NomeTransportadora,
                        veiculo.IdMarca, // Adicionado IdMarca
                        veiculo.NomeMarca, // Adicionado NomeMarca
                        veiculo.DataCadastro,
                        veiculo.DataAlteracao
                    );

                    formCadastro.FormClosed += (s, args) => CarregarVeiculos();
                    formCadastro.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Veículo não encontrado.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um veículo para editar.");
            }
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);

                Veiculo veiculo = controller.BuscarPorId(id);

                if (veiculo != null)
                {
                    var formCadastro = new frmCadastroVeiculo
                    {
                        modoExclusao = true 
                    };

                    formCadastro.CarregarVeiculo(
                        veiculo.Id,
                        veiculo.Placa,
                        veiculo.Modelo,
                        veiculo.AnoFabricacao,
                        veiculo.CapacidadeCargaKg,
                        veiculo.Ativo,
                        veiculo.TransportadoraId,
                        veiculo.NomeTransportadora,
                        veiculo.IdMarca,
                        veiculo.NomeMarca,
                        veiculo.DataCadastro,
                        veiculo.DataAlteracao
                    );

                    formCadastro.FormClosed += (s, args) => CarregarVeiculos();
                    formCadastro.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Veículo não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um veículo para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    CarregarVeiculos();
                }
                else if (int.TryParse(texto, out int id))
                {
                    Veiculo veiculo = controller.BuscarPorId(id);

                    if (veiculo != null)
                    {
                        ListViewItem item = new ListViewItem(veiculo.Id.ToString());
                        item.SubItems.Add(veiculo.Placa);
                        item.SubItems.Add(veiculo.NomeMarca ?? ""); 
                        item.SubItems.Add(veiculo.Modelo);
                        item.SubItems.Add(veiculo.AnoFabricacao?.ToString() ?? "");
                        item.SubItems.Add(veiculo.CapacidadeCargaKg?.ToString("F2") ?? "");
                        item.SubItems.Add(veiculo.NomeTransportadora);
                        item.SubItems.Add(veiculo.Ativo ? "Ativo" : "Inativo");

                        listView1.Items.Add(item);
                    }
                    else
                    {
                        MessageBox.Show("Veículo não encontrado.");
                    }
                }
                else
                {
                    MessageBox.Show("Digite um ID válido (número inteiro) para pesquisar.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao pesquisar: " + ex.Message);
            }
        }
    }
}