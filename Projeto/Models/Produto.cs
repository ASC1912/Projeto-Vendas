using System;

namespace Projeto.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string NomeProduto { get; set; }      
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
        public int? IdMarca { get; set; }
        public string NomeMarca { get; set; }
        public int? GrupoId { get; set; }             
        public string NomeGrupo { get; set; }
        public bool Ativo { get; set; }               
        public DateTime? DataCadastro { get; set; }   
        public DateTime? DataAlteracao { get; set; }
    }
}
