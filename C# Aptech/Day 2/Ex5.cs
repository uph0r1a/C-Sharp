for (var number = 1; number <= 10; number++)
{
    Console.WriteLine($"\nBang cuu chuong {number}");

    for (var multiplier = 0; multiplier <= 10; multiplier++)
    {
        Console.WriteLine($"{number} x {multiplier} = {number * multiplier}");
    }
}
