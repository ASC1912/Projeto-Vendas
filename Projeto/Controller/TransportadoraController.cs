using Projeto.DAO;
using Projeto.Models;
using Projeto.Services;
using Projeto.Services.Interfaces;
using Projeto.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projeto.Controller
{
    public class TransportadoraController
    {
        private readonly bool _useApi;
        private readonly ITransportadoraApiService _apiService;
        private readonly DAOTransportadora _dao;

        public TransportadoraController()
        {
            _useApi = ConfigHelper.UseApi();

            if (_useApi)
            {
                _apiService = new TransportadoraApiService();
            }
            else
            {
                _dao = new DAOTransportadora();
            }
        }

        public Task Salvar(Transportadora transportadora)
        {
            if (_useApi)
            {
                return _apiService.SaveTransportadoraAsync(transportadora);
            }
            else
            {
                return Task.Run(() => _dao.Salvar(transportadora));
            }
        }

        public Task<Transportadora> BuscarPorId(int id)
        {
            if (_useApi)
            {
                return _apiService.GetTransportadoraByIdAsync(id);
            }
            else
            {
                return Task.FromResult(_dao.BuscarPorId(id));
            }
        }

        public Task<List<Transportadora>> ListarTransportadoras()
        {
            if (_useApi)
            {
                return _apiService.GetTransportadorasAsync();
            }
            else
            {
                return Task.FromResult(_dao.ListarTransportadoras());
            }
        }

        public Task Excluir(int id)
        {
            if (_useApi)
            {
                return _apiService.DeleteTransportadoraAsync(id);
            }
            else
            {
                return Task.Run(() => _dao.Excluir(id));
            }
        }
    }
}