using System.Net.Http;
using System.Text;
using System.Text.Json;
using AiNavigator.Api.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace AiNavigator.Api.Services
{
    public class ModelsService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly IDistributedCache _cache;

        public ModelsService(HttpClient httpClient, IConfiguration config, IDistributedCache cache)
        {
            _httpClient = httpClient;
            _config = config;
            _cache = cache;
        }

        public async Task<PromptDetails> GetModelsAsync(PromptForm form)
        {
            var cacheKey = $"models:{form.Category}";

            var cached = await _cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cached))            
                return JsonSerializer.Deserialize<PromptDetails>(cached)!;
            

            var result = await FetchDataFromApi(form);

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24)
            };

            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(result), options);

            return result;
        }



        private async Task<PromptDetails> FetchDataFromApi(PromptForm form)  
        {
            var apiKey = _config["OpenAI:ApiKey"];
            var url = "https://api.openai.com/v1/chat/completions";

            #region PROMPT
            var prompt = $@"
                Jesteś ekspertem AI. Podaj listę TOP 5 najlepszych modeli AI dla kategorii: {form.Category}.
                Przeszukaj mozliwie jak najwiecej zrodel na ten temat, stworz porownania i wybierz najlepsze wyniki w podanej kategorii.
                Przedstaw dane w taki sposób, by użytkownik ktory nie ma styczności z technicznymi zagadnieniami mógł odpowiednio zrozumieć cał kontekst odpowiedzi.
                Przeanalizuj swoją odpowiedź i dostosuj ją by była jak najbardziej optymalna.
                Odpowiedz *tylko* czystym JSON-em, bez żadnych komentarzy ani bloków kodu.
                Prosze by kategorie zostaly zwracane tylko jako te dopasowane do enuma:
    public enum Category
    {{
        [Description(""Generowanie tekstu"")]
        Text,

        [Description(""Generowanie video"")]
        Video,

        [Description(""Programowanie"")]
        Development,

        [Description(""Generowanie grafiki"")]
        Graphics,

        [Description(""Agenci AI"")]
        Agents
    }}
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

            #endregion

            #region API CONFIG
            var requestJson = JsonSerializer.Serialize(requestBody);
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, url);
            httpRequest.Headers.Add("Authorization", $"Bearer {apiKey}");
            httpRequest.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");
            #endregion

            var response = await _httpClient.SendAsync(httpRequest);
            var responseString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)          
                throw new Exception($"OpenAI error: {response.StatusCode} - {responseString}");
            

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
