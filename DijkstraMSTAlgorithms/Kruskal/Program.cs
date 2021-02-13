using System;
using System.Collections.Generic;
using System.Linq;

namespace Kruskal_sAlgorithm
{
    public class Edge
    {
        public Edge(int first, int second, int weight)
        {
            First = first;
            Second = second;
            Weight = weight;
        }
        public int First { get; set; }
        public int Second { get; set; }
        public int Weight { get; set; }
    }
    class Program
    {
        private static List<Edge> graph;
        static void Main(string[] args)
        {
            int e = int.Parse(Console.ReadLine());
            graph = ReadGraph(e);
            var sortedEdges = graph.OrderBy(e => e.Weight).ToList();
            var nodes = graph.Select(a => a.First).Union(graph.Select(b => b.Second)).ToHashSet();

            var parents = Enumerable.Repeat(-1, nodes.Max() + 1).ToArray();

            foreach (var node in nodes)
            {
                parents[node] = node;
            }

            foreach (var edge in sortedEdges)
            {
                var firstNodeRoot = GetRoot(parents, edge.First);
                var secondNodeRoot = GetRoot(parents, edge.Second);

                if (firstNodeRoot == secondNodeRoot)
                {
                    continue;
                }

                Console.WriteLine($"{edge.First} - {edge.Second}");
                parents[firstNodeRoot] = secondNodeRoot;
            }

        }

        private static int GetRoot(int[] parents, int second)
        {
            while (second != parents[second])
            {
                second = parents[second];
            }

            return second;
        }

        private static List<Edge> ReadGraph(int e)
        {
            var result = new List<Edge>();

            for (int i = 0; i < e; i++)
            {
                int[] tokens = Console.ReadLine()
                    .Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();
                int first = tokens[0];
                int second = tokens[1];
                int weight = tokens[2];
                var edge = new Edge(first, second, weight);

                if (!result.Contains(edge))
                {
                    result.Add(edge);
                }

            }

            return result;
        }
    }
}
