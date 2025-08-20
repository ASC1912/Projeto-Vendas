using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Models;

namespace Projeto.Services.Interfaces
{
    public interface IFuncionarioApiService
    {
        Task<List<Funcionario>> GetFuncionariosAsync();
        Task<Funcionario> GetFuncionarioByIdAsync(int id);
        Task SaveFuncionarioAsync(Funcionario funcionario);
        Task DeleteFuncionarioAsync(int id);
    }
}
