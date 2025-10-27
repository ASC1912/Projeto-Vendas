using System;

namespace Projeto.Models
{
    public class ContasAPagar
    {
        // Chave Primária Composta (herdada da Compra)
        public string CompraModelo { get; set; }
        public string CompraSerie { get; set; }
        public int CompraNumeroNota { get; set; }
        public int CompraFornecedorId { get; set; }
        public int NumeroParcela { get; set; }

        // Dados da Parcela/Conta
        public int FornecedorId { get; set; }
        public string Descricao { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal ValorVencimento { get; set; }

        // Dados do Pagamento
        public int? IdFormaPagamento { get; set; } // O ID da forma de pagamento usada
        public string Status { get; set; }
        public decimal? Juros { get; set; }
        public decimal? Multa { get; set; }
        public decimal? Desconto { get; set; }
        public decimal? ValorPago { get; set; }
        public DateTime? DataPagamento { get; set; }

        // Auditoria
        public bool Ativo { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }

        // Campos auxiliares (para preencher na listagem/consulta)
        public string NomeFornecedor { get; set; }
        public string NomeFormaPagamento { get; set; }
    }
}