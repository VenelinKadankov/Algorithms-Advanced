using System;
using System.Collections.Generic;
using System.Linq;

namespace StronglyConnectedComponents
{
    class StronglyConnectedComponents
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
            Console.WriteLine("Strongly Connected Components:");

            while (sorted.Count > 0)
            {
                var node = sorted.Pop();

                if (visited[node])
                {
                    continue;
                }

                var component = new Stack<int>();

                ReverseDFS(node, visited, component);

                Console.WriteLine($"{{{string.Join(", ", component)}}}");
            }
        }

        private static void ReverseDFS(int node, bool[] visited, Stack<int> component)
        {
            if (visited[node])
            {
                return;
            }

            visited[node] = true;

            foreach (var child in reversedGraph[node])
            {
                ReverseDFS(child, visited, component);
            }

            component.Push(node);
        }

        private static Stack<int> TopologicalSorting()
        {
            var result = new Stack<int>();
            var visited = new bool[graph.Length];

            for (int node = 0; node < graph.Length; node++)
            {
                DFS(node, visited, result);
            }

            return result;
        }

        private static void DFS(int node, bool[] visited, Stack<int> result)
        {
            if (visited[node])
            {
                return;
            }

            visited[node] = true;

            foreach (var child in graph[node])
            {
                DFS(child, visited, result);
            }

            result.Push(node);
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
