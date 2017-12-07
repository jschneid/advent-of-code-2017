using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
namespace advent_of_code_2017
{
    public class Day7
    {
        public void Run()
        {
            List<TowerItem> towerItems = ProcessInput();

            TowerItem rootItem = towerItems.Where(ti => ti.Parent == null).Single();
            Console.WriteLine("Part 1 solution: " + rootItem.Name);
        }

        private List<TowerItem> ProcessInput()
        {
            List<TowerItem> towerItems = new List<TowerItem>();

            string[] lines = File.ReadAllLines("day7input.txt");
            foreach (string line in lines)
            {
                string name = GetName(line);
                TowerItem towerItem = GetExistingOrNewTowerItem(name, towerItems);
                towerItem.Weight = GetWeight(line);
                WireUpRelationships(line, towerItem, towerItems);
            }

            return towerItems;
        }

        private string GetName(string inputLine)
        {
            string name = inputLine.Split()[0];
            return name;
        }

        private int GetWeight(string inputLine)
        {
            string weightInParens = inputLine.Split()[1];
            string weightString = weightInParens.Substring(1, weightInParens.Length - 2);
            int weight = Int32.Parse(weightString);
            return weight;
        }

        private void WireUpRelationships(string inputLine, TowerItem towerItem, List<TowerItem> towerItems)
        {
            string[] childNames = GetTowerItemNames(inputLine);
            if (childNames == null)
            {
                return;
            }
            foreach (string childName in childNames)
            {
                TowerItem childItem = GetExistingOrNewTowerItem(childName, towerItems);
                towerItem.Children.Add(childItem);
                childItem.Parent = towerItem;
            }
        }

        private TowerItem GetExistingOrNewTowerItem(string name, List<TowerItem> towerItems)
        {
            TowerItem towerItem = towerItems.SingleOrDefault(ti => ti.Name == name);
            if (towerItem == null)
            {
                towerItem = new TowerItem { Name = name, Children = new List<TowerItem>() };
                towerItems.Add(towerItem);
            }
            return towerItem;
        }

        private string[] GetTowerItemNames(string inputLine)
        {
            int startIndex = inputLine.IndexOf('>');
            if (startIndex == -1)
            {
                return null;
            }
            string towerItemNamesCommaSpaceSeparated = inputLine.Substring(startIndex + 2);
            string[] names = towerItemNamesCommaSpaceSeparated.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            return names;
        }
    }

    class TowerItem
    {
        public string Name { get; set; }
        public int Weight { get; set; }
        public List<TowerItem> Children { get; set; }
        public TowerItem Parent { get; set; }
    }
}