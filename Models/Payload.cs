using System.Text.Json.Serialization;

namespace API.Models;

public class Payload
{
    [JsonPropertyName("channel_id")]
    public string ChannelId { get; set; } = "";

    [JsonPropertyName("return_url")]
    public string ReturnUrl { get; set; } = "";

    [JsonPropertyName("settings")]
    public List<Setting>? Settings { get; set; }
}