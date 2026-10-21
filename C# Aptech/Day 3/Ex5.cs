var s1 = new Student();
var s2 = new Student("SV001", "Nguyen Van A");
var s3 = new Student("SV002", "Tran Thi B", 20, "Computer Science");

s1.DisplayInfo();
s2.DisplayInfo();
s3.DisplayInfo();

public sealed class Student(string studentId, string fullName, int age, string major)
{
    public Student() : this("S000", "Unknown", 18, "Undeclared")
    {
    }

    public Student(string studentId, string fullName) : this(studentId, fullName, 18, "Undeclared")
    {
    }

    public string StudentId { get; } = !string.IsNullOrWhiteSpace(studentId) ? studentId : throw new ArgumentException("Student ID cannot be empty.", nameof(studentId));

    public string FullName { get; } = !string.IsNullOrWhiteSpace(fullName) ? fullName : throw new ArgumentException("Student full name cannot be empty.", nameof(fullName));

    public int Age { get; } = age > 0 ? age : throw new ArgumentOutOfRangeException(nameof(age), age, "Age must be greater than zero.");

    public string Major { get; } = !string.IsNullOrWhiteSpace(major) ? major : throw new ArgumentException("Student major cannot be empty.", nameof(major));

    public void DisplayInfo() =>
        Console.WriteLine($"""
            Student ID: {StudentId}
            Full Name: {FullName}
            Age: {Age}
            Major: {Major}
            """);
}