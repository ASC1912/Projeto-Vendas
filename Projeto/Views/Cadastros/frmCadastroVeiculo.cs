using Projeto.Controller;
using Projeto.Models;
using Projeto.Utils;
using Projeto.Views.Consultas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Projeto.Views.Cadastros
{
    public partial class frmCadastroVeiculo : Projeto.frmBase
    {
        private VeiculoController controller = new VeiculoController();
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
        }

        public void CarregarVeiculo(int id, string placa, string modelo, int? anoFabricacao, decimal? capacidadeCarga, bool ativo, int idTransportadora, string nomeTransportadora, int? idMarca, string nomeMarca, DateTime? dataCadastro, DateTime? dataAlteracao)
        {
            modoEdicao = true;

            txtCodigo.Text = id.ToString();
            txtPlaca.Text = placa;
            txtModelo.Text = modelo;
            txtAnoFabricacao.Text = anoFabricacao?.ToString();
            txtCapacidadeCarga.Text = capacidadeCarga?.ToString("F2");
            chkInativo.Checked = !ativo;

            transportadoraSelecionadaId = idTransportadora;
            txtTransportadora.Text = nomeTransportadora;

            marcaSelecionadaId = idMarca ?? -1;
            txtMarca.Text = nomeMarca; 

            lblDataCriacao.Text = dataCadastro.HasValue ? $"Criado em: {dataCadastro.Value:dd/MM/yyyy HH:mm}" : "Criado em: -";
            lblDataModificacao.Text = dataAlteracao.HasValue ? $"Modificado em: {dataAlteracao.Value:dd/MM/yyyy HH:mm}" : "Modificado em: -";
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (modoExclusao)
            {
                var confirmacao = MessageBox.Show("Tem certeza que deseja excluir este veículo?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmacao == DialogResult.Yes)
                {
                    try
                    {
                        int id = int.Parse(txtCodigo.Text);
                        controller.Excluir(id);
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
                if (transportadoraSelecionadaId <= 0)
                {
                    MessageBox.Show("Selecione uma transportadora.");
                    return;
                }
                if (marcaSelecionadaId <= 0)
                {
                    MessageBox.Show("Selecione uma marca.");
                    return;
                }

                try
                {
                    int id = string.IsNullOrEmpty(txtCodigo.Text) ? 0 : int.Parse(txtCodigo.Text);

                    var veiculos = controller.ListarVeiculos();
                    bool placaDuplicada = veiculos.Exists(v => v.Placa.Trim().Equals(txtPlaca.Text.Trim(), StringComparison.OrdinalIgnoreCase) && v.Id != id);

                    if (placaDuplicada)
                    {
                        MessageBox.Show("Já existe um veículo cadastrado com esta placa.", "Placa Duplicada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtPlaca.Focus();
                        return;
                    }

                    DateTime dataCriacao = id == 0 ? DateTime.Now : DateTime.Parse(lblDataCriacao.Text.Replace("Criado em: ", ""));
                    DateTime dataModificacao = DateTime.Now;

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
                        DataCadastro = dataCriacao,
                        DataAlteracao = dataModificacao
                    };

                    controller.Salvar(veiculo);
                    MessageBox.Show("Veículo salvo com sucesso!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao salvar veículo: " + ex.Message);
                }
            }
        }

        private void frmCadastroVeiculo_Load(object sender, EventArgs e)
        {
            if (modoExclusao)
            {
                btnSalvar.Text = "Deletar";
                txtPlaca.Enabled = false;
                txtModelo.Enabled = false;
                txtMarca.Enabled = false;
                btnMarca.Enabled = false;
                txtAnoFabricacao.Enabled = false;
                txtCapacidadeCarga.Enabled = false;
                txtTransportadora.Enabled = false;
                btnTransportadora.Enabled = false;
                chkInativo.Enabled = false;
            }
            else if (modoEdicao == false)
            {
                txtCodigo.Text = "0";
                DateTime agora = DateTime.Now;
                lblDataCriacao.Text = $"Criado em: {agora:dd/MM/yyyy HH:mm}";
                lblDataModificacao.Text = $"Modificado em: {agora:dd/MM/yyyy HH:mm}";
            }
        }
        private void btnBuscarTransportadora_Click(object sender, EventArgs e)
        {
            frmConsultaTransportadora consulta = new frmConsultaTransportadora();
            consulta.ModoSelecao = true;

            if (consulta.ShowDialog() == DialogResult.OK && consulta.TransportadoraSelecionada != null)
            {
                txtTransportadora.Text = consulta.TransportadoraSelecionada.Nome;
                transportadoraSelecionadaId = consulta.TransportadoraSelecionada.Id;
            }
        }

        private void btnBuscarMarca_Click(object sender, EventArgs e)
        {
            frmConsultaMarca consulta = new frmConsultaMarca();
            consulta.ModoSelecao = true;

            if (consulta.ShowDialog() == DialogResult.OK && consulta.MarcaSelecionado != null)
            {
                txtMarca.Text = consulta.MarcaSelecionado.NomeMarca;
                marcaSelecionadaId = consulta.MarcaSelecionado.Id;
            }
        }
    }
}