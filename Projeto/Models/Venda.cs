using System;
using System.Collections.Generic;

namespace Projeto.Models
{
    public class Venda
    {
        public string Modelo { get; set; }
        public string Serie { get; set; }
        public int NumeroNota { get; set; }
        public int ClienteId { get; set; } 
        public Cliente oCliente { get; set; }

        public int? CondicaoPagamentoId { get; set; }
        public CondicaoPagamento oCondicaoPagamento { get; set; }

        public DateTime? DataEmissao { get; set; }
        public DateTime? DataSaida { get; set; }
        public decimal ValorFrete { get; set; }
        public decimal ValorDesconto { get; set; }
        public decimal ValorTotal { get; set; }
        public string Observacao { get; set; }
        public bool Ativo { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }

        public List<ItemVenda> Itens { get; set; } 
        public List<ContasAReceber> Parcelas { get; set; } 

        public string NomeCliente => oCliente?.Nome; 
        public string NomeCondPgto => oCondicaoPagamento?.Descricao;

        public Venda()
        {
            Itens = new List<ItemVenda>();
            Parcelas = new List<ContasAReceber>();
        }
    }
}