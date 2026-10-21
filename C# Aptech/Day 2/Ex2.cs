var numberOfPizzas = ReadInt32("Enter number of pizzas: ", minimum: 0);
var time = ReadDouble("Enter time: ", minimum: 0);

decimal pricePerPizza = time switch
{
    > 30 => 0m,
    > 20 => 2.5m,
    > 10 => 4m,
    _ => 5.5m
};

var totalMoney = numberOfPizzas * pricePerPizza;

Console.WriteLine($"Total money: {totalMoney:N2}");

static int ReadInt32(string prompt, int minimum)
{
    while (true)
    {
        Console.Write(prompt);

        if (int.TryParse(Console.ReadLine(), out var value) && value >= minimum)
        {
            return value;
        }

        Console.WriteLine($"Enter an integer greater than or equal to {minimum}.");
    }
}

static double ReadDouble(string prompt, double minimum)
{
    while (true)
    {
        Console.Write(prompt);

        if (double.TryParse(Console.ReadLine(), out var value) && double.IsFinite(value) && value >= minimum)
        {
            return value;
        }

        Console.WriteLine($"Enter a finite number greater than or equal to {minimum}.");
    }
}
