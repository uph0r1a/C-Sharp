Console.Write("Enter P: ");
double p;

while (true)
{
    if (double.TryParse(Console.ReadLine(), out p) && p >= 0)
    {
        break;
    }

    Console.Write("Invalid P\nRe-enter P: ");
}

Console.Write("Enter R: ");
double r;

while (true)
{
    if (double.TryParse(Console.ReadLine(), out r) && r >= 0)
    {
        break;
    }

    Console.Write("Invalid R\nRe-enter R: ");
}

Console.Write("Enter T: ");
int t;

while (true)
{
    if (int.TryParse(Console.ReadLine(), out t) && t > 0)
    {
        break;
    }

    Console.Write("Invalid T (must be greater than 0)\nRe-enter T: ");
}

double monthlyPayment = p * (1 + r * t / 100.0) / (12 * t);

Console.WriteLine($"Monthly payment: {monthlyPayment:F2}");