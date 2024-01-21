// See https://aka.ms/new-onsole-template for more information


using AgeAvarageExperiment.Model;
using System.Linq;

string option = string.Empty;
List<Employee> employeesList = new List<Employee>();

do
{
    Console.WriteLine(ShowHeader());

    Console.WriteLine(ShowMenu());

    option = ReadMenuSelection();

    ProcessSelectedOption(option, employeesList);

} while (option != "3");

static void ProcessSelectedOption(string? option, List<Employee> employeesList)
{
    switch (option)
    {
        case "1":
            AddEmployee(employeesList);
            break;
        case "2":
            PrintAverageAgeByGenre(employeesList);
            break;
        case "3":
            Console.WriteLine("Thanks for playing.");
            break;
        default:
            Console.WriteLine("Invalid Option!");
            break;
    }
}

static void PrintAverageAgeByGenre(List<Employee> employeesList)
{
    Dictionary<Enum, double> genreAndAverageAge = new Dictionary<Enum, double>();


    foreach (Genre genre in (Genre[])Enum.GetValues(typeof(Genre)))
    {
        //Linq
        //var employeeByGenre = from emp in employeesList
        //                      where emp.Genre == genre
        //                      select emp;


        //Lambda
        //var employeeByGenre = employeesList.Where(p => p.Genre.Equals(genre)).ToList();
        var employeeAgeByGenre = employeesList.Where(p => p.Genre.Equals(genre)).Select(o => CalculateAge(o.BirthDate)).ToList();

        if (employeeAgeByGenre.Count > 0)
        {
            //Console.WriteLine($"The age is: {CalculateAge(employeeByGenre[0].BirthDate)}");
            //int ageAverage = employeeAgeByGenre.Average();
            genreAndAverageAge.Add(genre, employeeAgeByGenre.Average());
        }
    }

    Console.Clear();

    foreach (var item in genreAndAverageAge)
    {
        Console.WriteLine($"Genre: {item.Key} | Avarage Age: {item.Value}");
    }

    Console.WriteLine();
    Console.WriteLine();
}

static void AddEmployee(List<Employee> employeesList)
{
    Console.WriteLine("Employee Detail");

    Employee employee = new Employee();

    //preeencher placa,cor,hora,entrada e proprietário.
    Console.Write("Enter the Employee name: ");

    try
    {
        employee.Name = Console.ReadLine();
    }
    catch (FormatException fe)
    {
        Console.WriteLine("An issue ocorred: {0}", fe.Message);
        ReadToContinue();
        return;
    }

    Console.Write("Enter the Employee Birh Date (\"dd/MM/yyyy\"): ");
    string consoleBirthDate = Console.ReadLine();
    DateOnly dateOnly;
    while (!DateOnly.TryParseExact(consoleBirthDate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dateOnly))
    {
        Console.WriteLine("Invalid date, please retry");
        consoleBirthDate = Console.ReadLine();
    }

    employee.BirthDate = dateOnly;

    Console.Write("Enter the Employee Genre (1 - Male; 2 - Female): ");

    switch (Console.ReadLine())
    {
        case "1":
            employee.Genre = Genre.Male;
            break;
        case "2":
            employee.Genre = Genre.Female;
            break;
        default:
            Console.WriteLine("Selected Option not recognized...");
            break;
    }

    employeesList.Add(employee);

    Console.WriteLine(employeesList.Count.ToString());

    if (employeesList.Count > 0)
    {
        Console.Clear();
    }

    for (int i = 0; i < employeesList.Count; i++)
    {
        Console.WriteLine();
        Console.WriteLine($"Employee {i + 1} Name: {employeesList[i].Name}");
        Console.WriteLine($"Employee {i + 1} Birth Date: {employeesList[i].BirthDate}");
        Console.WriteLine($"Employee {i + 1} Genre: {employeesList[i].Genre}");
        Console.WriteLine();
    }

    Console.WriteLine("Employee created successfullly!!");

    Console.WriteLine();
    Console.WriteLine();
}

static void ReadToContinue()
{
    Console.WriteLine("Press any key to continue.");
    Console.ReadKey();
}

static string? ReadMenuSelection()
{
    Console.Write("Selected Option: ");

    return Console.ReadLine();
}

static string ShowMenu()
{
    string menu = "Select:\n" +
                    "1 - Add a Employee\n" +
                    "2 - Print the average age\n" +
                    "3 - Exit \n";
    return menu;
}

static string ShowHeader()
{
    return "Implementing the one interview test better";
}

//Helper
static int CalculateAge(DateOnly dateOfBirth)
{
    int age = 0;
    age = DateTime.Now.Year - dateOfBirth.Year;
    if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
        age = age - 1;

    return age;
}
