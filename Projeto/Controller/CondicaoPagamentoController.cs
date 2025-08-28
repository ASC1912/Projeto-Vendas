using Projeto.DAO;
using Projeto.Models;
using Projeto.Services; 
using Projeto.Services.Interfaces; 
using Projeto.Utils; 
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projeto.Controller
{
    internal class CondicaoPagamentoController
    {
        private readonly bool _useApi;
        private readonly ICondicaoPagamentoApiService _apiService;
        private readonly DAOCondicaoPagamento _dao;
        private readonly DAOParcela _daoParcela; 

        public CondicaoPagamentoController()
        {
            _useApi = ConfigHelper.UseApi();
            if (_useApi)
            {
                _apiService = new CondicaoPagamentoApiService();
            }
            else
            {
                _dao = new DAOCondicaoPagamento();
                _daoParcela = new DAOParcela();
            }
        }

        public Task Salvar(CondicaoPagamento condicao, List<Parcelamento> parcelas)
        {
            if (_useApi)
            {
                condicao.Parcelas = parcelas;
                return _apiService.SaveCondicaoPagamentoAsync(condicao);
            }
            else
            {
                return Task.Run(() => {
                    int condicaoId = _dao.SalvarCondicaoPagamento(condicao);
                    if (condicaoId > 0 && parcelas != null)
                    {
                        foreach (var p in parcelas) { p.CondicaoId = condicaoId; }
                        _daoParcela.SalvarParcelas(parcelas, condicaoId);
                    }
                });
            }
        }

        public Task<CondicaoPagamento> BuscarPorId(int id)
        {
            if (_useApi)
            {
                return _apiService.GetCondicaoPagamentoByIdAsync(id);
            }
            else
            {
                return Task.FromResult(_dao.BuscarPorId(id));
            }
        }

        public Task<List<CondicaoPagamento>> ListarCondicaoPagamento()
        {
            if (_useApi)
            {
                return _apiService.GetCondicoesPagamentoAsync();
            }
            else
            {
                return Task.FromResult(_dao.ListarCondicaoPagamento());
            }
        }

        public Task Excluir(int id)
        {
            if (_useApi)
            {
                return _apiService.DeleteCondicaoPagamentoAsync(id);
            }
            else
            {
                return Task.Run(() => _dao.Excluir(id));
            }
        }
    }
}