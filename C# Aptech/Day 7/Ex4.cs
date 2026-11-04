public delegate void AchievementDelegate(string studentName, string achievement);

public static class Notice
{
    public static void Main()
    {
        AchievementDelegate achievement = SendToParent;
        achievement += SendToTeacher;
        achievement += WriteToSchoolBoard;

        achievement("Adam Smith", "First Prize");
    }

    private static void SendToParent(
        string studentName,
        string achievement)
    {
        Console.WriteLine($"Send {studentName}'s {achievement} to parent.");
    }

    private static void SendToTeacher(string studentName, string achievement)
    {
        Console.WriteLine($"Send {studentName}'s {achievement} to teacher.");
    }

    private static void WriteToSchoolBoard(string studentName, string achievement)
    {
        Console.WriteLine($"Send {studentName}'s {achievement} to school board.");
    }
}