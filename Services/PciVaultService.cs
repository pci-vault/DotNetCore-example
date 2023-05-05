using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using DotNetExample.Models;

namespace DotNetExample.Services
{
    public class ApiConfig
    {
        public string Username { set; protected internal get; }
        public string Password { set; protected internal get; }
        public string Key { set; protected internal get; }
        public string Passphrase { set; protected internal get; }
    }

    public class PciVaultService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiConfig _config;

        public PciVaultService(ApiConfig config)
        {
            _httpClient = new HttpClient();
            _config = config;

            var credentials = Convert.ToBase64String(
                Encoding.ASCII.GetBytes($"{_config.Username}:{_config.Password}")
            );
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic", credentials
            );
        }

        public async Task<CreditCardForm> GetCreditCardForm()
        {
            var uri = $"https://api-stage.pcivault.io/v1/capture?user={_config.Key}&passphrase={_config.Passphrase}";
            var response = await _httpClient.PostAsync(uri, new StringContent(""));
            response.EnsureSuccessStatusCode();

            var ccForm =  await response.Content.ReadFromJsonAsync<CreditCardForm>();
            return ccForm;
        }
    }
}