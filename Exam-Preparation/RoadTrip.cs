using System;
using System.Linq;

namespace RoadTrip
{
    class Program
    {
        static void Main(string[] args)
        {
            var value = Console.ReadLine()
                .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var weight = Console.ReadLine()
                .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var maxCapacity = int.Parse(Console.ReadLine());
            var items = value.Length;

            var dp = new int[items + 1, maxCapacity + 1];

            for (int row = 1; row < dp.GetLength(0); row++)
            {
                var itemValue = value[row - 1];
                var itemWeight = weight[row - 1];

                for (int capacity = 1; capacity < dp.GetLength(1); capacity++)
                {
                    var skipValue = dp[row - 1, capacity];

                    if (capacity < itemWeight)
                    {
                        dp[row, capacity] = skipValue;
                        continue;
                    }

                    var takeValue = itemValue + dp[row - 1, capacity - itemWeight];

                    if (takeValue > skipValue)
                    {
                        dp[row, capacity] = takeValue;
                    }
                    else
                    {
                        dp[row, capacity] = skipValue;
                    }

                }
            }
            var maxValue = dp[items, maxCapacity];
            Console.WriteLine($"Maximum value: {maxValue}");
        }
    }
}
