var student1 = new StudentRecord("S001", "Alice Nguyen", new DateTime(2007, 5, 12), 85m, 78m);

Console.WriteLine($"{student1.FullName}: {student1.AverageScore:F2}\nClassification: {student1.GetClassification()}");

var student2 = student1 with
{
    MathScore = 95m,
    LiteratureScore = 92m
};

Console.WriteLine($"""
After updating scores:
{student2.FullName}: {student2.AverageScore:F2}
Classification: {student2.GetClassification()}
Original average: {student1.AverageScore:F2}
""");

public record StudentRecord
{
    public string StudentId { get; init; }
    public string FullName { get; init; }
    public DateTime DateOfBirth { get; init; }
    public decimal MathScore { get; init; }
    public decimal LiteratureScore { get; init; }

    public decimal AverageScore => (MathScore + LiteratureScore) / 2m;

    public StudentRecord(string studentId, string fullName, DateTime dateOfBirth, decimal mathScore, decimal literatureScore)
    {
        StudentId = !string.IsNullOrWhiteSpace(studentId) ? studentId : throw new ArgumentException("Student ID cannot be empty.", nameof(studentId));
        FullName = !string.IsNullOrWhiteSpace(fullName) ? fullName : throw new ArgumentException("Full name cannot be empty.", nameof(fullName));
        DateOfBirth = dateOfBirth != default && dateOfBirth <= DateTime.Today ? dateOfBirth : throw new ArgumentOutOfRangeException(nameof(dateOfBirth));
        MathScore = ValidateScore(mathScore, nameof(mathScore));
        LiteratureScore = ValidateScore(literatureScore, nameof(literatureScore));
    }

    public string GetClassification() => AverageScore switch
    {
        >= 90m => "Giỏi",
        >= 80m => "Khá",
        >= 65m => "Trung bình",
        _ => "Yếu"
    };

    private static decimal ValidateScore(decimal score, string parameterName) => score is >= 0m and <= 100m ? score : throw new ArgumentOutOfRangeException(parameterName, "Score must be between 0 and 100.");
}