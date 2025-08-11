using Projeto.DAO;
using Projeto.Models;
using Projeto.Services;
using Projeto.Services.Interfaces;
using Projeto.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projeto.Controller
{
    internal class CidadeController
    {
        private readonly bool _useApi;
        private readonly ICidadeApiService _apiService;
        private readonly DAOCidade _dao;

        public CidadeController()
        {
            _useApi = ConfigHelper.UseApi();
            if (_useApi)
            {
                _apiService = new CidadeApiService();
            }
            else
            {
                _dao = new DAOCidade();
            }
        }

        public Task Salvar(Cidade cidade)
        {
            if (_useApi)
            {
                return _apiService.SaveCidadeAsync(cidade);
            }
            else
            {
                return Task.Run(() => _dao.Salvar(cidade));
            }
        }

        public Task<Cidade> BuscarPorId(int id)
        {
            if (_useApi)
            {
                return _apiService.GetCidadeByIdAsync(id);
            }
            else
            {
                return Task.FromResult(_dao.BuscarPorId(id));
            }
        }

        public Task<List<Cidade>> ListarCidade()
        {
            if (_useApi)
            {
                return _apiService.GetCidadesAsync();
            }
            else
            {
                return Task.FromResult(_dao.ListarCidade());
            }
        }

        public Task Excluir(int id)
        {
            if (_useApi)
            {
                return _apiService.DeleteCidadeAsync(id);
            }
            else
            {
                return Task.Run(() => _dao.Excluir(id));
            }
        }
    }
}