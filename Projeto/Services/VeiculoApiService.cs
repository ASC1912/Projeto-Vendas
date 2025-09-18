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
    public class VeiculoApiService : IVeiculoApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7170/api/";

        public VeiculoApiService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            _httpClient = new HttpClient(handler) { BaseAddress = new Uri(BaseUrl) };
        }

        public async Task<List<Veiculo>> GetVeiculosAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Veiculo>>("Veiculos");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao conectar com a API: " + ex.Message, "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Veiculo>(); 
            }
        }

        public Task<Veiculo> GetVeiculoByIdAsync(int id)
        {
            return _httpClient.GetFromJsonAsync<Veiculo>($"Veiculos/{id}");
        }

        public async Task SaveVeiculoAsync(Veiculo veiculo)
        {
            HttpResponseMessage response;
            if (veiculo.Id == 0)
            {
                response = await _httpClient.PostAsJsonAsync("Veiculos", veiculo);
            }
            else
            {
                response = await _httpClient.PutAsJsonAsync($"Veiculos/{veiculo.Id}", veiculo);
            }
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteVeiculoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Veiculos/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}