using Projeto.Models;
using Projeto.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto.Services
{
    public class TransportadoraApiService : ITransportadoraApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7170/api/";

        public TransportadoraApiService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            _httpClient = new HttpClient(handler) { BaseAddress = new Uri(BaseUrl) };
        }

        public async Task<List<Transportadora>> GetTransportadorasAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Transportadora>>("Transportadoras");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao conectar com a API: " + ex.Message, "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Transportadora>(); 
            }
        }

        public Task<Transportadora> GetTransportadoraByIdAsync(int id)
        {
            return _httpClient.GetFromJsonAsync<Transportadora>($"Transportadoras/{id}");
        }

        public async Task SaveTransportadoraAsync(Transportadora transportadora)
        {
            HttpResponseMessage response;
            if (transportadora.Id == 0)
            {
                response = await _httpClient.PostAsJsonAsync("Transportadoras", transportadora);
            }
            else
            {
                response = await _httpClient.PutAsJsonAsync($"Transportadoras/{transportadora.Id}", transportadora);
            }
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteTransportadoraAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Transportadoras/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}