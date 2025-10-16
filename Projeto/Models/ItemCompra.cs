namespace Projeto.Models
{
    public class ItemCompra
    {
        public string CompraModelo { get; set; }
        public string CompraSerie { get; set; }
        public int CompraNumeroNota { get; set; }
        public int CompraFornecedorId { get; set; }

        public int ProdutoId { get; set; }

        public decimal Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotalItem { get; set; }
        public string NomeProduto { get; set; }
        public string NomeUnidadeMedida { get; set; } 
    }
}