using System;

namespace Projeto.Models
{
    public class ContasAReceber
    {
        // Chave Primária Composta (herdada da Venda)
        public string VendaModelo { get; set; }
        public string VendaSerie { get; set; }
        public int VendaNumeroNota { get; set; }
        public int VendaClienteId { get; set; }
        public int NumeroParcela { get; set; }

        // Dados da Parcela/Conta
        public int ClienteId { get; set; }
        public string Descricao { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal ValorVencimento { get; set; }

        // Dados do Recebimento
        public int? IdFormaPagamento { get; set; }
        public string Status { get; set; }
        public decimal? Juros { get; set; }
        public decimal? Multa { get; set; }
        public decimal? Desconto { get; set; }
        public decimal? ValorPago { get; set; }
        public DateTime? DataPagamento { get; set; }

        public bool Ativo { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public string MotivoCancelamento { get; set; }
        public string NomeCliente { get; set; }
        public string NomeFormaPagamento { get; set; }
    }
}