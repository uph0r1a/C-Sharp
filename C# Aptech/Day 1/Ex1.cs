using System.Globalization;

var employee = new Employee(Name: ReadRequiredString("Employee name: "), Company: ReadRequiredString("Company name: "), BirthYear: ReadInt32("Birth year: ", 1900, DateTime.Today.Year), HouseNumber: ReadRequiredString("House number: "), Address: ReadRequiredString("Street/address: "), District: ReadRequiredString("District: "), MonthsWorked: ReadInt32("Months worked: ", 0), BaseSalary: ReadDecimal("Base salary: ", 0m), SalaryCoefficient: ReadDecimal("Salary coefficient: ", 0m));

Console.WriteLine($"""
    ===== Employee Information =====
    Name: {employee.Name}
    Company: {employee.Company}
    Birth year: {employee.BirthYear}
    Age: {employee.Age}
    House number: {employee.HouseNumber}
    Address: {employee.Address}
    District: {employee.District}
    Months worked: {employee.MonthsWorked}
    Base salary: {employee.BaseSalary:N2}
    Salary coefficient: {employee.SalaryCoefficient:N2}
    Salary: {employee.Salary:N2}
    Bonus: {employee.Bonus:N2}
    """);

static string ReadRequiredString(string prompt)
{
    while (true)
    {
        Console.Write(prompt);

        if (Console.ReadLine() is { } value && !string.IsNullOrWhiteSpace(value))
        {
            return value.Trim();
        }

        Console.WriteLine("Input cannot be empty.");
    }
}

static int ReadInt32(string prompt, int minimum, int? maximum = null)
{
    while (true)
    {
        Console.Write(prompt);

        if (int.TryParse(Console.ReadLine(), out var value) && value >= minimum && (maximum is null || value <= maximum.Value))
        {
            return value;
        }

        Console.WriteLine("Invalid integer value.");
    }
}

static decimal ReadDecimal(string prompt, decimal minimum)
{
    while (true)
    {
        Console.Write(prompt);

        if (decimal.TryParse(Console.ReadLine(), NumberStyles.Number, CultureInfo.InvariantCulture, out var value) && value >= minimum)
        {
            return value;
        }

        Console.WriteLine("Invalid numeric value. Use a dot as the decimal separator.");
    }
}

public sealed record Employee(string Name, string Company, int BirthYear, string HouseNumber, string Address, string District, int MonthsWorked, decimal BaseSalary, decimal SalaryCoefficient)
{
    public int Age => DateTime.Today.Year - BirthYear;

    public decimal Salary => BaseSalary * SalaryCoefficient;

    public decimal Bonus => Salary * MonthsWorked / 12m;
}
