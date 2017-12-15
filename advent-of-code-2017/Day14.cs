using System;
using System.Globalization;
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
            Console.WriteLine("Part 1 solution: " + Part1("ljoxqyyw"));
        }

        private int Part1(string input)
        {
            Day10 knotHasher = new Day10();
            int totalUsed = 0;
            for (int i = 0; i < 128; i++)
            {
                string rowInput = input + "-" + i;
                string knotHash = knotHasher.Part2(rowInput);
                string binaryString = HexStringToBinaryString(knotHash);
                int usedCount = binaryString.Count(c => c == '1');
                totalUsed += usedCount;
            }
            return totalUsed;
        }

        private string HexStringToBinaryString(string hexString)
        {
            StringBuilder output = new StringBuilder();
            foreach (char c in hexString)
            {
                int decimalValue = Convert.ToInt32(c.ToString(), 16);
                string binaryString = Convert.ToString(decimalValue, 2);
                output.Append(binaryString);
            }
            return output.ToString();
        }
    }
}
