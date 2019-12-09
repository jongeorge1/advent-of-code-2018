namespace AoC2018.Solutions.Day22
{
    using System.Collections.Generic;
    using System.Linq;
    using Priority_Queue;

    public partial class Part02 : ISolution
    {
        public string Solve(string input)
        {
            // The question tells us we might have to go past the target in the X and/or
            // Y direction so I added the ability to specify padding to the map. I initially
            // tried with a padding of 10 but it gave the wrong answer. 20 gave me the right
            // answer. Trial an error gave me 16 as the correct padding to use, which takes
            // 0.5 - 1sec less time to execute than 20.
            var map = Map.FromInput(input, 16);

            // We need to find the fastest path to the destination. This is not necessarily
            // the shortest path. This can be done with yet another breadth first search.
            // The difference between this and the previous implementations is we're not
            // just looking at number of steps to determine the shortest path here, we're
            // looking at a separate metric (time) to determine the shortest path, and
            // also the tool we have when we reach a square is important. Note that it's
            // highly likely that Djikstra's algorithm would also be a good choice here.
            var paths = new List<List<(int X, int Y, Tools equippedTool, int timeTaken)>>();

            // Rather than just tracking the squares we've visited, we need to track the
            // equipped tool when we visited and how long it took to get here - two potential
            // paths could visit the same square at different times with different tools and
            // we won't know which is best until we've established the entire path - unless
            // we've ended up at the same place with the same tool already in less time.
            var shortestTimes = new Dictionary<(int X, int Y, Tools equippedTool), int>();

            // Today has introduced me to the concept of the "priority queue", which is
            // essentially a sorted queue. Using this type of queue is the difference between
            // an execution time of many minutes and a few seconds. Essentially, by keeping the
            // paths that have taken the shortest time so far at the front of the queue, we
            // have more likelihood of reaching sqaures in the shortest time, which means we
            // get to discard many more potential paths at an earlier stage than if we use a
            // normal queue.
            var searchQueue = new SimplePriorityQueue<List<(int X, int Y, Tools equippedTool, int timeTaken)>>();
            searchQueue.Enqueue(new List<(int X, int Y, Tools equippedTool, int timeTaken)> { (0, 0, Tools.Torch, 0) }, 0);

            while (searchQueue.Count != 0)
            {
                // Where are we
                List<(int X, int Y, Tools equippedTool, int timeTaken)> currentPath = searchQueue.Dequeue();
                (int X, int Y, Tools equippedTool, int timeTaken) currentSituation = currentPath.Last();

                // Are we in the right place?
                if (currentSituation.X == map.TargetLocation.X && currentSituation.Y == map.TargetLocation.Y)
                {
                    // If we're in the right place but we don't have the torch equipped, fake a move on the end
                    // of the path to reflect the need to change to the torch.
                    if (currentSituation.equippedTool != Tools.Torch)
                    {
                        currentPath.Add((currentSituation.X, currentSituation.Y, Tools.Torch, currentSituation.timeTaken + 7));
                    }

                    paths.Add(currentPath);
                }
                else
                {
                    // Have we been here before with the same tool equipped?
                    if (shortestTimes.TryGetValue((currentSituation.X, currentSituation.Y, currentSituation.equippedTool), out int currentShortestTime))
                    {
                        // Did it take less time to get here before?
                        if (currentShortestTime <= currentSituation.timeTaken)
                        {
                            // It's possible to get to this location faster via an alternate route.
                            // Abandon this path
                            continue;
                        }
                    }

                    // An alternate path got here, but in more time. Update the shortest time.
                    shortestTimes[(currentSituation.X, currentSituation.Y, currentSituation.equippedTool)] = currentSituation.timeTaken;

                    // Where can we go from here...
                    IEnumerable<(int X, int Y, Tools tool, int timeTaken)> potentialMoves = map.GetPotentialMovesFrom(currentSituation);

                    // ...that we haven't been before on this path?
                    potentialMoves = potentialMoves.Where(move => !currentPath.Any(prev => prev.X == move.X && prev.Y == move.Y));

                    foreach ((int X, int Y, Tools tool, int timeTaken) move in potentialMoves)
                    {
                        searchQueue.Enqueue(new List<(int X, int Y, Tools equippedTool, int timeTaken)>(currentPath) { move }, move.timeTaken);
                    }
                }
            }

            IOrderedEnumerable<List<(int X, int Y, Tools equippedTool, int timeTaken)>> orderedPaths = paths.OrderBy(x => x.Last().timeTaken);
            return orderedPaths.First().Last().timeTaken.ToString();
        }
    }
}
