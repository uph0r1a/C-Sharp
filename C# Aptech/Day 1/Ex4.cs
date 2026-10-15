Console.Write("Nhap canh a: ");
double a = Convert.ToDouble(Console.ReadLine());

Console.Write("Nhap canh b: ");
double b = Convert.ToDouble(Console.ReadLine());

Console.Write("Nhap canh c: ");
double c = Convert.ToDouble(Console.ReadLine());

Console.Write("Nhap canh d: ");
double d = Convert.ToDouble(Console.ReadLine());

Console.Write("Nhap h: ");
double h = Convert.ToDouble(Console.ReadLine());

double S = ((a + b) * h) / 2, cv = a + b + c + d;
Console.WriteLine($"Dien tich hinh thang: {S}\nChu vi hinh thang: {cv}");
