using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpAlgoData.Sorting
{
    public class SelectionSort
    {
        //BIG-O = O(n^2)
        //there are two loops, essentially
        public T[] SectionSortFunction<T>(T[] arr) where T: IComparable
        {
            T[] newArr = arr;
            for(int i = 0; i < newArr.Length-1; i++)
            {
                int minIndex = i;
                T minValue = newArr[i];
                //this loop will evaluate the last item in the array
                for (int j = i + 1; j < newArr.Length; j++)
                {
                    //less than = comes before
                    if (newArr[j].CompareTo(minValue) < 0) 
                    {
                        minIndex = j;
                        minValue = newArr[j];
                    }
                }
                T temp = newArr[i];
                newArr[i] = newArr[minIndex];
                newArr[minIndex] = temp;
            }
            return newArr;
        }

    }
}
