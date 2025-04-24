using Projeto.Controller;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Projeto.Views
{
    public partial class frmCadastroPais : Projeto.frmBase
    {
        public frmCadastroPais()
        {
            InitializeComponent();
            txtCodigo.Enabled = false;
        }

        public void CarregarPais(int id, string nome, bool status)
        {
            txtCodigo.Text = id.ToString();
            txtNome.Text = nome;
            chkInativo.Checked = !status;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = string.IsNullOrEmpty(txtCodigo.Text) ? 0 : int.Parse(txtCodigo.Text);
                string nome = txtNome.Text;
                bool status = !chkInativo.Checked;

                Pais pais = new Pais
                {
                    Id = id,
                    Nome = nome,
                    Status = status,
                };

                PaisController controller = new PaisController();
                controller.Salvar(pais);

                MessageBox.Show("País salvo com sucesso!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }
    }
}
