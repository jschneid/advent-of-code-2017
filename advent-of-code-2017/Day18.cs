using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace advent_of_code_2017
{
    /// <summary>
    /// Solves: https://adventofcode.com/2017/day/18 ("Duet")
    /// </summary>
    public class Day18
    {
        long _lastSoundPlayed;
        bool _doneProcessing = false;

        public void Run()
        {
            string[] inputLines = File.ReadAllLines("day18input.txt");

            Console.WriteLine("Part 1 solution: " + Part1(inputLines));
        }

        private long Part1(string[] inputLines)
        {
            Dictionary<char, long> registers = new Dictionary<char, long>();
            long instructionPointer = 0;

            while (true)
            {
                instructionPointer = Process(inputLines[instructionPointer], registers, instructionPointer);
                if (_doneProcessing)
                {
                    return _lastSoundPlayed;
                }
            }
        }

        private long Process(string inputLine, Dictionary<char, long> registers, long instructionPointer)
        {
            Instruction instruction = new Instruction(inputLine);
            switch (instruction.Operation)
            {
                case "snd":
                    _lastSoundPlayed = instruction.GetArgumentValue(0, registers);
                    break;

                case "set":
                    registers[instruction.ArgRegister[0]] = instruction.GetArgumentValue(1, registers);
                    break;

                case "add":
                    registers[instruction.ArgRegister[0]] = GetExistingOrNewRegister(instruction.ArgRegister[0], registers) + instruction.GetArgumentValue(1, registers);
                    break;

                case "mul":
                    registers[instruction.ArgRegister[0]] = GetExistingOrNewRegister(instruction.ArgRegister[0], registers) * instruction.GetArgumentValue(1, registers);
                    break;

                case "mod":
                    registers[instruction.ArgRegister[0]] = GetExistingOrNewRegister(instruction.ArgRegister[0], registers) % instruction.GetArgumentValue(1, registers);
                    break;

                case "rcv":
                    if (instruction.GetArgumentValue(0, registers) != 0)
                    {
                        _doneProcessing = true;
                    }
                    break;

                case "jgz":
                    if (instruction.GetArgumentValue(0, registers) > 0)
                    {
                        return instructionPointer + instruction.GetArgumentValue(1, registers);
                    }
                    break;
            }
            return (instructionPointer + 1);
        }

        /// <summary>
        /// If there's an existing register in the list with the specified name, returns its value.
        /// Otherwise, creates a new register with that name, adds it to the list, and returns 0
        /// (the value of the new register).
        /// </summary>
        public static long GetExistingOrNewRegister(char name, Dictionary<char, long> registers)
        {
            if (!registers.ContainsKey(name))
            {
                registers[name] = 0;
            }
            return registers[name];
        }
    }

    class Instruction
    {
        public string Operation { get; }
        public bool[] ArgIsNumber { get; }
        public long[] ArgNumber { get; }
        public char[] ArgRegister { get; }

        public Instruction(string line)
        {
            ArgIsNumber = new bool[2];
            ArgNumber = new long[2];
            ArgRegister = new char[2];

            string[] tokens = line.Split();
            Operation = tokens[0];

            ProcessToken(tokens[1], 0);
            if (tokens.Length > 2)
            {
                ProcessToken(tokens[2], 1);
            }
        }

        private void ProcessToken(string token, int argumentNumber)
        {
            if (Int64.TryParse(token, out ArgNumber[argumentNumber]))
            {
                ArgIsNumber[argumentNumber] = true;
            }
            else
            {
                ArgIsNumber[argumentNumber] = false;
                ArgRegister[argumentNumber] = token[0];
            }
        }

        public long GetArgumentValue(int argumentNumber, Dictionary<char, long> registers)
        {
            if (ArgIsNumber[argumentNumber])
            {
                return ArgNumber[argumentNumber];
            }
            else
            {
                return Day18.GetExistingOrNewRegister(ArgRegister[argumentNumber], registers);
            }
        }
    }
}