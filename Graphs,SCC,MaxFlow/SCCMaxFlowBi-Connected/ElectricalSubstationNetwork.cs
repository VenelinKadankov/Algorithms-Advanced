using System;
using System.Collections.Generic;
using System.Linq;

namespace ElectricalSubstationNetwork
{
    class ElectricalSubstationNetwork
    {
        private static List<int>[] graph;
        private static List<int>[] reversedGraph;
        private static Stack<int> sorted;
        static void Main(string[] args)
        {
            var nodesCount = int.Parse(Console.ReadLine());
            var linesCount = int.Parse(Console.ReadLine());

            (graph, reversedGraph) = ReadGraph(nodesCount, linesCount);

            sorted = TopologicalSorting();

            var visited = new bool[nodesCount];

            while (sorted.Count > 0)
            {
                var node = sorted.Pop();

                if (visited[node])
                {
                    continue;
                }

                var component = new Stack<int>();

                DFS(reversedGraph, node, visited, component);

                Console.WriteLine(string.Join(", ", component));
            }
        }

        private static void DFS(List<int>[] reversedGraph, int node, bool[] visited, Stack<int> component)
        {
            if (visited[node])
            {
                return;
            }

            visited[node] = true;

            foreach (var child in reversedGraph[node])
            {
                DFS(reversedGraph, child, visited, component);
            }

            component.Push(node);
        }

        private static Stack<int> TopologicalSorting()
        {
            var stack = new Stack<int>();
            var visited = new bool[graph.Length];

            for (int node = 0; node < graph.Length; node++)
            {
                if (!visited[node])
                {
                    DFS(graph, node, visited, stack);
                }
            }

            return stack;
        }

        private static (List<int>[] graph, List<int>[] reversedGraph) ReadGraph(int nodesCount, int linesCount)
        {
            var result = new List<int>[nodesCount];
            var reversed = new List<int>[nodesCount];

            for (int node = 0; node < nodesCount; node++)
            {
                result[node] = new List<int>();
                reversed[node] = new List<int>();
            }

            for (int i = 0; i < linesCount; i++)
            {
                var data = Console.ReadLine()
                    .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();
                var node = data[0];

                for (int j = 1; j < data.Length; j++)
                {
                    var child = data[j];
                    result[node].Add(child);
                    reversed[child].Add(node);
                }
            }

            return (result, reversed);
        }
    }
}
