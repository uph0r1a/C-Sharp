Console.WriteLine("Enter n (1-20):");

int n;
while (true)
{
    if (int.TryParse(Console.ReadLine(), out n) && n is >= 1 and <= 20)
    {
        break;
    }

    Console.WriteLine("Invalid n. Enter an integer from 1 to 20:");
}

var numbers = new int[n];

for (var i = 0; i < numbers.Length; i++)
{
    while (true)
    {
        Console.Write($"Enter number {i + 1}: ");

        if (int.TryParse(Console.ReadLine(), out numbers[i]))
        {
            break;
        }

        Console.WriteLine("Invalid integer. Please try again.");
    }
}

var max = numbers.Max();
var sum = numbers.Sum(x => (long)x);

Console.WriteLine($"Maximum: {max}\nSum: {sum}");
