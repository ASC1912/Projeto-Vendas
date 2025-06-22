using Projeto.DAO;
using Projeto.Models;
using System.Collections.Generic;

namespace Projeto.Controller
{
    internal class MarcaController
    {
        private DAOMarca dao = new DAOMarca();

        public void Salvar(Marca marca)
        {
            dao.Salvar(marca);
        }

        public Marca BuscarPorId(int id)
        {
            return dao.BuscarPorId(id);
        }

        public List<Marca> ListarMarcas()
        {
            return dao.ListarMarcas();
        }

        public void Excluir(int id)
        {
            dao.Excluir(id);
        }
    }
}
