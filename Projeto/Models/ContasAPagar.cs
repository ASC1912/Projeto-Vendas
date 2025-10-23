using System;
using System.Collections.Generic;

namespace Projeto.Models
{
    public class ContasAPagar
    {
        public int Id { get; set; }
        public int FornecedorId { get; set; }
        public string Descricao { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal ValorVencimento { get; set; }
        public string Status { get; set; } 

        public DateTime? DataPagamento { get; set; }
        public decimal? ValorPago { get; set; }
        public decimal? Juros { get; set; }
        public decimal? Multa { get; set; }
        public decimal? Desconto { get; set; }

        public string CompraModelo_FK { get; set; }
        public string CompraSerie_FK { get; set; }
        public int? CompraNumeroNota_FK { get; set; }
        public int? CompraFornecedorId_FK { get; set; }

        public string NomeFornecedor { get; set; }
        public string NomeFormaPagamento { get; set; }
    }
}