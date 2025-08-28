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

        public Interfaces()
        {
            oFrmConsultaPais = new frmConsultaPais();
            oFrmConsultaEstado = new frmConsultaEstado();
            oFrmConsultaCidade = new frmConsultaCidade();
        }

        public void PecaPaises(object obj, object ctrl)
        {
            //oFrmConsultaPais.ConhecaObj(obj, ctrl);
            oFrmConsultaPais.ShowDialog();
        }
    }
}
