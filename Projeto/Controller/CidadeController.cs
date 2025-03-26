using Projeto.DAO;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Controller
{
    internal class CidadeController
    {
        private DAOCidade dao = new DAOCidade();

        public void Salvar(Cidade cidade)
        {
            dao.Salvar(cidade);
        }

        public List<Cidade> ListarCidade()
        {
            return dao.ListarCidade();
        }

        public void Excluir(int id)
        {
            dao.Excluir(id);
        }
    }
}
