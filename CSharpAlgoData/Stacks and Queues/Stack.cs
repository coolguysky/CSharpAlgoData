using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpAlgoData.Stacks_and_Queues
{
    public class Stack {}
    public class StackReverseWord
    {
        Stack<char> chars = new Stack<char>();
        string word = "Greggory";

        public StackReverseWord()
        {
            foreach (char c in word)
            {
                chars.Push(c);
            }

            while(chars.Count > 0)
            {
                Console.Write(chars.Pop()); //to fit on one line
            }
            Console.WriteLine();//to return
        }
    }
    public class StackTowerofHanoi
    {
        //complete later
    }
}

//StackReverseWord s = new StackReverseWord();