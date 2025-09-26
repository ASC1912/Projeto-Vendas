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
    public class GrupoApiService : IGrupoApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7170/api/";

        public GrupoApiService()
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

        public async Task<List<Grupo>> GetGruposAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Grupo>>("Grupos");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar grupos via API: " + ex.Message, "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Grupo>();
            }
        }

        public async Task<Grupo> GetGrupoByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Grupo>($"Grupos/{id}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar grupo na API: " + ex.Message, "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public async Task SaveGrupoAsync(Grupo grupo)
        {
            try
            {
                HttpResponseMessage response;
                if (grupo.Id == 0)
                {
                    response = await _httpClient.PostAsJsonAsync("Grupos", grupo);
                }
                else
                {
                    response = await _httpClient.PutAsJsonAsync($"Grupos/{grupo.Id}", grupo);
                }
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar grupo na API: " + ex.Message, "Erro de Operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        public async Task DeleteGrupoAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"Grupos/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir grupo na API: " + ex.Message, "Erro de Operação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
    }
}