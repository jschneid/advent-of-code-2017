using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2017
{
    /// <summary>
    /// Solves http://adventofcode.com/2017/day/25 ("The Halting Problem")
    /// </summary>
    public class Day25
    {
        Dictionary<int, int> _tape;
        int _position = 0;
        Dictionary<char, State> _states;
        char _nextState;
        int _steps;

        public void Run()
        {
            _tape = new Dictionary<int, int>();
            ParseInput();

            for (int i = 0; i < _steps; i++)
            {
                RunState(_states[_nextState]);
            }

            Console.WriteLine("Solution: " + DiagnosticChecksum());
        }

        private void ParseInput()
        {
            string[] inputLines = File.ReadAllLines("day25input.txt");
            _states = new Dictionary<char, State>();

            string[] firstLineTokens = inputLines[0].Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            _nextState = firstLineTokens[3][0];

            string[] secondLineTokens = inputLines[1].Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            _steps = Int32.Parse(secondLineTokens[5]);

            for (int i = 3; i < inputLines.Length; i += 10)
            {
                State state = new State();

                char stateID = inputLines[i].Split(new char[0], StringSplitOptions.RemoveEmptyEntries)[2][0];

                state.ValueToWrite[0] = Int32.Parse(inputLines[i + 2].Split(new char[0], StringSplitOptions.RemoveEmptyEntries)[4][0].ToString());
                state.DirectionToMove[0] = inputLines[i + 3].Split(new char[0], StringSplitOptions.RemoveEmptyEntries)[6] == "right.";
                state.NextState[0] = inputLines[i + 4].Split(new char[0], StringSplitOptions.RemoveEmptyEntries)[4][0];

                state.ValueToWrite[1] = Int32.Parse(inputLines[i + 6].Split(new char[0], StringSplitOptions.RemoveEmptyEntries)[4][0].ToString());
                state.DirectionToMove[1] = inputLines[i + 7].Split(new char[0], StringSplitOptions.RemoveEmptyEntries)[6] == "right.";
                state.NextState[1] = inputLines[i + 8].Split(new char[0], StringSplitOptions.RemoveEmptyEntries)[4][0];

                _states[stateID] = state;
            }
        }

        private void RunState(State state)
        {
            int currentPositionTapeValue = GetCurrentPositionTapeValue();

            _tape[_position] = state.ValueToWrite[currentPositionTapeValue];

            if (state.DirectionToMove[currentPositionTapeValue] == false)
            {
                _position--;
            }
            else
            {
                _position++;
            }

            _nextState = state.NextState[currentPositionTapeValue];
        }

        private int GetCurrentPositionTapeValue()
        {
            if (!_tape.ContainsKey(_position))
            {
                _tape[_position] = 0;
            }

            return _tape[_position];
        }

        private int DiagnosticChecksum()
        {
            return _tape.Values.Sum(v => v);
        }
    }

    public class State
    {
        public int[] ValueToWrite { get; set;}
        public bool[] DirectionToMove { get; set; } // false == left, true == right
        public char[] NextState { get; set; }

        public State()
        {
            ValueToWrite = new int[2];
            DirectionToMove = new bool[2];
            NextState = new char[2];
        }
    }
}
