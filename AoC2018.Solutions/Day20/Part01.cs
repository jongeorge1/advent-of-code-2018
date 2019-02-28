namespace AoC2018.Solutions.Day20
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            Dictionary<Point, Doors> map = Mapper.Map(input);

            // We need to find the furthest room, defined as the room
            // for which the shortest path takes you through the most doors.
            // I see two options:
            // 1. Work out all the paths from the origin that end up at a dead end
            // 2. For each room, work out the shortest path.
            // I think the best approach is 1; we'll do a breadth-first search from
            // the origin and see what that gives us.
            var location = new Point(0, 0);
            var paths = new List<List<Point>>();
            var searchQueue = new Queue<List<Point>>();
            searchQueue.Enqueue(new List<Point> { location });

            while (searchQueue.Count != 0)
            {
                // Where are we
                List<Point> currentPath = searchQueue.Dequeue();
                Point currentLocation = currentPath.Last();

                // Where can we go from here that we haven't been before on this path?
                Point[] validMoves = map.GetAccessibleLocationsFrom(currentLocation).Where(x => !currentPath.Contains(x)).ToArray();

                if (validMoves.Length == 0)
                {
                    // This path is tapped out
                    paths.Add(currentPath);
                }
                else
                {
                    // Continue searching each of these paths
                    foreach (Point nextMove in validMoves)
                    {
                        searchQueue.Enqueue(new List<Point>(currentPath) { nextMove });
                    }
                }
            }

            // We may have multiple paths to the same room. To deal with this, we get
            // the shortest path to each room in the results.
            IEnumerable<List<Point>> shortestPathsToEachRoom = paths.GroupBy(x => x.Last()).Select(g => g.OrderBy(p => p.Count).First());
            List<Point> furthestRoom = shortestPathsToEachRoom.OrderByDescending(x => x.Count).First();
            return (furthestRoom.Count - 1).ToString();
        }
    }
}
