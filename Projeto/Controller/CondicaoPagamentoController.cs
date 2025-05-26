using Projeto.DAO;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Controller
{
    internal class CondicaoPagamentoController
    {
        private DAOCondicaoPagamento dao = new DAOCondicaoPagamento();
        private DAOParcela daoParcela = new DAOParcela();

        public void Salvar(CondicaoPagamento condicao, List<Parcelamento> parcelas)
        {
            int condicaoId = dao.SalvarCondicaoPagamento(condicao);

            if (condicaoId > 0)
            {
                foreach (var parcela in parcelas)
                {
                    parcela.CondicaoId = condicaoId; 
                }

                daoParcela.SalvarParcelas(parcelas, condicaoId);
            }
        }

        public CondicaoPagamento BuscarPorId(int id)
        {
            return dao.BuscarPorId(id);
        }


        public List<CondicaoPagamento> ListarCondicaoPagamento()
        {
            return dao.ListarCondicaoPagamento();
        }

        public void Excluir(int id)
        {
            dao.Excluir(id);
        }

        
    }
}
