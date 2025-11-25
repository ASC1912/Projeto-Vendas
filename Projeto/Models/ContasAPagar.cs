using System;

namespace Projeto.Models
{
    public class ContasAPagar
    {
        public string CompraModelo { get; set; }
        public string CompraSerie { get; set; }
        public int CompraNumeroNota { get; set; }
        public int CompraFornecedorId { get; set; }
        public int NumeroParcela { get; set; }

        public int FornecedorId { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal ValorVencimento { get; set; }

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
        public string NomeFornecedor { get; set; }
        public string NomeFormaPagamento { get; set; }
    }
}