public interface IBorrowable
{
    public float CalculateFee();
    public void DisplayInfo();
}

public class BorrowRecord(int id, string name, float fee, int day) : IBorrowable
{
    public int RecordID { get; set; } = id >= 0 ? id : throw new ArgumentOutOfRangeException();
    public string BorrowerName { get; set; } = !string.IsNullOrWhiteSpace(name) && name.Length >= 5 && name.Length <= 80 ? name : throw new ArgumentNullException();
    public float DailyFee { get; set; } = fee > 0 ? fee : throw new ArgumentOutOfRangeException();
    public int DaysBorrowed { get; set; } = day >= 1 ? day : throw new ArgumentOutOfRangeException();

    public BorrowRecord()
    {

    }

    public float CalculateFee() => DailyFee * DaysBorrowed;
    public void DisplayInfo() => Console.WriteLine($"Borrower: {BorrowerName} | Days: {DaysBorrowed} | Total Fee: ${CalculateFee()}");
}

public class PremiumBorrower(int id, string name, float fee, int day, float rate = 15) : BorrowRecord(id, name, fee, day)
{
    public float DiscountRate { get; set; } = rate > 0 ? rate : throw new ArgumentOutOfRangeException();

    public float CalculateFee() => (DailyFee * DaysBorrowed) * (1 - DiscountRate / 100);
    public void DisplayInfo() => Console.WriteLine($"Premium Borrower: {BorrowerName} | Days: {DaysBorrowed} | Total Fee: ${CalculateFee()} (with discount)");
}

public class BorrowManager
{
    public List<BorrowRecord> borrowRecords;

    public void AddRecord()
    {
        Console.Write("Enter record ID: ");
        int id;
        while (true)
        {
            try
            {
                id = int.TryParse(Console.ReadLine());
                if (!borrowRecords.Find(r => r.RecordID == id))
                {
                    break;
                }
                Console.Write("Invalid record ID\nRe-enter record ID: ");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        Console.Write("Enter borrower name: ");
        string name;
        while (true)
        {
            try
            {
                name = Console.ReadLine();
                if (name.Length >= 5 && name.Length <= 80)
                {
                    break;
                }
                Console.Write("Invalid borrower name\nRe-enter borrower name: ");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        Console.Write("Enter daily fee: ");
        float fee;
        while (true)
        {
            try
            {
                fee = Console.ReadLine();
                if (name.Length >= 5 && name.Length <= 80)
                {
                    break;
                }
                Console.Write("Invalid borrower name\nRe-enter borrower name: ");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        Console.Write("Enter borrower name: ");
        string name;
        while (true)
        {
            try
            {
                name = Console.ReadLine();
                if (name.Length >= 5 && name.Length <= 80)
                {
                    break;
                }
                Console.Write("Invalid borrower name\nRe-enter borrower name: ");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}