using System;
using CSharpAlgoData.Sorting;

namespace CSharpAlgoData
{
    class Program
    {
        static void Main(string[] args)
        {
            BubbleSort b = new BubbleSort();
            int[] arr = new int[] { 3, 2, 4, 1, 4, 0 };
            int[] a = b.BubbleSortFunction(arr);
            foreach (var item in a)
            {
                Console.Write(item);
            }




        }
    }
}
