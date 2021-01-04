using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpAlgoData.Trees
{
    //The presented implementation of the BST is based on the code shown at https://en.wikipedia.org/wiki/Binary_search_tree.
    class BinarySearchTree
    {
    }

    public class TreeNode<T>
    {
        public T Data { get; set; }
        public List<TreeNode<T>> Children { get; set; }
    }

    public class BinaryTreeNode<T> : TreeNode<T>
    {
        public BinaryTreeNode() => Children =
        new List<TreeNode<T>>() { null, null };

        public BinaryTreeNode<T> Parent { get; set; }

        public BinaryTreeNode<T> Left
        {
            get { return (BinaryTreeNode<T>)Children[0]; }
            set { Children[0] = value; }
        }

        public BinaryTreeNode<T> Right
        {
            get { return (BinaryTreeNode<T>)Children[1]; }
            set { Children[1] = value; }
        }

        public int GetHeight()
        {
            int height = 1;
            BinaryTreeNode<T> current = this;
            while (current.Parent != null)
            {
                height++;
                current = current.Parent;
            }
            return height;
        }
    }
    public class BinaryTree<T>
    {
        public BinaryTreeNode<T> Root { get; set; }
        public int Count { get; set; }
    }
    public class BinarySearchTree<T> : BinaryTree<T> where T : IComparable
    {
        public bool Contains(T data)
        {
            BinaryTreeNode<T> node = Root;
            while (node != null)
            {
                int result = data.CompareTo(node.Data);
                if (result == 0)
                {
                    return true;
                }
                else if (result < 0)
                {
                    node = node.Left;
                }
                else
                {
                    node = node.Right;
                }
            }
            return false;
        }
        public void Add(T data)
        {
            BinaryTreeNode<T> parent = GetParentForNewNode(data);
            BinaryTreeNode<T> node = new BinaryTreeNode<T>()
            { Data = data, Parent = parent };

            if (parent == null)
            {
                Root = node;
            }
            else if (data.CompareTo(parent.Data) < 0)
            {
                parent.Left = node;
            }
            else
            {
                parent.Right = node;
            }

            Count++;
        }
        private BinaryTreeNode<T> GetParentForNewNode(T data)
        {
            BinaryTreeNode<T> current = Root;
            BinaryTreeNode<T> parent = null;
            while (current != null)
            {
                parent = current;
                int result = data.CompareTo(current.Data);
                if (result == 0)
                {
                    throw new ArgumentException(
                        $"The node {data} already exists.");
                }
                else if (result < 0)
                {
                    current = current.Left;
                }
                else
                {
                    current = current.Right;
                }
            }

            return parent;
        }
        public void Remove(T data)
        {
            Remove(Root, data);
        }
        private void Remove(BinaryTreeNode<T> node, T data)
        {
            if (node == null)
            {
                throw new ArgumentException($"The node {data} does not exist.");
            }
            else if (data.CompareTo(node.Data) < 0)
            {
                Remove(node.Left, data);
            }
            else if (data.CompareTo(node.Data) > 0)
            {
                Remove(node.Right, data);
            }
            else
            {
                if (node.Left == null && node.Right == null)
                {
                    ReplaceInParent(node, null);
                    Count--;
                }
                else if (node.Right == null)
                {
                    ReplaceInParent(node, node.Left);
                    Count--;
                }
                else if (node.Left == null)
                {
                    ReplaceInParent(node, node.Right);
                    Count--;
                }
                else
                {
                    BinaryTreeNode<T> successor = FindMinimumInSubtree(node.Right);
                    node.Data = successor.Data;
                    Remove(successor, successor.Data);
                }
            }
        }
        private void ReplaceInParent(BinaryTreeNode<T> node, BinaryTreeNode<T> newNode)
        {
            if (node.Parent != null)
            {
                if (node.Parent.Left == node)
                {
                    node.Parent.Left = newNode;
                }
                else
                {
                    node.Parent.Right = newNode;
                }
            }
            else
            {
                Root = newNode;
            }

            if (newNode != null)
            {
                newNode.Parent = node.Parent;
            }
        }
        private BinaryTreeNode<T> FindMinimumInSubtree(BinaryTreeNode<T> node)
        {
            while (node.Left != null)
            {
                node = node.Left;
            }
            return node;
        }
    }
}
