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
        public string Rg { get; set; }
        public string Apelido { get; set; }
        public string Genero { get; set; }
        public string Matricula { get; set; }
        public DateTime? DataAdmissao { get; set; }
        public DateTime? DataDemissao { get; set; }

    }
}
