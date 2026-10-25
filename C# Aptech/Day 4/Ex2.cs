IPayment[] paymentMethods = [new CreditCardPayment(), new PayPalPayment(), new BankTransferPayment()];

foreach (var payment in paymentMethods)
{
    payment.ProcessPayment(250m);
    payment.Refund(50m);
    Console.WriteLine();
}

public interface IPayment
{
    void ProcessPayment(decimal amount);

    void Refund(decimal amount);
}

public sealed class CreditCardPayment : IPayment
{
    public void ProcessPayment(decimal amount)
    {
        ValidateAmount(amount);
        Console.WriteLine($"Processing credit card payment: {amount:C}");
    }

    public void Refund(decimal amount)
    {
        ValidateAmount(amount);
        Console.WriteLine($"Refunding credit card payment: {amount:C}");
    }

    private static void ValidateAmount(decimal amount)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);
    }
}

public sealed class PayPalPayment : IPayment
{
    public void ProcessPayment(decimal amount)
    {
        ValidateAmount(amount);
        Console.WriteLine($"Processing PayPal payment: {amount:C}");
    }

    public void Refund(decimal amount)
    {
        ValidateAmount(amount);
        Console.WriteLine($"Refunding PayPal payment: {amount:C}");
    }

    private static void ValidateAmount(decimal amount)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);
    }
}

public sealed class BankTransferPayment : IPayment
{
    public void ProcessPayment(decimal amount)
    {
        ValidateAmount(amount);
        Console.WriteLine($"Processing bank transfer: {amount:C}");
    }

    public void Refund(decimal amount)
    {
        ValidateAmount(amount);
        Console.WriteLine($"Refunding bank transfer: {amount:C}");
    }

    private static void ValidateAmount(decimal amount)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);
    }
}