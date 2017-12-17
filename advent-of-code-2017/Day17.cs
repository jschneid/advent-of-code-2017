using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code_2017
{
    public class Day17
    {
        public void Run()
        {
            int input = 329;
            List<int> buffer = new List<int> { 0 };

            Console.WriteLine("Part 1 solution: " + Part1(buffer, input));
        }

        private int Part1(List<int> buffer, int steps)
        {
            int position = 0;
            for (int i = 1; i <= 2017; i++)
            {
                position = StepForward(position, buffer.Count, steps);
                buffer.Insert(position + 1, i);
                position = (position + 1) % buffer.Count;
            }
            return buffer[(position + 1) % buffer.Count];
        }

        private int StepForward(int position, int bufferSize, int steps)
        {
            return (position + steps) % bufferSize;
        }
    }
}
