using NotificationSystem.Abstractions;
using NotificationSystem.Models;
using Serilog;

namespace NotificationSystem.Notifiers;

// Дополнительный модуль для ветки feature/new-module (ЛР №2)
public sealed class DiscordNotifier : INotifier
{
    private readonly string _webhookUrl;

    public DiscordNotifier(string webhookUrl)
    {
        _webhookUrl = webhookUrl;
    }

    public string ChannelName => "DISCORD";

    public void Send(User user, string message)
    {
        Log.Information("[DISCORD] Отправка {Name} через {Webhook}: {Message}",
            user.Name, _webhookUrl, message);
    }
}
