namespace Single_Delegate
{
    public delegate double MathOperation(double x, double y);

    public sealed class Calculator
    {
        public double Add(double x, double y) => x + y;

        public double Subtract(double x, double y) => x - y;

        public double Multiply(double x, double y) => x * y;

        public double Divide(double x, double y) => y != 0 ? x / y : throw new DivideByZeroException("Cannot divide by zero.");
    }

    public static class Program
    {
        public static void Main()
        {
            var calculator = new Calculator();
            MathOperation? operation = null;

            Console.Write("""
                === Calculator ===
                1. Addition
                2. Subtraction
                3. Multiplication
                4. Division
                Enter your choice: 
                """);

            var choice = Console.ReadLine();
            operation = choice switch
            {
                "1" => calculator.Add,
                "2" => calculator.Subtract,
                "3" => calculator.Multiply,
                "4" => calculator.Divide,
                _ => null
            };

            if (operation is null)
            {
                Console.WriteLine("Invalid choice.");
                return;
            }

            if (!TryReadDouble("Enter a: ", out var a) || !TryReadDouble("Enter b: ", out var b))
            {
                Console.WriteLine("Invalid number. Please enter a valid numeric value.");
                return;
            }

            try
            {
                Console.WriteLine($"Result: {operation(a, b):G}");
            }
            catch (DivideByZeroException exception)
            {
                Console.WriteLine($"Error: {exception.Message}");
            }
        }

        private static bool TryReadDouble(string prompt, out double value)
        {
            Console.Write(prompt);
            return double.TryParse(Console.ReadLine(), out value) && double.IsFinite(value);
        }
    }
}