using Projeto.DAO;
using Projeto.Models;
using Projeto.Services;
using Projeto.Services.Interfaces;
using Projeto.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projeto.Controller
{
    internal class FuncionarioController
    {
        private readonly bool _useApi;
        private readonly IFuncionarioApiService _apiService;
        private readonly DAOFuncionario _dao;

        public FuncionarioController()
        {
            _useApi = ConfigHelper.UseApi();
            if (_useApi)
            {
                _apiService = new FuncionarioApiService();
            }
            else
            {
                _dao = new DAOFuncionario();
            }
        }

        public Task Salvar(Funcionario funcionario)
        {
            if (_useApi)
            {
                return _apiService.SaveFuncionarioAsync(funcionario);
            }
            else
            {
                return Task.Run(() => _dao.Salvar(funcionario));
            }
        }

        public Task<Funcionario> BuscarPorId(int id)
        {
            if (_useApi)
            {
                return _apiService.GetFuncionarioByIdAsync(id);
            }
            else
            {
                return Task.FromResult(_dao.BuscarPorId(id));
            }
        }

        public Task<List<Funcionario>> ListarFuncionarios()
        {
            if (_useApi)
            {
                return _apiService.GetFuncionariosAsync();
            }
            else
            {
                return Task.FromResult(_dao.ListarFuncionarios());
            }
        }

        public Task Excluir(int id)
        {
            if (_useApi)
            {
                return _apiService.DeleteFuncionarioAsync(id);
            }
            else
            {
                return Task.Run(() => _dao.Excluir(id));
            }
        }
    }
}