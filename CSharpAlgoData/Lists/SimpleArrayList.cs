using System;
using System.Collections;

namespace CSharpAlgoData.Lists
{
    //when not generic, can hold different types
    public class SimpleArrayList
    {
        ArrayList arrayList = new ArrayList();
        public void NonGeneric()
        {
            arrayList.Add(5);
            arrayList.AddRange(new int[] { 6, -7, 8 });
            arrayList.AddRange(new object[] { "Marcin", "Mary" });
            arrayList.Insert(5, 7.8);

            object first = arrayList[0];
            int third = (int)arrayList[2]; // casting required as the array list stores objects

            foreach (object element in arrayList)
            {
                Console.WriteLine(element);
            }

            int count = arrayList.Count; //elements in the arraylist
            int capacity = arrayList.Capacity; //how many can be stored in it/usually count+1

            bool containsMary = arrayList.Contains("Mary");

            int minusIndex = arrayList.IndexOf(-7);

            arrayList.Remove(5);

            arrayList.RemoveAt(0);

            arrayList.RemoveRange(0, 3); //removes 3 elements

            arrayList.Reverse();// obvious

            arrayList.Clear(); // remove all
        }
        
        
    }
}

//SimpleArrayList arr = new SimpleArrayList();
//arr.NonGeneric();
