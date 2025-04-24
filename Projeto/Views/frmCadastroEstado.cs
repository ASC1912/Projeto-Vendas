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
    public partial class frmCadastroEstado : Projeto.frmBase
    {
        private PaisController paisController = new PaisController();
        private EstadoController controller = new EstadoController();
        private int paisSelecionadoId = -1;

        public frmCadastroEstado()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
        }

        public void CarregarEstado(int id, string nome, string nomePais, bool status)
        {
            txtCodigo.Text = id.ToString();
            txtNome.Text = nome;
            cbPais.Text = nomePais;
            chkInativo.Checked = !status;
        }


        private void CarregarPaises()
        {
            try
            {
                List<Pais> listaPaises = paisController.ListarPais();

                cbPais.DisplayMember = "Nome";
                cbPais.ValueMember = "Id";
                cbPais.DataSource = listaPaises;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar países: " + ex.Message);
            }
        }
        private void frmCadastroEstado_Load(object sender, EventArgs e)
        {
            CarregarPaises();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (cbPais.SelectedValue == null)
            {
                MessageBox.Show("Selecione um país!");
                return;
            }

            Estado estado = new Estado
            {
                Id = string.IsNullOrWhiteSpace(txtCodigo.Text) ? 0 : Convert.ToInt32(txtCodigo.Text), 
                Nome = txtNome.Text,
                IdPais = Convert.ToInt32(cbPais.SelectedValue),
                Status = !chkInativo.Checked,
            };

            try
            {
                controller.Salvar(estado);
                MessageBox.Show("Estado salvo com sucesso!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar estado: " + ex.Message);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            frmConsultaPais consultaPais = new frmConsultaPais();
            consultaPais.ModoSelecao = true;

            var resultado = consultaPais.ShowDialog();

            if (resultado == DialogResult.OK && consultaPais.PaisSelecionado != null)
            {
                cbPais.Text = consultaPais.PaisSelecionado.Nome;
                paisSelecionadoId = consultaPais.PaisSelecionado.Id;
            }
        }
    }
}
