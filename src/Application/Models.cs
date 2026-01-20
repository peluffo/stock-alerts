namespace StockAlerts.Application;

public sealed record MarketContext(
    string Symbol,
    string Timeframe,
    decimal LastPrice,
    IReadOnlyList<Candle> Candles,
    decimal Atr,
    decimal Rsi,
    decimal Volume,
    DateTimeOffset AsOf);

public sealed record Candle(DateTimeOffset Timestamp, decimal Open, decimal High, decimal Low, decimal Close, decimal Volume);

public enum SignalType
{
    BuySignal,
    SellSignal,
    ExitSignal,
    CoveredCallCandidate,
    Watch
}

public sealed record Factor(string Name, decimal Value, decimal Weight, decimal Contribution);

public sealed record SignalResult(
    SignalType Type,
    int Confidence,
    IReadOnlyList<Factor> Rationale,
    IReadOnlyList<string> RiskNotes,
    string SuggestedEntryZone,
    string InvalidationLevel);

public interface IStrategy
{
    string Id { get; }
    Task<SignalResult?> ComputeAsync(MarketContext context, CancellationToken cancellationToken);
}

public interface IFinnhubClient
{
    Task<Quote> GetQuoteAsync(string symbol, CancellationToken cancellationToken);
    Task<IReadOnlyList<Candle>> GetCandlesAsync(string symbol, string resolution, DateTimeOffset from, DateTimeOffset to, CancellationToken cancellationToken);
    Task<IReadOnlyList<CompanyNews>> GetCompanyNewsAsync(string symbol, DateTimeOffset from, DateTimeOffset to, CancellationToken cancellationToken);
    Task<NewsSentiment?> GetNewsSentimentAsync(string symbol, CancellationToken cancellationToken);
}

public sealed record Quote(decimal Current, decimal Change, decimal PercentChange, DateTimeOffset AsOf);

public sealed record CompanyNews(string Headline, string Summary, DateTimeOffset PublishedAt, string Url);

public sealed record NewsSentiment(decimal Score, decimal BullishPercent, decimal BearishPercent, DateTimeOffset AsOf);
