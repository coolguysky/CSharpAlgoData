using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CSharpAlgoData.Dictionary_and_Sets
{
    class HashSetExample
    {
    }

    class Coupons
    {
        public Coupons()
        {
            HashSet<int> usedCoupons = new HashSet<int>();
            do
            {
                Console.Write("Enter the coupon number: ");
                string couponString = Console.ReadLine();
                if (int.TryParse(couponString, out int coupon))
                {
                    if (usedCoupons.Contains(coupon))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("It has been already used :-(");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        usedCoupons.Add(coupon);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Thank you! :-)");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }
                else
                {
                    break;
                }
            }
            while (true);

            Console.WriteLine();
            Console.WriteLine("A list of used coupons:");
            foreach (int coupon in usedCoupons)
            {
                Console.WriteLine(coupon);
            }
        }
    }
    class Swimming
    {
        private static Random random = new Random();
        public enum PoolTypeEnum
        {
            RECREATION,
            COMPETITION,
            THERMAL,
            KIDS
        };
        public Swimming()
        {
            Dictionary<PoolTypeEnum, HashSet<int>> tickets = new Dictionary<PoolTypeEnum, HashSet<int>>()
            {
                { PoolTypeEnum.RECREATION, new HashSet<int>() },
                { PoolTypeEnum.COMPETITION, new HashSet<int>() },
                { PoolTypeEnum.THERMAL, new HashSet<int>() },
                { PoolTypeEnum.KIDS, new HashSet<int>() }
            };

            for (int i = 1; i < 100; i++)
            {
                foreach (KeyValuePair<PoolTypeEnum, HashSet<int>> type in tickets)
                {
                    if (GetRandomBoolean())
                    {
                        type.Value.Add(i); //adding a value to a set/false if already there
                    }
                }
            }

            Console.WriteLine("Number of visitors by a pool type:");
            foreach (KeyValuePair<PoolTypeEnum, HashSet<int>> type in tickets)
            {
                Console.WriteLine($" - {type.Key.ToString().ToLower()}:  { type.Value.Count} "); //will show how many were added
            }

            //use LINQ to show

            PoolTypeEnum maxVisitors = tickets.OrderByDescending(t => t.Value.Count).Select(t => t.Key).FirstOrDefault();
            Console.WriteLine($"Pool '{maxVisitors.ToString().ToLower()}' was the most popular.");

            //can also print a list of numbers for that pool

            var visitorsForMax = tickets.OrderByDescending(t => t.Value.Count).Select(t => t.Value).FirstOrDefault(); ; //the first t is the highest-I want its value
            foreach (int type in visitorsForMax)
            {
                Console.Write($" {type} "); //will show the numbers added
            }
            Console.WriteLine();
            Console.WriteLine($"Pool '{maxVisitors.ToString().ToLower()}' was the most popular.");
        }

        private static bool GetRandomBoolean()
        {
            return random.Next(2) == 1;
        }

        
    }
}
