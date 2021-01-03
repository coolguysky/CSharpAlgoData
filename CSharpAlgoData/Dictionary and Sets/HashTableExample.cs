using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CSharpAlgoData.Dictionary_and_Sets
{
    class HashTableExample
    {
        Hashtable phoneBook = new Hashtable() //create and initialize a hashtable with collections, non-generic ie no <>
            {
                { "Marcin Jamro", "000-000-000" }, // add key and value
                { "John Smith", "111-111-111" } //add key and value
            };

        public HashTableExample ()
        {
            phoneBook["Lily Smith"] = "333-333-333"; //add key and value

            try
            {
                phoneBook.Add("Mary Fox", "222-222-2222"); //add but check if key is used
            }
            catch (ArgumentException)
            {

                Console.WriteLine("The entry already exisits");
            }

            Console.WriteLine("Phone numbers: ");
            if(phoneBook.Count == 0)
            {
                Console.WriteLine("Empty");
            }
            else
            {
                foreach (DictionaryEntry item in phoneBook)
                {
                    Console.WriteLine($" - {item.Key}: {item.Value}"); //how to get data 
                }
            }

            Console.WriteLine();
            Console.WriteLine("Search by name: ");
            string name = Console.ReadLine();
            if (phoneBook.Contains(name))
            {
                string number = (string)phoneBook[name]; //need to cast since non-genric
                Console.WriteLine($"Found phone number: {number}");
            }
            else
            {
                Console.WriteLine("The entry does not exist.");
            }
            
        }
    }
}
