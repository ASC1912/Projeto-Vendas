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


    }
}
