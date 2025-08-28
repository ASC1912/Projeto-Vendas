using System.Text.Json.Serialization;

namespace Projeto.Models
{
    public class Parcelamento
    {
        [JsonPropertyName("numeroParcela")]
        public int NumParcela { get; set; }

        [JsonPropertyName("prazoDias")]
        public int PrazoDias { get; set; }

        [JsonPropertyName("porcentagemValor")]
        public decimal Porcentagem { get; set; }

        [JsonIgnore] 
        public int CondicaoId { get; set; }

        [JsonPropertyName("formaPagamento")]
        public FormaPagamento FormaPagamento { get; set; }

        [JsonIgnore]
        public int FormaPagamentoId { get; set; }
    }
}