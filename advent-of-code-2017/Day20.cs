using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace advent_of_code_2017
{
    /// <summary>
    /// Solves: http://adventofcode.com/2017/day/20 (Particle Swarm)
    /// </summary>
    public class Day20
    {
        public void Run()
        {
            string[] inputLines = File.ReadAllLines("day20input.txt");

            List<Particle> particles = new List<Particle>();
            for (int i = 0; i < inputLines.Length; i++)
            {
                particles.Add(new Particle(inputLines[i], i));
            }

            Console.WriteLine("Part 1 solution: " + Part1(particles));
            Console.WriteLine("Part 2 solution: " + Part2(particles));
        }

        /// <summary>
        /// I think the particle that will stay closest to 0,0,0 in the long
        /// term is simply the one with the slowest total acceleration...?
        /// </summary>
        /// <returns>The part1.</returns>
        private int Part1(List<Particle> particles)
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

        private int Part2(List<Particle> particles)
        {
            // Knowing when "all collisions are resolved" seems kind of tricky.
            // I'm just going to see if I can cheat by using a pretty high iteration count...
            for (int i = 0; i < 10000; i++)
            {
                RemoveCollided(particles);
                TickAll(particles);
            }

            return particles.Count;
        }

        private void RemoveCollided(List<Particle> particles)
        {
            int i = 0;
            while (i < particles.Count)
            {
                bool collisionFound = false;
                int j = i + 1;
                while (j < particles.Count)
                {
                    if (particles[i].Position.SequenceEqual(particles[j].Position))
                    {
                        particles.RemoveAt(j);
                        collisionFound = true;
                    }
                    else
                    {
                        j++;
                    }
                }

                if (collisionFound)
                {
                    particles.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
        }

        private void TickAll(List<Particle> particles)
        {
            foreach (Particle particle in particles)
            {
                Tick(particle);
            }
        }

        private void Tick(Particle particle)
        {
            for (int i = 0; i < 3; i++)
            {
                particle.Velocity[i] += particle.Acceleration[i];
                particle.Position[i] += particle.Velocity[i];
            }
        }
    }

    class Particle
    {
        public int ID { get; set; }

        // Array indices: x==0, y==1, z==2
        public long[] Position { get; set; }
        public long[] Velocity { get; set; }
        public long[] Acceleration { get; set; }

        public Particle(string inputLine, int id)
        {
            ID = id;

            Position = new long[3];
            Velocity = new long[3];
            Acceleration = new long[3];

            string[] inputs = inputLine.Split(new char[] { 'p', 'v', 'a', '=', '<', '>', ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            Position[0] = Int64.Parse(inputs[0]);
            Position[1] = Int64.Parse(inputs[1]);
            Position[2] = Int64.Parse(inputs[2]);
            Velocity[0] = Int64.Parse(inputs[3]);
            Velocity[1] = Int64.Parse(inputs[4]);
            Velocity[2] = Int64.Parse(inputs[5]);
            Acceleration[0] = Int64.Parse(inputs[6]);
            Acceleration[1] = Int64.Parse(inputs[7]);
            Acceleration[2] = Int64.Parse(inputs[8]);
        }

        public long TotalAcceleration()
        {
            long totalAcceleration = Math.Abs(Acceleration[0]);
            totalAcceleration += Math.Abs(Acceleration[1]);
            totalAcceleration += Math.Abs(Acceleration[2]);
            return totalAcceleration;
        }
    }
}
