using System.Text.Json.Serialization;

namespace EventPlanner.Mobile.Models;

public class MeResponse
{
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("isAdmin")]
    public bool IsAdmin { get; set; }
}