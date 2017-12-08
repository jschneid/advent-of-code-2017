using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
namespace advent_of_code_2017
{
    public class Day8
    {
        public void Run()
        {
            Dictionary<string, int> registers = new Dictionary<string, int>();

            string[] lines = File.ReadAllLines("day8input.txt");
            foreach (string line in lines)
            {
                string[] tokens = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                if (ShouldPerformOperation(tokens, registers))
                {
                    DoOperation(tokens, registers);       
                }
            }

            int maxRegisterValue = registers.Values.Max();
            Console.WriteLine("Part 1 Solution: " + maxRegisterValue);
        }

        private int GetRegisterValue(string name, Dictionary<string, int> registers)
        {
            if (!registers.ContainsKey(name))
            {
                registers[name] = 0;
            }
            return registers[name];
        }

        private bool ShouldPerformOperation(string[] tokens, Dictionary<string, int> registers)
        {
            int compareRegisterValue = GetRegisterValue(tokens[4], registers);
            int comparisonValue = Int32.Parse(tokens[6]);
            bool performOperation;
            switch (tokens[5])
            {
                case ">":
                    performOperation = compareRegisterValue > comparisonValue;
                    break;
                case "<":
                    performOperation = compareRegisterValue < comparisonValue;
                    break;
                case "==":
                    performOperation = compareRegisterValue == comparisonValue;
                    break;
                case "!=":
                    performOperation = compareRegisterValue != comparisonValue;
                    break;
                case ">=":
                    performOperation = compareRegisterValue >= comparisonValue;
                    break;
                case "<=":
                    performOperation = compareRegisterValue <= comparisonValue;
                    break;
                default:
                    throw new InvalidOperationException("Unrecognized operator token: " + tokens[5]);
            }
            return performOperation;
        }

        private void DoOperation(string[] tokens, Dictionary<string, int> registers)
        {
            int modificationValue = Int32.Parse(tokens[2]);
            int currentRegisterValue = GetRegisterValue(tokens[0], registers);
            switch (tokens[1])
            {
                case "inc":
                    registers[tokens[0]] = currentRegisterValue + modificationValue;
                    break;
                case "dec":
                    registers[tokens[0]] = currentRegisterValue - modificationValue;
                    break;
                default:
                    throw new InvalidOperationException("Unrecognized modifier token: " + tokens[1]);
            }
        }
    }
}
