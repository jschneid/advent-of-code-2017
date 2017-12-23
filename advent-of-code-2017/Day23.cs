using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace advent_of_code_2017
{
    /// <summary>
    /// Solves http://adventofcode.com/2017/day/23 ("Coprocessor Conflaguration")
    /// </summary>
    public class Day23
    {
        private int _mulCount = 0;

        public void Run()
        {
            Console.WriteLine("Part 1 solution: " + Part1());
            Console.WriteLine("Part 2 solution: " + Part2Fast());
        }

        private int Part1()
        {
            string[] inputLines = File.ReadAllLines("day23input.txt");

            Dictionary<char, long> registers = new Dictionary<char, long>();
            long instructionPointer = 0;

            while (true)
            {
                instructionPointer = Process(inputLines[instructionPointer], registers, instructionPointer);
                if (instructionPointer < 0 || instructionPointer >= inputLines.Length)
                {
                    return _mulCount;
                }
            }
        }

        /// <summary>
        /// Coming up with this involved reverse-engineering what the program from the problem input
        /// was doing, and then just writing my own implementation accomplishing the same thing --
        /// namely, finding the count of non-prime values between b and c inclusive, checking only
        /// every 17th value.
        /// </summary>
        /// <returns>The fast.</returns>
        private int Part2Fast()
        {
            int b = 106700;
            int c = 123700;

            int solution = 0;
            while (b <= c)
            {
                if (!IsPrime(b))
                {
                    solution++;
                }
                b += 17;
            }
            return solution;
        }

        /// <summary>
        /// https://stackoverflow.com/a/15743231/12484
        /// </summary>
        private static bool IsPrime(int candidate)
        {
            // Test whether the parameter is a prime number.
            if ((candidate & 1) == 0)
            {
                if (candidate == 2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            // Note:
            // ... This version was changed to test the square.
            // ... Original version tested against the square root.
            // ... Also we exclude 1 at the end.
            for (int i = 3; (i * i) <= candidate; i += 2)
            {
                if ((candidate % i) == 0)
                {
                    return false;
                }
            }
            return candidate != 1;
        }

        private long Process(string inputLine, Dictionary<char, long> registers, long instructionPointer)
        {
            Instruction instruction = new Instruction(inputLine);
            switch (instruction.Operation)
            {
                case "set":
                    registers[instruction.ArgRegister[0]] = instruction.GetArgumentValue(1, registers);
                    break;

                case "sub":
                    registers[instruction.ArgRegister[0]] = GetExistingOrNewRegister(instruction.ArgRegister[0], registers) - instruction.GetArgumentValue(1, registers);
                    break;

                case "mul":
                    registers[instruction.ArgRegister[0]] = GetExistingOrNewRegister(instruction.ArgRegister[0], registers) * instruction.GetArgumentValue(1, registers);
                    _mulCount++;
                    break;

                case "jnz":
                    if (instruction.GetArgumentValue(0, registers) != 0)
                    {
                        long nextInstruction = instructionPointer + instruction.GetArgumentValue(1, registers);
                        return nextInstruction;
                    }
                    break;
            }
            return (instructionPointer + 1);
        }

        /// <summary>
        /// Writes the present value of all registers. (I previously had calls to this in various places in the
        /// Process() method to help me figure out what the program from the problem input was doing.)
        /// </summary>
        private void WriteState(Dictionary<char, long> registers)
        {
            foreach (char c in registers.Keys)
            {
                Console.Write(c + ":" + registers[c] + " ");
            }
            Console.WriteLine();
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
                return Day23.GetExistingOrNewRegister(ArgRegister[argumentNumber], registers);
            }
        }
    }
}
