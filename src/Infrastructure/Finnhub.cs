using System.Net;
using System.Net.Http.Json;
using StackExchange.Redis;
using StockAlerts.Application;

namespace StockAlerts.Infrastructure;

public sealed class RateLimitedFinnhubGateway : IFinnhubClient
{
    private readonly HttpClient _httpClient;
    private readonly IDatabase _cache;
    private readonly TokenBucketLimiter _limiter;

    public RateLimitedFinnhubGateway(HttpClient httpClient, IConnectionMultiplexer redis, TokenBucketLimiter limiter)
    {
        _httpClient = httpClient;
        _cache = redis.GetDatabase();
        _limiter = limiter;
    }

    public Task<Quote> GetQuoteAsync(string symbol, CancellationToken cancellationToken) =>
        GetCachedAsync($"finnhub:quote:{symbol}", TimeSpan.FromSeconds(20), () =>
            SendAsync<Quote>($"/quote?symbol={symbol}", cancellationToken));

    public Task<IReadOnlyList<Candle>> GetCandlesAsync(string symbol, string resolution, DateTimeOffset from, DateTimeOffset to, CancellationToken cancellationToken) =>
        GetCachedAsync(
            $"finnhub:candles:{symbol}:{resolution}:{from:yyyyMMddHHmm}:{to:yyyyMMddHHmm}",
            resolution switch
            {
                "1" => TimeSpan.FromSeconds(60),
                "15" => TimeSpan.FromMinutes(5),
                _ => TimeSpan.FromMinutes(10)
            },
            () => SendAsync<List<Candle>>($"/stock/candle?symbol={symbol}&resolution={resolution}&from={from.ToUnixTimeSeconds()}&to={to.ToUnixTimeSeconds()}", cancellationToken));

    public Task<IReadOnlyList<CompanyNews>> GetCompanyNewsAsync(string symbol, DateTimeOffset from, DateTimeOffset to, CancellationToken cancellationToken) =>
        GetCachedAsync(
            $"finnhub:news:{symbol}:{from:yyyyMMdd}:{to:yyyyMMdd}",
            TimeSpan.FromMinutes(30),
            () => SendAsync<List<CompanyNews>>($"/company-news?symbol={symbol}&from={from:yyyy-MM-dd}&to={to:yyyy-MM-dd}", cancellationToken));

    public Task<NewsSentiment?> GetNewsSentimentAsync(string symbol, CancellationToken cancellationToken) =>
        GetCachedAsync(
            $"finnhub:sentiment:{symbol}",
            TimeSpan.FromMinutes(30),
            () => SendAsync<NewsSentiment?>($"/news-sentiment?symbol={symbol}", cancellationToken));

    private async Task<T> GetCachedAsync<T>(string key, TimeSpan ttl, Func<Task<T>> fetch)
    {
        var cached = await _cache.StringGetAsync(key);
        if (cached.HasValue)
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(cached!)!;
        }

        await _limiter.WaitAsync();

        var result = await fetch();
        var payload = System.Text.Json.JsonSerializer.Serialize(result);
        await _cache.StringSetAsync(key, payload, ttl);
        return result;
    }

    private async Task<T> SendAsync<T>(string path, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync(path, cancellationToken);
        if (response.StatusCode == HttpStatusCode.TooManyRequests)
        {
            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            response = await _httpClient.GetAsync(path, cancellationToken);
        }

        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<T>(cancellationToken: cancellationToken))!;
    }
}

public sealed class TokenBucketLimiter
{
    private readonly int _capacity;
    private readonly TimeSpan _refillInterval;
    private int _tokens;
    private DateTimeOffset _lastRefill = DateTimeOffset.UtcNow;
    private readonly SemaphoreSlim _mutex = new(1, 1);

    public TokenBucketLimiter(int capacity, TimeSpan refillInterval)
    {
        _capacity = capacity;
        _refillInterval = refillInterval;
        _tokens = capacity;
    }

    public async Task WaitAsync()
    {
        while (true)
        {
            await _mutex.WaitAsync();
            try
            {
                Refill();
                if (_tokens > 0)
                {
                    _tokens--;
                    return;
                }
            }
            finally
            {
                _mutex.Release();
            }

            await Task.Delay(TimeSpan.FromMilliseconds(200));
        }
    }

    private void Refill()
    {
        var now = DateTimeOffset.UtcNow;
        var elapsed = now - _lastRefill;
        if (elapsed < _refillInterval)
        {
            return;
        }

        var refillCount = (int)(elapsed.TotalMilliseconds / _refillInterval.TotalMilliseconds);
        if (refillCount <= 0)
        {
            return;
        }

        _tokens = Math.Min(_capacity, _tokens + refillCount);
        _lastRefill = now;
    }
}
