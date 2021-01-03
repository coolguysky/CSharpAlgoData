using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CSharpAlgoData.Dictionary_and_Sets
{
    public class DictionaryExample
    {
    }

    public class Catalog 
    { 
        public Catalog()
        {
            
            Dictionary<string, string> example = new Dictionary<string, string>();
            Dictionary<string, string> products = new Dictionary<string, string>
            {
                { "5900000000000", "A1" },
                { "5901111111111", "B5" },
                { "5902222222222", "C9" }
            };

            products["5903333333333"] = "D7";

            try
            {
                products.Add("5904444444444", "A3");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("The entry already exists.");
            }

            Console.WriteLine("All products:");
            if (products.Count == 0)
            {
                Console.WriteLine("Empty");
            }
            else
            {
                List<string> barcodesInOrder = products.OrderBy(b => b.Key)
                                                .Select(b => b.Key)
                                                .ToList();

                foreach (string b in barcodesInOrder) //Hashtable uses DictionaryEntry (non-generic)
                {
                    Console.WriteLine(b);
                }


                List<string> areasInOrder = products.OrderBy(b => b.Value)
                                                .Select(b => b.Value)
                                                .ToList();
                foreach (string area in areasInOrder) //Hashtable uses DictionaryEntry (non-generic)
                {
                    Console.WriteLine(area);
                }


                foreach (KeyValuePair<string, string> product in products) //Hashtable uses DictionaryEntry (non-generic)
                {
                    Console.WriteLine($" - {product.Key}: {product.Value}");
                }

                Console.WriteLine();
                Console.Write("Search by barcode: ");
                string barcode = Console.ReadLine();
                if (products.TryGetValue(barcode, out string location)) // to check whether the element exists, TryGetValue method uses the out parameter to return the found value of the element
                {
                    Console.WriteLine($"The product is in the area {location}.");
                }
                else
                {
                    Console.WriteLine("The product does not exist.");
                }
            }


        }
    }

    class EmployeesForLedger 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }


    class Ledger 
    {

        public Ledger()
        {
            Dictionary<int, EmployeesForLedger> employees = new Dictionary<int, EmployeesForLedger>();
            employees.Add(100, new EmployeesForLedger()
            {
                FirstName = "Marcin",
                LastName = "Jamro",
                PhoneNumber = "000-000-000"
            });
            employees.Add(210, new EmployeesForLedger()
            {
                FirstName = "Mary",
                LastName = "Fox",
                PhoneNumber = "111-111-111"
            });
            employees.Add(303, new EmployeesForLedger()
            {
                FirstName = "John",
                LastName = "Smith",
                PhoneNumber = "222-222-222"
            });

            bool isCorrect = true;
            do
            {
                Console.Write("Enter the employee identifier: ");
                string idString = Console.ReadLine();
                isCorrect = int.TryParse(idString, out int id);
                if (isCorrect)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    if (employees.TryGetValue(id, out EmployeesForLedger employee))
                    {
                        Console.WriteLine("First name: {1}{0}Last name: {2}{0}Phone number: {3}",
                            Environment.NewLine, // 0 a string containing a new line for unix and non-unix platforms
                            employee.FirstName, // 1
                            employee.LastName, //2
                            employee.PhoneNumber);//3
                    }
                    else
                    {
                        Console.WriteLine("The employee with the given identifier does not exist."); 
                    }
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
            while (isCorrect);
        }
    }


}

////ToDictionary is nice for List to Dict

//List<Package> packages = new List<Package> 
//{
//    new Package { Company = "Coho Vineyard", Weight = 25.2, TrackingNumber = 89453312L },
//    new Package { Company = "Lucerne Publishing", Weight = 18.7, TrackingNumber = 89112755L },
//    new Package { Company = "Wingtip Toys", Weight = 6.0, TrackingNumber = 299456122L },
//    new Package { Company = "Adventure Works", Weight = 33.8, TrackingNumber = 4665518773L } 
//};


//// Create a Dictionary of Package objects,
//// using TrackingNumber as the key.
//Dictionary<long, Package> dictionary =
//    packages.ToDictionary(p => p.TrackingNumber);

//foreach (KeyValuePair<long, Package> kvp in dictionary)
//{
//    Console.WriteLine(
//        "Key {0}: {1}, {2} pounds",
//        kvp.Key,
//        kvp.Value.Company,
//        kvp.Value.Weight);
//}