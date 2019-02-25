namespace AoC2018.Solutions.Day18
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            (char[] Map, int yOffset) map = MapParser.Parse(input);

            // Predictably, part 2 is just part 1 amped up. Equally predictably, this means
            // that after a certain amount of time, the pattern will settle into a loop. This
            // means we need to:
            // 1. Work out the point at which we start iterating.
            // 2. Find the iteration length.
            // 3. Do some maths
            // 4. Iterate a few more times
            var states = new List<string>(10000);
            string mapMemo = map.Memoize();

            do
            {
                states.Add(mapMemo);

                map = map.GetNextState();
                mapMemo = map.Memoize();
            }
            while (!states.Contains(mapMemo));

            Console.WriteLine($"Iteration point hit after {states.Count} iterations");

            // We've hit a point where we've seen this state before.
            int originalIterationForThisState = states.IndexOf(mapMemo);
            int iterationLength = states.Count - originalIterationForThisState;

            const int target = 1000000000;
            int numberOfWholeIterations = (target - originalIterationForThisState) / iterationLength;
            int remainingTicksRequired = target - originalIterationForThisState - (numberOfWholeIterations * iterationLength);

            for (int i = 0; i < remainingTicksRequired; i++)
            {
                map = map.GetNextState();
            }

            int woodCount = map.Map.Count(x => x == MapAcre.Trees);
            int lumberYardCount = map.Map.Count(x => x == MapAcre.Lumberyard);

            return (woodCount * lumberYardCount).ToString();
        }
    }
}
