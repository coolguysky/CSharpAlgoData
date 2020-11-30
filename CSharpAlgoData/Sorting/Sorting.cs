using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpAlgoData.Sorting
{
    //SELECTION SORT
    //INSERTION SORT
    //BUBBLE SORT

    public class SelectionSort
    {
        //BIG-O = O(n^2)
        //there are two loops, essentially
        public T[] SectionSortFunction<T>(T[] arr) where T : IComparable
        {
            T[] newArr = arr;
            for (int i = 0; i < newArr.Length - 1; i++)
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

    public class InsertionSort
    {
        // BIG-O = O(n^2)
        // for and while loop 
        public T[] InsertioSortFunction<T>(T[] arr) where T : IComparable
        {
            T[] newArr = arr;
            //set to one due to first value being part of
            //sorted set to begin with
            for (int i = 1; i < newArr.Length; i++)
            {
                int j = i;
                //the while loop iterates through the sorted set
                //checking each value until correct placement
                while (j > 0 && newArr[j].CompareTo(newArr[j - 1]) < 0)
                {
                    T temp = newArr[j];
                    newArr[j] = newArr[j - 1];
                    newArr[j - 1] = temp;
                }
            }
            return newArr;

        }
    }

    public class BubbleSort
    {
        public T[] BubbleSortFunction<T>(T[] arr) where T : IComparable
        {
            T[] newArr = arr;
            for (int i =0; i < arr.Length; i++) //will go through each index to check and switch
            {
                bool isAnyChange = false;
                for (int j = 0; j < arr.Length - 1; j++) //where the comparison occurs
                {
                    if (arr[j].CompareTo(arr[j + 1]) > 0)
                    {
                        isAnyChange = true;
                        T temp = newArr[j];
                        newArr[j] = newArr[j + 1];
                        newArr[j + 1] = temp;
                    }
                }
                if (!isAnyChange)
                {
                    break;
                }
            }
            return newArr;
        }
    }


    //private static void Swap<T>(T[] array, int first, int second)
    //{
    //    T temp = array[first];
    //    array[first] = array[second];
    //    array[second] = temp;
    //}


}
