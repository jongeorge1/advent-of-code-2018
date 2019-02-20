namespace AoC2018.Solutions.Day13
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            string[] inputLines = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            int yOffset = inputLines[0].Length;
            string map = string.Concat(inputLines);

            // Find the minecarts
            List<Minecart> minecarts = Minecart.FindInMap(ref map, yOffset);

            // We won't be able to just remove carts from the collection as we go,
            // so we keep a list of those that have been removed
            var removedCarts = new List<Minecart>();

            while (true)
            {
                // First, sort the minecarts by their position...
                minecarts = minecarts.OrderBy(x => x.Position).ToList();

                // Now move the ones that haven't crashed...
                foreach (Minecart current in minecarts)
                {
                    if (removedCarts.Contains(current))
                    {
                        continue;
                    }

                    current.Position += current.NextMove;
                    current.SetNextMove(yOffset, map[current.Position]);

                    // Has it crashed?
                    Minecart crashedIntoMinecart = current.GetCrashedIntoMinecart(minecarts.Where(x => !removedCarts.Contains(x)));

                    if (crashedIntoMinecart != null)
                    {
                        removedCarts.Add(current);
                        removedCarts.Add(crashedIntoMinecart);
                    }
                }

                // At the end of the tick - is there only one remaining?
                if (minecarts.Count - removedCarts.Count == 1)
                {
                    Minecart lastCart = minecarts.First(x => !removedCarts.Contains(x));
                    return $"{lastCart.Position % yOffset},{lastCart.Position / yOffset}";
                }
            }
        }
    }
}
