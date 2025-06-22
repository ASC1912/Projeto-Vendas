using MySql.Data.MySqlClient;
using Projeto.Controller;
using Projeto.Models;
using Projeto.Views;
using Projeto.Views.Consultas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

     

        private void formaDePagamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCadastroFrmPgto formCadastroPagamento = new frmCadastroFrmPgto();
            formCadastroPagamento.ShowDialog();
        }


        private void condiçãoDePagamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCadastroCondPgto formCadastroCondPgto = new frmCadastroCondPgto();
            formCadastroCondPgto.ShowDialog();
        }

        private void formaDePagamentoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmConsultaFrmPgto formConsultaFrmPgto = new frmConsultaFrmPgto();
            formConsultaFrmPgto.ShowDialog();
        }

        private void condiçãoDePagamentoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmConsultaCondPgto formConsultaCondPgto = new frmConsultaCondPgto();
            formConsultaCondPgto.ShowDialog();
        }

        private void paísToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultaPais formConsultaPais = new frmConsultaPais();
            formConsultaPais.ShowDialog();
        }

        private void estadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultaEstado formConsultaEstado = new frmConsultaEstado();
            formConsultaEstado.ShowDialog();
        }

        private void cidadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultaCidade formConsultaCidade = new frmConsultaCidade();
            formConsultaCidade.ShowDialog();
        }

        private void clienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultaCliente formConsultaCliente = new frmConsultaCliente();
            formConsultaCliente.ShowDialog();
        }

        private void funcionárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultaFuncionario formConsultaFuncionario = new frmConsultaFuncionario();
            formConsultaFuncionario.ShowDialog();
        }

        private void fornecedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultaFornecedor formConsultaFornecedor = new frmConsultaFornecedor();
            formConsultaFornecedor.ShowDialog();
        }

        private void marcasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultaMarca formConsultaMarca = new frmConsultaMarca();
            formConsultaMarca.ShowDialog();
        }

        private void gruposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultaGrupo formConsultaGrupo = new frmConsultaGrupo();
            formConsultaGrupo.ShowDialog();
        }

        private void produtosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultaProduto formConstulaProduto = new frmConsultaProduto();
            formConstulaProduto.ShowDialog();
        }

        private void transportadorasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultaTransportadora formConlsultaTransportadora = new frmConsultaTransportadora();
            formConlsultaTransportadora.ShowDialog();
        }
    }
}
