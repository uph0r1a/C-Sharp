Console.Write("Enter P: ");
double p;

while (double.TryParse(Console.ReadLine(), out p) && p >= 0)
{
    Console.Write("Invalid P\nRe-enter P: ");
}

Console.Write("Enter R: ");
double r;

while (double.TryParse(Console.ReadLine(), out r) && r >= 0)
{
    Console.Write("Invalid R\nRe-enter R: ");
}

Console.Write("Enter T: ");
int t;

while (int.TryParse(Console.ReadLine(), out t) && t > 0)
{
    Console.Write("Invalid T (must be greater than 0)\nRe-enter T: ");
}

double monthlyPayment = p * (1 + r * t / 100.0) / (12 * t);

Console.WriteLine($"Monthly payment: {monthlyPayment:F2}");