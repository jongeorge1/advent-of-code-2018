namespace AoC2018.Solutions.Day24
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            var inputs = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();

            int infectionIndex = inputs.IndexOf("Infection:");

            string[] immuneSystem = inputs.Skip(1).Take(infectionIndex - 1).ToArray();
            string[] infection = inputs.Skip(infectionIndex + 1).ToArray();

            var boost = 25;
            int remainingImmuneSystemUnits = 0;

            do
            {
                boost += 1;
                remainingImmuneSystemUnits = this.RunSimulation(immuneSystem, infection, boost);

                Console.WriteLine($"Boost: {boost}, Remaining immune system units: {remainingImmuneSystemUnits}");
            }
            while (remainingImmuneSystemUnits == 0);

            return remainingImmuneSystemUnits.ToString();
        }

        private int RunSimulation(string[] immuneSystemData, string[] infectionData, int boost)
        {
            var immuneSystem = immuneSystemData.Select((x, i) => new Group(i, "immune", x)).ToList();
            var infection = infectionData.Select((x, i) => new Group(i, "infection", x)).ToList();

            immuneSystem.Boost(boost);
            int totalUnitsOnLastIteration = immuneSystem.TotalUnits() + infection.TotalUnits();

            while (immuneSystem.ContainsUnits() && infection.ContainsUnits())
            {
                List<(Group Attacker, Group Defender)> infectionTargetAllocations = infection.SelectTargets(immuneSystem);
                List<(Group Attacker, Group Defender)> immuneSystemTargetAllocations = immuneSystem.SelectTargets(infection);
                var allAllocations = infectionTargetAllocations.Union(immuneSystemTargetAllocations).OrderByDescending(x => x.Attacker.Initiative).ToList();

                if (allAllocations.Count == 0)
                {
                    // Stalemate.
                    return 0;
                }

                foreach ((Group Attacker, Group Defender) allocation in allAllocations)
                {
                    allocation.Attacker.Attack(allocation.Defender);
                }

                int totalUnitsRemaining = immuneSystem.TotalUnits() + infection.TotalUnits();
                if (totalUnitsOnLastIteration == totalUnitsRemaining)
                {
                    // No units lost - stalemate
                    return 0;
                }

                totalUnitsOnLastIteration = totalUnitsRemaining;
            }

            return immuneSystem.TotalUnits();
        }
    }
}
