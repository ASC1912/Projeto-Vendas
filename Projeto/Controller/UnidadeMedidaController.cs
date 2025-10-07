using Projeto.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Models;

namespace Projeto.Controller
{
    public class UnidadeMedidaController
    {
        private readonly DAOUnidade _dao;

        public UnidadeMedidaController()
        {
            _dao = new DAOUnidade();
        }

        public Task Salvar(UnidadeMedida unidade)
        {
            return Task.Run(() => _dao.Salvar(unidade));   
        }

        public Task<UnidadeMedida> BuscarPorId(int id)
        {
            return Task.FromResult(_dao.BuscarPorId(id));
        }

        public Task<List<UnidadeMedida>> ListarUnidades()
        {
            return Task.FromResult(_dao.ListarUnidades());
        }

        public Task Excluir(int id)
        {
            return Task.Run(() => _dao.Excluir(id));
        }
    }
}
