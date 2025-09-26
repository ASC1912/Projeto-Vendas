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
    public class ProdutoApiService : IProdutoApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7170/api/";

        public ProdutoApiService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        public async Task<List<Produto>> GetProdutosAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Produto>>("Produtos");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar produtos via API: " + ex.Message, "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Produto>();
            }
        }

        public async Task<Produto> GetProdutoByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Produto>($"Produtos/{id}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar produto na API: " + ex.Message, "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public async Task SaveProdutoAsync(Produto produto)
        {
            try
            {
                HttpResponseMessage response;
                if (produto.Id == 0)
                {
                    response = await _httpClient.PostAsJsonAsync("Produtos", produto);
                }
                else
                {
                    response = await _httpClient.PutAsJsonAsync($"Produtos/{produto.Id}", produto);
                }
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar produto na API: " + ex.Message, "Erro de Operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        public async Task DeleteProdutoAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"Produtos/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir produto na API: " + ex.Message, "Erro de Operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
    }
}