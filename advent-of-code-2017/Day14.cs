using System;
using System.Linq;
using System.Text;

namespace advent_of_code_2017
{
    /// <summary>
    /// Solves: http://adventofcode.com/2017/day/14
    /// </summary>
    public class Day14
    {
        public void Run()
        {
            bool[,] grid = BuildGrid("ljoxqyyw");
            Console.WriteLine("Part 1 solution: " + Part1(grid));
            Console.WriteLine("Part 2 solution: " + Part2(grid));
        }

        private int Part1(bool[,] grid)
        {
            int count = (from bool cell in grid where cell select cell).Count();
            return count;
        }

        private string HexStringToBinaryString(string hexString)
        {
            StringBuilder output = new StringBuilder();
            foreach (char c in hexString)
            {
                int decimalValue = Convert.ToInt32(c.ToString(), 16);
                string binaryString = Convert.ToString(decimalValue, 2).PadLeft(4, '0');
                output.Append(binaryString);
            }
            return output.ToString();
        }

        private bool[,] BuildGrid(string input)
        {
            bool[,] grid = new bool[128, 128];
            Day10 knotHasher = new Day10();
            for (int x = 0; x < 128; x++)
            {
                string rowInput = input + "-" + x;
                string knotHash = knotHasher.Part2(rowInput);
                string binaryString = HexStringToBinaryString(knotHash);
                for (int y = 0; y < 128; y++)
                {
                    grid[x, y] = binaryString[y] == '1';
                }
            }
            return grid;
        }

        private int Part2(bool[,] grid)
        {
            int regions = 0;
            bool[,] visited = new bool[128, 128];
            for (int x = 0; x < 128; x++)
            {
                for (int y = 0; y < 128; y++)
                {
                    if (Traverse(grid, x, y, visited))
                    {
                        regions++;
                    }
                }
            }
            return regions;
        }

        private bool Traverse(bool[,] grid, int x, int y, bool[,] visited)
        {
            Console.WriteLine(x + "," + y);
            if (visited[x, y])
            {
                return false;
            }
            if (!grid[x, y])
            {
                return false;
            }

            visited[x, y] = true;
            if (x > 0)
            {
                Traverse(grid, x - 1, y, visited);
            }
            if (y > 0)
            {
                Traverse(grid, x, y - 1, visited);
            }
            if (x < 127)
            {
                Traverse(grid, x + 1, y, visited);
            }
            if (y < 127)
            {
                Traverse(grid, x, y + 1, visited);
            }
            return true;
        }
    }
}
