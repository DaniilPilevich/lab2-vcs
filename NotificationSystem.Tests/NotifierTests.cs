using NotificationSystem.Models;
using NotificationSystem.Notifiers;
using Serilog;

namespace NotificationSystem.Tests;

public sealed class NotifierTests
{
    public NotifierTests()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger();
    }

    [Theory]
    [InlineData(typeof(SmsNotifier), "SMS")]
    [InlineData(typeof(EmailNotifier), "EMAIL")]
    [InlineData(typeof(PushNotifier), "PUSH")]
    [InlineData(typeof(TelegramNotifier), "TELEGRAM")]
    public void Notifier_Has_Expected_ChannelName(Type notifierType, string expectedChannel)
    {
        var notifier = CreateNotifier(notifierType);
        Assert.Equal(expectedChannel, notifier.ChannelName);
    }

    [Fact]
    public void SmsNotifier_Send_Does_Not_Throw()
    {
        var notifier = new SmsNotifier("api.test");
        var user = new User { Name = "Test", Phone = "+375291234567" };
        var exception = Record.Exception(() => notifier.Send(user, "Hi"));
        Assert.Null(exception);
    }

    [Fact]
    public void EmailNotifier_Send_Does_Not_Throw()
    {
        var notifier = new EmailNotifier("smtp.test");
        var user = new User { Name = "Test", Email = "a@b.c" };
        var exception = Record.Exception(() => notifier.Send(user, "Hi"));
        Assert.Null(exception);
    }

    [Fact]
    public void TelegramNotifier_Send_Does_Not_Throw()
    {
        var notifier = new TelegramNotifier("@token");
        var user = new User { Name = "Test" };
        var exception = Record.Exception(() => notifier.Send(user, "Hi"));
        Assert.Null(exception);
    }

    private static Abstractions.INotifier CreateNotifier(Type type) => type.Name switch
    {
        nameof(SmsNotifier) => new SmsNotifier("api"),
        nameof(EmailNotifier) => new EmailNotifier("smtp"),
        nameof(PushNotifier) => new PushNotifier("key"),
        nameof(TelegramNotifier) => new TelegramNotifier("token"),
        _ => throw new ArgumentException($"Unknown notifier: {type.Name}")
    };
}
