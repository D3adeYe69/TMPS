# SOLID Principles Implementation

This project demonstrates the implementation of three SOLID principles: **S**ingle Responsibility, **O**pen-Closed, and **D**ependency Inversion through a notification system application.

## Implemented SOLID Principles

### 1. Single Responsibility Principle (SRP)
> "A class should have only one reason to change."

In our implementation, each notification service class has a single responsibility:

```csharp
public class EmailNotificationService : INotificationService
{
    public void Send(string message)
    {
        Console.WriteLine($"Email sent with message: {message}");
    }
}
```

**Why it works:** Each service class (Email, SMS, Push) is responsible for only one type of notification. If we need to modify how email notifications work, we only need to change the `EmailNotificationService` class, without affecting other notification types.

### 2. Open-Closed Principle (OCP)
> "Software entities should be open for extension, but closed for modification."

Our notification system demonstrates this principle through the `INotificationService` interface:

```csharp
public interface INotificationService
{
    void Send(string message);
}
```

**Why it works:** To add a new notification type (e.g., Slack notifications), we don't need to modify existing code. We simply create a new class that implements `INotificationService`:

```csharp
public class SlackNotificationService : INotificationService
{
    public void Send(string message)
    {
        Console.WriteLine($"Slack message sent: {message}");
    }
}
```

### 3. Dependency Inversion Principle (DIP)
> "High-level modules should not depend on low-level modules. Both should depend on abstractions."

Our `NotificationManager` class depends on the `INotificationService` abstraction, not concrete implementations:

```csharp
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
```

**Why it works:** The `NotificationManager` class doesn't need to know about specific notification implementations. It works with any class that implements `INotificationService`. This makes the system more flexible and easier to test, as we can easily swap different notification services.

## Project Structure

The project implements a notification system where users can:
1. Enter a message
2. Choose a notification method (Email, SMS, or Push)
3. Send the message using the selected method

## Running the Application

1. Build the project:
```
dotnet build
```

2. Run the application:
```
dotnet run
```

3. Follow the prompts to:
   - Enter your message
   - Choose a notification method (1 for Email, 2 for SMS, 3 for Push)

## Benefits of This Implementation

1. **Maintainability**: Each class has a single responsibility, making the code easier to maintain and modify.
2. **Extensibility**: New notification methods can be added without changing existing code.
3. **Flexibility**: The dependency injection pattern makes the system flexible and testable.
4. **Decoupling**: High-level and low-level modules are decoupled through abstractions.

## Future Improvements

- Add actual email/SMS/push notification implementation
- Implement logging system
- Add configuration for different notification services
- Add unit tests
- Implement the remaining SOLID principles (Interface Segregation and Liskov Substitution)