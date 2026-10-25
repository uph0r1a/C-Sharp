var employees = new List<EmployeeRecord>
{
    new("E001", "Alice Nguyen", "IT", 2_000m, new DateTime(2018, 3, 15)),
    new("E002", "Bob Tran", "HR", 1_500m, new DateTime(2021, 7, 1)),
    new("E003", "Charlie Le", "Finance", 1_800m, new DateTime(2015, 9, 20))
};

var promotedEmployee = employees[0] with
{
    Department = "Management",
    BaseSalary = employees[0].BaseSalary * 1.20m
};

employees[0] = promotedEmployee;
var totalPayroll = employees.Sum(static employee => employee.BaseSalary + employee.CalculateBonus);

Console.WriteLine("Employee list:");

foreach (var employee in employees)
{
    Console.WriteLine($"{employee.EmployeeId} | {employee.FullName} | {employee.Department} | Salary: {employee.BaseSalary:C} | Seniority: {employee.CalculateSeniority()} years | Bonus: {employee.CalculateBonus:C}");
}

Console.WriteLine($"\nTotal payroll: {totalPayroll:C}");

var mostSeniorEmployee = employees.MaxBy(static employee => employee.CalculateSeniority());

Console.WriteLine($"Most senior: {mostSeniorEmployee!.FullName} ({mostSeniorEmployee.CalculateSeniority()} years)");

public record EmployeeRecord
{
    public string EmployeeId { get; init; }
    public string FullName { get; init; }
    public string Department { get; init; }
    public decimal BaseSalary { get; init; }
    public DateTime JoinDate { get; init; }

    public EmployeeRecord(string employeeId, string fullName, string department, decimal baseSalary, DateTime joinDate)
    {
        EmployeeId = !string.IsNullOrWhiteSpace(employeeId) ? employeeId : throw new ArgumentException("Employee ID cannot be empty.", nameof(employeeId));
        FullName = !string.IsNullOrWhiteSpace(fullName) ? fullName : throw new ArgumentException("Full name cannot be empty.", nameof(fullName));
        Department = !string.IsNullOrWhiteSpace(department) ? department : throw new ArgumentException("Department cannot be empty.", nameof(department));
        BaseSalary = baseSalary > 0 ? baseSalary : throw new ArgumentOutOfRangeException(nameof(baseSalary));
        JoinDate = joinDate != default && joinDate <= DateTime.Today ? joinDate : throw new ArgumentOutOfRangeException(nameof(joinDate));
    }

    public int CalculateSeniority()
    {
        var today = DateTime.Today;
        var years = today.Year - JoinDate.Year;

        if (JoinDate.Date > today.AddYears(-years))
        {
            years--;
        }

        return years;
    }

    public decimal CalculateBonus => BaseSalary * Math.Min(CalculateSeniority() * 0.05m, 0.25m);
}