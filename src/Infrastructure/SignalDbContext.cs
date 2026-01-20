using Microsoft.EntityFrameworkCore;
using StockAlerts.Domain;

namespace StockAlerts.Infrastructure;

public sealed class SignalDbContext : DbContext
{
    public SignalDbContext(DbContextOptions<SignalDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Subscription> Subscriptions => Set<Subscription>();
    public DbSet<WatchlistItem> Watchlist => Set<WatchlistItem>();
    public DbSet<AlertRule> AlertRules => Set<AlertRule>();
    public DbSet<SignalEvent> SignalEvents => Set<SignalEvent>();
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<BacktestRun> BacktestRuns => Set<BacktestRun>();
}
