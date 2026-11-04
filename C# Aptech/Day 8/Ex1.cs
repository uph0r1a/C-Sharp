namespace Generic_Iterator.Generic_Class;

public class Box<T>
{
    private T? _value;

    public void SetValue(T value) => _value = value;

    public T? GetValue() => _value;

    public override string ToString() => _value is null ? $"Box<{typeof(T).Name}> is empty." : $"Box contains: {_value} (Type: {_value.GetType().Name})";
}

public sealed class StudentBox<T> where T : Student
{
    private T? _value;

    public void SetValue(T value) => _value = value;

    public T? GetValue() => _value;

    public void DisplayInfo()
    {
        if (_value is null)
        {
            Console.WriteLine("Student box is empty.");
            return;
        }

        Console.WriteLine($"Student [ID: {_value.ID}, Name: {_value.Name}]");
    }

    public override string ToString() => _value is null ? "Student box is empty." : $"Box contains: ID = {_value.ID} | " + $"Name = {_value.Name} | Type = {_value.GetType().Name}";
}

public sealed class Student
{
    public int ID { get; init; }

    public string Name { get; init; } = string.Empty;

    public override string ToString() => $"Student [ID: {ID}, Name: {Name}]";
}

public static class GenericRun
{
    public static void Run()
    {
        var intBox = new Box<int>();
        intBox.SetValue(100);
        Console.WriteLine(intBox);

        var stringBox = new Box<string>();
        stringBox.SetValue("Hi");
        Console.WriteLine(stringBox);

        var studentBox = new StudentBox<Student>();
        studentBox.SetValue(new Student { ID = 1, Name = "Adam Smith" });

        Console.WriteLine(studentBox);
        studentBox.DisplayInfo();
    }
}

public static class Program
{
    public static void Main() => GenericRun.Run();
}