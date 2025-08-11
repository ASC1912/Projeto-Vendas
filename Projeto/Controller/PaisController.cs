using Projeto.DAO;
using Projeto.Models;
using Projeto.Services;
using Projeto.Utils;
using Projeto.Services.Interfaces; 
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projeto.Controller
{
    internal class PaisController
    {
        private readonly bool _useApi;
        private readonly IPaisApiService _apiService; 
        private readonly DAOPais _dao;

        public PaisController()
        {
            _useApi = ConfigHelper.UseApi();

            if (_useApi)
            {
                _apiService = new PaisApiService(); 
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
                return _apiService.SavePaisAsync(pais); 
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
                return _apiService.GetPaisByIdAsync(id); 
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
                return _apiService.GetPaisesAsync();
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
                return _apiService.DeletePaisAsync(id); 
            }
            else
            {
                return Task.Run(() => _dao.Excluir(id));
            }
        }
    }
}