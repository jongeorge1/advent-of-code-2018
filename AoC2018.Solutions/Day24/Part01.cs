namespace AoC2018.Solutions.Day24
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            var inputs = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();

            int infectionIndex = inputs.IndexOf("Infection:");

            var immuneSystem = inputs.Skip(1).Take(infectionIndex - 1).Select((x, i) => new Group(i, "immune", x)).ToList();
            var infection = inputs.Skip(infectionIndex + 1).Select((x, i) => new Group(i, "infection", x)).ToList();

            while (immuneSystem.ContainsUnits() && infection.ContainsUnits())
            {
                List<(Group Attacker, Group Defender)> infectionTargetAllocations = infection.SelectTargets(immuneSystem);
                List<(Group Attacker, Group Defender)> immuneSystemTargetAllocations = immuneSystem.SelectTargets(infection);
                var allAllocations = infectionTargetAllocations.Union(immuneSystemTargetAllocations).OrderByDescending(x => x.Attacker.Initiative).ToList();

                foreach ((Group Attacker, Group Defender) allocation in allAllocations)
                {
                    allocation.Attacker.Attack(allocation.Defender);
                }
            }

            return (immuneSystem.TotalUnits() + infection.TotalUnits()).ToString();
        }
    }
}
