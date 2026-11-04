namespace LibraryBorrowingSystem;

public interface IBorrowable
{
    float CalculateFee();

    void DisplayInfo();
}

public class BorrowRecord : IBorrowable
{
    private string _borrowerName = "Unknown";
    private float _dailyFee = 1f;
    private int _daysBorrowed = 1;
    public int RecordId { get; set; }

    public string BorrowerName
    {
        get => _borrowerName;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Trim().Length < 5 || value.Trim().Length > 80)
            {
                throw new ArgumentException("Borrower name must contain 5-80 characters.");
            }

            _borrowerName = value.Trim();
        }
    }

    public float DailyFee
    {
        get => _dailyFee;
        set
        {
            if (!float.IsFinite(value) || value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Daily fee must be greater than 0.");
            }

            _dailyFee = value;
        }
    }

    public int DaysBorrowed
    {
        get => _daysBorrowed;
        set
        {
            if (value < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Days borrowed must be at least 1.");
            }

            _daysBorrowed = value;
        }
    }

    public BorrowRecord()
    {
        RecordId = 0;
        BorrowerName = "Unknown";
        DailyFee = 1f;
        DaysBorrowed = 1;
    }

    public BorrowRecord(int recordId, string borrowerName, float dailyFee, int daysBorrowed)
    {
        RecordId = recordId;
        BorrowerName = borrowerName;
        DailyFee = dailyFee;
        DaysBorrowed = daysBorrowed;
    }

    public virtual float CalculateFee() => DailyFee * DaysBorrowed;

    public virtual void DisplayInfo() => Console.WriteLine($"Borrower: {BorrowerName} | Days: {DaysBorrowed} | Total Fee: ${CalculateFee():N2}");
}

public sealed class PremiumBorrower(int recordId, string borrowerName, float dailyFee, int daysBorrowed, float discountRate = 15f) : BorrowRecord(recordId, borrowerName, dailyFee, daysBorrowed)
{
    private float _discountRate = 15f;

    public float DiscountRate
    {
        get => _discountRate;
        set
        {
            if (!float.IsFinite(value) || value < 0 || value > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Discount rate must be between 0 and 100.");
            }

            _discountRate = value;
        }
    }

    public PremiumBorrower(int recordId, string borrowerName, float dailyFee, int daysBorrowed) : this(recordId, borrowerName, dailyFee, daysBorrowed, 15f)
    {
    }

    public PremiumBorrower(int recordId, string borrowerName, float dailyFee, int daysBorrowed, float discountRate) : base(recordId, borrowerName, dailyFee, daysBorrowed) => DiscountRate = discountRate;

    public override float CalculateFee() => base.CalculateFee() * (1 - DiscountRate / 100f);

    public override void DisplayInfo() => Console.WriteLine($"Premium Borrower: {BorrowerName} | Days: {DaysBorrowed} | Total Fee: ${CalculateFee():N2} (with {DiscountRate:N0}% discount)");
}

public sealed class BorrowManager
{
    private readonly List<BorrowRecord> _borrowRecords = [];

    public void AddRecord()
    {
        Console.WriteLine("\n========== ADD BORROW RECORD ==========");

        int id = ReadInt("Enter record ID: ", 0);

        if (_borrowRecords.Any(record => record.RecordId == id))
        {
            Console.WriteLine("Record ID already exists.");
            return;
        }

        string name = ReadBorrowerName();
        float fee = ReadFloat("Enter daily fee: ", 0.000001f);
        int days = ReadInt("Enter days borrowed: ", 1);
        int choice = ReadInt("Is this a premium borrower? (1 = Yes, 2 = No): ", 1, 2);
        BorrowRecord record;

        if (choice == 1)
        {
            record = new PremiumBorrower(id, name, fee, days);
        }
        else
        {
            record = new BorrowRecord(id, name, fee, days);
        }

        _borrowRecords.Add(record);
        Console.WriteLine("Borrow record added successfully.");
    }

    public void ViewAllRecords()
    {
        Console.WriteLine("\n========== ALL BORROW RECORDS ==========");

        if (_borrowRecords.Count == 0)
        {
            Console.WriteLine("Borrow record list is empty.");
            return;
        }

        foreach (var record in _borrowRecords)
        {
            record.DisplayInfo();
        }
    }

    public void UpdateDailyFee()
    {
        Console.WriteLine("\n========== UPDATE DAILY FEE ==========");

        if (_borrowRecords.Count == 0)
        {
            Console.WriteLine("Borrow record list is empty.");
            return;
        }

        int id = ReadInt("Enter record ID: ", 0);
        var record = FindRecord(id);

        if (record is null)
        {
            Console.WriteLine("Record not found.");
            return;
        }

        float newFee = ReadFloat("Enter new daily fee: ", 0.000001f);
        record.DailyFee = newFee;
        Console.WriteLine("Daily fee updated successfully.");
    }

    public void DeleteRecord()
    {
        Console.WriteLine("\n========== DELETE RECORD ==========");

        if (_borrowRecords.Count == 0)
        {
            Console.WriteLine("Borrow record list is empty.");
            return;
        }

        int id = ReadInt("Enter record ID: ", 0);
        var record = FindRecord(id);

        if (record is null)
        {
            Console.WriteLine("Record not found.");
            return;
        }

        _borrowRecords.Remove(record);

        Console.WriteLine("Record deleted successfully.");
    }

    public void CalculateFeeById()
    {
        Console.WriteLine("\n========== CALCULATE BORROWING FEE ==========");

        int id = ReadInt("Enter record ID: ", 0);
        var record = FindRecord(id);

        if (record is null)
        {
            Console.WriteLine("Record not found.");
            return;
        }

        Console.WriteLine($"Total borrowing fee: ${record.CalculateFee():N2}");
    }

    private BorrowRecord? FindRecord(int id) => _borrowRecords.FirstOrDefault(record => record.RecordId == id);

    private static string ReadBorrowerName()
    {
        while (true)
        {
            Console.Write("Enter borrower name: ");
            string? name = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(name) && name.Trim().Length >= 5 && name.Trim().Length <= 80)
            {
                return name.Trim();
            }

            Console.WriteLine("Invalid name. Enter between 5 and 80 characters.");
        }
    }

    private static int ReadInt(string message, int min, int max = int.MaxValue)
    {
        while (true)
        {
            Console.Write(message);

            if (int.TryParse(Console.ReadLine(), out int value) && value >= min && value <= max)
            {
                return value;
            }

            Console.WriteLine($"Please enter an integer between {min} and {max}.");
        }
    }

    private static float ReadFloat(string message, float min)
    {
        while (true)
        {
            Console.Write(message);

            if (float.TryParse(Console.ReadLine(), out float value) && float.IsFinite(value) && value >= min)
            {
                return value;
            }

            Console.WriteLine($"Please enter a valid number greater than or equal to {min}.");
        }
    }
}

public static class Library
{
    public static void Main()
    {
        var manager = new BorrowManager();
        bool isExit = false;

        while (!isExit)
        {
            Console.Write("""
                
                ===== LIBRARY BORROWING MENU =====
                1. Add New Borrow Record / Premium Borrower
                2. View All Records
                3. Update Daily Fee
                4. Delete Record
                5. Calculate Fee by Record ID
                6. Exit
                Enter your choice: 
                """);

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                continue;
            }

            try
            {
                choice switch
                {
                    1 => manager.AddRecord(),
                    2 => manager.ViewAllRecords(),
                    3 => manager.UpdateDailyFee(),
                    4 => manager.DeleteRecord(),
                    5 => manager.CalculateFeeById(),
                    6 => () =>
                    {
                        isExit = true;
                        Console.WriteLine("Exiting Library Borrowing System...");
                    }
                    ,
                    _ => Console.Write("Invalid choice\nRe-enter your choice: ")
                };
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Input error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }
    }
}