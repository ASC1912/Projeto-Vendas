using Projeto.DAO;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Controller
{
    internal class FornecedorController
    {
        private DAOFornecedor dao = new DAOFornecedor();

        public void Salvar(Fornecedor fornecedor)
        {
            dao.Salvar(fornecedor);
        }
        public List<Fornecedor> ListarFornecedor()
        {
            return dao.ListarFornecedores();
        }


        public void Excluir(int id)
        {
            dao.Excluir(id);
        }
    }
}
