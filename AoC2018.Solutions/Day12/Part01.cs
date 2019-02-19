namespace AoC2018.Solutions.Day12
{
    using System;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            string[] inputLines = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var state = State.Parse(inputLines[0].Substring(15));

            string[] ruleLines = new string[inputLines.Length - 1];
            Array.Copy(inputLines, 1, ruleLines, 0, ruleLines.Length);

            Rule[] rules = ruleLines.Select(Rule.Parse).ToArray();

            for (int i = 0; i < 20; i++)
            {
                state = state.Apply(rules);
            }

            return state.PotsContainingPlants.Sum().ToString();
        }
    }
}
