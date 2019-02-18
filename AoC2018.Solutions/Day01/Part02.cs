namespace AoC2018.Solutions.Day01
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part02 : ISolution
    {
        public string Solve(string input)
        {
            int[] items = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
            var seenFrequencies = new Dictionary<int, bool>();

            int i = 0;
            int currentFrequency = 0;

            while (true)
            {
                seenFrequencies[currentFrequency] = true;

                currentFrequency += items[i];

                if (seenFrequencies.ContainsKey(currentFrequency))
                {
                    return currentFrequency.ToString();
                }

                i = (i + 1) % items.Length;
            }
        }
    }
}
