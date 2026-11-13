using NotificationSystem.Abstractions;
using NotificationSystem.Models;
using Serilog;

namespace NotificationSystem.Notifiers;

public sealed class TelegramNotifier : INotifier
{
    private readonly string _botToken;

    public TelegramNotifier(string botToken)
    {
        _botToken = botToken;
    }

    public string ChannelName => "TELEGRAM";

    public void Send(User user, string message)
    {
        Log.Information("[TELEGRAM] Отправка @{Name} (Token: {Token}): {Message}",
            user.Name, _botToken, message);
    }
}
