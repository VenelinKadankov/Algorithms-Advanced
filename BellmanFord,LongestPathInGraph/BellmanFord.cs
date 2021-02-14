using System;
using System.Collections.Generic;
using System.Linq;

namespace BellmanFord
{
    public class Edge
    {
        public int From { get; set; }
        public int To { get; set; }
        public int Weight { get; set; }
    }

    class BellmanFord
    {
        private static List<Edge> graph;

        static void Main(string[] args)
        {
            var nodes = int.Parse(Console.ReadLine());
            var edgesCount = int.Parse(Console.ReadLine());

            graph = new List<Edge>();

            for (int i = 0; i < edgesCount; i++)
            {
                var info = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var from = info[0];
                var to = info[1];
                var weight = info[2];

                graph.Add(new Edge
                {
                    From = from,
                    To = to,
                    Weight = weight
                });
            }
            var source = int.Parse(Console.ReadLine());
            var destination = int.Parse(Console.ReadLine());
            var distances = new double[nodes + 1];
            Array.Fill(distances, double.PositiveInfinity);
            distances[source] = 0;
            var previous = new int[nodes + 1];
            Array.Fill(previous, -1);

            for (int i = 0; i < nodes; i++)
            {
                var updated = false;

                foreach (var edge in graph)
                {
                    if (double.IsPositiveInfinity(edge.From))
                    {
                        continue;
                    }

                    var newDistance = distances[edge.From] + edge.Weight;

                    if (newDistance < distances[edge.To])
                    {
                        distances[edge.To] = newDistance;
                        previous[edge.To] = edge.From;
                        updated = true;
                    }
                }

                if (!updated)
                {
                    break;
                }

            }

            foreach (var edge in graph)
            {
                if (double.IsPositiveInfinity(edge.From))
                {
                    continue;
                }

                var newDistance = distances[edge.From] + edge.Weight;

                if (newDistance < distances[edge.To])
                {
                    Console.WriteLine($"Negative Cycle Detected");
                    return;
                }
            }
            ;
            var path = ReconstructPath(previous, destination);
            Console.WriteLine(string.Join(" ", path));
            Console.WriteLine(distances[destination]);
        }
        
        private static Stack<int> ReconstructPath(int[] previous, int node)
        {
            var path = new Stack<int>();

            while (node != -1)
            {
                path.Push(node);
                node = previous[node];
            }

            return path;
        }
    }
}
