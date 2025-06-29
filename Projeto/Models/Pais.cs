using System;

namespace Projeto.Models
{
    internal class Pais
    {
        public int Id { get; set; }
        public string NomePais { get; set; }
        public bool Ativo { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }
}
