Calculator calc = new();

Console.WriteLine($"""
Sum of two numbers: {calc.Add(5, 10)}
Sum of three numbers: {calc.Add(5, 10, 15)}
Concatenation of strings: {calc.Add("Hello", "World")}
""");

public class Calculator
{
    public int Add(int a, int b) => a + b;

    public int Add(int a, int b, int c) => a + b + c;

    public string Add(string a, string b) => a + b;
}
