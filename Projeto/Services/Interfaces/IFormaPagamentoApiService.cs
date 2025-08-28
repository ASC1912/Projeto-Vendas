using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Models;

namespace Projeto.Services.Interfaces
{
    public interface IFormaPagamentoApiService
    {
        Task<List<FormaPagamento>> GetFormasPagamentoAsync();
        Task<FormaPagamento> GetFormaPagamentoByIdAsync(int id);
        Task SaveFormaPagamentoAsync(FormaPagamento forma);
        Task DeleteFormaPagamentoAsync(int id);
    }
}
