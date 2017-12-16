using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace advent_of_code_2017
{
    /// <summary>
    /// Solves http://adventofcode.com/2017/day/16
    /// </summary>
    public class Day16
    {
        public void Run()
        {
            string inputText = File.ReadAllText("day16input.txt");
            string[] inputs = inputText.Split(new char[] { ',' });

            List<char> programs = new List<char>();
            for (char c = 'a'; c <= 'p'; c++)
            {
                programs.Add(c);
            }
            
            // The shortcut I took here was to initially run the Dance lots of times -- and have the program report
            // when the resulting sequence was the *same as the initial input*, forming a no-op loop of that number
            // of dances. For my input, that number turned out to be 42.  
            //
            // Then, I just needed to change the program to run (1000000000 % 42) times -- that is, 34 -- and report
            // the result.
            for (int i = 0; i < 34; i++)
            {
                Dance(inputs, programs);
            }

            Console.WriteLine("Part 2 solution: " + ProgramsString(programs));
        }

        private string ProgramsString(List<char> programs)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in programs)
            {
                sb.Append(c);
            }
            return sb.ToString();
        }

        private void Dance(string[] inputs, List<char>programs)
        { 
            foreach (string input in inputs)
            {
                char c;
                int index0;
                int index1;
                string[] split;
                switch (input[0])
                {
                    case 's':
                        int spinCount = Int32.Parse(input.Substring(1));
                        for (int i = 0; i < spinCount; i++)
                        {
                            c = programs[15];
                            programs.RemoveAt(15);
                            programs.Insert(0, c);
                        }
                        break;

                    case 'x':
                        split = input.Substring(1).Split(new char[] { '/' });
                        index0 = Int32.Parse(split[0]);
                        index1 = Int32.Parse(split[1]);
                        c = programs[index0];
                        programs[index0] = programs[index1];
                        programs[index1] = c;
                        break;

                    case 'p':
                        split = input.Substring(1).Split(new char[] { '/' });
                        index0 = programs.IndexOf(split[0][0]);
                        index1 = programs.IndexOf(split[1][0]);
                        c = programs[index0];
                        programs[index0] = programs[index1];
                        programs[index1] = c;
                        break;
                }
            }
        }
    }
}
