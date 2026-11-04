namespace StudentManagement;

public static class StudentManagement
{
    private static readonly List<Student> Students = [];

    public static void Main()
    {
        bool isExit = false;

        while (!isExit)
        {
            Console.Write("""
                
                ===== STUDENT MANAGEMENT =====
                1. Add student
                2. Display all students
                3. Search student by name
                4. Delete student by ID
                5. Student statistics
                0. Exit
                Enter your choice: 
                """);

            int choice = ReadInt("Invalid choice. Enter a number: ", 0, 5);

            switch (choice)
            {
                case 1:
                    AddStudent();
                    break;

                case 2:
                    DisplayStudents();
                    break;

                case 3:
                    SearchStudent();
                    break;

                case 4:
                    DeleteStudent();
                    break;

                case 5:
                    DisplayStatistics();
                    break;

                case 0:
                    isExit = true;
                    Console.WriteLine("Exiting Student Management System...");
                    break;
            }
        }
    }

    private static void AddStudent()
    {
        Console.Write("\nEnter student ID: ");
        string id = ReadRequiredString();

        if (Students.Any(s => s.Id.Equals(id, StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine("A student with this ID already exists.");
            return;
        }

        Console.Write("Enter student full name: ");
        string fullName = ReadRequiredString();
        int age = ReadInt("Enter student age: ", 1, 150);
        double score = ReadDouble("Enter student score: ", 0, 100);

        Students.Add(new Student(id, fullName, age, score));
        Console.WriteLine("Student added successfully!");
    }

    private static void DisplayStudents()
    {
        Console.WriteLine("\n===== STUDENT LIST =====");

        if (Students.Count == 0)
        {
            Console.WriteLine("No students found.");
            return;
        }

        Students.ForEach(Console.WriteLine);
    }

    private static void SearchStudent()
    {
        Console.Write("\nEnter student full name: ");
        string fullName = ReadRequiredString();

        var matchingStudents = Students.FindAll(s => s.FullName.Contains(fullName, StringComparison.OrdinalIgnoreCase));

        if (matchingStudents.Count == 0)
        {
            Console.WriteLine("No matching students found.");
            return;
        }

        Console.WriteLine("\n===== SEARCH RESULTS =====");
        matchingStudents.ForEach(Console.WriteLine);
    }

    private static void DeleteStudent()
    {
        Console.Write("\nEnter student ID: ");
        string id = ReadRequiredString();

        int deletedCount = Students.RemoveAll(s => s.Id.Equals(id, StringComparison.OrdinalIgnoreCase));

        Console.WriteLine(deletedCount > 0 ? "Student deleted successfully!" : "Student not found.");
    }

    private static void DisplayStatistics()
    {
        Console.WriteLine($"\n===== STUDENT STATISTICS =====\nTotal students: {Students.Count}");

        if (Students.Count == 0)
        {
            Console.WriteLine("No student statistics available.");
            return;
        }

        var highestScoringStudent = Students.MaxBy(s => s.Score)!;
        Console.WriteLine($"\nStudent with the highest score:\n{highestScoringStudent}");
    }

    private static string ReadRequiredString()
    {
        while (true)
        {
            string? input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
            {
                return input.Trim();
            }

            Console.Write("Input cannot be empty. Try again: ");
        }
    }

    private static int ReadInt(string message, int min, int max)
    {
        while (true)
        {
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int value) && value >= min && value <= max)
            {
                return value;
            }

            Console.Write(message);
        }
    }

    private static double ReadDouble(string message, double min, double max)
    {
        while (true)
        {
            string? input = Console.ReadLine();

            if (double.TryParse(input, out double value) && double.IsFinite(value) && value >= min && value <= max)
            {
                return value;
            }

            Console.Write(message);
        }
    }
}

public sealed class Student(string id, string fullName, int age, double score)
{
    public string Id { get; } = !string.IsNullOrWhiteSpace(id) ? id.Trim() : throw new ArgumentException("Student ID cannot be empty.", nameof(id));
    public string FullName { get; set; } = !string.IsNullOrWhiteSpace(fullName) ? fullName.Trim() : throw new ArgumentException("Full name cannot be empty.", nameof(fullName));
    public int Age { get; set; } = age > 0 && age <= 100 ? age : throw new ArgumentOutOfRangeException(nameof(age));
    public double Score { get; set; } = double.IsFinite(score) && score >= 0 && score <= 100 ? score : throw new ArgumentOutOfRangeException(nameof(score));

    public override string ToString() => $"""
        Student ID: {Id}
        Student full name: {FullName}
        Student age: {Age}
        Student score: {Score:N2}
        """;
}