using Projeto.DAO;
using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Controller
{
    internal class ClienteController
    {
        private DAOCliente dao = new DAOCliente();

        public void Salvar(Cliente cliente)
        {
            dao.Salvar(cliente);
        }
        public Cliente BuscarPorId(int id)
        {
            return dao.BuscarPorId(id);
        }
        public List<Cliente> ListarCliente()
        {
            return dao.ListarClientes();
        }
        public void Excluir(int id)
        {
            dao.Excluir(id);
        }
    }
}
