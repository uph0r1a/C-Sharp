var account = new BankAccount(1001, "Nguyen Van A", 1_000_000m);

account.DisplayInfo();

account.Deposit(500_000m);
account.Withdraw(200_000m);

Console.WriteLine("\nAfter transactions:");
account.DisplayInfo();

public sealed class BankAccount(int accountNumber, string owner, decimal balance)
{
    public int AccountNumber { get; } = accountNumber > 0 ? accountNumber : throw new ArgumentOutOfRangeException(nameof(accountNumber), accountNumber, "Account number must be positive.");

    private string Owner { get; } = !string.IsNullOrWhiteSpace(owner) ? owner : throw new ArgumentException("Owner cannot be empty.", nameof(owner));

    public decimal Balance { get; private set; } = balance >= 0 ? balance : throw new ArgumentOutOfRangeException(nameof(balance), balance, "Initial balance cannot be negative.");

    public void Deposit(decimal amount)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);
        Balance = checked(Balance + amount);
    }

    public void Withdraw(decimal amount)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);

        if (amount > Balance)
        {
            throw new InvalidOperationException("Insufficient funds.");
        }

        Balance -= amount;
    }

    public void DisplayInfo() =>
        Console.WriteLine($"""
            Account Number: {AccountNumber}
            Owner: {Owner}
            Balance: {Balance:N2}
            """);
}
