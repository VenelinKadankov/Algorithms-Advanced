using System;
using System.Collections.Generic;
using System.Linq;

namespace LongestZigzagSubsequence
{
    class LongestZigzagSubsequence
    {
        static void Main(string[] args)
        {
            var numbers = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var dp = new int[2, numbers.Length];
            dp[0, 0] = 1;
            dp[1, 0] = 1;

            var parent = new int[2, numbers.Length];
            parent[0, 0] = -1;
            parent[1, 0] = -1;

            var bestSize = 0;
            var lastRowIndex = 0;
            var lastColIndex = 0;

            for (int current = 0; current < numbers.Length; current++)
            {
                var currentNumber = numbers[current];

                for (int prev = current - 1; prev >= 0; prev--)
                {
                    var prevNumber = numbers[prev];

                    if (currentNumber > prevNumber && dp[1, prev] + 1 >= dp[0, current])
                    {
                        dp[0, current] = dp[1, prev] + 1;
                        parent[0, current] = prev;
                    }

                    if (currentNumber < prevNumber && dp[0, prev] + 1 >= dp[1, current])
                    {
                        dp[1, current] = dp[0, prev] + 1;
                        parent[1, current] = prev;
                    }
                }

                if (dp[0, current] > bestSize)
                {
                    bestSize = dp[0, current];
                    lastRowIndex = 0;
                    lastColIndex = current;
                }

                if (dp[1, current] > bestSize)
                {
                    bestSize = dp[1, current];
                    lastRowIndex = 1;
                    lastColIndex = current;
                }
            }

            var zigZag = new Stack<int>();

            while (lastColIndex != -1)
            {
                zigZag.Push(numbers[lastColIndex]);
                lastColIndex = parent[lastRowIndex, lastColIndex];

                if (lastRowIndex == 0)
                {
                    lastRowIndex = 1;
                }
                else
                {
                    lastRowIndex = 0;
                }
            }

            Console.WriteLine(string.Join(" ", zigZag));
        }
    }
}
