using Projeto.DAO;
using Projeto.Models;
using System.Collections.Generic;

namespace Projeto.Controller
{
    internal class ProdutoController
    {
        private DAOProduto dao = new DAOProduto();

        public void Salvar(Produto produto)
        {
            dao.Salvar(produto);
        }

        public Produto BuscarPorId(int id)
        {
            return dao.BuscarPorId(id);
        }

        public List<Produto> ListarProdutos()
        {
            return dao.ListarProdutos();
        }

        public void Excluir(int id)
        {
            dao.Excluir(id);
        }
    }
}
