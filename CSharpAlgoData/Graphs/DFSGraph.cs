using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpAlgoData.Graphs
{
    class DFSGraph
    {
        public List<Node<T>> DFS()
        {
            bool[] isVisited = new bool[Nodes.Count];
            List<Node<T>> result = new List<Node<T>>();
            DFS(isVisited, Nodes[0], result);
            return result;
        }
        private void DFS(bool[] isVisited, Node<T> node,List<Node<T>> result)
        {
            result.Add(node);
            isVisited[node.Index] = true;

            foreach (Node<T> neighbor in node.Neighbors)
            {
                if (!isVisited[neighbor.Index])
                {
                    DFS(isVisited, neighbor, result);
                }
            }
        }
    }
}
