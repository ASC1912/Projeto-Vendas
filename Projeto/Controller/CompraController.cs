using Projeto.DAO;
using Projeto.Models;
using System;
using System.Collections.Generic;

namespace Projeto.Controller
{
    internal class CompraController
    {
        private DAOCompra dao = new DAOCompra();
        private DAOProduto daoProduto = new DAOProduto();

        public void Salvar(Compra compra)
        {
            try
            {
                dao.Salvar(compra);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar a compra no controller: " + ex.Message);
            }
        }

        public void Cancelar(Compra compra)
        {
            try
            {
                dao.Cancelar(compra);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao cancelar a compra no controller: " + ex.Message);
            }
        }

        public List<Compra> Listar()
        {
            try
            {
                return dao.Listar();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao listar as compras no controller: " + ex.Message);
            }
        }

        public Compra BuscarPorChave(string modelo, string serie, int numeroNota, int fornecedorId)
        {
            try
            {
                return dao.BuscarPorChave(modelo, serie, numeroNota, fornecedorId);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar a compra no controller: " + ex.Message);
            }
        }

        public bool VerificarDuplicidade(string modelo, string serie, int numeroNota, int fornecedorId)
        {
            try
            {
                return dao.VerificarDuplicidade(modelo, serie, numeroNota, fornecedorId);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro no Controller ao verificar duplicidade: " + ex.Message);
            }
        }

        public List<Compra> Pesquisar(string busca)
        {
            try
            {
                return dao.Pesquisar(busca);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao pesquisar compras: " + ex.Message);
            }
        }

        public string ValidarCancelamento(Compra compra)
        {
            if (compra.Itens == null || compra.Itens.Count == 0)
            {
                return "Erro: Os itens da compra não foram carregados para validação.";
            }

            foreach (var item in compra.Itens)
            {
                Produto produtoNoEstoque = daoProduto.BuscarPorId(item.ProdutoId);

                if (produtoNoEstoque != null)
                {
                   
                    if (produtoNoEstoque.Estoque < item.Quantidade)
                    {
                        return $"OPERAÇÃO BLOQUEADA!\n\n" +
                               $"Produto: {produtoNoEstoque.Descricao}\n" +
                               $"Estoque Atual: {produtoNoEstoque.Estoque}\n" +
                               $"Qtd. nesta Nota: {item.Quantidade}\n\n" +
                               $"Você já vendeu itens desta nota. Não é possível cancelá-la pois o estoque ficaria negativo.";
                    }
                }
            }

            return "OK"; 
        }
    }
}