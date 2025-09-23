using Projeto.DAO;
using Projeto.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projeto.Controller
{
    internal class MesaController
    {
        private DAOMesa dao = new DAOMesa();

        public Task Salvar(Mesa mesa)
        {
            return Task.Run(() => dao.Salvar(mesa));
        }

        public Task<Mesa> BuscarPorNumero(int numeroMesa)
        {
            return Task.FromResult(dao.BuscarPorNumero(numeroMesa));
        }

        public Task<List<Mesa>> ListarMesas()
        {
            return Task.FromResult(dao.ListarMesas());
        }

        public Task Excluir(int numeroMesa)
        {
            return Task.Run(() => dao.Excluir(numeroMesa));
        }
    }
}