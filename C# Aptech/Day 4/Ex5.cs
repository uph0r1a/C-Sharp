List<Employee> employees = [new FullTimeEmployee("Alice", 2_000m, 300m), new PartTimeEmployee("Bob", 20m, 80m), new ContractEmployee("Charlie", 1_500m)];
var totalSalary = employees.Sum(static employee => employee.CalculateSalary());

foreach (var employee in employees)
{
    Console.WriteLine($"{employee.Name}: {employee.CalculateSalary():C}");
}

Console.WriteLine($"Total company salary: {totalSalary:C}");

public abstract class Employee(string name)
{
    public string Name { get; } = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentException("Name cannot be empty.", nameof(name));

    public abstract decimal CalculateSalary();
}

public interface IBonus
{
    decimal CalculateBonus();
}

public sealed class FullTimeEmployee(string name, decimal monthlySalary, decimal bonus) : Employee(name), IBonus
{
    public decimal MonthlySalary { get; } = monthlySalary > 0 ? monthlySalary : throw new ArgumentOutOfRangeException(nameof(monthlySalary));

    public decimal Bonus { get; } = bonus >= 0 ? bonus : throw new ArgumentOutOfRangeException(nameof(bonus));

    public override decimal CalculateSalary() => MonthlySalary + CalculateBonus();

    public decimal CalculateBonus() => Bonus;
}

public sealed class PartTimeEmployee(string name, decimal hourlyRate, decimal hoursWorked) : Employee(name)
{
    public decimal HourlyRate { get; } = hourlyRate > 0 ? hourlyRate : throw new ArgumentOutOfRangeException(nameof(hourlyRate));

    public decimal HoursWorked { get; } = hoursWorked >= 0 ? hoursWorked : throw new ArgumentOutOfRangeException(nameof(hoursWorked));

    public override decimal CalculateSalary() => HourlyRate * HoursWorked;
}

public sealed class ContractEmployee(string name, decimal contractFee) : Employee(name)
{
    public decimal ContractFee { get; } = contractFee > 0 ? contractFee : throw new ArgumentOutOfRangeException(nameof(contractFee));

    public override decimal CalculateSalary() => ContractFee;
}