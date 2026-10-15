int two = 0, one = 0, fifty = 0, twenty = 0, five = 0, ten = 0;

while (true)
{
    Console.Write("Enter coin (1, 2, 5, 10, 20, 50; 0 to finish): ");
    int value;
    while (true)
    {
        if (int.TryParse(Console.ReadLine(), out value) && (value == 1 || value == 2 || value == 5 || value == 10 || value == 20 || value == 50 || value == 0))
        {
            break;
        }

        Console.Write("Invalid value\nRe-enter value: ");
    }

    if (value == 0)
    {
        break;
    }

    if (value == 1)
        one++;
    else if (value == 2)
        two++;
    else if (value == 5)
        five++;
    else if (value == 10)
        ten++;
    else if (value == 20)
        twenty++;
    else if (value == 50)
        fifty++;
}

Console.WriteLine($"""
$1: {one}
$2: {two}
5c: {five}
10c: {ten}
20c: {twenty}
50c: {fifty}
""");