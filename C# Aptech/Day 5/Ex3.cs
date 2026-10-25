Coordinate a = new(0, 0, 0);
Coordinate b = new(1, 1, 1);
Coordinate c = new(1, 1, 1);

Console.WriteLine($"""
    A == B: {a == b}
    B == C: {b == c}
    A == C: {a == c}
    """);

public record Coordinate(double X, double Y, double Z);