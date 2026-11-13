using NotificationSystem.Abstractions;
using NotificationSystem.Models;

namespace NotificationSystem.Tests;

public sealed class MockNotifier : INotifier
{
    public List<string> Messages { get; } = [];

    public string ChannelName => "MOCK";

    public void Send(User user, string message)
    {
        Messages.Add($"{user.Name}:{message}");
    }
}
