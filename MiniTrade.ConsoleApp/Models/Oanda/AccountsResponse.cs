using System.Text.Json.Serialization;

namespace MiniTrade.ConsoleApp.Models.Oanda;

internal class AccountsResponse
{
    [JsonPropertyName("accounts")]
    public IEnumerable<AccountProperties>? Accounts { get; set; }
}
