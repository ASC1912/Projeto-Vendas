using System;

namespace Projeto.Models
{
    internal class Grupo
    {
        public int Id { get; set; }
        public string NomeGrupo { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }
}
