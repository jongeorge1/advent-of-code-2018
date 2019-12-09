namespace AoC2018.Solutions.Day03
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            var claims = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(Claim.FromString).ToList();

            var locations = new Dictionary<int, int>();
            const int yOffset = 1000;

            foreach (Claim claim in claims)
            {
                for (int x = 0; x < claim.Size.Width; x++)
                {
                    for (int y = 0; y < claim.Size.Height; y++)
                    {
                        int target = claim.Position.X + x + (yOffset * (claim.Position.Y + y));
                        locations[target] = locations.ContainsKey(target) ? locations[target] + 1 : 1;
                    }
                }
            }

            return locations.Values.Count(x => x > 1).ToString();
        }
    }
}
