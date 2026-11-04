namespace GenericRepositoryDemo;

public static class Program
{
    public static void Main()
    {
        IRepository<Student> studentRepo = new StudentRepository();

        studentRepo.Add(new Student { ID = 1, Name = "Adam Smith", ClassName = "12A" });
        studentRepo.Add(new Student { ID = 2, Name = "James John", ClassName = "12B" });

        Console.WriteLine("Student List:");

        foreach (var student in studentRepo.GetAll())
        {
            Console.WriteLine(student);
        }

        IRepository<Product> productRepo = new ProductRepository();

        productRepo.Add(new Product { ID = 101, Name = "Dell", Price = 25_000_000m });
        productRepo.Add(new Product { ID = 102, Name = "Sony", Price = 15_000_000m });

        Console.WriteLine("\nProduct List:");

        foreach (var product in productRepo.GetAll())
        {
            Console.WriteLine(product);
        }

        Console.WriteLine($"""

            Does student ID=1 exist: {studentRepo.Exists(1)}
            Does product ID=999 exist: {productRepo.Exists(999)}
            """);
    }
}

public interface IRepository<T> where T : class
{
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    T? GetByID(int id);
    IEnumerable<T> GetAll();
    bool Exists(int id);
}

public sealed class Student
{
    public int ID { get; init; }
    public string Name { get; set; } = string.Empty;
    public string ClassName { get; set; } = string.Empty;

    public override string ToString() => $"Student [ID: {ID}, Name: {Name}, Class: {ClassName}]";
}

public sealed class Product
{
    public int ID { get; init; }

    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public override string ToString() => $"Product [ID: {ID}, Name: {Name}, Price: {Price:N0} VND]";
}

public sealed class StudentRepository : IRepository<Student>
{
    private readonly List<Student> _students = [];

    public void Add(Student entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        _students.Add(entity);
    }

    public void Update(Student entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        var existing = GetByID(entity.ID);

        if (existing is null)
        {
            return;
        }

        existing.Name = entity.Name;
        existing.ClassName = entity.ClassName;
    }

    public void Delete(Student entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        _students.Remove(entity);
    }

    public Student? GetByID(int id) => _students.FirstOrDefault(student => student.ID == id);

    public IEnumerable<Student> GetAll() => _students;

    public bool Exists(int id) => _students.Any(student => student.ID == id);
}

public sealed class ProductRepository : IRepository<Product>
{
    private readonly List<Product> _products = [];

    public void Add(Product entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        _products.Add(entity);
    }

    public void Update(Product entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        var existing = GetByID(entity.ID);

        if (existing is null)
        {
            return;
        }

        existing.Name = entity.Name;
        existing.Price = entity.Price;
    }

    public void Delete(Product entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        _products.Remove(entity);
    }

    public Product? GetByID(int id) => _products.FirstOrDefault(product => product.ID == id);

    public IEnumerable<Product> GetAll() => _products;

    public bool Exists(int id) => _products.Any(product => product.ID == id);
}