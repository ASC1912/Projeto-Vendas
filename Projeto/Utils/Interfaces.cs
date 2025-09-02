using Projeto.Views;
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

        frmCadastroPais oFrmCadastroPais;
        frmCadastroEstado oFrmCadastroEstado;
        frmCadastroCidade oFrmCadastroCidade;
        frmCadastroFrmPgto oFrmCadastroFrmPgto;
        frmCadastroCondPgto oFrmCadastroCondPgto;
        frmCadastroCliente oFrmCadastroCliente;
        frmCadastroFuncionario oFrmCadastroFuncionario;
        frmCadastroFornecedor oFrmCadastroFornecedor;


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

            oFrmCadastroPais = new frmCadastroPais();
            oFrmCadastroEstado = new frmCadastroEstado();
            oFrmCadastroCidade = new frmCadastroCidade();
            oFrmCadastroFrmPgto = new frmCadastroFrmPgto();
            oFrmCadastroCondPgto = new frmCadastroCondPgto();
            oFrmCadastroCliente = new frmCadastroCliente();
            oFrmCadastroFuncionario = new frmCadastroFuncionario();
            oFrmCadastroFornecedor = new frmCadastroFornecedor();

            oFrmConsultaPais.setFrmCadastro(oFrmCadastroPais);
            oFrmConsultaEstado.setFrmCadastro(oFrmCadastroEstado);
            oFrmConsultaCidade.setFrmCadastro(oFrmCadastroCidade);
            oFrmConsultaFrmPgto.setFrmCadastro(oFrmCadastroFrmPgto);
            oFrmConsultaCondPgto.setFrmCadastro(oFrmCadastroCondPgto);
            oFrmConsultaCliente.setFrmCadastro(oFrmCadastroCliente);
            oFrmConsultaFuncionario.setFrmCadastro(oFrmCadastroFuncionario);
            oFrmConsultaFornecedor.setFrmCadastro(oFrmCadastroFornecedor);

            oFrmCadastroEstado.setFrmConsultaPais(oFrmConsultaPais);
            oFrmCadastroCidade.setFrmConsultaEstado(oFrmConsultaEstado);
            oFrmCadastroCondPgto.setFrmConsultaFormaPagamento(oFrmConsultaFrmPgto);
            oFrmCadastroCliente.setFrmConsultaCidade(oFrmConsultaCidade);
            oFrmCadastroCliente.setFrmConsultaCondPgto(oFrmConsultaCondPgto);
            oFrmCadastroFuncionario.setFrmConsultaCidade(oFrmConsultaCidade);
            oFrmCadastroFornecedor.setFrmConsultaCidade(oFrmConsultaCidade);
            oFrmCadastroFornecedor.setFrmConsultaCondPgto(oFrmConsultaCondPgto);

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
    }
}