using System;

namespace Projeto.Models
{
    internal class Estado
    {
        public int Id { get; set; }
        public string NomeEstado { get; set; }
        public string UF { get; set; }
        public int PaisId { get; set; }
        public string PaisNome { get; set; }
        public bool Ativo { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }
}
