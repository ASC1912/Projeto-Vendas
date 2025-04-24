using Projeto.Models;
using Projeto.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Controller
{
    internal class FormaPagamentoController
    {
        private DAOFormaPagamento dao = new DAOFormaPagamento();

        public void Salvar(FormaPagamento forma)
        {
            dao.Salvar(forma);
        }

        public List<FormaPagamento> ListarFormaPagamento()
        {
            return dao.ListarFormaPagamento();
        }

        public FormaPagamento BuscarPorId(int id)
        {
            return dao.BuscarPorId(id);
        }

        public void Excluir(int id)
        {
            dao.Excluir(id);
        }

        public string ObterDescricaoFormaPagamento(int id)
        {
            return dao.ObterDescricaoFormaPagamento(id);
        }

        public int ObterIdFormaPagamento(string descricao)
        {
            return dao.ObterIdFormaPagamento(descricao);
        }
    }
}
