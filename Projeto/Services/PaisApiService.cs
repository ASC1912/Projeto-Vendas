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
    public class PaisApiService : IPaisApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7170/api/";

        public PaisApiService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            _httpClient = new HttpClient(handler) { BaseAddress = new Uri(BaseUrl) };
        }

        public async Task<List<Pais>> GetPaisesAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Pais>>("Paises");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao conectar com a API: " + ex.Message, "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Pais>();
            }
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
    }
}
