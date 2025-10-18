public interface INotificationService
{
    void Send(string message);
}

public class EmailNotificationService : INotificationService
{
    public void Send(string message)
    {
        Console.WriteLine($"Email sent with message: {message}");
    }
}

public class SmsNotificationService : INotificationService
{
    public void Send(string message)
    {
        Console.WriteLine($"SMS sent with message: {message}");
    }
}

public class PushNotificationService : INotificationService
{
    public void Send(string message)
    {
        Console.WriteLine($"Push notification sent with message: {message}");
    }
}

public class NotificationManager
{
    private readonly INotificationService _notificationService;
    public NotificationManager(INotificationService notificationService)
    {
        _notificationService = notificationService;

    }
    public void Notify(string message)
    {
        _notificationService.Send(message);

    }
}
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter the message to send:");
        var message = Console.ReadLine();
        Console.WriteLine("Choose notification method: 1. Email 2. SMS 3. Push Notification");
        var choice = Console.ReadLine();
        INotificationService notificationService = choice switch
        {
            "1" => new EmailNotificationService(),
            "2" => new SmsNotificationService(),
            "3" => new PushNotificationService(),
            _ => throw new InvalidOperationException("Invalid choice")
        };
        var manager = new NotificationManager(notificationService);
        manager.Notify(message);



    }
}