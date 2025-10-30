﻿using Projeto.DAO;
using Projeto.Models;
using System;
using System.Collections.Generic; 
using System.Threading.Tasks;

namespace Projeto.Controller
{
    internal class ContasAPagarController
    {
        private DAOContasAPagar _dao;

        public ContasAPagarController()
        {
            _dao = new DAOContasAPagar();
        }


        public Task SalvarManual(ContasAPagar conta)
        {
            return Task.Run(() => _dao.SalvarManual(conta));
        }


        public Task<List<ContasAPagar>> Listar(string status, string busca)
        {
            return Task.Run(() => _dao.Listar(status, busca));
        }

        public Task Pagar(ContasAPagar conta)
        {
            return Task.Run(() => _dao.Pagar(conta));
        }


        public Task Estornar(ContasAPagar conta)
        {
            return Task.Run(() => _dao.Estornar(conta));
        }

        public Task CancelarManual(ContasAPagar conta, string motivo)
        {
            bool compraExiste = _dao.VerificarExistencia(
                conta.CompraModelo,
                conta.CompraSerie,
                conta.CompraNumeroNota,
                conta.CompraFornecedorId
            );

            if (compraExiste)
            {
                throw new InvalidOperationException("Esta conta está vinculada a uma Nota de Compra e não pode ser cancelada manualmente. Cancele a Nota de Compra correspondente.");
            }

            return Task.Run(() => _dao.CancelarContaManual(conta, motivo));
        }
    }
}