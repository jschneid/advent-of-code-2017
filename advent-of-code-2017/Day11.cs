using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace advent_of_code_2017
{
    /// <summary>
    /// Solves: http://adventofcode.com/2017/day/11
    /// </summary>
    public class Day11
    {
        public void Run()
        {
            string inputText = File.ReadAllText("day11input.txt");
            string[] inputs = inputText.Split(',');

            // This solution uses the "Cube coordinates" method of tracking hex
            // coordinates and computing distances between hexes from this 
            // excellent article: 
            // https://www.redblobgames.com/grids/hexagons/

            int x = 0;
            int y = 0;
            int z = 0;
            int maxDistance = 0;
            foreach (string input in inputs)
            {
                AdjustCubeCoordinates(input, ref x, ref y, ref z, ref maxDistance);
            }

            Console.WriteLine("Part 1 solution: " + CubeDistance(x, y, z));
            Console.WriteLine("Part 2 solution: " + maxDistance);
        }

        private void AdjustCubeCoordinates(string input, ref int x, ref int y, ref int z, ref int maxDistance)
        {
            switch (input)
            {
                case "n":
                    y++;
                    z--;
                    break;
                case "ne":
                    x++;
                    z--;
                    break;
                case "se":
                    x++;
                    y--;
                    break;
                case "s":
                    y--;
                    z++;
                    break;
                case "sw":
                    x--;
                    z++;
                    break;
                case "nw":
                    x--;
                    y++;
                    break;
            }
            int currentDistance = CubeDistance(x, y, z);
            if (currentDistance > maxDistance)
            {
                maxDistance = currentDistance;
            }
        }

        private int CubeDistance(int x, int y, int z)
        {
            return (Math.Abs(x) + Math.Abs(y) + Math.Abs(z)) / 2;
        }
    }
}
