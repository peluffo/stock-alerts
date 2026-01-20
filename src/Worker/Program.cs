using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<SignalScanWorker>();

builder.Services.AddSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var host = builder.Build();
await host.RunAsync();

public sealed class SignalScanWorker : BackgroundService
{
    private readonly ILogger<SignalScanWorker> _logger;

    public SignalScanWorker(ILogger<SignalScanWorker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Signal scan tick running at {Timestamp}", DateTimeOffset.UtcNow);
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}
