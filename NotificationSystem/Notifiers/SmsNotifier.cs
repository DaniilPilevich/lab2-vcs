using NotificationSystem.Abstractions;
using NotificationSystem.Models;
using Serilog;

namespace NotificationSystem.Notifiers;

public sealed class SmsNotifier : INotifier
{
    private readonly string _apiGateway;

    public SmsNotifier(string apiGateway)
    {
        _apiGateway = apiGateway;
    }

    public string ChannelName => "SMS";

    public void Send(User user, string message)
    {
        Log.Information("[SMS] Отправка на {Phone} через {Gateway}: {Message}",
            user.Phone, _apiGateway, message);
    }
}
