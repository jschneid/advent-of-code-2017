using System;
using System.IO;
using System.Linq;

namespace advent_of_code_2017
{
    /// <summary>
    /// Solves: http://adventofcode.com/2017/day/20
    /// </summary>
    public class Day20
    {
        public void Run()
        {
            string[] inputLines = File.ReadAllLines("day20input.txt");

            Particle[] particles = new Particle[inputLines.Length];
            for (int i = 0; i < inputLines.Length; i++)
            {
                particles[i] = new Particle(inputLines[i], i);
            }

            Console.WriteLine("Part 1 solution: " + Part1(particles));


        }

        /// <summary>
        /// I think the particle that will stay closest to 0,0,0 in the long
        /// term is simply the one with the slowest total acceleration...?
        /// </summary>
        /// <returns>The part1.</returns>
        private int Part1(Particle[] particles)
        {
            Particle slowest = particles[0];
            foreach (Particle particle in particles)
            {
                if (particle.TotalAcceleration() < slowest.TotalAcceleration())
                {
                    slowest = particle;
                }
            }
            return slowest.ID;
        }
    }

    class Particle
    {
        public int ID { get; set; }

        // Array indices: x==0, y==1, z==2
        public int[] Position { get; set; }
        public int[] Velocity { get; set; }
        public int[] Acceleration { get; set; }

        public Particle(string inputLine, int id)
        {
            ID = id;

            Position = new int[3];
            Velocity = new int[3];
            Acceleration = new int[3];

            string[] inputs = inputLine.Split(new char[] { 'p', 'v', 'a', '=', '<', '>', ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            Position[0] = Int32.Parse(inputs[0]);
            Position[1] = Int32.Parse(inputs[1]);
            Position[2] = Int32.Parse(inputs[2]);
            Velocity[0] = Int32.Parse(inputs[3]);
            Velocity[1] = Int32.Parse(inputs[4]);
            Velocity[2] = Int32.Parse(inputs[5]);
            Acceleration[0] = Int32.Parse(inputs[6]);
            Acceleration[1] = Int32.Parse(inputs[7]);
            Acceleration[2] = Int32.Parse(inputs[8]);
        }

        public int TotalAcceleration()
        {
            int totalAcceleration = Math.Abs(Acceleration[0]);
            totalAcceleration += Math.Abs(Acceleration[1]);
            totalAcceleration += Math.Abs(Acceleration[2]);
            return totalAcceleration;
        }
    }
}
