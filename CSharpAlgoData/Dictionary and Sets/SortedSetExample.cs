using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpAlgoData.Dictionary_and_Sets
{
    class SortedSetExample
    {
    }

    class NameList
    {
        public NameList()
        {
            List<string> names = new List<string>()
            {
                "Marcin",
                "Mary",
                "James",
                "Albert",
                "Lily",
                "Emily",
                "marcin",
                "James",
                "Jane"
            };
            SortedSet<string> sorted = new SortedSet<string>(names); //alphabetical, removes James bu not marcin
            SortedSet<string> sortedLower = new SortedSet<string>( names,
                Comparer<string>.Create((a, b) => a.ToLower().CompareTo(b.ToLower()))); //will not take lowercase into consideration
            foreach (string name in sorted)
            {
                Console.WriteLine(name);
            }
        }
    }


}
