using System.Text.Json.Serialization;

namespace MiniTrade.ConsoleApp.Models.Oanda;

internal class AccountResponse
{
    //[JsonPropertyName("accounts")]
    //public IEnumerable<AccountProperties>? Accounts { get; set; }

    [JsonPropertyName("account")]
    public Account Account { get; set; }

    [JsonPropertyName("lastTransactionID")]
    public string LastTransactionID { get; set; } = string.Empty;

}

internal class Account
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("alias")]
    public string Alias { get; set; } = string.Empty;

    [JsonPropertyName("currency")]
    public string Currency { get; set; } = string.Empty;

    [JsonPropertyName("createdByUserID")]
    public int CreatedByUserID { get; set; }

    [JsonPropertyName("createdTime")]
    public string CreatedTime { get; set; } = string.Empty;


    [JsonPropertyName("guaranteedStopLossOrderParameters")]
    public GuaranteedStopLossOrderParameters GuaranteedStopLossOrderParameters { get; set; }

    [JsonPropertyName("guaranteedStopLossOrderMode")]
    public string GuaranteedStopLossOrderMode { get; set; } = string.Empty;

    [JsonPropertyName("guaranteedStopLossOrderMutability")]
    public string GuaranteedStopLossOrderMutability { get; set; } = string.Empty;

    [JsonPropertyName("resettablePLTime")]
    public string ResettablePLTime { get; set; } = string.Empty;

    [JsonPropertyName("marginRate")]
    public string MarginRate { get; set; } = string.Empty;

    [JsonPropertyName("openTradeCount")]
    public int OpenTradeCount { get; set; }

    [JsonPropertyName("openPositionCount")]
    public int OpenPositionCount { get; set; }

    [JsonPropertyName("pendingOrderCount")]
    public int PendingOrderCount { get; set; }

    [JsonPropertyName("hedgingEnabled")]
    public bool HedgingEnabled { get; set; }

    [JsonPropertyName("unrealizedPL")]
    public string UnrealizedPL { get; set; } = string.Empty;

    [JsonPropertyName("NAV")]
    public string NAV { get; set; } = string.Empty;

    [JsonPropertyName("marginUsed")]
    public string MarginUsed { get; set; } = string.Empty;

    [JsonPropertyName("marginAvailable")]
    public string MarginAvailable { get; set; } = string.Empty;

    [JsonPropertyName("positionValue")]
    public string PositionValue { get; set; } = string.Empty;

    [JsonPropertyName("marginCloseoutUnrealizedPL")]
    public string MarginCloseoutUnrealizedPL { get; set; } = string.Empty;

    [JsonPropertyName("marginCloseoutNAV")]
    public string MarginCloseoutNAV { get; set; } = string.Empty;

    [JsonPropertyName("marginCloseoutMarginUsed")]
    public string MarginCloseoutMarginUsed { get; set; } = string.Empty;


    [JsonPropertyName("marginCloseoutPercent")]
    public string MarginCloseoutPercent { get; set; } = string.Empty;


    [JsonPropertyName("marginCloseoutPositionValue")]
    public string MarginCloseoutPositionValue { get; set; } = string.Empty;


    [JsonPropertyName("withdrawalLimit")]
    public string WithdrawalLimit { get; set; } = string.Empty;


    [JsonPropertyName("marginCallMarginUsed")]
    public string MarginCallMarginUsed { get; set; } = string.Empty;


    [JsonPropertyName("marginCallPercent")]
    public string MarginCallPercent { get; set; } = string.Empty;


    [JsonPropertyName("balance")]
    public string Balance { get; set; } = string.Empty;


    [JsonPropertyName("pl")]
    public string PL { get; set; } = string.Empty;

    [JsonPropertyName("resettablePL")]
    public string ResettablePL { get; set; } = string.Empty;

    [JsonPropertyName("financing")]
    public string Financing { get; set; } = string.Empty;

    [JsonPropertyName("commission")]
    public string Commission { get; set; } = string.Empty;

    [JsonPropertyName("dividendAdjustment")]
    public string DividendAdjustment { get; set; } = string.Empty;

    [JsonPropertyName("guaranteedExecutionFees")]
    public string GuaranteedExecutionFees { get; set; } = string.Empty;

    [JsonPropertyName("marginCallEnterTime")]
    public DateTime? MarginCallEnterTime { get; set; }

    [JsonPropertyName("marginCallExtensionCount")]
    public int MarginCallExtensionCount { get; set; }

    [JsonPropertyName("lastMarginCallExtensionTime")]
    public DateTime? LastMarginCallExtensionTime { get; set; }

    [JsonPropertyName("lastTransactionID")]
    public string LastTransactionID { get; set; } = string.Empty;

    [JsonPropertyName("trades")]
    public IEnumerable<TradeSummary> Trades { get; set; }

    [JsonPropertyName("positions")]
    public IEnumerable<Position> Positions { get; set; }

    [JsonPropertyName("orders")]
    public IEnumerable<Order> Orders { get; set; }

}

public class Order
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("createTime")]
    public DateTime? createTime { get; set; }

    [JsonPropertyName("state")]
    public string state { get; set; } = string.Empty;

    [JsonPropertyName("clientExtensions")]
    public ClientExtensions clientExtensions { get; set; }

}

public class Position
{
    [JsonPropertyName("instrument")]
    public string instrument { get; set; } = string.Empty;

    [JsonPropertyName("pl")]
    public string pl { get; set; } = string.Empty;

    [JsonPropertyName("unrealizedPL")]
    public string unrealizedPL { get; set; } = string.Empty;

    [JsonPropertyName("marginUsed")]
    public string marginUsed { get; set; } = string.Empty;

    [JsonPropertyName("resettablePL")]
    public string resettablePL { get; set; } = string.Empty;

    [JsonPropertyName("financing")]
    public string financing { get; set; } = string.Empty;

    [JsonPropertyName("commission")]
    public string commission { get; set; } = string.Empty;

    [JsonPropertyName("dividendAdjustment")]
    public string dividendAdjustment { get; set; } = string.Empty;

    [JsonPropertyName("guaranteedExecutionFees")]
    public string guaranteedExecutionFees { get; set; } = string.Empty;

    [JsonPropertyName("long")]
    public PositionSide Long { get; set; }

    [JsonPropertyName("short")]
    public PositionSide Short{ get; set; }
}

public class PositionSide
{
    [JsonPropertyName("units")]
    public string units { get; set; } = string.Empty;

    [JsonPropertyName("averagePrice")]
    public string averagePrice { get; set; } = string.Empty;

    [JsonPropertyName("tradeIDs")]
    public IEnumerable<string> tradeIDs { get; set; }

    [JsonPropertyName("pl")]
    public string pl { get; set; } = string.Empty;

    [JsonPropertyName("unrealizedPL")]
    public string unrealizedPL { get; set; } = string.Empty;

    [JsonPropertyName("resettablePL")]
    public string resettablePL { get; set; } = string.Empty;

    [JsonPropertyName("financing")]
    public string financing { get; set; } = string.Empty;

    [JsonPropertyName("dividendAdjustment")]
    public string dividendAdjustment { get; set; } = string.Empty;

    [JsonPropertyName("guaranteedExecutionFees")]
    public string guaranteedExecutionFees { get; set; } = string.Empty;



}

public class TradeSummary
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("instrument")]
    public string Instrument { get; set; } = string.Empty;

    [JsonPropertyName("price")]
    public string Price { get; set; } = string.Empty;

    [JsonPropertyName("openTime")]
    public DateTime? OpenTime { get; set; }


    [JsonPropertyName("state")]
    public string state { get; set; } = string.Empty;

    [JsonPropertyName("initialUnits")]
    public string initialUnits { get; set; } = string.Empty;

    [JsonPropertyName("initialMarginRequired")]
    public string initialMarginRequired { get; set; } = string.Empty;

    [JsonPropertyName("currentUnits")]
    public string currentUnits { get; set; } = string.Empty;

    [JsonPropertyName("realizedPL")]
    public string realizedPL { get; set; } = string.Empty;

    [JsonPropertyName("unrealizedPL")]
    public string unrealizedPL { get; set; } = string.Empty;

    [JsonPropertyName("marginUsed")]
    public string marginUsed { get; set; } = string.Empty;

    [JsonPropertyName("averageClosePrice")]
    public string averageClosePrice { get; set; } = string.Empty;


    [JsonPropertyName("closingTransactionIDs")]
    public IEnumerable<string> closingTransactionIDs { get; set; }

    [JsonPropertyName("financing")]
    public string financing { get; set; } = string.Empty;
    
    [JsonPropertyName("dividendAdjustment")]
    public string dividendAdjustment { get; set; } = string.Empty;

    [JsonPropertyName("closeTime")]
    public DateTime? closeTime { get; set; }


    [JsonPropertyName("clientExtensions")]
    public ClientExtensions clientExtensions { get; set; }

    [JsonPropertyName("takeProfitOrderID")]
    public string takeProfitOrderID { get; set; } = string.Empty;

    [JsonPropertyName("stopLossOrderID")]
    public string stopLossOrderID { get; set; } = string.Empty;

    [JsonPropertyName("guaranteedStopLossOrderID")]
    public string guaranteedStopLossOrderID { get; set; } = string.Empty;

    [JsonPropertyName("trailingStopLossOrderID")]
    public string trailingStopLossOrderID { get; set; } = string.Empty;

}


public class ClientExtensions
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("tag")]
    public string Tag { get; set; } = string.Empty;

    [JsonPropertyName("comment")]
    public string Comment { get; set; } = string.Empty;



}


public class GuaranteedStopLossOrderParameters
{
    [JsonPropertyName("mutabilityMarketOpen")]
    public string MutabilityMarketOpen { get; set; } = string.Empty;

    [JsonPropertyName("mutabilityMarketHalted")]
    public string MutabilityMarketHalted { get; set; } = string.Empty;
}