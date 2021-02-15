using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace CableNetwork
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
    class CableNetwork
    {
        private static List<Edge>[] graph;
        private static HashSet<int> spanningTree;
        static void Main(string[] args)
        {
            int budget = int.Parse(Console.ReadLine());
            int nodesCount = int.Parse(Console.ReadLine());
            int edgesCount = int.Parse(Console.ReadLine());
            ;
            spanningTree = new HashSet<int>();
            graph = ReadGraph(nodesCount, edgesCount);

            int usedBudget = Prim(budget);

            Console.WriteLine($"Budget used: {usedBudget}");

        }

        private static int Prim(int budget)
        {
            int usedBudget = 0;
            OrderedBag<Edge> queue = new OrderedBag<Edge>(
                Comparer<Edge>.Create((f, s) => f.Weight - s.Weight));

            foreach (var node in spanningTree)
            {
                queue.AddMany(graph[node]);
            }

            queue = new OrderedBag<Edge>(
                queue,
                Comparer<Edge>.Create((f, s) => f.Weight - s.Weight));

            while (queue.Count > 0)
            {
                Edge edge = queue.RemoveFirst();
                int nonTreeNode = GetNonTreeNode(edge);

                if (nonTreeNode == -1)
                {
                    continue;
                }

                if (edge.Weight > budget)
                {
                    break;
                }

                usedBudget += edge.Weight;
                budget -= edge.Weight;
                spanningTree.Add(nonTreeNode);
                queue.AddMany(graph[nonTreeNode]);
            }


            return usedBudget;
        }

        private static int GetNonTreeNode(Edge edge)
        {
            int nonTreeNode = -1;

            if (spanningTree.Contains(edge.From) &&
                !spanningTree.Contains(edge.To))
            {
                nonTreeNode = edge.To;
            }
            else if (spanningTree.Contains(edge.To) &&
                !spanningTree.Contains(edge.From))
            {
                nonTreeNode = edge.From;
            }

            return nonTreeNode;
        }

        private static List<Edge>[] ReadGraph(int nodesCount, int edgesCount)
        {
            var result = new List<Edge>[nodesCount];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new List<Edge>();
            }

            for (int i = 0; i < edgesCount; i++)
            {
                var edgeData = Console.ReadLine().Split(new char[] { ' '}, StringSplitOptions.RemoveEmptyEntries);

                var first = int.Parse(edgeData[0]);
                var second = int.Parse(edgeData[1]);
                var weight = int.Parse(edgeData[2]);

                if (edgeData.Length == 4)
                {
                    spanningTree.Add(first);
                    spanningTree.Add(second);
                }
                Edge edge = new Edge
                {
                    From = first,
                    To = second,
                    Weight = weight
                };

                result[first].Add(edge);
                result[second].Add(edge);

            }


            return result;
        }
    }
}
