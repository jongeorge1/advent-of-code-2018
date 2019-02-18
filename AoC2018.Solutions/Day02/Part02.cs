namespace AoC2018.Solutions.Day02
{
    using System;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            var boxIds = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (string outer in boxIds)
            {
                foreach (string inner in boxIds)
                {
                    if (outer == inner)
                    {
                        continue;
                    }

                    var differences = Enumerable.Range(0, outer.Length).Where(x => outer[x] != inner[x]).ToList();

                    if (differences.Count == 1)
                    {
                        return outer.Substring(0, differences[0]) + outer.Substring(differences[0] + 1);
                    }
                }
            }

            return "Failed to find a solution";
        }
    }
}
