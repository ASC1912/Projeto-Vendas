﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Models
{
    internal class Cliente : Pessoa
    {
        public string Rg { get; set; }
        public int? IdCondicao { get; set; }

    }
}
