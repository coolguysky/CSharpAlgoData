using PommaLabs.Hippie;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpAlgoData.Trees_and_Heaps
{
    class FibonacciHeap
    {

        public FibonacciHeap()
        {
            List<int> unsorted = new List<int>() { 50, 33, 78, -23, 90, 41 };
            MultiHeap<int> heap = HeapFactory.NewFibonacciHeap<int>();
            unsorted.ForEach(i => heap.Add(i));
            Console.WriteLine("Unsorted: " + string.Join(", ", unsorted));

            List<int> sorted = new List<int>(heap.Count);
            while (heap.Count > 0)
            {
                sorted.Add(heap.RemoveMin());
            }
            Console.WriteLine("Sorted: " + string.Join(", ", sorted));
        }
    }
}
