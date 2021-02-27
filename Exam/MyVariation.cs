using System;
using System.Collections.Generic;
using System.Linq;

namespace FindBi_ConnectedComponents
{
    class Program
    {
        private static List<int>[] graph;
        private static int[] depths;
        private static int[] lowpoint;
        private static int[] parent;
        private static bool[] visited;
        private static Stack<int> stack;
        private static List<HashSet<int>> components;
        private static int articulationPoint;

        static void Main(string[] args)
        {
            var nodesCount = int.Parse(Console.ReadLine());
            var wantedComponents = int.Parse(Console.ReadLine());

            graph = ReadGraph(nodesCount);
            depths = new int[nodesCount + 1];
            lowpoint = new int[nodesCount + 1];
            visited = new bool[nodesCount + 1];

            articulationPoint = 0;
            int desiredEdge = 0;

            components = new List<HashSet<int>>();
            parent = new int[nodesCount + 1];
            Array.Fill(parent, -1);
            stack = new Stack<int>();

            for (int node = 1; node < graph.Length; node++)
            {
                if (!visited[node])
                {
                    FindArticulationPoints(node, 1);
                    var component = new HashSet<int>();

                    while (stack.Count > 1)
                    {
                        var stackChild = stack.Pop();
                        var stackNode = stack.Pop();

                        component.Add(stackNode);
                        component.Add(stackChild);
                    }


                    components.Add(component);

                    if (components.Count == wantedComponents)
                    {
                        var first = components[0];
                        var second = components[1];

                        foreach (var number in first)
                        {
                            if (second.Contains(number))
                            {
                                articulationPoint = number;
                            }
                        }

                        desiredEdge = node;
                        break;
                    }
                }
            }

            Console.WriteLine(articulationPoint);
        }

        private static void FindArticulationPoints(int node, int depth)
        {
            visited[node] = true;
            depths[node] = depth;
            lowpoint[node] = depth;

            var childCount = 0;

            foreach (var child in graph[node])
            {
                if (!visited[child])
                {
                    stack.Push(node);
                    stack.Push(child);
                    parent[child] = node;
                    childCount += 1;

                    FindArticulationPoints(child, depth + 1);

                    if ((parent[node] == -1 && childCount > 1) ||
                        (parent[node] != -1 && lowpoint[child] >= depth))
                    {
                        var component = new HashSet<int>();

                        while (stack.Count > 1)
                        {
                            var stackChild = stack.Pop();
                            var stackNode = stack.Pop();

                            component.Add(stackNode);
                            component.Add(stackChild);

                            if (stackNode == node && stackChild == child)
                            {
                                break;
                            }
                        }

                        components.Add(component);
                    }

                    lowpoint[node] = Math.Min(lowpoint[node], lowpoint[child]);

                }
                else if (parent[node] != child && depths[child] < lowpoint[node])
                {
                    lowpoint[node] = depths[child];
                    stack.Push(node);
                    stack.Push(child);
                }
            }
        }

        private static List<int>[] ReadGraph(int nodesCount)
        {
            var result = new List<int>[nodesCount + 1];

            for (int node = 0; node < result.Length; node++)
            {
                result[node] = new List<int>();
            }

            for (int i = 1; i < nodesCount + 1; i++)
            {
                var children = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList();

               // var first = edge[0];
                //var second = edge[1];

                result[i] = children;

                foreach (var child in children)
                {
                    if (!result[child].Contains(i))
                    {
                        result[child].Add(i);
                    }

                }
            }

            return result;
        }

    }

}
