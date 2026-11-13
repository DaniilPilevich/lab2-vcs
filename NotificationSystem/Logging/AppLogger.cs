using Serilog;

namespace NotificationSystem.Logging;

/// <summary>
/// Простая обёртка логирования для демонстрации ветки feature/add-logging (ЛР №2).
/// </summary>
public static class AppLogger
{
    public static void Info(string message)
    {
        Log.Information("[AppLogger] {Message}", message);
    }

    public static void Warning(string message)
    {
        Log.Warning("[AppLogger] {Message}", message);
    }
}
