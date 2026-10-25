var product1 = new Product("P001", "Keyboard", 49.99m, 25);
var product2 = new Product("P002", "Mouse", 24.99m, 40);
var product3 = new Product("P003", "Monitor", 299.99m, 10);
var product4 = product3 with { Price = product3.Price * 0.9m };
Product[] products = [product1, product2, product3, product4];

foreach (var product in products)
{
    Console.WriteLine($"{product.ProductId} | {product.Name} | {product.Price:C} | Stock: {product.Stock}");
}

public record Product
{
    public string ProductId { get; init; }
    public string Name { get; init; }
    public decimal Price { get; init; }
    public int Stock { get; init; }

    public Product(string productId, string name, decimal price, int stock)
    {
        ProductId = !string.IsNullOrWhiteSpace(productId) ? productId : throw new ArgumentException("Product ID cannot be empty.", nameof(productId));
        Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentException("Product name cannot be empty.", nameof(name));
        Price = price >= 0 ? price : throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative.");
        Stock = stock >= 0 ? stock : throw new ArgumentOutOfRangeException(nameof(stock), "Stock cannot be negative.");
    }
}