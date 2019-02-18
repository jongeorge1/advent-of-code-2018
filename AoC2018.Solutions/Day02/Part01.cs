namespace AoC2018.Solutions.Day02
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Part01 : ISolution
    {
        public string Solve(string input)
        {
            IEnumerable<List<IGrouping<char, char>>> boxIds = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToCharArray().GroupBy(c => c).ToList());

            int exactlyTwo = boxIds.Count(x => x.Any(c => c.Count() == 2));
            int exactlyThree = boxIds.Count(x => x.Any(c => c.Count() == 3));

            return (exactlyThree * exactlyTwo).ToString();
        }
    }
}
