using Projeto.DAO;
using Projeto.Models;
using Projeto.Services;
using Projeto.Services.Interfaces;
using Projeto.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projeto.Controller
{
    internal class FornecedorController
    {
        private readonly bool _useApi;
        private readonly IFornecedorApiService _apiService;
        private readonly DAOFornecedor _dao;

        public FornecedorController()
        {
            _useApi = ConfigHelper.UseApi();
            if (_useApi)
            {
                _apiService = new FornecedorApiService();
            }
            else
            {
                _dao = new DAOFornecedor();
            }
        }

        public Task Salvar(Fornecedor fornecedor)
        {
            if (_useApi)
            {
                return _apiService.SaveFornecedorAsync(fornecedor);
            }
            else
            {
                return Task.Run(() => _dao.Salvar(fornecedor));
            }
        }

        public Task<Fornecedor> BuscarPorId(int id)
        {
            if (_useApi)
            {
                return _apiService.GetFornecedorByIdAsync(id);
            }
            else
            {
                return Task.FromResult(_dao.BuscarPorId(id));
            }
        }

        public Task<List<Fornecedor>> ListarFornecedor()
        {
            if (_useApi)
            {
                return _apiService.GetFornecedoresAsync();
            }
            else
            {
                return Task.FromResult(_dao.ListarFornecedores());
            }
        }

        public Task Excluir(int id)
        {
            if (_useApi)
            {
                return _apiService.DeleteFornecedorAsync(id);
            }
            else
            {
                return Task.Run(() => _dao.Excluir(id));
            }
        }
    }
}