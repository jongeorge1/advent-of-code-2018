namespace AoC2018.Solutions.Day07
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            (string Step, string PreRequisite)[] statements = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(this.ParseStatement).ToArray();
            string[] allSteps = statements.Select(x => x.Step).Union(statements.Select(x => x.PreRequisite)).Distinct().ToArray();
            var stepsWithDependencies = allSteps.ToDictionary(x => x, x => statements.Where(s => s.Step == x).Select(s => s.PreRequisite).ToArray());

            var resolvedSteps = new List<string>();

            while (resolvedSteps.Count != allSteps.Length)
            {
                resolvedSteps.Add(this.GetNextAvailableStep(stepsWithDependencies, resolvedSteps));
            }

            return string.Concat(resolvedSteps);
        }

        private string GetNextAvailableStep(Dictionary<string, string[]> stepsWithDependencies, List<string> resolvedSteps)
        {
            return stepsWithDependencies.Where(x => !resolvedSteps.Contains(x.Key)).Where(x => x.Value.All(y => resolvedSteps.Contains(y))).Select(x => x.Key).OrderBy(x => x).First();
        }

        private (string Step, string PreRequisite) ParseStatement(string statement)
        {
            string[] components = statement.Split(' ');
            return (components[7], components[1]);
        }
    }
}
