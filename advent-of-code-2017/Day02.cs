using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace advent_of_code_2017
{
    /// <summary>
    /// Solves http://adventofcode.com/2017/day/2
    /// </summary>
    class Day02
    {
        public void Run()
        {
            List<List<int>> inputs = ParseInputFile("day02input.txt");

            DoPart1(inputs);
            DoPart2(inputs);
        }

        private List<List<int>> ParseInputFile(string path)
        {
            string[] lines = File.ReadAllLines(path);

            List<List<int>> inputs = new List<List<int>>();
            foreach (string line in lines)
            {
                inputs.Add(new List<int>());
                string[] values = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                foreach (string value in values)
                {
                    inputs.Last().Add(Int32.Parse(value));
                }
            }

            return inputs;
        }

        private void DoPart1(List<List<int>> inputs)
        {
            int sum = 0;
            foreach (List<int> input in inputs)
            {
                sum += input.Max() - input.Min();
            }

            Console.WriteLine("Solution 1: " + sum);
        }

        private void DoPart2(List<List<int>> inputs)
        {
            int sum = 0;
            foreach (List<int> input in inputs)
            {
                sum += (DoDivisorCalculation(input));
            }

            Console.WriteLine("Solution 2: " + sum);
        }

        private int DoDivisorCalculation(List<int> values)
        {
            for (int i = 0; i < values.Count - 1; i++)
            {
                for (int j = i + 1; j < values.Count; j++)
                {
                    if (values[i] % values[j] == 0)
                    {
                        return values[i] / values[j];
                    }
                    else if (values[j] % values[i] == 0)
                    {
                        return values[j] / values[i];
                    }
                }
            }
            throw new ArgumentException("No solution for values: " + String.Join(",", values));
        }
    }
}
