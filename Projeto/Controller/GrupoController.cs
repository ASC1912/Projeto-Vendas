using Projeto.DAO;
using Projeto.Models;
using Projeto.Services;
using Projeto.Services.Interfaces;
using Projeto.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projeto.Controller
{
    public class GrupoController
    {
        private readonly bool _useApi;
        private readonly IGrupoApiService _apiService;
        private readonly DAOGrupo _dao;

        public GrupoController()
        {
            _useApi = ConfigHelper.UseApi();

            if (_useApi)
            {
                _apiService = new GrupoApiService();
            }
            else
            {
                _dao = new DAOGrupo();
            }
        }

        public Task Salvar(Grupo grupo)
        {
            if (_useApi)
            {
                return _apiService.SaveGrupoAsync(grupo);
            }
            else
            {
                return Task.Run(() => _dao.Salvar(grupo));
            }
        }

        public Task Excluir(int id)
        {
            if (_useApi)
            {
                return _apiService.DeleteGrupoAsync(id);
            }
            else
            {
                return Task.Run(() => _dao.Excluir(id));
            }
        }

        public Task<Grupo> BuscarPorId(int id)
        {
            if (_useApi)
            {
                return _apiService.GetGrupoByIdAsync(id);
            }
            else
            {
                return Task.FromResult(_dao.BuscarPorId(id));
            }
        }

        public Task<List<Grupo>> ListarGrupos()
        {
            if (_useApi)
            {
                return _apiService.GetGruposAsync();
            }
            else
            {
                return Task.FromResult(_dao.ListarGrupos());
            }
        }
    }
}