using Projeto.DAO;
using Projeto.Models;
using Projeto.Services;
using Projeto.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Utils;

namespace Projeto.Controller
{
    internal class EstadoController
    {
        private readonly bool _useApi;
        private readonly IEstadoApiService _apiService;
        private readonly DAOEstado _dao;

        public EstadoController()
        {
            _useApi = ConfigHelper.UseApi();
            if (_useApi)
            {
                _apiService = new EstadoApiService();
            }
            else
            {
                _dao = new DAOEstado();
            }
        }
        public Task Salvar(Estado estado)
        {
            if (_useApi)
            {
                return _apiService.SaveEstadoAsync(estado);
            }
            else
            {
                return Task.Run(() => _dao.Salvar(estado));
            }
        }
        public Task<Estado> BuscarPorId(int id)
        {
            if (_useApi)
            {
                return _apiService.GetEstadoByIdAsync(id);
            }
            else
            {
                return Task.FromResult(_dao.BuscarPorId(id));
            }
        }

        public Task<List<Estado>> ListarEstado()
        {
            if (_useApi)
            {
                return _apiService.GetEstadosAsync();
            }
            else
            {
                return Task.FromResult(_dao.ListarEstado());
            }
        }

        public Task Excluir(int id)
        {
            if (_useApi)
            {
                return _apiService.DeleteEstadoAsync(id);
            }
            else
            {
                return Task.Run(() => _dao.Excluir(id));
            }
        }
    }
}
