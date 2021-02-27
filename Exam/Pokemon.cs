using System;
using System.Collections.Generic;
using System.Text;

namespace Pokemon
{
    class Program
    {
        static void Main(string[] args)
        {
            int fuel = int.Parse(Console.ReadLine());
            string command = Console.ReadLine();

            int counter = 0;
            List<string> streets = new List<string>();

            while (command != "End")
            {
                string[] tokens = command.Split(", ", StringSplitOptions.RemoveEmptyEntries);
                int pokemons = int.Parse(tokens[1]);
                int length = int.Parse(tokens[2]);

                if (fuel >= length)
                {
                    counter += pokemons;
                    streets.Add(tokens[0]);
                    fuel -= length;

                    if (fuel <= 0)
                    {
                        break;
                    }
                }


                command = Console.ReadLine();
            }

            if (counter > 0)
            {
                Console.WriteLine(string.Join(" -> ", streets));
            }
            Console.WriteLine($"Total Pokemon caught -> {counter}");
            Console.WriteLine($"Fuel Left -> {fuel}");
        }
    }
}
