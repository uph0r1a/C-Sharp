Console.Write("Enter n: ");
int n;

while (!(int.TryParse(Console.ReadLine(), out n) && n >= 0 && n <= 20))
{
    Console.Write("Invalid n\nRe-enter n: ");
}

int[] arr = new int[n];

for (int i = 0; i < n; i++)
{
    Console.Write($"Enter number {i + 1}: ");

    while (!int.TryParse(Console.ReadLine(), out arr[i]))
    {
        Console.Write("Invalid number. Re-enter: ");
    }
}
int[] even = [.. arr.Where(n => n % 2 == 0)];
int[] odd = [.. arr.Where(n => n % 2 != 0)];

Console.WriteLine($"Even number: {string.Join(" ", even)}\nOdd number: {string.Join(" ", odd)}\nSum even: {even.Sum()}");