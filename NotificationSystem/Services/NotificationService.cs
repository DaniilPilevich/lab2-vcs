using NotificationSystem.Abstractions;
using NotificationSystem.Models;
using Serilog;

namespace NotificationSystem.Services;

public sealed class NotificationService
{
    private readonly IReadOnlyList<INotifier> _notifiers;

    public NotificationService(IEnumerable<INotifier> notifiers)
    {
        ArgumentNullException.ThrowIfNull(notifiers);
        _notifiers = notifiers.ToList();
    }

    public IReadOnlyList<NotificationRecord> Notify(User user, string message)
    {
        Log.Information("Оповещение пользователя «{Name}». Каналов в сети: {Count}",
            user.Name, _notifiers.Count);

        List<NotificationRecord> records = [];

        foreach (INotifier notifier in _notifiers)
        {
            notifier.Send(user, message);
            records.Add(new NotificationRecord
            {
                Channel = notifier.ChannelName,
                UserName = user.Name,
                Message = message
            });
        }

        return records;
    }
}
