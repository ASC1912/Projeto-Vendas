using Projeto.DAO;
using System;
using System.Text.Json.Serialization;

namespace Projeto.Models
{
    public class Cidade
    {
        public int Id { get; set; }

        [JsonPropertyName("cidade")]
        public string NomeCidade { get; set; }
        public int EstadoId { get; set; }
        public string DDD { get; set; } 
        public bool Ativo { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }

        [JsonPropertyName("estado")]
        public Estado oEstado { get; set; }
        public string EstadoNome => oEstado?.NomeEstado;
    }
}