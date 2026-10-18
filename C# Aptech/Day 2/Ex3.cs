Console.Write("Enter a number: ");
int num;
while (!(int.TryParse(Console.ReadLine(), out num) && 0 <= num && num <= 10))
{
    Console.Write("Invalid number\nRe-enter a number: ");
}

for (int i = 0; i <= 10; i++)
{
    Console.WriteLine($"{num} x {i} = {num * i}");
}