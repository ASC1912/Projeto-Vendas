using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Models
{
    internal class CondicaoPagamento
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int QtdParcelas { get; set; }
        public decimal Juros { get; set; }
        public decimal Multa { get; set; }
        public decimal Desconto { get; set; }
        public bool Status { get; set; }

    }
}
