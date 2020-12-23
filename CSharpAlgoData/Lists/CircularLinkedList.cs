using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace CSharpAlgoData.Lists
{
    public class CircularLinkedList<T> : LinkedList<T>
    {
        public new IEnumerator GetEnumerator()
        {
            return new CircularLinkedListEnumerator<T>(this); // able to iterate through all the elements
        }
    }
    public class CircularLinkedListEnumerator<T> : IEnumerator<T> //need to implement the interface
    {
        private LinkedListNode<T> _current;
        public T Current => _current.Value;
        object IEnumerator.Current => Current;

        public CircularLinkedListEnumerator(LinkedList<T> list)
        {
            _current = list.First;
        }
        public bool MoveNext()
        {
            if (_current == null)
            {
                return false;
            }

            _current = _current.Next ?? _current.List.First; //how it is circular 
            return true;
        }

        public void Reset()
        {
            _current = _current.List.First;
        }

        public void Dispose() { }
    }
    public static class CircularLinkedListExtensions
    {
        public static LinkedListNode<T> Next<T>(this LinkedListNode<T> node)
        {
            if (node != null && node.List != null)
            {
                return node.Next ?? node.List.First;
            }
            return null;
        }

        public static LinkedListNode<T> Previous<T>(this LinkedListNode<T> node)
        {
            if (node != null && node.List != null)
            {
                return node.Previous ?? node.List.Last;
            }
            return null;
        }
    }
    public class SpinWheel
    {
        CircularLinkedList<string> categories = new CircularLinkedList<string>();

        public SpinWheel()
        {
            categories.AddLast("Sport");
            categories.AddLast("Culture");
            categories.AddLast("History");
            categories.AddLast("Geography");
            categories.AddLast("People");
            categories.AddLast("Technology");
            categories.AddLast("Nature");
            categories.AddLast("Science");
            BeginTheGame();
            }
        public void BeginTheGame()
        {
            Random random = new Random();
            int totalTime = 0;
            int remainingTime = 0;

            foreach (string category in categories) //will go fast to slow
            {
                if (remainingTime <= 0) //will stop at the category and continue after if the user decides to, like a wheel
                {
                    Console.WriteLine("Press [Enter] to start or any other to exit.");
                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.Enter:
                        totalTime = random.Next(1000, 5000);
                        remainingTime = totalTime;
                        break;
                        default:
                        return;
                    }
                }

                int categoryTime = (-450 * remainingTime) / (totalTime - 50)
                    + 500 + (22500 / (totalTime - 50));
                remainingTime -= categoryTime;
                Thread.Sleep(categoryTime);

                Console.ForegroundColor = remainingTime <= 0
                    ? ConsoleColor.Red : ConsoleColor.Gray;
                Console.WriteLine(category);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
    }

}
