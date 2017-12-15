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
            //Console.Out.WriteLine("Part 1 solution: " + Part1());
            Console.Out.WriteLine("Part 2 solution: " + Part2());
        }

        private int Part1()
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
            return matches;
        }

        /// <summary>
        /// Returns true if the lowest 16 bits of the two values are the same.
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

        private int Part2()
        {
            int matches = 0;
            long valueA = 512;
            long valueB = 191;
            for (int i = 0; i < 5000000; i++)
            {
                valueA = GetNextPart2ValueA(valueA);
                valueB = GetNextPart2ValueB(valueB);

                if (Judge(valueA, valueB))
                {
                    matches++;
                }
            }
            return matches;
        }

        private long GetNextPart2ValueA(long initialValue)
        {
            int multiplier = 16807;
            long newValue = initialValue;
            do
            {
                newValue = (newValue * multiplier) % 2147483647;
            } while (newValue % 4 != 0);
            return newValue;
        }

        private long GetNextPart2ValueB(long initialValue)
        {
            int multiplier = 48271;
            long newValue = initialValue;
            do
            {
                newValue = (newValue * multiplier) % 2147483647;
            } while (newValue % 8 != 0);
            return newValue;
        }
    }
}
