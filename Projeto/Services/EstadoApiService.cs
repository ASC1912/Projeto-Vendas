using Projeto.Models;
using Projeto.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http.Json;

namespace Projeto.Services
{
    public class EstadoApiService : IEstadoApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7170/api/";

        public EstadoApiService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            _httpClient = new HttpClient(handler) { BaseAddress = new Uri(BaseUrl) };
        }

        public async Task<List<Estado>> GetEstadosAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Estado>>("Estados");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao conectar com a API: " + ex.Message, "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Estado>();
            }
        }

        public Task<Estado> GetEstadoByIdAsync(int id)
        {
            return _httpClient.GetFromJsonAsync<Estado>($"Estados/{id}");
        }

        public async Task SaveEstadoAsync(Estado estado)
        {
            HttpResponseMessage response;
            if (estado.Id == 0)
            {
                response = await _httpClient.PostAsJsonAsync("Estados", estado);
            }
            else
            {
                response = await _httpClient.PutAsJsonAsync($"Estados/{estado.Id}", estado);
            }
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteEstadoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Estados/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
