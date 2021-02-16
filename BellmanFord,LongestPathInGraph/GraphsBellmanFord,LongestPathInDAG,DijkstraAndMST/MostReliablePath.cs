using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace MostReliablePath
{
    class Edge
    {
        public int First { get; set; }
        public int Second { get; set; }
        public int Weight { get; set; }

        public override string ToString()
        {
            return $"{this.First} {this.Second} {this.Weight}";
        }
    }
    class MostReliablePath
    {
        private static List<Edge>[] graph;
        static void Main(string[] args)
        {
            int nodesCount = int.Parse(Console.ReadLine());
            int edgesCount = int.Parse(Console.ReadLine());

            graph = ReadGraph(nodesCount, edgesCount);
            ;

            int source = int.Parse(Console.ReadLine());
            int destination = int.Parse(Console.ReadLine());

            double[] distances = new double[nodesCount];
            int[] previous = new int[nodesCount];

            for (int i = 0; i < nodesCount; i++)
            {
                distances[i] = double.NegativeInfinity;
                previous[i] = -1;

            }

            distances[source] = 100;

            OrderedBag<int> queue = new OrderedBag<int>(
                Comparer<int>.Create((f, s) => distances[s].CompareTo(distances[f])));

            queue.Add(source);

            while (queue.Count > 0)
            {
                int node = queue.RemoveFirst();

                if (node == destination)
                {
                    break;
                }

                List<Edge> children = graph[node];

                foreach (var edge in children)
                {
                    int child = edge.First == node ? edge.Second : edge.First;

                    if (double.IsNegativeInfinity(distances[child]))
                    {
                        queue.Add(child);
                    }

                    double newDistance = distances[node] * edge.Weight / 100.0;

                    if (newDistance > distances[child])
                    {
                        distances[child] = newDistance;
                        previous[child] = node;

                        queue = new OrderedBag<int>(
                            queue,
                            Comparer<int>.Create((f, s) => distances[s].CompareTo(distances[f])));
                    }
                }
            }

            Console.WriteLine($"Most reliable path reliability: {distances[destination]:F2}%");
            Stack<int> path = GetPath(previous, destination);
            Console.WriteLine(string.Join(" -> ", path));
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
            var result = new List<Edge>[nodesCount];

            for (int i = 0; i < nodesCount; i++)
            {
                result[i] = new List<Edge>();
            }

            for (int i = 0; i < edgesCount; i++)
            {
                var edgeData = Console.ReadLine()
                    .Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var first = edgeData[0];
                var second = edgeData[1];
                var weight = edgeData[2];

                var edge = new Edge
                {
                    First = first,
                    Second = second,
                    Weight = weight
                };

                result[first].Add(edge);
                result[second].Add(edge);
            }


            return result;
        }
    }
}
