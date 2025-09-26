using Projeto.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projeto.Services.Interfaces
{
    public interface IMarcaApiService
    {
        Task<List<Marca>> GetMarcasAsync();
        Task<Marca> GetMarcaByIdAsync(int id);
        Task SaveMarcaAsync(Marca marca);
        Task DeleteMarcaAsync(int id);
    }
}