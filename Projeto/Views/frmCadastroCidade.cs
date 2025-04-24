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
    public partial class frmCadastroCidade : Projeto.frmBase
    {
        private EstadoController estadoController = new EstadoController();
        private CidadeController controller = new CidadeController();
        public frmCadastroCidade()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
        }

        public void CarregarCidade(int id, string nome, string nomeEstado, bool status)
        {
            txtCodigo.Text = id.ToString();
            txtNome.Text = nome;
            cbEstado.Text = nomeEstado;
            chkInativo.Checked = !status;
        }

        private void CarregarEstados()
        {
            try
            {
                List<Estado> listaEstados = estadoController.ListarEstado();

                cbEstado.DisplayMember = "Nome";
                cbEstado.ValueMember = "Id";
                cbEstado.DataSource = listaEstados;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar Estados: " + ex.Message);
            }
        }

        private void frmCadastroCidade_Load(object sender, EventArgs e)
        {
            CarregarEstados();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (cbEstado.SelectedValue == null)
            {
                MessageBox.Show("Selecione um estado!");
                return;
            }

            Cidade cidade = new Cidade
            {
                Id = string.IsNullOrWhiteSpace(txtCodigo.Text) ? 0 : Convert.ToInt32(txtCodigo.Text), 
                Nome = txtNome.Text,
                IdEstado = Convert.ToInt32(cbEstado.SelectedValue),
                Status = !chkInativo.Checked,
            };

            try
            {
                controller.Salvar(cidade);
                MessageBox.Show("Cidade salva com sucesso!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar cidade: " + ex.Message);
            }
        }
    }
}
