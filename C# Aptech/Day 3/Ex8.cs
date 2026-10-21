var employees = new List<Employee>
{
    new("E001", "Nguyen Van A", "Engineering", 15_000_000),
    new("E002", "Tran Thi B", "HR", 12_000_000),
    new("E003", "Le Van C", "Finance", 14_000_000),
    new("E004", "Pham Thi D", "Engineering")
};

foreach (var employee in employees)
{
    employee.IncreaseSalary(10);
    employee.DisplayInfo();
}

Console.WriteLine($"Total employees: {Employee.GetTotalEmployees()}");

public sealed class Employee
{
    public string EmployeeId { get; }
    public string FullName { get; }
    public string Department { get; }
    public double Salary { get; private set; }

    private static int _totalEmployees;

    public static int TotalEmployees => _totalEmployees;

    public Employee(string employeeId, string fullName, string department, double salary)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(employeeId);
        ArgumentException.ThrowIfNullOrWhiteSpace(fullName);
        ArgumentException.ThrowIfNullOrWhiteSpace(department);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(salary);

        EmployeeId = employeeId;
        FullName = fullName;
        Department = department;
        Salary = salary;

        _totalEmployees++;
    }

    public Employee(string employeeId, string fullName, string department) : this(employeeId, fullName, department, 10_000_000)
    {
    }

    public void IncreaseSalary(double percent)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(percent);

        Salary *= 1 + percent / 100;
    }

    public void DisplayInfo() =>
        Console.WriteLine($"""
            Employee ID: {EmployeeId}
            Full name: {FullName}
            Department: {Department}
            Salary: {Salary:N0} VND
            """);

    public static int GetTotalEmployees() => _totalEmployees;
}
