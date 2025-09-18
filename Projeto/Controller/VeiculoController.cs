using Projeto.DAO;
using Projeto.Models;
using Projeto.Services;
using Projeto.Services.Interfaces;
using Projeto.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projeto.Controller
{
    public class VeiculoController
    {
        private readonly bool _useApi;
        private readonly IVeiculoApiService _apiService;
        private readonly DAOVeiculo _dao;

        public VeiculoController()
        {
            _useApi = ConfigHelper.UseApi();

            if (_useApi)
            {
                _apiService = new VeiculoApiService();
            }
            else
            {
                _dao = new DAOVeiculo();
            }
        }

        public Task Salvar(Veiculo veiculo)
        {
            if (_useApi)
            {
                return _apiService.SaveVeiculoAsync(veiculo);
            }
            else
            {
                return Task.Run(() => _dao.Salvar(veiculo));
            }
        }

        public Task<Veiculo> BuscarPorId(int id)
        {
            if (_useApi)
            {
                return _apiService.GetVeiculoByIdAsync(id);
            }
            else
            {
                return Task.FromResult(_dao.BuscarPorId(id));
            }
        }

        public Task<List<Veiculo>> ListarVeiculos()
        {
            if (_useApi)
            {
                return _apiService.GetVeiculosAsync();
            }
            else
            {
                return Task.FromResult(_dao.ListarVeiculos());
            }
        }

        public Task Excluir(int id)
        {
            if (_useApi)
            {
                return _apiService.DeleteVeiculoAsync(id);
            }
            else
            {
                return Task.Run(() => _dao.Excluir(id));
            }
        }
    }
}