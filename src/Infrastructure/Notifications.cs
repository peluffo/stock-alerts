using StockAlerts.Domain;

namespace StockAlerts.Infrastructure;

public interface IEmailSender
{
    Task SendAsync(Notification notification, CancellationToken cancellationToken);
}

public interface ISmsSender
{
    Task SendAsync(Notification notification, CancellationToken cancellationToken);
}

public sealed class SendGridEmailSender : IEmailSender
{
    public Task SendAsync(Notification notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

public sealed class TwilioSmsSender : ISmsSender
{
    public Task SendAsync(Notification notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
