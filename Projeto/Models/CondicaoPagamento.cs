using System;
using System.Collections.Generic;
using System.Text.Json.Serialization; // Adicione este using

namespace Projeto.Models
{
    public class CondicaoPagamento
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("descricao")]
        public string Descricao { get; set; }

        [JsonPropertyName("quantidadeParcelas")]
        public int QtdParcelas { get; set; }

        [JsonPropertyName("juros")]
        public decimal Juros { get; set; }

        [JsonPropertyName("multa")]
        public decimal Multa { get; set; }

        [JsonPropertyName("desconto")]
        public decimal Desconto { get; set; }

        [JsonPropertyName("ativo")]
        public bool Ativo { get; set; }

        [JsonIgnore]
        public DateTime? DataCadastro { get; set; }
        [JsonIgnore]
        public DateTime? DataAlteracao { get; set; }

        [JsonPropertyName("parcelamentos")]
        public List<Parcelamento> Parcelas { get; set; }
    }
}