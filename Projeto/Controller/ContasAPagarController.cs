using Projeto.DAO;
using Projeto.Models;
using System.Threading.Tasks;

namespace Projeto.Controller
{
    internal class ContasAPagarController
    {
        private DAOContasAPagar _dao;

        public ContasAPagarController()
        {
            _dao = new DAOContasAPagar();
        }

        public Task SalvarManual(ContasAPagar conta)
        {
            //método diferente do Salvar(Compra)
            return Task.Run(() => _dao.SalvarManual(conta));
        }

        // (Futuramente teremos Listar, BuscarPorId, Pagar, etc.)
    }
}