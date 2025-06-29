using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Models
{
    internal class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF_CNPJ { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Tipo { get; set; }
        public string CEP { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public int? NumeroEndereco { get; set; }
        public string Complemento { get; set; }
        public int? IdCidade { get; set; }
        public string NomeCidade { get; set; }
        public bool Ativo { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }
}
