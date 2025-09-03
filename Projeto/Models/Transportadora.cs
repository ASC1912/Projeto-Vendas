using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Projeto.Models
{
    public  class Transportadora : Pessoa
    {
        [JsonPropertyName("transportadora")]
        public override string Nome { get; set; }

        [JsonPropertyName("inscricaoEstadual")]
        public string InscricaoEstadual { get; set; }

        [JsonPropertyName("inscricaoEstadualSubTrib")]
        public string InscricaoEstadualSubTrib { get; set; }

        [JsonPropertyName("condicoesPagamentoId")]
        public int? IdCondicao { get; set; }

        [JsonPropertyName("condicaoPagamento")]
        public CondicaoPagamento oCondicaoPagamento { get; set; }

        [JsonIgnore]
        public string DescricaoCondicao { get; set; }
    }
}
