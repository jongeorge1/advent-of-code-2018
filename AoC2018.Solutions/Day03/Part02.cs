namespace AoC2018.Solutions.Day03
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            var claims = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(Claim.FromString).ToList();
            var overlaps = claims.ToDictionary(x => x, _ => new List<Claim>());

            foreach (Claim outer in claims)
            {
                foreach (Claim inner in claims)
                {
                    if (outer == inner || overlaps[outer].Contains(inner))
                    {
                        continue;
                    }

                    if (outer.OverlapsWith(inner))
                    {
                        overlaps[outer].Add(inner);
                        overlaps[inner].Add(outer);
                    }
                }
            }

            return overlaps.Keys.Single(x => overlaps[x].Count == 0).Number.ToString();
        }
    }
}
