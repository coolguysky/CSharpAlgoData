using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpAlgoData.Graphs
{
    //very similar to 620 project just done differently

    #region NODE AND EDGE
    public class Node<T>
    {
        public int Index { get; set; }
        public T Data { get; set; }
        public List<Node<T>> Neighbors { get; set; } = new List<Node<T>>();
        public List<int> Weights { get; set; } = new List<int>();

        public override string ToString()
        {
            return $"Node with index {Index}: {Data}, neighbors: { Neighbors.Count}";
        }
    }
    public class Edge<T>
    {
        public Node<T> From { get; set; }
        public Node<T> To { get; set; }
        public int Weight { get; set; }

        public override string ToString()
        {
            return $"Edge: {From.Data} -> {To.Data}, weight: { Weight}";
        }
    }

    #endregion

    public class Graph<T>
    {
        private bool _isDirected = false;
        private bool _isWeighted = false;
        public List<Node<T>> Nodes { get; set; } = new List<Node<T>>();

        public Graph(bool isDirected, bool isWeighted)
        {
            _isDirected = isDirected;
            _isWeighted = isWeighted;
        }

        public Edge<T> this[int from, int to]
        {
            get
            {
                Node<T> nodeFrom = Nodes[from];
                Node<T> nodeTo = Nodes[to];
                int i = nodeFrom.Neighbors.IndexOf(nodeTo);
                if (i >= 0)
                {
                    Edge<T> edge = new Edge<T>()
                    {
                        From = nodeFrom,
                        To = nodeTo,
                        Weight = i < nodeFrom.Weights.Count
                            ? nodeFrom.Weights[i] : 0
                    };
                    return edge;
                }

                return null;
            }
        }
        #region GRAPH METHODS
        public Node<T> AddNode(T value)
        {
            Node<T> node = new Node<T>() { Data = value };
            Nodes.Add(node);
            UpdateIndices();
            return node;
        }
        public void RemoveNode(Node<T> nodeToRemove)
        {
            Nodes.Remove(nodeToRemove);
            UpdateIndices();
            foreach (Node<T> node in Nodes)
            {
                RemoveEdge(node, nodeToRemove);
            }
        }
        public void AddEdge(Node<T> from, Node<T> to, int weight = 0)
        {
            from.Neighbors.Add(to);
            if (_isWeighted)
            {
                from.Weights.Add(weight);
            }

            if (!_isDirected)
            {
                to.Neighbors.Add(from);
                if (_isWeighted)
                {
                    to.Weights.Add(weight);
                }
            }
        }
        public void RemoveEdge(Node<T> from, Node<T> to)
        {
            int index = from.Neighbors.FindIndex(n => n == to);
            if (index >= 0)
            {
                from.Neighbors.RemoveAt(index);
                if (_isWeighted)
                {
                    from.Weights.RemoveAt(index);
                }
            }
        }
        public List<Edge<T>> GetEdges()
        {
            List<Edge<T>> edges = new List<Edge<T>>();
            foreach (Node<T> from in Nodes)
            {
                for (int i = 0; i < from.Neighbors.Count; i++)
                {
                    Edge<T> edge = new Edge<T>()
                    {
                        From = from,
                        To = from.Neighbors[i],
                        Weight = i < from.Weights.Count
                            ? from.Weights[i] : 0
                    };
                    edges.Add(edge);
                }
            }
            return edges;
        }
        private void UpdateIndices()
        {
            int i = 0;
            Nodes.ForEach(n => n.Index = i++);
        }
        #endregion
        #region DEPTH-FIRST SEARCH
        //
        //
        //Depth-First Search
        //
        //
        public List<Node<T>> DFS()
        {
            bool[] isVisited = new bool[Nodes.Count];
            List<Node<T>> result = new List<Node<T>>();
            DFS(isVisited, Nodes[0], result);
            return result;
        }
        private void DFS(bool[] isVisited, Node<T> node, List<Node<T>> result)
        {
            result.Add(node); //modifies the parameter
            isVisited[node.Index] = true;

            foreach (Node<T> neighbor in node.Neighbors)
            {
                if (!isVisited[neighbor.Index])
                {
                    DFS(isVisited, neighbor, result); //recursive function (different than stack)--keeps going down
                }
            }
        }
        #endregion
        #region BREADTH-FIRST SEARCH
        //
        //
        //Breadth-First Search
        //
        //
        public List<Node<T>> BFS()
        {
            return BFS(Nodes[0]);
        }
        private List<Node<T>> BFS(Node<T> node)
        {
            bool[] isVisited = new bool[Nodes.Count];
            isVisited[node.Index] = true;

            List<Node<T>> result = new List<Node<T>>();
            Queue<Node<T>> queue = new Queue<Node<T>>(); //nodes that need to be traversed 
            queue.Enqueue(node);
            while (queue.Count > 0)
            {
                Node<T> next = queue.Dequeue(); //take next up
                result.Add(next);

                foreach (Node<T> neighbor in next.Neighbors)
                {
                    if (!isVisited[neighbor.Index])
                    {
                        isVisited[neighbor.Index] = true;
                        queue.Enqueue(neighbor); //adding each neighbor
                    }
                }
            }

            return result;
        }
        #endregion
        #region KRUSKAL'S ALGORITHM
        //
        //
        // Kruskal's algorithm
        //
        //
        public List<Edge<T>> MinimumSpanningTreeKruskal()
        {
            List<Edge<T>> edges = GetEdges();
            edges.Sort((a, b) => a.Weight.CompareTo(b.Weight));
            Queue<Edge<T>> queue = new Queue<Edge<T>>(edges);

            Subset<T>[] subsets = new Subset<T>[Nodes.Count];
            for (int i = 0; i < Nodes.Count; i++)
            {
                subsets[i] = new Subset<T>() { Parent = Nodes[i] };
            }

            List<Edge<T>> result = new List<Edge<T>>();
            while (result.Count < Nodes.Count - 1)
            {
                Edge<T> edge = queue.Dequeue();
                Node<T> from = GetRoot(subsets, edge.From);
                Node<T> to = GetRoot(subsets, edge.To);
                if (from != to)
                {
                    result.Add(edge);
                    Union(subsets, from, to);
                }
            }

            return result;
        }
        private Node<T> GetRoot(Subset<T>[] subsets, Node<T> node)
        {
            if (subsets[node.Index].Parent != node)
            {
                subsets[node.Index].Parent = GetRoot(
                    subsets,
                    subsets[node.Index].Parent);
            }

            return subsets[node.Index].Parent;
        }
        private void Union(Subset<T>[] subsets, Node<T> a, Node<T> b)
        {
            if (subsets[a.Index].Rank > subsets[b.Index].Rank)
            {
                subsets[b.Index].Parent = a;
            }
            else if (subsets[a.Index].Rank < subsets[b.Index].Rank)
            {
                subsets[a.Index].Parent = b;
            }
            else
            {
                subsets[b.Index].Parent = a;
                subsets[a.Index].Rank++;
            }
        }
        public class Subset<T>
        {
            public Node<T> Parent { get; set; }
            public int Rank { get; set; }

            public override string ToString()
            {
                return $"Subset with rank {Rank}, parent: {Parent.Data} (index: { Parent.Index})"; 
            }
        }
        #endregion

    }
}

#region IMPLEMENTATIONS
//Graph<int> graph = new Graph<int>(false, false);
//Node<int> n1 = graph.AddNode(1);
//Node<int> n2 = graph.AddNode(2);
//Node<int> n3 = graph.AddNode(3);
//Node<int> n4 = graph.AddNode(4);
//Node<int> n5 = graph.AddNode(5);
//Node<int> n6 = graph.AddNode(6);
//Node<int> n7 = graph.AddNode(7);
//Node<int> n8 = graph.AddNode(8);
//graph.AddEdge(n1, n2);
//graph.AddEdge(n1, n3);
//graph.AddEdge(n2, n4);
//graph.AddEdge(n3, n4);
//graph.AddEdge(n4, n5);
//graph.AddEdge(n5, n6);
//graph.AddEdge(n5, n7);
//graph.AddEdge(n5, n8);
//graph.AddEdge(n6, n7);
//graph.AddEdge(n7, n8);
//Graph<int> graph = new Graph<int>(true, true);
//Node<int> n1 = graph.AddNode(1);
//Node<int> n2 = graph.AddNode(2);
//Node<int> n3 = graph.AddNode(3);
//Node<int> n4 = graph.AddNode(4);
//Node<int> n5 = graph.AddNode(5);
//Node<int> n6 = graph.AddNode(6);
//Node<int> n7 = graph.AddNode(7);
//Node<int> n8 = graph.AddNode(8);
//graph.AddEdge(n1, n2, 9);
//graph.AddEdge(n1, n3, 5);
//graph.AddEdge(n2, n1, 3);
//graph.AddEdge(n2, n4, 18);
//graph.AddEdge(n3, n4, 12);
//graph.AddEdge(n4, n2, 2);
//graph.AddEdge(n4, n8, 8);
//graph.AddEdge(n5, n4, 9);
//graph.AddEdge(n5, n6, 2);
//graph.AddEdge(n5, n7, 5);
//graph.AddEdge(n5, n8, 3);
//graph.AddEdge(n6, n7, 1);
//graph.AddEdge(n7, n5, 4);
//graph.AddEdge(n7, n8, 6);
//graph.AddEdge(n8, n5, 3);

#endregion
