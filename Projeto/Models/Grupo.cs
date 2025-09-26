using System;
using System.Text.Json.Serialization;

namespace Projeto.Models
{
    public class Grupo
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("grupo")] 
        public string NomeGrupo { get; set; }
        [JsonPropertyName("descricao")]
        public string Descricao { get; set; }

        [JsonPropertyName("ativo")]
        public bool Ativo { get; set; }

        [JsonPropertyName("dataCadastro")]
        public DateTime? DataCadastro { get; set; }

        [JsonPropertyName("dataAlteracao")]
        public DateTime? DataAlteracao { get; set; }
    }
}