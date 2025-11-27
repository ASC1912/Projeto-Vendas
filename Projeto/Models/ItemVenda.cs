using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Models
{
    public class ItemVenda
    {
        public string VendaModelo { get; set; }
        public string VendaSerie { get; set; }
        public int VendaNumeroNota { get; set; }
        public int VendaClienteId { get; set; } 

        public int ProdutoId { get; set; }

        public decimal Quantidade { get; set; }
        public decimal ValorUnitario { get; set; } 
        public decimal ValorTotalItem { get; set; }
        public string NomeProduto { get; set; }
        public string NomeUnidadeMedida { get; set; }
    }
}