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

        frmCadastroPais oFrmCadastroPais;
        frmCadastroEstado oFrmCadastroEstado;
        frmCadastroCidade oFrmCadastroCidade;

        public Interfaces()
        {
            oFrmConsultaPais = new frmConsultaPais();
            oFrmConsultaEstado = new frmConsultaEstado();
            oFrmConsultaCidade = new frmConsultaCidade();

            oFrmCadastroPais = new frmCadastroPais();
            oFrmCadastroEstado = new frmCadastroEstado();
            oFrmCadastroCidade = new frmCadastroCidade();

            oFrmConsultaPais.setFrmCadastro(oFrmCadastroPais);
            oFrmConsultaEstado.setFrmCadastro(oFrmCadastroEstado);
            oFrmConsultaCidade.setFrmCadastro(oFrmCadastroCidade);
        }

        public void PecaPaises(object obj, object ctrl)
        {
            //oFrmConsultaPais.ConhecaObj(obj, ctrl);
            oFrmConsultaPais.ShowDialog();
        }

        public void PecaEstados(object obj, object ctrl)
        {
            oFrmConsultaEstado.ShowDialog();
        }

        public void PecaCidades(object obj, object ctrl)
        {
            oFrmConsultaCidade.ShowDialog();
        }
    }
}
