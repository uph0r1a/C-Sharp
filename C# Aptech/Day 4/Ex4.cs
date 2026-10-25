Device[] devices = [new SmartPhone("iPhone 17", 100, "Wi-Fi, 5G"), new Laptop("ThinkPad", 85, "Wi-Fi, Ethernet"), new Tablet("iPad Air", 60, "Wi-Fi")];

foreach (var device in devices)
{
    device.DisplayInfo();
    device.Connect();
    device.Recharge();
    Console.WriteLine();
}

public abstract class Device(string name, int batteryLevel)
{
    public string Name { get; } = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentException("Name cannot be empty.", nameof(name));

    public int BatteryLevel { get; private set; } = batteryLevel is >= 0 and <= 100 ? batteryLevel : throw new ArgumentOutOfRangeException(nameof(batteryLevel));

    public void DisplayInfo() => Console.WriteLine($"{GetType().Name}: {Name} | Battery: {BatteryLevel}%");

    public abstract void Connect();

    public virtual void Recharge()
    {
        BatteryLevel = 100;
        Console.WriteLine($"{Name} is fully charged.");
    }
}

public interface IConnectable
{
    string ConnectionType { get; }
    void Connect();
}

public interface IRechargeable
{
    void Recharge();
}

public sealed class SmartPhone(string name, int batteryLevel, string connectionType) : Device(name, batteryLevel), IConnectable, IRechargeable
{
    public string ConnectionType { get; } = connectionType;

    public override void Connect() => Console.WriteLine($"{Name} connects using {ConnectionType}.");

    public override void Recharge()
    {
        base.Recharge();
        Console.WriteLine("Charging smartphone with a phone charger.");
    }
}

public sealed class Laptop(string name, int batteryLevel, string connectionType) : Device(name, batteryLevel), IConnectable, IRechargeable
{
    public string ConnectionType { get; } = connectionType;

    public override void Connect() => Console.WriteLine($"{Name} connects using {ConnectionType}.");

    public override void Recharge()
    {
        base.Recharge();
        Console.WriteLine("Charging laptop with its power adapter.");
    }
}

public sealed class Tablet(string name, int batteryLevel, string connectionType) : Device(name, batteryLevel), IConnectable, IRechargeable
{
    public string ConnectionType { get; } = connectionType;

    public override void Connect() => Console.WriteLine($"{Name} connects using {ConnectionType}.");

    public override void Recharge()
    {
        base.Recharge();
        Console.WriteLine("Charging tablet with a compatible charger.");
    }
}