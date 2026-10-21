using System.Globalization;

var loanAmount = ReadDecimal("Loan amount (P): ", minimum: 0m);
var annualInterestRate = ReadDecimal("Annual interest rate (R%): ", minimum: 0m);
var termInYears = ReadInt32("Loan term (T years): ", minimum: 1);

var monthlyPayment = loanAmount * (1m + annualInterestRate * termInYears / 100m) / (12m * termInYears);

Console.WriteLine($"""
    ===== Loan Payment Summary =====
    Loan amount: {loanAmount:N2}
    Annual interest rate: {annualInterestRate:N2}%
    Loan term: {termInYears} years
    Monthly payment: {monthlyPayment:N2}
    """);

static decimal ReadDecimal(string prompt, decimal minimum)
{
    while (true)
    {
        Console.Write(prompt);

        if (decimal.TryParse(Console.ReadLine(), NumberStyles.Number, CultureInfo.InvariantCulture, out var value) && value >= minimum)
        {
            return value;
        }

        Console.WriteLine($"Enter a valid number greater than or equal to {minimum}.");
    }
}

static int ReadInt32(string prompt, int minimum)
{
    while (true)
    {
        Console.Write(prompt);

        if (int.TryParse(Console.ReadLine(), out var value) && value >= minimum)
        {
            return value;
        }

        Console.WriteLine($"Enter a valid integer greater than or equal to {minimum}.");
    }
}
