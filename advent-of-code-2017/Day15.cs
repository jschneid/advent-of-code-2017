using System;
namespace advent_of_code_2017
{
    public class Day15
    {
        /// <summary>
        /// Solves: http://adventofcode.com/2017/day/15
        /// </summary>
        public void Run()
        {
            int matches = 0;
            long valueA = 512;
            long valueB = 191;
            for (int i = 0; i < 40000000; i++)
            {
                valueA = GetNextValue(valueA, 'A');
                valueB = GetNextValue(valueB, 'B');

                if (Judge(valueA, valueB))
                {
                    matches++;
                }
            }

            Console.Out.WriteLine("Part 1 solution: " + matches);
        }

        /// <summary>
        /// Return true if the lowest 16 bits of the two values are the same.
        /// </summary>
        private bool Judge(long valueA, long valueB)
        {
            return (valueA % 65536 == valueB % 65536);
        }

        private long GetNextValue(long initialValue, char generator)
        {
            int multiplier = (generator == 'A' ? 16807 : 48271);
            long newValue = (initialValue * multiplier) % 2147483647;
            return newValue;
        }
    }
}
