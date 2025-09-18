using Projeto.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projeto.Services.Interfaces
{
    public interface IVeiculoApiService
    {
        Task<List<Veiculo>> GetVeiculosAsync();
        Task<Veiculo> GetVeiculoByIdAsync(int id);
        Task SaveVeiculoAsync(Veiculo veiculo);
        Task DeleteVeiculoAsync(int id);
    }
}