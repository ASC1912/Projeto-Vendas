using Projeto.DAO;
using Projeto.Models;
using System.Collections.Generic;

namespace Projeto.Controller
{
    internal class GrupoController
    {
        private DAOGrupo dao = new DAOGrupo();

        public void Salvar(Grupo grupo)
        {
            dao.Salvar(grupo);
        }

        public Grupo BuscarPorId(int id)
        {
            return dao.BuscarPorId(id);
        }

        public List<Grupo> ListarGrupos()
        {
            return dao.ListarGrupos();
        }

        public void Excluir(int id)
        {
            dao.Excluir(id);
        }
    }
}