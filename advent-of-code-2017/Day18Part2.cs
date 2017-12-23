using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Day18Part2
{
    /// <summary>
    /// Solves Part 2 of https://adventofcode.com/2017/day/18 ("Duet")
    /// </summary>
    public class Day18Part2
    {
        int[] _sendsCount = new int[2];
        bool[] _programWaiting = new bool[2];

        public void Run()
        {
            string[] inputLines = File.ReadAllLines("day18input.txt");

            Part2(inputLines);
            Console.WriteLine("Part 2 solution: " + _sendsCount[1]);
        }

        private void Part2(string[] inputLines)
        {
            Dictionary<char, long> registers0 = new Dictionary<char, long>();
            Dictionary<char, long> registers1 = new Dictionary<char, long>();
            long instructionPointer0 = 0;
            long instructionPointer1 = 0;
            List<long> queue0 = new List<long>();
            List<long> queue1 = new List<long>();

            registers1['p'] = 1;

            while (true)
            {
                instructionPointer0 = Process(inputLines[instructionPointer0], registers0, instructionPointer0, 0, queue0, queue1);
                if (instructionPointer0 < 0 || instructionPointer0 >= inputLines.Length || BothWaiting())
                {
                    return;
                }

                instructionPointer1 = Process(inputLines[instructionPointer1], registers1, instructionPointer1, 1, queue1, queue0);
                if (instructionPointer1 < 0 || instructionPointer1 >= inputLines.Length || BothWaiting())
                {
                    return;
                }
            }
        }

        private bool BothWaiting()
        {
            return (_programWaiting[0] && _programWaiting[1]);
        }

        private long Process(string inputLine, Dictionary<char, long> registers, long instructionPointer, int programID, List<long> myQueue, List<long> otherProgramQueue)
        {
            Instruction instruction = new Instruction(inputLine);
            _programWaiting[programID] = false;
            switch (instruction.Operation)
            {
                case "snd":
                    otherProgramQueue.Add(instruction.GetArgumentValue(0, registers));
                    _sendsCount[programID]++;
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
                    if (myQueue.Count > 0)
                    {
                        registers[instruction.ArgRegister[0]] = myQueue[0];
                        myQueue.RemoveAt(0);
                    }
                    else
                    {
                        _programWaiting[programID] = true;
                        return instructionPointer;
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
                return Day18Part2.GetExistingOrNewRegister(ArgRegister[argumentNumber], registers);
            }
        }
    }
}