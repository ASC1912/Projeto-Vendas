using System;

namespace Projeto.Models
{
    public class Mesa
    {
        public int NumeroMesa { get; set; }
        public int QuantidadeCadeiras { get; set; }
        public string Localizacao { get; set; }
        public char StatusMesa { get; set; }
        public bool Ativo { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }
}