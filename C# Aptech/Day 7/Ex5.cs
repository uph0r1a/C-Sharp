namespace SmartLightDemo;

public sealed class SmartLight
{
    public event EventHandler? LightTurnedOn;
    public event EventHandler? LightTurnedOff;
    public bool IsOn { get; private set; }

    public void TurnOn()
    {
        if (IsOn)
        {
            return;
        }

        IsOn = true;
        Console.WriteLine("Smart Light: Turning ON...");

        LightTurnedOn?.Invoke(this, EventArgs.Empty);
    }

    public void TurnOff()
    {
        if (!IsOn)
        {
            return;
        }

        IsOn = false;
        Console.WriteLine("Smart Light: Turning OFF...");

        LightTurnedOff?.Invoke(this, EventArgs.Empty);
    }
}

public sealed class NotificationService
{
    public void OnLightTurnedOn(object? sender, EventArgs e)
    {
        Console.WriteLine("[Notification] The light has been turned ON!");
    }

    public void OnLightTurnedOff(object? sender, EventArgs e)
    {
        Console.WriteLine("[Notification] The light has been turned OFF!");
    }
}

public sealed class LoggingService
{
    public void OnLightTurnedOn(object? sender, EventArgs e)
    {
        Console.WriteLine("[Log] Light turned ON at {0:T}", DateTime.Now);
    }

    public void OnLightTurnedOff(object? sender, EventArgs e)
    {
        Console.WriteLine("[Log] Light turned OFF at {0:T}", DateTime.Now);
    }
}

public static class Program
{
    public static void Main()
    {
        var smartLight = new SmartLight();
        var notificationService = new NotificationService();
        var loggingService = new LoggingService();

        smartLight.LightTurnedOn += notificationService.OnLightTurnedOn;
        smartLight.LightTurnedOn += loggingService.OnLightTurnedOn;
        smartLight.LightTurnedOff += notificationService.OnLightTurnedOff;
        smartLight.LightTurnedOff += loggingService.OnLightTurnedOff;

        smartLight.TurnOn();
        Console.WriteLine();
        smartLight.TurnOff();
    }
}