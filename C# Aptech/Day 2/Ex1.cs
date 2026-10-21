while (true)
{
    Console.Write("Enter an integer: ");

    if (int.TryParse(Console.ReadLine(), out var number))
    {
        Console.WriteLine(number % 2 == 0 ? "Even" : "Odd");
        break;
    }

    Console.WriteLine("Invalid input. Please enter a valid integer.");
}
