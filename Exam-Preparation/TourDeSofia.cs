using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace TourDeSofia
{
    public class Edge
    {
        public int From { get; set; }
        public int To { get; set; }
        public int Distance { get; set; }
    }

    class Program
    {
        public static List<Edge>[] graph;

        static void Main(string[] args)
        {
            var nodes = int.Parse(Console.ReadLine());
            var edges = int.Parse(Console.ReadLine());
            var startNode = int.Parse(Console.ReadLine());

            graph = ReadGraph(nodes, edges);

            var distance = new double[nodes];

            for (int node = 0; node < distance.Length; node++)
            {
                distance[node] = double.PositiveInfinity;
            }

            var queue = new OrderedBag<int>(
                Comparer<int>.Create((f, s) => distance[f].CompareTo(distance[s])));

            foreach (var edge in graph[startNode])
            {
                queue.Add(edge.To);
                distance[edge.To] = edge.Distance;
            }

            var visitedNodes = new HashSet<int> { startNode };

            while (queue.Count > 0)
            {
                var node = queue.RemoveFirst();
                visitedNodes.Add(node);

                if (node == startNode)
                {
                    break;
                }

                foreach (var edge in graph[node])
                {
                    var child = edge.To;

                    if (double.IsPositiveInfinity(distance[child]))
                    {
                        queue.Add(child);
                    }

                    var newDistance = distance[node] + edge.Distance;

                    if (newDistance < distance[child])
                    {
                        distance[child] = newDistance;
                    }

                    queue = new OrderedBag<int>(
                        queue,
                        Comparer<int>.Create((f, s) => distance[f].CompareTo(distance[s])));
                }
            }

            if (double.IsPositiveInfinity(distance[startNode]))
            {
                Console.WriteLine(visitedNodes.Count);
            }
            else
            {
                Console.WriteLine(distance[startNode]);
            }
        }

        private static List<Edge>[] ReadGraph(int nodes, int edges)
        {
            var result = new List<Edge>[nodes];

            for (int node = 0; node < result.Length; node++)
            {
                result[node] = new List<Edge>();
            }

            for (int i = 0; i < edges; i++)
            {
                var edgeInfo = Console.ReadLine()
                    .Split(new[] { ' '}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var from = edgeInfo[0];
                var to = edgeInfo[1];
                var distance = edgeInfo[2];

                result[from].Add(new Edge
                {
                    From = from,
                    To = to,
                    Distance = distance
                });
            }

            return result;
        }
    }
}
