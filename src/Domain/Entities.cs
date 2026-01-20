namespace StockAlerts.Domain;

public sealed class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public bool EmailConfirmed { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}

public sealed class Subscription
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string StripeCustomerId { get; set; } = string.Empty;
    public string StripeSubscriptionId { get; set; } = string.Empty;
    public string Status { get; set; } = "inactive";
    public DateTimeOffset? CurrentPeriodEnd { get; set; }
}

public sealed class WatchlistItem
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}

public sealed class AlertRule
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public string Timeframe { get; set; } = "1h";
    public string StrategyId { get; set; } = string.Empty;
    public string ParamsJson { get; set; } = "{}";
    public bool Enabled { get; set; } = true;
    public int CooldownMinutes { get; set; } = 60;
    public string DeliveryPrefs { get; set; } = "{\"email\":true,\"sms\":false,\"inApp\":true}";
    public string QuietHours { get; set; } = "{}";
}

public sealed class SignalEvent
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public string Timeframe { get; set; } = string.Empty;
    public string StrategyId { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public int Confidence { get; set; }
    public string RationaleJson { get; set; } = "[]";
    public string IndicatorSnapshotJson { get; set; } = "{}";
    public DateTimeOffset FiredAt { get; set; } = DateTimeOffset.UtcNow;
}

public sealed class Notification
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Channel { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public string Status { get; set; } = "pending";
    public string PayloadJson { get; set; } = "{}";
    public string ProviderId { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}

public sealed class AuditLog
{
    public Guid Id { get; set; }
    public Guid? ActorUserId { get; set; }
    public string Action { get; set; } = string.Empty;
    public string Entity { get; set; } = string.Empty;
    public string MetadataJson { get; set; } = "{}";
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}

public sealed class BacktestRun
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string StrategyId { get; set; } = string.Empty;
    public string ResultsJson { get; set; } = "{}";
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
