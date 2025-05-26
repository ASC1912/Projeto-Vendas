using Projeto.DAO;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Controller
{
    internal class EstadoController
    {
        private DAOEstado dao = new DAOEstado();

        public void Salvar(Estado estado)
        {
            dao.Salvar(estado);
        }
        public Estado BuscarPorId(int id)
        {
            return dao.BuscarPorId(id);
        }
        public List<Estado> ListarEstado()
        {
            return dao.ListarEstado();
        }

        public void Excluir(int id)
        {
            dao.Excluir(id);
        }
    }
}
