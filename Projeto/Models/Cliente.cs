using System;
using System.Text.Json.Serialization;

namespace Projeto.Models
{
    public class Cliente : Pessoa
    {
        [JsonPropertyName("cliente")]
        public override string Nome { get; set; }

        [JsonPropertyName("rg")]
        public string Rg { get; set; }

        [JsonPropertyName("genero")]
        public string Genero { get; set; }

        [JsonPropertyName("condicoesPagamentoId")]
        public int? IdCondicao { get; set; }

        [JsonPropertyName("condicaoPagamento")]
        public CondicaoPagamento oCondicaoPagamento { get; set; }

        [JsonIgnore]
        public string DescricaoCondicao { get; set; }

        [JsonPropertyName("dataNascimento")]
        public DateTime? DataNascimento { get; set; }
    }
}