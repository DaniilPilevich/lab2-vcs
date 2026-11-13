using Newtonsoft.Json;
using NotificationSystem.Abstractions;
using NotificationSystem.Models;
using NotificationSystem.Notifiers;
using NotificationSystem.Services;
using NotificationSystem.Validation;
using Serilog;

namespace NotificationSystem;

internal static class Program
{
    private static void Main()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("logs/app.log")
            .CreateLogger();

        try
        {
            User user = new()
            {
                Id = 1,
                Name = "Иван Иванов",
                Email = "ivan@mail.ru",
                Phone = "+777777777777"
            };

            // FluentValidation добавлен вручную в .csproj (без CLI)
            var validation = new UserValidator().Validate(user);
            if (!validation.IsValid)
            {
                foreach (var error in validation.Errors)
                {
                    Log.Warning("Ошибка валидации: {Error}", error.ErrorMessage);
                }

                return;
            }

            const string message = "Ваш заказ №101 оформлен и передан в доставку.";

            List<INotifier> notifiers =
            [
                new SmsNotifier("api.sms-provider.com"),
                new EmailNotifier("smtp.gmail.com"),
                new PushNotifier("AAAA_Firebase_Key")
            ];

            NotificationService service = new(notifiers);
            IReadOnlyList<NotificationRecord> baseRecords = service.Notify(user, message);

            Log.Information("--- Добавляем канал Telegram без изменения NotificationService ---");

            List<INotifier> extendedNotifiers = [.. notifiers, new TelegramNotifier("@MyBotToken")];
            NotificationService extendedService = new(extendedNotifiers);
            IReadOnlyList<NotificationRecord> extendedRecords = extendedService.Notify(user, message);

            var payload = new
            {
                Input = user,
                Message = message,
                Base = baseRecords,
                Extended = extendedRecords
            };

            string json = JsonConvert.SerializeObject(payload, Formatting.Indented);
            File.WriteAllText("notifications.json", json, System.Text.Encoding.UTF8);
            Log.Information("Результаты сохранены в notifications.json");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
