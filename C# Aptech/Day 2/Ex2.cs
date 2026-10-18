Console.Write("Enter number of pizza: ");
int number;
while (int.TryParse(Console.ReadLine(), out number) && number >= 0)
{
    Console.Write("Invalid number of pizzas\nRe-enter number of pizza: ");
}

Console.Write("Enter time: ");
double time;
while (double.TryParse(Console.ReadLine(), out time) && time >= 0)
{
    Console.Write("Invalid time\nRe-enter time: ");
}

Console.WriteLine("Total money: " + number * (time > 30 ? 0 : time > 20 ? 2.5 : time > 10 ? 4 : 5.5));