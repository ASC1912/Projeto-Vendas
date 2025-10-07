using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Projeto.Models
{
    public class UnidadeMedida
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("unidade")]
        public string NomeUnidade { get; set; }

        [JsonPropertyName("sigla")]
        public string Sigla { get; set; }

        [JsonPropertyName("ativo")]
        public bool Ativo { get; set; }

        [JsonPropertyName("dataCadastro")]
        public DateTime? DataCadastro { get; set; }

        [JsonPropertyName("dataAlteracao")]
        public DateTime? DataAlteracao { get; set; }

    }
}
