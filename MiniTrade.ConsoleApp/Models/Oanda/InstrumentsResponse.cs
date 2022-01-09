using System.Text.Json.Serialization;

namespace MiniTrade.ConsoleApp.Models.Oanda;

internal class InstrumentsResponse
{
    [JsonPropertyName("instruments")]
    public IEnumerable<Instrument> Instruments { get; set; }

    [JsonPropertyName("lastTransactionID")]
    public string LastTransactionID { get; set; } = string.Empty;
}


public class Instrument
{
    [JsonPropertyName("name")]
    public string name { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("displayName")]
    public string displayName { get; set; } = string.Empty;


    [JsonPropertyName("pipLocation")]
    public int pipLocation { get; set; }

    [JsonPropertyName("displayPrecision")]
    public int displayPrecision { get; set; }

    [JsonPropertyName("tradeUnitsPrecision")]
    public int tradeUnitsPrecision { get; set; }


    [JsonPropertyName("minimumTradeSize")]
    public string minimumTradeSize { get; set; } = string.Empty;

    [JsonPropertyName("maximumTrailingStopDistance")]
    public string maximumTrailingStopDistance { get; set; } = string.Empty;

    [JsonPropertyName("minimumGuaranteedStopLossDistance")]
    public string minimumGuaranteedStopLossDistance { get; set; } = string.Empty;

    [JsonPropertyName("minimumTrailingStopDistance")]
    public string minimumTrailingStopDistance { get; set; } = string.Empty;

    [JsonPropertyName("maximumPositionSize")]
    public string maximumPositionSize { get; set; } = string.Empty;

    [JsonPropertyName("maximumOrderUnits")]
    public string maximumOrderUnits { get; set; } = string.Empty;

    [JsonPropertyName("marginRate")]
    public string marginRate { get; set; } = string.Empty;

    [JsonPropertyName("commission")]
    public InstrumentCommission commission { get; set; }

    [JsonPropertyName("guaranteedStopLossOrderMode")]
    public string guaranteedStopLossOrderMode { get; set; } = string.Empty;

    [JsonPropertyName("guaranteedStopLossOrderExecutionPremium")]
    public string guaranteedStopLossOrderExecutionPremium { get; set; } = string.Empty;

    [JsonPropertyName("guaranteedStopLossOrderLevelRestriction")]
    public GuaranteedStopLossOrderLevelRestriction guaranteedStopLossOrderLevelRestriction { get; set; }

    [JsonPropertyName("financing")]
    public InstrumentFinancing financing { get; set; }

    [JsonPropertyName("tags")]
    public IEnumerable<Tag> tags { get; set; }
}

public class Tag
{
    [JsonPropertyName("type")]
    public string type { get; set; } = string.Empty;


    [JsonPropertyName("name")]
    public string name { get; set; } = string.Empty;



}

public class InstrumentFinancing
{
    [JsonPropertyName("longRate")]
    public string longRate { get; set; } = string.Empty;

    [JsonPropertyName("shortRate")]
    public string shortRate { get; set; } = string.Empty;

    [JsonPropertyName("financingDaysOfWeek")]
    public IEnumerable<FinancingDayOfWeek> financingDaysOfWeek { get; set; }

}


public class FinancingDayOfWeek
{
    [JsonPropertyName("dayOfWeek")]
    public string dayOfWeek { get; set; } = string.Empty;

    [JsonPropertyName("daysCharged")]
    public int daysCharged { get; set; }

}

public class GuaranteedStopLossOrderLevelRestriction
{
    [JsonPropertyName("volume")]
    public string volume { get; set; } = string.Empty;

    [JsonPropertyName("priceRange")]
    public string priceRange { get; set; } = string.Empty;

}

public class InstrumentCommission
{
    [JsonPropertyName("commission")]
    public string commission { get; set; } = string.Empty;

    [JsonPropertyName("unitsTraded")]
    public string unitsTraded { get; set; } = string.Empty;

    [JsonPropertyName("minimumCommission")]
    public string minimumCommission { get; set; } = string.Empty;

}