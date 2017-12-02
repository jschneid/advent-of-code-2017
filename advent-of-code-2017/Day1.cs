using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code_2017
{
    /// <summary>
    /// Solves: http://adventofcode.com/2017/day/1
    /// </summary>
    public class Day1
    {
        public void Run(string input)
        {
            Part1(input);
            Part2(input);
        }

        private void Part1(string input)
        {
            int sum = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == input[(i + 1) % input.Length])
                {
                    sum += Int32.Parse(input[i].ToString());
                }
            }
            Console.WriteLine("Part 1 Solution: " + sum);
        }

        private void Part2(string input)
        {
            int sum = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == input[(i + (input.Length / 2)) % input.Length])
                {
                    sum += Int32.Parse(input[i].ToString());
                }
            }
            Console.WriteLine("Part 2 Solution: " + sum);
        }
    }
}
