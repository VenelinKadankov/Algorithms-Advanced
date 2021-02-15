using System;
using System.Collections.Generic;
using System.Linq;

namespace CheapTownTour
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
    class CheapTownTour
    {
        private static List<Edge> edges;
        static void Main(string[] args)
        {
            int nodesCount = int.Parse(Console.ReadLine());
            int edgesCount = int.Parse(Console.ReadLine());

            edges = ReadEdges(edgesCount);

            List<Edge> sortedEdges = edges.OrderBy(e => e.Weight).ToList();
            int[] root = new int[nodesCount];

            for (int node = 0; node < nodesCount; node++)
            {
                root[node] = node;
            }

            int totalCost = 0;

            foreach (var edge in sortedEdges)
            {
                int firstRoot = GetRoot(edge.First, root);
                int secondRoot = GetRoot(edge.Second, root);

                if (firstRoot != secondRoot)
                {
                    root[firstRoot] = secondRoot;
                    totalCost += edge.Weight;
                }
            }

            Console.WriteLine($"Total cost: {totalCost}");
        }

        private static int GetRoot(int first, int[] root)
        {
            while (first != root[first])
            {
                first = root[first];
            }

            return first;
        }

        private static List<Edge> ReadEdges(int edgesCount)
        {
            List<Edge> result = new List<Edge>();

            for (int i = 0; i < edgesCount; i++)
            {
                int[] edgeData = Console.ReadLine()
                    .Split(" - ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                result.Add(new Edge
                {
                    First = edgeData[0],
                    Second = edgeData[1],
                    Weight = edgeData[2]
                });

            }

            return result;
        }
    }
}
