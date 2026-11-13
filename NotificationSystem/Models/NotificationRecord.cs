namespace NotificationSystem.Models;

public sealed class NotificationRecord
{
    public required string Channel { get; init; }
    public required string UserName { get; init; }
    public required string Message { get; init; }
    public DateTime SentAt { get; init; } = DateTime.Now;
}
