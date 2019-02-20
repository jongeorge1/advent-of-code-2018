namespace AoC2018.Solutions.Day14
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            var recipies = new List<int>(25000000) { 3, 7 };
            var inputNumbers = input.ToCharArray().Select(x => int.Parse(x.ToString())).ToList();

            int[] elves = new[] { 0, 1 };
            int iterations = 0;

            while (true)
            {
                // Generate new recipies
                int combinedScore = elves.Sum(x => recipies[x]);

                int tens = combinedScore / 10;
                int ones = combinedScore % 10;

                if (tens != 0)
                {
                    recipies.Add(tens);
                }

                recipies.Add(ones);

                // Move
                for (int i = 0; i < elves.Length; i++)
                {
                    int x = elves[i];
                    elves[i] = (x + 1 + recipies[x]) % recipies.Count;
                }

                iterations++;

                if (iterations % 1000000 == 0)
                {
                    // See if the target string is in there...
                    int startAt = Math.Max(0, iterations - 1000050);
                    int index = recipies.FindIndex(inputNumbers, startAt);

                    if (index != -1)
                    {
                        return index.ToString();
                    }
                }
            }
        }
    }
}
