using System.Globalization;

var a = ReadPositiveDouble("Nhap canh day lon a: ");
var b = ReadPositiveDouble("Nhap canh day nho b: ");
var c = ReadPositiveDouble("Nhap canh ben c: ");
var d = ReadPositiveDouble("Nhap canh ben d: ");
var h = ReadPositiveDouble("Nhap chieu cao h: ");

var area = (a + b) * h / 2;
var perimeter = a + b + c + d;

Console.WriteLine($"""
    ===== Hinh thang =====
    Canh day lon: {a:N2}
    Canh day nho: {b:N2}
    Canh ben c: {c:N2}
    Canh ben d: {d:N2}
    Chieu cao: {h:N2}
    Dien tich: {area:N2}
    Chu vi: {perimeter:N2}
    """);

static double ReadPositiveDouble(string prompt)
{
    while (true)
    {
        Console.Write(prompt);

        if (double.TryParse(Console.ReadLine(), NumberStyles.Float, CultureInfo.InvariantCulture, out var value) && double.IsFinite(value) && value > 0)
        {
            return value;
        }

        Console.WriteLine("Gia tri khong hop le. Vui long nhap so huu han lon hon 0.");
    }
}
