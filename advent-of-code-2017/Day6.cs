using System;
using System.Linq;
using System.Collections.Generic;
namespace advent_of_code_2017
{
    /// <summary>
    /// Solves: http://adventofcode.com/2017/day/6
    /// </summary>
    public class Day6
    {
        public void Run()
        {
            int[] banks = new int[] { 14, 0, 15, 12, 11, 11, 3, 5, 1, 6, 8, 4, 9, 1, 8, 4 };
            int cycles = Part1(banks);
            Console.WriteLine("Part 1 Solution: " + cycles);

            int[] banks2 = new int[] { 14, 0, 15, 12, 11, 11, 3, 5, 1, 6, 8, 4, 9, 1, 8, 4 };
            int frequency = Part2(banks2);
            Console.WriteLine("Part 2 Solution: " + frequency);
        }

        private int Part1(int[] banks)
        {
            int cycles = 0;
            HashSet<BanksState> statesSeen = new HashSet<BanksState>();
            BanksState initialState = new BanksState(banks);
            statesSeen.Add(initialState);

            while (true)
            {
                DoRedistribution(banks);
                cycles++;

                BanksState currentState = new BanksState(banks);
                if (statesSeen.Contains(currentState))
                {
                    return cycles;
                }

                statesSeen.Add(currentState);
            }                
        }

        private int Part2(int[] banks)
        {
            int cycles = 0;
            Dictionary<BanksState, int> statesSeen = new Dictionary<BanksState, int>();
            BanksState initialState = new BanksState(banks);
            statesSeen.Add(initialState, 0);

            while (true)
            {
                DoRedistribution(banks);
                cycles++;

                BanksState currentState = new BanksState(banks);
                if (statesSeen.Keys.Contains(currentState))
                {
                    return cycles - statesSeen[currentState];
                }

                statesSeen.Add(currentState, cycles);
            }
        }

        private void DoRedistribution(int[] banks)
        {
            int largestBank = FindLargestBank(banks);

            int redistributions = banks[largestBank];
            banks[largestBank] = 0;

            int pointer = (largestBank + 1) % banks.Length;
            for (int i = 0; i < redistributions; i++)
            {
                banks[pointer]++;
                pointer = (pointer + 1) % banks.Length;
            }
        }

        /// <summary>
        /// Returns the index of the largest item in the array, with ties being
        /// broken by the lowest index.
        /// </summary>
        private int FindLargestBank(int[] banks)
        {
            int largestIndex = 0;
            for (int i = 1; i < banks.Length; i++) 
            {
                if (banks[i] > banks[largestIndex])
                {
                    largestIndex = i;
                }
            }
            return largestIndex;
        }
    }

    /// <summary>
    /// This class is essentially just a wrapper around int[] which provides value
    /// equality instead of reference equality.
    /// </summary>
    class BanksState
    {
        public int[] Values { get; set; }

        public BanksState(int[] values)
        {
            this.Values = (int[])values.Clone();
        }

        public override bool Equals(object obj)
        {
            // All of this safety checking is overkill for this simple program, but it's
            // good practice (and habit!), so I included it.
            if (!(obj is BanksState))
            {
                return false;
            }
            BanksState other = (BanksState)obj;
            if (other.Values.Length != this.Values.Length)
            {
                return false;
            }

            for (int i = 0; i < this.Values.Length; i++)
            {
                if (this.Values[i] != other.Values[i])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// This is required for HashSet.Contains() calls on instances of this class to 
        /// work properly. Implementation reference: https://stackoverflow.com/a/263416/12484
        /// </summary>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                for (int i = 0; i < Values.Length; i++)
                {
                    hash = hash * 23 + Values[i];
                }
                return hash;
            }
        }
    }
}
