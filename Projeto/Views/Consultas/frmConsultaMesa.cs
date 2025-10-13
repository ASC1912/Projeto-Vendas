using Projeto.Controller;
using Projeto.Models;
using Projeto.Views.Cadastros;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto.Views.Consultas
{
    public partial class frmConsultaMesa : frmBaseConsulta
    {
        private MesaController controller = new MesaController();
        private frmCadastroMesa oFrmCadastroMesa;
        internal Mesa MesaSelecionada { get; private set; } 


        public frmConsultaMesa()
        {
            InitializeComponent();
        }

        public override void setFrmCadastro(object obj)
        {
            if (obj != null)
            {
                oFrmCadastroMesa = (frmCadastroMesa)obj;
            }
        }

        public override void ConhecaObj(object obj, object ctrl)
        {
            if (ctrl != null)
            {
                controller = (MesaController)ctrl;
            }
        }

        private async void frmConsultaMesa_Load(object sender, EventArgs e)
        {
            await CarregarMesas();

            if (btnSelecionar != null) 
            {
                btnSelecionar.Visible = ModoSelecao;
            }

            foreach (ColumnHeader column in listView1.Columns)
            {
                column.TextAlign = HorizontalAlignment.Center;
                switch (column.Text)
                {
                    case "Número":
                        column.Width = 80;
                        column.TextAlign = HorizontalAlignment.Right;
                        break;
                    case "Cadeiras":
                        column.Width = 100;
                        break;
                    case "Localização":
                        column.Width = 250;
                        break;
                    case "Status":
                        column.Width = 120;
                        break;
                    case "Ativo":
                        column.Width = 80;
                        break;
                    case "Data de Cadastro": 
                        column.Width = 150;
                        break;
                    case "Data de Modificação": 
                        column.Width = 150;
                        break;
                    default:
                        column.Width = 120;
                        break;
                }
            }
        }

        private async Task CarregarMesas()
        {
            try
            {
                listView1.Items.Clear();
                List<Mesa> mesas = await controller.ListarMesas();

                foreach (var mesa in mesas)
                {
                    ListViewItem item = new ListViewItem(mesa.NumeroMesa.ToString());
                    item.SubItems.Add(mesa.QuantidadeCadeiras.ToString());
                    item.SubItems.Add(mesa.Localizacao);

                    string status;
                    switch (mesa.StatusMesa)
                    {
                        case 'O':
                            status = "OCUPADO";
                            break;
                        case 'R':
                            status = "RESERVADO";
                            break;
                        default:
                            status = "LIVRE";
                            break;
                    }
                    item.SubItems.Add(status);

                    item.SubItems.Add(mesa.Ativo ? "Ativo" : "Inativo");
                    item.SubItems.Add(mesa.DataCadastro?.ToString("g") ?? "-");
                    item.SubItems.Add(mesa.DataAlteracao?.ToString("g") ?? "-");
                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar mesas: " + ex.Message);
            }
        }

        private async void btnIncluir_Click(object sender, EventArgs e)
        {
            oFrmCadastroMesa.modoEdicao = false;
            oFrmCadastroMesa.modoExclusao = false;
            oFrmCadastroMesa.LimparTxt();
            oFrmCadastroMesa.ShowDialog();
            await CarregarMesas();
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int numeroMesa = int.Parse(itemSelecionado.SubItems[0].Text);

                Mesa mesa = await controller.BuscarPorNumero(numeroMesa);

                if (mesa != null)
                {
                    oFrmCadastroMesa.modoEdicao = true;
                    oFrmCadastroMesa.modoExclusao = false;
                    oFrmCadastroMesa.ConhecaObj(mesa, controller);
                    oFrmCadastroMesa.CarregaTxt();
                    oFrmCadastroMesa.ShowDialog();
                    await CarregarMesas();
                }
                else
                {
                    MessageBox.Show("Mesa não encontrada.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma mesa para editar.");
            }
        }

        private async void btnDeletar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int numeroMesa = int.Parse(itemSelecionado.SubItems[0].Text);

                Mesa mesa = await controller.BuscarPorNumero(numeroMesa);

                if (mesa != null)
                {
                    oFrmCadastroMesa.modoExclusao = true;
                    oFrmCadastroMesa.modoEdicao = false;
                    oFrmCadastroMesa.ConhecaObj(mesa, controller);
                    oFrmCadastroMesa.CarregaTxt();
                    oFrmCadastroMesa.BloquearTxt(); 
                    oFrmCadastroMesa.btnSalvar.Text = "Excluir"; 

                    oFrmCadastroMesa.ShowDialog();

                    oFrmCadastroMesa.btnSalvar.Text = "Salvar";
                    oFrmCadastroMesa.DesbloquearTxt();
                    await CarregarMesas();
                }
                else
                {
                    MessageBox.Show("Mesa não encontrada.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma mesa para excluir.");
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
                    await CarregarMesas();
                }
                else if (int.TryParse(texto, out int numeroMesa))
                {
                    Mesa mesa = await controller.BuscarPorNumero(numeroMesa);

                    if (mesa != null)
                    {
                        ListViewItem item = new ListViewItem(mesa.NumeroMesa.ToString());
                        item.SubItems.Add(mesa.QuantidadeCadeiras.ToString());
                        item.SubItems.Add(mesa.Localizacao);

                        string status;
                        switch (mesa.StatusMesa)
                        {
                            case 'O':
                                status = "OCUPADO";
                                break;
                            case 'R':
                                status = "RESERVADO";
                                break;
                            default:
                                status = "LIVRE";
                                break;
                        }
                        item.SubItems.Add(status);

                        item.SubItems.Add(mesa.Ativo ? "Ativo" : "Inativo");
                        item.SubItems.Add(mesa.DataCadastro?.ToString("g") ?? "-");
                        item.SubItems.Add(mesa.DataAlteracao?.ToString("g") ?? "-");
                        listView1.Items.Add(item);
                    }
                    else
                    {
                        MessageBox.Show("Mesa não encontrada.");
                    }
                }
                else
                {
                    MessageBox.Show("Digite um número de mesa válido.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao pesquisar: " + ex.Message);
            }
        }

        private async void btnSelecionar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int numeroMesa = int.Parse(itemSelecionado.SubItems[0].Text);

                MesaSelecionada = await controller.BuscarPorNumero(numeroMesa);

                if (MesaSelecionada != null)
                {
                    this.DialogResult = DialogResult.OK; 
                    this.Close(); 
                }
                else
                {
                    MessageBox.Show("Erro ao carregar os dados da mesa selecionada.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma mesa.");
            }
        }
    }
}