using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpAlgoData.Lists
{
    //key-value pair
    class SortedLists
    {
        //Person located in GenericLists
        public void KeyValue()
        {
            SortedList<string, Person> people =
                new SortedList<string, Person>(); //keys are sorted A-Z

            people.Add("Marcin", new Person()
            {
                Name = "Marcin",
                Country = Person.CountryEnum.PL,
                Age = 29
            });
            people.Add("Sabine", new Person()
            {
                Name = "Sabine",
                Country = Person.CountryEnum.DE,
                Age = 25
            });
            people.Add("Ann", new Person()
            {
                Name = "Ann",
                Country = Person.CountryEnum.PL,
                Age = 31
            });

            foreach (KeyValuePair<string, Person> person in people) //need to add Value
            {
                Console.WriteLine($"{person.Value.Name} ({person.Value.Age} " +
                    $"years) from { person.Value.Country}."); 
            } 
        }
    }

    

}
