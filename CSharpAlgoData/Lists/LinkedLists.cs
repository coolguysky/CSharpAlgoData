using System;
using System.Collections.Generic;

namespace CSharpAlgoData.Lists
{
    public class LinkedLists
    {
        Page pageFirst = new Page() { Content = "First page..." };
        Page pageSecond = new Page() { Content = "Second page..." };
        Page pageThird = new Page() { Content = "Third page..." };
        Page pageFourth = new Page() { Content = "Fourth page..." };
        Page pageFifth = new Page() { Content = "Fifth page..." };
        Page pageSixth = new Page() { Content = "Sixth page..." };
        LinkedList<Page> pages = new LinkedList<Page>();

        public LinkedLists()
        {
            pages.AddLast(pageSecond);
            LinkedListNode<Page> nodePageFourth = pages.AddLast(pageFourth);
            pages.AddLast(pageSixth);
            pages.AddFirst(pageFirst);
            pages.AddBefore(nodePageFourth, pageThird); //need to store a reference to a node to be able to add before or after
            pages.AddAfter(nodePageFourth, pageFifth);
        }

    }

    public class Page
    {
        public string Content { get; set; }
    }
}
