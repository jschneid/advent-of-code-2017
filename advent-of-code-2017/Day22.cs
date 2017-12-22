using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace advent_of_code_2017
{
    /// <summary>
    /// Solves: http://adventofcode.com/2017/day/22
    /// </summary>
    public class Day22
    {
        Dictionary<Point, bool> _grid;
        Point _currentPosition;
        int _facing; // 0 == North; 1 == East; 2 == South; 3 == West

        public void Run()
        {
            Console.WriteLine("Part 1 solution: " + Part1());
            
        }

        private int Part1()
        {
            PopulateGrid();
            int burstCausedInfectionCount = 0;

            for (int i = 0; i < 10000; i++)
            {
                bool nodeBecameInfected = Burst();
                if (nodeBecameInfected)
                {
                    burstCausedInfectionCount++;
                }
            }

            return burstCausedInfectionCount;
        }

        private void PopulateGrid()
        {
            string[] inputLines = File.ReadAllLines("day22input.txt");
            _grid = new Dictionary<Point, bool>();
            int y = 0;
            foreach (string inputLine in inputLines)
            {
                for (int x = 0; x < inputLine.Length; x++)
                {
                    Point p = new Point(x, y);
                    _grid[p] = inputLine[x] == '#';
                }
                y++;
            }

            int initialPositionX = inputLines[0].Length / 2;
            int initialPositionY = inputLines.Length / 2;
            _currentPosition = new Point(initialPositionX, initialPositionY);

            _facing = 0;
        }

        /// <summary>
        /// Does a single "Burst" sequence as described in the problem statement.
        /// </summary>
        /// <returns>True if a node flipped from uninfected to infected; false otherwise. </returns>
        private bool Burst()
        {
            if (IsCurrentNodeInfected())
            {
                TurnRight();
            }
            else
            {
                TurnLeft();
            }

            bool nodeBecameInfected = ToggleCurrentNode();

            MoveForward();

            return nodeBecameInfected;
        }

        private bool IsCurrentNodeInfected()
        {
            if (!_grid.ContainsKey(_currentPosition))
            {
                return false;
            }
            return _grid[_currentPosition];
        }

        private bool ToggleCurrentNode()
        {
            if (IsCurrentNodeInfected())
            {
                _grid[_currentPosition] = false;
                return false;
            }
            else
            {
                _grid[_currentPosition] = true;
                return true;
            }
        }

        private void TurnRight()
        {
            _facing = (_facing + 1) % 4;
        }

        private void TurnLeft()
        {
            _facing--;
            if (_facing < 0)
            {
                _facing = 3;
            }
        }

        private void MoveForward()
        {
            switch (_facing)
            {
                case 0:
                    _currentPosition.Y--;
                    break;
                case 1:
                    _currentPosition.X++;
                    break;
                case 2:
                    _currentPosition.Y++;
                    break;
                case 3:
                    _currentPosition.X--;
                    break;
            }
        }
    }
}
