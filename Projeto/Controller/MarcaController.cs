using Projeto.DAO;
using Projeto.Models;
using Projeto.Services;
using Projeto.Services.Interfaces;
using Projeto.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projeto.Controller
{
    public class MarcaController
    {
        private readonly bool _useApi;
        private readonly IMarcaApiService _apiService;
        private readonly DAOMarca _dao;

        public MarcaController()
        {
            _useApi = ConfigHelper.UseApi();

            if (_useApi)
            {
                _apiService = new MarcaApiService();
            }
            else
            {
                _dao = new DAOMarca();
            }
        }

        public Task Salvar(Marca marca)
        {
            if (_useApi)
            {
                return _apiService.SaveMarcaAsync(marca);
            }
            else
            {
                return Task.Run(() => _dao.Salvar(marca));
            }
        }

        public Task<Marca> BuscarPorId(int id)
        {
            if (_useApi)
            {
                return _apiService.GetMarcaByIdAsync(id);
            }
            else
            {
                return Task.FromResult(_dao.BuscarPorId(id));
            }
        }

        public Task<List<Marca>> ListarMarcas()
        {
            if (_useApi)
            {
                return _apiService.GetMarcasAsync();
            }
            else
            {
                return Task.FromResult(_dao.ListarMarcas());
            }
        }

        public Task Excluir(int id)
        {
            if (_useApi)
            {
                return _apiService.DeleteMarcaAsync(id);
            }
            else
            {
                return Task.Run(() => _dao.Excluir(id));
            }
        }
    }
}