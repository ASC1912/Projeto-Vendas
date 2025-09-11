using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Models
{
    public class Compra
    {
        public string modelo { get; set; }
        public string serie { get; set; }
        public int numero { get; set; }
        public int FornecedorId { get; set; }
        public Fornecedor oFornecedor { get; set; }
        public string NomeFornecedor { get; set; }
        public Produto oProduto { get; set; }
        public string NomeProduto { get; set; }
        public int qtdProduto { get; set; }
        public float valorUnit { get; set; }
        public float valorFrete { get; set; }
        public float seguro { get; set; }
        public float despesas { get; set; }
        public CondicaoPagamento aCondPgto { get; set; }
        public string NomeCondPgto { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }

    }
}
