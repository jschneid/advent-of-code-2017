using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advent_of_code_2017
{
    /// <summary>
    /// Solves: http://adventofcode.com/2017/day/3
    /// </summary>
    public class Day03
    {
        public void Run()
        {
            const int target = 289326;
            FindTargetXY(target, out int x, out int y);
            int distanceToCenter = (Math.Abs(x) + Math.Abs(y));
            Console.WriteLine("Part 1 Solution: " + distanceToCenter);

            int part2Solution = Part2(target);
            Console.WriteLine("Part 2 Solution: " + part2Solution);
        }
        private void FindTargetXY(int target, out int x, out int y)
        {
            int i = 1;
            x = 0;
            y = 0;
            int maxX = 0;
            int maxY = 0;
            int minX = 0;
            int minY = 0;

            while (true)
            {
                // Right
                while (x <= maxX)
                {
                    x++;
                    if (++i == target) return;
                }
                maxX = x;

                // Up
                while (y <= maxY)
                {
                    y++;
                    if (++i == target) return;
                }
                maxY = y;

                // Left
                while (x >= minX)
                {
                    x--;
                    if (++i == target) return;
                }
                minX = x;

                // Down
                while (y >= minY)
                {
                    y--;
                    if (++i == target) return;
                }
                minY = y;
            }
        }

        private int Part2(int target)
        {
            int x = 0;
            int y = 0;
            int maxX = 0;
            int maxY = 0;
            int minX = 0;
            int minY = 0;

            // Set xyValues to a data structure containing values we can reference by doing xyValues[x][y] (including negative x,y values).
            Dictionary<int, Dictionary<int, int>> xyValues = new Dictionary<int, Dictionary<int, int>>();

            SetXYValue(0, 0, 1, xyValues);

            while (true)
            {
                // Right
                while (x <= maxX)
                {
                    x++;
                    CalculateAndSetValue(x, y, xyValues);
                    if (xyValues[x][y] > target) return xyValues[x][y];
                }
                maxX = x;

                // Up
                while (y <= maxY)
                {
                    y++;
                    CalculateAndSetValue(x, y, xyValues);
                    if (xyValues[x][y] > target) return xyValues[x][y];
                }
                maxY = y;

                // Left
                while (x >= minX)
                {
                    x--;
                    CalculateAndSetValue(x, y, xyValues);
                    if (xyValues[x][y] > target) return xyValues[x][y];
                }
                minX = x;

                // Down
                while (y >= minY)
                {
                    y--;
                    CalculateAndSetValue(x, y, xyValues);
                    if (xyValues[x][y] > target) return xyValues[x][y];
                }
                minY = y;
            }
        }

        private void CalculateAndSetValue(int x, int y, Dictionary<int, Dictionary<int, int>> xyValues)
        {
            int total = 0;

            // Iterate through each of the values immediately adjacent to the x,y value (including diagonals).
            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    // Add the value at i,j to the total if it exists, and is not the x,y value itself.
                    if ((!(i == x && j == y)) && xyValues.ContainsKey(i) && xyValues[i].ContainsKey(j))
                    {
                        total += xyValues[i][j];
                    }
                }
            }
            SetXYValue(x, y, total, xyValues);
        }

        /// <summary>
        /// Sets the value at x,y to the specified value.
        /// </summary>
        private void SetXYValue(int x, int y, int value, Dictionary<int, Dictionary<int, int>> xyValues)
        {
            if (!xyValues.ContainsKey(x))
            {
                xyValues.Add(x, new Dictionary<int, int>());
            }
            xyValues[x][y] = value;
        }
    }
}
