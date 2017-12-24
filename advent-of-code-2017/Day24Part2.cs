using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace advent_of_code_2017
{
    /// <summary>
    /// Solves http://adventofcode.com/2017/day/24 ("Electromagnetic Moat") Part 2
    /// </summary>
    public class Day24Part2
    {
        public void Run()
        {
            Console.WriteLine("Part 2 solution: " + Part2());
        }

        private int Part2()
        {
            List<Component> availableComponents = ParseInput();
            Bridge bridge = new Bridge();
            List<Tuple<int, int>> lengthsAndStrengths = new List<Tuple<int, int>>();

            Build(bridge, availableComponents, lengthsAndStrengths);

            return GetLongestStrongest(lengthsAndStrengths);
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

        private void Build(Bridge bridge, List<Component> components, List<Tuple<int, int>> lengthsAndStrengths)
        {
            List<Component> possibleNextComponents = GetPossibleNextComponents(bridge, components);
            if (possibleNextComponents.Count == 0)
            {
                Tuple<int, int> lengthAndStrength = new Tuple<int, int>(bridge.Length(), bridge.Strength());
                lengthsAndStrengths.Add(lengthAndStrength);
            }

            foreach (Component component in possibleNextComponents)
            {
                bridge.Add(component);
                Build(bridge, components, lengthsAndStrengths);
                bridge.RemoveLast();
            }
        }

        private int GetLongestStrongest(List<Tuple<int, int>> lengthsAndStrengths)
        {
            int maxLength = lengthsAndStrengths.Max(lengthAndStrength => lengthAndStrength.Item1);
            List<Tuple<int, int>> longest = lengthsAndStrengths.Where(lengthAndStrength => lengthAndStrength.Item1 == maxLength).ToList();
            int maxStrength = longest.Max(lengthAndStrength => lengthAndStrength.Item2);
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

        public int Length()
        {
            return _components.Count();
        }
    }
}
