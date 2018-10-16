/*
 1. До класу Employee додати методи вводу імені та прізвища, метод вводу окладу. 
   Методи повинні викидати виключення
 	- при введені у Імя чи Прізвище недопустимих символів(цифр, чи ін),
	 - введенні символьних даних в числові поля(оклад) 
Також виконати перевірку на переповнення даними змінної(checked), що містить значення окладу працівника при збільшенні окладу працівника на деяке число( чи процент), запропонувати ввести повторно  

Створити додатково клас Department(відділ), що міститиме масив(або список List<>) з працівників(Employeе) та 
методи додавання працівників, видалення працівника. 

До класу Department додати обробку виключних ситуацій
	- перевищення розміру масиву працівників відділу(або деякої кількості, якщо обрано колекцію List<>) )
	- видалення даних з пустого масиву(списку), тобто видалення працівника з відділу, якщо в відділі ще немає жодного працівника) 

У програмі надати користувачу можливість здійснити повторну спробу введення даних.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;


namespace _01_employee_exceprtion
{
    class Program
    {
        internal class Employee
        {
            string name;
            string surname;
            string position;
            int salary;
            const int minsalary = 3200;
            private static int ID;
            readonly int contract;
            static List<string> journal = new List<string>();
            readonly DateTime date;

            static Employee()
            {
                ID = 1000;
            }

            Random rand = new Random();

            public Employee(string name = "Unknown", string surname = "Unknown", string position = "Unknown", int salary = minsalary)
            {
                this.Name = name;
                this.Surname = surname;
                this.Position = position;
                this.Salary = salary;
                this.contract = ++ID;
                date = DateTime.Now;
                // шматок коду для різних значень дати
                Thread.Sleep(100);
                date = date.AddDays(rand.Next(-5000, -100));
                //
                journal.Add($"{contract} - {FullName}\t{date}");
            }

            public Employee(string name = "Unknown", string surname = "Unknown") : this(name, surname, "intern", minsalary)
            {
            }

            public Employee()
            {
                this.name = "noname";
                this.surname = "noname";
                this.contract = ++ID;
                journal.Add($"{contract} - {FullName}\t{date}");
            }


            public string Name
            {
                get => name;

                set
                {
                    if (value == null)
                        name = "Noname";
                    else
                        name = value;
                }
            }

            public string Surname
            {
                get => surname;

                set
                {
                    if (value == null)
                        name = "Noname";
                    else
                        surname = value;
                }
            }

            public string Position
            {
                get => position;

                set
                {
                    if (value == null)
                        position = "Trainee";
                    else
                        position = value;
                }
            }

            public int Salary
            {
                get => salary;
                set
                {
                    if (value < minsalary)
                        salary = minsalary;
                    else
                        salary = value;
                }
            }

            public string FullName
            {
                get => name + " " + surname;
            }

            public string Hobby
            { get; set; } = "Unkonwn hobby";

            public long Months
            {
                get
                {
                    TimeSpan span = DateTime.Now - date;
                    long months = span.Days;
                    return months / 12;
                }
            }

            public int Years
            {
                get
                {
                    TimeSpan span = DateTime.Now - date;
                    int years = span.Days;
                    return years / 365;
                }
            }

            public override string ToString()
            {
                return $"\nID:\t\t{contract}\nName:\t\t{FullName}\nPosition:\t{position}\nSalary:\t\t{salary}\nHobby:\t\t{Hobby}\nDate:\t\t{date}\n";
            }

            public static void GetSomePosition(Employee[] arr, string pos)
            {
                Console.WriteLine("\t-= Find for position: " + pos + " =-");
                foreach (var e in arr)
                {

                    if (e.Position == pos)
                        Console.WriteLine(e);
                }
            }

            public static void PrintJournal()
            {
                Console.WriteLine("\tJournals of Employee");
                foreach (var j in journal)
                    Console.WriteLine(j);
            }

            // метод вводу імені  
            public void SetName(string n)
            {
                bool flag = false;
                do
                {

                    try
                    {
                        if (!n.All(x => char.IsLetter(x)) || n == "")
                            throw new Exception();
                        else
                        {
                            Name = n;
                            flag = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Write($"Error! Wrong name '{n}', input correct name with letter:\t");
                        n = Console.ReadLine();
                    }
                } while (!flag);

            }

            // метод вводу прізвищa
            public void SetSurname(string s)
            {
                bool flag = false;
                do
                {

                    try
                    {
                        if (!s.All(x => char.IsLetter(x)) || s == "")
                            throw new Exception();
                        else
                        {
                            Surname = s;
                            flag = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Write($"Error! Wrong name '{s}', input correct name with letter:\t");
                        s = Console.ReadLine();
                    }
                } while (!flag);

            }

            //метод вводу окладу.
            public void SetSalary()
            {
                int salary_;
                bool flag = false;
                do
                {
                    try
                    {
                        checked
                        {
                            Console.Write("Enter salary:\t");
                            salary_ = (ushort)int.Parse(Console.ReadLine());
                            Salary = salary_;
                            flag = true;
                        }
                    }

                    catch (OverflowException)
                    {
                        Console.WriteLine($"Error Overflow Exception!");
                    }

                    catch (FormatException)
                    {
                        Console.WriteLine("Error Format Exception!");
                    }
                } while (!flag);
            }
        }

        public class Department// : Employee
        {
            List<Employee> employees = new List<Employee>();
            //List<Employee> employees;
            //List<int> numbers = new List<int>()

            public void AddEmployee(Employee empl)
            {
                //Employee employee = new Employee();
                    //client.EnterDataClient();
          
                employees.Add(empl);
                 //Shp.Add(new Rectangle(Sh));
               //  employees.Add(new Employee(empl));
                //List<Employee> employees = new List<Employee>();
                //employees.Add(new Employee() {empl});
                //people.Add(new Person() { Name = "Том" });
            }

            public void Print()
                {
                foreach(Employee e in employees)
                    {
                        Console.WriteLine(e);
                    }
                }

        }

        static void Main(string[] args)
        {
            
            Employee e1 = new Employee("John", "Smith", "Director", 20000);
            Employee e2 = new Employee("Dwain", "Parker");
            Employee e3 = new Employee("Jessy", "Johnson", "Engeneer", 1000); // salary буде змінено на 3200
            e3.Hobby = "Reading";
            Employee e4 = new Employee();
            e4.Name = "Brianne";
            e4.Surname = "Lessy";
            //Console.WriteLine(e4);

            /*
            Employee[] arr = { e1, e2, e3, new Employee("Will", "Terner", "HR", 15000) };
            arr[3].Hobby = "Watching TV";
            Console.WriteLine("\t-= Array of employees =-");
            foreach (var e in arr)
            {
                Console.WriteLine(e);
            }

            Employee.GetSomePosition(arr, "HR"); //пошук працівникиів за посадою HR
            Employee.PrintJournal(); // вивід журналу рестрації працівників

            Console.WriteLine($"\n{arr[3].FullName} works {arr[3].Months} months or {arr[3].Years} years");
            e1.SetName("Johntan");
            e1.SetSurname("Walker");
            //e1.SetSalary();
            Console.WriteLine(e1);
            */

            Department dp = new Department();
            
            dp.AddEmployee(e1);
            dp.AddEmployee(e2);
            dp.Print();

            Console.ReadKey();
        }
    }
}
