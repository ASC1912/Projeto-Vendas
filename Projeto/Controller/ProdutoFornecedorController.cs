using Projeto.DAO;
using Projeto.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projeto.Controller
{
    internal class ProdutoFornecedorController
    {
        private readonly DAOProdutoFornecedor _dao;

        public ProdutoFornecedorController()
        {
            _dao = new DAOProdutoFornecedor();
        }

        public Task Salvar(ProdutoFornecedor relacao)
        {
            return Task.Run(() => _dao.Salvar(relacao));
        }

        public Task Excluir(ProdutoFornecedor relacao)
        {
            return Task.Run(() => _dao.Excluir(relacao));
        }

        public Task<List<ProdutoFornecedor>> ListarPorProduto(int produtoId)
        {
            return Task.FromResult(_dao.ListarPorProduto(produtoId));
        }
    }
}