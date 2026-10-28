using SalesOrder = Company.Sales.Order;

namespace Company.Sales
{
    public sealed class Order(string orderId, string customerName, string productName, int quantity, decimal unitPrice)
    {
        public string OrderId { get; init; } = !string.IsNullOrWhiteSpace(orderId) ? orderId : throw new ArgumentException("Order ID is required.", nameof(orderId));

        public string CustomerName { get; init; } = !string.IsNullOrWhiteSpace(customerName) ? customerName : throw new ArgumentException("Customer name is required.", nameof(customerName));

        public string ProductName { get; init; } = !string.IsNullOrWhiteSpace(productName) ? productName : throw new ArgumentException("Product name is required.", nameof(productName));

        public int Quantity { get; init; } = quantity > 0 ? quantity : throw new Exceptions.InvalidOrderQuantityException(quantity);

        public decimal UnitPrice { get; init; } = unitPrice > 0 ? unitPrice : throw new ArgumentOutOfRangeException(nameof(unitPrice), "Unit price must be positive.");

        public decimal CalculateTotal() => Quantity * UnitPrice;
    }
}

namespace Company.Warehouse
{
    public sealed class Order(string orderId, string productName, int quantity, string warehouseLocation)
    {
        public string OrderId { get; init; } = !string.IsNullOrWhiteSpace(orderId) ? orderId : throw new ArgumentException("Order ID is required.", nameof(orderId));

        public string ProductName { get; init; } = !string.IsNullOrWhiteSpace(productName) ? productName : throw new ArgumentException("Product name is required.", nameof(productName));

        public int Quantity { get; init; } = quantity > 0 ? quantity : throw new Exceptions.InvalidOrderQuantityException(quantity);

        public string WarehouseLocation { get; init; } = !string.IsNullOrWhiteSpace(warehouseLocation) ? warehouseLocation : throw new ArgumentException("Warehouse location is required.", nameof(warehouseLocation));

        public void ShipOrder() => Console.WriteLine($"{OrderId}: Shipped {Quantity} x {ProductName} from {WarehouseLocation}.");
    }
}

namespace Company.Finance
{
    public sealed class Order(string orderId, decimal totalAmount, bool paymentStatus)
    {
        public string OrderId { get; init; } = !string.IsNullOrWhiteSpace(orderId) ? orderId : throw new ArgumentException("Order ID is required.", nameof(orderId));

        public decimal TotalAmount { get; init; } = totalAmount > 0 ? totalAmount : throw new ArgumentOutOfRangeException(nameof(totalAmount), "Total amount must be positive.");

        public bool PaymentStatus { get; private set; } = paymentStatus;

        public void ProcessPayment()
        {
            if (!PaymentStatus)
            {
                throw new Exceptions.PaymentFailedException(OrderId);
            }

            Console.WriteLine($"{OrderId}: Payment successful — {TotalAmount:N0} VND.");
        }
    }
}

namespace Company.Exceptions
{
    public sealed class InvalidOrderQuantityException(int quantity) : Exception($"Order quantity must be greater than zero. Received: {quantity}.")
    {
        public int Quantity { get; } = quantity;
    }

    public sealed class PaymentFailedException(string orderId) : Exception($"Payment failed for order {orderId}.")
    {
        public string OrderId { get; } = orderId;
    }
}

public static class Program
{
    public static void Main()
    {
        try
        {
            var salesOrder = new SalesOrder("ORD-001", "Alice Nguyen", "Laptop", 2, 15_000_000m);
            var total = salesOrder.CalculateTotal();

            Console.WriteLine($"""
            Sales order: {salesOrder.OrderId}
            Customer: {salesOrder.CustomerName}
            Product: {salesOrder.ProductName}
            Total: {total:N0} VND
            """);

            var warehouseOrder = new Company.Warehouse.Order(salesOrder.OrderId, salesOrder.ProductName, salesOrder.Quantity, "Warehouse A");
            warehouseOrder.ShipOrder();

            var financeOrder = new Company.Finance.Order(salesOrder.OrderId, total, paymentStatus: true);
            financeOrder.ProcessPayment();
        }
        catch (Company.Exceptions.InvalidOrderQuantityException ex)
        {
            Console.WriteLine($"Quantity error: {ex.Message}");
        }
        catch (Company.Exceptions.PaymentFailedException ex)
        {
            Console.WriteLine($"Payment error: {ex.Message}");
        }

        try
        {
            var invalidOrder = new SalesOrder("ORD-002", "Bob Tran", "Keyboard", 0, 500_000m);
        }
        catch (Company.Exceptions.InvalidOrderQuantityException ex)
        {
            Console.WriteLine($"Quantity error: {ex.Message}\nInvalid quantity: {ex.Quantity}");
        }

        try
        {
            var failedPayment = new Company.Finance.Order("ORD-003", 2_000_000m, paymentStatus: false);
            failedPayment.ProcessPayment();
        }
        catch (Company.Exceptions.PaymentFailedException ex)
        {
            Console.WriteLine($"Payment error: {ex.Message}\nOrder ID: {ex.OrderId}");
        }
    }
}