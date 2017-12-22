using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace advent_of_code_2017
{
    /// <summary>
    /// Solves: http://adventofcode.com/2017/day/22 ("Sporifica Virus") Part 2
    /// </summary>
    public class Day22Part2
    {
        Dictionary<Point, int> _grid; // 0 == Clean; 1 == Weakend; 2 == Infected; 3 == Flagged
        Point _currentPosition;
        int _facing; // 0 == North; 1 == East; 2 == South; 3 == West

        public void Run()
        {
            Console.WriteLine("Part 2 solution: " + Part2());
        }

        private int Part2()
        {
            PopulateGrid();
            int burstCausedInfectionCount = 0;

            for (int i = 0; i < 10000000; i++)
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
            _grid = new Dictionary<Point, int>();
            int y = 0;
            foreach (string inputLine in inputLines)
            {
                for (int x = 0; x < inputLine.Length; x++)
                {
                    Point p = new Point(x, y);
                    if (inputLine[x] == '#')
                    {
                        _grid[p] = 2;
                    }
                    else
                    {
                        _grid[p] = 0;
                    }
                }
                y++;
            }

            int initialPositionX = inputLines[0].Length / 2;
            int initialPositionY = inputLines.Length / 2;
            _currentPosition = new Point(initialPositionX, initialPositionY);

            _facing = 0;
        }

        /// <summary>
        /// Does a single "Burst" sequence as described in the problem statement (part 2).
        /// </summary>
        /// <returns>True if a node became infected; false otherwise. </returns>
        private bool Burst()
        {
            int currentNodeState = GetCurrentNodeState();

            bool nodeBecameInfected = (currentNodeState == 1);

            switch (currentNodeState)
            {
                case 0:
                    TurnLeft();
                    break;
                case 1:
                    break;
                case 2:
                    TurnRight();
                    break;
                case 3:
                    Reverse();
                    break;
            }

            UpdateCurrentNode();

            MoveForward();

            return nodeBecameInfected;
        }

        private int GetCurrentNodeState()
        {
            if (!_grid.ContainsKey(_currentPosition))
            {
                return 0; // Clean
            }
            return _grid[_currentPosition];
        }

        private void UpdateCurrentNode()
        {
            int currentState = GetCurrentNodeState();
            _grid[_currentPosition] = (currentState + 1) % 4;
        }

        private void TurnRight()
        {
            _facing = (_facing + 1) % 4;
        }

        private void Reverse()
        {
            _facing = (_facing + 2) % 4;
        }

        private void TurnLeft()
        {
            _facing = (_facing + 3) % 4;
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
