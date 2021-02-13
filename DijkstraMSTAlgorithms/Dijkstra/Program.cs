using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace Dijkstra_sAlgorithm
{
    public class Edge
    {
        public Edge(int first, int second, int third)
        {
            First = first;
            Second = second;
            Weight = third;
        }
        public int First { get; set; }
        public int Second { get; set; }
        public int Weight { get; set; }
    }
    class Program
    {
        public static Dictionary<int, List<Edge>> edgesByNode;
        static void Main(string[] args)
        {
            int e = int.Parse(Console.ReadLine());
            edgesByNode = ReadGraph(e);

            int start = int.Parse(Console.ReadLine());
            int end = int.Parse(Console.ReadLine());

            int maxNode = edgesByNode.Keys.Max();
            int[] distances = new int[maxNode + 1];

            for (int i = 0; i < distances.Length; i++)
            {
                distances[i] = int.MaxValue;
            }

            distances[start] = 0;
            int[] previous = new int[maxNode + 1];
            previous[start] = -1;
            var queue = new OrderedBag<int>(Comparer<int>.Create((f, s) => distances[f] - distances[s]));
            queue.Add(start);

            while (queue.Count > 0)
            {
                int minNode = queue.RemoveFirst();
                var children = edgesByNode[minNode];

                if (minNode == end)
                {
                    break;
                }

                foreach (var child in children)
                {
                    var childNode = child.First == minNode ? child.Second : child.First;

                    if (distances[childNode] == int.MaxValue)
                    {
                        queue.Add(childNode);
                    }

                    var newDistance = child.Weight + distances[minNode];

                    if (newDistance < distances[childNode])
                    {
                        distances[childNode] = newDistance;
                        previous[childNode] = minNode;
                        queue = new OrderedBag<int>(queue,
                            Comparer<int>.Create((f, s) => distances[f] - distances[s]));
                    }
                }


            }

            if (distances[end] == int.MaxValue)
            {
                Console.WriteLine("There is no such path.");
            }
            else
            {
                var path = new Stack<int>();
                var node = end;

                while (node != -1)
                {
                    path.Push(node);
                    node = previous[node];
                }
                Console.WriteLine(distances[end]);
                Console.WriteLine(string.Join(" ", path));


            }
        }

        private static Dictionary<int, List<Edge>> ReadGraph(int e)
        {
            var graph = new Dictionary<int, List<Edge>>();

            for (int i = 0; i < e; i++)
            {
                int[] tokens = Console.ReadLine()
                    .Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                var first = tokens[0];
                var second = tokens[1];
                var weight = tokens[2];

                if (!graph.ContainsKey(first))
                {
                    graph.Add(first, new List<Edge>());
                }

                if (!graph.ContainsKey(second))
                {
                    graph.Add(second, new List<Edge>());
                }


                var edge = new Edge(first, second, weight);

                graph[first].Add(edge);
                graph[second].Add(edge);
            }
            return graph;
        }
    }
}
