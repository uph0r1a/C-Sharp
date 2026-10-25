List<DELL> laptops = [];

while (true)
{
    Console.WriteLine("""
        ----------- Menu ----------------
        1. Nhập thông tin
        2. Xuất thông tin
        3. In giá trung bình của sản phẩm
        4. In laptop có giá thấp nhất
        5. Thoát
        ---------------------------------
        """);

    var choice = ReadInt("Enter your choice (1-5): ", 1, 5);

    switch (choice)
    {
        case 1:
            {
                var count = ReadInt("How many Dell laptops do you want to enter? (minimum 3): ", 3, 100);

                for (var i = 0; i < count; i++)
                {
                    Console.WriteLine($"\nEnter information for Dell laptop {i + 1}:");

                    var laptop = new DELL();
                    laptop.NhapTTDell();
                    laptops.Add(laptop);
                }
                break;
            }

        case 2:
            if (laptops.Count == 0)
            {
                Console.WriteLine("No laptops available.");
                break;
            }

            foreach (var laptop in laptops)
            {
                laptop.XuatTTDell();
                Console.WriteLine();
            }

            break;

        case 3:
            if (laptops.Count == 0)
            {
                Console.WriteLine("No laptops available.");
                break;
            }

            Console.WriteLine($"Average price: {GiaTB(laptops):N2}");
            break;

        case 4:
            MinDell(laptops);
            break;

        case 5:
            return;
    }
}

static int ReadInt(string prompt, int min, int max)
{
    while (true)
    {
        Console.Write(prompt);

        if (int.TryParse(Console.ReadLine(), out var value)
            && value >= min
            && value <= max)
        {
            return value;
        }

        Console.WriteLine($"Enter an integer from {min} to {max}.");
    }
}

static double GiaTB(List<DELL> laptops) => laptops.Average(static laptop => laptop.Price);

static void MinDell(List<DELL> laptops)
{
    if (laptops.Count == 0)
    {
        Console.WriteLine("No laptops available.");
        return;
    }

    var minPrice = laptops.Min(static laptop => laptop.Price);

    Console.WriteLine($"Lowest price: {minPrice:N2}");

    foreach (var laptop in laptops.Where(laptop => laptop.Price == minPrice))
    {
        laptop.XuatTTDell();
        Console.WriteLine();
    }
}

public class Laptop
{
    public string Name { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public double Price { get; set; }

    public void NhapLaptop()
    {
        Name = ReadRequiredString("Enter laptop name: ");
        Brand = ReadRequiredString("Enter laptop brand: ");
        Color = ReadRequiredString("Enter laptop color: ");
        Price = ReadPrice();
    }

    public void XuatThongtin_Laptop()
    {
        Console.WriteLine($"""
            Laptop name: {Name}
            Brand: {Brand}
            Color: {Color}
            Price: {Price:N2}
            Status: {TrangThai()}
            """);
    }

    public string TrangThai() => Price switch
    {
        < 1000 => "Hàng cũ",
        < 2000 => "Hàng còn mới 90%",
        _ => "Hàng mới nguyên seal"
    };

    protected static string ReadRequiredString(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
            {
                return input.Trim();
            }

            Console.WriteLine("Input cannot be empty.");
        }
    }

    private static double ReadPrice()
    {
        while (true)
        {
            Console.Write("Enter laptop price: ");

            if (double.TryParse(Console.ReadLine(), out var price)
                && double.IsFinite(price)
                && price > 0)
            {
                return price;
            }

            Console.WriteLine("Price must be a positive, finite number.");
        }
    }
}

public sealed class DELL : Laptop
{
    public string Seri { get; set; } = string.Empty;

    public void NhapTTDell()
    {
        NhapLaptop();
        Seri = ReadRequiredString("Enter Dell serial number: ");
    }

    public void XuatTTDell()
    {
        XuatThongtin_Laptop();
        Console.WriteLine($"Serial number: {Seri}");
    }

    public double GiaTB(List<DELL> laptops) =>
        laptops.Average(static laptop => laptop.Price);

    public void MinDell(List<DELL> laptops)
    {
        if (laptops.Count == 0)
        {
            Console.WriteLine("No laptops available.");
            return;
        }

        var minPrice = laptops.Min(static laptop => laptop.Price);

        foreach (var laptop in laptops.Where(laptop => laptop.Price == minPrice))
        {
            laptop.XuatTTDell();
        }
    }
}
