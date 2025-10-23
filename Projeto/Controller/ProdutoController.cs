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
    public class ProdutoController
    {
        private readonly bool _useApi;
        private readonly IProdutoApiService _apiService;
        private readonly DAOProduto _dao;

        public ProdutoController()
        {
            _useApi = ConfigHelper.UseApi();

            if (_useApi)
            {
                _apiService = new ProdutoApiService();
            }
            else
            {
                _dao = new DAOProduto();
            }
        }

        public Task Salvar(Produto produto, List<ProdutoFornecedor> fornecedores)
        {
            if (_useApi)
            {
                return _apiService.SaveProdutoAsync(produto);
            }
            else
            {
                return Task.Run(() => {
                    _dao.Salvar(produto, fornecedores);
                });
            }
        }

        public Task Excluir(int id)
        {
            if (_useApi)
            {
                return _apiService.DeleteProdutoAsync(id);
            }
            else
            {
                return Task.Run(() => _dao.Excluir(id));
            }
        }

        public Task<Produto> BuscarPorId(int id)
        {
            if (_useApi)
            {
                return _apiService.GetProdutoByIdAsync(id);
            }
            else
            {
                return Task.FromResult(_dao.BuscarPorId(id));
            }
        }

        public Task<List<Produto>> ListarProdutos()
        {
            if (_useApi)
            {
                return _apiService.GetProdutosAsync();
            }
            else
            {
                return Task.FromResult(_dao.ListarProdutos());
            }
        }
    }
}