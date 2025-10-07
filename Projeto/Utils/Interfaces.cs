using Projeto.Views;
using Projeto.Views.Cadastros;
using Projeto.Views.Consultas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Utils
{
    internal class Interfaces
    {
        frmConsultaPais oFrmConsultaPais;
        frmConsultaEstado oFrmConsultaEstado;
        frmConsultaCidade oFrmConsultaCidade;
        frmConsultaFrmPgto oFrmConsultaFrmPgto;
        frmConsultaCondPgto oFrmConsultaCondPgto;
        frmConsultaCliente oFrmConsultaCliente;
        frmConsultaFuncionario oFrmConsultaFuncionario;
        frmConsultaFornecedor oFrmConsultaFornecedor;
        frmConsultaTransportadora oFrmConsultaTransportadora;
        frmConsultaMarca oFrmConsultaMarca;
        frmConsultaGrupo oFrmConsultaGrupo;
        frmConsultaProduto oFrmConsultaProduto;
        frmConsultaVeiculo oFrmConsultaVeiculo;
        frmConsultaCompra oFrmConsultaCompra;
        frmConsultaPedido oFrmConsultaPedido;
        frmConsultaMesa oFrmConsultaMesa;
        frmConsultaUnidadeMedida oFrmConsultaUnidadeMedida;

        frmCadastroPais oFrmCadastroPais;
        frmCadastroEstado oFrmCadastroEstado;
        frmCadastroCidade oFrmCadastroCidade;
        frmCadastroFrmPgto oFrmCadastroFrmPgto;
        frmCadastroCondPgto oFrmCadastroCondPgto;
        frmCadastroCliente oFrmCadastroCliente;
        frmCadastroFuncionario oFrmCadastroFuncionario;
        frmCadastroFornecedor oFrmCadastroFornecedor;
        frmCadastroTransportadora oFrmCadastroTransportadora;
        frmCadastroMarca oFrmCadastroMarca;
        frmCadastroGrupo oFrmCadastroGrupo;
        frmCadastroProduto oFrmCadastroProduto;
        frmCadastroVeiculo oFrmCadastroVeiculo;
        frmCadastroCompra oFrmCadastroCompra;
        frmCadastroPedido oFrmCadastroPedido;
        frmCadastroMesa oFrmCadastroMesa;
        frmCadastroUnidadeMedida oFrmCadastroUnidadeMedida;


        public Interfaces()
        {
            oFrmConsultaPais = new frmConsultaPais();
            oFrmConsultaEstado = new frmConsultaEstado();
            oFrmConsultaCidade = new frmConsultaCidade();
            oFrmConsultaFrmPgto = new frmConsultaFrmPgto();
            oFrmConsultaCondPgto = new frmConsultaCondPgto();
            oFrmConsultaCliente = new frmConsultaCliente();
            oFrmConsultaFuncionario = new frmConsultaFuncionario();
            oFrmConsultaFornecedor = new frmConsultaFornecedor();
            oFrmConsultaTransportadora = new frmConsultaTransportadora();
            oFrmConsultaMarca = new frmConsultaMarca();
            oFrmConsultaGrupo = new frmConsultaGrupo();
            oFrmConsultaProduto = new frmConsultaProduto();
            oFrmConsultaVeiculo = new frmConsultaVeiculo();
            oFrmConsultaCompra = new frmConsultaCompra();
            oFrmConsultaMesa = new frmConsultaMesa();
            oFrmConsultaPedido = new frmConsultaPedido();
            oFrmConsultaUnidadeMedida = new frmConsultaUnidadeMedida();

            oFrmCadastroPais = new frmCadastroPais();
            oFrmCadastroEstado = new frmCadastroEstado();
            oFrmCadastroCidade = new frmCadastroCidade();
            oFrmCadastroFrmPgto = new frmCadastroFrmPgto();
            oFrmCadastroCondPgto = new frmCadastroCondPgto();
            oFrmCadastroCliente = new frmCadastroCliente();
            oFrmCadastroFuncionario = new frmCadastroFuncionario();
            oFrmCadastroFornecedor = new frmCadastroFornecedor();
            oFrmCadastroTransportadora = new frmCadastroTransportadora();
            oFrmCadastroMarca = new frmCadastroMarca();
            oFrmCadastroGrupo = new frmCadastroGrupo();
            oFrmCadastroProduto = new frmCadastroProduto();
            oFrmCadastroVeiculo = new frmCadastroVeiculo();
            oFrmCadastroCompra = new frmCadastroCompra();
            oFrmCadastroMesa = new frmCadastroMesa();
            oFrmCadastroPedido = new frmCadastroPedido();
            oFrmCadastroUnidadeMedida = new frmCadastroUnidadeMedida();

            oFrmConsultaPais.setFrmCadastro(oFrmCadastroPais);
            oFrmConsultaEstado.setFrmCadastro(oFrmCadastroEstado);
            oFrmConsultaCidade.setFrmCadastro(oFrmCadastroCidade);
            oFrmConsultaFrmPgto.setFrmCadastro(oFrmCadastroFrmPgto);
            oFrmConsultaCondPgto.setFrmCadastro(oFrmCadastroCondPgto);
            oFrmConsultaCliente.setFrmCadastro(oFrmCadastroCliente);
            oFrmConsultaFuncionario.setFrmCadastro(oFrmCadastroFuncionario);
            oFrmConsultaFornecedor.setFrmCadastro(oFrmCadastroFornecedor);
            oFrmConsultaTransportadora.setFrmCadastro(oFrmCadastroTransportadora);
            oFrmConsultaMarca.setFrmCadastro(oFrmCadastroMarca);
            oFrmConsultaGrupo.setFrmCadastro(oFrmCadastroGrupo);
            oFrmConsultaProduto.setFrmCadastro(oFrmCadastroProduto);
            oFrmConsultaVeiculo.setFrmCadastro(oFrmCadastroVeiculo);
            oFrmConsultaCompra.setFrmCadastro(oFrmCadastroCompra);
            oFrmConsultaMesa.setFrmCadastro(oFrmCadastroMesa);      
            oFrmConsultaPedido.setFrmCadastro(oFrmCadastroPedido);
            oFrmConsultaUnidadeMedida.setFrmCadastro(oFrmCadastroUnidadeMedida);

            oFrmCadastroEstado.setFrmConsultaPais(oFrmConsultaPais);
            oFrmCadastroCidade.setFrmConsultaEstado(oFrmConsultaEstado);
            oFrmCadastroCondPgto.setFrmConsultaFormaPagamento(oFrmConsultaFrmPgto);
            oFrmCadastroCliente.setFrmConsultaCidade(oFrmConsultaCidade);
            oFrmCadastroCliente.setFrmConsultaCondPgto(oFrmConsultaCondPgto);
            oFrmCadastroFuncionario.setFrmConsultaCidade(oFrmConsultaCidade);
            oFrmCadastroFornecedor.setFrmConsultaCidade(oFrmConsultaCidade);
            oFrmCadastroFornecedor.setFrmConsultaCondPgto(oFrmConsultaCondPgto);
            oFrmCadastroTransportadora.setFrmConsultaCidade(oFrmConsultaCidade);
            oFrmCadastroTransportadora.setFrmConsultaCondPgto(oFrmConsultaCondPgto);
            oFrmCadastroProduto.setFrmConsultaMarca(oFrmConsultaMarca);
            oFrmCadastroProduto.setFrmConsultaGrupo(oFrmConsultaGrupo);
            oFrmCadastroVeiculo.setFrmConsultaMarca(oFrmConsultaMarca);
            oFrmCadastroVeiculo.setFrmConsultaTransportadora(oFrmConsultaTransportadora);
            oFrmCadastroCompra.setFrmConsultaFornecedor(oFrmConsultaFornecedor);
            oFrmCadastroCompra.setFrmConsultaProduto(oFrmConsultaProduto);
            oFrmCadastroPedido.setFrmConsultaFuncionario(oFrmConsultaFuncionario);
            oFrmCadastroPedido.setFrmConsultaMesa(oFrmConsultaMesa);
            oFrmCadastroPedido.setFrmConsultaProduto(oFrmConsultaProduto);
            oFrmCadastroCompra.setFrmConsultaCondPgto(oFrmConsultaCondPgto);
            oFrmCadastroProduto.setFrmConsultaUnidadeMedida(oFrmConsultaUnidadeMedida);
            oFrmCadastroProduto.setFrmConsultaFornecedor(oFrmConsultaFornecedor);

        }

        public void PecaPaises(object obj, object ctrl)
        {
            oFrmConsultaPais.ConhecaObj(obj, ctrl);
            oFrmConsultaPais.ShowDialog();
        }

        public void PecaEstados(object obj, object ctrl)
        {
            oFrmConsultaEstado.ConhecaObj(obj, ctrl);
            oFrmConsultaEstado.ShowDialog();
        }

        public void PecaCidades(object obj, object ctrl)
        {
            oFrmConsultaCidade.ConhecaObj(obj, ctrl);
            oFrmConsultaCidade.ShowDialog();
        }

        public void PecaFormaPagamento(object obj, object ctrl)
        {
            oFrmConsultaFrmPgto.ConhecaObj(obj, ctrl);
            oFrmConsultaFrmPgto.ShowDialog();
        }

        public void PecaCondicaoPagamento(object obj, object ctrl)
        {
            oFrmConsultaCondPgto.ConhecaObj(obj, ctrl);
            oFrmConsultaCondPgto.ShowDialog();
        }

        public void PecaCliente(object obj, object ctrl)
        {
            oFrmConsultaCliente.ConhecaObj(obj, ctrl);
            oFrmConsultaCliente.ShowDialog();
        }

        public void PecaFuncionario(object obj, object ctrl)
        {
            oFrmConsultaFuncionario.ConhecaObj(obj, ctrl);
            oFrmConsultaFuncionario.ShowDialog();
        }

        public void PecaFornecedor(object obj, object ctrl)
        {
            oFrmConsultaFornecedor.ConhecaObj(obj, ctrl);
            oFrmConsultaFornecedor.ShowDialog();
        }

        public void PecaTransportadora(object obj, object ctrl)
        {
            oFrmConsultaTransportadora.ConhecaObj(obj, ctrl);
            oFrmConsultaTransportadora.ShowDialog();
        }

        public void PecaMarca(object obj, object ctrl)
        {
            oFrmConsultaMarca.ConhecaObj(obj, ctrl);
            oFrmConsultaMarca.ShowDialog();
        }

        public void PecaGrupo(object obj, object ctrl)
        {
            oFrmConsultaGrupo.ConhecaObj(obj, ctrl);
            oFrmConsultaGrupo.ShowDialog();
        }

        public void PecaProduto(object obj, object ctrl)
        {
            oFrmConsultaProduto.ConhecaObj(obj, ctrl);
            oFrmConsultaProduto.ShowDialog();
        }

        public void PecaVeiculo(object obj, object ctrl)
        {
            oFrmConsultaVeiculo.ConhecaObj(obj, ctrl);
            oFrmConsultaVeiculo.ShowDialog();
        }

        public void PecaCompra(object obj, object ctrl)
        {
            oFrmConsultaCompra.ConhecaObj(obj, ctrl);
            oFrmConsultaCompra.ShowDialog();
        }

        public void PecaMesa(object obj, object ctrl)
        {
            oFrmConsultaMesa.ConhecaObj(obj, ctrl);
            oFrmConsultaMesa.ShowDialog();
        }

        public void PecaPedido(object obj, object ctrl)
        {
            oFrmConsultaPedido.ConhecaObj(obj, ctrl);
            oFrmConsultaPedido.ShowDialog();
        }

        public void PecaUnidadeMedida(object obj, object ctrl)
        {
            oFrmConsultaUnidadeMedida.ConhecaObj(obj, ctrl);
            oFrmConsultaUnidadeMedida.ShowDialog();
        }
    }
}