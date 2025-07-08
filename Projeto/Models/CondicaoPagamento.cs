using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Models
{
    public class CondicaoPagamento
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int QtdParcelas { get; set; }
        public decimal Juros { get; set; }
        public decimal Multa { get; set; }
        public decimal Desconto { get; set; }
        public bool Ativo { get; set; }       
        public DateTime? DataCadastro { get; set; }  
        public DateTime? DataAlteracao { get; set; }
    }
}
