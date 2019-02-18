namespace AoC2018.Solutions.Day05
{
    using System.Collections.Generic;
    using System.Linq;

    public static class PolymerReactor
    {
        public static string React(string polymer)
        {
            bool changed = false;
            var current = polymer.ToCharArray().ToList();

            do
            {
                changed = false;

                int i = 1;

                while (i < current.Count)
                {
                    char first = current[i - 1];
                    char second = current[i];

                    if (first != second && char.ToUpper(first) == char.ToUpper(second))
                    {
                        current.RemoveRange(i - 1, 2);
                        changed = true;
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            while (changed);

            return new string(current.ToArray());
        }
    }
}
