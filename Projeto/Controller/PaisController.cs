using Projeto.DAO;
using Projeto.Models;
using Projeto.Services;
using Projeto.Utils; // Adicione este using
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projeto.Controller
{
    internal class PaisController
    {
        private readonly bool _useApi;
        private readonly ApiClient _apiClient;
        private readonly DAOPais _dao;

        public PaisController()
        {
            _useApi = ConfigHelper.UseApi();

            if (_useApi)
            {
                _apiClient = new ApiClient();
            }
            else
            {
                _dao = new DAOPais();
            }
        }

        public Task Salvar(Pais pais)
        {
            if (_useApi)
            {
                return _apiClient.SavePaisAsync(pais);
            }
            else
            {
                return Task.Run(() => _dao.Salvar(pais));
            }
        }

        public Task<Pais> BuscarPorId(int id)
        {
            if (_useApi)
            {
                return _apiClient.GetPaisByIdAsync(id);
            }
            else
            {
                return Task.FromResult(_dao.BuscarPorId(id));
            }
        }

        public Task<List<Pais>> ListarPais()
        {
            if (_useApi)
            {
                return _apiClient.GetPaisesAsync();
            }
            else
            {
                return Task.FromResult(_dao.ListarPais());
            }
        }

        public Task Excluir(int id)
        {
            if (_useApi)
            {
                return _apiClient.DeletePaisAsync(id);
            }
            else
            {
                return Task.Run(() => _dao.Excluir(id));
            }
        }
    }
}