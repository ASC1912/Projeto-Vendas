using MySql.Data.MySqlClient;
using Projeto.Controller;
using Projeto.Models;
using Projeto.Utils;
using Projeto.Views;
using Projeto.Views.Cadastros;
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
        Interfaces aInter = new Interfaces();
        Pais oPais = new Pais();
        PaisController CtrlPais = new PaisController();

        Pais oPais1 = new Pais();
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
            aInter.PecaPaises(oPais, CtrlPais);

            //frmConsultaPais formConsultaPais = new frmConsultaPais();
            //formConsultaPais.ShowDialog();
        }

        private void estadoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            aInter.PecaEstados(oPais, oPais1);

            //frmConsultaEstado formConsultaEstado = new frmConsultaEstado();
            //formConsultaEstado.ShowDialog();
        }

        private void cidadeToolStripMenuItem_Click(object sender, EventArgs e)
        {

            aInter.PecaCidades(oPais, oPais1);

            //frmConsultaCidade formConsultaCidade = new frmConsultaCidade();
            //formConsultaCidade.ShowDialog();
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
            frmConsultaTransportadora formConsultaTransportadora = new frmConsultaTransportadora();
            formConsultaTransportadora.ShowDialog();
        }

        private void veículosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultaVeiculo formConsultaVeiculo = new frmConsultaVeiculo();
            formConsultaVeiculo.ShowDialog();
        }

        private void comprasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCadastroCompras2 formCompra = new frmCadastroCompras2();
            formCompra.ShowDialog();
        }
    }
}
