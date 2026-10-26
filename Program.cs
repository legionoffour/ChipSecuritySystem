using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChipSecuritySystem
{
    class Program
    {
        private static List<ColorChip> allChips;
        private static List<ColorChip> bestPath;

        static void Main(string[] args)
        {
            // Readme example
            var bag1 = new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Yellow),
                new ColorChip(Color.Red, Color.Green),
                new ColorChip(Color.Yellow, Color.Red),
                new ColorChip(Color.Orange, Color.Purple)
            };

            // Should return error message
            var bag2 = new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Yellow),
                new ColorChip(Color.Orange, Color.Purple)
            };

            // Should have the longest chain (shortest path is 1 chip, longest is 5)
            var bag3 = new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Green),     // the 1 chip shortcut
                new ColorChip(Color.Blue, Color.Purple),
                new ColorChip(Color.Purple, Color.Orange),
                new ColorChip(Color.Orange, Color.Blue),
                new ColorChip(Color.Blue, Color.Yellow),
                new ColorChip(Color.Yellow, Color.Green)
            };

            Console.WriteLine("Bag 1");
            RunChipAlgorithm(bag1);
            Console.WriteLine("Bag 2");
            RunChipAlgorithm(bag2);
            Console.WriteLine("Bag 3");
            RunChipAlgorithm(bag3);
        }

        private static void RunChipAlgorithm(List<ColorChip> bag)
        {
            allChips = bag;
            bestPath = null;

            Search(Color.Blue, new List<ColorChip>());

            if (bestPath == null)
            {
                Console.WriteLine(Constants.ErrorMessage);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Longest chain of color chips from Blue to Green:");
                Console.WriteLine(string.Join(" -> ", bestPath)); // string.Join calls ColorChip.ToString() on each chip.
                Console.WriteLine("Chips used: " + bestPath.Count);
                Console.WriteLine();
            }
        }

        // Recursive search function to find the longest path starting with blue and ending with green
        private static void Search(Color currentColor, List<ColorChip> linkPath)
        {
            if (currentColor == Color.Green && (bestPath == null || linkPath.Count > bestPath.Count))
            {
                bestPath = linkPath; // update bestPath if a longer path is found
            }

            foreach (var chip in allChips)
            {
                // Each chip can only be used once
                if (!linkPath.Contains(chip) && chip.StartColor == currentColor)
                {
                    // Copy so deeper calls can't change the list this loop uses to try its next chip
                    var extendedPath = new List<ColorChip>(linkPath);
                    extendedPath.Add(chip);
                    Search(chip.EndColor, extendedPath);
                }
            }
        }
    }
}
