var calculator = new Calculator();

Console.WriteLine($"""
    Sum of two numbers: {calculator.Add(5, 10)}
    Sum of three numbers: {calculator.Add(5, 10, 15)}
    Concatenation of strings: {calculator.Add("Hello", "World")}
    """);

public sealed class Calculator
{
    public int Add(int a, int b) => a + b;

    public int Add(int a, int b, int c) => a + b + c;

    public string Add(string a, string b) => string.Concat(a, b);
}
