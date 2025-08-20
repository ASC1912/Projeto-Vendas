using System;
using System.Text.Json.Serialization;

namespace Projeto.Models
{
    public class Pessoa
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        public virtual string Nome { get; set; }

        [JsonPropertyName("cpfCnpj")]
        public string CPF_CNPJ { get; set; }

        [JsonPropertyName("telefone")]
        public string Telefone { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("tipo")]
        public string Tipo { get; set; }

        [JsonPropertyName("cep")]
        public string CEP { get; set; }

        [JsonPropertyName("endereco")]
        public string Endereco { get; set; }

        [JsonPropertyName("bairro")]
        public string Bairro { get; set; }

        [JsonPropertyName("numero")]
        public int? NumeroEndereco { get; set; }

        [JsonPropertyName("complemento")]
        public string Complemento { get; set; }

        [JsonPropertyName("cidadeId")]
        public int? CidadeId { get; set; }

        [JsonPropertyName("cidade")]
        public Cidade oCidade { get; set; }

        [JsonIgnore]
        public string NomeCidade => oCidade?.NomeCidade;

        [JsonPropertyName("ativo")]
        public bool Ativo { get; set; }

        [JsonPropertyName("dataCadastro")]
        public DateTime? DataCadastro { get; set; }

        [JsonPropertyName("dataAlteracao")]
        public DateTime? DataAlteracao { get; set; }
    }
}