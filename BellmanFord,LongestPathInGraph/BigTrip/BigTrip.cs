using System;
using System.Collections.Generic;
using System.Linq;

namespace BigTrip
{
    class Edge
    {
        public int From { get; set; }
        public int To { get; set; }
        public int Weight { get; set; }

        public override string ToString()
        {
            return $"{this.From} {this.To} {this.Weight}";
        }
    }
    class BigTrip
    {
        private static List<Edge>[] graph;
        static void Main(string[] args)
        {
            int nodesCount = int.Parse(Console.ReadLine());
            int edgesCount = int.Parse(Console.ReadLine());

            graph = ReadGraph(nodesCount, edgesCount);

            int source = int.Parse(Console.ReadLine());
            int destination = int.Parse(Console.ReadLine());

            double[] distances = new double[graph.Length];
            int[] previous = new int[graph.Length]; // daljinata

            for (int i = 0; i < graph.Length; i++)
            {
                distances[i] = double.NegativeInfinity;
                previous[i] = -1;
            }

            distances[source] = 0;
            Stack<int> sortedNodes = TopologicalSort();

            while (sortedNodes.Count >0)
            {
                int node = sortedNodes.Pop();

                foreach (var edge in graph[node])
                {
                    double newDistance = distances[node] + edge.Weight;

                    if (newDistance > distances[edge.To])
                    {
                        distances[edge.To] = newDistance;
                        previous[edge.To] = node;
                    }
                }
            }

            Console.WriteLine(distances[destination]);
            Console.WriteLine(string.Join(" ", GetPath(previous, destination)));
        }

        private static Stack<int> TopologicalSort()
        {
            bool[] visited = new bool[graph.Length];
            Stack<int> sorted = new Stack<int>();

            for (int node = 0; node < graph.Length; node++)
            {
                DFS(node, visited, sorted);
            }

            return sorted;
        }

        private static void DFS(int node, bool[] visited, Stack<int> sorted)
        {
            if (visited[node])
            {
                return;
            }

            visited[node] = true;

            foreach (var edge in graph[node])
            {
                DFS(edge.To, visited, sorted);
            }

            sorted.Push(node);
        }

        private static Stack<int> GetPath(int[] previous, int destination)
        {
            Stack<int> path = new Stack<int>();

            while (destination != -1)
            {
                path.Push(destination);
                destination = previous[destination];
            }

            return path;
        }
        private static List<Edge>[] ReadGraph(int nodesCount, int edgesCount)
        {
            var result = new List<Edge>[nodesCount + 1];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new List<Edge>();
            }

            for (int i = 0; i < edgesCount; i++)
            {
                var edgeData = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var from = edgeData[0];
                var to = edgeData[1];
                var weight = edgeData[2];

                result[from].Add(new Edge
                {
                    From = from,
                    To = to,
                    Weight = weight
                });

            }


            return result;
        }
    }
}
