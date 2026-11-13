using NotificationSystem.Abstractions;
using NotificationSystem.Models;
using Serilog;

namespace NotificationSystem.Notifiers;

public sealed class PushNotifier : INotifier
{
    private readonly string _firebaseKey;

    public PushNotifier(string firebaseKey)
    {
        _firebaseKey = firebaseKey;
    }

    public string ChannelName => "PUSH";

    public void Send(User user, string message)
    {
        Log.Information("[PUSH] Отправка клиенту {Name} (Key: {Key}): {Message}",
            user.Name, _firebaseKey, message);
    }
}
