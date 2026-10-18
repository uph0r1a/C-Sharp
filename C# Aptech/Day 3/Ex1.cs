Student s1 = new("SV001", "Tri", 20);

s1.Display();
s1.UpdateAge(21);
s1.Display();

public class Student(string id, string name, int age)
{
    public string Id { get; } = !string.IsNullOrWhiteSpace(id) ? id : throw new ArgumentException("ID cannot be empty.", nameof(id));
    public string Name { get; } = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentException("Name cannot be empty.", nameof(name));
    public int Age { get; private set; } = age >= 0 ? age : throw new ArgumentOutOfRangeException(nameof(age));

    public void UpdateAge(int newAge)
    {
        if (newAge < 0)
            throw new ArgumentOutOfRangeException(nameof(newAge));

        Age = newAge;
    }

    public void Display() =>
        Console.WriteLine($"""
            Student ID: {Id}
            Student name: {Name}
            Student age: {Age}
            """);
}
