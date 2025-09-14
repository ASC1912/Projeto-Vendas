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
    public class FornecedorApiService : IFornecedorApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7170/api/"; 

        public FornecedorApiService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            _httpClient = new HttpClient(handler) { BaseAddress = new Uri(BaseUrl) };
        }

        public async Task<List<Fornecedor>> GetFornecedoresAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Fornecedor>>("Fornecedores");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar fornecedores da API: " + ex.Message, "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Fornecedor>();
            }
        }

        public Task<Fornecedor> GetFornecedorByIdAsync(int id)
        {
            return _httpClient.GetFromJsonAsync<Fornecedor>($"Fornecedores/{id}");
        }

        public async Task SaveFornecedorAsync(Fornecedor fornecedor)
        {
            HttpResponseMessage response;
            if (fornecedor.Id == 0)
            {
                response = await _httpClient.PostAsJsonAsync("Fornecedores", fornecedor);
            }
            else
            {
                response = await _httpClient.PutAsJsonAsync($"Fornecedores/{fornecedor.Id}", fornecedor);
            }

            if (!response.IsSuccessStatusCode)
            {
                string erro = await response.Content.ReadAsStringAsync();
                MessageBox.Show(
                    $"A API retornou um erro ao salvar o Fornecedor:\n\n" +
                    $"Código: {(int)response.StatusCode} ({response.ReasonPhrase})\n\n" +
                    $"Detalhes: {erro}",
                    "Erro da API",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteFornecedorAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Fornecedores/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}