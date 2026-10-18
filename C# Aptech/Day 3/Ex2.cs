Employee employee = new("E001", "Nguyen Van A", "Software Developer", 15_000_000m);

employee.Display();

decimal increasePercent = 10m;
employee.IncreaseSalary(increasePercent);

Console.WriteLine($"""
Salary increased by {increasePercent:N2}%.
New salary: {employee.Salary:N0} VND
""");

employee.Display();

public class Employee(string id, string name, string position, decimal salary)
{
    public string Id { get; } = !string.IsNullOrWhiteSpace(id) ? id : throw new ArgumentException("ID cannot be empty.", nameof(id));
    public string Name { get; } = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentException("Name cannot be empty.", nameof(name));
    public string Position { get; } = !string.IsNullOrWhiteSpace(position) ? position : throw new ArgumentException("Position cannot be empty.", nameof(position));
    public decimal Salary { get; private set; } = salary >= 0 ? salary : throw new ArgumentOutOfRangeException(nameof(salary));

    public void IncreaseSalary(decimal percent)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(percent);
        Salary += Salary * percent / 100m;
    }

    public void Display() =>
        Console.WriteLine($"""
            Employee ID: {Id}
            Employee Name: {Name}
            Employee Position: {Position}
            Employee Salary: {Salary:N0} VND
            """);
}
