using System.Text;
using API.Contracts;
namespace API.Services;

public class WebhookService : IWebhookService
{
    private readonly HttpClient _httpClient;

    public WebhookService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task SendWebhookAsync(string url, object payload)
    {
        var content = new StringContent(
            System.Text.Json.JsonSerializer.Serialize(payload),
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient.PostAsync(url, content);
        response.EnsureSuccessStatusCode();
    }
}