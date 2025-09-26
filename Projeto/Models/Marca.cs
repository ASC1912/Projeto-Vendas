using System;
using System.Text.Json.Serialization; 

namespace Projeto.Models
{
    public class Marca
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("marca")]
        public string NomeMarca { get; set; } 

        [JsonPropertyName("ativo")] 
        public bool Ativo { get; set; }

        [JsonPropertyName("dataCadastro")]
        public DateTime? DataCadastro { get; set; }

        [JsonPropertyName("dataAlteracao")] 
        public DateTime? DataAlteracao { get; set; }
    }
}