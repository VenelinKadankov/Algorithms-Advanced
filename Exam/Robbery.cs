using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace Exam
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
    class StartUp
    {
        private static Dictionary<int, List<Edge>> graph;
        static void Main(string[] args)
        {
            int nodes = int.Parse(Console.ReadLine());
            int edges = int.Parse(Console.ReadLine());

            graph = ReadGraph(nodes, edges);

            ResizeGraph();

            int start = int.Parse(Console.ReadLine());
            int end = int.Parse(Console.ReadLine());

            int maxNode = graph.Keys.Max();
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
                var children = graph[minNode];

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

            Console.WriteLine(distances[end]);

                ;
        }

        private static void ResizeGraph()
        {
            string[] whiteBlack = Console.ReadLine().Split();

            for (int i = 0; i < whiteBlack.Length; i++)
            {
                string current = whiteBlack[i];

                if (current[1] == 'w')
                {
                    int cameraNum = int.Parse(current[0].ToString());


                    graph.Remove(cameraNum);

                    foreach (var node in graph)
                    {
                        while (node.Value.Any(a => a.Second == cameraNum))
                        {
                            node.Value.Remove(node.Value.FirstOrDefault(a => a.Second == cameraNum));
                        }
                    }
                }
            }
        }

        private static Dictionary<int, List<Edge>> ReadGraph(int nodes, int edges)
        {
            var result = new Dictionary<int, List<Edge>>(nodes);

            for (int node = 0; node < nodes; node++)
            {
                result.Add(node, new List<Edge>());
            }

            for (int edge = 0; edge < edges; edge++)
            {
                int[] tokens = Console.ReadLine()
                    .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                int first = tokens[0];
                int second = tokens[1];
                int weight = tokens[2];

                //if (!result.ContainsKey(first))
                //{
                //    result.Add(first, new List<Edge>());
                //}

                //if (!result.ContainsKey(second))
                //{
                //    result.Add(second, new List<Edge>());
                //}


                var currentEdge = new Edge(first, second, weight);

                result[first].Add(currentEdge);
                result[second].Add(new Edge(second, first, weight));

            }

            return result;
        }
    }
}
