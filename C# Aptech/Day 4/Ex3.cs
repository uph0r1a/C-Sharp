IFlyable flyingDuck = new Duck();
flyingDuck.Fly();

var duck = new Duck();
duck.Swim();

IFlyable airplane = new Airplane();
airplane.Fly();

public interface IFlyable
{
    void Fly();
}

public interface ISwimmable
{
    void Swim();
}

public sealed class Duck : IFlyable, ISwimmable
{
    void IFlyable.Fly() => Console.WriteLine("The duck flies.");

    public void Swim() => Console.WriteLine("The duck swims.");
}

public sealed class Airplane : IFlyable
{
    public void Fly() => Console.WriteLine("The airplane flies.");
}