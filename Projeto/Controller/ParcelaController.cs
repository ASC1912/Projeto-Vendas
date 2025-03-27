using Projeto.DAO;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Controller
{
    internal class ParcelaController
    {
        private DAOParcela dao = new DAOParcela();


        public List<Parcelamento> ListarParcelas(int condicaoId)
        {
            return dao.ListarParcelas(condicaoId);
        }
    }
}
