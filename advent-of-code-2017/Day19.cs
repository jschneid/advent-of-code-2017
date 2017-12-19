using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace advent_of_code_2017
{
    /// <summary>
    /// Solves http://adventofcode.com/2017/day/19
    /// </summary>
    public class Day19
    {
        string[] _inputLines;

        public void Run()
        {
            _inputLines = File.ReadAllLines("day19input.txt");


            Console.WriteLine("Part 1 solution: " + Part1());

        }
        private string Part1()
        {
            StringBuilder solution = new StringBuilder();
            int x = _inputLines[0].IndexOf('|');
            int y = 0;
            int direction = 2; // 0==North, 1==East, 2==South, 3==West

            while (true)
            {
                switch (CharAt(x, y))
                {
                    case '|':
                        Continue(ref x, ref y, direction);
                        break;
                    case '-':
                        Continue(ref x, ref y, direction);
                        break;

                    case '+':
                        Turn(ref x, ref y, ref direction);
                        break;

                    case ' ':
                        // We're done if we continued past a letter and onto a blank spot.
                        // NOTE: The problem statement is vague; we could possibly also terminate
                        // if we continue past a letter and onto a wrong-way-facing pipe?
                        return solution.ToString();

                    default: // A letter
                        solution.Append(CharAt(x, y));
                        Continue(ref x, ref y, direction);
                        break;
                            
                }
            }
        }

        private char CharAt(int x, int y)
        {
            // Return an empty cell if we ran off the edge of the input
            if (y >= _inputLines.Length || x >= _inputLines[y].Length || y < 0 || x < 0)
            {
                return ' ';
            }

            return _inputLines[y][x];
        }


        private void Continue(ref int x, ref int y, int direction)
        {
            switch (direction)
            {
                case 0:
                    y--;
                    break;
                case 1:
                    x++;
                    break;
                case 2:
                    y++;
                    break;
                case 3:
                    x--;
                    break;
            }
        }

        private void Turn(ref int x, ref int y, ref int direction)
        {
            if (direction == 0 || direction == 2) // We were heading North or South
            {
                if (CharAt(x + 1, y) == '-' || Char.IsLetter(CharAt(x + 1, y)))
                {
                    x++;
                    direction = 1;
                }
                else
                {
                    x--;
                    direction = 3;
                }
            }
            else // We were heading East or West
            {
                if (CharAt(x, y + 1) == '|' || Char.IsLetter(CharAt(x, y + 1)))
                {
                    y++;
                    direction = 2;
                }
                else
                {
                    y--;
                    direction = 0;
                }
            }            
        }
    }
}
