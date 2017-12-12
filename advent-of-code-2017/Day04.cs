using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent_of_code_2017
{
    /// <summary>
    /// Solves: http://adventofcode.com/2017/day/4
    /// </summary>
    public class Day04
    {
        public void Run()
        {
            string[] lines = File.ReadAllLines("day04input.txt");

            int validPassphrasesCountPart1 = 0;
            int validPassphrasesCountPart2 = 0;
            foreach (string line in lines)
            {
                if (IsValidPassphrase_Unique(line))
                {
                    validPassphrasesCountPart1++;
                }    

                if (IsValidPassphrase_Anagrams(line))
                {
                    validPassphrasesCountPart2++;
                }
            }

            Console.Out.WriteLine("Part 1 solution: " + validPassphrasesCountPart1);
            Console.Out.WriteLine("Part 2 solution: " + validPassphrasesCountPart2);
        }

        private bool IsValidPassphrase_Unique(string phrase)
        {
            string[] words = phrase.Split(new char[0]);
            return words.Distinct().Count() == words.Length;
        }
    
        private bool IsValidPassphrase_Anagrams(string phrase)
        {
            // Sort the letters in each word alphabetically. If any of the words are anagrams
            // of one another, their sorted verions will end up being the same word.
            // For example, "stop" and "post" both sort to "opst".
            string[] words = phrase.Split(new char[0]);
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = String.Concat(words[i].OrderBy(c => c));
            }
            return words.Distinct().Count() == words.Length;
        }
    }
}
