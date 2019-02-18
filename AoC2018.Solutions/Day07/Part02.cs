namespace AoC2018.Solutions.Day07
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string input) => this.Solve(input, 5, 60);

        public string Solve(string input, int workers, int baseStepTime)
        {
            (char Step, char PreRequisite)[] statements = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(this.ParseStatement).ToArray();
            char[] allSteps = statements.Select(x => x.Step).Union(statements.Select(x => x.PreRequisite)).Distinct().ToArray();
            var stepsWithDependencies = allSteps.ToDictionary(x => x, x => statements.Where(s => s.Step == x).Select(s => s.PreRequisite).ToArray());

            var resolvedSteps = new List<char>();
            var inProgressSteps = new List<(char Step, int CompletionTime)>();
            int second = 0;

            do
            {
                // Complete any in progress steps that are done
                foreach ((char Step, int CompletionTime) current in inProgressSteps)
                {
                    if (current.CompletionTime < second)
                    {
                        resolvedSteps.Add(current.Step);
                    }
                }

                inProgressSteps = inProgressSteps.Where(x => !resolvedSteps.Contains(x.Step)).ToList();

                // If we have any space, take the next available step
                while (inProgressSteps.Count < workers)
                {
                    char nextStep = this.GetNextAvailableStep(stepsWithDependencies, resolvedSteps, inProgressSteps);

                    if (nextStep == default(char))
                    {
                        // No available steps
                        break;
                    }

                    int nextStepFinishedBy = second + baseStepTime + nextStep - 'A';
                    inProgressSteps.Add((nextStep, nextStepFinishedBy));
                }

                second++;
            }
            while (resolvedSteps.Count != allSteps.Length);

            // We will have counted on to the next second after completion...
            return (second - 1).ToString();
        }

        private char GetNextAvailableStep(Dictionary<char, char[]> stepsWithDependencies, List<char> resolvedSteps, List<(char Step, int CompletionTime)> inProgressSteps)
        {
            return stepsWithDependencies.Where(x => !resolvedSteps.Contains(x.Key) && !inProgressSteps.Any(p => p.Step == x.Key) && x.Value.All(y => resolvedSteps.Contains(y))).Select(x => x.Key).OrderBy(x => x).FirstOrDefault();
        }

        private (char Step, char PreRequisite) ParseStatement(string statement)
        {
            string[] components = statement.Split(' ');
            return (components[7][0], components[1][0]);
        }
    }
}
