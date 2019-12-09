namespace AoC2018.Solutions.Day23
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Priority_Queue;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            var bots = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => new Nanobot(x))
                .ToList();

            (int MinX, int MaxX, int MinY, int MaxY, int MinZ, int MaxZ) bounds =
                (bots.Min(bot => bot.Position.X),
                bots.Max(bot => bot.Position.X),
                bots.Min(bot => bot.Position.Y),
                bots.Max(bot => bot.Position.Y),
                bots.Min(bot => bot.Position.Z),
                bots.Max(bot => bot.Position.Z));

            // Now we're going to try and align the bounds on numbers that are powers of 2. This will make repeatedly splitting
            // the area into smaller boxes a lot easier.

            int xRange = bounds.MaxX - bounds.MinX;
            int yRange = bounds.MaxY - bounds.MinY;
            int zRange = bounds.MaxZ - bounds.MinZ;

            int largestRange = Math.Max(Math.Max(xRange, yRange), zRange);
            int boxSize = 2;
            while (boxSize < largestRange)
            {
                boxSize *= 2;
            }

            // Now create the first box based on that
            var box = new Box
            {
                Corner1 = (bounds.MinX, bounds.MinY, bounds.MinZ),
                Corner2 = (bounds.MinX + boxSize - 1, bounds.MinY + boxSize - 1, bounds.MinZ + boxSize - 1),
                NanobotsInRange = bots.Count,
            };

            var boxQueue = new SimplePriorityQueue<Box>();

            // We're using the nanobots in range metric to order the priority queue. We have negate it
            // because we want the highest first. This would likely not work for all inputs - if there
            // were multiple points with the same number of nanobots in range we'd need to deal with that.
            // We'd likely do that by implementing a custom comparer that sorted on nanobots, then on box size
            // (largest first) and on distance from the origin (closest first).
            boxQueue.Enqueue(box, -box.NanobotsInRange);

            while (boxQueue.Count > 0)
            {
                Box currentBox = boxQueue.Dequeue();

                if (currentBox.EdgeLength == 1)
                {
                    return ManhattanDistance(0, 0, 0, currentBox.Corner1.X, currentBox.Corner1.Y, currentBox.Corner1.Z).ToString();
                }

                Box[] decomposedBoxes = currentBox.Decompose().ToArray();
                foreach (var current in decomposedBoxes)
                {
                    current.NanobotsInRange = bots.Count(x => x.RangeIntersectsBox(current));
                    boxQueue.Enqueue(current, -current.NanobotsInRange);
                }
            }

            return string.Empty;
        }

        private static int ManhattanDistance(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            return Math.Abs(x1 - x2)
                + Math.Abs(y1 - y2)
                + Math.Abs(z1 - z2);
        }
    }
}
