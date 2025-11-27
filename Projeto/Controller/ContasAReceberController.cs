using Projeto.DAO;
using Projeto.Models;
using System;
using System.Collections.Generic;

namespace Projeto.Controller
{
    internal class ContasAReceberController
    {
        private DAOContasAReceber dao = new DAOContasAReceber();

        public List<ContasAReceber> Listar(List<string> status, string termoBusca)
        {
            try
            {
                return dao.Listar(status, termoBusca);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar contas a receber: " + ex.Message);
            }
        }

        public void Receber(ContasAReceber conta)
        {
            try
            {
                dao.Receber(conta);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao receber a conta: " + ex.Message);
            }
        }
    }
}