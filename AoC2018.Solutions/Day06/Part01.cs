namespace AoC2018.Solutions.Day06
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string input)
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

            var ownership = points.ToDictionary(x => x, _ => new List<Point>());

            // Theory: if we find the grid that contains all the points,
            // then calculate the ownership of every cell in the grid,
            // then assume any areas that touch the edge will be infinite, so we
            // should discount them, then we should get the answer.
            foreach (Point gridLocation in topLeft.BuildRangeTo(bottomRight))
            {
                Point[] closestPoints = gridLocation.FindClosest(points);

                if (closestPoints.Length == 1)
                {
                    ownership[closestPoints[0]].Add(gridLocation);
                }
            }

            // Now get all the points whose areas don't touch the edge of the grid.
            var pointsWithBoundedAreas = ownership.Where(x => x.Value.All(p => p.X != topLeft.X && p.X != bottomRight.X && p.Y != topLeft.Y && p.Y != bottomRight.Y)).Select(x => x.Key).ToList();

            // Now work out the size of the area for each point
            IEnumerable<int> areaSizes = pointsWithBoundedAreas.Select(x => ownership[x].Count);
            int largestAreaSize = areaSizes.Max();

            return largestAreaSize.ToString();
        }
    }
}
