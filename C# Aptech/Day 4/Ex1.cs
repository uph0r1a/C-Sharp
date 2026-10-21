List<Shape> shapes = [new Circle("Red", 5), new Rectangle("Green", 4, 6), new Circle("Yellow", 3)];
double totalArea = 0;

foreach (var shape in shapes)
{
    shape.DisplayInfo();

    var area = shape.CalculateArea();
    var perimeter = shape.CalculatePerimeter();

    Console.WriteLine($"Area: {area:F2}");
    Console.WriteLine($"Perimeter: {perimeter:F2}");
    Console.WriteLine();

    totalArea += area;
}

Console.WriteLine($"Total area: {totalArea:F2}");

public abstract class Shape(string color)
{
    public string Color { get; } = !string.IsNullOrWhiteSpace(color) ? color : throw new ArgumentException("Color cannot be empty.", nameof(color));

    public abstract double CalculateArea();

    public abstract double CalculatePerimeter();

    public void DisplayInfo() => Console.WriteLine($"Shape {GetType().Name} - Color: {Color}");
}

public sealed class Circle(string color, double radius) : Shape(color)
{
    public double Radius { get; } = double.IsFinite(radius) && radius > 0 ? radius : throw new ArgumentOutOfRangeException(nameof(radius), radius, "Radius must be positive and finite.");

    public override double CalculateArea() => Math.PI * Radius * Radius;

    public override double CalculatePerimeter() => 2 * Math.PI * Radius;
}

public sealed class Rectangle(string color, double width, double height) : Shape(color)
{
    public double Width { get; } = double.IsFinite(width) && width > 0 ? width : throw new ArgumentOutOfRangeException(nameof(width), width, "Width must be positive and finite.");

    public double Height { get; } = double.IsFinite(height) && height > 0 ? height : throw new ArgumentOutOfRangeException(nameof(height), height, "Height must be positive and finite.");

    public override double CalculateArea() => Width * Height;

    public override double CalculatePerimeter() => 2 * (Width + Height);
}
