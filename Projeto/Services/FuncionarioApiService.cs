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
    public class FuncionarioApiService : IFuncionarioApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7170/api/";

        public FuncionarioApiService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            _httpClient = new HttpClient(handler) { BaseAddress = new Uri(BaseUrl) };
        }

        public async Task<List<Funcionario>> GetFuncionariosAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Funcionario>>("Funcionarios");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao conectar com a API: " + ex.Message, "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Funcionario>();
            }
        }

        public Task<Funcionario> GetFuncionarioByIdAsync(int id)
        {
            return _httpClient.GetFromJsonAsync<Funcionario>($"Funcionarios/{id}");
        }

        public async Task SaveFuncionarioAsync(Funcionario funcionario)
        {
            HttpResponseMessage response;
            if (funcionario.Id == 0)
            {
                response = await _httpClient.PostAsJsonAsync("Funcionarios", funcionario);
            }
            else
            {
                response = await _httpClient.PutAsJsonAsync($"Funcionarios/{funcionario.Id}", funcionario);
            }
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteFuncionarioAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Funcionarios/{id}");
            response.EnsureSuccessStatusCode();
        }

    }
}
