using Projeto.DAO;
using Projeto.Models;
using System.Collections.Generic; // Adicionado
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
            return Task.Run(() => _dao.SalvarManual(conta));
        }


        public Task<List<ContasAPagar>> Listar(string status, string busca)
        {
            return Task.Run(() => _dao.Listar(status, busca));
        }

        public Task Pagar(ContasAPagar conta)
        {
            return Task.Run(() => _dao.Pagar(conta));
        }


        public Task Estornar(ContasAPagar conta)
        {
            return Task.Run(() => _dao.Estornar(conta));
        }
    }
}