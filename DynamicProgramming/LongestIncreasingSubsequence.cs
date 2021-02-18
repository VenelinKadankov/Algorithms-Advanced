using System;
using System.Collections.Generic;
using System.Linq;

namespace LongestIncreasingSubsequence
{
    class LongestIncreasingSubsequence
    {
        static void Main(string[] args)
        {
            var numbers = Console.ReadLine()
                 .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                 .Select(int.Parse)
                 .ToArray();

            var len = new int[numbers.Length];
            var prev = new int[numbers.Length];

            var bestLen = 0;
            var lastIndex = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                prev[i] = -1;
                var currentNumber = numbers[i];
                var currentBestSequence = 1;

                for (int j = i - 1; j >= 0; j--)
                {
                    var previousNumber = numbers[j];

                    if (previousNumber < currentNumber && len[j] + 1 >= currentBestSequence) 
                    {
                        currentBestSequence = len[j] + 1;
                        prev[i] = j;
                    }
                }

                if (currentBestSequence > bestLen)
                {
                    bestLen = currentBestSequence;
                    lastIndex = i;
                }

                len[i] = currentBestSequence;
            }

            var LongestIncreasingSubsequence = new Stack<int>();

            while (lastIndex != -1)
            {
                LongestIncreasingSubsequence.Push(numbers[lastIndex]);
                lastIndex = prev[lastIndex];
            }

            Console.WriteLine(string.Join(" ", LongestIncreasingSubsequence));
        }
    }
}
