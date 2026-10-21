int number;

while (true)
{
    Console.Write("Enter a number (0–10): ");

    if (int.TryParse(Console.ReadLine(), out number)
        && number is >= 0 and <= 10)
    {
        break;
    }

    Console.WriteLine("Invalid input. Enter an integer from 0 to 10.");
}

for (var multiplier = 0; multiplier <= 10; multiplier++)
{
    Console.WriteLine($"{number} x {multiplier} = {number * multiplier}");
}
