﻿using System;
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
    }
}
