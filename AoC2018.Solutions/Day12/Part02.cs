namespace AoC2018.Solutions.Day12
{
    using System;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            string[] inputLines = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var state = State.Parse(inputLines[0].Substring(15));

            string[] ruleLines = new string[inputLines.Length - 1];
            Array.Copy(inputLines, 1, ruleLines, 0, ruleLines.Length);

            Rule[] rules = ruleLines.Select(Rule.Parse).ToArray();

            // A little playing around has shown that after a while, the
            // increment for each generation stabilises. So all we need to do
            // is wait for that to happen, then use the stabilised increment
            // to immediately calculate the score after the remaining
            // generations.
            // We'll decide that the increment is stable when it's been the
            // same for 100 generations. This is probably massive overkill.
            int lastResult = state.PotsContainingPlants.Sum();
            int lastIncrement = 0;
            int stableGenerations = 0;
            int generation = 0;

            while (true)
            {
                generation++;
                state = state.Apply(rules);
                int result = state.PotsContainingPlants.Sum();
                int increment = result - lastResult;

                if (increment == lastIncrement)
                {
                    stableGenerations++;
                }
                else
                {
                    stableGenerations = 0;
                    lastIncrement = increment;
                }

                lastResult = result;

                if (stableGenerations == 100)
                {
                    break;
                }
            }

            // We need to get to 50,000,000...
            long remainingGenerations = 50000000000 - generation;
            long remainingIncrement = remainingGenerations * lastIncrement;
            long total = lastResult + remainingIncrement;

            return total.ToString();
        }
    }
}
