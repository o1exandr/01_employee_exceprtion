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

 /*
  2) Доповнити клас Department  реалізацією інтерфейсу IEnumerable.
	для  класу Відділ- при використанні   foreach повиннен проходитися  масив(чи список) з працівників

При потребі виконати завдання за  допомогою ітератора(yield).
  */

using System;
using System.Collections.Generic;
using System.Collections;
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
                    catch (Exception)
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
                    catch (Exception)
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

        public class Department : IEnumerable
        {
            const int maxEmpl = 5;
            List<Employee> employees = new List<Employee>();

            // додати працівника
            public void AddEmployee(Employee empl)
            {
                try
                {
                    if (employees.Count >= maxEmpl)
                        throw new Exception();
                    employees.Add(empl);
                    Console.WriteLine($"\nEmployee {empl.FullName} ADD to department");
                }
                
                catch(Exception)
                {
                    Console.WriteLine("\n\tError! Department can contain only 5 employees!");
                }
  
            }

            // видалити працівника
            public void DelEmployee()
            {
                bool flag = true;
                do
                {
                    int count = 0;
                    // виводимо список для того щоб користувач обрав номер працівника якого потрібно видалити
                    foreach(Employee e in employees)
                    {
                        Console.WriteLine($"({count++})\t{e.FullName}\t{e.Position}");
                    }

                    Console.Write("\nSelect number of employee for delete:\t");
                    string tmp = Console.ReadLine();

                    try
                    {
                        if (int.TryParse(tmp, out int index))// && index < employees.Count) //якщо це число і воно не виходить за межі кількості елементів - видаляємо
                        {
                            Console.WriteLine($"({index}) {employees[index].FullName}\tDELETED");
                            employees.Remove(employees[index]);
                           flag = false;
                        }
                    else
                        Console.WriteLine($"'{tmp}' is not correct index");
                    }
     
                    // обробку виключення видалення даних з пустого масиву(списку), або при індексі, який виходить за межі ємності ліста 
                    catch(ArgumentOutOfRangeException)
                    {
                        if (employees.Count == 0)
                            {
                                Console.WriteLine("\n\tError! List is empty!");
                                flag = false;
                            }
                        else
                            Console.WriteLine("\n\tError! Out of range");
                    }
                }while (flag);
               
            }

            public void Print()
            {
                Console.WriteLine("\n\tList of employees");
                foreach(Employee e in employees)
                    {
                        Console.WriteLine(e);
                    }
            }

            public void PrintShort()
            {
                Console.WriteLine("\n\tList of employees");
                int index = 0;
                foreach(Employee e in employees)
                    {
                        Console.WriteLine($"({index++}) {e.FullName}\t{e.Position}");
                    }
            }

            // для  класу Відділ- при використанні   foreach повиннен проходитися  масив(чи список) з працівників
            public IEnumerator GetEnumerator()
            {
                foreach(Employee e in employees)
                {
                        yield return e;

                }
               
            }

        }

        static void Main(string[] args)
        {
            
            Employee e = new Employee("John", "Smith", "Director", 20000);
            //e.SetName("John1");
            //e.SetSurname("Smith;");
            //e.SetSalary();
            //Console.WriteLine(e);
            
            Department dp = new Department();
            
            
            // dp.DelEmployee(); //видаляємо з порожнього списку
        
            dp.AddEmployee(e);
            dp.AddEmployee(new Employee("Dwain", "Parker"));
            dp.AddEmployee(new Employee("Jessy", "Johnson", "Engeneer", 15000));
            //dp.Print();
            //dp.DelEmployee(); // видаляємо із списку з 3ма працівниками, якщо ввести номер 3 чи більше, програма виводить помилку про вихід за межі і просить ввести коректний індекс
            //dp.PrintShort(); //після видалення у департаменті 2 працівника
            
            //for(int i = 0; i < 4; i++) // додаємо ще 4 і на останньому працівнику ловимо виключення про перевищення ємності департаменту
                //dp.AddEmployee(e);
            // обхід перелічувального обєкта
            Console.WriteLine("\n\tIEnumerator GetEnumerator()");
            foreach (Employee t in dp) 
                Console.WriteLine(t);
            
            //dp.PrintShort();

            Console.ReadKey();
        }
    }
}
