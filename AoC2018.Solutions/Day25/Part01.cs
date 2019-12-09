namespace AoC2018.Solutions.Day25
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part01 : ISolution
    {
        public static int Distance((int X, int Y, int Z, int T) point1, (int X, int Y, int Z, int T) point2)
        {
            return Math.Abs(point1.X - point2.X)
                + Math.Abs(point1.Y - point2.Y)
                + Math.Abs(point1.Z - point2.Z)
                + Math.Abs(point1.T - point2.T);
        }

        public string Solve(string input)
        {
            (int X, int Y, int Z, int T)[] points = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split(',').Select(int.Parse).ToArray())
                .Select(y => (y[0], y[1], y[2], y[3]))
                .ToArray();

            // For each point, find all other points in range.
            var constellations = points.Select(x => points.Where(y => Distance(x, y) < 4).ToArray()).ToList();

            int constellationCount;

            do
            {
                constellationCount = constellations.Count;
                var processedConstellations = new List<(int X, int Y, int Z, int T)[]>();
                var mergedConstellations = new List<(int X, int Y, int Z, int T)[]>();

                foreach ((int X, int Y, int Z, int T)[] current in constellations)
                {
                    if (!processedConstellations.Contains(current))
                    {
                        // Find any constellations that contain points in the current constellation
                        IEnumerable<(int X, int Y, int Z, int T)[]> targetConstellations = constellations.Where(target => target.Intersect(current).Count() != 0);
                        IEnumerable<(int X, int Y, int Z, int T)> merged = current;
                        foreach ((int X, int Y, int Z, int T)[] currentTarget in targetConstellations)
                        {
                            merged = merged.Union(currentTarget);
                            processedConstellations.Add(currentTarget);
                        }

                        mergedConstellations.Add(merged.ToArray());
                    }
                }

                constellations = mergedConstellations;
            }
            while (constellationCount != constellations.Count);

            return constellations.Count.ToString();
        }
    }
}
