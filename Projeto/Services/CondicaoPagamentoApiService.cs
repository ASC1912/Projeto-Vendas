using Projeto.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Projeto.Models;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Windows.Forms;

namespace Projeto.Services
{
    public class CondicaoPagamentoApiService : ICondicaoPagamentoApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7170/api/";

        public CondicaoPagamentoApiService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, error) => true
            };
            _httpClient = new HttpClient(handler) { BaseAddress = new Uri(BaseUrl) };
        }

        public async Task<List<CondicaoPagamento>> GetCondicoesPagamentoAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<CondicaoPagamento>>("CondicoesPagamento");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar condições de pagamento da API: " + ex.Message, "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<CondicaoPagamento>();
            }
        }

        public Task<CondicaoPagamento> GetCondicaoPagamentoByIdAsync(int id)
        {
            return _httpClient.GetFromJsonAsync<CondicaoPagamento>($"CondicoesPagamento/{id}");
        }


        public async Task SaveCondicaoPagamentoAsync(CondicaoPagamento condicao)
        {
            HttpResponseMessage response;
            if (condicao.Id == 0)
            {
                response = await _httpClient.PostAsJsonAsync("CondicoesPagamento", condicao);
            }
            else
            {
                response = await _httpClient.PutAsJsonAsync($"CondicoesPagamento/{condicao.Id}", condicao);
            }
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCondicaoPagamentoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"CondicoesPagmento/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
