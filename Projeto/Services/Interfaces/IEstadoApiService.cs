using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Models;

namespace Projeto.Services.Interfaces
{
    public interface IEstadoApiService
    {
        Task<List<Estado>> GetEstadosAsync();
        Task<Estado> GetEstadoByIdAsync(int id);
        Task SaveEstadoAsync(Estado estado);
        Task DeleteEstadoAsync(int id);
    }
}
