using System;
using System.Collections.Generic;

namespace Projeto.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public int MesaNumero { get; set; }
        public int FuncionarioId { get; set; }
        public int? VendaId { get; set; } // Aceita nulo por enquanto
        public string Observacao { get; set; }
        public string Status { get; set; }
        public DateTime DataPedido { get; set; }
        public bool Ativo { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }

        public string NomeFuncionario { get; set; }
        public List<ItemPedido> Itens { get; set; }

        public Pedido()
        {
            Itens = new List<ItemPedido>();
        }
    }

    public class ItemPedido
    {
        public int PedidoId { get; set; }
        public int ProdutoId { get; set; }
        public int NumeroMesa { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public string Status { get; set; }

        // Propriedade para exibir o nome do produto
        public string NomeProduto { get; set; }
    }
}