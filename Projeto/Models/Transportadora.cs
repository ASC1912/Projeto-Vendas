using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Models
{
    internal  class Transportadora : Pessoa
    {
        public string InscricaoEstadual { get; set; }
        public string InscricaoEstadualSubTrib { get; set; }
        public int? IdCondicao { get; set; }
    }
}
