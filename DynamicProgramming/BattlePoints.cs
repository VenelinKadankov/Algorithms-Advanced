using System;
using System.Linq;

namespace BattlePoints
{
    class BattlePoints
    {
        static void Main(string[] args)
        {
            var requiredEnergy = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var battlePoints = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var initialEnergy = int.Parse(Console.ReadLine());

            var enemies = requiredEnergy.Length;

            var matrix = new int[enemies + 1, initialEnergy + 1];

            //var included = new bool[enemies + 1, initialEnergy + 1]; //

            for (int row = 1; row < matrix.GetLength(0); row++)
            {
                var enemyIndex = row - 1;
                var enemyEnergy = requiredEnergy[enemyIndex];
                var enemyPoints = battlePoints[enemyIndex];

                for (int energy = 1; energy < matrix.GetLength(1); energy++)
                {
                    var skip = matrix[row - 1, energy];

                    if (enemyEnergy > energy)
                    {
                        matrix[row, energy] = skip;
                        continue;
                    }

                    var take = enemyPoints + matrix[row - 1, energy - enemyEnergy];

                    matrix[row, energy] = Math.Max(skip, take);  // instead

                    //if (take > skip)
                    //{
                    //    matrix[row, energy] = take;
                    //    included[row, energy] = true;
                    //}
                    //else
                    //{
                    //    matrix[row, energy] = skip;
                    //}
                }
            }

            Console.WriteLine(matrix[enemies, initialEnergy]);


            //Console.WriteLine();

            //var currentEnergy = initialEnergy;

            //for (int row = enemies; row >= 0; row--)
            //{
            //    if (included[row, currentEnergy])
            //    {
            //        var enemyIndex = row - 1;

            //        Console.WriteLine(enemyIndex);
            //        currentEnergy -= requiredEnergy[enemyIndex];

            //        if (currentEnergy == 0)
            //        {
            //            break;
            //        }
            //    }
            //}
        }
    }
}
