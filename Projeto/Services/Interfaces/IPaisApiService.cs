using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Models;

namespace Projeto.Services.Interfaces
{
    public interface IPaisApiService
    {
        Task<List<Pais>> GetPaisesAsync();
        Task<Pais> GetPaisByIdAsync(int id);
        Task SavePaisAsync(Pais pais);
        Task DeletePaisAsync(int id);

    }
}
