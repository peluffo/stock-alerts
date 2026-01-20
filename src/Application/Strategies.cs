namespace StockAlerts.Application;

public abstract class StrategyBase : IStrategy
{
    protected StrategyBase(string id) => Id = id;

    public string Id { get; }

    public abstract Task<SignalResult?> ComputeAsync(MarketContext context, CancellationToken cancellationToken);

    protected static int Score(IReadOnlyList<Factor> factors)
    {
        var total = factors.Sum(f => f.Contribution);
        return (int)Math.Clamp(total, 0, 100);
    }
}

public sealed class BollingerRsiMeanReversionStrategy : StrategyBase
{
    public BollingerRsiMeanReversionStrategy() : base("bollinger_rsi_mean_reversion") { }

    public override Task<SignalResult?> ComputeAsync(MarketContext context, CancellationToken cancellationToken)
    {
        var factors = new List<Factor>
        {
            new("rsi_oversold", context.Rsi, 0.4m, context.Rsi < 30 ? 30 : 0),
            new("volatility_regime", context.Atr, 0.3m, context.Atr > 0 ? 20 : 0),
            new("mean_reversion_zone", context.LastPrice, 0.3m, 25)
        };

        var confidence = Score(factors);
        if (confidence < 50)
        {
            return Task.FromResult<SignalResult?>(null);
        }

        return Task.FromResult<SignalResult?>(new SignalResult(
            SignalType.BuySignal,
            confidence,
            factors,
            new[] { "Mean reversion signal; ensure position sizing aligns with volatility." },
            "Lower Bollinger band to mid-band",
            "Close below recent swing low"));
    }
}

public sealed class EmaTrendPullbackStrategy : StrategyBase
{
    public EmaTrendPullbackStrategy() : base("ema_trend_pullback") { }

    public override Task<SignalResult?> ComputeAsync(MarketContext context, CancellationToken cancellationToken)
    {
        var factors = new List<Factor>
        {
            new("trend_alignment", context.LastPrice, 0.4m, 30),
            new("rsi_reset", context.Rsi, 0.3m, context.Rsi is > 40 and < 60 ? 25 : 0),
            new("atr_zone", context.Atr, 0.3m, 20)
        };

        var confidence = Score(factors);
        if (confidence < 55)
        {
            return Task.FromResult<SignalResult?>(null);
        }

        return Task.FromResult<SignalResult?>(new SignalResult(
            SignalType.BuySignal,
            confidence,
            factors,
            new[] { "Pullback in trend; verify higher timeframe structure." },
            "Prior EMA cluster",
            "Close below pullback low"));
    }
}

public sealed class BreakoutVolumeRetestStrategy : StrategyBase
{
    public BreakoutVolumeRetestStrategy() : base("breakout_volume_retest") { }

    public override Task<SignalResult?> ComputeAsync(MarketContext context, CancellationToken cancellationToken)
    {
        var factors = new List<Factor>
        {
            new("volume_confirmation", context.Volume, 0.4m, context.Volume > 0 ? 30 : 0),
            new("breakout_level", context.LastPrice, 0.4m, 25),
            new("retest_signal", context.LastPrice, 0.2m, 20)
        };

        var confidence = Score(factors);
        if (confidence < 60)
        {
            return Task.FromResult<SignalResult?>(null);
        }

        return Task.FromResult<SignalResult?>(new SignalResult(
            SignalType.BuySignal,
            confidence,
            factors,
            new[] { "Breakout with retest; watch for false breakout risk." },
            "Retest zone above breakout level",
            "Close back under breakout level"));
    }
}

public sealed class SupportResistanceBounceStrategy : StrategyBase
{
    public SupportResistanceBounceStrategy() : base("support_resistance_bounce") { }

    public override Task<SignalResult?> ComputeAsync(MarketContext context, CancellationToken cancellationToken)
    {
        var factors = new List<Factor>
        {
            new("swing_proximity", context.LastPrice, 0.5m, 35),
            new("momentum_shift", context.Rsi, 0.3m, context.Rsi > 45 ? 20 : 0),
            new("volatility_filter", context.Atr, 0.2m, 15)
        };

        var confidence = Score(factors);
        if (confidence < 50)
        {
            return Task.FromResult<SignalResult?>(null);
        }

        return Task.FromResult<SignalResult?>(new SignalResult(
            SignalType.Watch,
            confidence,
            factors,
            new[] { "Bounce setup; wait for confirmation if entering." },
            "Support band",
            "Close below swing low"));
    }
}

public sealed class CoveredCallCandidateStrategy : StrategyBase
{
    public CoveredCallCandidateStrategy() : base("covered_call_candidate") { }

    public override Task<SignalResult?> ComputeAsync(MarketContext context, CancellationToken cancellationToken)
    {
        var factors = new List<Factor>
        {
            new("income_bias", context.LastPrice, 0.4m, 25),
            new("volatility_premium", context.Atr, 0.4m, 30),
            new("neutral_trend", context.Rsi, 0.2m, context.Rsi is > 40 and < 60 ? 20 : 0)
        };

        var confidence = Score(factors);
        if (confidence < 55)
        {
            return Task.FromResult<SignalResult?>(null);
        }

        return Task.FromResult<SignalResult?>(new SignalResult(
            SignalType.CoveredCallCandidate,
            confidence,
            factors,
            new[] { "Informational only; verify holdings and tax implications." },
            "Neutral to slightly bullish range",
            "Break above recent resistance"));
    }
}
