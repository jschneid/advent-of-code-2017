using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace advent_of_code_2017
{
    /// <summary>
    /// Solves http://adventofcode.com/2017/day/24 ("Electromagnetic Moat")
    /// </summary>
    public class Day24
    {
        public void Run()
        {
            Console.WriteLine("Part 1 solution: " + Part1());
            
        }

        private int Part1()
        {
            List<Component> availableComponents = ParseInput();
            Bridge bridge = new Bridge();

            return Build(bridge, availableComponents);
        }

        private List<Component> ParseInput()
        {
            string[] inputLines = File.ReadAllLines("day24input.txt");

            List<Component> availableComponents = new List<Component>();

            foreach (string input in inputLines)
            {
                string[] stringValues = input.Split(new char[] { '/' });
                int pins0 = Int32.Parse(stringValues[0]);
                int pins1 = Int32.Parse(stringValues[1]);
                Component component = new Component(pins0, pins1);
                availableComponents.Add(component);
            }

            return availableComponents;
        }

        private List<Component> GetPossibleNextComponents(Bridge bridge, List<Component> components)
        {
            IEnumerable<Component> unusedComponents = new List<Component>();
            unusedComponents = components.Where(c => bridge.Contains(c) == false);

            List<Component> possibleNextComponents = possibleNextComponents = unusedComponents.Where(c => c.Pins[0] == bridge.EndPins() || c.Pins[1] == bridge.EndPins()).ToList();

            return possibleNextComponents;
        }

        private int Build(Bridge bridge, List<Component> components)
        {
            List<Component> possibleNextComponents = GetPossibleNextComponents(bridge, components);
            if (possibleNextComponents.Count == 0)
            {
                return bridge.Strength();
            }

            int maxStrength = 0;
            foreach (Component component in possibleNextComponents)
            {
                bridge.Add(component);

                int strength = Build(bridge, components);
                if (strength > maxStrength)
                {
                    maxStrength = strength;
                }

                bridge.RemoveLast();
            }

            return maxStrength;
        }
    }

    class Component
    {
        public List<int> Pins { get; }

        public Component(int pins0, int pins1)
        {
            Pins = new List<int>();
            Pins.Add(pins0);
            Pins.Add(pins1);
        }

        public int Strength()
        {
            return Pins[0] + Pins[1];
        }
    }

    class Bridge
    {
        private List<Component> _components;
        private List<bool> _reversed;

        public Bridge()
        {
            _components = new List<Component>();
            _reversed = new List<bool>();
        }

        public void Add(Component component)
        {
            this._reversed.Add(this.EndPins() == component.Pins[1]);
            this._components.Add(component);
        }

        public void RemoveLast()
        {
            this._components.RemoveAt(this._components.Count - 1);
            this._reversed.RemoveAt(this._reversed.Count - 1);
        }

        public bool Contains(Component component)
        {
            return _components.Contains(component);
        }

        public int EndPins()
        {
            if (_components.Count == 0)
            {
                return 0;
            }
            if (_reversed.Last() == false)
            {
                return _components.Last().Pins[1];
            }
            else
            {
                return _components.Last().Pins[0];
            }
        }

        public int Strength()
        {
            return _components.Sum(c => c.Strength());
        }
    }
}
