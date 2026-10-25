Point3D a = new(0, 0, 0);
Point3D b = new(1, 1, 1);
Point3D c = new(1, 1, 1);
Point3D d = new(2, 3, 4);

Console.WriteLine($"""
Distance A → B: {a.DistanceTo(b):F3}
A == B: {a == b}
B == C: {b == c}
B == D: {b == d}
""");

public record struct Point3D(double X, double Y, double Z)
{
    public readonly double DistanceTo(Point3D other)
    {
        var dx = X - other.X;
        var dy = Y - other.Y;
        var dz = Z - other.Z;

        return Math.Sqrt(dx * dx + dy * dy + dz * dz);
    }
}