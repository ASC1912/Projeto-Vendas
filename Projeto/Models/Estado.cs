using System;
using System.Text.Json.Serialization;

namespace Projeto.Models
{
    public class Estado
    {
        public int Id { get; set; }

        [JsonPropertyName("estado")]
        public string NomeEstado { get; set; }
        public string UF { get; set; }
        public int PaisId { get; set; }
        public bool Ativo { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }

        [JsonPropertyName("pais")]
        public Pais oPais { get; set; }
        public string PaisNome => oPais?.NomePais;

    }
}
