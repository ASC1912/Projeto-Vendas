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
    public class MarcaApiService : IMarcaApiService
    {
        private readonly HttpClient _httpClient;

        private const string BaseUrl = "https://localhost:7170/";
        private const string Endpoint = "api/Marcas";

        public MarcaApiService()
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

        public async Task<List<Marca>> GetMarcasAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Marca>>(Endpoint);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao conectar com a API: " + ex.Message, "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Marca>();
            }
        }

        public async Task<Marca> GetMarcaByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Marca>($"{Endpoint}/{id}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar marca na API: " + ex.Message, "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public async Task SaveMarcaAsync(Marca marca)
        {
            try
            {
                HttpResponseMessage response;
                if (marca.Id == 0)
                {
                    response = await _httpClient.PostAsJsonAsync(Endpoint, marca);
                }
                else
                {
                    response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{marca.Id}", marca);
                }
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar marca na API: " + ex.Message, "Erro de Operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async Task DeleteMarcaAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{Endpoint}/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir marca na API: " + ex.Message, "Erro de Operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}