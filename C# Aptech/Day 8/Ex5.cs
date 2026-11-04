namespace BankTransactionDemo;

public enum TransType
{
    Deposit,
    Withdraw,
    Transfer
}

public class Transaction(string id, string accountNumber, decimal amount, TransType transType, DateTime transactionDate, string description)
{
    public string TransactionId { get; } = !string.IsNullOrWhiteSpace(id) ? id.Trim() : throw new ArgumentException("Transaction ID cannot be empty.");
    public string AccountNumber { get; } = !string.IsNullOrWhiteSpace(accountNumber) ? accountNumber.Trim() : throw new ArgumentException("Account number cannot be empty.");
    public decimal Amount { get; } = amount > 0 ? amount : throw new ArgumentOutOfRangeException(nameof(amount));
    public TransType TransactionType { get; } = Enum.IsDefined(transType) ? transType : throw new ArgumentException("Invalid transaction type.");
    public DateTime TransactionDate { get; } = transactionDate;
    public string Description { get; } = !string.IsNullOrWhiteSpace(description) ? description.Trim() : throw new ArgumentException("Description cannot be empty.");

    public override string ToString() => $"""
        Transaction ID: {TransactionId}
        Account Number: {AccountNumber}
        Amount: {Amount:N0} VND
        Type: {TransactionType}
        Date: {TransactionDate:dd/MM/yyyy HH:mm}
        Description: {Description}
        """;
}

public interface ITransactionRepository<T> where T : Transaction
{
    void AddTransaction(T transaction);

    IEnumerable<T> GetAllTransactions();

    IEnumerable<T> GetTransactionsByDateRange(DateTime from, DateTime to);

    IEnumerable<T> GetLargeTransactions(decimal minAmount);

    IEnumerable<T> GetTransactionsByType(string type);
}

public sealed class TransactionRepository : ITransactionRepository<Transaction>
{
    private readonly List<Transaction> _transactions = [];

    public void AddTransaction(Transaction transaction)
    {
        ArgumentNullException.ThrowIfNull(transaction);

        if (_transactions.Any(t => t.TransactionId.Equals(transaction.TransactionId, StringComparison.OrdinalIgnoreCase)))
        {
            throw new ArgumentException($"Transaction ID {transaction.TransactionId} already exists.");
        }

        _transactions.Add(transaction);
    }

    public IEnumerable<Transaction> GetAllTransactions()
    {
        foreach (var transaction in _transactions)
        {
            yield return transaction;
        }
    }

    public IEnumerable<Transaction> GetTransactionsByDateRange(DateTime from, DateTime to)
    {
        if (from > to)
        {
            throw new ArgumentException("Start date must not be later than end date.");
        }

        foreach (var transaction in _transactions)
        {
            if (transaction.TransactionDate >= from && transaction.TransactionDate <= to)
            {
                yield return transaction;
            }
        }
    }

    public IEnumerable<Transaction> GetLargeTransactions(decimal minAmount)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(minAmount);

        foreach (var transaction in _transactions)
        {
            if (transaction.Amount >= minAmount)
            {
                yield return transaction;
            }
        }
    }

    public IEnumerable<Transaction> GetTransactionsByType(string type)
    {
        if (!Enum.TryParse<TransType>(type, ignoreCase: true, out var transactionType) || !Enum.IsDefined(transactionType))
        {
            throw new ArgumentException("Invalid transaction type. Use Deposit, Withdraw or Transfer.", nameof(type));
        }

        foreach (var transaction in _transactions)
        {
            if (transaction.TransactionType == transactionType)
            {
                yield return transaction;
            }
        }
    }
}

public static class Bank
{
    private static readonly ITransactionRepository<Transaction> Repository = new TransactionRepository();

    public static void Main()
    {
        SeedTransactions();
        bool isExit = false;

        while (!isExit)
        {
            Console.Write("""
                
                ===== BANK TRANSACTION MANAGEMENT =====
                1. Display all transactions
                2. Search transactions by date range
                3. Search large transactions
                4. Search transactions by type
                0. Exit
                Enter your choice: 
                """);

            int choice = ReadInt(0, 4);

            choice switch
            {
                1 => DisplayTransactions(Repository.GetAllTransactions()),
                2 => SearchByDateRange(),
                3 => SearchLargeTransactions(),
                4 => SearchByType(),
                0 => () =>
                {
                    isExit = true;
                    Console.WriteLine("Exiting Bank Transaction System...");
                }
            };
        }
    }

    private static void SeedTransactions()
    {
        Repository.AddTransaction(new Transaction("TX001", "ACC001", 5_000_000m, TransType.Deposit, new DateTime(2026, 10, 1, 9, 30, 0), "Salary deposit"));
        Repository.AddTransaction(new Transaction("TX002", "ACC001", 2_000_000m, TransType.Withdraw, new DateTime(2026, 10, 2, 10, 15, 0), "ATM withdrawal"));
        Repository.AddTransaction(new Transaction("TX003", "ACC002", 15_000_000m, TransType.Deposit, new DateTime(2026, 10, 3, 14, 20, 0), "Business income"));
        Repository.AddTransaction(new Transaction("TX004", "ACC001", 8_000_000m, TransType.Transfer, new DateTime(2026, 10, 4, 11, 0, 0), "Transfer to ACC003"));
        Repository.AddTransaction(new Transaction("TX005", "ACC003", 500_000m, TransType.Withdraw, new DateTime(2026, 10, 5, 16, 45, 0), "Shopping"));
        Repository.AddTransaction(new Transaction("TX006", "ACC002", 25_000_000m, TransType.Transfer, new DateTime(2026, 10, 6, 8, 30, 0), "Property payment"));
        Repository.AddTransaction(new Transaction("TX007", "ACC001", 1_000_000m, TransType.Deposit, new DateTime(2026, 10, 7, 13, 10, 0), "Cash deposit"));
        Repository.AddTransaction(new Transaction("TX008", "ACC003", 12_000_000m, TransType.Withdraw, new DateTime(2026, 10, 8, 15, 0, 0), "Car payment"));
    }

    private static void SearchByDateRange()
    {
        Console.Write("\nEnter start date (dd/MM/yyyy): ");
        DateTime from = ReadDate();

        Console.Write("Enter end date (dd/MM/yyyy): ");
        DateTime to = ReadDate();

        to = to.Date.AddDays(1).AddTicks(-1);

        DisplayTransactions(Repository.GetTransactionsByDateRange(from.Date, to));
    }

    private static void SearchLargeTransactions()
    {
        Console.Write("\nEnter minimum transaction amount (VND): ");
        decimal minAmount = ReadDecimal();

        DisplayTransactions(Repository.GetLargeTransactions(minAmount));
    }

    private static void SearchByType()
    {
        Console.Write("""
            
            Transaction types:
            1. Deposit
            2. Withdraw
            3. Transfer
            Enter transaction type: 
            """);

        int choice = ReadInt(1, 3);

        string type = choice switch
        {
            1 => nameof(TransType.Deposit),
            2 => nameof(TransType.Withdraw),
            3 => nameof(TransType.Transfer),
            _ => throw new InvalidOperationException()
        };

        DisplayTransactions(Repository.GetTransactionsByType(type));
    }

    private static void DisplayTransactions(IEnumerable<Transaction> transactions)
    {
        Console.WriteLine("\n========== TRANSACTION RESULTS ==========");

        int count = 0;
        foreach (var transaction in transactions)
        {
            Console.WriteLine(transaction);
            Console.WriteLine(new string('-', 45));
            count++;
        }

        Console.WriteLine($"Total matching transactions: {count}");
    }

    private static int ReadInt(int min, int max)
    {
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int value) && value >= min && value <= max)
            {
                return value;
            }

            Console.Write($"Please enter a number between {min} and {max}: ");
        }
    }

    private static decimal ReadDecimal()
    {
        while (true)
        {
            if (decimal.TryParse(Console.ReadLine(), out decimal value) && value >= 0)
            {
                return value;
            }

            Console.Write("Invalid amount. Enter a non-negative number: ");
        }
    }

    private static DateTime ReadDate()
    {
        while (true)
        {
            if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime date))
            {
                return date;
            }

            Console.Write("Invalid date. Use dd/MM/yyyy: ");
        }
    }
}