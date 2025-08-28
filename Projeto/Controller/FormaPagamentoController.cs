using Projeto.DAO;
using Projeto.Models;
using Projeto.Services;
using Projeto.Services.Interfaces;
using Projeto.Utils;
using System.Collections.Generic;
using System.Linq; 
using System.Threading.Tasks;

namespace Projeto.Controller
{
    internal class FormaPagamentoController
    {
        private readonly bool _useApi;
        private readonly IFormaPagamentoApiService _apiService;
        private readonly DAOFormaPagamento _dao;

        public FormaPagamentoController()
        {
            _useApi = ConfigHelper.UseApi();
            if (_useApi)
            {
                _apiService = new FormaPagamentoApiService();
            }
            else
            {
                _dao = new DAOFormaPagamento();
            }
        }

        public Task Salvar(FormaPagamento forma)
        {
            if (_useApi)
            {
                return _apiService.SaveFormaPagamentoAsync(forma);
            }
            else
            {
                return Task.Run(() => _dao.Salvar(forma));
            }
        }

        public Task<List<FormaPagamento>> ListarFormaPagamento()
        {
            if (_useApi)
            {
                return _apiService.GetFormasPagamentoAsync();
            }
            else
            {
                return Task.FromResult(_dao.ListarFormaPagamento());
            }
        }

        public Task<FormaPagamento> BuscarPorId(int id)
        {
            if (_useApi)
            {
                return _apiService.GetFormaPagamentoByIdAsync(id);
            }
            else
            {
                return Task.FromResult(_dao.BuscarPorId(id));
            }
        }

        public Task Excluir(int id)
        {
            if (_useApi)
            {
                return _apiService.DeleteFormaPagamentoAsync(id);
            }
            else
            {
                return Task.Run(() => _dao.Excluir(id));
            }
        }

        public async Task<string> ObterDescricaoFormaPagamento(int id)
        {
            if (_useApi)
            {
                var lista = await ListarFormaPagamento();
                return lista.FirstOrDefault(f => f.Id == id)?.Descricao;
            }
            else
            {
                return _dao.ObterDescricaoFormaPagamento(id);
            }
        }

        public async Task<int> ObterIdFormaPagamento(string descricao)
        {
            if (_useApi)
            {
                var lista = await ListarFormaPagamento();
                return lista.FirstOrDefault(f => f.Descricao.Equals(descricao, System.StringComparison.OrdinalIgnoreCase))?.Id ?? 0;
            }
            else
            {
                return _dao.ObterIdFormaPagamento(descricao);
            }
        }
    }
}