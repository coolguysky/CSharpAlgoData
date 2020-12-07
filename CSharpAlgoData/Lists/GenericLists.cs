using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CSharpAlgoData.Lists
{
    //Add, AddRange, Clear, Contains, IndexOf, Insert,
    //InsertRange, LastIndexOf, Remove, RemoveAt, RemoveRange,
    //Reverse, and ToArray 
    //
    //System.Linq -- finding the minimum or maximum value (Min or Max),
    //calculating the average (Average),
    //ordering in an ascending or descending order (OrderBy or OrderByDescending),
    //as well as checking whether all the elements in the list satisfy a condition (All)
    class GenericLists
    {

        public void FindAverageValue()
        {
            List<double> numbers = new List<double>();
            do
            {
                Console.Write("Enter the number: ");
                string numberString = Console.ReadLine();
                //double num = Convert.ToDouble(numberString);
                if (!double.TryParse(numberString, NumberStyles.Float,
                    new NumberFormatInfo(), out double number)) //way to convert string to float and check
                {
                    break;
                }

                numbers.Add(number);
                Console.WriteLine($"The average value: {numbers.Average()}"); // Average is LINQ
            }
            while (true);
        }

        public void ListOfPeople()
        {
            List<Person> people = new List<Person>(); 
            people.Add(new Person() { 
                Name = "Marcin",
                Country = Person.CountryEnum.PL,
                Age = 29
            });
            people.Add(new Person() {
                Name = "Sabine",
                Country = Person.CountryEnum.DE,
                Age = 25
            });
            people.Add(new Person() { 
                Name = "Ann",
                Country = Person.CountryEnum.PL,
                Age = 31
            });

            List<Person> results = people.OrderBy(p => p.Name).ToList();

            foreach (Person person in results)
            {
                Console.WriteLine($"{person.Name} ({person.Age} years) " +
                    $"from { person.Country}."); 
            }

            List<string> names = people.Where(p => p.Age <= 30) //linq method syntax
                                       .OrderBy(p => p.Name)
                                       .Select(p => p.Name)
                                       .ToList();

            List<string> namesTwo = (from p in people     //linq query syntax
                                     where p.Age <= 30
                                     orderby p.Name
                                     select p.Name).ToList();
        }
    }

    public class Person
    {
        public string Name { get; set; } //3 public properties
        public int Age { get; set; }
        public CountryEnum Country { get; set; }
        public enum CountryEnum
        {
            PL,
            UK,
            DE
        }
    }
}
