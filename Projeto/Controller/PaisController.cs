using Projeto.DAO;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Controller
{
    internal class PaisController
    {
        private DAOPais dao = new DAOPais();

        public void Salvar(Pais pais)
        {
            dao.Salvar(pais);
        }
        public Pais BuscarPorId(int id)
        {
            return dao.BuscarPorId(id);
        }

        public List<Pais> ListarPais()
        {
            return dao.ListarPais();
        }

        public void Excluir(int id)
        {
            dao.Excluir(id);
        }
    }
}
