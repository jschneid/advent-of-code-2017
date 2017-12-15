using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace advent_of_code_2017
{
    public class Day10
    {
        /// <summary>
        /// Solves: http://adventofcode.com/2017/day/10
        /// </summary>
        public void Run()
        {
            Console.WriteLine("Part 1 solution: " + Part1(new int[] { 63, 144, 180, 149, 1, 255, 167, 84, 125, 65, 188, 0, 2, 254, 229, 24 }));
            Console.WriteLine("Part 2 solution: " + Part2("63,144,180,149,1,255,167,84,125,65,188,0,2,254,229,24"));
        }

        private int Part1(int[] lengths)
        {
            int[] list = InitList();

            int position = 0;
            int skipSize = 0;

            RunRound(list, lengths, ref position, ref skipSize);

            return list[0] * list[1];
        }

        private int[] InitList()
        {
            int[] list = new int[256];
            for (int i = 0; i < list.Length; i++)
            {
                list[i] = i;
            }
            return list;
        }

        private void RunRound(int[] list, int[] lengths, ref int position, ref int skipSize)
        {
            foreach (int length in lengths)
            {
                List<int> elementsToReverse = new List<int>();
                int p2 = position;
                for (int i = 0; i < length; i++)
                {
                    elementsToReverse.Add(list[p2]);
                    p2 = (p2 + 1) % list.Length;
                }

                elementsToReverse.Reverse();

                p2 = position;
                for (int i = 0; i < length; i++)
                {
                    list[p2] = elementsToReverse[i];
                    p2 = (p2 + 1) % list.Length;
                }

                position = (position + length + skipSize) % list.Length;
                skipSize++;
            }
        }

        public string Part2(string input)
        {
            int[] lengths = ConvertToBytes(input);

            lengths = lengths.Concat(new int[] { 17, 31, 73, 47, 23 }).ToArray();

            int[] list = InitList();
            int position = 0;
            int skipSize = 0;

            for (int i = 0; i < 64; i++)
            {
                RunRound(list, lengths, ref position, ref skipSize);
            }

            int[] denseHash = GetDenseHash(list);

            string hexOutput = GetHexOutput(denseHash);
            return hexOutput;
        }

        private int[] ConvertToBytes(string input)
        {
            int[] lengths = new int[input.Length];
            int i = 0;
            foreach (char c in input)
            {
                lengths[i] = (byte)c;
                i++;
            }
            return lengths;
        }

        private int[] GetDenseHash(int[] list)
        {
            int[] denseHash = new int[16];
            for (int i = 0; i < 256; i += 16)
            {
                int xoredValue = list[i];
                for (int j = 1; j < 16; j++)
                {
                    xoredValue ^= list[j + i];
                }
                denseHash[i / 16] = xoredValue;
            }
            return denseHash;
        }

        private string GetHexOutput(int[] denseHash)
        {
            StringBuilder hexOutput = new StringBuilder();
            for (int i = 0; i < 16; i++)
            {
                hexOutput.Append(denseHash[i].ToString("X2"));
            }
            return hexOutput.ToString().ToLower();
        }
    }
}
