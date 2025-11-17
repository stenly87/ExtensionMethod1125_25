// методы расширения
// методы расширения не позволяют получить доступ к приватным членам класса
// методы расширения вызываются только на экземплярах
// методы расширения позволяют вынести отдельно логику, которая может замусорить основной код
// методы расширения должны быть созданы в статичном классе, в одном классе могут быть методы для разных классов, но лучше один класс - один метод

using System.Runtime.CompilerServices;
using System.Text.Json;

public class Program
{
    private static void Main(string[] args)
    {
        IamNotYourFather notYourFather =
            new("Лунтик", new DateTime(2999, 1, 1));
        // использование метода расширения позволяет вынести в отдельный класс
        // какое-то поведение, которое относится к расширяемому классу
        int age = notYourFather.GetAge();
        string json = notYourFather.GetJson();
        string name = notYourFather.GetName();
        Console.WriteLine(age);
        Console.WriteLine(json);
        Console.WriteLine(name);

        // вызов метода расширения как обычный статичный метод
        age = IamNotYourFatherExtension.GetAge(notYourFather);
        //new Program().Test();
        // string json = new Program().GetJson();

        // куча полезным методов в коллекциях являются методами расширения
        // объявлены в классе Enumerable

        Point point1 = new Point { X = 1, Y = 1 };
        Point point2 = new Point { X = 2, Y = 2 };
        // дурость, но теперь так можно
        Line line = point1 & point2;
        // дурость, но теперь так можно
        Line test = (Line)point1;
        // неявное преобразование
        Line test1 = point1;

        // если оператор + будет принимать и возвращать Point
        point1 += point2;
    }
}

// пример класса, который нельзя наследовать
// sealed - запечатанный класс
public sealed class IamNotYourFather
{
    public string Name { get; set; }
    public DateTime BirthDay { get; set; }

    public IamNotYourFather(string name, DateTime birthDay)
    {
        Name = name;
        BirthDay = birthDay;
    }
}

public class Line
{ 
    public Point Point1 { get; set; }
    public Point Point2 { get; set; }
}
public class Point
{
    public double X { get; set; }
    public double Y { get; set; }

    // внутри класса можно переопределить действие для стандартных операторов
    // тем самым определив для них новые задачи
    //public static Line operator +(Point start, Point end)
    //{
    //    return new Line { Point1 = start, Point2 = end };
    //}

    public static Point operator +(Point start, Point end)
    {
        return new Point { X = start.X + end.X, Y = start.Y + end.Y  };
    }

    public static Line operator &(Point start, Point end)
    {
        return new Line { Point1 = start, Point2 = end };
    }

    // преобразование типов (explicit - явное преобразование)
    //public static explicit operator Line(Point point)
    //{
    //    return new Line { Point1 = point, Point2 = point };
    //}
    // преобразование типов (implicit - неявное преобразование)
    public static implicit operator Line(Point point)
    {
        return new Line { Point1 = point, Point2 = point };
    }
}

// для создания метода расширения требуется статичный класс (имя не важно, убирать по степени удобства)
public static class IamNotYourFatherExtension
{
    // метод расширения - обязательно static
    // первый аргумент - обязательно this и тип класса, который нужно расширить
    public static int GetAge(this IamNotYourFather father)
    {
        // абсолютно неверный метод расчета кол-ва лет
        return (int)((DateTime.Now - father.BirthDay).TotalDays / 365);
    }

    public static void Test(this Program program)
    {
        // абсолютно бесполезный метод
        Console.WriteLine(program.GetType().Name);
    }
}

public static class JsonExtension
{
    // метод расширения для всего
    public static string GetJson(this object obj)
    {
        return JsonSerializer.Serialize(obj);
    }
}

public static class AnotherTestExtension
{
    // метод расширения для всего через обобщения
    public static string GetName<T>(this T obj) where T : class
    {
        return obj.ToString();
    }
}






/* унаследоваться нельзя
public class Luke : IamNotYourFather
{ 

}
*/
