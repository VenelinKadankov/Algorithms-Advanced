using System;
using System.Collections.Generic;
using System.Linq;

namespace Undefined
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
    class Undefined
    {
        private static List<Edge> edges;
        static void Main(string[] args)
        {
            int nodesCount = int.Parse(Console.ReadLine());
            int edgesCount = int.Parse(Console.ReadLine());

            edges = ReadEdges(edgesCount);

            int source = int.Parse(Console.ReadLine());
            int destination = int.Parse(Console.ReadLine());

            double[] distances = new double[nodesCount + 1];
            int[] previous = new int[edgesCount + 1];

            for (int i = 0; i < nodesCount + 1; i++)
            {
                distances[i] = double.PositiveInfinity;
                previous[i] = -1;
            }

            distances[source] = 0;

            for (int i = 0; i < nodesCount - 1; i++)
            {
                bool updated = false;

                foreach (var edge in edges)
                {
                    if (double.IsPositiveInfinity(distances[edge.From]))
                    {
                        continue;
                    }

                    double newDistance = distances[edge.From] + edge.Weight;

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

            foreach (var edge in edges)
            {
                double newDistance = distances[edge.From] + edge.Weight;

                if (newDistance < distances[edge.To])
                {
                    Console.WriteLine("Undefined");
                    return;
                }
            }

            Stack<int> path = GetPath(previous, destination);
            Console.WriteLine(string.Join(" ", path));
            Console.WriteLine(distances[destination]);
        }

        private static List<Edge> ReadEdges(int edgesCount)
        {
            List<Edge> result = new List<Edge>();

            for (int i = 0; i < edgesCount; i++)
            {
                int[] edgeData = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                result.Add(new Edge
                {
                    From = edgeData[0],
                    To = edgeData[1],
                    Weight = edgeData[2]
                });

            }

            return result;
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
    }
}
