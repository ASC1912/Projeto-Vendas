using Projeto.DAO;
using Projeto.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projeto.Controller
{
    internal class PedidoController
    {
        private DAOPedido dao = new DAOPedido();

        public Task Salvar(Pedido pedido)
        {
            return Task.Run(() => dao.Salvar(pedido));
        }

        public Task<Pedido> BuscarPorId(int id)
        {
            return Task.FromResult(dao.BuscarPorId(id));
        }

        public Task<List<Pedido>> ListarPedidos()
        {
            return Task.FromResult(dao.ListarPedidos());
        }

        public Task Excluir(int id)
        {
            return Task.Run(() => dao.Excluir(id));
        }
    }
}