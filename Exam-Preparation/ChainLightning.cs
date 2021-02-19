﻿using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace ChainLightning
{
    public class Edge
    {
        public int First { get; set; }
        public int Second { get; set; }
        public int Distance { get; set; }
    }

    class Program
    {

        private static List<Edge>[] graph;
        private static Dictionary<int, Dictionary<int, int>> treeByNode;
        private static Dictionary<int, int> damageByNode;
        static void Main(string[] args)
        {
            var nodes = int.Parse(Console.ReadLine());
            var edges = int.Parse(Console.ReadLine());
            var lightning = int.Parse(Console.ReadLine());

            graph = ReadGraph(nodes, edges);
            treeByNode = new Dictionary<int, Dictionary<int, int>>();
            damageByNode = new Dictionary<int, int>();

            for (int i = 0; i < lightning; i++)
            {
                var lightningData = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                var node = lightningData[0];
                var damage = lightningData[1];

                if (!treeByNode.ContainsKey(node))
                {
                    treeByNode[node] = Prim(node);
                }

                var tree = treeByNode[node];

                foreach (var kvp in tree)
                {
                    var currentDamage = CalculateDamage(damage, kvp.Value);

                    if (!damageByNode.ContainsKey(kvp.Key))
                    {
                        damageByNode.Add(kvp.Key, 0);
                    }

                    damageByNode[kvp.Key] += currentDamage;
                }

            }

            var maxDamage = damageByNode.Max(KeyValuePair => KeyValuePair.Value);
            Console.WriteLine(maxDamage);
        }

        private static int CalculateDamage(int damage, int depth)
        {
            for (int i = 0; i < depth; i++)
            {
                damage /= 2;
            }

            return damage;
        }

        private static Dictionary<int, int> Prim(int startNode)
        {
            var tree = new Dictionary<int, int>
            {
                {
                    startNode,
                    0
                }
            };

            var queue = new OrderedBag<Edge>(
                Comparer<Edge>.Create((f, s) => f.Distance - s.Distance));

            queue.AddMany(graph[startNode]);

            while (queue.Count > 0)
            {
                var edge = queue.RemoveFirst();
                var nonTreeNode = GetNonTreeNode(tree, edge);

                if (nonTreeNode == -1)
                {
                    continue;
                }

                var treeNode = GetTreeNode(tree, edge);
                tree.Add(nonTreeNode, tree[treeNode] + 1);
                queue.AddMany(graph[nonTreeNode]);
            }

            return tree;
        }

        private static int GetTreeNode(Dictionary<int, int> tree, Edge edge)
        {
            if (tree.ContainsKey(edge.First))
            {
                return edge.First;
            }

            return edge.Second;
        }

        private static int GetNonTreeNode(Dictionary<int, int> tree, Edge edge)
        {
            if (tree.ContainsKey(edge.First) &&
                !tree.ContainsKey(edge.Second))
            {
                return edge.Second;
            }

            if (tree.ContainsKey(edge.Second) &&
                !tree.ContainsKey(edge.First))
            {
                return edge.First;
            }

            return -1;
        }

        private static List<Edge>[] ReadGraph(int nodes, int edges)
        {
            var result = new List<Edge>[nodes];

            for (int node = 0; node < result.Length; node++)
            {
                result[node] = new List<Edge>();
            }

            for (int i = 0; i < edges; i++)
            {
                var edgeInfo = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                var first = edgeInfo[0];
                var second = edgeInfo[1];
                var distance = edgeInfo[2];

                var edge = new Edge
                {
                    First = first,
                    Second = second,
                    Distance = distance
                };

                result[first].Add(edge);
                result[second].Add(edge);
            }

            return result;
        }
    }
}
