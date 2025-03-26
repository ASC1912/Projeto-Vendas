using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Models
{
    internal class Funcionario : Pessoa
    {
        public string Cargo { get; set; }
        public decimal Salario { get; set; }
    }
}
