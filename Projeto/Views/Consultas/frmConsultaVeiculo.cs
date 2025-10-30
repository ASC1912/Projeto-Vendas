using Projeto.Controller;
using Projeto.Models;
using Projeto.Views.Cadastros;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto.Views.Consultas
{
    public partial class frmConsultaVeiculo : Projeto.frmBaseConsulta
    {
        private VeiculoController controller = new VeiculoController();
        private frmCadastroVeiculo oFrmCadastroVeiculo;

        public frmConsultaVeiculo() : base()
        {
            InitializeComponent();
        }

        public override void setFrmCadastro(object obj)
        {
            if (obj != null)
            {
                oFrmCadastroVeiculo = (frmCadastroVeiculo)obj;
            }
        }

        public override void ConhecaObj(object obj, object ctrl)
        {
            if (ctrl != null)
            {
                controller = (VeiculoController)ctrl;
            }
        }

        private async void frmConsultaVeiculo_Load(object sender, EventArgs e)
        {
            await CarregarVeiculos();
        }

        private async Task CarregarVeiculos()
        {
            try
            {
                listView1.Items.Clear();
                List<Veiculo> veiculos = await controller.ListarVeiculos();

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
                MessageBox.Show("Erro ao carregar veículos. Verifique se a API está em execução.\n\nDetalhes: " + ex.Message, "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnIncluir_Click(object sender, EventArgs e)
        {
            oFrmCadastroVeiculo.modoEdicao = false;
            oFrmCadastroVeiculo.modoExclusao = false;
            oFrmCadastroVeiculo.ConhecaObj(new Veiculo(), controller);
            oFrmCadastroVeiculo.LimparTxt();
            oFrmCadastroVeiculo.ShowDialog();
            await CarregarVeiculos();
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);
                Veiculo veiculo = await controller.BuscarPorId(id);

                if (veiculo != null)
                {
                    oFrmCadastroVeiculo.modoEdicao = true;
                    oFrmCadastroVeiculo.modoExclusao = false;
                    oFrmCadastroVeiculo.ConhecaObj(veiculo, controller);
                    oFrmCadastroVeiculo.CarregaTxt();
                    oFrmCadastroVeiculo.ShowDialog();
                    await CarregarVeiculos();
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

        private async void btnDeletar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);
                Veiculo veiculo = await controller.BuscarPorId(id);

                if (veiculo != null)
                {
                    oFrmCadastroVeiculo.modoExclusao = true;
                    oFrmCadastroVeiculo.modoEdicao = false;
                    oFrmCadastroVeiculo.ConhecaObj(veiculo, controller);
                    oFrmCadastroVeiculo.CarregaTxt();
                    oFrmCadastroVeiculo.BloquearTxt();
                    oFrmCadastroVeiculo.btnSalvar.Text = "Excluir";
                    oFrmCadastroVeiculo.ShowDialog();
                    oFrmCadastroVeiculo.btnSalvar.Text = "Salvar";
                    oFrmCadastroVeiculo.DesbloquearTxt();
                    await CarregarVeiculos();
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

        private async void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                listView1.Items.Clear();
                string texto = txtPesquisar.Text.Trim();

                if (string.IsNullOrEmpty(texto))
                {
                    await CarregarVeiculos();
                }
                else if (int.TryParse(texto, out int id))
                {
                    Veiculo veiculo = await controller.BuscarPorId(id);
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
                MessageBox.Show("Erro ao pesquisar: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}