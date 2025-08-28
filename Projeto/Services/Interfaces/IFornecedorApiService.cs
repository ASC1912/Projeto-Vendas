using Projeto.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projeto.Services.Interfaces
{
    public interface IFornecedorApiService
    {
        Task<List<Fornecedor>> GetFornecedoresAsync();
        Task<Fornecedor> GetFornecedorByIdAsync(int id);
        Task SaveFornecedorAsync(Fornecedor fornecedor);
        Task DeleteFornecedorAsync(int id);
    }
}