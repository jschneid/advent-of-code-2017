using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
namespace advent_of_code_2017
{
    /// <summary>
    /// Solves: http://adventofcode.com/2017/day/5
    /// </summary>
    public class Day5
    {
        public void Run()
        {
            string[] lines = File.ReadAllLines("day5input.txt");

            List<int> jumps = lines.Select(line => Int32.Parse(line)).ToList();
            int moveCount = Part1(jumps);
            Console.WriteLine("Part 1 solution: " + moveCount);

            jumps = lines.Select(line => Int32.Parse(line)).ToList();
            moveCount = Part2(jumps);
            Console.WriteLine("Part 2 solution: " + moveCount);
        }

        private int Part1(List<int> jumps)
        {
            int currentPosition = 0;
            int moveCount = 0;
            while (currentPosition >= 0 && currentPosition < jumps.Count)
            {
                currentPosition += jumps[currentPosition]++;
                moveCount++;
            }
            return moveCount;
        }

        private int Part2(List<int> jumps)
        {
            int currentPosition = 0;
            int moveCount = 0;
            while (currentPosition >= 0 && currentPosition < jumps.Count)
            {
                int nextPosition = currentPosition + jumps[currentPosition];
                DoPart2OffsetAdjustment(jumps, currentPosition);
                currentPosition = nextPosition;
                moveCount++;
            }
            return moveCount;
        }

        private void DoPart2OffsetAdjustment(List<int> jumps, int position)
        {
            if (jumps[position] >= 3)
            {
                jumps[position]--;
            }
            else
            {
                jumps[position]++;
            }
        }
    }
}
