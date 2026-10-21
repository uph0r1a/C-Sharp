Console.Write("Enter n (1-20): ");

int n;
while (!int.TryParse(Console.ReadLine(), out n) || n is < 1 or > 20)
{
    Console.Write("Invalid n. Enter an integer from 1 to 20: ");
}

var numbers = new int[n];

for (var i = 0; i < numbers.Length; i++)
{
    Console.Write($"Enter number {i + 1}: ");

    while (!int.TryParse(Console.ReadLine(), out numbers[i]))
    {
        Console.Write("Invalid integer. Re-enter: ");
    }
}

var even = numbers.Where(static number => number % 2 == 0).ToArray();
var odd = numbers.Where(static number => number % 2 != 0).ToArray();

var evenSum = even.Sum(static number => (long)number);

Console.WriteLine($"""
    Even numbers: {string.Join(" ", even)}
    Odd numbers:  {string.Join(" ", odd)}
    Sum of even numbers: {evenSum}
    """);
