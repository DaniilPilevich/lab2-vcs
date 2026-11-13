using NotificationSystem.Models;
using NotificationSystem.Services;

namespace NotificationSystem.Tests;

public sealed class NotificationServiceTests
{
    [Fact]
    public void NotificationService_Should_Call_All_Notifiers()
    {
        var mock1 = new MockNotifier();
        var mock2 = new MockNotifier();
        var service = new NotificationService([mock1, mock2]);

        var user = new User { Id = 1, Name = "Test", Email = "a@b.c", Phone = "1" };
        var records = service.Notify(user, "Hello");

        Assert.Equal(2, records.Count);
        Assert.Single(mock1.Messages);
        Assert.Single(mock2.Messages);
    }

    [Fact]
    public void NotificationService_Preserves_Notifier_Order()
    {
        var first = new MockNotifier();
        var second = new MockNotifier();
        var service = new NotificationService([first, second]);

        var user = new User { Name = "Ordered" };
        var records = service.Notify(user, "Msg");

        Assert.Equal(2, records.Count);
        Assert.Equal(first.Messages[0], $"{user.Name}:Msg");
        Assert.Equal(second.Messages[0], $"{user.Name}:Msg");
        Assert.Equal("MOCK", records[0].Channel);
    }

    [Fact]
    public void NotificationService_Passes_Correct_User_And_Message()
    {
        var mock = new MockNotifier();
        var service = new NotificationService([mock]);
        var user = new User { Name = "Иван" };

        service.Notify(user, "Заказ готов");

        Assert.Contains("Иван:Заказ готов", mock.Messages);
    }
}
