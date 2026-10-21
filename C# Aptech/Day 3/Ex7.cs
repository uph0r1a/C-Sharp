var mathHelper = new MathHelper();

Console.WriteLine($"""
    Product of two integers: {mathHelper.Multiply(5, 10)}
    Product of two doubles: {mathHelper.Multiply(5.5, 2.5)}
    Product of three integers: {mathHelper.Multiply(5, 10, 15)}
    Repeated string: {mathHelper.Multiply("Hello", 3)}
    """);

public sealed class MathHelper
{
    public int Multiply(int a, int b) => a * b;

    public double Multiply(double a, double b) => a * b;

    public int Multiply(int a, int b, int c) => a * b * c;

    public string Multiply(string text, int times)
    {
        ArgumentNullException.ThrowIfNull(text);
        ArgumentOutOfRangeException.ThrowIfNegative(times);

        return string.Create(checked(text.Length * times), (Text: text, Times: times), static (span, state) =>
            {
                for (var i = 0; i < state.Times; i++)
                {
                    state.Text.AsSpan().CopyTo(span[(i * state.Text.Length)..]);
                }
            });
    }
}
