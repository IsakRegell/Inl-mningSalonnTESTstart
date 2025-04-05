using System.Text.Json;
using InlämningSalonn.Interfaces;

namespace InlämningSalonn.Services
{
    public class ChatGTPServices : IChatGPT
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ChatGTPServices(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> GetChatGPTResponse(string userMessage, string language)
        {
            var apiKey = _configuration["OpenAI:ApiKey"];

            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new { role = "system", content = $"Svara alltid på {language}." },
                    new { role = "user", content = userMessage },
                }
            };
            var response = await _httpClient.PostAsJsonAsync(
                "https://api.openai.com/v1/chat/completions", requestBody);

            if (!response.IsSuccessStatusCode)
            {
                return $"Error: {response.StatusCode}";
            }

            var json = await response.Content.ReadFromJsonAsync<JsonElement>();

            var message = json
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();
            return message;

        }
    }
}
