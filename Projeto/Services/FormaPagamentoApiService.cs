using Projeto.Models;
using Projeto.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto.Services
{
    public class FormaPagamentoApiService : IFormaPagamentoApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7170/api/"; 

        public FormaPagamentoApiService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            _httpClient = new HttpClient(handler) { BaseAddress = new Uri(BaseUrl) };
        }

        public async Task<List<FormaPagamento>> GetFormasPagamentoAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<FormaPagamento>>("FormasPagamento");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao conectar com a API: " + ex.Message, "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<FormaPagamento>();
            }
        }
        public Task<FormaPagamento> GetFormaPagamentoByIdAsync(int id)
        {
            return _httpClient.GetFromJsonAsync<FormaPagamento>($"FormasPagamento/{id}");
        }

        public async Task SaveFormaPagamentoAsync(FormaPagamento forma)
        {
            HttpResponseMessage response;
            if (forma.Id == 0)
            {
                response = await _httpClient.PostAsJsonAsync("FormasPagamento", forma);
            }
            else
            {
                response = await _httpClient.PutAsJsonAsync($"FormasPagamento/{forma.Id}", forma);
            }
            response.EnsureSuccessStatusCode(); 
        }

        public async Task DeleteFormaPagamentoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"FormasPagamento/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
