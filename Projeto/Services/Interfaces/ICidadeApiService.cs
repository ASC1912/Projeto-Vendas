using Projeto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Services.Interfaces
{
    public interface ICidadeApiService
    {
        Task<List<Cidade>> GetCidadesAsync();
        Task<Cidade> GetCidadeByIdAsync(int id);
        Task SaveCidadeAsync(Cidade cidade);
        Task DeleteCidadeAsync(int id);
    }
}
