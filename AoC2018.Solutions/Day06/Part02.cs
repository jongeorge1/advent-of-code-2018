namespace AoC2018.Solutions.Day06
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string input) => this.Solve(input, 10000);

        public string Solve(string input, int targetDistance)
        {
            var points = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(PointBuilder.FromString).ToList();

            // Find the extent of the board that contains points
            var topLeft = new Point
            {
                X = points.Min(x => x.X),
                Y = points.Min(x => x.Y),
            };

            var bottomRight = new Point
            {
                X = points.Max(x => x.X),
                Y = points.Max(x => x.Y),
            };

            int pointsInRange = 0;

            // Theory: if we find the grid that contains all the points,
            // then calculate the ownership of every cell in the grid,
            // then assume any areas that touch the edge will be infinite, so we
            // should discount them, then we should get the answer.
            foreach (Point gridLocation in topLeft.BuildRangeTo(bottomRight))
            {
                if (points.Sum(x => x.ManhattanDistanceFrom(gridLocation)) < targetDistance)
                {
                    pointsInRange++;
                }
            }

            return pointsInRange.ToString();
        }
    }
}
