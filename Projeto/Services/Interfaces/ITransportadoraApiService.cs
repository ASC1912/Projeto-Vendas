using Projeto.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projeto.Services.Interfaces
{
    public interface ITransportadoraApiService
    {
        Task<List<Transportadora>> GetTransportadorasAsync();
        Task<Transportadora> GetTransportadoraByIdAsync(int id);
        Task SaveTransportadoraAsync(Transportadora transportadora);
        Task DeleteTransportadoraAsync(int id);
    }
}