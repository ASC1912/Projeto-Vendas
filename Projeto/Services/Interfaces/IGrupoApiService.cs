using Projeto.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Projeto.Services.Interfaces
{
    public interface IGrupoApiService
    {
        Task<List<Grupo>> GetGruposAsync();
        Task<Grupo> GetGrupoByIdAsync(int id);
        Task SaveGrupoAsync(Grupo grupo);
        Task DeleteGrupoAsync(int id);
    }
}