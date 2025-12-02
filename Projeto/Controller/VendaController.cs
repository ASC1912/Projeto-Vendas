using Projeto.DAO;
using Projeto.Models;
using System;
using System.Collections.Generic;

namespace Projeto.Controller
{
    internal class VendaController
    {
        private DAOVenda dao = new DAOVenda();

        public void Salvar(Venda venda)
        {
            try
            {
                dao.Salvar(venda);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar a venda no controller: " + ex.Message);
            }
        }

        public void Cancelar(Venda venda)
        {
            try
            {
                dao.Cancelar(venda);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao cancelar a venda no controller: " + ex.Message);
            }
        }

        public List<Venda> Listar()
        {
            try
            {
                return dao.Listar();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar as vendas no controller: " + ex.Message);
            }
        }

        public Venda BuscarPorChave(string modelo, string serie, int numeroNota, int clienteId)
        {
            try
            {
                return dao.BuscarPorChave(modelo, serie, numeroNota, clienteId);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar a venda no controller: " + ex.Message);
            }
        }

        public bool VerificarVendaExistente(int modelo, string serie, int numeroNota, int idCliente)
        {
            return dao.VerificarVendaExistente(modelo, serie, numeroNota, idCliente);
        }

        public List<Venda> Pesquisar(string busca)
        {
            try
            {
                return dao.Pesquisar(busca);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao pesquisar vendas: " + ex.Message);
            }
        }
    }
}