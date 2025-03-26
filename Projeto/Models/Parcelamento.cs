using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Models
{
    internal class Parcelamento
    {
        public int NumParcela { get; set; }
        public int PrazoDias {  get; set; }
        public decimal Porcentagem { get; set; }
        public int CondicaoId { get; set; }
        public int FormaPagamentoId { get; set; }
        
    }
}
