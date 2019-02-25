namespace AoC2018.Solutions.Day17
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    public static class Parser
    {
        public static Point[] Parse(string input)
        {
            IEnumerable<Point> clayPoints = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).SelectMany(ParseRow);
            return clayPoints.ToArray();
        }

        public static IEnumerable<Point> ParseRow(string input)
        {
            string[] components = input.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            int[] xRange;
            int[] yRange;

            if (components[0][0] == 'x')
            {
                xRange = ParseCoordinate(components[0].Substring(2));
                yRange = ParseCoordinate(components[1].Substring(2));
            }
            else
            {
                xRange = ParseCoordinate(components[1].Substring(2));
                yRange = ParseCoordinate(components[0].Substring(2));
            }

            foreach (int x in xRange)
            {
                foreach (int y in yRange)
                {
                    yield return new Point(x, y);
                }
            }
        }

        public static int[] ParseCoordinate(string input)
        {
            int[] components = input.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();

            if (components.Length == 1)
            {
                return new[] { components[0] };
            }

            return Enumerable.Range(components[0], components[1] - components[0] + 1).ToArray();
        }
    }
}
