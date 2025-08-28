using System;
using System.Text.Json.Serialization;

namespace Projeto.Models
{
    public class Fornecedor : Pessoa
    {
        [JsonPropertyName("fornecedor")]
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