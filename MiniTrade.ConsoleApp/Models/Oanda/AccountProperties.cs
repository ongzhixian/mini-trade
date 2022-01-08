using System.Text.Json.Serialization;

namespace MiniTrade.ConsoleApp.Models.Oanda;

internal class AccountProperties
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("mt4AccountID")]
    public int Mt4AccountID { get; set; }

    [JsonPropertyName("tags")]
    public IEnumerable<string> Tags { get; set; } = new List<string>();
    
}
