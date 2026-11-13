using NotificationSystem.Abstractions;
using NotificationSystem.Models;
using Serilog;

namespace NotificationSystem.Notifiers;

public sealed class EmailNotifier : INotifier
{
    private readonly string _smtpServer;

    public EmailNotifier(string smtpServer)
    {
        _smtpServer = smtpServer;
    }

    public string ChannelName => "EMAIL";

    public void Send(User user, string message)
    {
        Log.Information("[EMAIL] Отправка на {Email} через {Smtp}: {Message}",
            user.Email, _smtpServer, message);
    }
}
