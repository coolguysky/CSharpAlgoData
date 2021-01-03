using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpAlgoData.Trees
{
    class BinaryTree
    {
    }

    public class BinaryTreeNode<T> : TreeNode<T> //in basic tree
    {
        public BinaryTreeNode() => Children =
            new List<TreeNode<T>>() { null, null };

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
    }
    public class BinaryTree<T>
    {
        public enum TraversalEnum
        {
            PREORDER,
            INORDER,
            POSTORDER
        }
        public BinaryTreeNode<T> Root { get; set; }
        public int Count { get; set; }

        private void TraversePreOrder(BinaryTreeNode<T> node, List<BinaryTreeNode<T>> result)
        {
            if (node != null)
            {
                result.Add(node);
                TraversePreOrder(node.Left, result);
                TraversePreOrder(node.Right, result);
            }
        }

        private void TraverseInOrder(BinaryTreeNode<T> node, List<BinaryTreeNode<T>> result)
        {
            if (node != null)
            {
                TraverseInOrder(node.Left, result);
                result.Add(node);
                TraverseInOrder(node.Right, result);
            }
        }

        private void TraversePostOrder(BinaryTreeNode<T> node, List<BinaryTreeNode<T>> result)
        {
            if (node != null)
            {
                TraversePostOrder(node.Left, result);
                TraversePostOrder(node.Right, result);
                result.Add(node);
            }
        }

        public List<BinaryTreeNode<T>> Traverse(TraversalEnum mode)
        {
            List<BinaryTreeNode<T>> nodes = new List<BinaryTreeNode<T>>();
            switch (mode)
            {
                case TraversalEnum.PREORDER:
                TraversePreOrder(Root, nodes);
                break;
                case TraversalEnum.INORDER:
                TraverseInOrder(Root, nodes);
                break;
                case TraversalEnum.POSTORDER:
                TraversePostOrder(Root, nodes);
                break;
            }
            return nodes;
        }

        public int GetHeight()
        {
            int height = 0;
            foreach (BinaryTreeNode<T> node
                in Traverse(TraversalEnum.PREORDER))
            {
                height = Math.Max(height, node.GetHeight());
            }
            return height;
        }
    }

    public class QuizItem
    {
        public string Text { get; set; }
        public QuizItem(string text) => Text = text;
    }

    public class QuizItemImplementation
    {
        public QuizItemImplementation()
        {
            BinaryTree<QuizItem> tree = GetTree();
            BinaryTreeNode<QuizItem> node = tree.Root;
            while (node != null)
            {
                if (node.Left != null || node.Right != null)
                {
                    Console.Write(node.Data.Text);
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.Y:
                        WriteAnswer(" Yes");
                        node = node.Left;
                        break;
                        case ConsoleKey.N:
                        WriteAnswer(" No");
                        node = node.Right;
                        break;
                    }
                }
                else
                {
                    WriteAnswer(node.Data.Text);
                    node = null;
                }
            }

        }

        private static BinaryTree<QuizItem> GetTree()
        {
            BinaryTree<QuizItem> tree = new BinaryTree<QuizItem>();
            tree.Root = new BinaryTreeNode<QuizItem>()
            {
                Data = new QuizItem("Do you have experience in developingapplications ? "),

                Children = new List<TreeNode<QuizItem>>()
                {
                    new BinaryTreeNode<QuizItem>()
                    {
                        Data = new QuizItem("Have you worked as a developer for more than 5 years ? "),
                        Children = new List<TreeNode<QuizItem>>()
                        {
                            new BinaryTreeNode<QuizItem>() {Data = new QuizItem("Apply as a senior developer!") },
                            new BinaryTreeNode<QuizItem>() {Data = new QuizItem("Apply as a middle developer!") }
                        }
                    },
                    new BinaryTreeNode<QuizItem>()
                    {
                        Data = new QuizItem("Have you completed the university?"),
                        Children = new List<TreeNode<QuizItem>>()
                        {
                            new BinaryTreeNode<QuizItem>()
                            {
                                Data = new QuizItem("Apply for a junior developer!")
                            },
                            new BinaryTreeNode<QuizItem>()
                            {
                                Data = new QuizItem("Will you find some time during the semester?"),
                                Children = new List<TreeNode<QuizItem>>()
                                {
                                    new BinaryTreeNode<QuizItem>()
                                    {
                                        Data = new QuizItem("Apply for our  long-time internship program!")
                                    },
                                    new BinaryTreeNode<QuizItem>()
                                    {
                                        Data = new QuizItem("Apply forsummer internship program!")
                                    }
                                }
                            }
                        }
                    }
                }
            };
            tree.Count = 9;
            return tree;
        }
        private static void WriteAnswer(string text)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

    }
}
