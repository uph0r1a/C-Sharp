using System;

Console.Write("Enter employee name: ");
string name = Console.ReadLine() ?? "";

Console.Write("Enter employee company name: ");
string company = Console.ReadLine() ?? "";

Console.Write("Enter employee birthyear: ");
int birthyear;

while (!(int.TryParse(Console.ReadLine(), out birthyear) && birthyear >= 1900 && birthyear <= DateTime.Now.Year))
{
    Console.Write("Invalid birthyear\nRe-enter employee birthyear: ");
}

Console.Write("Enter employee address: ");
string address = Console.ReadLine() ?? "";

Console.Write("Enter number of months worked: ");
int month;

while (!(int.TryParse(Console.ReadLine(), out month) && month >= 0))
{
    Console.Write("Invalid number of months\nRe-enter number of months worked: ");
}

Console.Write("Enter employee salary: ");
double salary;

while (!(double.TryParse(Console.ReadLine(), out salary) && salary >= 0))
{
    Console.Write("Invalid employee salary\nRe-enter employee salary: ");
}

Console.Write("Enter salary level: ");
int level;

while (!(int.TryParse(Console.ReadLine(), out level) && level >= 0))
{
    Console.Write("Invalid salary level\nRe-enter salary level: ");
}

double s = salary * level;
double bonus = s * month / 12.0;
int age = DateTime.Now.Year - birthyear;

Console.WriteLine($"""
===== Employee Information =====
Employee name: {name}
Employee company: {company}
Employee address: {address}
Employee age: {age}
Employee salary: {s:F2}
Employee bonus: {bonus:F2}
""");