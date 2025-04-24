using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Models
{
    internal class Cidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }    
        public int IdEstado { get; set; }   
        public string EstadoNome { get; set; }
        public bool Status { get; set; }

    }
}
