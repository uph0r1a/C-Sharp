static double AveragePrice(List<DELL> laptop)
{
    double sum = 0;
    laptop.ForEach(l => sum += l.Price);
    return sum;
}

static void MinDell(List<DELL> laptop)
{
    DELL minLaptop;
    laptop.ForEach(l => minLaptop = l.Price < minLaptop.Price ? l : minLaptop);
    minLaptop.OutputLaptop();
}


while (true)
{
    
}

public class Laptop(string name, string brand, string color, double price)
{
    public string Name { set; get; } = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentException();
    public string Brand { set; get; } = !string.IsNullOrWhiteSpace(brand) ? brand : throw new ArgumentException();
    public string Color { set; get; } = !string.IsNullOrWhiteSpace(color) ? color : throw new ArgumentException();
    public double Price { set; get; } = price > 0 ? price : ArgumentOutOfRangeException();

    public void InputLaptop()
    {
        Console.Write("Enter laptop name: ");
        Name = Console.ReadLine();
        Console.Write("Enter laptop brand: ");
        Brand = Console.ReadLine();
        Console.Write("Enter laptop color: ");
        Color = Console.ReadLine();
        Console.Write("Enter laptop price: ");
        Price = Convert.ToDouble(Console.ReadLine());
    }

    public void OutputLaptop()
    {
        Console.WriteLine($"""
        Laptop name: {Name}
        Laptop brand: {Brand}
        Laptop color: {Color}
        Laptop price: {Price}
        """);
    }

    public string Status()
    {
        return Price > 2000 ? "New" : Price >1000 ? "90% new" : "Old";
    }
}

public class DELL(string name, string brand, string color, double price, string serial) : Laptop(name, brand, color, price)
{
    public string Serial {set;get;} = !string.IsNullOrWhiteSpace(serial) ? serial : throw new ArgumentException();

    public void InputDell()
    {
        InputLaptop();
        Console.Write("Enter laptop serial: ");
        Serial = Console.ReadLine();
    }

    public void OutputDell()
    {
        OutputLaptop();
        Console.WriteLine($"Laptop serial: {Serial}");
    }
}