using System;
using System.Collections.Generic;

namespace Projeto.Models
{
    public class Compra
    {
        public string Modelo { get; set; }
        public string Serie { get; set; }
        public int NumeroNota { get; set; }
        public int FornecedorId { get; set; }
        public Fornecedor oFornecedor { get; set; } 

        public int? CondicaoPagamentoId { get; set; }
        public CondicaoPagamento oCondicaoPagamento { get; set; }

        public DateTime? DataEmissao { get; set; }
        public DateTime? DataChegada { get; set; }
        public decimal ValorFrete { get; set; }
        public decimal Seguro { get; set; }
        public decimal Despesas { get; set; }
        public decimal ValorTotal { get; set; }
        public string Observacao { get; set; }
        public bool Ativo { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }

        public List<ItemCompra> Itens { get; set; }

        public string NomeFornecedor => oFornecedor?.Nome;
        public string NomeCondPgto => oCondicaoPagamento?.Descricao;

        public Compra()
        {
            Itens = new List<ItemCompra>();
        }
    }
}