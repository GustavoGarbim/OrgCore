using OrgCore.Domain.Interfaces;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;

namespace OrgCore.Infrastructure.ExternalServices
{
    public class InvertextoResponse
    {
        public bool Valid { get; set; }
    }

    public class InvertextoValidator : IDocValidator
    {
        private readonly HttpClient _httpClient;
        private readonly string _token;

        public InvertextoValidator(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _token = configuration["ExternalServices:InvertextoToken"];

            if(string.IsNullOrEmpty(_token))
            {
                throw new ArgumentException("Invertexto token is not configured.");
            }
        }

        public async Task<bool> ValidarEmpresa(string cnpj)
        {
            var cnpjLimpo = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            var url = $"https://api.invertexto.com/v1/validator?token={_token}&value={cnpjLimpo}&type=cnpj";

            try
            {
                var response = await _httpClient.GetFromJsonAsync<InvertextoResponse>(url);
                return response?.Valid ?? false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ValidarPessoa(string cpf)
        {
            var cpfLimpo = cpf.Replace(".", "").Replace("-", "");
            var url = $"https://api.invertexto.com/v1/validator?token={_token}&value={cpfLimpo}&type=cpf";
            try
            {
                var response = await _httpClient.GetFromJsonAsync<InvertextoResponse>(url);
                return response?.Valid ?? false;
            }
            catch
            {
                return false;
            }
        }
    }
}
