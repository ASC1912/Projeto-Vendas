using Projeto.DAO;
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


        public Task<List<ContasAPagar>> Listar(List<string> statuses, string busca)
        {
            return Task.Run(() => _dao.Listar(statuses, busca));
        }

        public Task Pagar(ContasAPagar conta)
        {
            if (conta.NumeroParcela > 1)
            {
                bool anteriorPaga = _dao.VerificarParcelaAnteriorPaga(conta);
                if (!anteriorPaga)
                {
                    throw new InvalidOperationException($"Não é possível pagar esta parcela (Nº {conta.NumeroParcela}). A parcela anterior (Nº {conta.NumeroParcela - 1}) ainda está em aberto.");
                }
            }

            return Task.Run(() => _dao.Pagar(conta));
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