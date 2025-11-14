using Projeto.Controller;
using Projeto.Models;
using Projeto.Utils;
using Projeto.Views.Consultas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto.Views.Cadastros
{
    public partial class frmCadastroVeiculo : Projeto.frmBase
    {
        Veiculo oVeiculo;
        private frmConsultaTransportadora oFrmConsultaTransportadora;
        private frmConsultaMarca oFrmConsultaMarca;

        private VeiculoController controller = new VeiculoController();
        private MarcaController marcaController = new MarcaController(); 
        private int transportadoraSelecionadaId = -1;
        private int marcaSelecionadaId = -1;
        public bool modoEdicao = false;
        public bool modoExclusao = false;

        public frmCadastroVeiculo() : base()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
            txtTransportadora.ReadOnly = true;
            txtMarca.ReadOnly = true;

            this.txtIdMarca.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress_ApenasNumerosInteiros);
            this.txtAnoFabricacao.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress_ApenasNumerosInteiros);

            this.txtCapacidadeCarga.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress_ApenasNumerosEVirgula);
        }

        public override void ConhecaObj(object obj, object ctrl)
        {
            if (obj != null)
            {
                oVeiculo = (Veiculo)obj;
            }
            if (ctrl != null)
            {
                controller = (VeiculoController)ctrl;
            }
        }

        public void setFrmConsultaTransportadora(object obj)
        {
            if (obj != null)
            {
                oFrmConsultaTransportadora = (frmConsultaTransportadora)obj;
            }
        }

        public void setFrmConsultaMarca(object obj)
        {
            if (obj != null)
            {
                oFrmConsultaMarca = (frmConsultaMarca)obj;
            }
        }

        public override void CarregaTxt()
        {
            txtCodigo.Text = oVeiculo.Id.ToString();
            txtPlaca.Text = oVeiculo.Placa;
            txtModelo.Text = oVeiculo.Modelo;
            txtIdMarca.Text = oVeiculo.IdMarca.ToString();
            txtMarca.Text = oVeiculo.NomeMarca;
            marcaSelecionadaId = oVeiculo.IdMarca ?? -1;
            txtAnoFabricacao.Text = oVeiculo.AnoFabricacao.ToString();
            txtCapacidadeCarga.Text = oVeiculo.CapacidadeCargaKg.ToString();
            txtTransportadora.Text = oVeiculo.NomeTransportadora;
            transportadoraSelecionadaId = oVeiculo.TransportadoraId;
            chkInativo.Checked = !oVeiculo.Ativo;
            lblDataCriacao.Text = oVeiculo.DataCadastro.HasValue ? $"Criado em: {oVeiculo.DataCadastro.Value:dd/MM/yyyy HH:mm}" : "Criado em: -";
            lblDataModificacao.Text = oVeiculo.DataAlteracao.HasValue ? $"Modificado em: {oVeiculo.DataAlteracao.Value:dd/MM/yyyy HH:mm}" : "Modificado em: -";
        }

        public override void BloquearTxt()
        {
            txtPlaca.Enabled = false;
            txtModelo.Enabled = false;
            txtIdMarca.Enabled = false;
            txtMarca.Enabled = false;
            btnMarca.Enabled = false;
            txtAnoFabricacao.Enabled = false;
            txtCapacidadeCarga.Enabled = false;
            txtTransportadora.Enabled = false;
            btnTransportadora.Enabled = false;
            chkInativo.Enabled = false;
        }

        public override void DesbloquearTxt()
        {
            txtPlaca.Enabled = true;
            txtModelo.Enabled = true;
            txtIdMarca.Enabled = true;
            txtMarca.Enabled = true;
            btnMarca.Enabled = true;
            txtAnoFabricacao.Enabled = true;
            txtCapacidadeCarga.Enabled = true;
            txtTransportadora.Enabled = true;
            btnTransportadora.Enabled = true;
            chkInativo.Enabled = true;
        }

        public override void LimparTxt()
        {
            txtCodigo.Text = "0";
            txtPlaca.Clear();
            txtModelo.Clear();
            txtIdMarca.Clear();
            txtMarca.Clear();
            txtAnoFabricacao.Clear();
            txtCapacidadeCarga.Clear();
            txtTransportadora.Clear();
            transportadoraSelecionadaId = -1;
            marcaSelecionadaId = -1;
            chkInativo.Checked = false;
            DateTime agora = DateTime.Now;
            lblDataCriacao.Text = $"Criado em: {agora:dd/MM/yyyy HH:mm}";
            lblDataModificacao.Text = $"Modificado em: {agora:dd/MM/yyyy HH:mm}";
        }

        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            if (modoExclusao)
            {
                var confirmacao = MessageBox.Show("Tem certeza que deseja excluir este veículo?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmacao == DialogResult.Yes)
                {
                    try
                    {
                        int id = int.Parse(txtCodigo.Text);
                        await controller.Excluir(id);
                        MessageBox.Show("Veículo excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao excluir: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                if (!Validador.CampoObrigatorio(txtPlaca, "A placa é obrigatória.")) return;
                if (!Validador.ValidarIdSelecionado(transportadoraSelecionadaId, "Selecione uma transportadora.")) return;
                if (!Validador.ValidarIdSelecionado(marcaSelecionadaId, "Selecione uma marca.")) return;

                try
                {
                    int id = string.IsNullOrEmpty(txtCodigo.Text) ? 0 : int.Parse(txtCodigo.Text);

                    var veiculos = await controller.ListarVeiculos();
                    if (Validador.VerificarDuplicidade(veiculos, v => v.Placa.Trim().Equals(txtPlaca.Text.Trim(), StringComparison.OrdinalIgnoreCase) && v.Id != id, "Já existe um veículo cadastrado com esta placa."))
                    {
                        txtPlaca.Focus();
                        return;
                    }

                    Veiculo veiculo = new Veiculo
                    {
                        Id = id,
                        Placa = txtPlaca.Text,
                        Modelo = txtModelo.Text,
                        AnoFabricacao = string.IsNullOrWhiteSpace(txtAnoFabricacao.Text) ? (int?)null : Convert.ToInt32(txtAnoFabricacao.Text),
                        CapacidadeCargaKg = string.IsNullOrWhiteSpace(txtCapacidadeCarga.Text) ? (decimal?)null : Convert.ToDecimal(txtCapacidadeCarga.Text),
                        Ativo = !chkInativo.Checked,
                        TransportadoraId = transportadoraSelecionadaId,
                        IdMarca = marcaSelecionadaId,
                        DataCadastro = id == 0 ? DateTime.Now : oVeiculo.DataCadastro,
                        DataAlteracao = id > 0 ? DateTime.Now : (DateTime?)null
                    };

                    await controller.Salvar(veiculo);
                    MessageBox.Show("Veículo salvo com sucesso!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao salvar veículo: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBuscarTransportadora_Click(object sender, EventArgs e)
        {
            oFrmConsultaTransportadora.ModoSelecao = true;
            if (oFrmConsultaTransportadora.ShowDialog() == DialogResult.OK && oFrmConsultaTransportadora.TransportadoraSelecionada != null)
            {
                txtTransportadora.Text = oFrmConsultaTransportadora.TransportadoraSelecionada.Nome;
                transportadoraSelecionadaId = oFrmConsultaTransportadora.TransportadoraSelecionada.Id;
            }
        }

        private void btnBuscarMarca_Click(object sender, EventArgs e)
        {
            oFrmConsultaMarca.ModoSelecao = true;
            if (oFrmConsultaMarca.ShowDialog() == DialogResult.OK && oFrmConsultaMarca.MarcaSelecionado != null)
            {
                var marca = oFrmConsultaMarca.MarcaSelecionado;
                txtIdMarca.Text = marca.Id.ToString();
                txtMarca.Text = marca.NomeMarca;
                marcaSelecionadaId = marca.Id;
            }
        }

        private async void txtIdMarca_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdMarca.Text))
            {
                txtMarca.Clear();
                marcaSelecionadaId = -1;
                return;
            }
            if (int.TryParse(txtIdMarca.Text, out int id))
            {
                var marca = await marcaController.BuscarPorId(id);
                if (marca != null)
                {
                    txtMarca.Text = marca.NomeMarca;
                    marcaSelecionadaId = marca.Id;
                }
                else
                {
                    MessageBox.Show("Marca não encontrada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMarca.Clear();
                    marcaSelecionadaId = -1;
                }
            }
            else
            {
                MessageBox.Show("ID da Marca inválido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMarca.Clear();
                marcaSelecionadaId = -1;
            }
        }

        private void frmCadastroVeiculo_Load(object sender, EventArgs e)
        {
        }
    }
}