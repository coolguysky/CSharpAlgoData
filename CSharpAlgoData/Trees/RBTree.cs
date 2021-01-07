using System;
using System.Collections.Generic;
using System.Text;
using TreeLib;

namespace CSharpAlgoData.Trees
{
    //log n
    class RBTree
    {
        public RBTree()
        {
            RedBlackTreeList<int> tree = new RedBlackTreeList<int>();
            for (int i = 1; i <= 10; i++)
            {
                tree.Add(i);
            }
            bool contains = tree.ContainsKey(5);
            Console.WriteLine(
                "Does value exist? " + (contains ? "yes" : "no"));
            uint count = tree.Count;
            tree.Greatest(out int greatest);
            tree.Least(out int least);
            Console.WriteLine(
                $"{count} elements in the range {least}-{greatest}");
            Console.WriteLine("Values: " + string.Join(", ", tree.GetEnumerable()));
            Console.Write("Values: ");
            foreach (EntryList<int> node in tree)
            {
                Console.Write(node + " ");
            }
        }
    }
}
