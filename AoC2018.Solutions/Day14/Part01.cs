namespace AoC2018.Solutions.Day14
{
    using System.Collections.Generic;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            int targetRecipeCount = int.Parse(input);
            var recipies = new List<int>(targetRecipeCount + 10) { 3, 7 };

            int[] elves = new[] { 0, 1 };

            while (recipies.Count < targetRecipeCount + 10)
            {
                // Generate new recipies
                int combinedScore = elves.Sum(x => recipies[x]);
                int[] combinedScoreDigits = combinedScore.ToString().ToCharArray().Select(x => int.Parse(x.ToString())).ToArray();
                recipies.AddRange(combinedScoreDigits);

                // Move
                elves = elves.Select(x => (x + 1 + recipies[x]) % recipies.Count).ToArray();
            }

            // Now extract the last 10 recipies
            List<int> next10 = recipies.GetRange(targetRecipeCount, 10);
            return string.Join(string.Empty, next10);
        }
    }
}
