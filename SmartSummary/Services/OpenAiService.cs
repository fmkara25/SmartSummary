using System.Net.Http.Json;
using System.Text.Json;

namespace SmartSummary.Services
{
    public interface IOpenAiService
    {
        Task<string> SummarizeAsync(string input, int sentenceCount = 3, string? language = null);
    }

    public class OpenAiService : IOpenAiService
    {
        private readonly IHttpClientFactory _httpFactory;
        private readonly IConfiguration _cfg;

        public OpenAiService(IHttpClientFactory httpFactory, IConfiguration cfg)
        {
            _httpFactory = httpFactory;
            _cfg = cfg;
        }

        public async Task<string> SummarizeAsync(string input, int sentenceCount = 3, string? language = null)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            var model = _cfg["OpenAI:Model"] ?? "gpt-4o-mini";
            var client = _httpFactory.CreateClient("OpenAI");

            var body = new
            {
                model = model,
                messages = new object[]
                {
                    new { role = "system", content = "You are a concise summarizer. Keep the core meaning, avoid fluff." },
                    new { role = "user", content =
                        $"Summarize the following text in {sentenceCount} sentences." +
                        (string.IsNullOrWhiteSpace(language) ? "" : $" Answer in {language}.") +
                        "\n\n---\n" + input
                    }
                },
                temperature = 0.2
            };

            using var req = new HttpRequestMessage(HttpMethod.Post, "v1/chat/completions")
            {
                Content = JsonContent.Create(body)
            };

            var res = await client.SendAsync(req);

            if (!res.IsSuccessStatusCode)
            {
                var errorText = await res.Content.ReadAsStringAsync();
                throw new Exception($"OpenAI API error {(int)res.StatusCode} {res.StatusCode}: {errorText}");
            }

            using var stream = await res.Content.ReadAsStreamAsync();
            using var doc = await JsonDocument.ParseAsync(stream);

            var content = doc.RootElement.GetProperty("choices")[0]
                                .GetProperty("message")
                                .GetProperty("content")
                                .GetString();

            return content ?? string.Empty;
        }
    }
}
