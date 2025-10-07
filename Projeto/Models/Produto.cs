using System;
using System.Text.Json.Serialization;

namespace Projeto.Models
{
    public class Produto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("produto")]
        public string NomeProduto { get; set; }

        [JsonPropertyName("descricao")]
        public string Descricao { get; set; }

        [JsonPropertyName("precoCusto")]
        public decimal PrecoCusto { get; set; } 

        [JsonPropertyName("precoVenda")]
        public decimal PrecoVenda { get; set; } 

        [JsonPropertyName("porcentagemLucro")]
        public decimal PorcentagemLucro { get; set; } 

        [JsonPropertyName("estoque")]
        public int Estoque { get; set; }

        [JsonPropertyName("marcasId")]
        public int? IdMarca { get; set; }

        [JsonPropertyName("grupoId")]
        public int? GrupoId { get; set; }

        [JsonPropertyName("unidadeMedidaId")]
        public int? UnidadeMedidaId { get; set; }

        [JsonPropertyName("ativo")]
        public bool Ativo { get; set; }

        [JsonPropertyName("dataCadastro")]
        public DateTime? DataCadastro { get; set; }

        [JsonPropertyName("dataAlteracao")]
        public DateTime? DataAlteracao { get; set; }

        [JsonIgnore]
        public string NomeMarca { get; set; }

        [JsonIgnore]
        public string NomeGrupo { get; set; }

        [JsonIgnore]
        public string NomeUnidadeMedida { get; set; }
    }
}