using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code_2017
{
    /// <summary>
    /// Solves: http://adventofcode.com/2017/day/17
    /// </summary>
    public class Day17
    {
        public void Run()
        {
            int input = 329;

            List<int> buffer = new List<int> { 0 };
            int finalPosition = Spinlock(buffer, input, 2017);
            int valueAfter2017 = buffer[(finalPosition + 1) % buffer.Count];
            Console.WriteLine("Part 1 solution: " + valueAfter2017);

            Console.WriteLine("Part 2 solution: " + SpinlockLean(input, 50000000));
        }

        private int Spinlock(List<int> buffer, int steps, int maxValue)
        {
            int position = 0;
            for (int i = 1; i <= maxValue; i++)
            {
                position = StepForward(position, buffer.Count, steps);
                buffer.Insert(position + 1, i);
                position = (position + 1) % buffer.Count;
            }
            return position;
        }

        /// <summary>
        /// This is an optimized version of Spinlock which returns the value at index 1 (the "value after 0" from 
        /// the problem; since 0 is always at index 0, we can just return the value at index 1). Skipping actually
        /// tracking the values at any buffer index except index 1, and avoiding all of the corresponding object
        /// allocations and assignments that the regular Spinlock implementation does, makes this run a LOT faster 
        /// for the high iteration count.
        /// </summary>
        private int SpinlockLean(int steps, int maxValue)
        {
            int position = 0;
            int bufferLength = 1;
            int valueAtIndex1 = 0;
            for (int i = 1; i <= maxValue; i++)
            {
                position = StepForward(position, bufferLength, steps);
                if (position == 0)
                {
                    valueAtIndex1 = i;
                }
                bufferLength++;
                position = (position + 1) % bufferLength;
            }
            return valueAtIndex1;
        }

        private int StepForward(int position, int bufferSize, int steps)
        {
            return (position + steps) % bufferSize;
        }
    }
}
