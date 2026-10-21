var student = new Student("SV001", "Tri", 20);

student.Display();
student.UpdateAge(21);
student.Display();

public sealed class Student(string id, string name, int age)
{
    public string Id { get; } = !string.IsNullOrWhiteSpace(id) ? id : throw new ArgumentException("ID cannot be empty.", nameof(id));

    public string Name { get; } = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentException("Name cannot be empty.", nameof(name));

    public int Age { get; private set; } = age >= 0 ? age : throw new ArgumentOutOfRangeException(nameof(age), age, "Age cannot be negative.");

    public void UpdateAge(int newAge)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(newAge);
        Age = newAge;
    }

    public void Display() =>
        Console.WriteLine($"""
            Student ID: {Id}
            Student name: {Name}
            Student age: {Age}
            """);
}
