using Projeto.DAO;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Controller
{
    internal class TransportadoraController
    {
        private DAOTransportadora dao = new DAOTransportadora();

        public void Salvar(Transportadora transportadora)
        {
            dao.Salvar(transportadora);
        }
        public Transportadora BuscarPorId(int id)
        {
            return dao.BuscarPorId(id);
        }
        public List<Transportadora> ListarTransportadora()
        {
            return dao.ListarTransportadoras();
        }
        public void Excluir(int id)
        {
            dao.Excluir(id);
        }
    }
}
