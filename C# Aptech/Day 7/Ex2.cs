namespace OrderSystemDemo;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("=== HỆ THỐNG QUẢN LÝ ĐƠN HÀNG ===\n");

        var processor = new OrderProcessor();
        var emailService = new EmailService();
        var smsService = new SMSService();
        var loggingService = new LoggingService();
        var inventoryService = new InventoryService();

        processor.OrderCreated += emailService.OnOrderCreated;
        processor.OrderCreated += smsService.OnOrderCreated;
        processor.OrderCreated += loggingService.OnOrderCreated;
        processor.OrderCreated += inventoryService.OnOrderCreated;
        processor.OrderShipped += emailService.OnOrderShipped;
        processor.OrderShipped += loggingService.OnOrderShipped;

        processor.CreateOrder("DH001", "Nguyễn Văn A", "iPhone 15", 28_000_000m);

        Console.WriteLine("----------------------------------------");
        processor.CreateOrder("DH002", "Trần Thị B", "Laptop Dell", 32_000_000m);
        Console.WriteLine("----------------------------------------");

        processor.ShipOrder("DH001");
        Console.WriteLine("----------------------------------------");

        try
        {
            processor.CreateOrder("DH001", "Nguyễn Văn C", "Keyboard", 500_000m);
        }
        catch (ArgumentException exception)
        {
            Console.WriteLine($"[ERROR] {exception.Message}");
        }

        try
        {
            processor.ShipOrder("DH999");
        }
        catch (KeyNotFoundException exception)
        {
            Console.WriteLine($"[ERROR] {exception.Message}");
        }

        Console.WriteLine("\n=== Hệ thống xử lý đơn hàng hoàn tất ===");
    }
}

public sealed class OrderEventArgs(string orderId, string customerName, string productName, decimal amount) : EventArgs
{
    public string OrderId { get; } = orderId;
    public string CustomerName { get; } = customerName;
    public string ProductName { get; } = productName;
    public decimal Amount { get; } = amount;
}

public sealed class Order(string orderId, string customerName, string productName, decimal amount)
{
    public string OrderId { get; } = orderId;
    public string CustomerName { get; } = customerName;
    public string ProductName { get; } = productName;
    public decimal Amount { get; } = amount;

    public bool IsShipped { get; private set; }

    public void MarkAsShipped() => IsShipped = true;
}

public class OrderProcessor
{
    private readonly Dictionary<string, Order> _orders = new(StringComparer.OrdinalIgnoreCase);
    public event EventHandler<OrderEventArgs>? OrderCreated;
    public event EventHandler<OrderEventArgs>? OrderShipped;

    public void CreateOrder(string orderId, string customerName, string productName, decimal amount)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(orderId);
        ArgumentException.ThrowIfNullOrWhiteSpace(customerName);
        ArgumentException.ThrowIfNullOrWhiteSpace(productName);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);

        if (_orders.ContainsKey(orderId))
        {
            throw new ArgumentException($"Order ID '{orderId}' already exists.", nameof(orderId));
        }

        var order = new Order(orderId, customerName, productName, amount);
        _orders.Add(order.OrderId, order);

        Console.WriteLine($"[PROCESSOR] Đang tạo đơn hàng {order.OrderId} " + $"cho khách {order.CustomerName}...");
        OnOrderCreated(order);
    }

    public void ShipOrder(string orderId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(orderId);

        if (!_orders.TryGetValue(orderId, out var order))
        {
            throw new KeyNotFoundException($"Order '{orderId}' was not found.");
        }

        if (order.IsShipped)
        {
            throw new InvalidOperationException($"Order '{orderId}' has already been shipped.");
        }

        order.MarkAsShipped();
        Console.WriteLine($"[PROCESSOR] Đang giao đơn hàng {order.OrderId}...");
        OnOrderShipped(order);
    }

    protected virtual void OnOrderCreated(Order order)
    {
        OrderCreated?.Invoke(this, new OrderEventArgs(order.OrderId, order.CustomerName, order.ProductName, order.Amount));
    }

    protected virtual void OnOrderShipped(Order order)
    {
        OrderShipped?.Invoke(this, new OrderEventArgs(order.OrderId, order.CustomerName, order.ProductName, order.Amount));
    }
}

public sealed class EmailService
{
    public void OnOrderCreated(object? sender, OrderEventArgs e) => Console.WriteLine($"[EMAIL] Gửi email xác nhận đơn hàng {e.OrderId} " + $"cho {e.CustomerName}");

    public void OnOrderShipped(object? sender, OrderEventArgs e) => Console.WriteLine($"[EMAIL] Thông báo đơn hàng {e.OrderId} " + "đã được giao.");
}

public sealed class SMSService
{
    public void OnOrderCreated(object? sender, OrderEventArgs e) => Console.WriteLine($"[SMS] Gửi tin nhắn cho khách {e.CustomerName}: " + $"Đơn hàng {e.OrderId} đã tạo.");
}

public sealed class LoggingService
{
    public void OnOrderCreated(object? sender, OrderEventArgs e) => Console.WriteLine($"[LOG] Đã ghi log đơn hàng {e.OrderId} - " + $"Giá trị: {e.Amount:N0} VND");

    public void OnOrderShipped(object? sender, OrderEventArgs e) => Console.WriteLine($"[LOG] Đã ghi log giao hàng cho đơn {e.OrderId}.");
}

public sealed class InventoryService
{
    public void OnOrderCreated(object? sender, OrderEventArgs e) => Console.WriteLine($"[INVENTORY] Đang xử lý kho sản phẩm {e.ProductName} " + $"cho đơn {e.OrderId}.");
}