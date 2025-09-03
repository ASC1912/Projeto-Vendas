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
        Estado oEstado = new Estado();
        Cidade aCidade = new Cidade();
        FormaPagamento aFrmPgot = new FormaPagamento();
        CondicaoPagamento aCondPgto = new CondicaoPagamento();
        Cliente oCliente = new Cliente();
        Funcionario oFunc = new Funcionario();
        Fornecedor oForn = new Fornecedor();
        Transportadora oTransportadora = new Transportadora();
        Marca aMarca = new Marca();
        Grupo oGrupo = new Grupo();
        Produto oProduto = new Produto();
        Veiculo oVeiculo = new Veiculo();

        PaisController CtrlPais = new PaisController();
        EstadoController CtrlEstado = new EstadoController();
        CidadeController CtrlCidade = new CidadeController();
        FormaPagamentoController CtrlFormaPagamento = new FormaPagamentoController();
        CondicaoPagamentoController CtrlCondicaoPagamento = new CondicaoPagamentoController();
        ClienteController CtrlCliente = new ClienteController();
        FuncionarioController CtrlFuncionario = new FuncionarioController();
        FornecedorController CtrlFornecedor = new FornecedorController();
        TransportadoraController CtrlTransportadora = new TransportadoraController();
        MarcaController CtrlMarca = new MarcaController();
        GrupoController CtrlGrupo = new GrupoController();
        ProdutoController CtrlProduto = new ProdutoController();
        VeiculoController CtrlVeiculo = new VeiculoController();

        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

     

        private void formaDePagamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aInter.PecaFormaPagamento(aFrmPgot, CtrlFormaPagamento);

            //frmCadastroFrmPgto formCadastroPagamento = new frmCadastroFrmPgto();
            //formCadastroPagamento.ShowDialog();
        }


        private void condiçãoDePagamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aInter.PecaCondicaoPagamento(aCondPgto, CtrlCondicaoPagamento);

            //frmCadastroCondPgto formCadastroCondPgto = new frmCadastroCondPgto();
            //formCadastroCondPgto.ShowDialog();
        }

        private void paísToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aInter.PecaPaises(oPais, CtrlPais);

            //frmConsultaPais formConsultaPais = new frmConsultaPais();
            //formConsultaPais.ShowDialog();
        }

        private void estadoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            aInter.PecaEstados(oEstado, CtrlEstado);

            //frmConsultaEstado formConsultaEstado = new frmConsultaEstado();
            //formConsultaEstado.ShowDialog();
        }

        private void cidadeToolStripMenuItem_Click(object sender, EventArgs e)
        {

            aInter.PecaCidades(aCidade, CtrlCidade);

            //frmConsultaCidade formConsultaCidade = new frmConsultaCidade();
            //formConsultaCidade.ShowDialog();
        }

        private void clienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aInter.PecaCliente(oCliente, CtrlCliente);

            //frmConsultaCliente formConsultaCliente = new frmConsultaCliente();
            //formConsultaCliente.ShowDialog();
        }

        private void funcionárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aInter.PecaFuncionario(oFunc, CtrlFuncionario);

            /*
            frmConsultaFuncionario formConsultaFuncionario = new frmConsultaFuncionario();
            formConsultaFuncionario.ShowDialog();
            */
        }

        private void fornecedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aInter.PecaFornecedor(oForn, CtrlFornecedor);

            /*
            frmConsultaFornecedor formConsultaFornecedor = new frmConsultaFornecedor();
            formConsultaFornecedor.ShowDialog();
            */
        }

        private void marcasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aInter.PecaMarca(aMarca, CtrlMarca);

            /*
            frmConsultaMarca formConsultaMarca = new frmConsultaMarca();
            formConsultaMarca.ShowDialog();
            */
        }

        private void gruposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aInter.PecaGrupo(oGrupo, CtrlGrupo);

            /*
            frmConsultaGrupo formConsultaGrupo = new frmConsultaGrupo();
            formConsultaGrupo.ShowDialog();
            */
        }

        private void produtosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aInter.PecaProduto(oProduto, CtrlProduto);

            /*
            frmConsultaProduto formConstulaProduto = new frmConsultaProduto();
            formConstulaProduto.ShowDialog();
            */
        }

        private void transportadorasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aInter.PecaTransportadora(oTransportadora, CtrlTransportadora);

            /*
            frmConsultaTransportadora formConsultaTransportadora = new frmConsultaTransportadora();
            formConsultaTransportadora.ShowDialog();
            */
        }

        private void veículosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aInter.PecaVeiculo(oVeiculo, CtrlVeiculo);

            /*
            frmConsultaVeiculo formConsultaVeiculo = new frmConsultaVeiculo();
            formConsultaVeiculo.ShowDialog();
            */
        }

        private void comprasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCadastroCompras2 formCompra = new frmCadastroCompras2();
            formCompra.ShowDialog();
        }
    }
}
