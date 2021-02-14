using System;
using System.Collections.Generic;
using System.Linq;

namespace LongestPathInGraph
{
    public class Edge
    {
        public int From { get; set; }
        public int To { get; set; }
        public int Weight { get; set; }

        public override string ToString()
        {
            return $"{From} {To} {Weight}";
        }
    }
    class LongestPathInGraph
    {
        private static List<Edge>[] graph;
        static void Main(string[] args)
        {
            var nodes = int.Parse(Console.ReadLine());
            var edgesCount = int.Parse(Console.ReadLine());

            graph = new List<Edge>[nodes + 1];

            for (int i = 0; i < graph.Length; i++)
            {
                graph[i] = new List<Edge>();
            }

            //i = 1
            for (int i = 0; i < edgesCount; i++)
            {
                var edgeData = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var from = edgeData[0];
                var to = edgeData[1];
                var weight = edgeData[2];

                //tuk mai e ot i
                graph[from].Add(new Edge
                {
                    From = from,
                    To = to,
                    Weight = weight
                });
            }

            var source = int.Parse(Console.ReadLine());
            var destination = int.Parse(Console.ReadLine());

            var sortedNodes = TopologicalSorting();
            var distances = new double[graph.Length];
            Array.Fill(distances, double.NegativeInfinity);
            distances[source] = 0;

            while (sortedNodes.Count > 0)
            {
                var node = sortedNodes.Pop();

                foreach (var item in graph[node])
                {
                    var newDistance = distances[node] + item.Weight;

                    if (newDistance > distances[item.To])
                    {
                        distances[item.To] = newDistance;
                    }
                }
            }
            Console.WriteLine(distances[destination]);
        }

        private static Stack<int> TopologicalSorting()
        {
            var visited = new bool[graph.Length];
            var sorted = new Stack<int>();

            for (int node = 1; node < graph.Length; node++)
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
    }
}
