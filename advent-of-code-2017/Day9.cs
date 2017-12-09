using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace advent_of_code_2017
{
    /// <summary>
    /// Solves http://adventofcode.com/2017/day/9
    /// </summary>
    public class Day9
    {
        public void Run()
        {
            string input = File.ReadAllText("day9input.txt");
            int score = Process(input, out int totalNonCanceledGarbageCharCount);
            Console.WriteLine("Part 1 solution: " + score);
            Console.WriteLine("Part 2 solution: " + totalNonCanceledGarbageCharCount);
        }

        private int Process(string input, out int totalNonCanceledGarbageCharCount)
        {
            int streamLevel = 0;
            int score = 0;
            totalNonCanceledGarbageCharCount = 0;

            int i = 0;
            while (i < input.Length)
            {
                switch(input[i])
                {
                    case '{':
                        streamLevel++;
                        i++;
                        break;
                    case '}':
                        score += streamLevel;
                        streamLevel--;
                        i++;
                        break;
                    case '!':
                        i += 2;
                        break;
                    case '<':
                        i = SkipToEndOfGarbage(i, input, out int nonCanceledGarbageCharCount);
                        totalNonCanceledGarbageCharCount += nonCanceledGarbageCharCount;
                        break;
                    default:
                        i++;
                        break;
                }
            }
            return score;
        }

        private int SkipToEndOfGarbage(int garbageStartIndex, string input, out int nonCanceledGarbageCharCount)
        {
            int i = garbageStartIndex + 1;
            nonCanceledGarbageCharCount = 0;
            while(true)
            {
                switch(input[i])
                {
                    case '>':
                        return i + 1;
                    case '!':
                        i += 2;
                        break;
                    default:
                        i++;
                        nonCanceledGarbageCharCount++;
                        break;
                }
            }
        }
    }
}
