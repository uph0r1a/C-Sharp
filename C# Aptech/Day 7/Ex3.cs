namespace UnitConverter;

public delegate double ConversionDelegate(double value);

public static class Program
{
    public static void Main()
    {
        Console.Write("""
            === UNIT CONVERTER ===
            1. Centimetres -> Inches
            2. Inches -> Centimetres
            3. Celsius -> Fahrenheit
            4. Fahrenheit -> Celsius
            Enter your choice: 
            """);

        var choice = Console.ReadLine();

        ConversionDelegate? conversion = choice switch
        {
            "1" => CmToInch,
            "2" => InchToCm,
            "3" => CelsiusToFahrenheit,
            "4" => FahrenheitToCelsius,
            _ => null
        };

        if (conversion is null)
        {
            Console.WriteLine("Invalid choice.");
            return;
        }

        Console.Write("Enter a value: ");
        if (!double.TryParse(Console.ReadLine(), out var value) || !double.IsFinite(value))
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
            return;
        }

        var result = conversion(value);
        Console.WriteLine($"Result: {result:N2}");
    }

    private static double CmToInch(double value) => value / 2.54;

    private static double InchToCm(double value) => value * 2.54;

    private static double CelsiusToFahrenheit(double value) => value * 9 / 5 + 32;

    private static double FahrenheitToCelsius(double value) => (value - 32) * 5 / 9;
}