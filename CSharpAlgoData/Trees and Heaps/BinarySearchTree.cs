using CSharpAlgoData.Trees;
using System;
using System.Text;
using System.Collections.Generic;

namespace CSharpAlgoData.Trees
{
    //The presented implementation of the BST is based on the code shown at https://en.wikipedia.org/wiki/Binary_search_tree.
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
                throw new ArgumentException(
                    $"The node {data} does not exist.");
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
                    BinaryTreeNode<T> successor =
                        FindMinimumInSubtree(node.Right);
                    node.Data = successor.Data;
                    Remove(successor, successor.Data);
                }
            }
        }

        private void ReplaceInParent(BinaryTreeNode<T> node,BinaryTreeNode<T> newNode)
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

    public class BTSExample
    {
        private const int COLUMN_WIDTH = 5;
        
        public BTSExample()
        {
            Console.OutputEncoding = Encoding.UTF8;

            BinarySearchTree<int> tree = new BinarySearchTree<int>();
            tree.Root = new BinaryTreeNode<int>() { Data = 100 };
            tree.Root.Left = new BinaryTreeNode<int>()
            { Data = 50, Parent = tree.Root };
            tree.Root.Right = new BinaryTreeNode<int>()
            { Data = 150, Parent = tree.Root };
            tree.Count = 3;
            VisualizeTree(tree, "The BST with three nodes (50, 100, 150):");


            tree.Add(75);
            tree.Add(125);
            VisualizeTree(tree, "The BST after adding two nodes (75, 125):");
    

            tree.Remove(25);
            VisualizeTree(tree, "The BST after removing the node 25:");
 
        //Console.Write("Pre-order traversal:\t");
        //    Console.Write(string.Join(", ", tree.Traverse(TraversalEnum.PREORDER).Select(n => n.Data))); //issue with not being in main
        //    Console.Write("\nIn-order traversal:\t");
        //    Console.Write(string.Join(", ", tree.Traverse(TraversalEnum.INORDER).Select(n => n.Data)));
        //    Console.Write("\nPost-order traversal:\t");
        //    Console.Write(string.Join(", ", tree.Traverse(TraversalEnum.POSTORDER).Select(n => n.Data)));
        }

        private void VisualizeTree(BinarySearchTree<int> tree, string caption)
        {
            char[][] console = InitializeVisualization(tree, out int width);
            VisualizeNode(tree.Root, 0, width / 2, console, width);
            Console.WriteLine(caption);
            foreach (char[] row in console)
            {
                Console.WriteLine(row);
            }
        }

        private char[][] InitializeVisualization(BinarySearchTree<int> tree, out int width)
        {
            int height = tree.GetHeight();
            width = (int)Math.Pow(2, height) - 1;
            char[][] console = new char[height * 2][];
            for (int i = 0; i < height * 2; i++)
            {
                console[i] = new char[COLUMN_WIDTH * width];
            }
            return console;
        }

        private static void VisualizeNode(BinaryTreeNode<int> node, int row, int column, char[][] console, int width)
        {
            if (node != null)
            {
                char[] chars = node.Data.ToString().ToCharArray();
                int margin = (COLUMN_WIDTH - chars.Length) / 2;
                for (int i = 0; i < chars.Length; i++)
                {
                    console[row][COLUMN_WIDTH * column + i + margin]
                        = chars[i];
                }

                int columnDelta = (width + 1) /
                    (int)Math.Pow(2, node.GetHeight() + 1);
                VisualizeNode(node.Left, row + 2, column - columnDelta,
                    console, width);
                VisualizeNode(node.Right, row + 2, column + columnDelta,
                    console, width);
                DrawLineLeft(node, row, column, console, columnDelta);
                DrawLineRight(node, row, column, console, columnDelta);
            }
        }
        private static void DrawLineLeft(BinaryTreeNode<int> node,int row, int column, char[][] console, int columnDelta)
        {
            if (node.Left != null)
            {
                int startColumnIndex =
                    COLUMN_WIDTH * (column - columnDelta) + 2;
                int endColumnIndex = COLUMN_WIDTH * column + 2;
                for (int x = startColumnIndex + 1;
                    x < endColumnIndex; x++)
                {
                    console[row + 1][x] = '-';
                }
                console[row + 1][startColumnIndex] = '\u250c';
                console[row + 1][endColumnIndex] = '+';
            }
        }
        private static void DrawLineRight(BinaryTreeNode<int> node, int row, int column, char[][] console, int columnDelta)
        {
            if (node.Right != null)
            {
                int startColumnIndex = COLUMN_WIDTH * column + 2;
                int endColumnIndex =
                    COLUMN_WIDTH * (column + columnDelta) + 2;
                for (int x = startColumnIndex + 1;
                    x < endColumnIndex; x++)
                {
                    console[row + 1][x] = '-';
                }
                console[row + 1][startColumnIndex] = '+';
                console[row + 1][endColumnIndex] = '\u2510';
            }
        }
    }


}
