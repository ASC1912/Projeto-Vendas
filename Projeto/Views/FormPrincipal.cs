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
        Compra aCompra = new Compra();

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
        CompraController CtrlCompra = new CompraController();

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
        }


        private void condiçãoDePagamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aInter.PecaCondicaoPagamento(aCondPgto, CtrlCondicaoPagamento);
        }

        private void paísToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aInter.PecaPaises(oPais, CtrlPais);
        }

        private void estadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aInter.PecaEstados(oEstado, CtrlEstado);
        }

        private void cidadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aInter.PecaCidades(aCidade, CtrlCidade);
        }

        private void clienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aInter.PecaCliente(oCliente, CtrlCliente);
        }

        private void funcionárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aInter.PecaFuncionario(oFunc, CtrlFuncionario);
        }

        private void fornecedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aInter.PecaFornecedor(oForn, CtrlFornecedor);
        }

        private void marcasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aInter.PecaMarca(aMarca, CtrlMarca);
        }

        private void gruposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aInter.PecaGrupo(oGrupo, CtrlGrupo);
        }

        private void produtosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aInter.PecaProduto(oProduto, CtrlProduto);
        }

        private void transportadorasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aInter.PecaTransportadora(oTransportadora, CtrlTransportadora);
        }

        private void veículosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aInter.PecaVeiculo(oVeiculo, CtrlVeiculo);
        }

        private void comprasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //aInter.PecaCompra(aCompra, CtrlCompra);

            frmCadastroCompra formCompra = new frmCadastroCompra();
            formCompra.ShowDialog();
        }
    }
}
