using Projeto.Controller;
using Projeto.Models;
using Projeto.Views.Cadastros;
using System;
using System.Collections.Generic;
using System.Threading.Tasks; // Adicionado
using System.Windows.Forms;

namespace Projeto.Views.Consultas
{
    public partial class frmConsultaTransportadora : Projeto.frmBaseConsulta
    {
        private TransportadoraController controller = new TransportadoraController();
        private CidadeController cidadeController = new CidadeController(); 
        internal Transportadora TransportadoraSelecionada { get; private set; }

        private frmCadastroTransportadora oFrmCadastroTransportadora;


        public frmConsultaTransportadora() : base()
        {
            InitializeComponent();
        }

        public override void setFrmCadastro(object obj)
        {
            if (obj != null)
            {
                oFrmCadastroTransportadora = (frmCadastroTransportadora)obj;
            }
        }

        public override void ConhecaObj(object obj, object ctrl)
        {
            if (ctrl != null)
            {
                controller = (TransportadoraController)ctrl;
            }
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            oFrmCadastroTransportadora.modoEdicao = false;
            oFrmCadastroTransportadora.modoExclusao = false;
            oFrmCadastroTransportadora.LimparTxt();
            oFrmCadastroTransportadora.ShowDialog();
            CarregarTransportadoras();
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

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);

                Transportadora transportadora = controller.BuscarPorId(id); 

                if (transportadora != null)
                {
                    Cidade cidade = null;
                    if (transportadora.CidadeId.HasValue)
                    {
                        cidade = await cidadeController.BuscarPorId(transportadora.CidadeId.Value);
                    }

                    oFrmCadastroTransportadora.modoEdicao = true;
                    oFrmCadastroTransportadora.modoExclusao = false;
                    oFrmCadastroTransportadora.ConhecaObj(transportadora, controller);
                    oFrmCadastroTransportadora.LimparTxt();
                    oFrmCadastroTransportadora.CarregaTxt();
                    oFrmCadastroTransportadora.ShowDialog();
                    CarregarTransportadoras();
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

        private async void btnDeletar_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var itemSelecionado = listView1.SelectedItems[0];
                int id = int.Parse(itemSelecionado.SubItems[0].Text);

                Transportadora transportadora = controller.BuscarPorId(id); 

                if (transportadora != null)
                {
                    Cidade cidade = null;
                    if (transportadora.CidadeId.HasValue)
                    {
                        cidade = await cidadeController.BuscarPorId(transportadora.CidadeId.Value);
                    }

                    oFrmCadastroTransportadora.modoEdicao = false;
                    oFrmCadastroTransportadora.modoExclusao = true;
                    oFrmCadastroTransportadora.ConhecaObj(transportadora, controller);
                    oFrmCadastroTransportadora.LimparTxt();
                    oFrmCadastroTransportadora.CarregaTxt();
                    oFrmCadastroTransportadora.BloquearTxt();
                    oFrmCadastroTransportadora.btnSalvar.Text = "Excluir";
                    oFrmCadastroTransportadora.ShowDialog();
                    oFrmCadastroTransportadora.btnSalvar.Text = "Salvar";
                    oFrmCadastroTransportadora.DesbloquearTxt();
                    CarregarTransportadoras();
                }
                else
                {
                    MessageBox.Show("Transportadora não encontrada.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma transportadora para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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