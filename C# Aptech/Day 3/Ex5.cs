Student s1 = new();
Student s2 = new("SV001", "Nguyen Van A");
Student s3 = new("SV002", "Tran Thi B", 20, "Computer Science");

s1.DisplayInfo();
s2.DisplayInfo();
s3.DisplayInfo();

public class Student(string studentID, string fullName, int age, string major)
{
    public string StudentID { get; } = !string.IsNullOrWhiteSpace(studentID) ? studentID : throw new ArgumentException("Student ID cannot be empty.", nameof(studentID));
    public string FullName { get; } = !string.IsNullOrWhiteSpace(fullName) ? fullName : throw new ArgumentException("Student full name cannot be empty.", nameof(fullName));
    public int Age { get; } = age > 0 ? age : throw new ArgumentOutOfRangeException(nameof(age));
    public string Major { get; } = !string.IsNullOrWhiteSpace(major) ? major : throw new ArgumentException("Student major cannot be empty.", nameof(major));

    public Student() : this("S000", "Unknown", 18, "Undeclared")
    {
    }

    public Student(string studentID, string fullName) : this(studentID, fullName, 18, "Undeclared")
    {
    }

    public void DisplayInfo() =>
        Console.WriteLine($"""
            Student ID: {StudentID}
            Full Name: {FullName}
            Age: {Age}
            Major: {Major}
            """);
}
