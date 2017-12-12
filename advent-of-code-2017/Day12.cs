using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace advent_of_code_2017
{
    /// <summary>
    /// Solves: http://adventofcode.com/2017/day/12
    /// </summary>
    public class Day12
    {
        public void Run()
        {
            List<Node> graph = BuildGraph();

            Node nodeZero = graph.Single(n => n.Id == 0);
            Traverse(nodeZero);
            int reachableFromNodeZeroCount = graph.Count(n => n.Explored);
            Console.WriteLine("Part 1 solution: " + reachableFromNodeZeroCount);

            int groupCount = CountRemainingGroups(graph);
            Console.WriteLine("Part 2 solution: " + groupCount);
        }

        private List<Node> BuildGraph()
        {
            List<Node> graph = new List<Node>();
            string[] lines = File.ReadAllLines("day12input.txt");
            foreach (string line in lines)
            {
                string[] tokens = line.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

                int id = Int32.Parse(tokens[0]);
                Node node = GetExistingOrNewNode(id, graph);

                for (int i = 2; i < tokens.Length; i++)
                {
                    id = Int32.Parse(tokens[i]);
                    Node neighbor = GetExistingOrNewNode(id, graph);
                    node.Pipes.Add(neighbor);
                }
            }
            return graph;
        }

        /// <summary>
        /// Flag this node as having been explored; then, recursively do the same for 
        /// each of its neighbors which haven't already been explored.
        /// </summary>
        private void Traverse(Node node)
        {
            node.Explored = true;
            foreach (Node neighbor in node.Pipes)
            {
                if (!neighbor.Explored)
                {
                    Traverse(neighbor);
                }
            }
        }

        /// <summary>
        /// If there's an existing Node in the graph with the specified ID, returns it.
        /// Otherwise, creates a new Node with that name, adds it to the graph, and returns that.
        /// </summary>
        private Node GetExistingOrNewNode(int id, List<Node> graph)
        {
            Node node = graph.SingleOrDefault(n => n.Id == id);
            if (node == null)
            {
                node = new Node { Id = id, Pipes = new List<Node>(), Explored = false };
                graph.Add(node);
            }
            return node;
        }

        private int CountRemainingGroups(List<Node> graph)
        {
            int groupCount = 1; // We already explored the group with node ID 0
            foreach (Node node in graph)
            {
                if (!node.Explored)
                {
                    groupCount++;
                    Traverse(node);
                }
            }
            return groupCount;
        }
    }

    class Node
    {
        public int Id { get; set; }
        public List<Node> Pipes { get; set; }
        public bool Explored { get; set; }
    }
}
