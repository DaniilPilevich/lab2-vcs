using NotificationSystem.Models;

namespace NotificationSystem.Abstractions;

public interface INotifier
{
    string ChannelName { get; }
    void Send(User user, string message);
}
