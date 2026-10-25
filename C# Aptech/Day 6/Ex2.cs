public static class Bank
{
    public static void Main()
    {
        try
        {
            var account = new BankAccount("123456", 2_000_000m);
            account.Withdraw(3_000_000m);
        }
        catch (BalanceException exception)
        {
            Console.WriteLine($"""
            Error: {exception.Message}
            Current balance: {exception.CurrentBalance:N0} VND
            Requested amount: {exception.RequestAmount:N0} VND
            """);
        }
    }
}

public sealed class BankAccount(string accountNumber, decimal balance)
{
    public string AccountNumber { get; } = !string.IsNullOrWhiteSpace(accountNumber) ? accountNumber : throw new ArgumentException("Account number cannot be empty.", nameof(accountNumber));
    public decimal Balance { get; private set; } = balance >= 0 ? balance : throw new ArgumentOutOfRangeException(nameof(balance), "Balance cannot be negative.");

    public void Withdraw(decimal amount)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);

        if (amount > Balance)
        {
            throw new BalanceException(Balance, amount);
        }

        Balance -= amount;
        Console.WriteLine($"Withdraw {amount:N0} VND successfully.");
    }
}

public sealed class BalanceException(decimal currentBalance, decimal requestAmount) : Exception($"Current balance {currentBalance:N0} VND is insufficient to withdraw {requestAmount:N0} VND.")
{
    public decimal CurrentBalance { get; } = currentBalance;
    public decimal RequestAmount { get; } = requestAmount;
}