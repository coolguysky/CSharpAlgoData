using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpAlgoData.Sorting
{
    //SELECTION SORT//O(n^2)
    //INSERTION SORT//O(n^2)
    //BUBBLE SORT/////O(n^2)
    //QUICKSORT///////O(n log(n)) 

    public class SelectionSort
    {
        //BIG-O = O(n^2)
        //there are two loops, essentially
        //two arrays, exchanges smallest number with first number in unsorted
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
        //two arrays but will insert the smallest number in the sorted array
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
                    j--;
                }
            }
            return newArr;

        }
    }

    // BIG-O = O(n^2)
    // for loops
    //will switch the numbers in a "bubble"
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

    //must be unique numbers
    //tutorialspoint version
    //divide and conquer
    // BIG-O = O(n log(n)) 
    // recursion causes the set to divide each time
    public static class QuickSort
    {
        public static void QuickSortFunction(int[] arr, int left, int right)
        {
            int pivot;
            if (left < right)
            {
                pivot = Partition(arr, left, right);
                if (pivot > 1)
                {
                    QuickSortFunction(arr, left, pivot - 1);
                }
                if (pivot + 1 < right)
                {
                    QuickSortFunction(arr, pivot + 1, right);
                }
            }
        }
        public static int Partition(int[] arr, int left, int right)
        {
            int pivot;
            pivot = arr[left];
            while (true)
            {
                while (arr[left] < pivot)
                {
                    left++;
                }
                while (arr[right] > pivot)
                {
                    right--;
                }
                if (left < right)
                {
                    int temp = arr[right];
                    arr[right] = arr[left];
                    arr[left] = temp;
                }
                else
                {
                    return right;
                }
            }
        }
        //int[] arr = new int[] { 3, 2, 4, 1, 0};
        //QuickSort.QuickSortFunction(arr, 0, 4);
        //for (int j = 0; j < 5; j++)
        //{
        //    Console.Write(arr[j] + " ");
        //}
        //Console.WriteLine(" ");
        //int[] arr2 = { 67, 12, 95, 56, 85, 1, 100, 23, 60, 9 };
        //int n = 10, i;
        //QuickSort.QuickSortFunction(arr2, 0, 9);
        //for (i = 0; i < n; i++)
        //{
        //    Console.Write(arr2[i] + " ");
        //}

    }
}
