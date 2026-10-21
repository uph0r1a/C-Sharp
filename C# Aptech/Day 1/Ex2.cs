var denominations = new[] { 200, 100, 50, 20, 10, 5 };
var coinCounts = new Dictionary<int, int>(denominations.Length);

foreach (var denomination in denominations)
{
    coinCounts[denomination] = ReadNonNegativeInt($"Number of {denomination}c coins: ");
}

var totalCents = coinCounts.Sum(static coin => (long)coin.Key * coin.Value);

Console.WriteLine("\n===== Coin Bag Summary =====");

foreach (var (denomination, count) in coinCounts)
{
    Console.WriteLine($"{denomination,3}c: {count:N0}");
}

Console.WriteLine($"Total value: {totalCents:N0} cents");

static int ReadNonNegativeInt(string prompt)
{
    while (true)
    {
        Console.Write(prompt);

        if (int.TryParse(Console.ReadLine(), out var value)
            && value >= 0)
        {
            return value;
        }

        Console.WriteLine("Enter a non-negative integer.");
    }
}
