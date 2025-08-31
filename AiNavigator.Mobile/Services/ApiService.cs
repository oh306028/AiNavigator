using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using AiNavigator.Mobile.Models;

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

            var baseAddress = DeviceInfo.Platform == DevicePlatform.WinUI ? "https://localhost:7233/" : "https://10.0.2.2:7233/";

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(baseAddress)
            };
        }

        public async Task<ApiResult> GetModelsAsync(Category category)
        {
            var form = new PromptForm { Category = category };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/models", form);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<ApiResult>();
                return result!;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Błąd HTTP Request: {ex.StatusCode} - {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ogólny błąd w ApiService: {ex.Message}");
                throw;
            }
        }

        public async Task<List<ApiGroupResult>> GetHistoryAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync("api/models/history"); 
                var history = JsonSerializer.Deserialize<List<ApiGroupResult>>(response);
                return history ?? new List<ApiGroupResult>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetHistoryAsync Error: {ex}");
                return new List<ApiGroupResult>();
            }
        }
    }   
}