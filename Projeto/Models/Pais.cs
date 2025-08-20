using System;
using System.Text.Json.Serialization;

namespace Projeto.Models
{
    public class Pais
    {
        public int Id { get; set; }

        [JsonPropertyName("pais")]
        public string NomePais { get; set; }
        public bool Ativo { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }
}
