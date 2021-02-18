using System;
using System.Collections.Generic;

namespace LongestStringChain
{
    class LongestStringChain
    {
        static void Main(string[] args)
        {
            var words = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            var longestStringChain = 0;
            var lastIndex = 0;
            var len = new int[words.Length];
            var parent = new int[words.Length];

            for (int current = 0; current < words.Length; current++)
            {
                len[current] = 1;
                parent[current] = -1;
                var currentWord = words[current];

                for (int prev = current - 1; prev >= 0; prev--)
                {
                    var prevWord = words[prev];

                    if (currentWord.Length > prevWord.Length &&
                        len[prev] + 1 >= len[current])
                    {
                        len[current] = len[prev] + 1;
                        parent[current] = prev;
                    }
                }

                if (len[current] > longestStringChain)
                {
                    longestStringChain = len[current];
                    lastIndex = current;
                }
            }

            var stringChain = new Stack<string>();

            while (lastIndex != -1)
            {
                stringChain.Push(words[lastIndex]);
                lastIndex = parent[lastIndex];
            }

            Console.WriteLine(string.Join(" ", stringChain));
        }
    }
}
