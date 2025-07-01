using Projeto.DAO;
using Projeto.Models;
using System;
using System.Collections.Generic;

namespace Projeto.Controller
{
    internal class VeiculoController
    {
        private DAOVeiculo dao = new DAOVeiculo();

        public void Salvar(Veiculo veiculo)
        {
            dao.Salvar(veiculo);
        }

        public Veiculo BuscarPorId(int id)
        {
            return dao.BuscarPorId(id);
        }

        public List<Veiculo> ListarVeiculos()
        {
            return dao.ListarVeiculos();
        }

        public void Excluir(int id)
        {
            dao.Excluir(id);
        }
    }
}