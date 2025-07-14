using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Models
{
    public class Cliente : Pessoa
    {
        public string Rg { get; set; }
        public string Genero { get; set; }
        public int? IdCondicao { get; set; }
        public string DescricaoCondicao { get; set; } 

    }
}
