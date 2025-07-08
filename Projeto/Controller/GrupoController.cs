using Projeto.DAO;
using Projeto.Models;
using Projeto.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projeto.Controller
{
    internal class GrupoController
    {
        private readonly IGrupoService _grupoService;

        public GrupoController()
        {

            _grupoService = new DAOGrupo();
        }

        public Task Salvar(Grupo grupo)
        {
            return _grupoService.Salvar(grupo);
        }

        public Task<Grupo> BuscarPorId(int id)
        {
            return _grupoService.BuscarPorId(id);
        }

        public Task<List<Grupo>> ListarGrupos()
        {
            return _grupoService.ListarGrupos();
        }

        public Task Excluir(int id)
        {
            return _grupoService.Excluir(id);
        }
    }
}