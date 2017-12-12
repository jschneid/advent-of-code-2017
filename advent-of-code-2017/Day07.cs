using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
namespace advent_of_code_2017
{
    public class Day07
    {
        public void Run()
        {
            List<TowerItem> towerItems = ProcessInput();

            TowerItem rootItem = towerItems.Where(ti => ti.Parent == null).Single();
            Console.WriteLine("Part 1 solution: " + rootItem.Name);

            TraverseAssignDescendantsWeights(rootItem);
            int correctedWeightForIncorrectItem = TraverseFindUnbalancedNodeProperWeight(rootItem);
            Console.WriteLine("Part 2 solution: " + correctedWeightForIncorrectItem);
        }

        private List<TowerItem> ProcessInput()
        {
            List<TowerItem> towerItems = new List<TowerItem>();

            string[] lines = File.ReadAllLines("day07input.txt");
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

        /// <summary>
        /// If there's an existing TowerItem in the list with the specified name, returns it.
        /// Otherwise, creates a new TowerItem with that name, adds it to the list, and returns that.
        /// </summary>
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

        private int TraverseAssignDescendantsWeights(TowerItem towerItem)
        {
            if (towerItem.Children == null)
            {
                towerItem.DescendantsWeight = 0;
                return towerItem.Weight;
            }

            int totalDescendantsWeights = 0;
            foreach (TowerItem child in towerItem.Children)
            {
                totalDescendantsWeights += TraverseAssignDescendantsWeights(child);
            }

            towerItem.DescendantsWeight = totalDescendantsWeights;
            return totalDescendantsWeights + towerItem.Weight;
        }

        private int TraverseFindUnbalancedNodeProperWeight(TowerItem towerItem)
        {
            if (towerItem.Children == null)
            {
                return -1;
            }

            // See if any of our children's towers are unbalanced
            foreach (TowerItem child in towerItem.Children)
            {
                int childResult = TraverseFindUnbalancedNodeProperWeight(child);
                if (childResult != -1)
                {
                    return childResult;
                }
            }

            // They aren't; so now see if the towers starting from this item are unbalanced
            if (towerItem.Children.Select(childItem => childItem.DescendantsWeight + childItem.Weight).Distinct().Count() > 1)
            {
                return CalculateWeightCorrection(towerItem);
            }

            return -1;
        }

        private int CalculateWeightCorrection(TowerItem towerItem)
        {
            TowerItem childWithIncorrectWeight = FindChildWithUniqueWeight(towerItem.Children);
            int correctChildTotalWeight;
            if (towerItem.Children[0] == childWithIncorrectWeight)
            {
                correctChildTotalWeight = towerItem.Children[1].Weight + towerItem.Children[1].DescendantsWeight;
            }
            else
            {
                correctChildTotalWeight = towerItem.Children[0].Weight + towerItem.Children[0].DescendantsWeight;
            }

            int correctedWeightForIncorrectChild = correctChildTotalWeight - childWithIncorrectWeight.DescendantsWeight;
            return correctedWeightForIncorrectChild;
        }

        private TowerItem FindChildWithUniqueWeight(List<TowerItem> childItems)
        {
            Dictionary<int, int> weightToCountMap = new Dictionary<int, int>();
            for (int i = 0; i < childItems.Count; i++)
            {
                if (!weightToCountMap.ContainsKey(childItems[i].Weight + childItems[i].DescendantsWeight))
                {
                    weightToCountMap[childItems[i].Weight + childItems[i].DescendantsWeight] = 0;
                }
                weightToCountMap[childItems[i].Weight + childItems[i].DescendantsWeight]++;
            }

            int uniqueWeight = weightToCountMap.Single(wtcm => wtcm.Value == 1).Key;

            TowerItem childWithUniqueWeight = childItems.Where(ci => ci.Weight + ci.DescendantsWeight == uniqueWeight).Single();
            return childWithUniqueWeight;
        }
    }

    class TowerItem
    {
        public string Name { get; set; }
        public int Weight { get; set; }
        public List<TowerItem> Children { get; set; }
        public TowerItem Parent { get; set; }
        public int DescendantsWeight { get; set; }
    }
}