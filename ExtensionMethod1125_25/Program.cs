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
    public static string GetName<T>(this T obj)
    {
        return obj.ToString();
    }
}


/* унаследоваться нельзя
public class Luke : IamNotYourFather
{ 

}
*/
