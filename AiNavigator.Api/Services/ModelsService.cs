using System.Net.Http;
using System.Text;
using System.Text.Json;
using AiNavigator.Api.Models;

namespace AiNavigator.Api.Services
{
    public class ModelsService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public ModelsService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<PromptDetails> GetModelsAsync(PromptForm form)
        {
            var apiKey = _config["OpenAI:ApiKey"];
            var url = "https://api.openai.com/v1/chat/completions";

            var prompt = $@"
                Jesteś ekspertem AI. Podaj listę TOP 5 najlepszych modeli AI dla kategorii: {form.Category}.
                Przeszukaj mozliwie jak najwiecej zrodel na ten temat, stworz porownania i wybierz najlepsze wyniki w podanej kategorii.
                Przedstaw dane w taki sposób, by użytkownik ktory nie ma styczności z technicznymi zagadnieniami mógł odpowiednio zrozumieć cał kontekst odpowiedzi.
                Przeanalizuj swoją odpowiedź i dostosuj ją by była jak najbardziej optymalna.
                Odpowiedź zwróć wyłącznie jako poprawny JSON w formacie:

                {{
                  ""queryDate"": ""{{dzisiejsza data w formacie yyyy-MM-dd}}"",
                  ""category"": ""{form.Category}"",
                  ""generalSummary"": ""{{krótkie podsumowanie trendów w tej kategorii}}"",
                  ""topModels"": [
                    {{
                      ""name"": ""string"",
                      ""shortDescription"": ""string"",
                      ""link"": ""string"",
                      ""pros"": [""string"", ""string""],
                      ""cons"": [""string"", ""string""],
                      ""rank"": number
                    }}
                  ]
                }}";

            var requestBody = new
            {
                model = "gpt-4o-mini",
                messages = new[]
                {
                    new { role = "system", content = "Jesteś ekspertem AI." },
                    new { role = "user", content = prompt }
                },
                temperature = 0.2
            };

            var requestJson = JsonSerializer.Serialize(requestBody);
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, url);
            httpRequest.Headers.Add("Authorization", $"Bearer {apiKey}");
            httpRequest.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(httpRequest);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(responseString);
            var content = doc.RootElement
                             .GetProperty("choices")[0]
                             .GetProperty("message")
                             .GetProperty("content")
                             .GetString();

            if (string.IsNullOrWhiteSpace(content))
                throw new Exception("Brak odpowiedzi od modelu");

            var result = JsonSerializer.Deserialize<PromptDetails>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result ?? new PromptDetails();
        }
    }
}
