using System;

namespace Projeto.Models
{
    public class Cidade
    {
        public int Id { get; set; }
        public string NomeCidade { get; set; }
        public int EstadoId { get; set; }
        public string EstadoNome { get; set; }
        public bool Ativo { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }
}
