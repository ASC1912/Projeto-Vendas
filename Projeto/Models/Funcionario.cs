using System;
using System.Text.Json.Serialization;

namespace Projeto.Models
{
    public class Funcionario : Pessoa
    {
       
        [JsonPropertyName("funcionario")]
        public override string Nome { get; set; }

        [JsonPropertyName("apelido")]
        public string Apelido { get; set; }

        [JsonPropertyName("genero")]
        public string Genero { get; set; }

        [JsonPropertyName("rg")]
        public string Rg { get; set; }

        [JsonPropertyName("dataNascimento")]
        public DateTime? DataNascimento { get; set; }

        [JsonPropertyName("matricula")]
        public string Matricula { get; set; }

        [JsonPropertyName("cargo")]
        public string Cargo { get; set; }

        [JsonPropertyName("salario")]
        public decimal Salario { get; set; }

        [JsonPropertyName("dataAdmissao")]
        public DateTime? DataAdmissao { get; set; }

        [JsonPropertyName("dataDemissao")]
        public DateTime? DataDemissao { get; set; }
    }
}