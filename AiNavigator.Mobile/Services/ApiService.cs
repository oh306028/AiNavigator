using AiNavigator.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http; // Dodaj to using

namespace AiNavigator.Mobile.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            var handler = new HttpClientHandler();

            handler.ServerCertificateCustomValidationCallback =
                (message, cert, chain, errors) => true;

            _httpClient = new HttpClient(handler)
            {

                BaseAddress = new Uri("https://10.0.2.2:7233/")
            };
        }

        public async Task<PromptDetails> GetModelsAsync(Category category)
        {
            var form = new PromptForm { Category = category };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/models", form);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<PromptDetails>();
                return result!;
            }
            catch (HttpRequestException ex)
            {

                Console.WriteLine($"Błąd HTTP Request: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ogólny błąd: {ex.Message}");
                throw;
            }
        }
    }
}