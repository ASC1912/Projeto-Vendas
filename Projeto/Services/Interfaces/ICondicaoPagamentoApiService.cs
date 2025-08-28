using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Models;

namespace Projeto.Services.Interfaces
{
    internal interface ICondicaoPagamentoApiService
    {
        Task<List<CondicaoPagamento>> GetCondicoesPagamentoAsync();
        Task<CondicaoPagamento> GetCondicaoPagamentoByIdAsync(int id);
        Task SaveCondicaoPagamentoAsync(CondicaoPagamento condicao);
        Task DeleteCondicaoPagamentoAsync(int id);
    }
}
