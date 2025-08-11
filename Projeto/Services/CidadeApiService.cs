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
    public class CidadeApiService : ICidadeApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7170/api/";

        public CidadeApiService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            _httpClient = new HttpClient(handler) { BaseAddress = new Uri(BaseUrl) };
        }

        public async Task<List<Cidade>> GetCidadesAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Cidade>>("Cidades");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao conectar com a API: " + ex.Message, "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Cidade>();
            }
        }

        public Task<Cidade> GetCidadeByIdAsync(int id)
        {
            return _httpClient.GetFromJsonAsync<Cidade>($"Cidades/{id}");
        }

        public async Task SaveCidadeAsync(Cidade cidade)
        {
            HttpResponseMessage response;
            if (cidade.Id == 0)
            {
                response = await _httpClient.PostAsJsonAsync("Cidades", cidade);
            }
            else
            {
                response = await _httpClient.PutAsJsonAsync($"Cidades/{cidade.Id}", cidade);
            }
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCidadeAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Cidades/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}