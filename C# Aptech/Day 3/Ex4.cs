var book1 = new Book("B001", "Clean Code", "Robert C. Martin", 250_000m);
var book2 = new Book("B002", "The Pragmatic Programmer", "David Thomas", 300_000m);
var book3 = new Book();

book1.DisplayInfo();
book2.DisplayInfo();
book3.DisplayInfo();

public sealed class Book(string bookId, string title, string author, decimal price)
{
    public Book() : this("B000", "Untitled", "Unknown", 1m)
    {
    }

    public string BookId { get; } = !string.IsNullOrWhiteSpace(bookId) ? bookId : throw new ArgumentException("Book ID cannot be empty.", nameof(bookId));

    public string Title { get; } = !string.IsNullOrWhiteSpace(title) ? title : throw new ArgumentException("Book title cannot be empty.", nameof(title));

    public string Author { get; } = !string.IsNullOrWhiteSpace(author) ? author : throw new ArgumentException("Book author cannot be empty.", nameof(author));

    public decimal Price { get; } = price > 0 ? price : throw new ArgumentOutOfRangeException(nameof(price), price, "Price must be greater than zero.");

    public void DisplayInfo() =>
        Console.WriteLine($"""
            Book ID: {BookId}
            Book title: {Title}
            Book author: {Author}
            Book price: {Price:N0}
            """);
}
