using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace advent_of_code_2017
{
    public class Day10
    {
        public void Run()
        {
            Part1();
        }

        private void Part1()
        {
            int[] lengths = new int[] { 63, 144, 180, 149, 1, 255, 167, 84, 125, 65, 188, 0, 2, 254, 229, 24 };
            int[] list = new int[256];
            for (int i = 0; i < list.Length; i++)
            {
                list[i] = i;
            }

            int position = 0;
            int skipSize = 0;

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

            Console.WriteLine(list[0] * list[1]);
        }
    }
}
