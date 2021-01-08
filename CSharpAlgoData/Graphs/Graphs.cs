using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;
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
        //
        //Graph<int> graph = new Graph<int>(true, true);
        //Node<int> n1 = graph.AddNode(1);
        //Node<int> n8 = graph.AddNode(8);
        //graph.AddEdge(n1, n2, 9);
        //graph.AddEdge(n8, n5, 3); 
        //List<Node<int>> dfsNodes = graph.DFS();
        //dfsNodes.ForEach(n => Console.WriteLine(n)); 
        //
        //Here, you initialize a directed and weighted graph.
        //To start traversing the graph, you just need to call the DFS method,
        //which returns a list of Node instances.Then, you can easily iterate through elements of the list to print some basic information about each node.
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
            edges.Sort((a, b) => a.Weight.CompareTo(b.Weight));// begin with the lowest weighted edge
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
        //Graph<int> graph = new Graph<int>(false, true);
        //Node<int> n1 = graph.AddNode(1);
        //Node<int> n8 = graph.AddNode(8);
        //graph.AddEdge(n1, n2, 3);
        //graph.AddEdge(n7, n8, 20); 
        //List<Edge<int>> mstKruskal = graph.MinimumSpanningTreeKruskal();
        //mstKruskal.ForEach(e => Console.WriteLine(e)); 
        ////
        //First, you initialize an undirected and weighted graph, as well as add nodes and edges.
        //Then, you call the MinimumSpanningTreeKruskal method to find the MST using Kruskal's algorithm.
        //At the end, you use the ForEach method to write the data of each edge from the MST in the console.
        #endregion
        #region PRIM'S ALGORITHM
        public List<Edge<T>> MinimumSpanningTreePrim()
        {
            int[] previous = new int[Nodes.Count];
            previous[0] = -1;

            int[] minWeight = new int[Nodes.Count];
            Fill(minWeight, int.MaxValue);
            minWeight[0] = 0;

            bool[] isInMST = new bool[Nodes.Count];
            Fill(isInMST, false);

            for (int i = 0; i < Nodes.Count - 1; i++)
            {
                int minWeightIndex = GetMinimumWeightIndex(
                    minWeight, isInMST);
                isInMST[minWeightIndex] = true;

                for (int j = 0; j < Nodes.Count; j++)
                {
                    Edge<T> edge = this[minWeightIndex, j];
                    int weight = edge != null ? edge.Weight : -1;
                    if (edge != null
                        && !isInMST[j]
                        && weight < minWeight[j])
                    {
                        previous[j] = minWeightIndex;
                        minWeight[j] = weight;
                    }
                }
            }

            List<Edge<T>> result = new List<Edge<T>>();
            for (int i = 1; i < Nodes.Count; i++)
            {
                Edge<T> edge = this[previous[i], i];
                result.Add(edge);
            }
            return result;
        }
        private int GetMinimumWeightIndex(int[] weights, bool[] isInMST)
        {
            int minValue = int.MaxValue;
            int minIndex = 0;

            for (int i = 0; i < Nodes.Count; i++)
            {
                if (!isInMST[i] && weights[i] < minValue)
                {
                    minValue = weights[i];
                    minIndex = i;
                }
            }

            return minIndex;
        }
        private void Fill<Q>(Q[] array, Q value)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = value;
            }
        }
        //
        //Graph<int> graph = new Graph<int>(false, true);
        //Node<int> n1 = graph.AddNode(1); (...) 
        //Node<int> n8 = graph.AddNode(8);
        //graph.AddEdge(n1, n2, 3); (...) 
        //graph.AddEdge(n7, n8, 20); 
        //List<Edge<int>> mstPrim = graph.MinimumSpanningTreePrim();
        //mstPrim.ForEach(e => Console.WriteLine(e)); 
        //
        //First, you initialize an undirected and weighted graph, as well as add nodes and edges.
        //Then, you call the MinimumSpanningTreePrim method to find the MST using Prim's algorithm.
        //At the end, you use the ForEach method to write the data of each edge from the MST in the console.
        #endregion
        #region COLORING
        public int[] Color()
        {
            int[] colors = new int[Nodes.Count];
            FillC(colors, -1);
            colors[0] = 0;

            bool[] availability = new bool[Nodes.Count];
            for (int i = 1; i < Nodes.Count; i++)
            {
                FillC(availability, true);

                int colorIndex = 0;
                foreach (Node<T> neighbor in Nodes[i].Neighbors)
                {
                    colorIndex = colors[neighbor.Index];
                    if (colorIndex >= 0)
                    {
                        availability[colorIndex] = false;
                    }
                }

                colorIndex = 0;
                for (int j = 0; j < availability.Length; j++)
                {
                    if (availability[j])
                    {
                        colorIndex = j;
                        break;
                    }
                }

                colors[i] = colorIndex;
            }

            return colors;
        }
        private void FillC<Q>(Q[] array, Q value)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = value;
            }
        }
        //Graph<int> graph = new Graph<int>(false, false);
        //Node<int> n1 = graph.AddNode(1);
        //Node<int> n8 = graph.AddNode(8);
        //graph.AddEdge(n1, n2);
        //graph.AddEdge(n7, n8); 
        //int[] colors = graph.Color(); 
        //for (int i = 0; i<colors.Length; i++) 
        //{ 
        //    Console.WriteLine($"Node {graph.Nodes[i].Data}: {colors[i]}"); 
        //}
        //
        //Here, you create a new undirected and unweighted graph, add nodes and edges, and call the Color method to perform the node coloring.
        //As a result, you receive an array with indices of colors for particular nodes. Then, you present the results in the console
        #endregion
        #region DIJKSTRA'S ALGORITHM
        public List<Edge<T>> GetShortestPathDijkstra(
    Node<T> source, Node<T> target)
        {
            int[] previous = new int[Nodes.Count];
            Fill(previous, -1);

            int[] distances = new int[Nodes.Count];
            Fill(distances, int.MaxValue);
            distances[source.Index] = 0;

            SimplePriorityQueue<Node<T>> nodes = new SimplePriorityQueue<Node<T>>();
            for (int i = 0; i < Nodes.Count; i++)
            {
                nodes.Enqueue(Nodes[i], distances[i]);
            }

            while (nodes.Count != 0)
            {
                Node<T> node = nodes.Dequeue();
                for (int i = 0; i < node.Neighbors.Count; i++)
                {
                    Node<T> neighbor = node.Neighbors[i];
                    int weight = i < node.Weights.Count
                        ? node.Weights[i] : 0;
                    int weightTotal = distances[node.Index] + weight;

                    if (distances[neighbor.Index] > weightTotal)
                    {
                        distances[neighbor.Index] = weightTotal;
                        previous[neighbor.Index] = node.Index;
                        nodes.UpdatePriority(neighbor,
                            distances[neighbor.Index]);
                    }
                }
            }

            List<int> indices = new List<int>();
            int index = target.Index;
            while (index >= 0)
            {
                indices.Add(index);
                index = previous[index];
            }

            indices.Reverse();
            List<Edge<T>> result = new List<Edge<T>>();
            for (int i = 0; i < indices.Count - 1; i++)
            {
                Edge<T> edge = this[indices[i], indices[i + 1]];
                result.Add(edge);
            }
            return result;
        }
        //Graph<int> graph = new Graph<int>(true, true);
        //Node<int> n1 = graph.AddNode(1); (...) 
        //Node<int> n8 = graph.AddNode(8);
        //graph.AddEdge(n1, n2, 9); (...) 
        //graph.AddEdge(n8, n5, 3); 
        //List<Edge<int>> path = graph.GetShortestPathDijkstra(n1, n5);
        //path.ForEach(e => Console.WriteLine(e)); 
        //
        //Here, you create a new directed and weighted graph, add nodes and edges, and call the GetShortestPathDijkstra method to search the shortest path between two nodes,
        //namely between the nodes 1 and 5. As a result, you receive a list of edges forming the shortest path.Then, you just iterate through all edges and present the results in the console:
        #endregion
        #region DIJKSTRA GAME
        public void Game()
        {
            string[] lines = new string[]
            {
                "0011100000111110000011111",
                "0011100000111110000011111",
                "0011100000111110000011111",
                "0000000000011100000011111",
                "0000001110000000000011111",
                "0001001110011100000011111",
                "1111111111111110111111100",
                "1111111111111110111111101",
                "1111111111111110111111100",
                "0000000000000000111111110",
                "0000000000000000111111100",
                "0001111111001100000001101",
                "0001111111001100000001100",
                "0001100000000000111111110",
                "1111100000000000111111100",
                "1111100011001100100010001",
                "1111100011001100001000100"
            };
            bool[][] map = new bool[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                map[i] = lines[i].Select(c => int.Parse(c.ToString()) == 0).ToArray();
            }
            Graph<string> graph = new Graph<string>(false, true);
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    if (map[i][j])
                    {
                        Node<string> from = graph.AddNode($"{i}-{j}");

                        if (i > 0 && map[i - 1][j])
                        {
                            Node<string> to = graph.Nodes.Find(
                                n => n.Data == $"{i - 1}-{j}");
                            graph.AddEdge(from, to, 1);
                        }

                        if (j > 0 && map[i][j - 1])
                        {
                            Node<string> to = graph.Nodes.Find(
                                n => n.Data == $"{i}-{j - 1}");
                            graph.AddEdge(from, to, 1);
                        }
                    }
                }
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
