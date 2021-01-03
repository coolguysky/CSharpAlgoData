using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpAlgoData.Trees
{
    class BasicTree
    {
        public BasicTree()
        {
            Tree<int> tree = new Tree<int>();
            tree.Root = new TreeNode<int>() { Data = 100 };
            tree.Root.Children = new List<TreeNode<int>>
            {
                new TreeNode<int>() { Data = 50, Parent = tree.Root },
                new TreeNode<int>() { Data = 1, Parent = tree.Root },
                new TreeNode<int>() { Data = 150, Parent = tree.Root }
            };
            tree.Root.Children[0].Children = new List<TreeNode<int>>()
            {
                new TreeNode<int>(){ Data = 12, Parent = tree.Root.Children[0] }
            };
            tree.Root.Children[1].Children = new List<TreeNode<int>>()
            {
                new TreeNode<int>(){ Data = 70, Parent = tree.Root.Children[1] },
                new TreeNode<int>(){ Data = 61, Parent = tree.Root.Children[1] }
            };
            tree.Root.Children[2].Children = new List<TreeNode<int>>()
            {
                new TreeNode<int>(){ Data = 30, Parent = tree.Root.Children[2] },
                new TreeNode<int>(){ Data = 5, Parent = tree.Root.Children[2] },
                new TreeNode<int>(){ Data = 11, Parent = tree.Root.Children[2] }
            };
        }
    }

    public class TreeNode<T>
    {
        public T Data { get; set; }
        public TreeNode<T> Parent { get; set; }
        public List<TreeNode<T>> Children { get; set; }

        public int GetHeight()
        {
            int height = 1;
            TreeNode<T> current = this;
            while (current.Parent != null)
            {
                height++;
                current = current.Parent;
            }
            return height;
        }
    }

    public class Tree<T>
    {
        public TreeNode<T> Root { get; set; } //find root, then access the children until leafs
    }
}
