namespace API.Contracts;

public interface IWebhookService
{
    Task SendWebhookAsync(string url, object payload);
}
