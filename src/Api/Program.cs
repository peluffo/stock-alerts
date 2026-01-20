using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using StockAlerts.Application;
using StockAlerts.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddHealthChecks();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

builder.Services.AddSingleton(new TokenBucketLimiter(55, TimeSpan.FromMinutes(1)));

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/admin/health");
app.MapHub<SignalHub>("/hubs/signals");

app.MapGet("/", () => Results.Ok(new
{
    service = "Signal Alerting Platform",
    disclaimer = "Not financial advice. Educational/informational only.",
    mode = "alerts-only"
}));

app.Run();

public sealed class SignalHub : Microsoft.AspNetCore.SignalR.Hub { }
