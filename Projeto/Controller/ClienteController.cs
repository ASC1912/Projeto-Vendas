using Projeto.DAO;
using Projeto.Models;
using Projeto.Services;
using Projeto.Services.Interfaces;
using Projeto.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Controller
{
    internal class ClienteController
    {
        private readonly bool _useApi;
        private readonly IClienteApiService _apiService;
        private readonly DAOCliente _dao;

        public ClienteController()
        {
            _useApi = ConfigHelper.UseApi();
            if (_useApi)
            {
                _apiService = new ClienteApiService();
            }
            else
            {
                _dao = new DAOCliente();
            }
        }

        public Task Salvar(Cliente cliente)
        {
            if (_useApi)
            {
                return _apiService.SaveClienteAsync(cliente);
            }
            else
            {
                return Task.Run(() => _dao.Salvar(cliente));
            }
        }

        public Task<Cliente> BuscarPorId(int id)
        {
            if (_useApi)
            {
                return _apiService.GetClienteByIdAsync(id);
            }
            else
            {
                return Task.FromResult(_dao.BuscarPorId(id));
            }
        }

        public Task<List<Cliente>> ListarCliente()
        {
            if (_useApi)
            {
                return _apiService.GetClientesAsync();
            }
            else
            {
                return Task.FromResult(_dao.ListarClientes());
            }
        }

        public Task Excluir(int id)
        {
            if (_useApi)
            {
                return _apiService.DeleteClienteAsync(id);
            }
            else
            {
                return Task.Run(() => _dao.Excluir(id));
            }
        }
    }
}
