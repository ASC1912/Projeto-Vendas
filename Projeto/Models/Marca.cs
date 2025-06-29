using System;

namespace Projeto.Models
{
    internal class Marca
    {
        public int Id { get; set; }
        public string NomeMarca { get; set; }  
        public bool Ativo { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }
}
