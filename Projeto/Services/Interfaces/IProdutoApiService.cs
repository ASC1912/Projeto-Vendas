using Projeto.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projeto.Services.Interfaces
{
    public interface IProdutoApiService
    {
        Task<List<Produto>> GetProdutosAsync();
        Task<Produto> GetProdutoByIdAsync(int id);
        Task SaveProdutoAsync(Produto produto);
        Task DeleteProdutoAsync(int id);
    }
}