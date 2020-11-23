using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpAlgoData.Sorting
{
    public class InsertionSort
    {
        // BIG-O = O(n^2)
        // for and while loop 
        public T[] InsertioSortFunction<T>(T[] arr) where T : IComparable
        {
            T[] newArr = arr;
            //set to one due to first value being part of
            //sorted set to begin with
            for (int i = 1;  i < newArr.Length; i++) 
            {
                int j = i;
                //the while loop iterates through the sorted set
                //checking each value until correct placement
                while(j > 0 && newArr[j].CompareTo(newArr[j-1]) < 0)
                {
                    T temp = newArr[j];
                    newArr[j] = newArr[j-1];
                    newArr[j-1] = temp;
                }
            }
            return newArr;

        }
    }
}


