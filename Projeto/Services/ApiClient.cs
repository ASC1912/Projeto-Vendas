using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Projeto.Models;

namespace Projeto.Services
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7170/api/";

        public ApiClient()
        {
            // Esta configuração permite que o HttpClient ignore erros de certificado SSL 
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            _httpClient = new HttpClient(handler) { BaseAddress = new Uri(BaseUrl) };
        }

        // --- MÉTODOS PARA PAÍS ---

        public Task<List<Pais>> GetPaisesAsync()
        {
            return _httpClient.GetFromJsonAsync<List<Pais>>("Paises");
        }

        public Task<Pais> GetPaisByIdAsync(int id)
        {
            return _httpClient.GetFromJsonAsync<Pais>($"Paises/{id}");
        }

        public async Task SavePaisAsync(Pais pais)
        {
            HttpResponseMessage response;
            if (pais.Id == 0)
            {
                response = await _httpClient.PostAsJsonAsync("Paises", pais);
            }
            else
            {
                response = await _httpClient.PutAsJsonAsync($"Paises/{pais.Id}", pais);
            }
            response.EnsureSuccessStatusCode();
        }

        public async Task DeletePaisAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Paises/{id}");
            response.EnsureSuccessStatusCode();
        }

        // --- FUTUROS MÉTODOS 
    }
}