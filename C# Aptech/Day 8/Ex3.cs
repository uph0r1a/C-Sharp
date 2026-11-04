public sealed class Iterator
{
    private readonly List<int> _numbers = [10, 20, 30, 40, 50, 60, 70, 80, 90, 100];

    public IEnumerable<int> GetNumbers()
    {
        foreach (var num in _numbers)
        {
            Console.WriteLine($"Value: {num}");
            yield return num;

            if (num == 50)
            {
                yield break;
            }
        }
    }
}

public static class Program
{
    public static void Main()
    {
        var iterator = new Iterator();

        foreach (var number in iterator.GetNumbers())
        {
            Console.WriteLine($"Received: {number}");
        }
    }
}